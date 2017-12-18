using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class WebRegistryTree : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTree();
            }
        }

        private void LoadTree()
        {
            var query = new WQuery(this);
            //qs[WebColumns.ParentId] = "-1";
            var id = query.GetId(WebColumns.RegistryId);
            var pId = query.GetId(WebColumns.ParentId);
            query.BaseAddress = CentralPages.WebRegistry;
            query.Remove(WebColumns.RegistryId);

            var node = new TreeNode();
            node.Text = "<strong>Registry</strong>";
            node.NavigateUrl = query.BuildQuery();
            //node.Target = WebConstants.MainFrame;

            var items = WebRegistry.GetList();
            Func<int, TreeNode, bool> GenerateTree = null;
            GenerateTree = (parentId, parentNode) =>
            {
                bool hasExpanded = false;
                var childItems = items.Where(item => item.ParentId == parentId);
                if (childItems.Count() > 0)
                {
                    foreach (var child in childItems)
                    {
                        query.Set(WebColumns.ParentId, child.Id);
                        
                        var childNode = new TreeNode();
                        childNode.Text = child.Key;
                        childNode.NavigateUrl = query.BuildQuery();
                        //childNode.Target = WebConstants.MainFrame;
                        parentNode.ChildNodes.Add(childNode);
                        var isExpanded = GenerateTree(child.Id, childNode) || child.Id == id || child.Id == pId;
                        childNode.Expanded = isExpanded;
                        hasExpanded = hasExpanded || isExpanded;
                    }
                }
                return hasExpanded;
            };

            GenerateTree(-1, node);
            //node.CollapseAll();
            node.Expand();
            TreeView1.Nodes.Add(node);
        }
    }
}