<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebSiteElementSelector.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.Controls.WebSiteElementSelector" %>
Web Site:&nbsp;<asp:DropDownList ID="cboWebSites" runat="server" CssClass="input" 
    AutoPostBack="True" 
    onselectedindexchanged="cboWebSites_SelectedIndexChanged">
</asp:DropDownList>
<asp:TreeView ID="treeElements" runat="server">
</asp:TreeView>
<asp:HiddenField runat="server" ID="hidPartId" Value="-1" />