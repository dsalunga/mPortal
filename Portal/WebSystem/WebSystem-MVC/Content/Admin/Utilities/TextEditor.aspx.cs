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

public partial class _CMS_TextEditor : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // Put user code to initialize the page here

        if (!Page.IsPostBack)
        {
            string sSrc = HttpUtility.UrlDecode(Request.QueryString["Src"]);

            if(!string.IsNullOrEmpty(sSrc))
            {
                txtContent.Text = FileHelper.ReadFile(MapPath(sSrc));
            }
        }
    }

    protected void cmdCancel_Click(object sender, System.EventArgs e)
    {
        this.ReturnParentPage();
    }

    private void ReturnParentPage()
    {
        string sReturn = HttpUtility.UrlDecode(Request.QueryString["Return"]);
        if (!string.IsNullOrEmpty(sReturn))
        {
            Response.Redirect(sReturn, true);
        }
    }

    protected void cmdUpdate_Click(object sender, System.EventArgs e)
    {
        string sSrc = HttpUtility.UrlDecode(Request.QueryString["Src"]);

        if (!string.IsNullOrEmpty(sSrc))
        {
            FileHelper.WriteFile(txtContent.Text, Server.MapPath(sSrc));
        }

        this.ReturnParentPage();
    }
}
