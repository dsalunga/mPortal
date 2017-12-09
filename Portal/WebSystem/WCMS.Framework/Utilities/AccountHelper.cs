using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;
using System.IO;
using System.Web.UI.WebControls;

namespace WCMS.Framework.Utilities
{
    /// <summary>
    /// Account formats: username_or_groupname, USER\username, GROUP\groupname, objectid\recordid
    /// </summary>
    public abstract class AccountHelper
    {
        public static string ToUniqueShortString(WebUser item)
        {
            return string.Format(@"{0}{2}{1}", WebObjects.WebUser, item.Id, AccountConstants.AccountSplitter);
        }

        public static string ToUniqueShortString(WebGroup item)
        {
            return string.Format(@"{0}{2}{1}", WebObjects.WebGroup, item.Id, AccountConstants.AccountSplitter);
        }

        public static string GetPrefixedName(WebUser user, bool firstNameOnly = false)
        {
            return GetPrefixedName(user, NamePrefixes.Brotherhood, firstNameOnly);
        }

        public static string GetNamePrefix(WebUser user, NamePrefixes prefixType)
        {
            if (user != null && user.Gender != GenderTypes.Unspecified)
            {
                switch (prefixType)
                {
                    case NamePrefixes.Brotherhood:
                        return user.Gender == GenderTypes.Male ? "Bro" : (user.Gender == GenderTypes.Female ? "Sis" : "");
                }
            }

            return string.Empty;
        }

        public static string GetPrefixedName(WebUser user, NamePrefixes prefixType, bool firstNameOnly = false)
        {
            if (user != null)
            {
                var prefix = GetNamePrefix(user, prefixType);

                if (!string.IsNullOrEmpty(prefix))
                    return string.Format("{0}. {1}", prefix, firstNameOnly ? user.FirstName : user.FirstAndLastName);
                else
                    return firstNameOnly ? user.FirstName : user.FirstAndLastName;
            }

            return string.Empty;
        }

        public static WebUser FindUser(string userNameOrEmail)
        {
            return Validator.IsRegexMatch(userNameOrEmail, RegexPresets.Email) ? WebUser.GetByEmail(userNameOrEmail) ?? WebUser.Get(userNameOrEmail) : WebUser.Get(userNameOrEmail) ?? WebUser.Provider.GetByEmailId(userNameOrEmail);
        }

        public static WebUser ValidateLogin(WebUser user, string password)
        {
            bool isMatch = false;
            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(password))
            {
                if (user.Password == password)
                    isMatch = true;
            }
            else if (user.Password.Equals(password, StringComparison.InvariantCulture))
                isMatch = true;

            if (isMatch)
            {
                if (user.LoginFailureCount > 0)
                {
                    user.LoginFailureCount = 0;
                    user.LastLoginFailureDate = WConstants.DateTimeMinValue;
                    user.Update();
                }

                return user;
            }

            // Not matched
            if (user.LoginFailureCount >= WebUser.LOGIN_LOCKOUT_MAX_ATTEMPT)
                user.LoginFailureCount = 1;
            else
                user.LoginFailureCount++;
            user.LastLoginFailureDate = DateTime.Now;
            user.Update();
            return null;
        }

        public static WebUser ValidateLogin(string userName, string password)
        {
            var user = FindUser(userName);
            return ValidateLogin(user, password);
        }

        public static int GetObjectId(string uniqueString)
        {
            string[] parts = uniqueString.Split(AccountConstants.AccountSplitter);

            return DataHelper.GetId(parts.First());
        }

        public static int GetRecordId(string uniqueString)
        {
            string[] parts = uniqueString.Split(AccountConstants.AccountSplitter);

            return DataHelper.GetId(parts[1]);
        }

        public static string ToAccountsString(IEnumerable<NamedWebObject> accounts, bool useUnique = true, char? delim = null)
        {
            StringBuilder sb = new StringBuilder();
            var delimiter = (delim == null ? AccountConstants.AccountDelimiter : delim.Value);
            foreach (var account in accounts)
            {
                sb.Append(useUnique ? ToUniqueString(account) : account.Name);
                sb.Append(delimiter);
                sb.Append(WConstants.CHAR_SPACE);
            }

            return sb.ToString().TrimEnd(new char[] { delimiter, WConstants.CHAR_SPACE });
        }

        public static string ToUniqueString(NamedWebObject account)
        {
            return string.Format("{0}{1}",
                account.OBJECT_ID == WebObjects.WebGroup
                    ? AccountConstants.GROUP_PREFIX : AccountConstants.USER_PREFIX,
                account.Name
            );
        }

        public static string AddAccount(string accounts, NamedWebObject account)
        {
            var items = AccountHelper.FromAccountsString(accounts);
            var item = items.Find(i => i.OBJECT_ID == account.OBJECT_ID && i.Id == account.Id);
            if (item == null)
                items.Add(account);

            return AccountHelper.ToAccountsString(items);
        }

