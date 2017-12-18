using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class WebBindingController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboSites.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());

                int id = DataHelper.GetId(Request, "SiteIdentityId");
                if (id > 0)
                {
                    var item = WebSiteIdentity.Provider.Get(id);
                    if (item != null)
                    {
                        txtIP.Text = item.IPAddress;
                        txtHostHeader.Text = item.HostName;
                        txtPort.Text = item.Port.ToString();
                        txtUrlPath.Text = item.UrlPath;
                        txtRedirectUrl.Text = item.RedirectUrl;

                        cboSites.DataBind();
                        WebHelper.SetCboValue(cboSites, item.SiteId);
                        WebHelper.SetCboValue(cboProtocols, item.ProtocolId);
                    }
                }

                var siteId = DataHelper.GetId(Request, WebColumns.SiteId);
                if (siteId > 0)
                {
                    if (cboSites.SelectedIndex == 0)
                        WebHelper.SetCboValue(cboSites, siteId);
                    cboSites.Enabled = false;
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ReturnPage();
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.Remove("SiteIdentityId");
            query.Redirect(CentralPages.WebIdentities);
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            int siteId = int.Parse(cboSites.SelectedValue);
            int id = DataHelper.GetId(Request, "SiteIdentityId");
            WebSiteIdentity item = null;

            if (id > 0)
            {
                item = WebSiteIdentity.Provider.Get(id);
            }
            else
            {
                item = new WebSiteIdentity();
                item.SiteId = DataHelper.GetId(Request, WebColumns.SiteId);
            }

            item.SiteId = siteId;
            item.HostName = txtHostHeader.Text.Trim();
            item.Port = DataHelper.GetInt32(txtPort.Text.Trim(), 80);
            item.UrlPath = txtUrlPath.Text.Trim();
            item.IPAddress = txtIP.Text.Trim();
            item.RedirectUrl = txtRedirectUrl.Text.Trim().TrimEnd('/');
            item.ProtocolId = DataHelper.GetInt32(cboProtocols.SelectedValue);
            item.Update();

            this.ReturnPage();
        }
    }
}