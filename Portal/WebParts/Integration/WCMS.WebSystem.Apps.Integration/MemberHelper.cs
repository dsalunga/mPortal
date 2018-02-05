using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using Tamir.SharpSsh;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Security;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;
using WCMS.WebSystem.Apps.Integration.CommonWS;
using WCMS.WebSystem.Apps.Integration.Ext;
using WCMS.Framework.Net;
using WCMS.WebSystem.Agent;
using WCMS.Framework.Core;
using System.Web;

namespace WCMS.WebSystem.Apps.Integration
{
    public abstract class MemberHelper
    {
        public static WebUser CreateDraftUser(Member member)
        {
            var user = new WebUser();
            user.FirstName = member.FirstName;
            user.LastName = member.LastName;
            user.Gender = char.Parse(member.Gender.Substring(0, 1).ToUpper());
            return user;
        }

        public static bool ActivateAccount(int userId, int groupId, ParameterizedWebObject paramSet, HttpContext context)
        {
            var user = WebUser.Get(userId);
            var item = WebUserGroup.Get(userId, groupId);
            if (item == null)
                item = user.AddToGroup(groupId);

            if (item != null)
            {
                var group = item.Group;
                var link = MemberLink.Provider.GetByUserId(user.Id);

                var baseAddress = paramSet.GetParameterValue("BaseAddress");
                if (string.IsNullOrEmpty(baseAddress))
                    baseAddress = WConfig.BaseAddress;

                bool accountActivatedNow = false;
                if (user.Status == AccountStatus.PENDING)
                {
                    user.Status = AccountStatus.ACTIVE;
                    user.Update();
                    accountActivatedNow = true;
                }

                if (!item.IsActive)
                {
                    item.Active = 1;
                    item.Update();
                }

                link.TryLinkToExt();

                if (user != null && group != null)
                {
                    // Send Notification Email
                    var content = FileHelper.ReadFile(context.Server.MapPath(paramSet.GetParameterValue("AccountApprovedEmailToUser")));
                    var subject = paramSet.GetParameterValue("AccountApprovedEmailToUserSubject", "Integration Portal: Congratulations! Your New Account is Now Approved!");
                    var loginUrl = paramSet.GetParameterValue("LoginUrl");
                    if (loginUrl.StartsWith("/"))
                        loginUrl = WebHelper.CombineAddress(baseAddress, loginUrl);
                    var country = link.LocaleCountry;
                    var userPhotoUrl = link.GetPhotoPathIfNull();

                    var values = new NamedValueProvider();
                    values.Add("EXTERNAL_ID_NO", link.ExternalIdNo);
                    values.Add("MEMBERSHIP_DATE", link.MembershipDate.ToString("dd-MMM-yyyy"));
                    values.Add("BASE_ADDRESS", baseAddress);
                    //provider.Add("PHOTO_URL", member.GetPhotoPath("200x200"));
                    values.Add("PHOTO_URL", WebHelper.BuildAddress(baseAddress, string.IsNullOrEmpty(userPhotoUrl) ? WConstants.NoPhotoThumb : userPhotoUrl));
                    values.Add("FIRST_NAME", user.FirstName);
                    values.Add("LAST_NAME", user.LastName);
                    values.Add("MOBILE", user.MobileNumber);
                    values.Add("EMAIL", user.Email);
                    values.Add("USER_NAME", user.UserName);
                    values.Add("PASSWORD", accountActivatedNow && user.IsPasswordExpired ? user.Password : "(YOUR CURRENT PASSWORD)");
                    values.Add("MEMBER_NAME", AccountHelper.GetPrefixedName(user, true));
                    values.Add("LOCALE", link.Locale);
                    values.Add("COUNTRY", country != null ? country.CountryName : "");
                    values.Add("URL", loginUrl);
                    content = Substituter.Substitute(content, values);

                    WebMessageQueue.CreateEmail(content, subject, user.Email).Update();
                    AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                }

                return true;
            }

            return false;
        }

