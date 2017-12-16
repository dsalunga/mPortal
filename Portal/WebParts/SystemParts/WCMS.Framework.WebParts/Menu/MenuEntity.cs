using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Menu.Providers;

namespace WCMS.WebSystem.WebParts.Menu
{
    public class MenuEntity : NamedWebObject, ISelfManager
    {
        private static IMenuProvider _manager;

        static MenuEntity()
        {
            _manager = WebObject.ResolveManager<MenuEntity, IMenuProvider>(WebObject.ResolveProvider<MenuEntity, IMenuProvider>());
        }

        public MenuEntity()
        {
            Active = true;
            SiteId = -1;
            UserId = -1;
            PageId = -1;
            IncludeChildren = 1;
        }

        public int IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public int SiteId { get; set; }
        public int UserId { get; set; }
        public int PageId { get; set; }
        public int IncludeChildren { get; set; }

        public IEnumerable<MenuItem> Children
        {
            get { return MenuItem.Provider.GetList(Id); }
        }

        public bool Active
        {
            get{ return IsActive == 1;}
            set { IsActive = value ? 1 : 0; }
        }

        public override int OBJECT_ID
        {
            get { return MenuConstants.Menu_ObjectId; }
        }

        public static IMenuProvider Provider
        {
            get { return _manager; }
        }

        #region ISelfManager Members

        public bool Delete()
        {
            var children = Children;
            foreach (var child in children)
                child.Delete();

            return _manager.Delete(Id);
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        #endregion
    }
}
