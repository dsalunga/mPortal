using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;
using WCMS.Framework;

namespace WCMS.WebSystem.ViewModel
{
    public class WebPageElementViewModel
    {
        public static void BuildTabs(WebPageElement element, WQuery query, ITabControl tabControl)
        {
            var q = query.Clone();

            query.Remove(ObjectKey.KeySource);
            query.Remove(ObjectKey.KeyString);

            var admin = element.PartControlTemplate.PartControl.PartAdmin;

            tabControl.AddTab("tabConfigure", "General", query.BuildQuery(CentralPages.WebPageElementHome), CentralPages.WebPageElementHome);
            tabControl.AddTab("tabContent", "Settings", query.BuildQuery(admin != null && admin.TemplateEngineId == TemplateEngineTypes.Razor ? CentralPages.LoaderRazor : CentralPages.LoaderMain), CentralPages.LoaderMain2);

            WebSystemViewModel.BuildGenericTabs(WebObjects.WebPageElement, element.Id, q, tabControl);
        }
    }
}
