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
	///		Summary description for CONTENT_AdItem_04.
	/// </summary>
	public partial class CONTENT_AdItem_04 : System.Web.UI.UserControl
	{
		//private SqlData db;
		//private SqlDataReader r;
		//private SqlCommand cmd;
		private string AID, AIID;


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
			//db = new SqlData();
			AID = Request.QueryString["__ad"];
			AIID = Request.QueryString["__adi"];
			
			if(!Page.IsPostBack)
			{
				this.LoadData();
			}
		}

		private void LoadData()
		{
			if(AIID!=null && AIID!=string.Empty)
			{
				string s_Filename, s_FilenameOnly;
				// EDIT
                using (SqlDataReader r = SqlHelper.ExecuteReader("ADS_SELECT_AdItem",
                    new SqlParameter("@AdItemID", int.Parse(AIID))))
                {
                    if (r.Read())
                    {
                        txtAlternateText.Text = r["AlternateText"].ToString();

                        // GET FILE INFO
                        s_Filename = r["ImageUrl"].ToString();
                        s_FilenameOnly = Path.GetFileNameWithoutExtension(s_Filename);

                        txtFilename.Text = s_Filename;
                        cmdFile.Attributes.Add("onclick", "Upload('txtFilename','../Assets/Uploads/Image/SECTIONS/ADS/xml/images','&_f=true&_filename=" + s_FilenameOnly + "');");

                        txtNavigateUrl.Text = r["NavigateUrl"].ToString();
                        txtKeyword.Text = r["Keyword"].ToString();
                        txtImpressions.Text = r["Impressions"].ToString();
                    }
                }
			}
			else
			{
				// ADD: GENERATE NEW IMAGE FILENAME (SHOULD BE UNIQUE)
				string s_FilenameOnly;

                using (SqlDataReader r = SqlHelper.ExecuteReader("ADS_SELECT_AdItem_IMAGEURL_LAST"))
                {
                    if (r.HasRows)
                    {
                        string s_Filename, s_TMP;
                        int i_ID;

                        r.Read();
                        s_Filename = r["ImageUrl"].ToString();
                        s_TMP = Path.GetFileNameWithoutExtension(s_Filename);
                        i_ID = int.Parse(s_TMP.Substring(6)) + 1;

                        s_FilenameOnly = "IMAGE_" + i_ID.ToString();
                    }
                    else
                    {
                        s_FilenameOnly = "IMAGE_1";
                    }
                }

				cmdFile.Attributes.Add("onclick","Upload('txtFilename','../Assets/Uploads/Image/SECTIONS/ADS/xml/images','&_f=true&_filename=" + s_FilenameOnly + "');");
			}
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			QueryParser qs = new QueryParser(this);
			qs.Remove("__adi");
			qs["Page"] = "3";
			Response.Redirect(".?" + qs.ToString());
		}

		protected void cmdUpdate_Click(object sender, System.EventArgs e)
		{
			QueryParser qs;
			string s_AlternateText = txtAlternateText.Text.Trim();
			string s_ImageUrl = txtFilename.Text.Trim();
			string s_NavigateUrl = txtNavigateUrl.Text.Trim();
			string s_Keyword = txtKeyword.Text.Trim();
			int i_Impressions;

			try
			{
				i_Impressions = int.Parse(txtImpressions.Text.Trim());
			}
			catch
			{
				// SHOULD GENERATE ERROR
				i_Impressions = 0;
			}

			if(AIID!=null && AIID!=string.Empty)
			{
				// EDIT

                SqlHelper.ExecuteNonQuery("ADS_UPDATE_AdItem",
				new SqlParameter("@AdItemID", int.Parse(AIID)),
				new SqlParameter("@ImageUrl", s_ImageUrl),
				new SqlParameter("@AlternateText", s_AlternateText),
				new SqlParameter("@Keyword", s_Keyword),
				new SqlParameter("@Impressions", i_Impressions),
				new SqlParameter("@NavigateUrl", s_NavigateUrl));
			}
			else
			{
				// ADD
                SqlHelper.ExecuteNonQuery("ADS_INSERT_AdItem",
				new SqlParameter("@AdID", int.Parse(AID)),
				new SqlParameter("@ImageUrl", s_ImageUrl),
				new SqlParameter("@AlternateText", s_AlternateText),
				new SqlParameter("@Keyword", s_Keyword),
				new SqlParameter("@Impressions", i_Impressions),
				new SqlParameter("@NavigateUrl", s_NavigateUrl));
			}

			qs = new QueryParser(this);
			qs.Remove("__adi");
			qs["Page"] = "3";
			Response.Redirect(".?" + qs.ToString());
		}
	}
}
