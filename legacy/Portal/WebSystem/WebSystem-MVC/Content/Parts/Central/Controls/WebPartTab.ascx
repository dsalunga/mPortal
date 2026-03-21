<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartTab.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Controls.WebPartTab" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%--<h3 id="lblHeader" runat="server">
    Web Parts
</h3>--%>
<h1 id="lblHeader" runat="server" style="font-weight: normal; margin-bottom: 5px; margin-top: 30px;" class="page-header">Apps</h1>
<uc1:TabControl ID="TabControl1" runat="server" />
