using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class WebGroupTree : System.Web.UI.UserControl
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
            var isAdmin = WSession.Current.IsAdministrator;
            var query = new WQuery(true);
            var id = query.GetId(WebColumns.GroupId);
            var pId = query.GetId(WebColumns.ParentId);

            query.Remove(WebColumns.GroupId);
            query.Remove(WebColumns.ParentId);

            var node = new TreeNode();
            node.Text = "<strong>Groups</strong>";
            node.NavigateUrl = query.BuildQuery();
            //node.Target = WebConstants.MainFrame;
            //query.BasePath = CentralPages.WebGroupHome;

            var items = WebGroup.GetList();
            Func<int, TreeNode, bool> GenerateTree = null;
            GenerateTree = (parentId, parentNode) =>
            {
                bool hasExpanded = false;
                var childItems = items.Where(item => item.ParentId == parentId);
                if (childItems.Count() > 0)
                {
                    foreach (var child in childItems)
                    {
                        if (isAdmin || (child.Id != SystemGroups.ADMINS_GROUP_ID))
                        {
                            query.Set(WebColumns.ParentId, child.Id);
                            query.Set(WebColumns.GroupId, child.Id);

                            var childNode = new TreeNode();
                            childNode.Text = child.Name;
                            childNode.NavigateUrl = query.BuildQuery();
                            //childNode.Target = WebConstants.MainFrame;
                            parentNode.ChildNodes.Add(childNode);
                            var isExpanded = GenerateTree(child.Id, childNode) || child.Id == id || child.Id == pId;
                            childNode.Expanded = isExpanded;
                            hasExpanded = hasExpanded || isExpanded;
                        }
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