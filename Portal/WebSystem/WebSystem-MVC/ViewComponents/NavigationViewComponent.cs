using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Renders page/site navigation as a hierarchical menu.
    /// Replaces CascadeMenu.ascx (SystemParts/Menu).
    /// Usage: @await Component.InvokeAsync("Navigation", new { objectId, recordId })
    /// </summary>
    public class NavigationViewComponent : WViewComponent
    {
        public NavigationViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            var model = new NavigationViewModel();

            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            // Load menu properties and items via stored procedures
            using (var reader = SqlHelper.ExecuteReader("MenuObject_Get",
                new Microsoft.Data.SqlClient.SqlParameter("@ObjectId", WcmsContext.ObjectId),
                new Microsoft.Data.SqlClient.SqlParameter("@RecordId", WcmsContext.RecordId)))
            {
                if (reader.Read())
                {
                    model.IsHorizontal = DataHelper.GetBool(reader["Horizontal"]);
                    model.Width = reader["Width"]?.ToString();
                    model.Height = reader["Height"]?.ToString();
                    model.MenuId = DataHelper.GetId(reader["MenuId"]);
                }
            }

            if (model.MenuId > 0)
            {
                var ds = SqlHelper.ExecuteDataSet("MenuItem_Get",
                    new Microsoft.Data.SqlClient.SqlParameter("@MenuID", model.MenuId));

                if (ds.Tables.Count > 0)
                {
                    model.Items = BuildMenuTree(ds.Tables[0], -1);
                }
            }

            return View(model);
        }

        private List<NavMenuItem> BuildMenuTree(System.Data.DataTable table, int parentId)
        {
            var items = new List<NavMenuItem>();
            var rows = table.Select("ParentID=" + parentId);

            foreach (var row in rows)
            {
                bool isActive = row["IsActive"]?.ToString() == "1";
                if (!isActive) continue;

                var item = new NavMenuItem
                {
                    Text = row["Text"]?.ToString(),
                    NavigateUrl = row["NavigateURL"]?.ToString(),
                    Target = row["Target"]?.ToString(),
                    Children = BuildMenuTree(table, (int)row["Id"])
                };

                items.Add(item);
            }

            return items;
        }
    }

    public class NavigationViewModel
    {
        public int MenuId { get; set; }
        public bool IsHorizontal { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public List<NavMenuItem> Items { get; set; } = new List<NavMenuItem>();
    }

    public class NavMenuItem
    {
        public string Text { get; set; }
        public string NavigateUrl { get; set; }
        public string Target { get; set; }
        public List<NavMenuItem> Children { get; set; } = new List<NavMenuItem>();
    }
}
