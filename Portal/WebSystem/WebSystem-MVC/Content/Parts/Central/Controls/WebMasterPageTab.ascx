<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebMasterPageTab.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Controls.WebMasterPageTab" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%--<h3 id="lblHeader" runat="server">Web Master Page
</h3>
<div id="tableWebSite" runat="server" visible="false">
    <asp:Label ID="lblSiteURL" runat="server"></asp:Label>
</div>--%>
<h1 style="font-weight: normal; margin-bottom: 5px; margin-top: 30px;" class="page-header"><a href="#" id="linkHeader" runat="server">Web Master Page</a></h1>
<uc1:TabControl ID="TabControl1" runat="server" />