        public static string RemoveAccount(string accounts, NamedWebObject account)
        {
            var items = FromAccountsString(accounts);
            var item = items.Find(i => i.OBJECT_ID == account.OBJECT_ID && i.Id == account.Id);
            if (item != null)
                items.Remove(item);

            return AccountHelper.ToAccountsString(items);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountsString"></param>
        /// <param name="userFormatString">Format: {0} = Id, {1} = FirstNameAndLastName</param>
        /// <returns></returns>
        public static string FormatForDisplay(string accountsString, string userFormatString)
        {
            var items = FromAccountsString(accountsString);
            var sb = new StringBuilder();

            foreach (var item in items)
            {
                if (item.OBJECT_ID == WebObjects.WebUser)
                {
                    var user = item as WebUser;
                    sb.AppendFormat("{0}, ", string.IsNullOrEmpty(userFormatString) ? user.FirstAndLastName : string.Format(userFormatString, user.Id, user.FirstAndLastName));
                }
                else
                {
                    sb.AppendFormat("{0}, ", item.Name);
                }
            }

            return sb.ToString().TrimEnd(',', ' ');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="userFormatString">Format: {0} = Id, {1} = FirstNameAndLastName</param>
        /// <returns></returns>
        public static string FormatForDisplay(NamedWebObject item, string userFormatString)
        {
            var sb = new StringBuilder();

            if (item.OBJECT_ID == WebObjects.WebUser)
            {
                var user = item as WebUser;
                sb.AppendFormat("{0}", string.IsNullOrEmpty(userFormatString) ? user.FirstAndLastName : string.Format(userFormatString, user.Id, user.FirstAndLastName));
            }
            else
            {
                sb.AppendFormat("{0}", item.Name);
            }

            return sb.ToString();
        }

        public static List<NamedWebObject> FromAccountsString(string accountsString)
        {
            List<NamedWebObject> accounts = new List<NamedWebObject>();

            if (!string.IsNullOrWhiteSpace(accountsString))
            {
                var items = accountsString.Split(new char[] { WConstants.EmailSeparator, AccountConstants.AccountDelimiter });
                foreach (var listItem in items)
                {
                    string item = listItem.Trim();
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item.Contains(AccountConstants.AccountSplitter))
                        {
                            if (item.StartsWith(AccountConstants.USER_PREFIX) || item.StartsWith(AccountConstants.GROUP_PREFIX))
                            {
                                // UniqueName
                                var group = WebGroup.GetByUniqueName(item);
                                if (group != null)
                                {
                                    accounts.Add(group);
                                }
                                else
                                {
                                    var user = WebUser.GetByUniqueName(item);
                                    if (user != null)
                                        accounts.Add(user);
                                }
                            }
                            else
                            {
                                // UniqueShortName
                                string[] parts = item.Split(AccountConstants.AccountSplitter);

                                int objectId = DataHelper.GetId(parts.First());
                                int recordId = DataHelper.GetId(parts[1]);

                                if (objectId > 0 && recordId > 0)
                                {
                                    if (objectId == WebObjects.WebUser)
                                    {
                                        var user = WebUser.Get(recordId);
                                        if (user != null)
                                            accounts.Add(user);
                                    }
                                    else if (objectId == WebObjects.WebGroup)
                                    {
                                        var group = WebGroup.Get(recordId);
                                        if (group != null)
                                            accounts.Add(group);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var group = WebGroup.Get(item);
                            if (group != null)
                            {
                                accounts.Add(group);
                            }
                            else
                            {
                                var user = WebUser.Get(item);
                                if (user != null)
                                    accounts.Add(user);
                            }
                        }
                    }
                }
            }

            return accounts.Distinct().ToList();
        }

        public static NamedWebObject FromAccountIds(int objectId, int recordId)
        {
            if (objectId == WebObjects.WebGroup)
                return WebGroup.Get(recordId);
            else if (objectId == WebObjects.WebUser)
                return WebUser.Get(recordId);

            return null;
        }

        public static NamedWebObject FromAccountString(string accountString)
        {
            if (!string.IsNullOrWhiteSpace(accountString))
            {
                var items = accountString.Split(new char[] { WConstants.EmailSeparator, AccountConstants.AccountDelimiter });
                foreach (var listItem in items)
                {
                    string item = listItem.Trim();
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item.Contains(AccountConstants.AccountSplitter))
                        {
                            if (item.StartsWith(AccountConstants.USER_PREFIX) || item.StartsWith(AccountConstants.GROUP_PREFIX))
                            {
                                // UniqueName
                                var group = WebGroup.GetByUniqueName(item);
                                if (group != null)
                                {
                                    return group;
                                }
                                else
                                {
                                    var user = WebUser.GetByUniqueName(item);
                                    if (user != null)
                                        return user;
                                }
                            }
                            else
                            {
                                // UniqueShortName
                                string[] parts = item.Split(AccountConstants.AccountSplitter);

                                int objectId = DataHelper.GetId(parts.First());
                                int recordId = DataHelper.GetId(parts[1]);

                                if (objectId > 0 && recordId > 0)
                                {
                                    if (objectId == WebObjects.WebUser)
                                    {
                                        var user = WebUser.Get(recordId);
                                        if (user != null)
                                            return user;
                                    }
                                    else if (objectId == WebObjects.WebGroup)
                                    {
                                        var group = WebGroup.Get(recordId);
                                        if (group != null)
                                            return group;
                                    }
                                }
                            }
                        }
                        else
                        {
                            var group = WebGroup.Get(item);
                            if (group != null)
                            {
                                return group;
                            }
                            else
                            {
                                var user = WebUser.Get(item);
                                if (user != null)
                                    return user;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public static NamedWebObject FromUniqueName(string uniqueName)
        {
            string[] parts = uniqueName.Split(AccountConstants.AccountSplitter);
            if (parts.Length == 2)
            {
                var prefix = parts.First();
                var name = parts[1];

                if (uniqueName.StartsWith(AccountConstants.USER_PREFIX, StringComparison.InvariantCultureIgnoreCase))
                {
                    return WebUser.Get(name);
                }
                else if (uniqueName.StartsWith(AccountConstants.GROUP_PREFIX, StringComparison.InvariantCultureIgnoreCase))
                {
                    return WebGroup.Get(name);
                }
                else
                {
                    var objectId = DataHelper.GetId(prefix);
                    if (objectId > 0)
                    {
                        var recordId = DataHelper.GetId(name);
                        if (recordId > 0)
                        {
                            if (objectId == WebObjects.WebGroup)
                                return WebGroup.Get(recordId);
                            else if (objectId == WebObjects.WebUser)
                                return WebUser.Get(recordId);
                        }
                    }
                }
            }

            return null;
        }

        public static NamedWebObject FromUniqueShortString(string uniqueString)
        {
            string[] parts = uniqueString.Split(AccountConstants.AccountSplitter);

            int objectId = DataHelper.GetId(parts.First());
            int recordId = DataHelper.GetId(parts[1]);

            if (objectId == WebObjects.WebGroup)
                return WebGroup.Get(recordId);
            else if (objectId == WebObjects.WebUser)
                return WebUser.Get(recordId);
            else
                return null;
        }

        public const string SEARCH = "/SEARCH:";
        public static IEnumerable<WebUser> Search<T>(string search) where T : WebUser
        {
            string key = search.ToLower().Trim();
            if (key.StartsWith(search, StringComparison.InvariantCultureIgnoreCase))
                key = key.Substring(SEARCH.Length).Trim();

            var result = from i in WebUser.GetList()
                         where
                                i.UserName.ToLower().Contains(key) ||
                                i.FirstAndLastName.ToLower().Contains(key) ||
                                i.Email.ToLower().Contains(key)
                         select i;

            return result;
        }

        /// <summary>
        /// Supports UniqueName and UniqueShortName
        /// </summary>
        /// <param name="emailUserGroupsString"></param>
        /// <param name="includeEmailsOnly"></param>
        /// <returns></returns>
        public static List<MailAddress> CollectEmails(string emailUserGroupsString, bool includeEmailsOnly = true)
        {
            List<MailAddress> emails = new List<MailAddress>();

            if (!string.IsNullOrWhiteSpace(emailUserGroupsString))
            {
                var items = emailUserGroupsString.Split(new char[] { WConstants.EmailSeparator, AccountConstants.AccountDelimiter });
                foreach (var listItem in items)
                {
                    string item = listItem.Trim();
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item.Contains(AccountConstants.AccountSplitter))
                        {
                            if (item.StartsWith(AccountConstants.USER_PREFIX) || item.StartsWith(AccountConstants.GROUP_PREFIX))
                            {
                                // UniqueName
                                var group = WebGroup.GetByUniqueName(item);
                                if (group != null)
                                {
                                    var users = group.Users;
                                    foreach (var user in users)
                                        emails.Add(new MailAddress(user.Email, user.FirstAndLastName));
                                }
                                else
                                {
                                    var user = WebUser.GetByUniqueName(item);
                                    if (user != null)
                                        emails.Add(new MailAddress(user.Email, user.FirstAndLastName));
                                }
                            }
                            else
                            {
                                // UniqueShortName
                                string[] parts = item.Split(AccountConstants.AccountSplitter);

                                int objectId = DataHelper.GetId(parts.First());
                                int recordId = DataHelper.GetId(parts[1]);

                                if (objectId > 0 && recordId > 0)
                                {
                                    if (objectId == WebObjects.WebUser)
                                    {
                                        var user = WebUser.Get(recordId);
                                        if (user != null)
                                            emails.Add(new MailAddress(user.Email, user.FirstAndLastName));
                                    }
                                    else if (objectId == WebObjects.WebGroup)
                                    {
                                        var group = WebGroup.Get(recordId);
                                        if (group != null)
                                        {
                                            var users = group.Users;
                                            foreach (var user in users)
                                                emails.Add(new MailAddress(user.Email, user.FirstAndLastName));
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var group = WebGroup.Get(item);
                            if (group != null)
                            {
                                var users = group.Users;
                                foreach (var user in users)
                                    emails.Add(new MailAddress(user.Email, user.FirstAndLastName));
                            }
                            else
                            {
                                var user = WebUser.Get(item);
                                if (user != null)
                                    emails.Add(new MailAddress(user.Email, user.FirstAndLastName));
                                else if (includeEmailsOnly && Validator.IsRegexMatch(item, RegexPresets.Email))
                                    emails.Add(new MailAddress(item));
                            }
                        }
                    }
                }
            }

            return emails.Distinct().ToList();
        }

        public static List<UserDataTag> CollectTaggedEmails(string emailUserGroupsString, bool includeEmailsOnly = true)
        {
            List<UserDataTag> emails = new List<UserDataTag>();

            if (!string.IsNullOrWhiteSpace(emailUserGroupsString))
            {
                var items = emailUserGroupsString.Split(new char[] { WConstants.EmailSeparator, AccountConstants.AccountDelimiter });
                foreach (var listItem in items)
                {
                    string item = listItem.Trim();
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item.Contains(AccountConstants.AccountSplitter))
                        {
                            if (item.StartsWith(AccountConstants.USER_PREFIX) || item.StartsWith(AccountConstants.GROUP_PREFIX))
                            {
                                // UniqueName
                                var group = WebGroup.GetByUniqueName(item);
                                if (group != null)
                                {
                                    var users = group.Users;
                                    foreach (var user in users)
                                        emails.Add(new UserDataTag(user.Email, user));
                                }
                                else
                                {
                                    var user = WebUser.GetByUniqueName(item);
                                    if (user != null)
                                        emails.Add(new UserDataTag(user.Email, user));
                                }
                            }
                            else
                            {
                                // UniqueShortName
                                string[] parts = item.Split(AccountConstants.AccountSplitter);

                                int objectId = DataHelper.GetId(parts.First());
                                int recordId = DataHelper.GetId(parts[1]);

                                if (objectId > 0 && recordId > 0)
                                {
                                    if (objectId == WebObjects.WebUser)
                                    {
                                        var user = WebUser.Get(recordId);
                                        if (user != null)
                                            emails.Add(new UserDataTag(user.Email, user));
                                    }
                                    else if (objectId == WebObjects.WebGroup)
                                    {
                                        var group = WebGroup.Get(recordId);
                                        if (group != null)
                                        {
                                            var users = group.Users;
                                            foreach (var user in users)
                                                emails.Add(new UserDataTag(user.Email, user));
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var group = WebGroup.Get(item);
                            if (group != null)
                            {
                                var users = group.Users;
                                foreach (var user in users)
                                    emails.Add(new UserDataTag(user.Email, user));
                            }
                            else
                            {
                                var user = WebUser.Get(item);
                                if (user != null)
                                    emails.Add(new UserDataTag(user.Email, user));
                                else if (includeEmailsOnly && Validator.IsRegexMatch(item, RegexPresets.Email))
                                    emails.Add(new UserDataTag(item));
                            }
                        }
                    }
                }
            }

            return emails.Distinct(new UserTagEqualityComparer()).ToList();
        }

        public static List<string> CollectEmailString(string emailUserGroupsString, bool includeEmailsOnly = true)
        {
            List<string> emails = new List<string>();

            if (!string.IsNullOrWhiteSpace(emailUserGroupsString))
            {
                var items = emailUserGroupsString.Split(new char[] { WConstants.EmailSeparator, AccountConstants.AccountDelimiter });
                foreach (var listItem in items)
                {
                    string item = listItem.Trim();
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item.Contains(AccountConstants.AccountSplitter))
                        {
                            if (item.StartsWith(AccountConstants.USER_PREFIX) || item.StartsWith(AccountConstants.GROUP_PREFIX))
                            {
                                // UniqueName
                                var group = WebGroup.GetByUniqueName(item);
                                if (group != null)
                                {
                                    var users = group.Users;
                                    foreach (var user in users)
                                        emails.Add(user.Email);
                                }
                                else
                                {
                                    var user = WebUser.GetByUniqueName(item);
                                    if (user != null)
                                        emails.Add(user.Email);
                                }
                            }
                            else
                            {
                                // UniqueShortName
                                string[] parts = item.Split(AccountConstants.AccountSplitter);

                                int objectId = DataHelper.GetId(parts.First());
                                int recordId = DataHelper.GetId(parts[1]);

                                if (objectId > 0 && recordId > 0)
                                {
                                    if (objectId == WebObjects.WebUser)
                                    {
                                        var user = WebUser.Get(recordId);
                                        if (user != null)
                                            emails.Add(user.Email);
                                    }
                                    else if (objectId == WebObjects.WebGroup)
                                    {
                                        var group = WebGroup.Get(recordId);
                                        if (group != null)
                                        {
                                            var users = group.Users;
                                            foreach (var user in users)
                                                emails.Add(user.Email);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var group = WebGroup.Get(item);
                            if (group != null)
                            {
                                var users = group.Users;
                                foreach (var user in users)
                                    emails.Add(user.Email);
                            }
                            else
                            {
                                var user = WebUser.Get(item);
                                if (user != null)
                                    emails.Add(user.Email);
                                else if (includeEmailsOnly && Validator.IsRegexMatch(item, RegexPresets.Email))
                                    emails.Add(item);
                            }
                        }
                    }
                }
            }

            return emails.Distinct().ToList();
        }

        /// <summary>
        /// Supports UniqueName and UniqueShortName
        /// </summary>
        /// <param name="mobileNumberUserGroupsString"></param>
        /// <param name="includeMobileNumbersOnly"></param>
        /// <returns></returns>
        public static List<string> CollectMobileNumbers(string mobileNumberUserGroupsString, bool includeMobileNumbersOnly = true)
        {
            List<string> numbers = new List<string>();

            if (!string.IsNullOrWhiteSpace(mobileNumberUserGroupsString))
            {
                var items = mobileNumberUserGroupsString.Split(new char[] { WConstants.EmailSeparator, AccountConstants.AccountDelimiter });
                foreach (var listItem in items)
                {
                    string item = listItem.Trim();
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item.Contains(AccountConstants.AccountSplitter))
                        {
                            if (item.StartsWith(AccountConstants.USER_PREFIX) || item.StartsWith(AccountConstants.GROUP_PREFIX))
                            {
                                // UniqueName
                                var group = WebGroup.GetByUniqueName(item);
                                if (group != null)
                                {
                                    var users = group.Users;
                                    foreach (var user in users)
                                        if (!string.IsNullOrEmpty(user.MobileNumber))
                                            numbers.Add(user.MobileNumber);
                                }
                                else
                                {
                                    var user = WebUser.GetByUniqueName(item);
                                    if (user != null && !string.IsNullOrEmpty(user.MobileNumber))
                                        numbers.Add(user.MobileNumber);
                                }
                            }
                            else
                            {
                                // UniqueShortName
                                string[] parts = item.Split(AccountConstants.AccountSplitter);

                                int objectId = DataHelper.GetId(parts.First());
                                int recordId = DataHelper.GetId(parts[1]);

                                if (objectId > 0 && recordId > 0)
                                {
                                    if (objectId == WebObjects.WebUser)
                                    {
                                        var user = WebUser.Get(recordId);
                                        if (user != null && !string.IsNullOrEmpty(user.MobileNumber))
                                            numbers.Add(user.MobileNumber);
                                    }
                                    else if (objectId == WebObjects.WebGroup)
                                    {
                                        var group = WebGroup.Get(recordId);
                                        if (group != null)
                                        {
                                            var users = group.Users;
                                            foreach (var user in users)
                                                if (!string.IsNullOrEmpty(user.MobileNumber))
                                                    numbers.Add(user.MobileNumber);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var group = WebGroup.Get(item);
                            if (group != null)
                            {
                                var users = group.Users;
                                foreach (var user in users)
                                    if (!string.IsNullOrEmpty(user.MobileNumber))
                                        numbers.Add(user.MobileNumber);
                            }
                            else
                            {
                                var user = WebUser.Get(item);
                                if (user != null)
                                {
                                    if (!string.IsNullOrEmpty(user.MobileNumber))
                                        numbers.Add(user.MobileNumber);
                                }
                                else if (includeMobileNumbersOnly && Validator.IsRegexMatch(item, RegexPresets.MobileNumber))
                                {
                                    numbers.Add(item);
                                }
                            }
                        }
                    }
                }
            }

            return numbers.Distinct().ToList();
        }

        public static List<UserDataTag> CollectTaggedMobileNumbers(string mobileNumberUserGroupsString, bool includeMobileNumbersOnly = true)
        {
            List<UserDataTag> numbers = new List<UserDataTag>();

            if (!string.IsNullOrWhiteSpace(mobileNumberUserGroupsString))
            {
                var items = mobileNumberUserGroupsString.Split(new char[] { WConstants.EmailSeparator, AccountConstants.AccountDelimiter });
                foreach (var listItem in items)
                {
                    string item = listItem.Trim();
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item.Contains(AccountConstants.AccountSplitter))
                        {
                            if (item.StartsWith(AccountConstants.USER_PREFIX) || item.StartsWith(AccountConstants.GROUP_PREFIX))
                            {
                                // UniqueName
                                var group = WebGroup.GetByUniqueName(item);
                                if (group != null)
                                {
                                    var users = group.Users;
                                    foreach (var user in users)
                                        if (!string.IsNullOrEmpty(user.MobileNumber))
                                            numbers.Add(new UserDataTag(user.MobileNumber, user));
                                }
                                else
                                {
                                    var user = WebUser.GetByUniqueName(item);
                                    if (user != null && !string.IsNullOrEmpty(user.MobileNumber))
                                        numbers.Add(new UserDataTag(user.MobileNumber, user));
                                }
                            }
                            else
                            {
                                // UniqueShortName
                                string[] parts = item.Split(AccountConstants.AccountSplitter);

                                int objectId = DataHelper.GetId(parts.First());
                                int recordId = DataHelper.GetId(parts[1]);

                                if (objectId > 0 && recordId > 0)
                                {
                                    if (objectId == WebObjects.WebUser)
                                    {
                                        var user = WebUser.Get(recordId);
                                        if (user != null && !string.IsNullOrEmpty(user.MobileNumber))
                                            numbers.Add(new UserDataTag(user.MobileNumber, user));
                                    }
                                    else if (objectId == WebObjects.WebGroup)
                                    {
                                        var group = WebGroup.Get(recordId);
                                        if (group != null)
                                        {
                                            var users = group.Users;
                                            foreach (var user in users)
                                                if (!string.IsNullOrEmpty(user.MobileNumber))
                                                    numbers.Add(new UserDataTag(user.MobileNumber, user));
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var group = WebGroup.Get(item);
                            if (group != null)
                            {
                                var users = group.Users;
                                foreach (var user in users)
                                    if (!string.IsNullOrEmpty(user.MobileNumber))
                                        numbers.Add(new UserDataTag(user.MobileNumber, user));
                            }
                            else
                            {
                                var user = WebUser.Get(item);
                                if (user != null)
                                {
                                    if (!string.IsNullOrEmpty(user.MobileNumber))
                                        numbers.Add(new UserDataTag(user.MobileNumber, user));
                                }
                                else if (includeMobileNumbersOnly && Validator.IsRegexMatch(item, RegexPresets.MobileNumber))
                                {
                                    numbers.Add(new UserDataTag(item));
                                }
                            }
                        }
                    }
                }
            }

            return numbers.Distinct(new UserTagEqualityComparer()).ToList();
        }

        public static List<WebUser> CollectUsersHierarchal(string userGroupsString)
        {
            List<WebUser> userAccounts = new List<WebUser>();

            if (!string.IsNullOrWhiteSpace(userGroupsString))
            {
                var accounts = userGroupsString.Split(new char[] { AccountConstants.AccountDelimiter, WConstants.EmailSeparator });
                foreach (var listItem in accounts)
                {
                    string item = listItem.Trim();
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item.Contains(AccountConstants.AccountSplitter))
                        {
                            var user = WebUser.GetByUniqueName(item);
                            if (user != null)
                            {
                                userAccounts.Add(user);
                            }
                            else
                            {
                                var group = WebGroup.GetByUniqueName(item);
                                if (group != null)
                                {
                                    var users = group.Users;
                                    foreach (var u in users)
                                        userAccounts.Add(u);
                                }
                            }

                            //var group = WebGroup.GetByUniqueName(item);
                            //if (group != null)
                            //{
                            //    var users = group.Users;
                            //    foreach (var user in users)
                            //        userAccounts.Add(user);
                            //}
                            //else
                            //{
                            //    var user = WebUser.GetByUniqueName(item);
                            //    if (user != null)
                            //        userAccounts.Add(user);
                            //}
                        }
                        else
                        {
                            var user = WebUser.Get(item);
                            if (user != null)
                            {
                                userAccounts.Add(user);
                            }
                            else
                            {
                                var group = WebGroup.Get(item);
                                if (group != null)
                                {
                                    var users = group.Users;
                                    foreach (var u in users)
                                        userAccounts.Add(u);
                                }
                            }

                            //var group = WebGroup.Get(item);
                            //if (group != null)
                            //{
                            //    var users = group.Users;
                            //    foreach (var user in users)
                            //        userAccounts.Add(user);
                            //}
                            //else
                            //{
                            //    var user = WebUser.Get(item);
                            //    if (user != null)
                            //        userAccounts.Add(user);
                            //}
                        }
                    }
                }
            }

            return userAccounts;
        }

        public static List<WebUser> CollectUsers(string usersString)
        {
            var userAccounts = new List<WebUser>();
            if (!string.IsNullOrWhiteSpace(usersString))
            {
                var accounts = usersString.Split(new char[] { AccountConstants.AccountDelimiter, WConstants.EmailSeparator });
                foreach (var listItem in accounts)
                {
                    string item = listItem.Trim();
                    WebUser user = item.Contains(AccountConstants.AccountSplitter) ?
                        WebUser.GetByUniqueName(item) : WebUser.GetByEmailOrUsername(item);

                    if (user != null)
                        userAccounts.Add(user);
                }
            }

            return userAccounts;
        }

        public static List<WebGroup> CollectGroups(string groupsString)
        {
            var groups = new List<WebGroup>();
            if (!string.IsNullOrWhiteSpace(groupsString))
            {
                var accounts = groupsString.Split(new char[] { AccountConstants.AccountDelimiter, WConstants.EmailSeparator });
                foreach (var listItem in accounts)
                {
                    string item = listItem.Trim();

                    if (item.Contains(AccountConstants.AccountSplitter))
                    {
                        var group = WebGroup.GetByUniqueName(item);
                        if (group != null)
                            groups.Add(group);
                    }
                    else
                    {
                        var group = WebGroup.Get(item);
                        if (group != null)
                            groups.Add(group);
                    }
                }
            }

            return groups;
        }

        public static bool IsPresentOrMember(string userGroupsString, bool checkEvenIfAdmin = false, bool ignoreGrpMembership = false)
        {
            return IsPresentOrMember(-1, userGroupsString, checkEvenIfAdmin, ignoreGrpMembership);
        }

        /// <summary>
        /// Checks current user is if he/she is included in the list (string). 
        /// The list can be a list of users in unique format or list of groups
        /// either in unique format or not. 
        /// </summary>
        /// <param name="userGroupsString">List of users or groups in unique format separated
        /// by account delimeter like semi-color. If not in unique format they are treated as group.
        /// </param>
        /// <returns></returns>
        public static bool IsPresentOrMember(int userId, string userGroupsString, bool checkEvenIfAdmin = false, bool ignoreGrpMembership = false)
        {
            int uid = userId > 0 ? userId : WSession.Current.UserId;
            if (uid > 0 && !string.IsNullOrWhiteSpace(userGroupsString))
            {
                if (checkEvenIfAdmin && uid == WSession.Current.UserId && WSession.Current.IsAdministrator)
                    return true;

                var emails = new List<WebUser>();

                var accounts = userGroupsString.Split(new char[] { AccountConstants.AccountDelimiter, WConstants.EmailSeparator });
                for (int i = 0; i < accounts.Length; i++)
                {
                    var item = accounts[i].Trim();
                    if (item.Contains(AccountConstants.AccountSplitter))
                    {
                        if (item.StartsWith(AccountConstants.USER_PREFIX) || item.StartsWith(AccountConstants.GROUP_PREFIX))
                        {
                            var group = WebGroup.GetByUniqueName(item);
                            if (group != null && !ignoreGrpMembership)
                            {
                                var users = group.Users;
                                var count = users.Count();
                                for (int u = 0; u < count; u++)
                                    if (uid == users.ElementAt(u).Id)
                                        return true;
                            }
                            else
                            {
                                var user = WebUser.GetByUniqueName(item);
                                if (user != null && uid == user.Id)
                                    return true;
                            }
                        }
                        else
                        {
                            // UniqueShortName
                            string[] parts = item.Split(AccountConstants.AccountSplitter);

                            int objectId = DataHelper.GetId(parts.First());
                            int recordId = DataHelper.GetId(parts[1]);

                            if (objectId > 0 && recordId > 0)
                            {
                                if (objectId == WebObjects.WebUser)
                                {
                                    if (uid == recordId)
                                        return true;
                                }
                                else if (objectId == WebObjects.WebGroup && !ignoreGrpMembership)
                                {
                                    var group = WebGroup.Get(recordId);
                                    if (group != null)
                                    {
                                        var users = group.Users;
                                        var count = users.Count();
                                        for (int u = 0; u < count; u++)
                                            if (uid == users.ElementAt(u).Id)
                                                return true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // Default is, treat it as a Group
                        var group = ignoreGrpMembership ? null : WebGroup.Get(item);
                        if (group != null)
                        {
                            var users = group.Users;
                            var count = users.Count();
                            for (int u = 0; u < count; u++)
                                if (uid == users.ElementAt(u).Id)
                                    return true;
                        }
                        else
                        {
                            var user = WebUser.Get(item);
                            if (user != null && uid == user.Id)
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        public static string FormatUserDisplay(WebUser user, string formatString, bool firstNameOnly = true, bool showPreviewOnly = false)
        {
            if (user != null && !string.IsNullOrEmpty(formatString))
            {
                var displayName = AccountHelper.GetPrefixedName(user, NamePrefixes.Brotherhood, firstNameOnly);
                return FormatUserDisplay(user.Id, showPreviewOnly ? DataHelper.GetStringPreview(displayName, 18) : displayName, formatString);
            }

            return string.Empty;
        }

        public static string FormatUserDisplay(int userId, string displayName, string formatString)
        {
            if (!string.IsNullOrEmpty(formatString) && userId > 0 && !string.IsNullOrEmpty(displayName))
            {
                var provider = new NamedValueProvider();
                provider.Add("UserId", userId);
                provider.Add("Name", displayName);

                return Substituter.Substitute(formatString, provider);
            }

            return displayName;
        }

        public static void FinalizePhotoUpload(WebUser user, int oldUserId, string ext, int thumbSize = 200)
        {
            if (oldUserId == -1)
                oldUserId = user.Id;

            var photoPathUrl = WConfig.UserPhotoPath;

            var oldFileName = string.Format("U{0}{1}", oldUserId, ext);
            var newFileName = string.Format("U{0}{1}", user.Id, ext);

            var photoFileUrl = WebHelper.CombineAddress(photoPathUrl, newFileName);

            var absPhotoPath = WebHelper.MapPath(photoPathUrl);
            var absPhotoFilePath = WebHelper.MapPath(photoFileUrl);

            var tempAbsPath = FileHelper.Combine(absPhotoPath, "temp");
            var origAbsFilePath = FileHelper.Combine(absPhotoPath, @"original\" + newFileName);
            var thumbsAbsFilePath = FileHelper.Combine(absPhotoPath, string.Format(@"thumb\{0}{1}", string.Format("U{0}", user.Id), ext));

            var updateFilePath = FileHelper.Combine(tempAbsPath, oldFileName);
            var uploadPreviewFile = FileHelper.Combine(tempAbsPath, string.Format("U{0}.preview{1}", oldUserId, ext));

            // Move resized version to main folder
            if (File.Exists(absPhotoFilePath))
                File.Delete(absPhotoFilePath);
            File.Move(uploadPreviewFile, absPhotoFilePath);

            // Move original file to original folder
            if (File.Exists(origAbsFilePath))
                File.Delete(origAbsFilePath);
            File.Move(updateFilePath, origAbsFilePath);

            if (File.Exists(thumbsAbsFilePath))
                File.Delete(thumbsAbsFilePath);

            /*
            var thumbFiles = Directory.EnumerateFiles(FileHelper.Combine(absPhotoPath, string.Format(@"thumbs\{0}.*{2}", fileNameWE, ext)));
            if (thumbFiles.Count() > 0)
                foreach (var thumbFile in thumbFiles)
                    File.Delete(thumbFile);
            */
            if (!ImageUtil.GenerateThumbnail(origAbsFilePath, thumbsAbsFilePath, thumbSize, thumbSize, true))
                throw new Exception("Error creating thumbnail.");

            user.PhotoPath = photoFileUrl;
            user.Update();
        }

        public static string UploadPhotoForPreview(int userId, FileUpload photoUpload, int photoSize = 600)
        {
            var photoPathUrl = WConfig.UserPhotoPath;
            var absPhotoPath = WebHelper.MapPath(photoPathUrl);
            var tempAbsPath = FileHelper.Combine(absPhotoPath, "temp");
            var origAbsPath = FileHelper.Combine(absPhotoPath, "original");
            var thumbsAbsPath = FileHelper.Combine(absPhotoPath, "thumb");

            if (!Directory.Exists(absPhotoPath))
                Directory.CreateDirectory(absPhotoPath);

            if (!Directory.Exists(tempAbsPath))
                Directory.CreateDirectory(tempAbsPath);

            if (!Directory.Exists(thumbsAbsPath))
                Directory.CreateDirectory(thumbsAbsPath);

            if (!Directory.Exists(origAbsPath))
                Directory.CreateDirectory(origAbsPath);

            var fileName = Path.GetFileName(photoUpload.PostedFile.FileName);
            var ext = Path.GetExtension(fileName);
            var fileNameWE = Path.GetFileNameWithoutExtension(fileName);
            var newPreviewName = string.Format("U{0}.preview{1}", userId, ext);
            var tempFile = FileHelper.Combine(tempAbsPath, string.Format("U{0}{1}", userId, ext));
            var tempThumbFile = FileHelper.Combine(tempAbsPath, newPreviewName);

            if (File.Exists(tempFile))
                File.Delete(tempFile);

            if (File.Exists(tempThumbFile))
                File.Delete(tempThumbFile);

            // Upload the file
            photoUpload.PostedFile.SaveAs(tempFile);
            ImageUtil.GenerateThumbnail(tempFile, tempThumbFile, photoSize, photoSize, true);

            return WebHelper.CombineAddress(photoPathUrl, "temp", newPreviewName);
        }
    }
}
