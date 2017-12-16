<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.CreateQuestionItems"
    CodeBehind="WM_CreateQuestionItems_07.ascx.cs" %>
<table width="100%" border="0">
    <tr>
        <td>
            <!-- Add Question Item -->
            <table height="100%" width="100%" border="0">
                <tr>
                    <td>
                        <font size="2">Option ID:</font>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOptionID" ReadOnly="True" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        <font size="2">Option Type:</font>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboTypes" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <font size="2">Display Text:</font>
                    </td>
                    <td>
                        <asp:TextBox ID="txtItemText" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <font size="2">Ranking:</font>
                    </td>
                    <td>
                        <asp:TextBox ID="txtItemRanking" runat="server" Width="72px">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="cmdInsert" runat="server" Width="72px" Text="Insert" CssClass="Command"
                            OnClick="cmdInsert_Click" />&nbsp;
                        <asp:Button ID="cmdDone" runat="server" Width="72px" Text="Done" CssClass="Command" OnClick="cmdDone_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="ControlBox">
                        <asp:Button ID="cmdDelete" runat="server" Width="72px" Text="Delete" CssClass="Command"
                            OnClick="cmdDelete_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataGrid ID="grdOptions" runat="server" Width="100%" BorderStyle="None" BorderColor="#CCCCCC"
                            BorderWidth="1px" BackColor="White" CellPadding="3" AutoGenerateColumns="False"
                            OnItemCommand="grdOptions_ItemCommand">
                            <FooterStyle ForeColor="#000066" BackColor="White" />
                            <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
                            <ItemStyle Font-Size="10pt" Font-Names="Verdana" ForeColor="#000066"></ItemStyle>
                            <HeaderStyle Font-Size="10pt" Font-Names="Verdana" Font-Bold="True" ForeColor="White"
                                BackColor="#006699"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="Id" HeaderText="Option ID"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Id" HeaderText="&lt;input type=&quot;checkbox&quot; name=&quot;chkCheckedMain&quot; onclick=&quot;CheckAll(this, 'chkChecked');&quot;&gt;"
                                    DataFormatString="&lt;input type=&quot;checkbox&quot; name=&quot;chkChecked&quot; value=&quot;{0}&quot;&gt;">
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Rank" HeaderText="Rank">
                                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Caption" HeaderText="Display Text"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Label" HeaderText="Option Type">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Edit">
                                    <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" CommandName="edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                            ID="Imagebutton1"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages">
                            </PagerStyle>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
