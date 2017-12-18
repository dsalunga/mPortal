using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.BranchLocator
{
    public class MChapterViewModel
    {
        public static List<ListItem> GenerateListItem(int parentId)
        {
            return GenerateListItem(parentId, "");
        }

        public static List<ListItem> GenerateListItem(int parentId, string rootTitle)
        {
            string baseTab = WConstants.TAB;
            var items = MChapter.Provider.GetList();
            var listItems = new List<ListItem>();

            if (!string.IsNullOrEmpty(rootTitle))
            {
                listItems.Add(new ListItem(FormatTab(baseTab) + rootTitle, "-1"));
                baseTab += WConstants.TAB;
            }

            Action<IEnumerable<MChapter>, List<ListItem>, int, string> BuildListItemRecursive = null;
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

        private static string FormatTab(string tab)
        {
            return !string.IsNullOrEmpty(tab) ? tab + WConstants.BULLET : string.Empty;
        }
    }
}
