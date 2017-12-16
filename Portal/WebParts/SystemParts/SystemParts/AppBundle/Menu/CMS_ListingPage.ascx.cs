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

public partial class _Sections_STDMENU_CMS_ListingPage : System.Web.UI.UserControl
{
    private int sPageType;
    private int sSitePageItemID;

    protected void Page_Load(object sender, EventArgs e)
    {
        WebPartContext ctx = new WebPartContext(this);

        sPageType = ctx.ObjectId;
        sSitePageItemID = ctx.RecordId;

        if (!Page.IsPostBack)
        {
            for (int i = 1; i < 11; i++)
            {
                cboRepeatColumns.Items.Add(new ListItem(i.ToString()));
            }

            for (int i = 0; i < 16; i++)
            {
                cboPadding.Items.Add(new ListItem(i.ToString()));
            }

            using (SqlDataReader r = SqlHelper.ExecuteReader("StdMenuProperties_Get",
                new SqlParameter("@PageType", sPageType),
                new SqlParameter("@SitePageItemID", sSitePageItemID)))
            {
                if (r.Read())
                {
                    txtHeaderText.Text = r["HeaderText"].ToString();

                    try
                    {
                        cboRepeatColumns.SelectedValue = r["RepeatColumns"].ToString();
                    }
                    catch { }

                    try
                    {
                        cboType.SelectedValue = r["ListingType"].ToString();
                    }
                    catch { }

                    try
                    {
                        cboPadding.SelectedValue = r["CellPadding"].ToString();
                    }
                    catch { }
                }
            }
        }
    }

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        string sHeaderText = txtHeaderText.Text.Trim();
        string sListingType = cboType.SelectedValue;
        int iRepeatColumns = int.Parse(cboRepeatColumns.SelectedValue);
        int iCellPadding = int.Parse(cboPadding.SelectedValue);


        // PERSIST DATA
        SqlHelper.ExecuteNonQuery("StdMenuProperties_Set",
            new SqlParameter("@PageType", sPageType),
            new SqlParameter("@SitePageItemID", sSitePageItemID),
            new SqlParameter("@HeaderText", sHeaderText),
            new SqlParameter("@ListingType", sListingType),
            new SqlParameter("@RepeatColumns", iRepeatColumns),
            new SqlParameter("@CellPadding", iCellPadding)
        );

        lblStatus.Text = "UPDATE COMPLETE";
    }
}