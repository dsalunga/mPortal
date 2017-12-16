namespace WCMS.WebSystem.WebParts.Media
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using WCMS.Common.Utilities;
    using WCMS.Framework;

    /// <summary>
    ///		Summary description for ProductList.
    /// </summary>
    public partial class MediaList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            WContext ctx = new WContext(this);

            int sLocation = ctx.ObjectId;
            int iID = ctx.RecordId;

            using (SqlDataReader r = SqlHelper.ExecuteReader("MG_SELECT_MediaGallery",
                       new SqlParameter("@LocationItemID", iID),
                       new SqlParameter("@LocationType", sLocation)))
            {
                rProducts.DataSource = r;
                rProducts.DataBind();
            }

            string sMID = ctx["MID"];
            if (sMID != null)
            {
                using (SqlDataReader r = SqlHelper.ExecuteReader("MG_SELECT_MediaGallery",
                           new SqlParameter("@MediaID", int.Parse(sMID))))
                {
                    if (r.Read())
                    {
                        lLoadedMedia.Text = r["Content"].ToString();
                        lLoadedMediaTitle.Text = r["Name"].ToString();
                        lBlurb.Text = r["Agency"].ToString();
                        pPlayer.Visible = true;
                    }
                }
            }
        }

        protected string CreateLink(string sMID)
        {
            WContext ctx = new WContext(this);
            //qs["SS"] = "PC";
            ctx["MID"] = sMID;
            //qs["P"] = "1";
            return ctx.BuildQuery();
        }
    }
}
