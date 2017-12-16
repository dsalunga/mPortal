using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace WCMS.WebSystem.WebParts.Menu
{
    public class MenuItemModelCollection : List<MenuItemModel>
    {
        public MenuItemModelCollection(MenuItemModel parent)
        {
            Parent = parent;
        }

        public MenuItemModel Parent { get; set; }

        public new void Add(MenuItemModel item)
        {
            item.Parent = Parent;
            base.Add(item);
        }
    }
}
