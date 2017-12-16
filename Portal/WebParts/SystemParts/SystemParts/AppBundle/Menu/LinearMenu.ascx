<%@ Control Language="C#" AutoEventWireup="true" Inherits="_Sections_STDMENU_LinearMenu"
    CodeBehind="LinearMenu.ascx.cs" %>
<asp:DataList ID="DataList1" runat="server" RepeatColumns="3" CellPadding="5">
    <ItemTemplate>
        •&nbsp;<a class="click" href='<%# Eval("Url") %>'><%# Eval("Name") %></a>
    </ItemTemplate>
</asp:DataList>