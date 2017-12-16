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

namespace WCMS.WebSystem.WebParts.Misc
{
    public partial class _Sections_Date_DateDisplay : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WContext context = new WContext(this);

            int sPageType = context.ObjectId;
            int sSitePageItemID = context.RecordId;
            string sFormatString = "{0:MMMM d, yyyy}";

            using (SqlDataReader r = SqlHelper.ExecuteReader("DateDisplay.SELECT_Properties",
                    new SqlParameter("@PageType", sPageType),
                    new SqlParameter("@SitePageItemID", sSitePageItemID)
                ))
            {
                if (r.Read())
                {
                    sFormatString = r["FormatString"].ToString();
                }
            }

            Literal l = new Literal();
            l.Text = string.Format(sFormatString, DateTime.Now); // DateTime.Now.ToString(sFormatString);
            this.Controls.Add(l);
        }
    }
}