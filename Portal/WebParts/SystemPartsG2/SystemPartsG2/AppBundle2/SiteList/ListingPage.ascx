<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.SiteList._Sections_SITELIST_ListingPage"
    CodeBehind="ListingPage.ascx.cs" %>
<div class="title" runat="server" id="divHeaderText">
    International Humanitarian Laws</div>
<br />
<asp:DataList ID="DataList1" runat="server" RepeatColumns="3" CellPadding="5">
    <ItemTemplate>
        <!--<img src="/Assets/Uploads/Image/HOME/PNRC/redcircle.gif" />-->
        •&nbsp;<a class="click" href='<%# DataBinder.Eval(Container.DataItem, "SiteURL") %>'><%# DataBinder.Eval(Container.DataItem, "SiteName") %></a>
    </ItemTemplate>
</asp:DataList>