namespace WCMS.WebSystem.WebParts.Ads
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Drawing;
	using System.IO;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using WCMS.Common.Utilities;

	/// <summary>
	///		Summary description for CONTENT_AdItems_03.
	/// </summary>
	public partial class CONTENT_AdItems_03 : System.Web.UI.UserControl
	{
		//private SqlData db;
		//private SqlCommand cmd;
		private string AID;


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
			AID = Request.QueryString["__ad"];
			//db = new SqlData();
			
			if(!Page.IsPostBack)
			{
				cmdDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete?');");
				this.LoadData();
			}
		}

		private void LoadData(int iPageIndex)
		{
			DataSet ds = SqlHelper.ExecuteDataSet("ADS_SELECT_AdItems",
                new SqlParameter("@AdID", int.Parse(AID)));

			grd.DataSource = ds.Tables[0].DefaultView;
			grd.DataBind();
		}

		private void LoadData()
		{
			this.LoadData(-1);
		}

		protected void cmdDone_Click(object sender, System.EventArgs e)
		{
			QueryParser qs = new QueryParser(this);
			qs.Remove("Page");
			qs.Remove("__ad");
			Response.Redirect(".?" + qs.ToString());
		}

		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{
			QueryParser qs = new QueryParser(this);
			qs.Set("Page", "4");
			qs.Set("__ad", AID);
			Response.Redirect(".?" + qs.ToString());
		}

		protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			string sChecked = Request.Form["chkChecked"];
			if(!string.IsNullOrEmpty(sChecked))
			{
				SqlHelper.ExecuteNonQuery(CommandType.Text,"DELETE FROM AdItems WHERE AdItemID IN (" + sChecked + ");");
				this.LoadData(0);
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
					qs.Set("Page", "4");
					qs.Set("__ad", AID);
					qs.Set("__adi", ID);
					Response.Redirect(".?" + qs.ToString());
					break;
			}
		}

		protected void cmdRender_Click(object sender, System.EventArgs e)
		{
			DataSet ds = SqlHelper.ExecuteDataSet("ADS_SELECT_AdItems_XML",
				new SqlParameter("@AdID", int.Parse(AID)));

			ds.DataSetName = "Advertisements";
			ds.Tables[0].TableName = "Ad";

			string s_PATH = this.MapPath("../../Assets/Uploads/Image/SECTIONS/ADS/xml/AD_" + AID + ".xml");
			ds.WriteXml(s_PATH, XmlWriteMode.IgnoreSchema);

			lblStatus.Text = "[ XML rendering complete. ]";
		}

		private void RenderXML()
		{
		}

		private void grd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.LoadData(e.NewPageIndex);
		}
	}
}
