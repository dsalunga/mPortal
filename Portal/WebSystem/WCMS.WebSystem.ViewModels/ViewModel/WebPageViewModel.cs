using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;
using WCMS.Framework;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.ViewModel
{
    public class WebPageViewModel
    {
        public static List<ListItem> GenerateListItem(int siteId, int parentId)
        {
            return GenerateListItem(siteId, parentId, false);
        }

        public static List<ListItem> GenerateListItem(int siteId, int parentId, bool includeRoot)
        {
            string baseTab = WConstants.TAB;

            var site = WSite.Get(siteId);
            var items = WPage.GetList(siteId);
            var listItems = new List<ListItem>();
            Action<IEnumerable<WPage>, List<ListItem>, int, string> BuildListItemRecursive = null;

            if (includeRoot)
            {
                listItems.Add(new ListItem(FormatTab(baseTab) + site.Name, "-1"));
                baseTab += WConstants.TAB;
            }

            BuildListItemRecursive = (webPages, list, id, tab) =>
                {
                    var subPages = webPages.Where(s => s.ParentId == id);
                    foreach (var webPage in subPages)
                    {
                        listItems.Add(new ListItem(FormatTab(tab) + webPage.Name, webPage.Id.ToString()));
                        if (webPage.HasChildren)
                            BuildListItemRecursive(webPages, list, webPage.Id, tab + WConstants.TAB);
                    }
                };
            BuildListItemRecursive(items, listItems, parentId, baseTab);

            return listItems;
        }

        private static string FormatTab(string sTab)
        {
            return !string.IsNullOrEmpty(sTab) ? sTab + WConstants.BULLET : string.Empty;
        }

        public static void BuildTabs(WPage page, WQuery query, ITabControl tabControl)
        {
            query.Remove(WebColumns.PartConfigId);

            var q = query.Clone();

            query.Remove(ObjectKey.KeySource);
            query.Remove(ObjectKey.KeyString);

            var admin = page.PartControlTemplate.PartControl.PartAdmin;

            tabControl.AddTab("tabMain", "General", query.BuildQuery(CentralPages.WebPageHome), CentralPages.WebPageHome);
            tabControl.AddTab("tabContent", "Settings", query.BuildQuery(admin != null && admin.TemplateEngineId == TemplateEngineTypes.Razor ? CentralPages.LoaderRazor : CentralPages.LoaderMain), CentralPages.LoaderMain2);
            tabControl.AddTab("tabPages", "Children", query.BuildQuery(CentralPages.WebChildPages), CentralPages.WebChildPages);
            tabControl.AddTab("tabPanels", "Panels", query.BuildQuery(CentralPages.WebPagePanels), CentralPages.WebPagePanels);
            tabControl.AddTab("tabElements", "Elements", query.BuildQuery(CentralPages.WebPageElements), CentralPages.WebPageElements);

            // General tabs

            WebSystemViewModel.BuildGenericTabs(WebObjects.WebPage, page.Id, q, tabControl);
        }
    }
}
