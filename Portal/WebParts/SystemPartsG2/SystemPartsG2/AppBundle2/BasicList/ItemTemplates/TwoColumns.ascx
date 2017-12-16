<%@ Control Language="C#" %>
<asp:DataList ID="DataList1" runat="server" DataKeyField="ListItemID" RepeatColumns="2"
    CellPadding="5" RepeatDirection="Vertical" Width="100%">
    <ItemTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width="80%">
            <tr>
                <td>
                        <%# Eval("Field1") %>
                </td>
                <td>
                        <%# Eval("Field2") %>
                </td>
            </tr>
        </table>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="center" />
    <AlternatingItemStyle BackColor="#1A1A1A" HorizontalAlign="center" />
</asp:DataList>