        public static IEnumerable<WebUser> CollectUsers(string usersString, bool searchONEUsersAndCreate = false)
        {
            var userAccounts = new List<WebUser>();
            if (!string.IsNullOrWhiteSpace(usersString))
            {
                var accounts = usersString.Split(new char[] { AccountConstants.AccountDelimiter, WConstants.EmailSeparator });
                foreach (var listItem in accounts)
                {
                    string item = listItem.Trim();
                    WebUser user = null;

                    if (item.Contains(AccountConstants.AccountSplitter))
                    {
                        user = WebUser.GetByUniqueName(item);
                    }
                    else
                    {
                        user = WebUser.GetByEmailOrUsername(item);
                        if (user == null)
                        {
                            var link = MemberLink.Provider.Get(item);
                            if (link != null)
                                user = link.User;
                        }

                        if (searchONEUsersAndCreate && user == null) // && !item.Contains('@'))
                        {
                            var oneUser = ExtProvider.GetUserInfo(item);
                            if (oneUser != null)
                            {
                                user = new WebUser();
                                user.UserName = oneUser.UserName;
                                user.Email = oneUser.Email;
                                user.FirstName = oneUser.FirstName;
                                user.LastName = oneUser.LastName;
                                user.MiddleName = oneUser.MiddleName;
                                user.Status = AccountStatus.DRAFT;
                                user.ProviderId = AccountConstants.DefaultExternalProvider;
                                user.Update();

                                var link = new MemberLink();
                                link.UserId = user.Id;
                                link.ExternalIdNo = oneUser.ExternalId;
                                link.Update();
                            }
                        }
                    }

                    if (user != null)
                        userAccounts.Add(user);
                }
            }

            return userAccounts.Distinct(new UserIdEqualityComparer());
        }

        public static string GetChuchId(int userId)
        {
            var link = MemberLink.Provider.GetByUserId(userId);
            if (link != null)
                return link.ExternalIdNo;

            return string.Empty;
        }

        public static string SetMakeUpStatusImage(object statusObj)
        {
            int status = DataUtil.GetInt32(statusObj);

            switch (status)
            {
                case LessonReviewerSessionStatus.Draft:
                    return "/Content/Assets/Images/edit.png";

                case LessonReviewerSessionStatus.Approved:
                    return "/Content/Assets/Images/ok.png";

                case LessonReviewerSessionStatus.PendingApproval:
                    return "/Content/Assets/Images/favorite.png";

                case LessonReviewerSessionStatus.Rejected:
                    return "/Content/Assets/Images/error.png";

                default:
                    return "/Content/Assets/Images/alert.png";
            }
        }

        //public static string GetPrefixedMemberName(WebUser user)
        //{
        //    if (user != null)
        //    {
        //        if (user.Gender != GenderTypes.Unspecified)
        //            return string.Format("{0}. {1}", GetUserPrefix(user), user.FirstAndLastName);
        //        else
        //            return user.FirstAndLastName;
        //    }

        //    return "";
        //}

        //public static string GetPrefixedMemberFirstName(WebUser user)
        //{
        //    if (user != null)
        //    {
        //        if (user.Gender != GenderTypes.Unspecified)
        //            return string.Format("{0}. {1}", GetUserPrefix(user), user.FirstName);
        //        else
        //            return user.FirstName;
        //    }

        //    return "";
        //}

        //private static string GetUserPrefix(WebUser user)
        //{
        //    if (user != null)
        //        return user.Gender == GenderTypes.Male ? "Bro" : (user.Gender == GenderTypes.Female ? "Sis" : "");
        //    else
        //        return "";
        //}

        public static string GetShortService(string service)
        {
            int space = service.IndexOf(" ");
            if (space > 0)
                return service.Substring(0, space);
            return service;
        }

        public static Service GetService(int serviceId)
        {
            var services = GetServices();
            foreach (var service in services)
            {
                if (service.ServiceID == serviceId)
                    return service;
            }

            return null;
        }

        public static IEnumerable<Service> GetServices()
        {
            var cache = MemoryCache.Default;
            IEnumerable<Service> services = cache[ExtConstants.ServicesCacheKey] as IEnumerable<Service>;
            if (services == null)
            {
                var client = new CommonWSSoapClient();
                services = client.GetServices();

                var policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(6.0);

                // Fetch the file contents.
                cache.Set(ExtConstants.ServicesCacheKey, services, policy);
            }

            return services;
        }

        public static string GetShortService(int serviceId)
        {
            var service = GetService(serviceId);
            if (service != null)
                return service.ServiceCode;

            //switch (serviceId)
            //{
            //    case 1:
            //        return "PM";

            //    case 3:
            //        return "WS";

            //    case 2:
            //        return "TG";

            //    case 5:
            //        return "ITG1";

            //    case 6:
            //        return "ITG2";

            //    case 7:
            //        return "ITG3";

            //    case 4:
            //        return "CP";

            //    case 8:
            //        return "LS";
            //}

            return "NA";
        }

