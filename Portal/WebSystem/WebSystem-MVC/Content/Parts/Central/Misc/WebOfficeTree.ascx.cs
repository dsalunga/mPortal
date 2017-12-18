using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Misc
{
    public partial class WebOfficeTree : System.Web.UI.UserControl
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
            QueryParser query = new QueryParser(CentralPages.WebOffices);

            TreeNode node = new TreeNode();
            node.Text = "<strong>Offices</strong>";
            node.NavigateUrl = query.BuildQuery();
            //node.Target = WebConstants.MainFrame;

            var items = WebOffice.GetList();
            Action<int, TreeNode> GenerateTree = null;
            GenerateTree = (parentId, parentNode) =>
            {
                var childItems = items.Where(item => item.ParentId == parentId);
                if (childItems.Count() > 0)
                {
                    foreach (WebOffice child in childItems)
                    {
                        query.Set(WebColumns.ParentId, child.Id);

                        TreeNode childNode = new TreeNode();
                        childNode.Text = child.Name;
                        childNode.NavigateUrl = query.BuildQuery();
                        //childNode.Target = WebConstants.MainFrame;
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