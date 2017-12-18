using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.WebSites
{
    public partial class WebMasterPagesController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!IsSiteAuthor())
                {
                    cmdAddFull.Enabled = false;
                    cmdDelete.Enabled = false;
                }
            }
        }

        private bool IsSiteAuthor()
        {
            int id = DataHelper.GetId(Request, WebColumns.SiteId);
            return WSite.IsUserSiteAuthor(id);
        }

        protected void cmdAddFull_Click(object sender, EventArgs e)
        {
            if (IsSiteAuthor())
            {
                var query = new QueryParser(this);
                query.Redirect(CentralPages.WebMasterPage);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);
            var query = new WQuery(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.MasterPageId, id);
                    query.Redirect(CentralPages.WebMasterPageHome);
                    break;

                case "Custom_Delete":
                    if (IsSiteAuthor())
                    {
                        var master = WebMasterPage.Get(id);
                        if (master != null)
                        {
                            master.Delete();

                            GridView1.DataBind();
                        }
                    }
                    break;
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                if (IsSiteAuthor())
                {
                    var ids = DataHelper.ParseCommaSeparatedIdList(sChecked);
                    foreach (int id in ids)
                    {
                        var master = WebMasterPage.Get(id);
                        if (master != null)
                            master.Delete();
                    }

                    GridView1.DataBind();
                }
            }
        }

        public DataSet GetMasterPages(int siteId)
        {
            WebTemplate template = null;
            var site = WSite.Get(siteId);
            var query = new WQuery(true);

            return DataHelper.ToDataSet(
                from i in WebMasterPage.FilterPermitted(siteId)
                select new
                {
                    i.Id,
                    i.Name,
                    IsDefault = site.DefaultMasterPageId == i.Id ? 1 : 0,
                    TemplateName = (template = i.Template) != null ? template.Name : string.Empty,
                    NameUrl = query.Set(WebColumns.MasterPageId, i.Id).BuildQuery(CentralPages.WebMasterPageHome)
                });
        }
    }
}