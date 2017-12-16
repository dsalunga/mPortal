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
	///		Summary description for CONTENT_Ad.
	/// </summary>
	public partial class CONTENT_Media_02 : System.Web.UI.UserControl
	{
		private string sMID;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			sMID = Request.QueryString["__m"];

			if(!Page.IsPostBack)
			{

                cmdFile.Attributes.Add("onclick", "Upload('txtFilename','../Assets/Uploads/Image/SECTIONS/MG','&_f=true');");

				this.LoadData();
			}
		}

		private void LoadData()
		{
			string sSID = Request.QueryString["SiteID"];

			// RANK
			int iRank = (int)SqlHelper.ExecuteScalar("MG_GET_Rank_MediaGallery");
			if(sMID==null || sMID==string.Empty)
				iRank++;

			for(int i=1; i<=iRank; i++)
				cboRank.Items.Add(i.ToString());

			// OWNER SITES
			using (SqlDataReader r = SqlHelper.ExecuteReader("CMS.SELECT_Sites"))
			{
				cboSites.DataSource = r;
				cboSites.DataTextField = "SiteName";
				cboSites.DataValueField = "SiteID";
				cboSites.DataBind();
			}
			cboSites.Items.Insert(0, new ListItem("[ NONE ]", "-1"));

			/*
			// CATEGORIES
			using (SqlDataReader r = SqlHelper.ExecuteReader("MG_C_SELECT_ProductCategories"))
			{
				cboCategories.DataSource = r;
				cboCategories.DataTextField = "Name";
				cboCategories.DataValueField = "ProductCategoryID";
				cboCategories.DataBind();
			}
			cboCategories.Items.Insert(0, new ListItem("[ NONE ]", "-1"));
			*/

			// BEGIN LOAD PRODUCT
			if(sMID!=null && sMID!=string.Empty)
			{
				// EDIT
				using (SqlDataReader r = SqlHelper.ExecuteReader("MG_C_SELECT_MediaGallery",
						   new SqlParameter("@MediaID", int.Parse(sMID))))
				{
					if(r.Read())
					{
						txtName.Text = r["Name"].ToString();
						txtVersion.Text = r["MediaVersion"].ToString();
						//txtLength.Text = r["MediaLength"].ToString();
						txtAgency.Text = r["Agency"].ToString();
						txtContent.Value = r["Content"].ToString();
						txtFilename.Text = r["Thumbnail"].ToString();
						
						try
						{
							cboRank.SelectedValue = r["Rank"].ToString();
						}
						catch{}
						try
						{
							chkActive.Checked = (bool)r["IsActive"];
						}
						catch{}

						try{ cboSites.SelectedValue = r["SiteID"].ToString(); }
						catch
						{
							if(sSID != null)
								cboSites.SelectedValue = sSID;
						}
					}
				}
			}
			else
			{
				// ADD

				if(sSID != null)
					cboSites.SelectedValue = sSID;

				// GENERATE NEW RANK
				cboRank.SelectedValue = iRank.ToString();
			}

			if(sSID != null)
				cboSites.Enabled = false;
		}

		protected void cmdUpdate_Click(object sender, System.EventArgs e)
		{
			string sName = txtName.Text.Trim();
			string sVersion = txtVersion.Text.Trim();
			string sLength = string.Empty; //txtLength.Text.Trim();
			string sAgency = txtAgency.Text.Trim();
			string sContent = txtContent.Value.Trim();
			//string sCID = cboCategories.SelectedValue;
			string sThumbnail = txtFilename.Text;
			//int iCategoryID = int.Parse(cboCategories.SelectedValue);
			int iRank = int.Parse(cboRank.SelectedValue);
			int iSiteID = int.Parse(cboSites.SelectedValue);
			bool IsActive = chkActive.Checked;

			if(sMID!=null && sMID!=string.Empty)
			{
				// UPDATE
				SqlHelper.ExecuteNonQuery("MG_UPDATE_MediaGallery",
					new SqlParameter("@MediaID", int.Parse(sMID)),
					new SqlParameter("@Name", sName),
					new SqlParameter("@MediaVersion", sVersion),
					new SqlParameter("@MediaLength", sLength),
					new SqlParameter("@Agency", sAgency),
					new SqlParameter("@Content", sContent),
					new SqlParameter("@Rank", iRank),
					new SqlParameter("@IsActive", IsActive),
					//new SqlParameter("@ProductCategoryID", iCategoryID),
					new SqlParameter("@SiteID", iSiteID),
					new SqlParameter("@Thumbnail", sThumbnail));
			}
			else
			{
				//string sUID = Session["UserID"].ToString();

				// INSERT
				SqlHelper.ExecuteNonQuery("MG_INSERT_MediaGallery",
					//new SqlParameter("@UserID", int.Parse(sUID)),
					new SqlParameter("@Name", sName),
					new SqlParameter("@MediaVersion", sVersion),
					new SqlParameter("@MediaLength", sLength),
					new SqlParameter("@Agency", sAgency),
					new SqlParameter("@Content", sContent),
					new SqlParameter("@Rank", iRank),
					new SqlParameter("@IsActive", IsActive),
					//new SqlParameter("@ProductCategoryID", iCategoryID),
					new SqlParameter("@SiteID", iSiteID),
					new SqlParameter("@Thumbnail", sThumbnail));
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
			qs.Remove("__m");
			Response.Redirect(".?" + qs, true);
		}

	}
}
