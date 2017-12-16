<%@ Control Language="C#" %>
<asp:DataList ID="DataList1" runat="server" DataKeyField="ListItemID" RepeatColumns="2"
    CellPadding="5" RepeatDirection="Vertical" Width="100%">
    <ItemTemplate>
            <%# Eval("Field1") %>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Left" />
    <AlternatingItemStyle BackColor="#1A1A1A" HorizontalAlign="Left" />
</asp:DataList>