using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.SiteList
{
    public partial class _Sections_SITELIST_ListingPage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WContext ctx = new WContext(this);
            int sParentID = ctx.Page.SiteId;
            bool bSortByName = false;

            // MENU PROPERTIES
            int sPageType = ctx.ObjectId;
            int sSitePageItemID = ctx.RecordId;

            using (SqlDataReader r = SqlHelper.ExecuteReader("SiteList.SELECT_ListingPageProperties",
                    new SqlParameter("@PageType", sPageType),
                    new SqlParameter("@SitePageItemID", sSitePageItemID)))
            {
                if (r.Read())
                {
                    DataList1.RepeatColumns = (int)r["RepeatColumns"];
                    DataList1.CellPadding = (int)r["CellPadding"];
                    sParentID = DataHelper.GetId(r["ParentID"]);
                    divHeaderText.InnerHtml = r["HeaderText"].ToString();

                    try
                    {
                        bSortByName = (bool)r["SortByName"];
                    }
                    catch { }
                }
            }

            DataSet ds = SqlHelper.ExecuteDataSet("CMS.SELECT_Sites",
                    new SqlParameter("@ParentID", sParentID)
                );

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (bSortByName) ds.Tables[0].DefaultView.Sort = "SiteName";
                DataList1.DataSource = ds.Tables[0].DefaultView;
                DataList1.DataBind();
            }
        }
    }
}