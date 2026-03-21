using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Test
{
    public partial class TemplateFormEditor : System.Web.UI.UserControl
    {
        private string sMenuID;

        protected void Page_Load(object sender, EventArgs e)
        {
            sMenuID = Request["MenuId"];

            if (!Page.IsPostBack)
            {
                ddlSites.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());

                if (!string.IsNullOrEmpty(sMenuID))
                {
                    using (SqlDataReader r = SqlHelper.ExecuteReader("Menu_Get",
                        new SqlParameter("@MenuID", int.Parse(sMenuID))))
                    {
                        if (r.Read())
                        {
                            txtCaption.Text = r["Name"].ToString();

                            try
                            {
                                chkIsActive.Checked = (bool)r["IsActive"];
                            }
                            catch { }

                            ddlSites.DataBind();
                            try
                            {
                                ddlSites.SelectedValue = r[WebColumns.SiteId].ToString();
                            }
                            catch { }
                        }
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ReturnPage();
        }
        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            string sName = txtCaption.Text.Trim();
            bool isActive = chkIsActive.Checked;
            int iSiteID = int.Parse(ddlSites.SelectedValue);

            if (!string.IsNullOrEmpty(sMenuID))
            {
                /* UPDATE */

                SqlHelper.ExecuteNonQuery("Menu_Set",
                    new SqlParameter("@MenuID", int.Parse(sMenuID)),
                    new SqlParameter("@Name", sName),
                    new SqlParameter("@SiteID", iSiteID),
                    new SqlParameter("@IsActive", isActive)
                );
            }
            else
            {
                /* INSERT */
                //int iUserID = int.Parse(Session["UserID"].ToString());

                SqlHelper.ExecuteNonQuery("Menu_Set",
                    new SqlParameter("@Name", sName),
                    new SqlParameter("@SiteID", iSiteID),
                    new SqlParameter("@IsActive", isActive),
                    new SqlParameter("@UserID", WSession.Current.UserId)
                );
            }

            this.ReturnPage();
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.Remove("MenuId");
            query.LoadAndRedirect("CCMS_Menu_08");
        }
    }
}