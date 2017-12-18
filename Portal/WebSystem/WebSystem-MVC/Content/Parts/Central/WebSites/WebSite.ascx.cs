using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.WebSystem.Controls;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Common.Utilities;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebSiteController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // Setup tab navigation
                editTab.AddTab("tabProperties", "Properties");
                editTab.AddTab("tabExtended", "Extended Properties");
                editTab.SelectedIndex = 0;

                txtName.Attributes["onblur"] = "if(WCMS.Dom.Get('" + txtUrlName.ClientID + "').value==''){WCMS.Dom.Get('" + txtUrlName.ClientID + "').value=GenerateUrlName(this.value);}";

                int siteId = DataHelper.GetId(Request, WebColumns._SiteId);

                #region Rank

                int iRank = WSite.GetMaxRank();
                if (iRank < 1)
                    iRank = 1;

                for (int i = iRank > 25 ? iRank - 25 : iRank; i <= iRank + 25; i += 5)
                    cboRank.Items.Add(i.ToString());

                #endregion

                // Home Page DropDown
                cboHomePage.Items.AddRange(WebPageViewModel.GenerateListItem(siteId, -1, false).ToArray());

                cboTheme.DataSource = WebTheme.Provider.GetList();
                cboTheme.DataBind();

                WSite site = null;
                if (siteId > 0 && (site = WSite.Get(siteId)) != null)
                {
                    if (cboRank.Items.FindByValue(site.Rank.ToString()) == null)
                        cboRank.Items.Insert(0, site.Rank.ToString());

                    lblTitle.InnerHtml = site.Name;
                    txtName.Text = site.Name;
                    txtUrlName.Text = site.Identity;
                    txtTitle.Text = site.Title;
                    txtTitleFormat.Text = site.PageTitleFormat;
                    txtLoginPage.Text = site.LoginPage;
                    txtBaseAddress.Text = site.BaseAddress;
                    cboRank.SelectedValue = site.Rank.ToString();
                    chkIsActive.Checked = site.Active == 1;

                    #region Security

                    int publicAccess = site.PublicAccess;
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

                    // Default Master Page
                    cboDefaultMasterPage.DataBind();
                    if (cboDefaultMasterPage.Items.Count > 0)
                        cboDefaultMasterPage.SelectedValue = site.DefaultMasterPageId.ToString();

                    // Home Page
                    cboHomePage.DataBind();
                    if (cboHomePage.Items.Count > 0)
                        cboHomePage.SelectedValue = site.HomePageId.ToString();

                    // Identity
                    cboPrimaryIdentity.DataSource = WebSiteIdentity.Provider.GetList(siteId);
                    cboPrimaryIdentity.DataBind();
                    WebHelper.SetCboValue(cboPrimaryIdentity, site.PrimaryIdentityId);
                    panelPrimaryIdentity.Visible = true;

                    ManagementSecurityOption1.Value = site.ManagementAccess;
                    WebHelper.SetCboValue(cboTheme, site.ThemeId);
                }
                else
                {
                    // Generate New Rank
                    cboRank.SelectedValue = (iRank + 5).ToString();

                    int parentId = DataHelper.GetId(Request, "ParentSiteId");
                    if (parentId > 0) { }
                    else
                    {
                        cboPublicAccess.SelectedValue = WebPublicAccess.Anonymous.ToString();
                    }

                    panelHomePage.Visible = false;
                    panelMasterPage.Visible = false;
                }
            }
        }

        protected void editTab_OnSelectedTabChanged(object oSender, TabEventArgs args)
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

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            ReturnPage();
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            int siteId = DataHelper.GetId(Request, WebColumns._SiteId);
            int parentId = DataHelper.GetId(Request, "ParentSiteId");

            WSite site = null;
            if (siteId > 0 && (site = WSite.Get(siteId)) != null)
            {
                // Update
                int masterPageId = DataHelper.GetId(cboDefaultMasterPage.SelectedValue);
                site.HomePageId = DataHelper.GetId(cboHomePage.SelectedValue);
                site.DefaultMasterPageId = masterPageId;
                site.PrimaryIdentityId = DataHelper.GetId(cboPrimaryIdentity.SelectedValue);
            }
            else
            {
                // Insert
                site = new WSite();
                site.ParentId = parentId;
            }

            #region Security

            int publicAccess = DataHelper.GetInt32(cboPublicAccess.SelectedValue);
            if (chkAccount.Checked)
                publicAccess += WebPublicAccess.AllAccountExceptEntries;

            if (chkIPAddress.Checked)
                publicAccess += WebPublicAccess.AllIPAddressExceptEntries;

            site.PublicAccess = publicAccess;
            site.ManagementAccess = ManagementSecurityOption1.Value;

            #endregion

            site.Name = txtName.Text.Trim();
            site.Identity = txtUrlName.Text.Trim();
            site.Title = txtTitle.Text.Trim();
            site.PageTitleFormat = txtTitleFormat.Text.Trim();
            site.LoginPage = txtLoginPage.Text.Trim();
            site.BaseAddress = txtBaseAddress.Text.Trim().TrimEnd('/');
            site.Active = chkIsActive.Checked ? 1 : 0;
            site.Rank = int.Parse(cboRank.SelectedValue);
            site.ThemeId = DataHelper.GetId(cboTheme.SelectedValue);
            site.Update();

            ReturnPage();
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            if (!string.IsNullOrEmpty(query.Get(WebColumns._SiteId)))
            {
                query.Remove(WebColumns._SiteId);
                query.Redirect(CentralPages.WebSiteHome);
            }
            else
            {
                int parentId = query.GetId(WebColumns.ParentSiteId);
                query.Redirect(parentId > 0 ? CentralPages.WebChildSites : CentralPages.WebSites);
            }
        }

        public IEnumerable<WebMasterPage> GetMasterPages(int siteId)
        {
            if (siteId > 0)
                return WebMasterPage.GetList(siteId);

            return null;
        }
    }
}