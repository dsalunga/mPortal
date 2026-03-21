using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.Menu.Providers;

namespace WCMS.WebSystem.WebParts.Menu.Managers
{
    public class MenuItemManager : StandardDataManager<MenuItem>, IMenuItemProvider
    {
        protected IMenuItemProvider _provider;

        public MenuItemManager(IMenuItemProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IMenuItemProvider Members

        public IEnumerable<MenuItem> GetList(int menuId = -2, int parentId = -2, int pageId = -2)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return (from i in _cache.ObjectCache.Values
                        orderby i.ParentId, i.Rank, i.Text
                        where (menuId == -2 || i.MenuId == menuId)
                        && (parentId == -2 || i.ParentId == parentId)
                        && (pageId == -2 || i.PageId == pageId)
                        select i);
            }

            return _provider.GetList(menuId, parentId, pageId);
        }

        #endregion
    }
}
