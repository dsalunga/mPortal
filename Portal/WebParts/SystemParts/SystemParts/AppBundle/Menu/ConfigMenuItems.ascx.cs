using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Menu
{
    public partial class ConfigMenuItems : System.Web.UI.UserControl
    {
        private int menuId;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            menuId = DataHelper.GetId(Request, "MenuId");
            if (!Page.IsPostBack)
            {
                this.PopulateTreeView();

                string sParentID = Request["ParentId"];
                if (!string.IsNullOrEmpty(sParentID))
                    WebHelper.FindTreeNode(TreeView1.Nodes, sParentID);
            }
        }

        private void PopulateTreeView()
        {
            string rootName = WebRegistry.SelectNode("/System/Name").Value; //"Portal";

            if (menuId > 0)
            {
                var item = MenuEntity.Provider.Get(menuId);
                if (item != null)
                    rootName = item.Name;
            }

            TreeView1.Nodes.Clear();
            DropDownList1.Items.Clear();

            // TREE VIEW
            TreeNode tnRoot = new TreeNode(rootName, "-1");
            tnRoot.Selected = true;
            TreeView1.Nodes.Add(tnRoot);

            // COMBO BOX
            ListItem itemRoot = new ListItem(rootName, "-1");
            DropDownList1.Items.Add(itemRoot);
            {
                var items = MenuItem.Provider.GetList(menuId);

                // START RECURSIVE
                LoadRecursiveTree(-1, items, tnRoot, "");
            }

            TreeView1.CollapseAll();
            TreeView1.Nodes[0].Expanded = true;
        }

        private void LoadRecursiveTree(int parentId, IEnumerable<MenuItem> menuItems, TreeNode tnRoot, string tab)
        {
            tab += WConstants.TAB;

            var levelItems = menuItems.Where(i => i.ParentId == parentId);
            foreach (var menuItem in levelItems)
            {
                // TREE VIEW
                TreeNode node = new TreeNode(menuItem.Text, menuItem.Id.ToString());
                node.SelectAction = TreeNodeSelectAction.Select;
                tnRoot.ChildNodes.Add(node);

                // COMBO BOX
                ListItem listItem = new ListItem(tab + "\u2022\u00a0" + DataHelper.GetStringPreview(menuItem.Text, 50), menuItem.Id.ToString());
                DropDownList1.Items.Add(listItem);

                LoadRecursiveTree(menuItem.Id, menuItems, node, tab);
            }
        }

        protected void cmdAdd_Click(object sender, System.EventArgs e)
        {
            var query = new WQuery(this);
            query.Set(WebColumns.ParentId, TreeView1.SelectedNode.Value);
            query.LoadAndRedirect("ConfigMenuItemEdit.ascx");
        }

        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(sChecked);
                foreach (var id in ids)
                    MenuItem.Provider.Delete(id);

                var query = new WQuery(this);
                query.Redirect();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            var query = new WQuery(this);

            switch (e.CommandName)
            {
                case "edit_item":
                    query.Set("MenuItemID", id);
                    query.Set(WebColumns.ParentId, TreeView1.SelectedNode.Value);
                    query.LoadAndRedirect("ConfigMenuItemEdit.ascx");
                    break;

                case "View_ChildNodes":
                    query.Set(WebColumns.ParentId, id);
                    query.Redirect();
                    break;
            }
        }
        protected void cmdMove_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                int parentId = int.Parse(DropDownList1.SelectedValue);
                var ids = DataHelper.ParseCommaSeparatedIdList(sChecked);
                if (ids.Count > 0)
                {
                    foreach (var id in ids)
                    {
                        var item = MenuItem.Provider.Get(id);
                        if (item != null && item.Id != parentId)
                        {
                            item.ParentId = parentId;
                            item.Update();
                        }
                    }

                    var query = new WQuery(this);
                    query.Redirect();
                }
            }
        }

        protected void cmdGO_Click(object sender, EventArgs e)
        {
            WebHelper.FindTreeNode(TreeView1.Nodes, DropDownList1.SelectedValue);
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeView1.SelectedNode.Expand();
        }

        public DataSet Select(int menuId, int parentId)
        {
            var query = new WQuery(true);

            return DataHelper.ToDataSet(
                from i in MenuItem.Provider.GetList(menuId, parentId)
                select new
                {
                    i.Id,
                    i.Text,
                    i.Target,
                    i.Active,
                    i.CheckPermission,
                    i.Rank,
                    i.PageUrl,
                    TextUrl = query.Set(WebColumns.ParentId, i.Id).BuildQuery()
                });
        }
    }
}