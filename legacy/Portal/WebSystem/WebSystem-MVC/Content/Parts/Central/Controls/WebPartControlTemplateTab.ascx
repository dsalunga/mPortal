<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPartControlTemplateTab.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Controls.WebPartControlTemplateTab" %>
<%@ Register Src="../../../Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<h3 id="lblHeader" runat="server">Web Parts</h3>
<uc1:TabControl ID="TabControl1" runat="server" />