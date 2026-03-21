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
	///		Summary description for CMS_Ad.
	/// </summary>
	public partial class CMS_Media : System.Web.UI.UserControl
	{
		private string sSID, sSSID, sIID, sLocation;
		private int iID;


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
			this.grdAds.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdAds_PageIndexChanged);

		}
		#endregion
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			sIID = Request.QueryString["_i"];
			sSSID = Request.QueryString["SiteSectionID"];
			sSID = Request.QueryString["SiteID"];

			if(string.IsNullOrEmpty(sSSID))
			{
				Response.Redirect(".");
			}

			switch(sSSID)
			{
				case "HOME":
					iID = int.Parse(sIID);
					sLocation = "H";
					break;
				case "CONTENT":
					iID = int.Parse(sIID);
					sLocation = "L";
					break;
				default:
					iID = int.Parse(sSSID);
					sLocation = "S";
					break;
			}

			if(!Page.IsPostBack)
			{
				cmdDelete.OnClientClick = "return confirm('Remove Selected Items?');";

				// OTHER DATA (COMBO, ETC)
				this.LoadEtcData();

				// GRID DATA
				LoadData();
			}
		}

		private void LoadEtcData()
		{
			// LOAD SITES
			using (SqlDataReader r = SqlHelper.ExecuteReader("CMS.SPC_SELECT_Sites"))
			{
				cboSites.DataSource = r;
				cboSites.DataTextField = "SiteName";
				cboSites.DataValueField = "SiteID";
				cboSites.DataBind();
			}
			cboSites.Items.Insert(0, new ListItem("[ ALL ]", "-1"));
			cboSites.SelectedValue = sSID;
		}

		private void LoadData(int iPageIndex)
		{
			string sSiteID = cboSites.SelectedValue;

			// INSERTED ITEMS
			DataSet dsInserted = SqlHelper.ExecuteDataSet("MG_SELECT_MediaGallery_SITE",
				new SqlParameter("@LocationItemID", iID),
				new SqlParameter("@LocationType", sLocation));
			grd.DataSource = dsInserted.Tables[0].DefaultView;
			grd.DataBind();

			// SHOW ALL UNINSERTED
			DataSet ds = SqlHelper.ExecuteDataSet("MG_SELECT_MediaGallery_SITE_ALL",
				new SqlParameter("@LocationItemID", iID),
				new SqlParameter("@LocationType", sLocation));

			if(sSiteID != "-1")
				ds.Tables[0].DefaultView.RowFilter = "SiteID = " + sSiteID;

			if(iPageIndex != -1)
				grdAds.CurrentPageIndex = iPageIndex;	

			grdAds.DataSource = ds.Tables[0].DefaultView;
			grdAds.DataBind();
		}

		private void LoadData()
		{
			this.LoadData(-1);
		}

		private void dtgArticles_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string strSiteArticleID = e.Item.Cells[0].Text;
		}

		private void dtgAllContents_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string strArticleID = e.Item.Cells[0].Text;
		}

		protected void btnInsert_Click(object sender, System.EventArgs e)
		{
			string sChecked = Request.Form["chkCheckedAll"];
			if(!string.IsNullOrEmpty(sChecked))
			{
				foreach(string sMediaID in sChecked.Split(','))
				{
					if(sMediaID != string.Empty)
					{
						SqlHelper.ExecuteNonQuery("MG_INSERT_SiteMediaGallery",
							new SqlParameter("@LocationItemID", iID),
							new SqlParameter("@LocationType", sLocation),
							new SqlParameter("@MediaID", int.Parse(sMediaID)),
							new SqlParameter("@SiteID", int.Parse(sSID)));
					}
				}
				this.LoadData(0);
			}
		}

		protected void cmdView_Click(object sender, System.EventArgs e)
		{
			this.LoadData(0);
		}

		private void grdAds_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.LoadData(e.NewPageIndex);
		}

		protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			string sChecked = Request.Form["chkChecked"];
			if(!string.IsNullOrEmpty(sChecked))
			{
				SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM SiteMediaGallery WHERE LocationItemID=" + iID +
					" AND LocationType='" + sLocation +
					"' AND MediaID IN (" + sChecked + ")");

				this.LoadData(0);
			}
		}

		protected void cboSites_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.LoadData(0);
		}	
	}
}
