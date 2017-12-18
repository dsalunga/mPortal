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
    public partial class WebToolsTree : System.Web.UI.UserControl
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

            if (!WSession.Current.IsAdministrator)
            {
                var sitePolicy = WebGlobalPolicy.Provider.Get(GlobalPolicies.Administration);
                var perm = sitePolicy.TryGetUserPermission(Permissions.FullControl);

                if (perm != null && perm.IsAllowed)
                {
                    // Tools
                    BuildToolsNode(t);
                }
            }
            else
            {
                BuildToolsNode(t);
            }

            //t.Nodes.Clear();
            //t.Nodes.Add(tnRoot);
            //t.CollapseAll();

            if (t.Nodes.Count > 0)
                t.Nodes[0].Expanded = true;
        }

        private static void BuildToolsNode(TreeView treeView)
        {
            TreeNode toolsNode = new TreeNode("Tools");
            toolsNode.ImageUrl = "~/Content/Assets/Images/Common/Tools.gif";
            toolsNode.SelectAction = TreeNodeSelectAction.Expand;
            {
                TreeNode toolNode = null;

                toolNode = new TreeNode("Online Setup");
                toolNode.NavigateUrl = CentralPages.Setup;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/ico_tools.gif";
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("Web Registry");
                toolNode.NavigateUrl = CentralPages.WebRegistry;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/mp2.gif";
                toolsNode.ChildNodes.Add(toolNode);

                //tn2 = new TreeNode("IP Restrictions");
                //tn2.NavigateUrl = "~/Content/Admin/IPAddresses.aspx";
                //tn1.ChildNodes.Add(tn2);

                //tn2 = new TreeNode("Web Logs");
                //tn2.NavigateUrl = "~/Content/Admin/SiteLogs.aspx";
                //tn1.ChildNodes.Add(tn2);

                toolNode = new TreeNode("Query Analyzer");
                toolNode.NavigateUrl = CentralPages.QueryAnalyzer;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/view.gif";
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("File Manager");
                toolNode.NavigateUrl = CentralPages.FileManager;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/webfolder.gif";
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("SMTP Diagnostics");
                toolNode.NavigateUrl = CentralPages.SmtpAnalyzer;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/ico_postnew.gif";
                toolsNode.ChildNodes.Add(toolNode);

                // Manages all data stores like database tables, xml files, etc
                //toolNode = new TreeNode("Data Manager");
                //toolNode.NavigateUrl = "~/Central/WebDataStoreManager.aspx"; // Entity Manager?
                //toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("Data Explorer");
                toolNode.NavigateUrl = CentralPages.WebDataExplorer;
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("Parameter Sets");
                toolNode.NavigateUrl = CentralPages.WebParameterSets;
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("Template Manager");
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/Page.gif";
                toolNode.NavigateUrl = CentralPages.WebTemplates;
                toolsNode.ChildNodes.Add(toolNode);

                toolNode = new TreeNode("Resource Manager");
                toolNode.NavigateUrl = CentralPages.WebResourceManager;
                toolNode.ImageUrl = "~/Content/Assets/Images/Common/Objects.gif";
                toolsNode.ChildNodes.Add(toolNode);
            }

            treeView.Nodes.Add(toolsNode);
        }
    }
}