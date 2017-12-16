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
	///		Summary description for CMS_SurveyPages_03.
	/// </summary>
	public partial class CMS_SurveyPages_03 : System.Web.UI.UserControl
	{
		private string listId;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			listId = Request.QueryString["ListId"];

			if(!Page.IsPostBack)
			{
				cmdDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete the selected items?');");
				this.LoadData(-1);
			}
		}

		private void LoadData(int iPageIndex)
		{
			DataSet ds = SqlHelper.ExecuteDataSet("GenericListPartition_Get",
				new SqlParameter("@ListId", int.Parse(listId)));

			if(iPageIndex != -1)
			{
				grd.CurrentPageIndex = iPageIndex;
			}

			grd.DataSource = ds.Tables[0].DefaultView;
			grd.DataBind();
		}

		protected void cmdCreate_Click(object sender, System.EventArgs e)
		{
			var query = new WQuery(this.Request.QueryString);
            query.LoadAndRedirect("WM_CreateSurveyPage_04.ascx");
		}

        protected void grd_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string sID = e.Item.Cells[0].Text;
			var query = new WQuery(this.Request.QueryString);

			switch(e.CommandName)
			{
				case "questions":
					query["PartitionId"] = sID;
                    query.LoadAndRedirect("WM_SurveyQuestions_05.ascx");
					break;

				case "edit":
					query["PartitionId"] = sID;
                    query.LoadAndRedirect("WM_CreateSurveyPage_04.ascx");
					break;
			}
		}

        protected void cmdReturn_Click(object sender, System.EventArgs e)
		{
			var query = new WQuery(this.Request.QueryString);
			query.Remove("ListId");
            query.LoadAndRedirect("WM_Surveys_01.ascx");
		}

        protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			string sChecked = Request.Form["chkChecked"];

			if(!string.IsNullOrEmpty(sChecked))
			{
				SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM GenericListPartition WHERE Id IN (" + sChecked + ")");
				this.LoadData(0);
			}
		}
	}
}
