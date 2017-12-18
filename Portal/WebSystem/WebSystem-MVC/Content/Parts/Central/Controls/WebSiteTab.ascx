<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebSiteTab.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Controls.WebSiteTab" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%--<h3 id="lblHeader" runat="server">
    Web Site
</h3>
<div style="margin-bottom: 3px" id="tableWebSite" runat="server" visible="false">
    URL:&nbsp;&nbsp;
    <asp:Literal ID="lblSiteURL" runat="server"></asp:Literal>
    <br />
    <br />
</div>--%>
<h1 style="font-weight: normal; margin-bottom: 5px; margin-top: 30px;" class="page-header"><a href="#" id="linkHeader" runat="server">Web Site</a></h1>
<uc1:TabControl ID="TabControl1" runat="server" />
