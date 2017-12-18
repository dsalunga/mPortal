using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebPartTemplatePanelController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new QueryParser(this);
                var templatePanelId = query.GetId(WebColumns.TemplatePanelId);

                WebTemplatePanel item = null;
                if (templatePanelId > 0 && (item = WebTemplatePanel.Get(templatePanelId)) != null)
                {
                    txtName.Text = item.Name;
                    txtPanelName.Text = item.PanelName;
                    txtRank.Text = item.Rank.ToString();

                    var template = item.Template;
                    if (template != null && template.PrimaryPanelId == item.Id)
                        chkSetDefault.Checked = true;
                }
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.Redirect();
        }

        private void Redirect()
        {
            var query = new QueryParser(this);
            query.Remove(WebColumns.TemplatePanelId);
            query.Redirect(CentralPages.WebPartTemplatePanels);
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            int panelId = DataHelper.GetId(Request, WebColumns.TemplatePanelId);
            int templateId = DataHelper.GetId(Request, WebColumns.PartControlTemplateId);

            var item = (panelId > 0) ? WebTemplatePanel.Get(panelId) : new WebTemplatePanel();
            item.Name = txtName.Text.Trim();
            item.PanelName = txtPanelName.Text.Trim();
            item.Rank = DataHelper.GetInt32(txtRank.Text.Trim());
            item.ObjectId = WebObjects.WebPartControlTemplate;
            item.RecordId = templateId;
            item.Update();

            if (chkSetDefault.Checked)
            {
                var template = item.Template;
                if (template != null && template.PrimaryPanelId != item.Id)
                {
                    template.PrimaryPanelId = item.Id;
                    template.Update();
                }
            }

            this.Redirect();
        }
    }
}