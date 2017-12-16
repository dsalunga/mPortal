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

namespace WCMS.WebSystem.WebParts.Misc
{
    public partial class _Sections_DateDisplay_CMS_DateDisplay : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string sPageType = Request.QueryString["PageType"];
                int iSitePageItemID = int.Parse(Request.QueryString["SitePageItemID"]);

                using (SqlDataReader r = SqlHelper.ExecuteReader("DateDisplay.SELECT_Properties",
                    new SqlParameter("@PageType", sPageType),
                    new SqlParameter("@SitePageItemID", iSitePageItemID)
                ))
                {
                    if (r.Read())
                    {
                        txtFormatString.Text = r["FormatString"].ToString();
                    }
                }
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            string sPageType = Request.QueryString["PageType"];
            int iSitePageItemID = int.Parse(Request.QueryString["SitePageItemID"]);
            string sFormatString = txtFormatString.Text.Trim();

            SqlHelper.ExecuteNonQuery("DateDisplay.UPDATE_Properties",
                new SqlParameter("@PageType", sPageType),
                new SqlParameter("@SitePageItemID", iSitePageItemID),
                new SqlParameter("@FormatString", sFormatString),
                new SqlParameter("@UserID", Membership.GetUser().ProviderUserKey)
            );

            lblStatus.Text = "Update successful.";
        }
    }
}