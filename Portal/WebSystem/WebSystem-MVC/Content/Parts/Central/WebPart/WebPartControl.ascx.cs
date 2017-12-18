using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;
using System.IO;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebPartControlController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                var query = new WQuery(this);
                var partId = query.GetId(WebColumns.PartId);
                if (partId == -1)
                {
                    query.Redirect(CentralPages.WebParts);
                    return;
                }
                
                WebPartControl item = null;
                PopulatePartAdmins();

                cboParts.DataSource = WPart.GetList();
                cboParts.DataBind();
                WebHelper.SetCboValue(cboParts, partId);

                int partControlId = query.GetId(WebColumns.PartControlId);
                if (partControlId > 0 && (item = WebPartControl.Get(partControlId)) != null)
                {
                    txtName.Text = item.Name;
                    txtIdentity.Text = item.Identity;
                    chkEntryPoint.Checked = item.EntryPoint == 1;

                    panelConfig.Visible = true;
                    cboConfig.DataBind();
                    if (cboConfig.Items.FindByValue(item.ConfigFileName) != null)
                        cboConfig.SelectedValue = item.ConfigFileName;

                    if (item.PartAdminId > 0)
                        if (cboPartAdmins.Items.FindByValue(item.PartAdminId.ToString()) != null)
                            cboPartAdmins.SelectedValue = item.PartAdminId.ToString();
                }
                else
                {
                    WPart part = null;
                    if (partId > 0 && (part = WPart.Get(partId)) != null)
                        txtIdentity.Text = part.Identity;
                    panelPath.Visible = true;
                }
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            var query = new WQuery(this);
            if (query.GetId(WebColumns.PartControlId) > 0)
            {
                query.Redirect(CentralPages.WebPartControlHome);
            }
            else
            {
                query.Remove(WebColumns.PartControlId);
                query.Redirect(CentralPages.WebPartControls);
            }
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            bool isNew = false;
            var query = new WQuery(this);
            WebPartControl item = null;

            var partControlId = query.GetId(WebColumns.PartControlId);
            var partId = DataHelper.GetId(cboParts.SelectedValue); //query.GetId(WebColumns.PartId);
            if (partControlId > 0 && (item = WebPartControl.Get(partControlId)) != null) { }
            else
            {
                item = new WebPartControl();
                isNew = true;
            }
            item.PartId = partId;
            item.Name = txtName.Text.Trim();
            item.Identity = txtIdentity.Text.Trim();
            item.PartAdminId = DataHelper.GetId(cboPartAdmins.SelectedValue);
            item.ConfigFileName = cboConfig.SelectedValue;
            item.EntryPoint = chkEntryPoint.Checked ? 1 : 0;
            item.Update();

            query.Set(WebColumns.PartId, partId);
            if (isNew)
            {
                query.Set(WebColumns.PartControlId, item.Id);
                var path = txtPath.Text.Trim();
                if (!string.IsNullOrEmpty(path))
                {
                    var template = new WebPartControlTemplate();
                    template.PartControlId = item.Id;
                    template.Name = txtName.Text.Trim();
                    template.Path = path;
                    template.FileName = Path.GetFileName(path); //txtFileName.Text.Trim();
                    template.Identity = txtIdentity.Text.Trim();
                    template.IsStandalone = false;
                    template.TemplateEngineId = TemplateEngineTypes.GetValue(Path.GetExtension(path));
                    template.Update();

                    query.Set(WebColumns.PartControlTemplateId, template.Id);
                    query.Redirect(CentralPages.WebPartControlTemplateHome);
                }
                else
                {
                    query.Redirect(CentralPages.WebPartControlTemplate);
                }
            }
            else
            {
                query.Redirect(CentralPages.WebPartControlHome);
            }
        }

        private void PopulatePartAdmins()
        {
            var query = new WQuery(this);
            int partId = query.GetId(WebColumns.PartId);
            var part = WPart.Get(partId);
            var items = WebPartAdmin.GetList(part.Id);
            PopulateRecursivePartAdmins(-1, items, "");
        }

        private bool PopulateRecursivePartAdmins(int parentId, IEnumerable<WebPartAdmin> items, string tab)
        {
            tab += WConstants.TAB;
            var subItems = items.Where(item => item.ParentId == parentId);
            foreach (WebPartAdmin item in subItems)
            {
                var item1 = new ListItem(tab + "\u2022\u00a0" + item.Name, item.Id.ToString());
                cboPartAdmins.Items.Add(item1);
                this.PopulateRecursivePartAdmins(item.Id, items, tab);
            }

            return true;
        }

        public IQueryable<WebPartConfig> Select(int partId)
        {
            return WebPartConfig.GetList(partId).AsQueryable();
        }
    }
}