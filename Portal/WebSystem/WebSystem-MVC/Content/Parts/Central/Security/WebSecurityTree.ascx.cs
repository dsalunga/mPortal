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
    public partial class WebSecurityTree : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && WSession.Current.UserId > 0)
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
            //tnRoot.NavigateUrl = CentralPages.WebSystemDashboard;

            // Templates
            //BuildTemplatesNode(tnRoot);
            //lblStatus.InnerHtml += "<br/>Web Templates: " + localwatch.Elapsed;
            //localwatch.Reset();
            //localwatch.Start();

            if (!WSession.Current.IsAdministrator)
            {
                var sitePolicy = WebGlobalPolicy.Provider.Get(GlobalPolicies.Administration);
                var perm = sitePolicy.TryGetUserPermission(Permissions.FullControl);

                if (perm != null && perm.IsAllowed)
                    BuildSecurityNode(t);
            }
            else
            {
                BuildSecurityNode(t);
            }

            //t.Nodes.Clear();
            //t.Nodes.Add(tnRoot);
            //t.ExpandAll();

            if (t.Nodes.Count > 0)
                t.Nodes[0].Expanded = true;
        }

        private static void BuildSecurityNode(TreeView treeView)
        {
            var secNode = new TreeNode("Security");
            secNode.ImageUrl = "~/Content/Assets/Images/TreeView/l.gif";
            secNode.SelectAction = TreeNodeSelectAction.Expand;
            {
                // SUPER ADMIN MODULES
                var securityNode = new TreeNode("My Profile");
                securityNode.NavigateUrl = CentralPages.WebUserHome + "?UserId=" + WSession.Current.UserId;
                securityNode.ImageUrl = "~/Content/Assets/Images/Common/ico_edit2.gif";
                secNode.ChildNodes.Add(securityNode);

                if (WSession.Current.IsAdministrator)
                {
                    //tn2 = new TreeNode("Security Audits");
                    //tn2.SelectAction = TreeNodeSelectAction.None;
                    //tn1.ChildNodes.Add(tn2);

                    securityNode = new TreeNode("Users");
                    securityNode.NavigateUrl = CentralPages.WebUsers;
                    securityNode.ImageUrl = "~/Content/Assets/Images/Common/ico_persons.gif";
                    secNode.ChildNodes.Add(securityNode);

                    securityNode = new TreeNode("Groups");
                    securityNode.NavigateUrl = CentralPages.WebGroups;
                    securityNode.ImageUrl = "~/Content/Assets/Images/TreeView/u.gif";
                    secNode.ChildNodes.Add(securityNode);

                    // Define global permissions like access to WebPart Management, Portal management, etc
                    securityNode = new TreeNode("Global Policy");
                    securityNode.NavigateUrl = CentralPages.WebGlobalPolicy;
                    securityNode.ImageUrl = "~/Content/Assets/Images/Common/lock.gif";
                    secNode.ChildNodes.Add(securityNode);

                    securityNode = new TreeNode("Roles");
                    securityNode.NavigateUrl = CentralPages.WebRoles;
                    securityNode.ImageUrl = "~/Content/Assets/Images/TreeView/u.gif";
                    secNode.ChildNodes.Add(securityNode);

                    securityNode = new TreeNode("Permissions");
                    securityNode.NavigateUrl = CentralPages.WebPermissions;
                    securityNode.ImageUrl = "~/Content/Assets/Images/Common/ico_lock.gif";
                    secNode.ChildNodes.Add(securityNode);

                    /*
                    tn2 = new TreeNode("System Events");
                    tn2.NavigateUrl = "~/Content/Admin/SystemEvents.aspx";
                    tn1.ChildNodes.Add(tn2);
                    */
                }
            }

            treeView.Nodes.Add(secNode);
        }
    }
}