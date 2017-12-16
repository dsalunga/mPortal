using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.GenericForm
{
    public partial class ListData : System.Web.UI.UserControl
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            int surveyId = GetSurveyId();
            if (surveyId > 0)
            {
                ObjectDataSource1.SelectParameters["listId"].DefaultValue = surveyId.ToString();
                GridView1.DataBind();
            }
        }

        private int GetSurveyId()
        {
            WContext ctx = new WContext(this);

            // MENU PROPERTIES
            int listId = ctx.GetId("ListId");

            if (listId <= 0)
            {
                listId = DataHelper.GetId(
                                    SqlHelper.ExecuteScalar("GenericListLink_Get",
                                        new SqlParameter("@RecordId", ctx.RecordId),
                                        new SqlParameter("@ObjectId", ctx.ObjectId))
                               );

                return listId;
            }

            return -1;
        }

        public DataSet Select(int listId)
        {
            return SqlHelper.ExecuteDataSet("GenericListRow_Get",
                new SqlParameter("@ListId", listId));
        }

        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];

            if (!string.IsNullOrEmpty(sChecked))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM GenericListRow WHERE Id IN (" + sChecked + ")");
                GridView1.DataBind();
            }
        }

        //protected void cmdDone_Click(object sender, System.EventArgs e)
        //{
        //    QueryParser qs = new QueryParser(this);
        //    qs.Remove("ListId");

        //    qs.Load();
        //}

        protected void cmdDownload_Click(object sender, System.EventArgs e)
        {
            int surveyId = GetSurveyId();
            if (surveyId > 0)
            {
                string fileName = "ListRows.xls";
                string filePath = MapPath("~/Admin/Data/" + fileName);
                DataSet ds = SqlHelper.ExecuteDataSet("GenericListRow_GetXml",
                    new SqlParameter("@ListId", surveyId)
                    );

                ds.DataSetName = "GenericList";
                ds.Tables[0].TableName = "ListRows";
                ds.WriteXml(filePath);

                Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
                Response.TransmitFile(filePath);
                Response.End();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sID = e.CommandArgument.ToString();
            var query = new WQuery(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query["RowId"] = sID;
                    query.LoadAndRedirect("WM_Responses_09.ascx");
                    break;
            }
        }
    }
}