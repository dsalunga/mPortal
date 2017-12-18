<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebRoleTab.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.Controls.WebRoleTab" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%--<h3 id="lblHeader" runat="server" class="Header">
</h3>--%>
<h1 id="linkHeader" runat="server" class="central page-header"></h1>
<uc1:TabControl ID="TabControl1" runat="server" SelectedIndex="0" />
