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
	///		Summary description for CONTENT_AdCategories_01.
	/// </summary>
	public partial class CONTENT_AdCategories_01 : System.Web.UI.UserControl
	{
		//private SqlData db;
		//private SqlCommand cmd;


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
			this.grd.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grd_ItemCommand);
			this.grd.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grd_PageIndexChanged);

		}
		#endregion
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			//db = new SqlData();

			if(!Page.IsPostBack)
			{
				cmdDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete?');");
				this.LoadData();
			}
		}

		private void LoadData(int iPageIndex)
		{
			//DataSet ds = new DataSet();

			// LOAD COMMON SECTION ITEMS
            DataSet ds = SqlHelper.ExecuteDataSet("ADS_SELECT_AdCategories");
			if(iPageIndex!=-1)
			{
				grd.CurrentPageIndex = iPageIndex;
			}
			grd.DataSource = ds.Tables[0].DefaultView;
			grd.DataBind();
		}

		private void LoadData()
		{
			this.LoadData(-1);
		}

		protected string SetStateImage(object obj)
		{
			bool b;

			try
			{
				b = (bool)obj;
			}
			catch
			{
				b = false;
			}

			if(b)
			{
				return "images/ico_check.gif";
			}
			else
			{
				return "images/ico_x.gif";
			}
		}

		private void grd_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string ID = e.Item.Cells[0].Text;
			QueryParser qs;

			switch(e.CommandName)
			{
				case "edit":
					qs = new QueryParser(this);
					qs.Set("Page", "2");
					qs.Set("__ac", ID);
					Response.Redirect(".?" + qs.ToString(), false);
					break;
			}
		}

		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{
			QueryParser qs = new QueryParser(this);
			qs.Set("Page", "2");
			Response.Redirect(".?" + qs.ToString());
		}

		protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			string sChecked = Request.Form["chkChecked"];
			if(!string.IsNullOrEmpty(sChecked))
			{
				SqlHelper.ExecuteNonQuery("DELETE FROM AdCategories WHERE AdCategoryID IN (" + sChecked + ");");
				this.LoadData(0);
			}
		}

		private void grd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.LoadData(e.NewPageIndex);
		}
	}
}
