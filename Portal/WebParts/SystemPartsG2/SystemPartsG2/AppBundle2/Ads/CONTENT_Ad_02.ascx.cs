namespace WCMS.WebSystem.WebParts.Ads
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Drawing;
	using System.Web;
    using System.Web.Security;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using WCMS.Common.Utilities;

	/// <summary>
	///		Summary description for CONTENT_Ad.
	/// </summary>
	public partial class CONTENT_Ad : System.Web.UI.UserControl
	{
		private string AID;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			AID = Request.QueryString["__ad"];

			if(!Page.IsPostBack)
			{
				this.LoadData();
			}
		}

		private void LoadData()
		{
            using (SqlDataReader r = SqlHelper.ExecuteReader("ADS_SELECT_AdCategories"))
            {
                cboCategories.DataSource = r;
                cboCategories.DataTextField = "Name";
                cboCategories.DataValueField = "AdCategoryID";
                cboCategories.DataBind();
            }

			if(AID!=null && AID!=string.Empty)
			{
				// EDIT
                using (SqlDataReader r = SqlHelper.ExecuteReader("ADS_SELECT_Ad",
                    new SqlParameter("@AdID", int.Parse(AID))))
                {
                    if (r.Read())
                    {
                        txtName.Text = r["Name"].ToString();
                        try
                        {
                            cboCategories.SelectedValue = r["AdCategoryID"].ToString();
                        }
                        catch { }
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
			string s_ACID = cboCategories.SelectedValue;

			if(AID!=null && AID!=string.Empty)
			{
				// UPDATE
                SqlHelper.ExecuteNonQuery("ADS_UPDATE_Ad",
				new SqlParameter("@AdID", int.Parse(AID)),
				new SqlParameter("@Name", sName),
				new SqlParameter("@AdCategoryID", int.Parse(s_ACID)));
			}
			else
			{
				// INSERT
                SqlHelper.ExecuteNonQuery("ADS_INSERT_Ad",
				new SqlParameter("@Name", sName),
				new SqlParameter("@AdCategoryID", int.Parse(s_ACID)),
                new SqlParameter("@UserID", Membership.GetUser().ProviderUserKey));
			}

			this.ReturnParent();
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.ReturnParent();
		}

		private void ReturnParent()
		{
			QueryParser query = new QueryParser(this);
			query.Remove("Page");
			query.Remove("__ad");
			Response.Redirect(".?" + query.ToString(), false);
		}

	}
}
