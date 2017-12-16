using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.Menu.Providers;

namespace WCMS.WebSystem.WebParts.Menu
{
    public class MenuItem : WebObjectBase, ISelfManager
    {
        private static IMenuItemProvider _manager;

        static MenuItem()
        {
            _manager = WebObject.ResolveManager<MenuItem, IMenuItemProvider>(WebObject.ResolveProvider<MenuItem, IMenuItemProvider>());
        }

        public MenuItem()
        {
            ParentId = -1;
            MenuId = -1;
            PageId = -1;
            Type = 1;
            CheckPermission = 0;

            Target = string.Empty;
        }

        public MenuItem(WPage page, bool buildHierarchy = true)
            : this()
        {
            NavigateUrl = page.BuildRelativeUrl();
            Text = page.EvaluatedTitle;
            PageId = page.Id;
            Active = page.Active;
            Rank = page.Rank;

            var parent = page.Parent;
            if (buildHierarchy && parent != null)
                this.Parent = new MenuItem(parent, true);
        }

        public override int OBJECT_ID { get { return MenuConstants.MenuItem_ObjectId; } }

        [ObjectColumn]
        public string NavigateUrl { get; set; }

        [ObjectColumn]
        public string Text { get; set; }

        [ObjectColumn]
        public string Target { get; set; }

        [ObjectColumn]
        public int ParentId { get; set; }

        [ObjectColumn]
        public int MenuId { get; set; }

        [ObjectColumn]
        public int Active { get; set; }

        [ObjectColumn]
        public int Rank { get; set; }

        [ObjectColumn]
        public int PageId { get; set; }

        [ObjectColumn]
        public int Type { get; set; }

        [ObjectColumn]
        public int CheckPermission { get; set; }

        public MenuEntity Menu
        {
            get
            {
                if (MenuId > 0)
                    return MenuEntity.Provider.Get(MenuId);

                return null;
            }
        }

        public WPage Page
        {
            get
            {
                if (PageId > 0)
                    return WPage.Get(PageId);

                return null;
            }
        }

        public bool IsActive { get { return Active == 1; } }

        public bool IsVisible
        {
            get
            {
                var session = WSession.Current;
                if (CheckPermission == 1 && PageId > 0 && !session.IsAdministrator)
                {
                    var page = Page;
                    if (page != null)
                        return page.GetPublicAccess(session) == PublicAccessCheckResult.Granted;
                }

                return true;
            }
        }

        private MenuItem _parent = null;
        public MenuItem Parent
        {
            get
            {
                if (_parent == null && ParentId > 0)
                    _parent = MenuItem.Provider.Get(ParentId);

                return _parent;
            }

            set
            {
                _parent = value;
            }
        }

        private string _pageUrl = string.Empty;
        public string PageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_pageUrl))
                {
                    if (!string.IsNullOrEmpty(NavigateUrl))
                        _pageUrl = NavigateUrl;
                    else if (PageId > 0)
                    {
                        var page = Page;
                        if (page != null)
                            _pageUrl = page.BuildAbsoluteUrl();
                    }
                }

                return _pageUrl;
            }
        }

        public static IEnumerable<MenuItem> GetListByPageId(int pageId)
        {
            return _manager.GetList(-2, -2, pageId);
        }

        public static IMenuItemProvider Provider
        {
            get { return _manager; }
        }

        public IEnumerable<MenuItem> Children
        {
            get { return _manager.GetList(MenuId, Id); }
        }

        #region ISelfManager Members

        public int Update()
        {
            _pageUrl = string.Empty;

            return _manager.Update(this);
        }

        public bool Delete()
        {
            return _manager.Delete(this.Id);
        }

        #endregion
    }
}
