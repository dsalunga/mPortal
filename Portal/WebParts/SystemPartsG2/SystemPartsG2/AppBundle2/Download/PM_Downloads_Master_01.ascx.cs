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
    public partial class _Sections_Download_CCMS_Downloasd_01 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //cmdDelete.OnClientClick = "return confirm('Are you sure you want to delete the selected items?');";
            }
        }

        protected void cmdNew_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this.Request.QueryString);
            query["Page"] = "2";
            Response.Redirect(".?" + query, true);
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                //SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM Download.Downloads WHERE DownloadID IN (" + sChecked +")");
                //GridView1.DataBind();


            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sID = e.CommandArgument.ToString();
            QueryParser query = new QueryParser(this.Request.QueryString);
            query["DownloadID"] = sID;

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query["Page"] = "2";
                    Response.Redirect(".?" + query, true);
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

        /*
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal)
                {
                    e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#a9bec7';";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='#EFF3FB'";
                }
                else
                {
                    e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#a9bec7';";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white'";
                }
            }
        }
        */
    }
}