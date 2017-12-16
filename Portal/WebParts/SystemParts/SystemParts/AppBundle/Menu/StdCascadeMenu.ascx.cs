using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;

using WCMS.Common.Utilities;
using WCMS.Framework;

public partial class _Sections_STDMENU_CascadeMenu : System.Web.UI.UserControl
{
    private void Page_Load(object sender, System.EventArgs e)
    {
        WebPartContext ctx = new WebPartContext(this);

        int siteId = ctx.Page.SiteId;
        int pageId = -1;
        string sHome = "";

        // MENU PROPERTIES
        int objectId = ctx.ObjectId;
        int recordId = ctx.RecordId;

        using (SqlDataReader r = SqlHelper.ExecuteReader("StdMenu_Get",
                new SqlParameter("@PageType", objectId),
                new SqlParameter("@SitePageItemID", recordId)))
        {
            if (r.Read())
            {
                string sWidth = r["Width"].ToString();
                string sHeight = r["Height"].ToString();
                int _sSiteID = DataHelper.GetId(r["SiteID"]);
                int _pageId = DataHelper.GetId(r["SiteSectionID"]);
                sHome = r["HomeText"].ToString();

                try
                {
                    Menu1.Orientation = Convert.ToBoolean(r["Horizontal"]) ? Orientation.Horizontal : Orientation.Vertical;
                }
                catch { }

                if (_sSiteID > 0)
                    siteId = _sSiteID;

                if (_pageId > 0)
                    pageId = _pageId;

                if (!string.IsNullOrEmpty(sWidth))
                    Menu1.Width = Unit.Parse(sWidth);

                if (!string.IsNullOrEmpty(sHeight))
                    Menu1.Height = Unit.Parse(sHeight);
            }
        }

        if (!string.IsNullOrEmpty(sHome))
        {
            MenuItem tnRoot = new MenuItem("<div>" + sHome + "</div>", "0");
            tnRoot.NavigateUrl = ctx.Page.Site.GenerateRelativeUrl();
            Menu1.Items.Add(tnRoot);
        }

        var pages = WebPage.GetList(siteId);

        var rows = pages.FindAll(item => item.ParentId == pageId);
        for (int i = 0; i < rows.Count; i++) //foreach (DataRow row in rows)
        {
            var row = rows.ElementAt(i);
            bool isActive = row.IsActive;

            // TREE VIEW
            MenuItem tn1 = new MenuItem();
            if (isActive) tn1.Text = "<div>" + row.Name + "</div>";
            else tn1.Text = "<div style=\"cursor: default;height:27px;vertical-align: middle\">" + row.Name + "</div>";
            tn1.NavigateUrl = row.GenerateRelativeUrl();
            tn1.Selectable = isActive;

            if (!(!LoadRecursiveMenu(row.Id, pages, tn1) && !isActive))
            {
                Menu1.Items.Add(tn1);
            }
        }
    }

    private bool LoadRecursiveMenu(int iParentID, List<WebPage> table, MenuItem tnRoot)
    {
        var rows = table.FindAll(item => item.ParentId == iParentID);
        foreach (var row in rows)
        {
            bool isActive = row.IsActive;

            // TREE VIEW
            MenuItem tn1 = new MenuItem();
            if (isActive) tn1.Text = "<div>" + row.Name + "</div>";
            else tn1.Text = "<div style=\"cursor: default;\">" + row.Name + "</div>";
            tn1.NavigateUrl = row.GenerateRelativeUrl();
            tn1.Selectable = isActive;

            if (!(!LoadRecursiveMenu(row.Id, table, tn1) && !isActive))
                tnRoot.ChildItems.Add(tn1);
        }

        return (rows.Count > 0);
    }
}
