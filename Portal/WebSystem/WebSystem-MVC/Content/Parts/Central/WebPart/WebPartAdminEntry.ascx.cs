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
    public partial class WebPartAdminEntry : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int partId = DataHelper.GetId(Request, WebColumns.PartId);
                int partAdminId = DataHelper.GetId(Request, WebColumns.PartAdminId);

                WPart part = WPart.Get(partId);
                WebPartAdmin item = null;
                cmdFile.Attributes.Add("onclick", string.Format("Upload('{0}','/Content/Parts/{1}','&FileOnly=true');", txtControlURL.ClientID, part.Identity));

                cboParts.DataSource = WPart.GetList();
                cboParts.DataBind();
                WebHelper.SetCboValue(cboParts, partId);

                if (partAdminId > 0 && (item = WebPartAdmin.Get(partAdminId)) != null)
                {
                    txtName.Text = item.Name;
                    txtControlURL.Text = item.FileName;
                    chkActive.Checked = item.IsActive;
                    chkVisible.Checked = item.IsVisible;
                    chkInSiteContext.Checked = item.IsInSiteContext;
                    chkAutoTitle.Checked = item.IsAutoTitle;
                    WebHelper.SetCboValue(cboTemplateEngine, item.TemplateEngineId);
                }
                else
                {
                    // Create NEW
                }
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            Return();
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            var query = new WQuery(this);

            //var oldPartId = query.GetId(WebColumns.PartId);
            var partAdminId = query.GetId(WebColumns.PartAdminId);
            var parentId = query.GetId(WebColumns.ParentId);
            var partId = DataHelper.GetId(cboParts.SelectedValue);

            WebPartAdmin item = null;
            if (partAdminId < 1 || (item = WebPartAdmin.Get(partAdminId)) == null)
            {
                item = new WebPartAdmin();
                item.ParentId = parentId;
            }

            item.PartId = partId;
            item.Name = txtName.Text.Trim();
            item.FileName = txtControlURL.Text.Trim();
            item.IsActive = chkActive.Checked;
            item.IsVisible = chkVisible.Checked;
            item.IsAutoTitle = chkAutoTitle.Checked;
            item.IsInSiteContext = chkInSiteContext.Checked;
            item.TemplateEngineId = DataHelper.GetInt32(cboTemplateEngine.SelectedValue);
            item.Update();
            Return();
        }

        private void Return()
        {
            var query = new QueryParser(this);
            query.Remove(WebColumns.PartAdminId);
            query.Redirect(CentralPages.WebPartAdmin);
        }
    }
}