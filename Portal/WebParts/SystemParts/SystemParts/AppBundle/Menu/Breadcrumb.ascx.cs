using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Diagnostics;
using WCMS.Framework.Core;


namespace WCMS.WebSystem.WebParts.Menu
{
    public partial class BreadcrumbController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var element = context.Element;
            var page = context.Page;
            var site = context.Site;

            var pageUrl = page.BuildRelativeUrl();
            var set = context.GetParameterSet();

            var firstItemTemplate = ParameterizedWebObject.GetValue(ParameterKeys.FirstItemTemplate, element, set);
            //var lastItemTemplate = set.GetParameterValue(ParameterKeys.LastItemTemplate);

            var itemTemplate = ParameterizedWebObject.GetValue(ParameterKeys.ItemTemplate, element, set);
            var containerTemplate = ParameterizedWebObject.GetValue(ParameterKeys.ContainerTemplate, element, set);
            var separator = ParameterizedWebObject.GetValue(ParameterKeys.Separator, element, set);

            bool hasSeparator = !string.IsNullOrEmpty(separator);

            var menuId = DataHelper.GetId(ParameterizedWebObject.GetValue(ParameterKeys.MenuId, element, set));
            IEnumerable<MenuItem> items = null;
            MenuItem item = null;
            MenuItem home = null;

            if (menuId > 0)
            {
                // Use a custom menu
                items = MenuItem.Provider.GetList(menuId);
                item = items.FirstOrDefault(i => i.PageUrl.StartsWith(pageUrl, StringComparison.InvariantCultureIgnoreCase));

                bool itemFound = item != null;
                bool homeFound = false;

                foreach (var i in items)
                {
                    if (!itemFound && i.PageUrl.StartsWith(pageUrl, StringComparison.InvariantCultureIgnoreCase))
                    {
                        itemFound = true;
                        item = i;

                        if (homeFound)
                            break;
                    }

                    if (!homeFound && i.PageId == site.HomePageId)
                    {
                        homeFound = true;
                        home = i;

                        if (itemFound)
                            break;
                    }
                }
            }
            else
            {
                // Build directly from page structure
                item = new MenuItem(page, true);

                var pageHome = site.HomePage;
                if (pageHome != null)
                    home = new MenuItem(pageHome, false);
            }


            StringBuilder builder = new StringBuilder();

            Action<MenuItem, string> BuildEntry = (menuItem, template) =>
            {
                NamedValueProvider values = new NamedValueProvider();
                values.Add(TemplateKeys.Target, menuItem.Target);
                values.Add(TemplateKeys.TargetProperty,
                    string.IsNullOrEmpty(menuItem.Target) ? string.Empty : string.Format(@" target=""{0}""", menuItem.Target));
                values.Add(TemplateKeys.Text, menuItem.Text);
                values.Add(TemplateKeys.Url, menuItem.PageUrl);

                builder.Insert(0, values.Substitute(template));
            };

            Action CreateHomeEntry = () =>
            {
                if (home != null)
                {
                    if (builder.Length > 0)
                        if (hasSeparator)
                            builder.Insert(0, separator);

                    BuildEntry(home, string.IsNullOrEmpty(firstItemTemplate) ? itemTemplate : firstItemTemplate);
                }
            };

            if (page.Id == site.HomePageId)
            {
                if (item.PageId > 0)
                {
                    // Last item and is a valid page
                    if (item.PageId != site.HomePageId)
                        CreateHomeEntry(); // Add Home Page
                }
                else
                {
                    CreateHomeEntry(); // Add Home Page
                }
            }
            else
            {
                while (item != null)
                {
                    MenuItem lastItem = item;

                    while (true)
                    {
                        lastItem = lastItem.Parent;
                        if (lastItem == null || lastItem != null && lastItem.IsActive && lastItem.IsVisible)
                            break;
                    }

                    // Prepare Substituter parameters
                    BuildEntry(item, itemTemplate);

                    if (lastItem != null && hasSeparator)
                        builder.Insert(0, separator);

                    if (lastItem == null)
                    {
                        if (item.PageId > 0)
                        {
                            // Last item and is a valid page
                            if (item.PageId != site.HomePageId)
                                CreateHomeEntry(); // Add Home Page
                        }
                        else
                        {
                            CreateHomeEntry(); // Add Home Page
                        }
                    }

                    item = lastItem;
                }
            }

            if (builder.Length == 0)
                CreateHomeEntry();

            literalOutput.Text = string.IsNullOrEmpty(containerTemplate) ? builder.ToString() : Substituter.Substitute(containerTemplate, builder.ToString());
        }
    }
}