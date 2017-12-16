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

public partial class MENU_CMS_CascadeMenu : System.Web.UI.UserControl
{
    private void Page_Load(object sender, System.EventArgs e)
    {
        WebPartContext ctx = new WebPartContext(this);

        int menuId = -1;

        // MENU PROPERTIES
        using (SqlDataReader r = SqlHelper.ExecuteReader("MenuObject_Get",
                new SqlParameter("@ObjectId", ctx.ObjectId),
                new SqlParameter("@RecordId", ctx.RecordId)))
        {
            if (r.Read())
            {
                string sWidth = r["Width"].ToString();
                string sHeight = r["Height"].ToString();
                try
                {
                    Menu1.Orientation = ((bool)r["Horizontal"]) ? Orientation.Horizontal : Orientation.Vertical;
                }
                catch { }

                if (!string.IsNullOrEmpty(sWidth))
                    Menu1.Width = Unit.Parse(sWidth);
                if (!string.IsNullOrEmpty(sHeight))
                    Menu1.Height = Unit.Parse(sHeight);

                menuId = DataHelper.GetId(r["MenuId"]);
            }
        }

        DataSet ds = SqlHelper.ExecuteDataSet("MenuItem_Get",
            new SqlParameter("@MenuID", menuId));

        DataRow[] rows = ds.Tables[0].Select("ParentID=-1");
        foreach (DataRow row in rows)
        {
            string sText = row["Text"].ToString();
            bool isActive = row["IsActive"].ToString() == "1";

            // TREE VIEW
            if (isActive)
            {
                MenuItem tn1 = new MenuItem("<div>" + sText + "</div>");
                tn1.NavigateUrl = row["NavigateURL"].ToString();
                tn1.Target = row["Target"].ToString();
                tn1.Selectable = isActive;

                if (!(!LoadRecursiveMenu((int)row["Id"], ds.Tables[0], tn1) && !isActive))
                    Menu1.Items.Add(tn1);
            }
        }
    }

    private bool LoadRecursiveMenu(int iParentID, DataTable table, MenuItem tnRoot)
    {
        DataRow[] rows = table.Select("ParentID=" + iParentID);
        foreach (DataRow row in rows)
        {
            string sText = row["Text"].ToString();
            bool isActive = row["IsActive"].ToString() == "1";

            // TREE VIEW
            if (isActive)
            {
                MenuItem tn1 = new MenuItem("<div>" + sText + "</div>");
                tn1.NavigateUrl = row["NavigateURL"].ToString();
                tn1.Target = row["Target"].ToString();
                tn1.Selectable = isActive;

                if (!(!LoadRecursiveMenu((int)row["Id"], table, tn1) && !isActive))
                    tnRoot.ChildItems.Add(tn1);
            }
        }

        return (rows.Length > 0);
    }
}
