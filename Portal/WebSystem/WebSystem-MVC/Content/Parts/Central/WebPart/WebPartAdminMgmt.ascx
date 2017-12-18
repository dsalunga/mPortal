<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartAdminMgmt.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebPartAdminMgmt" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.Framework.Core" %>
<%@ Register Src="../Controls/WebPartTab.ascx" TagName="WebPartTab" TagPrefix="uc1" %>

<uc1:WebPartTab ID="WebPartTab1" runat="server" />
<br />
<%
    var partId = DataHelper.GetId(Request, WebColumns.PartId);
    if (partId > 0) // && WebGlobalPolicy.IsUserAdded(GlobalPolicies.WebPartManagement)) // NOT CONTENT USER
    {
        //bool hasMgmtPermission = WebGlobalPolicy.IsUserPermitted(GlobalPolicies.WebPartManagement, Permissions.ManageInstance);
        var part = WPart.Get(partId);

        // Limit the list if not an Admin
        if (!WSession.Current.IsSiteManager)
        {
            // Check WebPart permissions
            part = WebObjectSecurity.IsUserAdded(part) ? part : null;
        }
%>
<%--<h3>Administration Parts</h3>--%>
<div class="row">
    <% 
            int adminCounter = 0;
            bool firstPart = true;

            if (part != null && part.IsActive)
            {
                //TreeNode partNode = new TreeNode(part.Name);
                var adminParts = WebPartAdmin.GetList(partId).Where(item => item.IsVisible && item.IsActive);
                Func<int, IEnumerable<WebPartAdmin>, TreeNode, bool> LoadRecursiveParts = null;

                // Definition
                LoadRecursiveParts = (int parentId, IEnumerable<WebPartAdmin> items, TreeNode node) =>
                {
                    // Get all AdminParts in the specific level (having same parents)
                    var levelParts = items.Where(item => item.PartId == part.Id && item.ParentId == parentId);
                    if (!WSession.Current.IsAdministrator)
                    {
                        // Check permission
                        var securityParts = from p in levelParts
                                            where WebObjectSecurity.IsUserAdded(p)
                                            select p;

                        if (securityParts.Count() > 0)
                            levelParts = securityParts;
                    }

                    if (levelParts.Count() > 0)
                    {
                        if (parentId == -1)
                        {
                            if (adminCounter == 0)
                            {
                                firstPart = true;
    %><div class="col-md-4">
        <%
                            }

                            adminCounter += levelParts.Count();

                            if (!firstPart)
                            {
        %><br />
        <%
                            }
                            else
                            {
                                firstPart = false;
                            }
                            
        %><%--<h4 class="muted"><%= part.Name %>: Administration</h4>--%>
        <%
                        }

                        foreach (var partAdmin in levelParts)
                        {
                            if (partAdmin.ParentId != 0)
                            {
        %>
        <div class="media">
            <a class="pull-left" href="<%= WQuery.BuildQuery(CentralPages.LoaderMain, WebColumns.PartAdminId, partAdmin.Id) %>">
                <img class="media-object" width="64" src="/Content/Assets/Images/grafle.png" />
            </a>
            <div class="media-body">
                <h4 class="media-heading"><a title="<%=partAdmin.Name %>" href="<%= WQuery.BuildQuery(partAdmin.TemplateEngineId == TemplateEngineTypes.Razor ? CentralPages.LoaderRazor : CentralPages.LoaderMain, WebColumns.PartAdminId, partAdmin.Id) %>"><%= partAdmin.Name %></a></h4>
                <p>Place description here</p>
                <%
                                LoadRecursiveParts(partAdmin.Id, items, null);
                %>
            </div>
        </div>
        <%
                            }
                        }

                        if (parentId == -1)
                        {
                            if (adminCounter >= 10)
                            {
                                adminCounter = 0;
        %>
    </div>
    <%    
                            }
                        }
                    }
                    return levelParts.Count() > 0;
                };

                if (LoadRecursiveParts(-1, adminParts, null))
                {
                    //partsNode.ChildNodes.Add(partNode);
                }
            } 
    %>
</div>
<% } %>