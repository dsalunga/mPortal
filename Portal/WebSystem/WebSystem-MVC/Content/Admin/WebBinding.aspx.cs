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
using WCMS.Framework;

public partial class _CMS_Binding : System.Web.UI.Page
{
    private string bindingId;

    protected void Page_Load(object sender, EventArgs e)
    {
        bindingId = Request.QueryString["BindingID"];

        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(bindingId))
            {
                //CMSDALTableAdapters.BindingsAdapter adapter = new CMSDALTableAdapters.BindingsAdapter();
                //CMSDAL.BindingsDataTable table = adapter.GetData(Convert.ToInt32(sBindingID), null);

                //if (table.Rows.Count > 0)
                //{
                //    CMSDAL.BindingsRow row = table[0];

                //    txtIP.Text = row.IPAddress;
                //    txtHostHeader.Text = row.HostHeader;
                //    txtPort.Text = row.Port.ToString();
                //    //txtdescription.Text = r["Description"].ToString();
                //}
            }
        }
    }
    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        this.ReturnPage();
    }

    private void ReturnPage()
    {
        QueryParser qs = new QueryParser(this);
        qs.Remove("BindingID");

        Response.Redirect("Bindings.aspx?" + qs, true);
    }

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        string sIP = txtIP.Text.Trim();
        string sHostHeader = txtHostHeader.Text.Trim();
        int? iPort = 80;
        int? iBindingID = null;
        int iSiteID = Convert.ToInt32(Request.QueryString[WebColumns.SiteId]);

        if (!string.IsNullOrEmpty(bindingId))
        {
            iBindingID = int.Parse(bindingId);
        }

        try
        {
            iPort = Convert.ToInt32(txtPort.Text.Trim());
        }
        catch { }

        //CMSDALTableAdapters.BindingsAdapter adapter = new CMSDALTableAdapters.BindingsAdapter();
        //adapter.Update(iBindingID, iSiteID, sIP, iPort, sHostHeader);

        this.ReturnPage();
    }
}
