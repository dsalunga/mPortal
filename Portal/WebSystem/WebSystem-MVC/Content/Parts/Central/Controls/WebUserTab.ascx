<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserTab.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Controls.WebUserTab" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%--<h3 id="lblHeader" runat="server">
</h3>--%>
<h1 id="lblHeader" runat="server" style="font-weight: normal; margin-bottom: 5px; margin-top: 30px;" class="page-header"></h1>
<uc1:TabControl ID="TabControl1" runat="server" SelectedIndex="0" />
