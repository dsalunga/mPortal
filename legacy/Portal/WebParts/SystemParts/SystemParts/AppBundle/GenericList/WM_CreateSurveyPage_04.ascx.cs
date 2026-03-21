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
	///		Summary description for CMS_CreateSurveyPage.
	/// </summary>
	public partial class CMS_CreateSurveyPage : System.Web.UI.UserControl
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
				if(sPID!=null && sPID!=string.Empty)
				{
					using (SqlDataReader r = SqlHelper.ExecuteReader(CommandType.Text, "SELECT * FROM GenericListPartition WHERE Id=@PartitionId",
							   new SqlParameter("@PartitionId", int.Parse(sPID))))
					{
						if(r.Read())
						{
							txtTitle.Text =r["Title"].ToString();
							txtRank.Text = r["Rank"].ToString();
						}
					}
				}
			}
		}


		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			var query = new WQuery(this.Request.QueryString);
			query.Remove("PartitionId");
            query.LoadAndRedirect("WM_SurveyPages_03.ascx");
		}

		protected void cmdQUpdate_Click(object sender, System.EventArgs e)
		{
			string sTitle = txtTitle.Text.Trim();
			int iRank = 0;

			try
			{
				iRank = int.Parse(txtRank.Text.Trim());
			}
			catch{}

			if(sPID!=null && sPID!=string.Empty)
			{
				// UPDATE

                SqlHelper.ExecuteNonQuery("GenericListPartition_Set", 
					new SqlParameter("@PartitionId", int.Parse(sPID)),
					new SqlParameter("@Rank", iRank),
					new SqlParameter("@Title", sTitle)
					);
			}
			else
			{
				// INSERT

                SqlHelper.ExecuteNonQuery("GenericListPartition_Set", 
					new SqlParameter("@ListId", int.Parse(sSID)),
					new SqlParameter("@Rank", iRank),
					new SqlParameter("@Title", sTitle)
					);
			}

			var query = new WQuery(this.Request.QueryString);
			query.Remove("PartitionId");
            query.LoadAndRedirect("WM_SurveyPages_03.ascx");
		}
	}
}
