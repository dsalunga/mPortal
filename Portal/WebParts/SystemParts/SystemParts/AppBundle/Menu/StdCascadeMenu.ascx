<%@ Control Language="C#" AutoEventWireup="true" Inherits="_Sections_STDMENU_CascadeMenu"
    EnableViewState="false" CodeBehind="StdCascadeMenu.ascx.cs" %>
<asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" CssClass="Menu" StaticPopOutImageUrl="/Images/WebSites/test/menu_arrow.gif">
    <StaticMenuItemStyle CssClass="StaticMenuItem" ForeColor="Black" />
    <StaticHoverStyle CssClass="StaticHover" BackColor="#D9D9D9" BorderStyle="None" ForeColor="Black" />
    <DynamicHoverStyle CssClass="DynamicHover" BackColor="Gray" />
    <DynamicMenuItemStyle CssClass="DynamicMenuItem" />
    <DynamicMenuStyle CssClass="DynamicMenu" />
</asp:Menu>
