<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebGenericTab.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Controls.WebGenericTab" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<h1 class="central page-header"><a href="#" id="linkHeader" runat="server"></a></h1>
<%--<div style="margin-bottom: 20px" id="panelSiteInfo" runat="server" visible="false">
    <span class="url muted">&nbsp;<asp:Literal ID="lblSiteURL" runat="server"></asp:Literal></span>
</div>--%>
<uc1:TabControl ID="GenericTab" runat="server" />
