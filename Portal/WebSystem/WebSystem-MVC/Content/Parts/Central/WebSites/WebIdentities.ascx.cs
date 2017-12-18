using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebIdentities : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboSites.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());

                var siteId = DataHelper.GetId(Request, WebColumns.SiteId);
                if (siteId > 0)
                {
                    WebHelper.SetCboValue(cboSites, siteId);
                    cboSites.Visible = false;
                    ObjectDataSource1.SelectParameters["siteId"].DefaultValue = siteId.ToString();
                }
                else
                {
                    cmdDone.Visible = false;
                }

                GridView1.DataBind();
            }
        }

        public DataSet Select(int siteId)
        {
            WSite site = null;

            return DataHelper.ToDataSet(
                from item in WebSiteIdentity.Provider.GetList(siteId)
                select new
                {
                    item.Id,
                    item.HostName,
                    item.Port,
                    item.IPAddress,
                    item.UrlPath,
                    item.RedirectUrl,
                    SiteName = (item.SiteId > 0 ? site = WSite.Get(item.SiteId) : null) != null ? site.Name : string.Empty,
                    Protocol = item.ProtocolId == 0 ? "http" : "https"
                }
            );
        }

        protected void cmdAddFull_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.Redirect(CentralPages.WebIdentity);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var query = new WQuery(this);
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set("SiteIdentityId", id);
                    query.Redirect(CentralPages.WebIdentity);
                    break;

                case "Custom_Delete":
                    WebSiteIdentity.Provider.Delete(id);
                    GridView1.DataBind();
                    break;
            }
        }

        protected void cboSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void cmdDone_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            int siteId = query.GetId(WebColumns.SiteId);

            query.Redirect(siteId > 0 ? CentralPages.WebSiteHome : CentralPages.WebSites);
        }
    }
}