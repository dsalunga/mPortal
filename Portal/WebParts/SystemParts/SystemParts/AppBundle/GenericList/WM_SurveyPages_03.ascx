<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.CMS_SurveyPages_03"
    CodeBehind="WM_SurveyPages_03.ascx.cs" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="ControlBox">
            <asp:Button ID="cmdCreate" runat="server" Text="Create Page" CssClass="Command" OnClick="cmdCreate_Click" />
            <asp:Button ID="cmdDelete" runat="server" Text="Delete" OnClick="cmdDelete_Click"
                CssClass="Command" />
            <asp:Button ID="cmdReturn" runat="server" Text="Done" OnClick="cmdReturn_Click" CssClass="Command" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataGrid ID="grd" runat="server" Width="100%" BorderStyle="None" AutoGenerateColumns="False"
                CellPadding="3" BackColor="White" BorderWidth="1px" BorderColor="#CCCCCC" OnItemCommand="grd_ItemCommand">
                <FooterStyle ForeColor="#000066" BackColor="White" />
                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999" />
                <ItemStyle Font-Size="10pt" Font-Names="Verdana" ForeColor="#000066" />
                <HeaderStyle Font-Size="10pt" Font-Names="Verdana" Font-Bold="True" ForeColor="White"
                    BackColor="#006699" />
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="Id" HeaderText="ID"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Id" HeaderText="&lt;input type=&quot;checkbox&quot; name=&quot;chkCheckedMain&quot; onclick=&quot;CheckAll(this, 'chkChecked');&quot;&gt;"
                        DataFormatString="&lt;input type=&quot;checkbox&quot; name=&quot;chkChecked&quot; value=&quot;{0}&quot;&gt;">
                        <HeaderStyle Width="25px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Rank" HeaderText="Rank">
                        <HeaderStyle Width="50px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Title" HeaderText="Page Title"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Questions">
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <asp:ImageButton runat="server" CommandName="questions" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                                ID="Imagebutton3" NAME="Imagebutton2"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Edit">
                        <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton runat="server" CommandName="edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                ID="Imagebutton2" NAME="Imagebutton2"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages">
                </PagerStyle>
            </asp:DataGrid>
        </td>
    </tr>
</table>
