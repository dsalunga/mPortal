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

	/// <summary>
	///		Summary description for SurveyFinish.
	/// </summary>
	public partial class SurveyFinish : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				QueryParser qs = new QueryParser(this);

                using (SqlDataReader r = SqlHelper.ExecuteReader("GenericList_Get",
                           new SqlParameter("@ListId", qs.GetId("ListId"))))
				{
					if(r.Read())
					{
						this.lTitle.Text = r["Title"].ToString();
						lblMessage.Text = r["EndingText"].ToString();
					}
				}
			}
		}
	}
}
