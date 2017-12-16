using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Download
{
    public partial class _Sections_Download_SM_Downloads_03 : System.Web.UI.UserControl
    {
        private string sPageType;
        private string sSitePageItemID;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            sPageType = Request.QueryString["PageType"];
            sSitePageItemID = Request.QueryString["SitePageItemID"];

            if (!Page.IsPostBack)
            {
                using (SqlDataReader r = SqlHelper.ExecuteReader("Download.SELECT_DownloadProperties",
                        new SqlParameter("@PageType", sPageType),
                        new SqlParameter("@SitePageItemID", int.Parse(sSitePageItemID))
                        ))
                {
                    if (r.Read())
                    {
                        try
                        {
                            cboControls.SelectedValue = r["InitialControl"].ToString();
                        }
                        catch { }

                        try
                        {
                            chkForceDownload.Checked = Convert.ToBoolean(r["ForceDownload"]);
                        }
                        catch { }

                        txtColumns.Text = r["Columns"].ToString();
                        txtRows.Text = r["Rows"].ToString();
                        txtMaxRecords.Text = r["MaxRecords"].ToString();
                    }
                }

                btnRemove.OnClientClick = "return confirm('Are you sure you want to remove selected items?');";
                MultiView1.SetActiveView(viewBasic);
            }
        }

        protected void btnInsert_Click(object sender, System.EventArgs e)
        {
            QueryParser qs = new QueryParser(this.Request.QueryString);
            string sSiteID = qs["SiteID"];
            string sSitePageItemID = qs["SitePageItemID"];
            string sPageType = qs["PageType"];

            string sID = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sID))
            {
                string strQuery = string.Empty;
                foreach (string s in sID.Split(','))
                {
                    strQuery += string.Format("INSERT INTO Download.DownloadLocations (SiteID, PageType, SitePageItemID, DownloadID) VALUES({0}, '{1}', {2}, {3});", sSiteID, sPageType, sSitePageItemID, s);
                }
                SqlHelper.ExecuteNonQuery(CommandType.Text, strQuery);

                GridView1.DataBind();
                GridView2.DataBind();
            }
        }

        protected void btnRemove_Click(object sender, System.EventArgs e)
        {
            string sID = Request.Form["chkChecked2"];
            if (!string.IsNullOrEmpty(sID))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text,
                    "DELETE Download.DownloadLocations WHERE(DownloadLocationID) IN(" + sID + ");"
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
            string sInitialControl = cboControls.SelectedValue;
            int iColumns = 0;
            int iRows = 0;
            bool forceDownload = chkForceDownload.Checked;
            int maxRecords = -1;

            try
            {
                maxRecords = Convert.ToInt32(txtMaxRecords.Text.Trim());
            }
            catch { }

            if (!int.TryParse(txtColumns.Text.Trim(), out iColumns))
            {
                lblStatus.Text = "Invalid Column value.";
                return;
            }

            if (!int.TryParse(txtRows.Text.Trim(), out iRows))
            {
                lblStatus.Text = "Invalid Row value.";
                return;
            }

            // PERSIST DATA
            SqlHelper.ExecuteNonQuery("Download.UPDATE_DownloadProperties",
                new SqlParameter("@PageType", sPageType),
                new SqlParameter("@SitePageItemID", int.Parse(sSitePageItemID)),
                new SqlParameter("@InitialControl", sInitialControl),
                new SqlParameter("@Columns", iColumns),
                new SqlParameter("@Rows", iRows),
                new SqlParameter("@MaxRecords", maxRecords),
                new SqlParameter("@ForceDownload", forceDownload)
            );

            lblStatus.Text = "UPDATE COMPLETE";
        }

        protected void cmdNew_Click(object sender, EventArgs e)
        {
            QueryParser qs = new QueryParser(this.Request.QueryString);
            qs["Page"] = "2";
            Response.Redirect(".?" + qs, true);
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sID = e.CommandArgument.ToString();
            QueryParser qs = new QueryParser(this.Request.QueryString);
            qs["DownloadID"] = sID;

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    qs["Page"] = "2";
                    Response.Redirect(".?" + qs, true);
                    break;
                case "Custom_Delete":
                    object obj = SqlHelper.ExecuteScalar("Download.DELETE_Downloads",
                        new SqlParameter("@DownloadID", Convert.ToInt32(sID))
                    );

                    if (obj != null)
                    {
                        // Try to Delete the file
                        string sFullFile = Server.MapPath("/Assets/Uploads/Image/SECTIONS/Download/" + obj);
                        if (File.Exists(sFullFile))
                        {
                            try
                            {
                                File.Delete(sFullFile);
                            }
                            catch { }
                        }
                    }

                    GridView1.DataBind();

                    break;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sID = e.CommandArgument.ToString();
            QueryParser qs = new QueryParser(this.Request.QueryString);
            qs["DownloadID"] = sID;

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    qs["Page"] = "2";
                    Response.Redirect(".?" + qs, true);
                    break;
                case "Custom_Delete":
                    object obj = SqlHelper.ExecuteScalar("Download.DELETE_Downloads",
                        new SqlParameter("@DownloadID", Convert.ToInt32(sID))
                    );

                    if (obj != null)
                    {
                        // Try to Delete the file
                        string sFullFile = Server.MapPath("/Assets/Uploads/Image/SECTIONS/Download/" + obj);
                        if (File.Exists(sFullFile))
                        {
                            try
                            {
                                File.Delete(sFullFile);
                            }
                            catch { }
                        }
                    }

                    GridView1.DataBind();

                    break;
            }
        }
    }
}