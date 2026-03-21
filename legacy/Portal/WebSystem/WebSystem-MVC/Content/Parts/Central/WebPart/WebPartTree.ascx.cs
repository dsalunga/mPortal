using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebPartTree : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && WSession.Current.UserId > 0)
            {
                this.BuildTreeView();
            }
        }

        private void BuildTreeView()
        {
            // Start the stopwatch
            //Stopwatch localwatch = new Stopwatch();
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //localwatch.Start();

            bool showOrphan = WebRegistry.SelectNode("/System/ShowOrphan").Value == "true";
            bool serverMode = WebRegistry.SelectNode("/System/TreeViewServerMode").Value == "true";
            bool showSiteSecurity = WebRegistry.SelectNode("/System/ShowSiteSecurity").Value == "true";

            t.EnableClientScript = !serverMode;
            t.EnableViewState = serverMode;
            t.Nodes.Clear();

            //linkAboutMe.HRef += UserSession.UserId;

            //TreeNode tnRoot = new TreeNode(WConfig.SystemName);
            //tnRoot.NavigateUrl = CentralPages.WebSystemHome;

            // Templates
            //BuildTemplatesNode(tnRoot);
            //lblStatus.InnerHtml += "<br/>Web Templates: " + localwatch.Elapsed;
            //localwatch.Reset();
            //localwatch.Start();

            // WebParts
            BuildWebPartsNode(t);
            //lblStatus.InnerHtml += "<br/>Web Parts: " + localwatch.Elapsed;

            //t.Nodes.Clear();
            //t.Nodes.Add(tnRoot);
            t.CollapseAll();

            if (t.Nodes.Count > 0)
                t.Nodes[0].Expanded = true;

            // Stop the stopwatch
            //stopwatch.Stop();
            //lblStatus.InnerHtml += "<br/>Execution time: " + stopwatch.Elapsed;
        }

        private static void BuildWebPartsNode(TreeView treeView)
        {
            if (WSession.Current.IsAdministrator || WebUser.Get(WSession.Current.UserId).IsMemberOf("WebPart Manager")) // NOT CONTENT USER
            {
                TreeNode partsNode = new TreeNode("Web Parts");
                partsNode.ImageUrl = "~/Content/Assets/Images/Common/Modules2.gif";

                IEnumerable<WPart> parts = WPart.GetList();
                var admin = WebPartAdmin.GetList();

                if (!WSession.Current.IsAdministrator)
                {
                    // Check WebPart permissions
                    parts = (from p in parts
                             where WebObjectSecurity.IsUserAdded(p) // WebObjectSecurity.Provider.Get(WebObjects.WebPart, p.Id, WebObjects.WebUser, WSession.Current.UserId, 0) != null
                             select p);
                }

                foreach (WPart part in parts)
                {
                    if (part.Active != 1) continue;

                    TreeNode partNode = new TreeNode(part.Name);
                    var adminParts = admin.Where(item => item.PartId == part.Id);
                    Func<int, IEnumerable<WebPartAdmin>, TreeNode, int, bool> LoadRecursiveParts = null;

                    LoadRecursiveParts = (int parentId, IEnumerable<WebPartAdmin> items, TreeNode node, int partId) =>
                    {
                        var levelParts = items.Where(item => item.PartId == partId && item.ParentId == parentId);
                        if (!WSession.Current.IsAdministrator)
                        {
                            // Check permission
                            var securityParts = (from p in levelParts
                                                 where WebObjectSecurity.IsUserAdded(p) //.Provider.Get(WebObjects.WebPartAdmin, p.Id, WebObjects.WebUser, WSession.Current.UserId, 0) != null
                                                 select p);
                            if (securityParts.Count() > 0)
                                levelParts = securityParts;
                        }

                        foreach (WebPartAdmin partAdmin in levelParts)
                        {
                            if (partAdmin.ParentId != 0)
                            {
                                TreeNode adminNode = new TreeNode(partAdmin.Name);
                                adminNode.ImageUrl = "~/Content/Assets/Images/TreeView/cl.gif";
                                adminNode.NavigateUrl = "~/Content/Parts/Central/?PartAdminId=" + partAdmin.Id;

                                LoadRecursiveParts(partAdmin.Id, items, adminNode, partId);
                                node.ChildNodes.Add(adminNode);
                            }
                        }

                        return levelParts.Count() > 0;
                    };

                    if (LoadRecursiveParts(-1, adminParts, partNode, part.Id))
                    {
                        partsNode.ChildNodes.Add(partNode);
                        partNode.ImageUrl = "~/Content/Assets/Images/TreeView/mo.gif";

                        if (WSession.Current.IsAdministrator)
                            partNode.NavigateUrl = string.Format("{0}{1}{2}", CentralPages.WebPartHome, "?PartId=", part.Id);
                        else
                            partNode.SelectAction = TreeNodeSelectAction.Expand;
                    }
                }

                if (WSession.Current.IsAdministrator)
                {
                    partsNode.NavigateUrl = CentralPages.WebParts;
                    treeView.Nodes.Add(partsNode);
                }
                else
                {
                    partsNode.SelectAction = TreeNodeSelectAction.Expand;
                    if (partsNode.ChildNodes.Count > 0)
                    {
                        treeView.Nodes.Add(partsNode);
                    }
                }
            }
        }
    }
}