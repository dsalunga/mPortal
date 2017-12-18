using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class WebPartTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                var query = new WQuery(this);
                int partId = query.GetId(WebColumns.PartId);

                WPart item = null;
                if (partId > 0 && (item = WPart.Get(partId)) != null)
                    lblHeader.InnerHtml = item.Name;

                if (!Page.IsPostBack)
                {
                    query.Remove(WebColumns.PartControlId);
                    query.Remove(WebColumns.PartControlTemplateId);

                    TabControl1.AddTab("tabMgmt", "Settings", query.BuildQuery(CentralPages.WebPartManagement), CentralPages.WebPartManagement);
                    if (WSession.Current.IsAdministrator)
                    {
                        TabControl1.AddTab("tabMain", "General", query.BuildQuery(CentralPages.WebPartHome), CentralPages.WebPartHome);
                        TabControl1.AddTab("tabControls", "Controls", query.BuildQuery(CentralPages.WebPartControls), CentralPages.WebPartControls);
                        TabControl1.AddTab("tabAdmin", "Administration", query.BuildQuery(CentralPages.WebPartAdmin), CentralPages.WebPartAdmin);
                        TabControl1.AddTab("tabConfig", "*Configuration", query.BuildQuery(CentralPages.WebPartConfig), CentralPages.WebPartConfig);
                    }
                }

                TabControl1.SelectTab(query.BasePath);
            }
        }
    }
}