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

using WCMS.Common.Utilities;

public partial class _CMS_Bindings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CMSDALTableAdapters.SitesAdapter adapter = new CMSDALTableAdapters.SitesAdapter();
            //object obj = adapter.GetName(Convert.ToInt32(Request.QueryString[WebColumns.SiteId]));

            //if (obj != null)
            //{
            //    tdHeader.InnerHtml += " - " + obj;
            //}
        }
    }

    protected void cmdAddFull_Click(object sender, EventArgs e)
    {
        QueryParser qs = new QueryParser(this);
        Response.Redirect("Binding.aspx?" + qs, true);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sID = e.CommandArgument.ToString();
        QueryParser qs = new QueryParser(this);
        qs["BindingID"] = sID;

        switch (e.CommandName)
        {
            case "Custom_Edit":
                Response.Redirect("Binding.aspx?" + qs, true);
                break;
        }
    }

    protected void cmdDone_Click(object sender, EventArgs e)
    {
        QueryParser qs = new QueryParser(this);
        Response.Redirect("Sites.aspx?" + qs, true);
    }
}
