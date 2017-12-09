using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Common.Utilities;

using WCMS.Framework.Utilities;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Manager;

namespace WCMS.Framework
{
    [Serializable]
    public class WebUser : UserInfo, ISelfManager, IWebAccount
    {
        public const int LOGIN_LOCKOUT_MINS = 15;
        public const int LOGIN_LOCKOUT_MAX_ATTEMPT = 5;
        private static IWebUserProvider _manager;

        static WebUser()
        {
            _manager = WebObject.ResolveManager<WebUser, IWebUserProvider>(WebObject.ResolveProvider<WebUser, IWebUserProvider>());
        }

        public WebUser()
        {
            Id = WConstants.NULL_ID;
            ProviderId = WConstants.NULL_ID;
            MaritalStatusId = WConstants.NULL_ID;

            Status = AccountStatus.PENDING;
            LoginFailureCount = 0;
            //UserType = UserTypes.User;

            ActivationKey = string.Empty;
            Password = string.Empty;
            NewEmail = string.Empty;
            Email2 = string.Empty;
            NameSuffix = string.Empty;
            MiddleName = string.Empty;

            StatusText = string.Empty;
            PhotoPath = string.Empty;

            LastUpdate = WConstants.DateTimeMinValue;
            DateCreated = WConstants.DateTimeMinValue;
            LastLogin = WConstants.DateTimeMinValue;
            PasswordExpiryDate = WConstants.DateTimeMinValue;
            LastLoginFailureDate = WConstants.DateTimeMinValue;
        }

        #region Properties

        [ObjectColumn]
        public string Password { get; set; }

        [ObjectColumn]
        public string Email2 { get; set; }

        [ObjectColumn]
        public string ActivationKey { get; set; }

        [ObjectColumn]
        public string NewEmail { get; set; }

        [ObjectColumn]
        public string NameSuffix { get; set; }

        [ObjectColumn]
        public DateTime LastLogin { get; set; }

        [ObjectColumn]
        public string StatusText { get; set; }

        [ObjectColumn]
        public DateTime PasswordExpiryDate { get; set; }

        [ObjectColumn]
        public int ProviderId { get; set; }
        public int MaritalStatusId { get; set; }

        public DateTime LastLoginFailureDate { get; set; }
        public int LoginFailureCount { get; set; }

        private string _photoPath;

        [ObjectColumn]
        public string PhotoPath
        {
            get { return _photoPath; }
            set
            {
                _photoPath = value;
                _photoThumbPath = null;
            }
        }

        private string _photoThumbPath;
        public string PhotoThumbPath
        {
            get
            {
                if (!string.IsNullOrEmpty(_photoThumbPath))
                {
                    var thumbPath = WebHelper.CombineAddress(WConfig.UserPhotoPath, "thumb");
                    _photoThumbPath = WebHelper.CombineAddress(thumbPath, VirtualPathUtility.GetFileName(PhotoPath));
                }

                return _photoPath;
            }
        }

        private string _photoOriginalPath;
        public string PhotoOriginalPath
        {
            get
            {
                if (string.IsNullOrEmpty(_photoOriginalPath) && !string.IsNullOrEmpty(PhotoPath) && !WebHelper.IsAbsUrl(PhotoPath))
                {
                    var path = WebHelper.CombineAddress(WConfig.UserPhotoPath, "original");
                    var absFolder = WebHelper.MapPath(path);
                    var fileName = VirtualPathUtility.GetFileName(PhotoPath);
                    var name = System.IO.Path.GetFileNameWithoutExtension(fileName);

                    var files = System.IO.Directory.EnumerateFiles(absFolder, name + ".*");
                    if(files.Count() > 0)
                        fileName = System.IO.Path.GetFileName(files.First());

                    _photoOriginalPath = WebHelper.CombineAddress(path, fileName);
                }

                return _photoOriginalPath;
            }
        }

