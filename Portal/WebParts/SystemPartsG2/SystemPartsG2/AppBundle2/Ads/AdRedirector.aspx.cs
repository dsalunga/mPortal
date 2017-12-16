using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Ads
{
	/// <summary>
	/// Summary description for Banner.
	/// </summary>
	public partial class Banner : System.Web.UI.Page
	{
		#region Web Form designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			string s_AD = Request.QueryString["ad"];
			string sURL = Request.QueryString["url"];

			if((s_AD==null || s_AD==string.Empty) || (sURL==null || sURL==string.Empty))
			{
				Response.Redirect("about:blank", false);
			}
			else
			{
				SqlHelper.ExecuteNonQuery("ADS_UPDATE_AdItem_Hits",
					new SqlParameter("@AdItemID", int.Parse(s_AD)));
				
				Response.Redirect(sURL, false);
			}
		}
	}
}
