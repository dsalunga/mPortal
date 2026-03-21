using System;
using System.Collections.Generic;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Menu.Providers
{
    public interface IMenuItemProvider : IDataProvider<MenuItem>
    {
        IEnumerable<MenuItem> GetList(int menuId = -2, int parentId = -2, int pageId = -2);
    }
}
