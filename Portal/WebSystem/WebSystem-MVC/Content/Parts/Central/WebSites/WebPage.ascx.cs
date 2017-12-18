using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;

using WCMS.WebSystem.Controls;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebPageController : System.Web.UI.UserControl
    {
        // Setup tab navigation
        protected void tabNavigation_SelectedTabChanged(object oSender, TabEventArgs args)
        {
            switch (args.TabName)
            {
                case "tabProperties":
                    MultiView1.SetActiveView(viewBasic);
                    break;

                case "tabExtended":
                    MultiView1.SetActiveView(viewAdvanced);
                    break;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                tabNavigation.AddTab("tabProperties", "Properties");
                tabNavigation.AddTab("tabExtended", "Extended Properties");

                lblExtension.InnerHtml = WConfig.HasPageExt ? WConfig.PageExt : "/";

                int partControlTemplateId = 1;
                int _pageId = DataHelper.GetId(Request, WebColumns._PageId);
                WPage item = _pageId > 0 ? WPage.Get(_pageId) : null;
                var siteId = item == null ? DataHelper.GetId(Request, WebColumns.SiteId) : item.SiteId;

                txtName.Attributes["onblur"] = "if(WCMS.Dom.Get('" + txtIdentityName.ClientID + "').value==''){WCMS.Dom.Get('" + txtIdentityName.ClientID + "').value=GenerateUrlName(this.value);}";

                int iRank = WPage.GetMaxRank(siteId);
                if (iRank < 1) iRank = 1;

                for (int i = iRank > WConstants.ElementRankInterval ? iRank - WConstants.ElementRankInterval : iRank; i <= iRank + WConstants.ElementRankInterval; i += 5)
                    cboRank.Items.Add(i.ToString());

                if (item != null)
                {
                    if (cboRank.Items.FindByValue(item.Rank.ToString()) == null)
                        cboRank.Items.Insert(0, item.Rank.ToString());

                    // Load Page information
                    txtName.Text = item.Name;
                    txtIdentityName.Text = item.Identity;
                    txtTitle.Text = item.Title;
                    lblTitle.InnerHtml = item.Name;

                    var parentPage = item.Parent;
                    if (parentPage != null)
                        txtParentUrl.Text = item.Parent.BuildRelativeUrl();

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

                    #endregion

                    hiddenCSITID.Value = item.PartControlTemplateId.ToString();
                    chkIsActive.Checked = item.Active == 1;
                    cboRank.SelectedValue = item.Rank.ToString();

                    partControlTemplateId = item.PartControlTemplateId;

                    cboMasterPage.DataBind();
                    cboMasterPage.SelectedValue = item.MasterPageId.ToString();

                    cboPageType.SelectedValue = item.PageType.ToString();
                    chkUsePartTemplatePath.Checked = item.UsePartTemplatePath == 1;
                    ManagementSecurityOption1.Value = item.ManagementAccess;

                    LoadThemes(item.SkinId);

                    // Url Pre
                    lblUrlPre.InnerHtml = WebRewriter.BuildPreUrl(item);
                }
                else
                {
                    // ADD NEW:
                    cboRank.SelectedValue = (iRank + 5).ToString();

                    LoadThemes();

                    var pageId = DataHelper.GetId(Request, WebColumns.PageId);
                    if (pageId > 0 && (item = WPage.Get(pageId)) != null)
                    {
                        lblUrlPre.InnerHtml = WebRewriter.BuildPreUrl(item, true);
                        txtParentUrl.Text = item.BuildRelativeUrl();
                    }
                    else if (siteId > 0)
                    {
                        var site = WSite.Get(siteId);
                        lblUrlPre.InnerHtml = WebRewriter.BuildPreUrl(site, true);
                    }
                }

                // WebPart
                hiddenCSITID.Value = partControlTemplateId.ToString();
                hiddenTempCSITID.Value = partControlTemplateId.ToString();
                this.DisplayWebPartInfo(partControlTemplateId);
            }
        }

        protected void cboMasterPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadThemes();
        }

        private void LoadThemes(int themeId = -1)
        {
            var item = cboThemes.Items[0];

            cboThemes.Items.Clear();
            cboThemes.Items.Add(item);
            cboThemes.SelectedIndex = 0;

            WebMasterPage masterPage = null;
            var masterPageId = DataHelper.GetId(cboMasterPage.SelectedValue);
            if (masterPageId > 0 && (masterPage = WebMasterPage.Get(masterPageId)) != null)
            {
                if (masterPage.TemplateId > 0)
                {
                    cboThemes.DataSource = WebSkin.Provider.GetList(WebObjects.WebMasterPage, masterPage.TemplateId);
                    cboThemes.DataBind();

                    if (themeId > 0)
                        if (cboThemes.Items.FindByValue(themeId.ToString()) != null)
                            cboThemes.SelectedValue = themeId.ToString();
                }
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            var query = new WQuery(this);
            int siteId = query.GetId(WebColumns.SiteId);
            int pageId = query.GetId(WebColumns.PageId);

            WPage page = pageId > 0 ? WPage.Get(pageId) : null;
            WSite site = page != null ? page.Site : (siteId > 0 ? WSite.Get(siteId) : null);

            query.Remove(WebColumns._PageId);

            if (page != null)
            {
                query.Redirect(CentralPages.WebPageHome);
            }
            else if (siteId > 0)
            {
                query.Remove(WebColumns.PageId);
                query.Redirect(CentralPages.WebPages);
            }
            else if (page != null)
            {
                query.Redirect(page.BuildRelativeUrl());
            }
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            var item = this.SavePage();
            if (item.Id > 0)
            {
                var query = new WQuery(this);
                int editId = query.GetId(WebColumns._PageId);
                int pageId = query.GetId(WebColumns.PageId);
                int siteId = query.GetId(WebColumns.SiteId);

                query.Remove(WebColumns._PageId);

                if (editId > 0)
                    query.Redirect(CentralPages.WebPageHome);
                else if (pageId > 0)
                {
                    query.Set(WebColumns.PageId, pageId);
                    query.Set(WebColumns.SiteId, item.SiteId);
                    query.Redirect(CentralPages.WebChildPages);
                }
                else if (siteId > 0)
                    query.Redirect(CentralPages.WebPages);
                else
                    query.Redirect(item.BuildRelativeUrl());
            }
        }

        protected void cmdNext_Click(object sender, EventArgs e)
        {
            var item = this.SavePage();
            if (item.Id > 0)
            {
                var query = new WQuery(this);
                query.Set(WebColumns.PageId, item.Id);
                query.Remove(WebColumns._PageId);
                query.Redirect(CentralPages.LoaderMain);
            }
        }

        private WPage SavePage()
        {
            int siteId = DataHelper.GetId(Request, WebColumns.SiteId);
            int editId = DataHelper.GetId(Request, WebColumns._PageId);
            int pageId = DataHelper.GetId(Request, WebColumns.PageId);

            var templateId = DataHelper.GetId(hiddenCSITID.Value);
            if (templateId <= 0)
                throw new Exception("A WebPart must be selected");

            //// VALIDATION GOES HERE...
            WPage page = null;
            if (editId > 0)
            {
                page = WPage.Get(editId);
            }
            else if (siteId > 0)
            {
                page = new WPage();
                page.SiteId = siteId;
                page.ParentId = pageId;
            }
            else
            {
                throw new Exception("SiteId must be present");
            }

            #region Security

            int publicAccess = DataHelper.GetInt32(cboPublicAccess.SelectedValue);
            if (chkAccount.Checked)
                publicAccess += WebPublicAccess.AllAccountExceptEntries;

            if (chkIPAddress.Checked)
                publicAccess += WebPublicAccess.AllIPAddressExceptEntries;

            page.PublicAccess = publicAccess;
            page.ManagementAccess = ManagementSecurityOption1.Value;

            #endregion

            var site = page.Site;
            var parentUrl = txtParentUrl.Text.Trim();
            var parentPage = parentUrl.Equals("/") ? null : parentUrl.StartsWith("/") ? WPage.SelectNode(site.Id, parentUrl) : WebRewriter.ResolvePage(parentUrl);
            if (parentPage != null && page.SiteId == parentPage.SiteId)
                page.ParentId = parentPage.Id;
            else
                page.ParentId = WConstants.NULL_ID;

            page.Name = txtName.Text.Trim();
            page.Rank = Convert.ToInt32(cboRank.SelectedValue);
            page.Active = chkIsActive.Checked ? 1 : 0;
            page.Identity = txtIdentityName.Text.Trim();
            page.Title = txtTitle.Text.Trim();
            page.MasterPageId = DataHelper.GetInt32(cboMasterPage.SelectedValue);
            page.PartControlTemplateId = templateId;
            page.PageType = DataHelper.GetInt32(cboPageType.SelectedValue);
            page.UsePartTemplatePath = chkUsePartTemplatePath.Checked ? 1 : 0;
            page.SkinId = DataHelper.GetId(cboThemes.SelectedValue);
            page.Update();

            // Update site's home page if not yet set
            if (site.HomePageId < 1)
            {
                site.HomePageId = page.Id;
                site.Update();
            }

            return page;
        }

        protected void viewModuleChooser_Activate(object sender, EventArgs e)
        {
            this.PopulateModuleTree();

            // HIDE OBJECTS
            trControlBox.Visible = false;
            tabNavigation.Visible = false;
        }

        protected void imgPreview_Click(object sender, ImageClickEventArgs e)
        {
            MultiView1.SetActiveView(viewModuleChooser);
        }

        protected void cmdOK_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewBasic);

            trControlBox.Visible = true;
            tabNavigation.Visible = true;

            hiddenCSITID.Value = hiddenTempCSITID.Value;
            this.DisplayWebPartInfo(DataHelper.GetId(hiddenCSITID.Value));
        }

        protected void cmdTemplateCancel_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewBasic);

            trControlBox.Visible = true;
            tabNavigation.Visible = true;
        }

        private void PopulateModuleTree()
        {
            // LOAD MODULES
            int partControlTemplateId = DataHelper.GetId(hiddenCSITID.Value);

            var tnRoot = WebPartViewModel.GenerateModuleChooserTree(partControlTemplateId, hiddenTempCSITID.ClientID, chkShowAll.Checked);
            tv1.Nodes.Clear();
            tv1.Nodes.Add(tnRoot);
        }

        private void DisplayWebPartInfo(int partControlTemplateId)
        {
            var template = WebPartControlTemplate.Get(partControlTemplateId);
            if (template != null)
            {
                // MODULE INFO
                var partControl = template.PartControl;

                imgPreview.ImageUrl = "~/Content/Assets/Images/PartThumb.jpg";
                lTemplateName.Text = string.Format("<strong style=\"font-size: larger;\">{0}</strong><br />{1}<br />{2}",
                    template.Name, partControl.Name, partControl.Part.Name);
            }
        }

        private bool ValidateIdentity(string identityName)
        {
            return (identityName.IndexOfAny(WConstants.InvalidPathChars) < 0);
        }

        public DataSet GetMasterPages(int siteId, int pageId)
        {
            if (siteId < 1 && pageId > 0)
                siteId = WPage.Get(pageId).SiteId;

            return DataHelper.ToDataSet(WebMasterPage.GetList(siteId));
        }

        protected void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            PopulateModuleTree();
        }
    }
}