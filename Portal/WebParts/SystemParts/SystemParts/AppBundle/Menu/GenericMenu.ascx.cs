using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Diagnostics;


using DbMenuItem = WCMS.WebSystem.WebParts.Menu.MenuItem;
using WebMenuItem = System.Web.UI.WebControls.MenuItem;

namespace WCMS.WebSystem.WebParts.Menu
{
    public partial class GenericMenuPresenter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sw = PerformanceLog.StartLog();
            var output = new StringBuilder();
            var context = new WContext(this);
            var pageUrl = context.Page.BuildRelativeUrl();

            int menuId = -1;
            int parameterSetId = -1;
            int renderMode = RenderModes.Absolute;

            var menuObject = MenuObject.Provider.Get(context.ObjectId, context.RecordId);
            if (menuObject != null)
            {
                menuId = menuObject.MenuId;
                parameterSetId = menuObject.ParameterSetId;
                renderMode = menuObject.RenderMode;
            }

            // Prepare the menu template
            var paramSet = parameterSetId > 0 ? WebParameterSet.Provider.Get(parameterSetId) as ParameterizedWebObject : null;
            var element = paramSet ?? context.Element;
            var model = new MenuModel(element.GetParameterValue(ParameterKeys.ItemTemplate, null));
            var siteMapMode = DataHelper.GetBool(element.GetParameterValue("SiteMapMode"), false);

            //menu.Header = element.GetParameterValue(ParameterKeys.Header, null);
            //menu.Footer = element.GetParameterValue(ParameterKeys.Footer, null);
            //menu.BodyTemplate = element.GetParameterValue(ParameterKeys.BodyTemplate, null);
            model.Separator = element.GetParameterValue(ParameterKeys.Separator, null);
            model.SeparatorItemTemplate = element.GetParameterValue(ParameterKeys.SeparatorItem, null);
            model.HeaderItemTemplate = element.GetParameterValue(ParameterKeys.HeaderItem, null);

            model.FirstItemTemplate = element.GetParameterValue(ParameterKeys.FirstItemTemplate, null);
            model.SelectedItemTemplate = element.GetParameterValue(ParameterKeys.SelectedItemTemplate, null);
            model.UpOneLevelTemplate = element.GetParameterValue(ParameterKeys.UpOneLevelTemplate, null);
            model.SeeAlsoTemplate = element.GetParameterValue(ParameterKeys.SeeAlsoTemplate, null);
            model.AlternateItemTemplate = element.GetParameterValue(ParameterKeys.AlternateItemTemplate, null);
            model.AlternateParentItemTemplate = element.GetParameterValue(ParameterKeys.AlternateParentItemTemplate, null);
            model.AlternateRootItemTemplate = element.GetParameterValue(ParameterKeys.AlternateRootItemTemplate, null);
            model.RootItemTemplate = element.GetParameterValue(ParameterKeys.RootItemTemplate, null);
            model.RootItemNoChildrenTemplate = element.GetParameterValue(ParameterKeys.RootItemNoChildrenTemplate, null);
            model.ParentItemTemplate = element.GetParameterValue(ParameterKeys.ParentItemTemplate, null);
            model.SelectedParentItemTemplate = element.GetParameterValue(ParameterKeys.SelectParentItemTemplate, null);
            model.SelectedRootItemTemplate = element.GetParameterValue(ParameterKeys.SelectedRootItemTemplate, null);
            
            model.ContainerTemplate = element.GetParameterValue(ParameterKeys.ContainerTemplate, null);
            model.BuildItems(menuId, pageUrl, renderMode, siteMapMode);
            literalOutput.Text = model.RenderAsText();

            PerformanceLog.EndLog(string.Format("Generic Menu: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
        }
    }
}