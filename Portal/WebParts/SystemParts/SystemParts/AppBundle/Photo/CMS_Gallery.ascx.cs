using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Photo
{
    /// <summary>
    ///		Summary description for CMS_Gallery.
    /// </summary>
    public partial class CMS_Gallery : System.Web.UI.UserControl
    {
        private string objectId;
        private int recordId;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            objectId = Request["ObjectId"];
            recordId = DataHelper.GetId(Request, "RecordId");

            if (!Page.IsPostBack)
            {
                using (SqlDataReader r = SqlHelper.ExecuteReader("GalleryObject_Get",
                        new SqlParameter("@ObjectId", objectId),
                        new SqlParameter("@RecordId", recordId)))
                {
                    if (r.Read())
                    {
                        try
                        {
                            cboControls.SelectedValue = r["InitialControl"].ToString();
                        }
                        catch { }

                        txtAlbumColumns.Text = r["AlbumColumns"].ToString();
                        txtThumbColumns.Text = r["ThumbColumns"].ToString();
                        txtThumbRows.Text = r["ThumbRows"].ToString();
                        txtAlbumPadding.Text = r["AlbumCellPadding"].ToString();
                    }
                }

                MultiView1.SetActiveView(viewBasic);
            }
        }

        protected void btnInsert_Click(object sender, System.EventArgs e)
        {
            int siteId = DataHelper.GetId(Request, "SiteId");

            string sID = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sID))
            {
                StringBuilder sb = new StringBuilder();
                var ids = DataHelper.ParseCommaSeparatedIdList(sID);

                foreach (var id in ids)
                    sb.AppendFormat("INSERT INTO GalleryLink (SiteID, ObjectId, RecordId, GalleryID) VALUES({0}, '{1}', {2}, {3});", siteId, objectId, recordId, id);

                SqlHelper.ExecuteNonQuery(CommandType.Text, sb.ToString());

                GridView1.DataBind();
                GridView2.DataBind();
            }
        }

        protected void btnRemove_Click(object sender, System.EventArgs e)
        {
            string ids = Request.Form["chkChecked2"];
            if (!string.IsNullOrEmpty(ids))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text,
                    "DELETE GalleryLink WHERE Id IN(" + ids + ");"
                );

                GridView1.DataBind();
                GridView2.DataBind();
            }
        }

        protected void cmdBasic_Click(object sender, EventArgs e)
        {
            divBasic.Attributes.Add("class", "tab_button");
            divAdvanced.Attributes.Add("class", "tab_button_blur");

            MultiView1.SetActiveView(viewBasic);
        }

        protected void cmdAdvanced_Click(object sender, EventArgs e)
        {
            divBasic.Attributes.Add("class", "tab_button_blur");
            divAdvanced.Attributes.Add("class", "tab_button");

            MultiView1.SetActiveView(viewAdvanced);
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            string initialControl = cboControls.SelectedValue;
            int albumColumns = 0;
            int thumbColumns = 0;
            int thumbRows = 0;
            int albumCellPadding = 0;

            if (!int.TryParse(txtAlbumColumns.Text.Trim(), out albumColumns))
            {
                lblStatus.Text = "Invalid Album Columns value.";
                return;
            }

            if (!int.TryParse(txtAlbumPadding.Text.Trim(), out albumCellPadding))
            {
                lblStatus.Text = "Invalid Album Cell Padding value.";
                return;
            }

            if (!int.TryParse(txtThumbColumns.Text.Trim(), out thumbColumns))
            {
                lblStatus.Text = "Invalid Thumbnail Columns value.";
                return;
            }

            if (!int.TryParse(txtThumbRows.Text.Trim(), out thumbRows))
            {
                lblStatus.Text = "Invalid Thumbnail Rows value.";
                return;
            }

            // PERSIST DATA
            SqlHelper.ExecuteNonQuery("GalleryObject_Set",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@InitialControl", initialControl),
                new SqlParameter("@AlbumColumns", albumColumns),
                new SqlParameter("@ThumbColumns", thumbColumns),
                new SqlParameter("@ThumbRows", thumbRows),
                new SqlParameter("@AlbumCellPadding", albumCellPadding)
            );

            lblStatus.Text = "UPDATE COMPLETE";
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView2.DataBind();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
    }
}