using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Utilities;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Manager;

namespace WCMS.Framework
{
    [Serializable]
    public class WebGroup : ParameterizedWebObject, ISelfManager, IWebAccount
    {
        public const string PageUrlKey = "PageUrl";

        private static IWebGroupProvider _provider;

        static WebGroup()
        {
            _provider = WebObject.ResolveManager<WebGroup, IWebGroupProvider>(WebObject.ResolveProvider<WebGroup, IWebGroupProvider>());
        }

        public WebGroup()
        {
            Id = -1;
            ParentId = -1;
            OwnerId = -1;
            PageId = -1;

            IsSystem = 0;
            JoinAlert = 0;
            JoinApproval = 0;

            PageUrl = string.Empty;
            Description = string.Empty;
            Managers = string.Empty;

            DateModified = DateTime.Now;
        }

        #region Properties

        [ObjectColumn]
        public int IsSystem { get; set; }

        [ObjectColumn]
        public int ParentId { get; set; }

        [ObjectColumn]
        public int OwnerId { get; set; }

        [ObjectColumn]
        public int JoinApproval { get; set; }

        [ObjectColumn]
        public int JoinAlert { get; set; }

        [ObjectColumn]
        public string PageUrl { get; set; }

        [ObjectColumn]
        public int PageId { get; set; }

        [ObjectColumn]
        public string Description { get; set; }

        [ObjectColumn]
        public DateTime DateModified { get; set; }

        [ObjectColumn]
        public string Managers { get; set; }

        public WebUser Owner
        {
            get { return WebUser.Get(OwnerId); }
        }

        public WebGroup Parent
        {
            get { return WebGroup.Get(ParentId); }
        }

        public IEnumerable<WebGroup> Children
        {
            get { return WebGroup.Provider.GetList(this.Id); }
        }

        public static IWebGroupProvider Provider
        {
            get { return _provider; }
        }

        public bool HasChildren
        {
            get { return Children.Count() > 0; }
        }

        public bool HasGroupChildren { get; set; }
        public bool HasUserChildren { get; set; }


        //public string UniqueName
        //{
        //    get { return AccountConstants.GROUP_PREFIX + Name; }
        //}

        public bool RequireApproval
        {
            get { return JoinApproval == 1; }
            set { JoinApproval = value ? 1 : 0; }
        }

        public WPage Page
        {
            get
            {
                if (PageId > 0)
                    return WPage.Get(PageId);
                else if (!string.IsNullOrWhiteSpace(PageUrl))
                    return WebRewriter.ResolvePage(PageUrl);

                return null;
            }
        }

        public string EvalPageUrl
        {
            get
            {
                if (PageId > 0)
                {
                    var page = WPage.Get(PageId);
                    if (page != null)
                        return page.BuildRelativeUrl();
                }

                return PageUrl;
            }
        }

        public IEnumerable<WebUser> Users
        {
            get
            {
                var wugs = WebUserGroup.GetByGroupId(Id, 1);
                WebUser user = null;

                return (from wug in wugs
                        where (user = wug.User) != null
                        select user);
            }
        }

        public string DisplayHTML
        {
            get
            {
                var html = GetParameterValue("FormatString");
                if (string.IsNullOrEmpty(html))
                    return this.Name;

                return html.Replace("$(Name)", Name);
            }
        }

        // None-universal feature
        //public string PageUrl
        //{
        //    get
        //    {
        //        var pageUrl = GetParameterValue(PageUrlKey);
        //        return pageUrl;
        //    }
        //}

        #endregion

        #region ISelfManager Members

