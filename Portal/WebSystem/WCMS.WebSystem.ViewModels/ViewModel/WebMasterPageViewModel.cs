using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.ViewModel
{
    public class WebMasterPageViewModel
    {
        public static void BuildTabs(int id, WQuery query, ITabControl tabControl)
        {
            var q = query.Clone();

            query.Remove(ObjectKey.KeySource);
            query.Remove(ObjectKey.KeyString);

            tabControl.AddTab("tabMain", "General", query.BuildQuery(CentralPages.WebMasterPageHome), CentralPages.WebMasterPageHome);
            tabControl.AddTab("tabPanels", "Panels", query.BuildQuery(CentralPages.WebPagePanels), CentralPages.WebPagePanels);
            tabControl.AddTab("tabElements", "Elements", query.BuildQuery(CentralPages.WebPageElements), CentralPages.WebPageElements);

            // General tabs

            WebSystemViewModel.BuildGenericTabs(WebObjects.WebMasterPage, id, q, tabControl);
        }
    }
}
