using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebMasterPageController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int masterPageId = DataHelper.GetId(Request, WebColumns.MasterPageId);
                int siteId = DataHelper.GetId(Request, WebColumns.SiteId);

                cboOwnerPage.Items.AddRange(WebPageViewModel.GenerateListItem(siteId, -1).ToArray());
                if (cboOwnerPage.Items.Count == 0)
                    panelOwnerPage.Visible = false;

                cboPageTemplates.DataBind();

                ObjectDataSourceMasterPages.SelectParameters["siteId"].DefaultValue = siteId.ToString();
                cboParent.DataBind();

                WebMasterPage item = null;
                if (masterPageId > 0 && (item = WebMasterPage.Get(masterPageId)) != null)
                {
                    txtName.Text = item.Name;
                    WebHelper.SetCboValue(cboPageTemplates, item.TemplateId);
                    WebHelper.SetCboValue(cboOwnerPage, item.OwnerPageId);
                    WebHelper.SetCboValue(cboParent, item.ParentId);

                    LoadSkins(item.SkinId);

                    var site = WSite.Get(siteId);
                    chkIsDefault.Checked = site.DefaultMasterPageId == item.Id;

                    #region Security

                    int publicAccess = item.PublicAccess;
                    if ((publicAccess & WebPublicAccess.AllIPAddressExceptEntries) > 0)
                    {
                        chkIPAddress.Checked = true;
                        publicAccess -= WebPublicAccess.AllIPAddressExceptEntries;
                    }

                    if ((publicAccess & WebPublicAccess.AllAccountExceptEntries) > 0)
                    {
                        chkAccount.Checked = true;
                        publicAccess -= WebPublicAccess.AllAccountExceptEntries;
                    }

                    cboPublicAccess.SelectedValue = publicAccess.ToString();

                    ManagementSecurityOption1.Value = item.ManagementAccess;

                    #endregion
                }
                else
                {
                    LoadSkins();
                }
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            int masterPageId = DataHelper.GetId(Request, WebColumns.MasterPageId);
            int siteId = DataHelper.GetId(Request, WebColumns.SiteId);
            int templateId = DataHelper.GetId(cboPageTemplates.SelectedValue);
            WebMasterPage item = null;

            if (masterPageId > 0)
                item = WebMasterPage.Get(masterPageId);

            if (item == null)
                item = new WebMasterPage();

            #region Security

            int publicAccess = DataHelper.GetInt32(cboPublicAccess.SelectedValue);

            if (chkAccount.Checked)
                publicAccess += WebPublicAccess.AllAccountExceptEntries;

            if (chkIPAddress.Checked)
                publicAccess += WebPublicAccess.AllIPAddressExceptEntries;

            item.PublicAccess = publicAccess;

            item.ManagementAccess = ManagementSecurityOption1.Value;

            #endregion

            item.SkinId = DataHelper.GetId(cboThemes.SelectedValue);
            item.Name = txtName.Text.Trim();
            item.SiteId = siteId;
            item.TemplateId = templateId;
            item.ParentId = DataHelper.GetId(cboParent.SelectedValue);
            item.OwnerPageId = DataHelper.GetId(cboOwnerPage.SelectedValue);
            item.Update();

            WSite site = item.Site;
            if (site.DefaultMasterPageId < 1 || chkIsDefault.Checked)
            {
                site.DefaultMasterPageId = item.Id;
                site.Update();
            }

            Return();
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Return();
        }

        private void Return()
        {
            var query = new WQuery(this);
            if (query.GetId(WebColumns.MasterPageId) > 0)
                query.Redirect(CentralPages.WebMasterPageHome);
            else
                query.Redirect(CentralPages.WebMasterPages);
        }

        public IEnumerable<KeyValuePair<int, string>> GetTemplates()
        {
            return from i in WebTemplate.Provider.GetList()
                   select new KeyValuePair<int, string>(
                       i.Id,
                       i.TemplateEngineId == TemplateEngineTypes.Razor ? i.Name : i.Name + " (ASCX)");
        }

        public IEnumerable<KeyValuePair<int, string>> GetMasterPages(int siteId)
        {
            return from i in WebMasterPage.GetList(siteId)
                select new KeyValuePair<int, string>(
                    i.Id,
                    i.Name
            );
        }

        protected void cboPageTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSkins();
        }

        private void LoadSkins(int skinId = -1)
        {
            var item = cboSkins.Items[0];

            cboSkins.Items.Clear();
            cboSkins.Items.Add(item);
            cboSkins.SelectedIndex = 0;

            var templateId = DataHelper.GetId(cboPageTemplates.SelectedValue);
            if (templateId > 0)
            {
                cboSkins.DataSource = WebSkin.Provider.GetList(WebObjects.WebTemplate, templateId);
                cboSkins.DataBind();

                if (cboSkins.Items.Count > 1)
                {
                    panelSkin.Visible = true;

                    if (skinId > 0)
                        if (cboSkins.Items.FindByValue(skinId.ToString()) != null)
                            cboSkins.SelectedValue = skinId.ToString();
                }
                else
                {
                    panelSkin.Visible = false;
                }
            }
            else
            {
                panelSkin.Visible = false;
            }
        }
    }
}