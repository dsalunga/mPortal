using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Menu
{
    public sealed class MenuHelper
    {
        public static void PopulateCboMenuItems(DropDownList cbo, int menuId, bool includeRoot = true, int excludeId = -1)
        {
            if (menuId > 0)
            {
                var menuItems = MenuItem.Provider.GetList(menuId);

                Action<int, string> LoadRecursiveTree = null;
                LoadRecursiveTree = (int parentId, string tab) =>
                {
                    tab += WConstants.TAB;

                    var levelItems = menuItems.Where(i => i.ParentId == parentId);
                    foreach (var menuItem in levelItems)
                    {
                        if (menuItem.Id != excludeId)
                        {
                            var listItem = new ListItem(tab + WConstants.BULLET + menuItem.Text, menuItem.Id.ToString());
                            cbo.Items.Add(listItem);

                            LoadRecursiveTree(menuItem.Id, tab);
                        }
                    }
                };

                if (includeRoot)
                {
                    var item = MenuEntity.Provider.Get(menuId);
                    if (item != null)
                    {
                        var rootName = item.Name;
                        var itemRoot = new ListItem(rootName, "-1");
                        cbo.Items.Add(itemRoot);
                    }
                }


                // START RECURSIVE
                LoadRecursiveTree(-1, "");
            }
        }
    }
}
