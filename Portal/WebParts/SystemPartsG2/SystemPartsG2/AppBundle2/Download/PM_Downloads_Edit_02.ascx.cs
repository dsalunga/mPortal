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

namespace WCMS.WebSystem.WebParts.Download
{
    public partial class _Sections_Download_WM_Download_02 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cmdUpload.OnClientClick = "Upload('" + txtFilename.ClientID + "','/Assets/Uploads/Image/SECTIONS/Download','&FileOnly=true'); return false;";

                string sDownloadID = Request.QueryString["DownloadID"];
                if (!string.IsNullOrEmpty(sDownloadID))
                {
                    using (SqlDataReader r = SqlHelper.ExecuteReader("Download.SELECT_Downloads",
                        new SqlParameter("@DownloadID", Convert.ToInt32(sDownloadID))
                    ))
                    {
                        if (r.Read())
                        {
                            txtName.Text = r["Name"].ToString();
                            txtdescription.Value = r["Description"].ToString();
                            txtRank.Text = r["Rank"].ToString();
                            txtFilename.Text = r["Filename"].ToString();

                            try
                            {
                                txtDate.Text = ((DateTime)r["FileDate"]).ToString("MMMM d, yyyy");
                            }
                            catch { }
                        }
                    }
                }
                else
                {
                    //txtDate.Text = DateTime.Now.ToString("MMMM d, yyyy");
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.NavigateBack();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            string sName = txtName.Text.Trim();
            string sdescription = txtdescription.Value.Trim();
            string sFilename = txtFilename.Text.Trim();
            string sRank = txtRank.Text.Trim();
            string sFileDate = txtDate.Text.Trim();
            string sDownloadID = Request.QueryString["DownloadID"];

            int? iDownloadID = null;
            int? iRank = null;
            DateTime? fileDate = null;

            // Identity
            if (!string.IsNullOrEmpty(sDownloadID))
            {
                iDownloadID = Convert.ToInt32(sDownloadID);
            }

            // Rank
            if (sRank != string.Empty)
            {
                try
                {
                    iRank = Convert.ToInt32(sRank);
                }
                catch
                {
                    lblMessage.Text = "Rank must be a numeric value.";
                    return;
                }
            }

            // File Date
            if (sFileDate != string.Empty)
            {
                try
                {
                    fileDate = Convert.ToDateTime(sFileDate);
                }
                catch
                {
                    lblMessage.Text = "Invalid date format.";
                    return;
                }
            }

            SqlHelper.ExecuteNonQuery("Download.Update_Downloads",
                new SqlParameter("@DownloadID", iDownloadID),
                new SqlParameter("@Name", sName),
                new SqlParameter("@description", sdescription),
                new SqlParameter("@FileDate", fileDate),
                new SqlParameter("@Filename", sFilename),
                new SqlParameter("@Rank", iRank),
                new SqlParameter("@UserID", Membership.GetUser().ProviderUserKey)
            );

            this.NavigateBack();
        }

        private void NavigateBack()
        {
            QueryParser query = new QueryParser(this.Request.QueryString);
            query.Remove("DownloadID");
            //qs["Page"] = "1";
            query.Remove("Page");
            Response.Redirect(".?" + query, true);
        }
    }
}