        public static MemberAttendance GetAttendance(int memberId, DateTime date, string serviceType)
        {
            if (memberId > 0)
            {
                var link = MemberLink.Provider.GetByMemberId(memberId);
                var client = new MemberSoapClient(false);

                var attendances = client.GetAttendances(memberId, -1, -1, date, date);
                var attendance = attendances.FirstOrDefault(i => MemberHelper.GetShortService(i.ServiceType).Equals(serviceType, StringComparison.InvariantCultureIgnoreCase));
                if (attendance != null)
                {
                    attendance.ExternalIdNo = link.ExternalIdNo;

                    return attendance;
                }
            }

            return null;
        }

        public static MakeUpSession TryRecreateSession(WContext context)
        {
            return TryRecreateSession(context.Query);
        }

        public static MakeUpSession TryRecreateSession(WQuery context)
        {
            var sessionIdString = context.Get("SessionId");
            if (!string.IsNullOrEmpty(sessionIdString))
            {
                var sessionId = DataHelper.GetLong(sessionIdString);
                if (sessionId > 0)
                {
                    try
                    {
                        var dateStarted = new DateTime(sessionId);
                        if (dateStarted > DateTime.Now.AddDays(-2))
                        {
                            var attendance = GetAttendance(context);
                            if (attendance != null)
                                return new MakeUpSession(attendance, dateStarted);
                        }
                    }
                    catch (Exception) { }
                }
            }

            return null;
        }

        public static MemberAttendance GetAttendance(WContext context)
        {
            return GetAttendance(context.Query);
        }

        public static MemberAttendance GetAttendance(WQuery context)
        {
            int memberId = -1;
            var dateString = context.Get("Date");
            var serviceType = context.Get("ServiceType");

            if (!string.IsNullOrEmpty(dateString))
            {
                var date = DataUtil.GetDateTime(dateString);
                var user = WSession.Current.User;

                var link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null)
                    memberId = link.MemberId;

                if (memberId > 0)
                {
                    var client = new MemberSoapClient(false);
                    var attendances = client.GetAttendances(memberId, -1, -1, date, date);
                    var attendance = attendances.FirstOrDefault(i => MemberHelper.GetShortService(i.ServiceType).Equals(serviceType, StringComparison.InvariantCultureIgnoreCase));
                    if (attendance != null)
                    {
                        attendance.ExternalIdNo = link.ExternalIdNo;
                        return attendance;
                    }
                }
            }

            return null;
        }

        public static string GetLocaleGroup(int userId, string defaultValue = "")
        {
            if (userId > 0)
            {
                string localeGroupsPath = MemberConstants.LocaleGroupPath;

                // Instead of getting all groups, just get the links
                var userGroups = WebUserGroup.GetByUserId(userId, 1);

                var groups = WebGroup.SelectNode(localeGroupsPath).Children;
                if (groups.Count() > 0)
                    foreach (var g in groups)
                        foreach (var ug in userGroups)
                            if (g.Id == ug.GroupId)
                                return g.Name;
            }

            return defaultValue;
        }

        public static string UpdateGlobalPassword(WebUser user, string newPassword)
        {
            try
            {
                var cmd = MemberConfig.GetEmailUnixCommand();

                var provider = new NamedValueProvider();
                provider.Add("Email", user.Email);
                provider.Add("Password", newPassword);

                var command = Substituter.Substitute(cmd.Command, provider);
                string stdOut = "";
                string stdErr = "";

                SshExec exec = new SshExec(cmd.Server, cmd.Username);
                //if (input.Pass != null) exec.Password = input.Pass;
                //if (input.IdentityFile != null) 
                exec.AddIdentityFile(cmd.PrivateKeyPath);

                //Console.Write("Connecting...");
                exec.Connect();
                //Console.WriteLine("OK");
                //Console.Write("Enter a command to execute ['Enter' to cancel]: ");

                int output = exec.RunCommand(command, ref stdOut, ref stdErr);
                //Console.WriteLine(output);

                //Console.Write("Disconnecting...");
                exec.Close();
                //Console.WriteLine("OK");

                if (output > 0)
                {
                    LogHelper.WriteLog("Zimbra Cmd: {0}, UNIX Output: {1}", command, stdErr);
                    int idx = stdErr.IndexOf("ERROR");
                    if (idx >= 0)
                        return stdErr.Substring(idx);
                    else
                        return stdErr;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                return e.Message;
            }

            return "";
        }

        public static bool IsExternalId(string id)
        {
            return !string.IsNullOrEmpty(id) && (id.Length == 8 || id.Length == 11);
        }
    }
}
