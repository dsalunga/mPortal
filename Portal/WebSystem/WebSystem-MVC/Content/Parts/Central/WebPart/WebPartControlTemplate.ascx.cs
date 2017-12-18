using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebPartControlTemplateController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new WQuery(this);
                int id = query.GetId(WebColumns.PartControlTemplateId);
                WebPartControlTemplate item = null;
                if (id > 0 &&
                    (item = WebPartControlTemplate.Get(id)) != null)
                {
                    txtName.Text = item.Name;
                    txtIdentity.Text = item.Identity;
                    //txtFileName.Text = template.FileName;
                    txtPath.Text = item.Path;
                    chkStandalone.Checked = item.IsStandalone;

                    WebHelper.SetCboValue(cboTemplateEngine, item.TemplateEngineId);
                    //imageThumbnail.ImageUrl = "Handlers/Handler.ashx?Section=SectionTemplate&ID=" + CSITID;
                }
                else
                {
                    int partControlId = query.GetId(WebColumns.PartControlId);
                    WebPartControl partControl = null;
                    if (partControlId > 0 && (partControl = WebPartControl.Get(partControlId)) != null)
                    {
                        txtIdentity.Text = partControl.Identity;
                        txtName.Text = partControl.Name;
                        txtPath.Text = string.Format("~/Content/Parts/{0}/FileName.ascx", partControl.Part.Identity);
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.Return();
        }

        public void Return()
        {
            var query = new WQuery(this);
            int id = query.GetId(WebColumns.PartControlTemplateId);
            if (id > 0)
                query.Redirect(CentralPages.WebPartControlTemplateHome);
            else
                query.Redirect(CentralPages.WebPartControlTemplates);
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            this.UpdateData(false);
        }

        private void UpdateData(bool updateContinue)
        {
            var query = new WContext(this);
            int partControlTemplateId = query.GetId(WebColumns.PartControlTemplateId);
            int partControlId = query.GetId(WebColumns.PartControlId);

            WebPartControlTemplate item = null;
            if (partControlTemplateId > 0 && (item = WebPartControlTemplate.Get(partControlTemplateId)) != null)
            { }
            else
            {
                item = new WebPartControlTemplate();
                item.PartControlId = partControlId;
            }

            item.Name = txtName.Text.Trim();
            item.Path = txtPath.Text.Trim();
            item.FileName = Path.GetFileName(item.Path); //txtFileName.Text.Trim();
            item.Identity = txtIdentity.Text.Trim();
            item.IsStandalone = chkStandalone.Checked;
            item.TemplateEngineId = DataHelper.GetInt32(cboTemplateEngine.SelectedValue);
            item.Update();

            if (updateContinue)
            {
                query.Set(WebColumns.PartControlTemplateId, item.Id);
                query.Redirect(CentralPages.WebPartControlTemplateHome);
            }
        }

        protected void cmdUpdateContinue_Click(object sender, EventArgs e)
        {
            this.UpdateData(true);
        }
    }
}