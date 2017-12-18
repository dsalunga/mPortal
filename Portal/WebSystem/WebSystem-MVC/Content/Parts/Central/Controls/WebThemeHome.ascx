<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebThemeHome.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Controls.WebThemeHome" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<h1 id="lblHeader" runat="server" class="central page-header">
    Web Theme
</h1>
<uc1:TabControl ID="TabControl1" runat="server" />
