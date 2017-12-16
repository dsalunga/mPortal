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

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.BasicList
{
    public partial class _Sections_BasicList_CMS_01_BasicList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cmdDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete the selected items?');");

                QueryParser query = new QueryParser(this);
                Type gl = typeof(GridLines);
                foreach (string s in Enum.GetNames(gl))
                {
                    cboGridLines.Items.Add(new ListItem(s, ((int)Enum.Parse(gl, s)).ToString()));
                }

                int sPageType = WebObjects.WebPageElement;
                int iSitePageItemID = query.GetId("PageElementId");

                using (SqlDataReader r = SqlHelper.ExecuteReader("BasicList_Get",
                    new SqlParameter("@PageType", sPageType),
                    new SqlParameter("@SitePageItemID", iSitePageItemID)
                ))
                {
                    if (r.Read())
                    {
                        txtCellPadding.Text = r["CellPadding"].ToString();
                        txtRepeatColumns.Text = r["RepeatColumns"].ToString();
                        txtPageSize.Text = r["PageSize"].ToString();
                        txtAlternateColor.Text = r["AlternatingColor"].ToString();
                        txtTextColor.Text = r["TextColor"].ToString();

                        try
                        {
                            cboGridLines.SelectedValue = r["GridLines"].ToString();
                        }
                        catch { }

                        try
                        {
                            cboItemTemplate.SelectedValue = r["ItemTemplate"].ToString();
                        }
                        catch { }
                    }
                }
            }
        }

        protected void cmdBasic_Click(object sender, EventArgs e)
        {
            divBasic.Attributes.Add("class", "tab_button");
            divAdvanced.Attributes.Add("class", "tab_button_blur");

            MultiView1.SetActiveView(viewBasic);
        }

        protected void cmdAdvanced_Click(object sender, EventArgs e)
        {
            divBasic.Attributes.Add("class", "tab_button_blur");
            divAdvanced.Attributes.Add("class", "tab_button");

            MultiView1.SetActiveView(viewAdvanced);
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            multiViewList.SetActiveView(viewDetails);
            hiddenID.Value = string.Empty;
            this.ResetFields();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int sPageType = WebObjects.WebPageElement;
            int iSitePageItemID = query.GetId("PageElementId");
            string sField1 = txtField1.Text.Trim();
            string sField2 = txtField2.Text.Trim();
            string sField3 = txtField3.Text.Trim();
            int iRank = 0;

            if (!int.TryParse(txtRank.Text.Trim(), out iRank))
            {
                lblMessage.Text = "Invalid Rank value.";
                return;
            }

            int? iListItemID = null;
            if (hiddenID.Value != string.Empty) iListItemID = int.Parse(hiddenID.Value);

            SqlHelper.ExecuteNonQuery("BasicListItem_Set",
                new SqlParameter("@ListItemID", iListItemID),
                new SqlParameter("@PageType", sPageType),
                new SqlParameter("@SitePageItemID", iSitePageItemID),
                new SqlParameter("@Field1", sField1),
                new SqlParameter("@Field2", sField2),
                new SqlParameter("@Field3", sField3),
                new SqlParameter("@Rank", iRank)
            );

            GridView1.DataBind();
            multiViewList.SetActiveView(viewGrid);
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            multiViewList.SetActiveView(viewGrid);
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM BasicListItem WHERE ListItemID IN ({0})", sChecked));
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sID = e.CommandArgument.ToString();

            switch (e.CommandName)
            {
                case "edit_item":
                    using (SqlDataReader r = SqlHelper.ExecuteReader("BasicListItem_Get",
                        new SqlParameter("@ListItemID", int.Parse(sID))
                    ))
                    {
                        if (r.Read())
                        {
                            txtField1.Text = r["Field1"].ToString();
                            txtField2.Text = r["Field2"].ToString();
                            txtField3.Text = r["Field3"].ToString();
                            txtRank.Text = r["Rank"].ToString();

                            multiViewList.SetActiveView(viewDetails);
                            hiddenID.Value = sID;
                        }
                    }
                    break;
            }
        }

        private void ResetFields()
        {
            txtField1.Text = string.Empty;
            txtField2.Text = string.Empty;
            txtField3.Text = string.Empty;
        }

        protected void cmdUpdateProperties_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int sPageType = WebObjects.WebPageElement;
            int iSitePageItemID = query.GetId("PageElementId");

            string sItemTemplate = cboItemTemplate.SelectedValue;
            string sAlternatingColor = txtAlternateColor.Text.Trim();
            string sTextColor = txtTextColor.Text.Trim();
            int iGridLines = int.Parse(cboGridLines.SelectedValue);
            int iCellPadding = 0;
            int iRepeatColumns = 0;
            int iPageSize = 0;

            if (!int.TryParse(txtCellPadding.Text.Trim(), out iCellPadding))
            {
                lblStatus.Text = "Invalid Cell Padding value.";
                return;
            }

            if (!int.TryParse(txtRepeatColumns.Text.Trim(), out iRepeatColumns))
            {
                lblStatus.Text = "Invalid Repeat Columns value.";
                return;
            }

            if (!int.TryParse(txtPageSize.Text.Trim(), out iPageSize))
            {
                lblStatus.Text = "Invalid Page Size value.";
                return;
            }

            SqlHelper.ExecuteNonQuery("BasicList_Set",
                new SqlParameter("@PageType", sPageType),
                new SqlParameter("@SitePageItemID", iSitePageItemID),
                new SqlParameter("@CellPadding", iCellPadding),
                new SqlParameter("@RepeatColumns", iRepeatColumns),
                new SqlParameter("@PageSize", iPageSize),
                new SqlParameter("@GridLines", iGridLines),
                new SqlParameter("@ItemTemplate", sItemTemplate),
                new SqlParameter("@AlternatingColor", sAlternatingColor),
                new SqlParameter("@TextColor", sTextColor),
                new SqlParameter("@UserID", Membership.GetUser().ProviderUserKey)
            );

            lblStatus.Text = "Update successful.";
        }
    }
}