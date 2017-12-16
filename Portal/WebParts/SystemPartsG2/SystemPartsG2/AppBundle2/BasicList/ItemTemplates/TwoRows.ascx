<%@ Control Language="C#" %>
<asp:DataList ID="DataList1" runat="server" DataKeyField="ListItemID" RepeatColumns="2"
    CellPadding="5" RepeatDirection="Vertical" Width="100%">
    <ItemTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center"><%# Eval("Field1") %></td>
            </tr>
            <tr>
                <td align="center" style="font-style: italic"><%# Eval("Field2") %></td>
            </tr>
        </table>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="center" />
    <AlternatingItemStyle HorizontalAlign="center" />
</asp:DataList>