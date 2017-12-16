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
	///		Summary description for Responses.
	/// </summary>
	public partial class Responses : System.Web.UI.UserControl
	{
		private string sRID;
		private string sSID;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			sRID = Request["ResponseID"];
			sSID = Request["ListId"];

			if(!Page.IsPostBack)
			{
				this.LoadData(-1);
			}
		}

		private void LoadData(int iPageIndex)
		{
			DataSet ds = SqlHelper.ExecuteDataSet("GenericListRow_Get",
				new SqlParameter("@ResponseID", int.Parse(sRID)),
				new SqlParameter("@ListId", int.Parse(sSID))
				);

			if(iPageIndex != -1)
			{
				grdSurveys.CurrentPageIndex = iPageIndex;
			}

			grdSurveys.DataSource = ds.Tables[0].DefaultView;
			grdSurveys.DataBind();
		}

		protected void cmdDone_Click(object sender, System.EventArgs e)
		{
			var query = new WQuery(this.Request.QueryString);
			query.Remove("ResponseID");
            query.LoadAndRedirect("WM_Results_08.ascx");
		}
	}
}
