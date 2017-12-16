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
	///		Summary description for CMS_SurveyQuestions.
	/// </summary>
	public partial class CMS_SurveyQuestions : System.Web.UI.UserControl
	{
		private string sSID;
		private string sPID;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			sSID = Request.QueryString["ListId"];
			sPID = Request.QueryString["PartitionId"];

			if(!Page.IsPostBack)
			{
				this.LoadData(-1);

				cmdDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete?');");
			}
		}

		private void LoadData(int iPageIndex)
		{
			DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, "SELECT * FROM GenericListColumn WHERE ListId=@ListId AND PartitionId=@PartitionId ORDER BY Rank",
				new SqlParameter("@ListId", int.Parse(sSID)),
				new SqlParameter("@PartitionId", int.Parse(sPID))
				);

			if(iPageIndex != -1)
			{
				this.grdQuestions.CurrentPageIndex = iPageIndex;
			}

			grdQuestions.DataSource = ds.Tables[0].DefaultView;
			grdQuestions.DataBind();
		}

		protected void grdQuestions_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string sID = e.Item.Cells[0].Text;
			var query = new WQuery(this.Request.QueryString);

			switch(e.CommandName)
			{
				case "edit":
					query["ColumnId"] = sID;
                    query.LoadAndRedirect("WM_CreateQuestion_06.ascx");
					break;

				case "items":
					query["ColumnId"] = sID;
                    query.LoadAndRedirect("WM_CreateQuestionItems_07.ascx");
					break;

				case "view":
					// PREVIEW
					break;
			}
		}

        protected void cmdNewQuestion_Click(object sender, System.EventArgs e)
		{
			var query = new WQuery(this.Request.QueryString);
            query.LoadAndRedirect("WM_CreateQuestion_06.ascx");
		}

        protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			string sChecked = Request.Form["chkChecked"];

			if(!string.IsNullOrEmpty(sChecked))
			{
				SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM GenericListColumn WHERE Id IN (" + sChecked + ");");

				this.LoadData(0);
			}
		}

        protected void cmdReturn_Click(object sender, System.EventArgs e)
		{
			var query = new WQuery(this.Request.QueryString);
			query.Remove("PartitionId");
            query.LoadAndRedirect("WM_SurveyPages_03.ascx");
		}
	}
}
