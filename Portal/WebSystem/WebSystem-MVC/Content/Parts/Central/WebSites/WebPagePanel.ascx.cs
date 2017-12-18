using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebPagePanelController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int pageId = DataHelper.GetId(Request[WebColumns.PageId]);
                int templatePanelId = DataHelper.GetId(Request[WebColumns.TemplatePanelId]);

                if (pageId > 0 && templatePanelId > 0)
                {
                    WebPagePanel panel = WebPagePanel.Get(templatePanelId, pageId);
                    if (panel != null)
                    {
                        cboPanelUsage.SelectedValue = panel.UsageTypeId.ToString();
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Return();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            int pageId = DataHelper.GetId(Request, WebColumns.PageId);
            int templatePanelId = DataHelper.GetId(Request, WebColumns.TemplatePanelId);

            if (pageId > 0 && templatePanelId > 0)
            {
                WebPagePanel panel = WebPagePanel.Get(templatePanelId, pageId);
                if (panel == null)
                {
                    panel = new WebPagePanel();
                    panel.PageId = pageId;
                    panel.TemplatePanelId = templatePanelId;
                }

                panel.UsageTypeId = Convert.ToInt32(cboPanelUsage.SelectedValue);
                panel.Update();

                this.Return();
            }
        }

        private void Return()
        {
            QueryParser query = new QueryParser(this);
            query.Redirect(CentralPages.WebPagePanelHome);

            //WebRedirector.ReturnFromContentMgt();
        }
    }
}