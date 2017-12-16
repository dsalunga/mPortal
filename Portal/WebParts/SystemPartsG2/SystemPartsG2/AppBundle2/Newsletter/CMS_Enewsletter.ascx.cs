namespace WCMS.WebSystem.WebParts.Newsletter
{
    using System;
    using System.IO;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using WCMS.Common.Utilities;

    /// <summary>
    ///		Summary description for CMS_eNewsletter.
    /// </summary>
    public partial class CMS_eNewsletter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnDelete.OnClientClick = "return confirm('Delete Selected Items');";
            }
        }

        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            string strFormString = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(strFormString))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE Newsletter.eNewsletter WHERE(eNewsletterID) IN(" + strFormString + ");");
                GridView1.DataBind();
            }
        }

        protected void btnDownloadCSV_Click(object sender, System.EventArgs e)
        {
            string sFile = "eNewsletter_Subscribers.xls";
            string sFilePath = MapPath("~/_CMS/Temp/Newsletter/" + sFile);

            string sDir = Path.GetDirectoryName(sFilePath);
            if (!Directory.Exists(sDir))
            {
                Directory.CreateDirectory(sDir);
            }

            System.Data.DataSet ds = SqlHelper.ExecuteDataSet("Newsletter.SELECT_eNewsLetterAll");
            ds.DataSetName = "eNewsletter";
            ds.Tables[0].TableName = "Subscribers";
            ds.WriteXml(sFilePath);

            Response.AppendHeader("content-disposition", "attachment; filename=" + sFile);
            Response.WriteFile(sFilePath);
            Response.End();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}