        public int Update()
        {
            if (_managerList != null && _managersCache != Managers)
            {
                _managerList = null;
                _managersCache = null;
            }

            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        #endregion

        #region Instance Methods

        private string _managersCache = null;
        private IEnumerable<WebUser> _managerList = null;
        public IEnumerable<WebUser> GetManagerList()
        {
            if (_managerList != null)
                return _managerList;

            _managersCache = Managers;

            if (!string.IsNullOrEmpty(Managers))
                return AccountHelper.CollectUsersHierarchal(Managers);

            return new List<WebUser>();
        }

        public bool IsManager(WebUser user, bool checkParents = false)
        {
            var managers = GetManagerList();
            if (managers.Count() > 0 && managers.Contains(user, new UserIdEqualityComparer()))
                return true;

            if (checkParents)
            {
                var parent = Parent;
                if (parent != null)
                    return parent.IsManager(user, checkParents);
            }

            return false;
        }

        public void AddUser(int userId)
        {
            if (!this.IsMember(userId))
            {
                var userGroup = new WebUserGroup();
                userGroup.GroupId = this.Id;
                userGroup.RecordId = userId;
                userGroup.ObjectId = WebObjects.WebUser;
                userGroup.Update();
            }
        }

        public bool RemoveUser(int userId)
        {
            if (this.IsMember(userId))
            {
                var userGroup = WebUserGroup.Get(this.Id, userId);
                if (userGroup != null)
                {
                    userGroup.Delete();
                    return true;
                }
            }

            return false;
        }

        public bool RemoveGroup(int userId)
        {
            if (this.IsMember(userId))
            {
                var userGroup = WebUserGroup.Get(this.Id, userId);
                if (userGroup != null)
                {
                    userGroup.Delete();
                    return true;
                }
            }

            return false;
        }

        public string ToUniqueShortString()
        {
            return AccountHelper.ToUniqueShortString(this);
        }

        public string ToUniqueString()
        {
            return AccountHelper.ToUniqueString(this);
        }

        public bool IsMember(int userId, int active = RecordStatus.Active)
        {
            if (userId == WConstants.NULL_ID) return false;
            var ug = WebUserGroup.Get(this.Id, userId);
            return (ug != null && (active == RecordStatus.Null || ug.Active == active));
        }

        #endregion

        #region Static Methods

        public static WebGroup FromShortString(string shortString)
        {
            string[] parts = shortString.Split('\\');

            int objectId = DataHelper.GetId(parts.First());
            int recordId = DataHelper.GetId(parts[1]);

            if (objectId == WebObjects.WebGroup)
                return WebGroup.Get(recordId);
            else
                return null;
        }

        /// <summary>
        /// Most comprehensive, also calls GetByAny
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static WebGroup SelectNode(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            if (path.Contains("/"))
            {
                int parentId = -1;

                string[] nodeNames = path.Split('/');
                for (int i = 0; i < nodeNames.Length - 1; i++)
                {
                    string nodeName = nodeNames[i].Trim();
                    if (string.IsNullOrEmpty(nodeName))
                        continue;

                    parentId = WebGroup.Provider.Get(parentId, nodeName).Id;
                }

                string lastNode = nodeNames[nodeNames.Length - 1];
                return WebGroup.Provider.Get(parentId, lastNode);
            }

            return WebGroup.GetByAny(path);
        }

        public static bool IsValidUniqueName(string uniqueName)
        {
            return (uniqueName.ToUpper().StartsWith(AccountConstants.GROUP_PREFIX));
        }

        public static WebGroup GetByUniqueName(string uniqueName)
        {
            if (IsValidUniqueName(uniqueName))
                return WebGroup.Get(uniqueName.Substring(AccountConstants.GROUP_PREFIX.Length));

            return null;
        }

        /// <summary>
        /// Accepts whether unique or simple format name. For a more comprehensive call, use SelectNode
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static WebGroup GetByAny(string name)
        {
            if (IsValidUniqueName(name))
                return WebGroup.Get(name.Substring(AccountConstants.GROUP_PREFIX.Length));
            else
                return Get(name);
        }

        public static WebGroup Get(int id)
        {
            return _provider.Get(id);
        }

        /// <summary>
        /// Straight-forward group search by name. Better performance
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static WebGroup Get(string name)
        {
            return _provider.Get(name);
        }

        public static IEnumerable<WebGroup> GetByUserId(int userId, int active)
        {
            WebGroup g = null;
            var groups = from r in WebUserGroup.GetByUserId(userId, active)
                         where (g = r.Group) != null
                         select g;
            return groups;
        }

        public static IEnumerable<WebGroup> GetList()
        {
            return _provider.GetList();
        }

        public static bool Delete(int id)
        {
            return _provider.Delete(id);
        }

        #endregion

        #region IWebObject Members

        public override int OBJECT_ID
        {
            get { return WebObjects.WebGroup; }
        }

        #endregion

    }
}
