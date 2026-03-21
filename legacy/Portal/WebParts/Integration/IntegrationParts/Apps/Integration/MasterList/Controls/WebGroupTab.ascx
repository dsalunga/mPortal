<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebGroupTab.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.Controls.WebGroupTab" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.WebSystem.Apps.Integration" %>
<ul class="breadcrumb">
    <asp:Literal runat="server" ID="lblBreadcrumb"></asp:Literal>
    <%--<li><a href="#">Home</a> <span class="divider">/</span></li>
  <li><a href="#">Library</a> <span class="divider">/</span></li>
  <li class="active">Data</li>--%>
</ul>
<h2 runat="server" id="lblTitle"></h2>
<%--<h4 runat="server" id="lblTitle" class="heading colr">Root</h4>--%>
<%--<% 
    var open = DataHelper.Get(Request, WConstants.OpenKey);
    if (!open.Equals(MasterListConstants.OPEN_KEY_MEMBER_EDIT) && !open.Equals(MasterListConstants.OPEN_KEY_GROUP_EDIT))
    {
%>--%>
<asp:HiddenField ID="hAccess" Value="0" runat="server" />
<ul class="nav nav-tabs">
    <li class="<%= SelectedTab == MasterListTab.GroupOverview ? "active" : "" %>"><a runat="server" id="linkOverview" href="#">Group</a></li>
    <li class="<%= SelectedTab == MasterListTab.Members ? "active" : "" %>"><a runat="server" id="linkMembers" href="#">Members</a></li>
    <li class="<%= SelectedTab == MasterListTab.Groups ? "active" : "" %>"><a runat="server" id="linkSubGroups" href="#">Subgroups</a></li>
    <% if (hAccess.Value == "1")
       { %>
    <li class="<%= SelectedTab == MasterListTab.Access ? "active" : "" %>"><a runat="server" id="linkAccess" href="#">Access</a></li>
    <% } %>
</ul>
<%--<% } %>--%>