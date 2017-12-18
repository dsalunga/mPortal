using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using des.Utils;


namespace des.Web.CMS
{
    /// <summary>
    /// Summary description for TemplateOrphans.
    /// </summary>
    public partial class TemplateOrphans : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here

            if (!Page.IsPostBack)
            {
                cmdDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete?');");
            }
        }

        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM CMS.SitePageItems WHERE SitePageItemID IN (" + sChecked + ");");
                GridView1.DataBind();
            }
        }

        protected void cmdMoveTo_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            string sPlaceHolderID = cboPlaceholders.SelectedValue;

            if (!string.IsNullOrEmpty(sChecked))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, "UPDATE CMS.SitePageItems SET PlaceHolderID=@PlaceHolderID WHERE SitePageItemID IN (" + sChecked + ")",
                    new SqlParameter("@PlaceHolderID", int.Parse(sPlaceHolderID))
                    );

                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}
