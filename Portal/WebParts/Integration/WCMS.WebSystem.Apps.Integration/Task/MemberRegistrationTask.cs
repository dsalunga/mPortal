using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Web;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration
{
    public class MemberRegistrationTask : AgentTaskBase
    {
        public override void Execute()
        {
            //ExternalDBEntities db = new ExternalDBEntities();
            var client = MemberSoapClient.GetNewClientInstance();
            var members = MemberLink.Provider.GetList();
            var users = WebUser.GetList();
            //var memberStatuses = db.MemberStatuses;

            var xmlPath = WebHelper.MapPath(this.Attributes["XmlPath"]);
            if (!string.IsNullOrEmpty(xmlPath) && File.Exists(xmlPath))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(xmlPath);

                var groupsToAdd = WebRegistry.SelectNodeValue(MemberConstants.GroupsToAddPath);

                var nodes = xdoc.SelectNodes("//Member");
                foreach (XmlNode node in nodes)
                {
                    var memberId = DataUtil.GetId(XmlUtil.GetValue(node, "Id"));
                    var email = XmlUtil.GetValue(node, "Email");

                    // Has no MemberLink
                    if (memberId > 0 && members.FirstOrDefault(i => i.MemberId == memberId) == null)
                    {
                        var memberStatus = client.GetMembershipStatus(memberId); // memberStatuses.FirstOrDefault(i => i.MemberID == memberId);
                        if (memberStatus != null)
                        {
                            var externalId = XmlUtil.GetValue(node, "ExternalId");
                            var tempExternalId = XmlUtil.GetValue(node, "TempExternalId");
                            var userName = XmlUtil.GetValue(node, "UserName");
                            var password = XmlUtil.GetValue(node, "Password");

                            if (string.IsNullOrEmpty(externalId))
                                externalId = tempExternalId;

                            if (!string.IsNullOrEmpty(externalId))
                            {
                                var member = GetMemberProfile(externalId, (DateTime)memberStatus.MembershipDate);
                                if (member != null)
                                {
                                    if (users.FirstOrDefault(i => i.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase)) == null)
                                    {
                                        var user = SetupWebUser(member, userName, password, groupsToAdd, email);
                                        if (user != null)
                                        {
                                            var memberLink = SetupMemberLink(member, (DateTime)memberStatus.MembershipDate, user);
                                            if (memberLink == null)
                                                Logger.WriteLine("[{0}] {1} Resulting MemberLink is null for \"{2}\"", TaskName, DateTime.Now, email);
                                            else
                                                Logger.WriteLine("[{0}] {1} Registration successful for \"{2}\"!", TaskName, DateTime.Now, email);
                                        }
                                        else
                                        {
                                            Logger.WriteLine("[{0}] {1} Resulting WebUser is null for \"{2}\"", TaskName, DateTime.Now, email);
                                        }
                                    }
                                    else
                                    {
                                        Logger.WriteLine("[{0}] {1} WebUser already exists for \"{2}\"", TaskName, DateTime.Now, email);
                                    }
                                }
                            }
                            else
                            {
                                Logger.WriteLine("[{0}] {1} Empty ExternalId for \"{2}\"", TaskName, DateTime.Now, email);
                            }
                        }
                        else
                        {
                            Logger.WriteLine("[{0}] {1} Unable to get MemberStatus object for \"{2}\"", TaskName, DateTime.Now, email);
                        }
                    }
                    else
                    {
                        Logger.WriteLine("[{0}] {1} MemberLink already exists for \"{2}\"", TaskName, DateTime.Now, email);
                    }
                }
            }
            else
            {
                Logger.WriteLine("[{0}] {1} XML file does not exist \"{2}\"", TaskName, DateTime.Now, xmlPath);
            }
        }

        private Member GetMemberProfile(string externalIdNo, DateTime membershipDate)
        {
            try
            {
                MemberSoapClient memWS = new MemberSoapClient(false);
                return memWS.GetProfile(externalIdNo, membershipDate);
            }
            catch (Exception) { }

            return null;
        }

        private MemberLink SetupMemberLink(Member mem, DateTime membershipDate, WebUser user)
        {
            var link = new MemberLink();
            link.Approved = MemberAccountStatus.Approved;

            link.UserId = user.Id;
            link.MemberId = (int)mem.MemberID;
            link.ExternalIdNo = mem.EvalExternalId;
            link.Nickname = mem.NickName;
            //memLink.MobileNumber = mem.Mobile;
            //memLink.HomePhone = mem.Phone;
            link.PhotoPath = mem.PhotoPath;
            link.MembershipDate = membershipDate;
            link.LastUpdate = DateTime.Now;
            //memLink.HomeAddressLine1 = txtAddress.Text.Trim();
            //memLink.Locale = txtLocale.Text.Trim();

            // Already covered by SetupWebUser
            //user.MobileNumber = mem.Mobile;
            //user.TelephoneNumber = mem.Phone;
            //user.Update();

            var client = MemberSoapClient.GetNewClientInstance();
            link.TryPopulateGroupsFromExt(client);
            link.TryPopulateHomeAddressFromExt(client);
            link.TryPopulateProfileFromExt(client);
            link.TryStatusFromExt(client);

            link.Update();
            return link;
        }

        private WebUser SetupWebUser(Member mem, string userName, string password, string groupsToAdd, string email)
        {
            var user = new WebUser();
            user.Password = password;
            user.UserName = userName;
            user.FirstName = mem.FirstName;
            user.MiddleName = mem.MiddleName;
            user.LastName = mem.LastName;
            user.Email = email;
            user.Email2 = mem.Email;
            user.MobileNumber = mem.Mobile;
            user.TelephoneNumber = mem.Phone;
            user.ActivationKey = string.Empty;
            user.IsActive = true;
            user.Update();

            user.AddToGroups(groupsToAdd);

            //var address = string.Empty; //txtAddress.Text.Trim();
            //var homeAddress = user.GetAddress(AddressTags.Home);
            //if (homeAddress != null)
            //{
            //    if (!string.IsNullOrEmpty(homeAddress.AddressLine1))
            //        homeAddress.AddressLine1 = address;
            //}
            //else
            //{
            //    homeAddress = user.NewAddress(AddressTags.Home);
            //    homeAddress.AddressLine1 = address;
            //}

            //homeAddress.Update();

            return user;
        }
    }
}
