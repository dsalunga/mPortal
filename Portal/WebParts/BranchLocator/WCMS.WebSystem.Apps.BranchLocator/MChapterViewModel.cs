using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.BranchLocator
{
    public class MChapterViewModel
    {
        public static List<KeyValuePair<string, string>> GenerateListItem(int parentId)
        {
            return GenerateListItem(parentId, "");
        }

        public static List<KeyValuePair<string, string>> GenerateListItem(int parentId, string rootTitle)
        {
            string baseTab = WConstants.TAB;
            var items = MChapter.Provider.GetList();
            var listItems = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(rootTitle))
            {
                listItems.Add(new KeyValuePair<string, string>(FormatTab(baseTab) + rootTitle, "-1"));
                baseTab += WConstants.TAB;
            }

            Action<IEnumerable<MChapter>, List<KeyValuePair<string, string>>, int, string> BuildListItemRecursive = null;
            BuildListItemRecursive = (webItems, list, id, tab) =>
            {
                var subItems = webItems.Where(s => s.ParentId == id);
                foreach (var webItem in subItems)
                {
                    listItems.Add(new KeyValuePair<string, string>(FormatTab(tab) + webItem.Name, webItem.Id.ToString()));
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
