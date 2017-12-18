<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSystemHome" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.Framework.Core" %>
<%
    var isAdmin = WSession.Current.IsAdministrator;
    var isSiteAdmin = WSession.Current.IsSiteManager; // WebGlobalPolicy.IsUserAdded(GlobalPolicies.WebSiteManagement);
    var siteId = DataHelper.GetId(Request, WebColumns.SiteId);
    WSite site = null;

    if (siteId > 0)
        site = WSite.Get(siteId);
%>

<style type="text/css">
    .dashboard h1, .dashboard h2, .dashboard h3, .dashboard h4 {
        font-weight: bold;
    }
</style>
<section class="dashboard">
    <div class="page-header">
        <h1>
            <asp:Literal ID="lWebAppName" runat="server"></asp:Literal>&nbsp;<small>Dashboard</small></h1>
    </div>

    <% if (isSiteAdmin)
       { %>
    <h3>Web Portal</h3>
    <div class="row">
        <div class="col-md-4 col-sm-5">
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebSites %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/globe.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebSites %>">Web Sites</a></h4>
                    Place description here
                </div>
            </div>
            <% if(isAdmin){ %>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebTemplates %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/windows.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebThemes %>">Themes & Templates</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.ShortUrlManager %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/home.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.ShortUrlManager %>">Short URL Manager</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%= WebPartAdmin.BuildUrl("Central", "Web Bindings") %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/faq.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= WebPartAdmin.BuildUrl("Central", "Web Bindings") %>">Site Bindings</a></h4>
                    Place description here
                </div>
            </div>
            <% } %>
        </div>
        <% if(isSiteAdmin){ %>
        <div class="col-md-4 col-sm-5">
            <% if(WebGlobalPolicy.IsUserPermitted(GlobalPolicies.Administration, Permissions.UsersManagement)) { %>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebUsers %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/user_green.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebUsers %>">Users</a></h4>
                    Place description here
                </div>
            </div>
            <% }
               
               if(WebGlobalPolicy.IsUserPermitted(GlobalPolicies.Administration, Permissions.GroupsManagement)) {
            %>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebGroups %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/user_group.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebGroups %>">Groups</a></h4>
                    Place description here
                </div>
            </div>
            <% } %>
            <%--<div class="media">
                <a class="pull-left" href="<%= CentralPages.WebRoles %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/user_group.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebRoles %>">Roles</a></h4>
                    Place description here
                </div>
            </div>--%>
            <% if(isAdmin) { %>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebPermissions %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/lock.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebPermissions %>">Permissions</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebGlobalPolicy %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/police.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebGlobalPolicy %>">Security Policy</a></h4>
                    Place description here
                </div>
            </div>
            <% } %>
        </div>
        <% } %>
        <div class="col-md-4 col-sm-5">
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebParts %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/install.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebParts %>">Applications</a></h4>
                    Place description here
                </div>
            </div>
            <% if(isAdmin){ %>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebRegistry %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/grafle.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebRegistry %>">Registry</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebParameterSets %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/plugin.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebParameterSets %>">Parameter Sets</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebResourceManager %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/cd.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebResourceManager %>">Resources</a></h4>
                    Place description here
                </div>
            </div>
            <% } %>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebOffices %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/www_dial.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebOffices %>">Office Manager</a></h4>
                    Place description here
                </div>
            </div>
        </div>
    </div>
    <br />
    <% } %>

    <% if (site != null && isSiteAdmin) { %>
    <h3><%=site.Name %></h3>
    <div class="row">
        <div class="col-md-4 col-sm-5">
            <div class="media">
                <a class="pull-left" href="<%=WQuery.BuildQuery(CentralPages.WebSiteHome, WebColumns.SiteId, siteId) %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/web.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%=WQuery.BuildQuery(CentralPages.WebSiteHome, WebColumns.SiteId, siteId) %>">Properties</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%=WQuery.BuildQuery(CentralPages.WebPages, WebColumns.SiteId, siteId) %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/doc_globe.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%=WQuery.BuildQuery(CentralPages.WebPages, WebColumns.SiteId, siteId) %>">Web Pages</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%=WQuery.BuildQuery(CentralPages.WebMasterPages, WebColumns.SiteId, siteId) %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/connect.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%=WQuery.BuildQuery(CentralPages.WebMasterPages, WebColumns.SiteId, siteId) %>">Master Pages</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%=WQuery.BuildQuery(CentralPages.WebSites, WebColumns.SiteId, siteId) %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/faq.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%=WQuery.BuildQuery(CentralPages.WebSites, WebColumns.SiteId, siteId) %>">Web Bindings</a></h4>
                    Place description here
                </div>
            </div>
        </div>
        <div class="col-md-4 col-sm-5">
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebUsers %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/user_green.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= WQuery.BuildQuery(CentralPages.WebUsers, WebColumns.SiteId, siteId) %>">Users</a></h4>
                    Place description here
                </div>
            </div>
        </div>
        <%--<div class="col-md-4 col-sm-5">
            <div class="media">
                <a class="pull-left" href="<%=QueryParser.BuildQuery(CentralPages.WebPages, WebColumns.SiteId, siteId) %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/web.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%=QueryParser.BuildQuery(CentralPages.WebPages, WebColumns.SiteId, siteId) %>">Resources</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%=QueryParser.BuildQuery(CentralPages.WebPages, WebColumns.SiteId, siteId) %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/locked.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%=QueryParser.BuildQuery(CentralPages.WebPages, WebColumns.SiteId, siteId) %>">Security</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%=QueryParser.BuildQuery(CentralPages.WebPages, WebColumns.SiteId, siteId) %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/web.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%=QueryParser.BuildQuery(CentralPages.WebPages, WebColumns.SiteId, siteId) %>">Parameters</a></h4>
                    Place description here
                </div>
            </div>
        </div>--%>
    </div>
    <br />
    <% } %>

    <% if (isAdmin)
       { %>
    <h3>Tools & Utilities</h3>
    <div class="row">
        <div class="col-md-4 col-sm-5">
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.QueryAnalyzer %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/statics-1.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.QueryAnalyzer %>">Query Analyzer</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.FileManager %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/folder.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.FileManager %>">File Manager</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.SmtpAnalyzer %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/email2.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.SmtpAnalyzer %>">SMTP Diagnostics</a></h4>
                    Place description here
                </div>
            </div>
        </div>
        <div class="col-md-4 col-sm-5">
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebObjectManager %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/chart.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebObjectManager %>">Object Manager</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.WebDataExplorer %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/database.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.WebDataExplorer %>">Data Explorer</a></h4>
                    Place description here
                </div>
            </div>
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.FileManager %>?Path=<%=WebHelper.TEMP_DATA_PATH %>Logs/">
                    <img class="media-object" width="64" src="/Content/Assets/Images/bug.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.FileManager %>?Path=<%=WebHelper.TEMP_DATA_PATH %>Logs/">Error Logs Viewer</a></h4>
                    Place description here
                </div>
            </div>
        </div>
        <div class="col-md-4 col-sm-5">
            <div class="media">
                <a class="pull-left" href="<%= CentralPages.Setup %>">
                    <img class="media-object" width="64" src="/Content/Assets/Images/gears.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="<%= CentralPages.Setup %>">Online Setup</a></h4>
                    Place description here
                </div>
            </div>
        </div>
    </div>
    <br />
    <% } %>

    <h3>More links</h3>
    <div class="row">
        <div class="col-md-4 col-sm-5">
            <div class="media">
                <a class="pull-left" href="#">
                    <img class="media-object" width="64" src="/Content/Assets/Images/web.png" />
                </a>
                <div class="media-body">
                    <h4 class="media-heading"><a href="#" id="defaultWebSite" runat="server"></a></h4>
                    Playground website for testing.
                </div>
            </div>
        </div>
    </div>
</section>
