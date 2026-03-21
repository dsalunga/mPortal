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

using des.Common.Utilities;

public partial class _CMS_SiteLogs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MultiView1.SetActiveView(viewSites);
        }
    }

    protected void cmdSites_Click(object sender, EventArgs e)
    {
        if (chkDownload.Checked)
        {
            DownloadXML(1);
        }
        else
        {
            MultiView1.SetActiveView(viewSites);
            GridView1.DataBind();
        }
    }

    protected void cmdSiteSections_Click(object sender, EventArgs e)
    {
        if (chkDownload.Checked)
        {
            DownloadXML(2);
        }
        else
        {
            MultiView1.SetActiveView(viewSiteSections);
            GridView2.DataBind();
        }
    }
    protected void cmdLogs_Click(object sender, EventArgs e)
    {
        if (chkDownload.Checked)
        {
            DownloadXML(0);
        }
        else
        {
            MultiView1.SetActiveView(viewLogDetails);
            GridView3.DataBind();
        }
    }

    private void DownloadXML(int iType)
    {
        DateTime? dateFrom = null;
        DateTime? dateTo = null;

        string sFrom = txtFrom.Text.Trim();
        string sTo = txtTo.Text.Trim();

        if (!string.IsNullOrEmpty(sFrom))
        {
            dateFrom = DateTime.Parse(sFrom);
        }

        if (!string.IsNullOrEmpty(sTo))
        {
            dateTo = DateTime.Parse(sTo);
        }

        string sFile = "SiteLogs.xls";
        string sFilePath = MapPath("~/Admin/Temp/" + sFile);
        DataSet ds = SqlHelper.ExecuteDataSet("CMS.SELECT_SiteLogs_XML",
                new SqlParameter("@Type", iType),
                new SqlParameter("@FromDate", dateFrom),
                new SqlParameter("@ToDate", dateTo)
        );

        ds.DataSetName = "SiteLogs";
        ds.Tables[0].TableName = "SiteLogs";
        ds.WriteXml(sFilePath);

        Response.AppendHeader("content-disposition", "attachment; filename=" + sFile);
        Response.WriteFile(sFilePath);
        Response.End();
    }
}
