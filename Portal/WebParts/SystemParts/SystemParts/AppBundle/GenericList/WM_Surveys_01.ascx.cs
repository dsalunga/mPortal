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
	///		Summary description for CMS_Surveys.
	/// </summary>
	public partial class CMS_Surveys : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			if(!Page.IsPostBack)
			{
				this.LoadData();

				cmdDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete?');");
			}
		}

		private void LoadData()
		{
			DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, "SELECT * FROM GenericList");

			grdSurveys.DataSource = ds.Tables[0].DefaultView;
			grdSurveys.DataBind();
		}

		protected void cmdCreate_Click(object sender, System.EventArgs e)
		{
			var query = new WQuery(this.Request.QueryString);
			query.LoadAndRedirect("WM_CreateSurvey_02.ascx");
		}

        protected void grdSurveys_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			var query = new WQuery(this.Request.QueryString);
			string sID = e.Item.Cells[0].Text;

			switch(e.CommandName)
			{
				case "edit":
					query["ListId"] = sID;
                    query.LoadAndRedirect("WM_CreateSurvey_02.ascx");
					break;

				case "questions":
					query["ListId"] = sID;
                    query.LoadAndRedirect("WM_SurveyQuestions_05.ascx");
					break;

				case "pages":
					query["ListId"] = sID;
                    query.LoadAndRedirect("WM_SurveyPages_03.ascx");
					break;

				case "csv":
					query["ListId"] = sID;
                    query.LoadAndRedirect("WM_Results_08.ascx");
					break;
			}
		}

        protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			string sChecked = Request.Form["chkChecked"];

			if(!string.IsNullOrEmpty(sChecked))
			{
				SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM GenericList WHERE Id IN (" + sChecked + ");");
				this.LoadData();
			}
		}

        protected void cmdActivate_Click(object sender, System.EventArgs e)
		{
			string sChecked = Request.Form["chkChecked"];

			if(!string.IsNullOrEmpty(sChecked))
			{
				SqlHelper.ExecuteNonQuery(CommandType.Text, "UPDATE GenericList SET IsActive=1 WHERE Id IN (" + sChecked + ");");
				this.LoadData();
			}
		}

        protected void cmdDeactivate_Click(object sender, System.EventArgs e)
		{
			string sChecked = Request.Form["chkChecked"];

			if(!string.IsNullOrEmpty(sChecked))
			{
				SqlHelper.ExecuteNonQuery(CommandType.Text, "UPDATE GenericList SET IsActive=0 WHERE Id IN (" + sChecked + ");");
				this.LoadData();
			}
		}
	}
}
