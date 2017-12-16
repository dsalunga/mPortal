<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.Responses"
    CodeBehind="WM_Responses_09.ascx.cs" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="ControlBox">
            <asp:Button ID="cmdDone" CssClass="Command" runat="server" Text="Done" Width="72px" OnClick="cmdDone_Click">
            </asp:Button>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataGrid ID="grdSurveys" runat="server" Width="100%" BorderStyle="None" AutoGenerateColumns="False"
                CellPadding="3" BackColor="White" BorderWidth="1px" BorderColor="#CCCCCC">
                <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
                <ItemStyle Font-Size="10pt" Font-Names="Verdana" ForeColor="#000066"></ItemStyle>
                <HeaderStyle Font-Size="10pt" Font-Names="Verdana" Font-Bold="True" ForeColor="White"
                    BackColor="#006699"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn DataField="Label" HeaderText="Question"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Answer" HeaderText="Answer"></asp:BoundColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages">
                </PagerStyle>
            </asp:DataGrid>
        </td>
    </tr>
</table>
