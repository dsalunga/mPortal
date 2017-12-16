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
using System.Linq;

using WCMS.Common.Utilities;
using WCMS.Framework;

public partial class _Sections_STDMENU_LinearMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WebPartContext context = new WebPartContext(this);
        int siteId = context.Page.SiteId;

        int pageId = context.PageId;
        string sListingType = "Child";
        int parentId = 0;
        int iRepeatColumns = 3;


        // MENU PROPERTIES
        int objectId = context.ObjectId;
        int recordId = context.RecordId;


        using (SqlDataReader r = SqlHelper.ExecuteReader("StdMenuProperties_Get",
                new SqlParameter("@PageType", objectId),
                new SqlParameter("@SitePageItemID", recordId)))
        {
            if (r.Read())
            {
                sListingType = r["ListingType"].ToString();
                DataList1.CellPadding = (int)r["CellPadding"];
                iRepeatColumns = DataHelper.GetInt32(r["RepeatColumns"]);
            }
        }

        if (pageId > 0)
        {
            switch (sListingType)
            {
                case "Full":
                    parentId = -1;
                    break;

                case "Relative":
                    var page = WebPage.Get(pageId);
                    if (page != null)
                        parentId = page.ParentId;
                    break;

                case "Child":
                    parentId = pageId;
                    break;
            }
        }

        var pages = WebPage.GetList(siteId, parentId);
        DataList1.DataSource = from page in pages
                               where page.IsActive
                               select new {page.Name, Url = page.GenerateRelativeUrl()};
        DataList1.DataBind();
    }
}
