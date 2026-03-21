using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using des.Utils;

namespace CMS
{
    public partial class SiteCategory : System.Web.UI.Page
    {
        private string sSCID;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            sSCID = Request.QueryString["SiteCategoryID"];

            if (!Page.IsPostBack)
            {
                cmdFile.Attributes.Add("onclick", "Upload('" + txtFilename.ClientID + "','/Uploads/Image/SECTIONS/SITECATEGORY','&FileOnly=true');");

                this.LoadData();
            }
        }

        private void LoadData()
        {
            if (!string.IsNullOrEmpty(sSCID))
            {
                // EDIT
                using (SqlDataReader r = SqlHelper.ExecuteReader("CMS.SELECT_SiteCategories",
                    new SqlParameter("@SiteCategoryID", int.Parse(sSCID))))
                {
                    if (r.Read())
                    {
                        txtName.Text = r["SiteCategoryName"].ToString();
                        txtFilename.Text = r["ImageUrl"].ToString();
                        txtBlurb.Text = r["Blurb"].ToString();

                        try
                        {
                            chkMenu.Checked = (bool)r["ShowInMenu"];
                        }
                        catch { }
                    }
                }
            }
            else
            {
                // GENERATE NEW RANK
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            QSParser qs = new QSParser(this.Request.QueryString);
            qs.Remove("SiteCategoryID");
            Response.Redirect("SiteCategories.aspx?" + qs, true);
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            string sName = txtName.Text.Trim();
            string sBlurb = txtBlurb.Text.Trim();
            string sImageUrl = txtFilename.Text.Trim();
            bool showInMenu = chkMenu.Checked;
            int iRank;

            try
            {
                iRank = int.Parse(txtRank.Text.Trim());
            }
            catch
            {
                // SHOULD GENERATE ERROR
                iRank = 1;
            }

            if (!string.IsNullOrEmpty(sSCID))
            {
                // EDIT
                SqlHelper.ExecuteNonQuery("CMS.UPDATE_SiteCategories",
                    new SqlParameter("@SiteCategoryID", int.Parse(sSCID)),
                    new SqlParameter("@ImageUrl", sImageUrl),
                    new SqlParameter("@Blurb", sBlurb),
                    new SqlParameter("@SiteCategoryName", sName),
                    new SqlParameter("@Rank", iRank),
                    new SqlParameter("@ShowInMenu", showInMenu)
                );
            }
            else
            {
                // ADD
                int iParentID = int.Parse(Request.QueryString["ParentID"]);

                SqlHelper.ExecuteNonQuery("CMS.UPDATE_SiteCategories",
                    new SqlParameter("@ImageUrl", sImageUrl),
                    new SqlParameter("@SiteCategoryName", sName),
                    new SqlParameter("@Blurb", sBlurb),
                    new SqlParameter("@Rank", iRank),
                    new SqlParameter("@ShowInMenu", showInMenu),
                    new SqlParameter("@ParentID", iParentID)
                );
            }

            QSParser qs = new QSParser(this.Request.QueryString);
            qs.Remove("SiteCategoryID");
            Response.Redirect("SiteCategories.aspx?" + qs, true);
        }
    }
}