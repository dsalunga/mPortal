namespace WCMS.WebSystem.WebParts.Media
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
	///		Summary description for FullContent.
	/// </summary>
	public partial class FullContent : System.Web.UI.UserControl
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			if(!Page.IsPostBack)
			{
				QueryParser qs = new QueryParser(this.ID);

				string sMID = qs["MID"];

				object obj = SqlHelper.ExecuteScalar("MG_SELECT_MediaGallery", 
					new SqlParameter("@MediaID", sMID));

				if(obj != null)
					lContent.Text = obj.ToString();
			}

		}

		protected string CreateLink()
		{
			QueryParser qs = new QueryParser(this.ID);
			qs["SS"] = "MG";
			qs["P"] = "1";
			return ".?" + qs;
		}
	}
}
