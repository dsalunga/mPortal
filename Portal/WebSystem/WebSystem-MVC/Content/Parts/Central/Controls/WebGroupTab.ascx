<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebGroupTab.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Controls.WebGroupTab" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%--<h3 id="lblHeader" runat="server">Groups</h3>--%>
<h1 runat="server" id="lblHeader" style="font-weight: normal; margin-bottom: 5px; margin-top: 30px;" class="page-header">Groups</h1>
<div style="padding: 5px 0 5px 0">
    <span runat="server" class="breadcrumb" id="lblBreadcrumb"></span>
</div>
<br />
<uc1:TabControl ID="TabControl1" runat="server" SelectedIndex="0" />
