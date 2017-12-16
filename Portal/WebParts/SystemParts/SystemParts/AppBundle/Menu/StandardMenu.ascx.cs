namespace WCMS.WebSystem.WebParts.Menu
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Linq;

    using WCMS.Common.Utilities;
    using WCMS.Framework;

    /// <summary>
    ///		Summary description for StandardMenudes.Web.
    /// </summary>
    public partial class StandardMenu : System.Web.UI.UserControl
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            var context = new WContext(this);
            int siteId = context.Page.SiteId;
            int parentId = 0;

            using (var r = SqlHelper.ExecuteReader("StdMenu_Get",
                    new SqlParameter("@ObjectId", context.ObjectId),
                    new SqlParameter("@RecordId", context.RecordId)))
            {
                if (r.Read())
                {
                    siteId = DataHelper.GetId(r, "SiteID");
                    parentId = DataHelper.GetId(r, "SiteSectionID");
                }
            }

            // LOAD ALL SITE SECTIONS WHERE ISSHOWMENU=TRUE
            var pages = WPage.GetList(siteId, parentId);

            rMenu.DataSource = from page in pages
                               where page.IsActive
                               select new { page.Name, Url = page.BuildRelativeUrl() };
            rMenu.DataBind();
        }
    }
}