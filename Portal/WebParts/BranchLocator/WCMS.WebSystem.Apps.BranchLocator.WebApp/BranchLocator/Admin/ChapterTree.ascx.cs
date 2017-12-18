using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.BranchLocator
{
    public partial class WebOfficeTree : System.Web.UI.UserControl
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
            query.Set(WConstants.Load, "FindALocale/Admin/Chapters");

            var node = new TreeNode();
            node.Text = "<strong>Chapters</strong>";
            node.NavigateUrl = query.BuildQuery();
            //node.Target = WConstants.MainFrame;

            var items = MChapter.Provider.GetList();
            Action<int, TreeNode> GenerateTree = null;
            GenerateTree = (parentId, parentNode) =>
            {
                var childItems = items.Where(item => item.ParentId == parentId);
                if (childItems.Count() > 0)
                {
                    foreach (var child in childItems)
                    {
                        query.Set(WebColumns.ParentId, child.Id);

                        var childNode = new TreeNode();
                        childNode.Text = child.Name;
                        childNode.NavigateUrl = query.BuildQuery();
                        //childNode.Target = WConstants.MainFrame;
                        parentNode.ChildNodes.Add(childNode);

                        GenerateTree(child.Id, childNode);
                    }
                }
            };

            GenerateTree(-1, node);

            TreeView1.Nodes.Add(node);
        }
    }
}