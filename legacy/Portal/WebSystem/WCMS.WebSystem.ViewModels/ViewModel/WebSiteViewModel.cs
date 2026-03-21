using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.Controls;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.ViewModel
{
    public class WebSiteViewModel
    {
        public static List<ListItem> GenerateListItem(int parentId)
        {
            return GenerateListItem(parentId, false);
        }

        public static List<ListItem> GenerateListItem(int parentId, bool includeRoot)
        {
            string baseTab = WConstants.TAB;

            var items = WSite.GetList();
            List<ListItem> listItems = new List<ListItem>();

            if (includeRoot)
            {
                listItems.Add(new ListItem(FormatTab(baseTab) + WConfig.SystemName, "-1"));
                baseTab += WConstants.TAB;
            }

            Action<IEnumerable<WSite>, List<ListItem>, int, string> BuildListItemRecursive = null;
            BuildListItemRecursive = (webSites, list, id, tab) =>
                {
                    var subSites = webSites.Where(s => s.ParentId == id);
                    foreach (var webSite in subSites)
                    {
                        listItems.Add(new ListItem(FormatTab(tab) + webSite.Name, webSite.Id.ToString()));
                        if (webSite.HasChildren)
                            BuildListItemRecursive(webSites, list, webSite.Id, tab + WConstants.TAB);
                    }
                };

            BuildListItemRecursive(items, listItems, parentId, baseTab);

            return listItems;
        }

        private static string FormatTab(string tab)
        {
            return !string.IsNullOrEmpty(tab) ? tab + WConstants.BULLET : string.Empty;
        }

        public static void BuildTabs(int siteId, WQuery query, ITabControl tabControl)
        {
            var q = query.Clone();

            query.Remove(ObjectKey.KeySource);
            query.Remove(ObjectKey.KeyString);

            tabControl.AddTab("tabMain", "General", query.BuildQuery(CentralPages.WebSiteHome), CentralPages.WebSiteHome);
            tabControl.AddTab("tabPages", "Pages", query.BuildQuery(CentralPages.WebPages), CentralPages.WebPages);
            tabControl.AddTab("tabMasterPages", "Master Pages", query.BuildQuery(CentralPages.WebMasterPages), CentralPages.WebMasterPages);
            tabControl.AddTab("tabSubSites", "Sites", query.BuildQuery(CentralPages.WebChildSites), CentralPages.WebChildSites);
            //tabControl.AddTab("tabPreview", "Preview");

            // General tabs

            WebSystemViewModel.BuildGenericTabs(WebObjects.WebSite, siteId, q, tabControl);
        }
    }
}
