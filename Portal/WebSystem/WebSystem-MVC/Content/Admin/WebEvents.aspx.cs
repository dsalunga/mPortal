using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using des.Common.Utilities;

namespace CMS
{
    public partial class SystemEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cmdDelete.OnClientClick = "return confirm('Are you sure you want to delete the selected items?');";
            }
        }
        protected void cmdAddFull_Click(object sender, EventArgs e)
        {
            QSParser qs = new QSParser(this);
            Response.Redirect("SystemEvent.aspx?" + qs, true);
        }
        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM CMS.SystemEvents WHERE SystemEventID IN (" + sChecked + ")");
                GridView1.DataBind();
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sID = e.CommandArgument.ToString();
            QSParser qs = new QSParser(this);
            qs["SystemEventID"] = sID;

            switch (e.CommandName)
            {
                case "edit_item":
                    Response.Redirect("SystemEvent.aspx?" + qs, true);
                    break;
            }
        }
    }
}