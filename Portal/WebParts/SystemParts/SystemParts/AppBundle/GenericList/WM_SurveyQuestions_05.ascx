<%@ Control Language="c#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.GenericForm.CMS_SurveyQuestions"
    CodeBehind="WM_SurveyQuestions_05.ascx.cs" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:Label ID="lblTitle" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="ControlBox">
            <asp:Button ID="cmdNewQuestion" runat="server" CssClass="Command" Text="New Column" OnClick="cmdNewQuestion_Click" />
            <asp:Button ID="cmdDelete" runat="server" Text="Delete" CssClass="Command" OnClick="cmdDelete_Click" />
            <asp:Button ID="cmdReturn" runat="server" Text="Done" CssClass="Command" OnClick="cmdReturn_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataGrid ID="grdQuestions" runat="server" BorderStyle="None" Width="100%" BorderColor="#CCCCCC"
                BorderWidth="1px" BackColor="White" CellPadding="3" AutoGenerateColumns="False"
                OnItemCommand="grdQuestions_ItemCommand">
                <FooterStyle ForeColor="#000066" BackColor="White" />
                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999" />
                <ItemStyle Font-Size="10pt" Font-Names="Verdana" ForeColor="#000066" />
                <HeaderStyle Font-Size="10pt" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
                    ForeColor="White" BackColor="#006699" />
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="Id" HeaderText="Column ID"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Id" HeaderText="&lt;input type=&quot;checkbox&quot; name=&quot;chkCheckedMain&quot; onclick=&quot;CheckAll(this, 'chkChecked');&quot;&gt;"
                        DataFormatString="&lt;input type=&quot;checkbox&quot; name=&quot;chkChecked&quot; value=&quot;{0}&quot;&gt;">
                        <HeaderStyle Width="25px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Rank" HeaderText="Rank">
                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Label" HeaderText="Question Text"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Required">
                        <ItemStyle HorizontalAlign="Center" Width="45px" />
                        <ItemTemplate>
                            <asp:Image runat="server" ImageAlign="AbsMiddle" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("IsRequired")) %>'
                                ID="Image1" NAME="Image1" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Horizontal">
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Image runat="server" ImageAlign="AbsMiddle" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("IsHorizontal")) %>'
                                ID="Image2" NAME="Image1" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Choices">
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <asp:ImageButton runat="server" CommandName="items" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                                ID="Imagebutton2" NAME="Imagebutton1" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Edit">
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <asp:ImageButton runat="server" CommandName="edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                ID="Imagebutton1" NAME="Imagebutton1" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages">
                </PagerStyle>
            </asp:DataGrid>
        </td>
    </tr>
</table>
