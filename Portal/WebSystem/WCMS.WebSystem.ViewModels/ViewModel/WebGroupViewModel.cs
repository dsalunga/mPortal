using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

using WCMS.Framework;

namespace WCMS.WebSystem.ViewModel
{
    public class WebGroupViewModel
    {
        public static List<ListItem> GenerateListItem(int parentId)
        {
            return GenerateListItem(parentId, false);
        }

        public static List<ListItem> GenerateListItem(int parentId, bool includeRoot)
        {
            string baseTab = WConstants.TAB;

            var items = WebGroup.GetList();
            List<ListItem> listItems = new List<ListItem>();

            if (includeRoot)
            {
                var parentGroup = parentId > 0 ? items.FirstOrDefault(i => i.Id == parentId) : null;
                string parentLabel = parentGroup == null ? WConfig.SystemName : parentGroup.Name;
                listItems.Add(new ListItem(FormatTab(baseTab) + parentLabel, "-1"));
                baseTab += WConstants.TAB;
            }

            Action<IEnumerable<WebGroup>, List<ListItem>, int, string> BuildListItemRecursive = null;
            BuildListItemRecursive = (webItems, list, id, tab) =>
            {
                var subItems = webItems.Where(s => s.ParentId == id);
                foreach (var webItem in subItems)
                {
                    listItems.Add(new ListItem(FormatTab(tab) + webItem.Name, webItem.Id.ToString()));
                    if (webItem.HasChildren)
                        BuildListItemRecursive(webItems, list, webItem.Id, tab + WConstants.TAB);
                }
            };

            BuildListItemRecursive(items, listItems, parentId, baseTab);

            return listItems;
        }

        private static string FormatTab(string sTab)
        {
            return !string.IsNullOrEmpty(sTab) ? sTab + WConstants.BULLET : string.Empty;
        }
    }
}
