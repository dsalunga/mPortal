namespace WCMS.WebSystem.WebParts.GenericForm
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using WCMS.Common.Utilities;
    using WCMS.Framework;

    /// <summary>
    ///		Summary description for CMS_Results.
    /// </summary>
    public partial class CMS_Results : System.Web.UI.UserControl
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cmdDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete the selected items?');");
            }
        }

        public DataSet Select(int surveyId)
        {
            return SqlHelper.ExecuteDataSet("GenericListRow_Get",
                new SqlParameter("@ListId", surveyId));
        }

        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];

            if (!string.IsNullOrEmpty(sChecked))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM GenericForm_Responses WHERE ResponseID IN (" + sChecked + ")");
                GridView1.DataBind();
            }
        }

        protected void cmdDone_Click(object sender, System.EventArgs e)
        {
            var query = new WQuery(this);
            query.Remove("ListId");
            query.UnloadAndRedirect();
        }

        //protected void grdSurveys_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        //{
        //    string sID = e.Item.Cells[2].Text;
        //    QueryParser qs = new QueryParser(this);

        //    switch(e.CommandName)
        //    {
        //        case "edit":
        //            qs["ResponseID"] = sID;

        //            qs.Load("WM_Responses_09.ascx");
        //            break;
        //    }
        //}

        protected void cmdDownload_Click(object sender, System.EventArgs e)
        {
            int surveyId = DataHelper.GetId(Request["ListId"]);
            string fileName = "Responses.xls";
            string filePath = MapPath("~/Admin/Data/" + fileName);
            DataSet ds = SqlHelper.ExecuteDataSet("GenericForm_Responses_GetXml",
                new SqlParameter("@ListId", surveyId)
                );

            ds.DataSetName = "Survey";
            ds.Tables[0].TableName = "Responses";
            ds.WriteXml(filePath);

            Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            Response.TransmitFile(filePath);
            Response.End();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sID = e.CommandArgument.ToString();
            var query = new WQuery(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query["ResponseID"] = sID;
                    query.LoadAndRedirect("WM_Responses_09.ascx");
                    break;
            }
        }
    }
}
