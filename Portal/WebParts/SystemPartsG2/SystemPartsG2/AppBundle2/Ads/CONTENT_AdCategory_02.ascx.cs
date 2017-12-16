namespace WCMS.WebSystem.WebParts.Ads
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
	///		Summary description for CONTENT_AdCategory_02.
	/// </summary>
	public partial class CONTENT_AdCategory_02 : System.Web.UI.UserControl
	{
		//private SqlData db;
		//private SqlCommand cmd;
		//private SqlDataReader r;
		private string ACID;


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
		///		Required method for designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			ACID = Request.QueryString["__ac"];
			//db = new SqlData();

			if(!Page.IsPostBack)
			{
				this.LoadData();
			}
		}

		private void LoadData()
		{
			if(ACID!=null && ACID!=string.Empty)
			{
				// EDIT
                using (SqlDataReader r = SqlHelper.ExecuteReader("ADS_SELECT_AdCategory",
                    new SqlParameter("@AdCategoryID", int.Parse(ACID))))
                {
                    if (r.Read())
                    {
                        txtName.Text = r["Name"].ToString();
                    }
                }
			}
			else
			{
				// ADD
			}
		}

		protected void cmdUpdate_Click(object sender, System.EventArgs e)
		{
			string sName = txtName.Text.Trim();

			if(ACID!=null && ACID!=string.Empty)
			{
				// UPDATE
                SqlHelper.ExecuteNonQuery("ADS_UPDATE_AdCategory",
				new SqlParameter("@AdCategoryID", int.Parse(ACID)),
				new SqlParameter("@Name", sName));
			}
			else
			{
				// INSERT
                SqlHelper.ExecuteNonQuery("ADS_INSERT_AdCategory",
				new SqlParameter("@Name", sName));
			}

			this.ReturnParent();
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.ReturnParent();
		}

		private void ReturnParent()
		{
			QueryParser qs = new QueryParser(this);
			qs.Remove("Page");
			qs.Remove("__ac");
			Response.Redirect(".?" + qs.ToString());
		}
	}
}
