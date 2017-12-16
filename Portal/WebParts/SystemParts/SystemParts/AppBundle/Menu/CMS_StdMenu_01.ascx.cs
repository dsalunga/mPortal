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
using WCMS.Framework.ViewModel;

public partial class _Sections_STDMENU_CMS_StdMenu_01 : System.Web.UI.UserControl
{
    private int objectId = -1;
    private int recordId = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        WebPartContext ctx = new WebPartContext(this);
        objectId = ctx.ObjectId;
        recordId = ctx.RecordId;

        if (!Page.IsPostBack)
        {
            DropDownList1.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());
            int siteId = ctx.Page.SiteId;
            int pageId = -1;

            using (SqlDataReader r = SqlHelper.ExecuteReader("StdMenu_Get",
                new SqlParameter("@PageType", objectId),
                new SqlParameter("@SitePageItemID", recordId)))
            {
                if (r.Read())
                {
                    try
                    {
                        cboOrientation.SelectedValue = ((bool)r["Horizontal"]) ? "Horizontal" : "Vertical";
                    }
                    catch { }

                    siteId = DataHelper.GetId(r["SiteID"]);
                    pageId = DataHelper.GetId(r["SiteSectionID"]);

                    txtHome.Text = r["HomeText"].ToString();
                    txtWidth.Text = r["Width"].ToString();
                    txtHeight.Text = r["Height"].ToString();
                }
            }

            this.LoadMenuList(siteId, pageId);
        }
    }

    private void LoadMenuList(int siteId, int pageId)
    {
        DropDownList2.Items.Clear();
        DropDownList2.Items.AddRange(WebPageViewModel.GenerateListItem(siteId, -1, true).ToArray());

        DropDownList2.SelectedValue = pageId.ToString();
        DropDownList1.SelectedValue = siteId.ToString();
    }

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        string sWidth = txtWidth.Text.Trim();
        string sHeight = txtHeight.Text.Trim();
        bool isHorizontal = (cboOrientation.SelectedValue == "Horizontal");
        int siteId = int.Parse(DropDownList1.SelectedValue);
        int pageId = int.Parse(DropDownList2.SelectedValue);
        string sHome = txtHome.Text.Trim();

        // VALIDATION
        if (sWidth == string.Empty)
        {
            sWidth = null;
        }
        else
        {
            // Check value if valid
            try
            {
                Unit.Parse(sWidth);
            }
            catch { return; }
        }
        if (sHeight == string.Empty)
        {
            sHeight = null;
        }
        else
        {
            // Check value if valid
            try
            {
                Unit.Parse(sHeight);
            }
            catch { return; }
        }

        // PERSIST DATA
        SqlHelper.ExecuteNonQuery("StdMenu_Set",
            new SqlParameter("@PageType", objectId),
            new SqlParameter("@SitePageItemID", recordId),
            new SqlParameter("@Height", sHeight),
            new SqlParameter("@Width", sWidth),
            new SqlParameter("@Horizontal", isHorizontal),
            new SqlParameter("@SiteID", siteId),
            new SqlParameter("@SiteSectionID", pageId),
            new SqlParameter("@HomeText", sHome)
        );

        lblStatus.Text = "UPDATE COMPLETE";
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadMenuList(int.Parse(DropDownList1.SelectedValue), -1);
    }
}
