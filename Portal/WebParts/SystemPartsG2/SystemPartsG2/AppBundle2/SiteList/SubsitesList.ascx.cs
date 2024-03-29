namespace WCMS.WebSystem.WebParts.SiteList
{
	using System;
	using System.Configuration;
	using System.Data;
	using System.Data.SqlClient;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using WCMS.Common.Utilities;

	/// <summary>
	///		Summary description for SubsitesList.
	/// </summary>
	public partial class SubsitesList : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//if(!Page.IsPostBack)
			{
				SqlDataReader r;

				if((ConfigurationManager.AppSettings["VHEnabled"] == "1"))
					r = SqlHelper.ExecuteReader("CMS.SELECT_Sites_LIST", 
						new SqlParameter("@VHEnabled", true));
				else
					r = SqlHelper.ExecuteReader("CMS.SELECT_Sites_LIST");

				using (r)
				{
					rList.DataSource = r;
					rList.DataBind();
				}
			}
		}

		protected string EvalLink(object isEnabled)
		{
			bool bEnabled = false;

			try
			{
				bEnabled = (bool)isEnabled;
			}
			catch{}

			return bEnabled ? "return true;" : "return false;";
		}
	}
}
