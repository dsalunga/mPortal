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
	///		Summary description for CONTENT_AdWriter.
	/// </summary>
	public partial class CONTENT_AdWriter : System.Web.UI.UserControl
	{
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
			// LOAD COMMON SECTION ITEMS
			DataSet ds = SqlHelper.ExecuteDataSet("ADS_SELECT_Ads");
			
			if(iPageIndex!=-1)
				grd.CurrentPageIndex = iPageIndex;

			grd.DataSource = ds.Tables[0].DefaultView;
			grd.DataBind();
		}

		private void LoadData()
		{
			this.LoadData(-1);
		}

		protected string SetStateImage(object obj)
		{
			bool b = false;

			try
			{
				b = (bool)obj;
			}
			catch{}

			return b ? "images/ico_check.gif" : "images/ico_x.gif";
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
					qs.Set("__ad", ID);
					Response.Redirect(".?" + qs.ToString());
					break;
				case "items":
					qs = new QueryParser(this);
					qs.Set("Page", "3");
					qs.Set("__ad", ID);
					Response.Redirect(".?" + qs.ToString());
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
				SqlHelper.ExecuteNonQuery(CommandType.Text,"DELETE FROM Ads WHERE AdID IN (" + sChecked + ");");
				this.LoadData(0);
			}
		}

		private void grd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.LoadData(e.NewPageIndex);
		}
	}
}