        public bool HasPhoto
        {
            get
            {
                var photoPath = PhotoPath;
                return !string.IsNullOrEmpty(photoPath) && !photoPath.EndsWith(WConstants.NoPhotoFile, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public override string Name
        {
            get { return UserName; } //ToUniqueString(); }
        }

        public override int OBJECT_ID
        {
            get { return WebObjects.WebUser; }
        }

        //public int UserType { get; set; }

        public bool IsPasswordExpired
        {
            get { return PasswordExpiryDate > WConstants.DateTimeMinValue && DateTime.Now > PasswordExpiryDate; }
        }

        public bool IsActive
        {
            get { return Status == 1; }
            set { Status = value ? 1 : 0; }
        }

        public string EmailId
        {
            get
            {
                if (!string.IsNullOrEmpty(Email))
                {
                    var atIndex = Email.IndexOf("@");
                    if (atIndex > 0)
                        return Email.Substring(0, atIndex + 1);
                }

                return string.Empty;
            }
        }

        public IEnumerable<WebGroup> Groups { get { return WebGroup.GetByUserId(this.Id, 1); } }
        public IEnumerable<WebAddress> Addresses { get { return WebAddress.Provider.GetList(this.OBJECT_ID, this.Id); } }

        public string FullName
        {
            get
            {
                string fullName = "";
                if (LastName.Length > 0) fullName = LastName + ",";
                if (FirstName.Length > 0) fullName += " " + FirstName;
                if (MiddleName.Length > 0) fullName += " " + MiddleName.Substring(0, 1) + ".";

                return fullName.Trim();
            }
        }

        public string FirstAndLastName
        {
            get
            {
                if (FirstName.Length > 0 && LastName.Length > 0)
                    return string.Format("{0} {1}", FirstName, LastName);
                else if (FirstName.Length > 0)
                    return FirstName;
                else
                    return LastName;
            }
        }

        public bool NoLastUpdate
        {
            get { return LastUpdate.Equals(WConstants.DateTimeMinValue); }
        }

        public bool HaveNotLoggedIn
        {
            get { return LastLogin.Equals(WConstants.DateTimeMinValue); }
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Login lock-out check: 5 failed retries, locked for 15mins
        /// </summary>
        public bool IsLoginLockedOut
        {
            get
            {
                return this.LoginFailureCount >= LOGIN_LOCKOUT_MAX_ATTEMPT && this.LastLoginFailureDate.AddMinutes(LOGIN_LOCKOUT_MINS) > DateTime.Now;
            }

            set
            {
                if (!value)
                {
                    LoginFailureCount = 0;
                    LastLoginFailureDate = WConstants.DateTimeMinValue;
                }
            }
        }

        public WebAddress NewAddress(string tag)
        {
            var item = new WebAddress();
            item.ObjectId = this.OBJECT_ID;
            item.RecordId = this.Id;
            item.Tag = tag;

            return item;
        }

        public WebAddress GetAddress(string tag)
        {
            var addresses = Addresses;
            if (addresses.Count() > 0)
                return addresses.FirstOrDefault(i => i.Tag.Equals(tag, StringComparison.InvariantCultureIgnoreCase));

            return null;
        }

        public void Activate()
        {
            this.Status = 1;
            this.ActivationKey = string.Empty;
            this.Update();
        }

        public string ToUniqueShortString()
        {
            return AccountHelper.ToUniqueShortString(this);
        }

        public string ToUniqueString()
        {
            return AccountHelper.ToUniqueString(this);
        }

        public int Update()
        {
            if (0 > Id)
                this.DateCreated = DateTime.Now;

            if (string.IsNullOrEmpty(UserName))
                UserName = Guid.NewGuid().ToString("D");

            return _manager.Update(this);
        }

        public int Update(bool updateLastUpdate)
        {
            if (updateLastUpdate)
                this.LastUpdate = DateTime.Now;

            return Update();
        }

        public bool IsMemberOf(string groupName)
        {
            var group = WebGroup.GetByAny(groupName);
            if (group == null)
                return false;

            return group.IsMember(this.Id);
        }

        public bool IsServiceAccount()
        {
            return IsMemberOf("Service Accounts");
        }

        public bool IsAdministrator()
        {
            return IsMemberOf(SystemGroups.ADMINS_GROUP_ID); // || WebGlobalPolicy.IsUserAdded(Id, GlobalPolicies.Administration);
        }

        public bool IsMemberOf(int groupId, int active = RecordStatus.Active)
        {
            var group = WebGroup.Get(groupId);
            if (group == null)
                return false;

            return group.IsMember(this.Id, active);
        }

        public bool IsMemberOrEqual(int objectId, int recordId)
        {
            if (objectId == WebObjects.WebUser && recordId == Id)
            {
                return true;
            }
            else if (objectId == WebObjects.WebGroup)
            {
                var g = WebGroup.Get(recordId);
                return g.IsMember(Id);
            }

            return false;
        }

        // returns existing or newly added UserGroup
        public WebUserGroup AddToGroup(int groupId, int active = RecordStatus.Active, int createdById = -1)
        {
            var item = WebUserGroup.Get(groupId, this.Id);
            if (item == null)
            {
                item = new WebUserGroup();
                item.GroupId = groupId;
                item.RecordId = this.Id;
                item.ObjectId = WebObjects.WebUser;
                item.Active = active;
            }
            else if (item.Active != active)
            {
                item.Active = active;
            }

            if (createdById > 0 && item.CreatedById <= WConstants.NULL_ID)
                item.CreatedById = createdById;
            item.Update();
            
            return item;
        }

        public WebUserGroup AddToGroup(string groupName, int active = RecordStatus.Active, int createdById = -1)
        {
            var item = WebGroup.Get(groupName);
            if (item != null)
                return this.AddToGroup(item.Id, active, createdById);

            return null;
        }

        public void RemoveToGroup(int groupId)
        {
            var item = WebUserGroup.Get(groupId, this.Id);
            if (item != null)
                WebUserGroup.Delete(item.Id);
        }

        public string GetPhotoPath(string size = null, bool absPath = false)
        {
            if (string.IsNullOrEmpty(_photoPath))
            {
                if (!string.IsNullOrEmpty(size))
                {
                    var thumb = PhotoThumbPath;
                    if (!string.IsNullOrEmpty(thumb))
                    {
                        _photoPath = thumb; // If not photo, use thumb as main photo
                        _photoThumbPath = thumb;
                        return absPath ? WHelper.ToAbsPath(_photoThumbPath) : _photoThumbPath;
                    }
                }

                if (string.IsNullOrEmpty(_photoPath))
                    return absPath ? WHelper.ToAbsPath(WConstants.NoPhotoThumb) : WConstants.NoPhotoThumb;
            }

            //if (!string.IsNullOrEmpty(_photoThumbPath))
            //    return _photoThumbPath;

            if (!string.IsNullOrEmpty(size))
            {
                if (_photoPath.Contains("/brethren/photos/"))
                {
                    var path = _photoPath.Replace("/brethren/photos/", string.Format("/brethren/photos/{0}/", size));
                    return absPath ? WHelper.ToAbsPath(path) : path;
                }

                if (!string.IsNullOrEmpty(_photoThumbPath))
                    return absPath ? WHelper.ToAbsPath(_photoThumbPath) : _photoThumbPath;
            }

            return absPath ? WHelper.ToAbsPath(_photoPath) : _photoPath;
        }

        #endregion

        #region Static Methods

        public static IWebUserProvider Provider { get { return _manager; } }

        public static WebUser FromShortString(string shortString)
        {
            string[] parts = shortString.Split(AccountConstants.AccountSplitter);

            int objectId = DataHelper.GetId(parts.First());
            int recordId = DataHelper.GetId(parts[1]);

            if (objectId == WebObjects.WebUser)
                return WebUser.Get(recordId);
            else
                return null;
        }

        public static bool IsValidUniqueName(string uniqueName)
        {
            return (uniqueName.Contains(AccountConstants.AccountSplitter) && uniqueName.StartsWith(AccountConstants.USER_PREFIX, StringComparison.InvariantCultureIgnoreCase));
        }

        public static WebUser GetByUniqueName(string uniqueName)
        {
            if (IsValidUniqueName(uniqueName))
                return WebUser.Get(uniqueName.Substring(AccountConstants.USER_PREFIX.Length));

            return null;
        }

        public static WebUser Get(string userName)
        {
            if (IsValidUniqueName(userName))
                return GetByUniqueName(userName);
            else
                return _manager.Get(userName);
        }

        public static WebUser GetByEmailOrUsername(string userNameOrEmail)
        {
            WebUser user = null;
            bool isEmail = Validator.IsRegexMatch(userNameOrEmail, RegexPresets.Email);

            if (isEmail)
                user = GetByEmail(userNameOrEmail);

            if (user == null)
                user = Get(userNameOrEmail);

            return user;
        }

        public static WebUser GetByEmail(string email)
        {
            return _manager.GetByEmail(email);
        }

        public static WebUser Get(int userId)
        {
            if (userId > 0)
                return _manager.Get(userId);

            return null;
        }

        public static IEnumerable<WebUser> GetList()
        {
            return _manager.GetList();
        }

        public static IEnumerable<WebUser> GetList(int groupId = -1, int status = -1)
        {
            if (groupId > 0)
            {
                var items = WebUserGroup.GetByGroupId(groupId, status == AccountStatus.ACTIVE ? 1 : -1);
                WebUser user = null;

                return (from i in items
                        where (user = i.User) != null && (status == -1 || user.Status == status)
                        select i.User);
            }

            return _manager.GetList(status);
        }

        /// <summary>
        /// Includes validation checks and removes user's membership to groups
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool Delete(int userId)
        {
            var anonymous = GetAnonymous();
            if (anonymous != null && anonymous.Id == userId)
                throw new Exception("Could not delete ANONYMOUS user.");

            // Delete group membership
            var userGroups = WebUserGroup.GetByUserId(userId, -1);
            if (userGroups.Count() > 0)
                for (int i = userGroups.Count() - 1; i >= 0; i--)
                    userGroups.ElementAt(i).Delete();

            // Delete Addresses
            var addresses = WebAddress.Provider.GetList(WebObjects.WebUser, userId);
            if (addresses.Count() > 0)
                foreach (var address in addresses)
                    address.Delete();

            return _manager.Delete(userId);
        }

        private static WebUser _anonymous = null;
        public static WebUser GetAnonymous()
        {
            if (_anonymous == null)
                _anonymous = Get(WConstants.ANONYMOUS);

            return _anonymous;
        }

        #endregion

        #region ISelfManager Members


        public bool Delete()
        {
            return Delete(Id);
        }

        #endregion

        public List<WebUserGroup> AddToGroups(string groups, int status = RecordStatus.Active)
        {
            var items = new List<WebUserGroup>();
            if (!string.IsNullOrEmpty(groups))
            {
                var groupsArray = groups.Split(AccountConstants.AccountDelimiter);
                foreach (var group in groupsArray)
                    if (!string.IsNullOrWhiteSpace(group))
                        items.Add(this.AddToGroup(group, status));
            }

            return items;
        }

        public string[] GetNewEmails()
        {
            var emails = NewEmail.Split('|').ToList();
            if (emails.Count == 1)
                emails.Add("");

            return emails.ToArray();
        }

        public void SetNewEmails(string[] emails)
        {
            NewEmail = string.Join("|", emails);
        }

        public void SetActivationKeys(string[] keys)
        {
            ActivationKey = string.Join("|", keys);
        }
        public string[] GetActivationKeys ()
        {
            var keys = ActivationKey.Split('|').ToList();
            if (keys.Count == 1)
                keys.Add("");

            return keys.ToArray();
        }

        public string RenewActivationKey()
        {
            var keys = GetActivationKeys();
            var key = Guid.NewGuid().ToString().Replace("-", "");
            keys[0] = key;
            this.ActivationKey = string.Join("|", keys);

            return key;
        }

        public string RenewActivationKey2()
        {
            var keys = GetActivationKeys();
            var key = Guid.NewGuid().ToString().Replace("-", "");
            keys[1] = key;
            this.ActivationKey = string.Join("|", keys);

            return key;
        }

        public int GetUserType()
        {
            if (IsMemberOf(SystemGroups.ADMINS_GROUP_ID))
                return UserTypes.Administrator;

            if (IsMemberOf(SystemGroups.MANAGERS_GROUP_ID))
                return UserTypes.SiteManager;

            return UserTypes.User;
        }
    }
}
