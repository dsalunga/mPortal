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
    public partial class WebFolderTree : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadTree();
            }
        }

        private void LoadTree()
        {
            QueryParser query = new QueryParser(CentralPages.WebDataExplorer);
            var items = WebFolder.Provider.GetList();

            //TreeView1.Target = WebConstants.MainFrame;

            // Root Node
            TreeNode node = new TreeNode();
            node.Text = "<strong>Data Explorer</strong>";
            node.NavigateUrl = query.BuildQuery();


            // Public Node
            TreeNode subNode = new TreeNode();
            subNode.Text = "Public";
            subNode.SelectAction = TreeNodeSelectAction.Expand;
            node.ChildNodes.Add(subNode);

            // Generate Tree Recursive
            Action<int, TreeNode> GenerateTreeRecursive = null;
            GenerateTreeRecursive = (parentId, parentNode) =>
            {
                var childItems = items.Where(item => item.ParentId == parentId);
                if (childItems.Count() > 0)
                {
                    foreach (WebFolder child in childItems)
                    {
                        query.Set(WebColumns.ParentId, child.Id);

                        TreeNode childNode = new TreeNode();
                        childNode.Text = child.Name;
                        childNode.NavigateUrl = query.BuildQuery();
                        //childNode.Target = WebConstants.MainFrame;
                        parentNode.ChildNodes.Add(childNode);

                        GenerateTreeRecursive(child.Id, childNode);
                    }
                }
            };

            // Public Tree
            GenerateTreeRecursive(-1, subNode);

            // WebSites Node
            subNode = new TreeNode();
            subNode.Text = "WebSites";
            subNode.SelectAction = TreeNodeSelectAction.Expand;
            node.ChildNodes.Add(subNode);

                // WebSite Node
                    // WebSite-Public Node
                    // WebParts Node

            // Public WebParts Node

            TreeView1.Nodes.Add(node);
        }
    }
}