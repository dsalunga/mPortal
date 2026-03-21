<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Photo.CONTENTCMS_Category" CodeBehind="CCMS_Category.ascx.cs" %>
<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="viewGrid" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="ControlBox">
                    <asp:Button ID="btnAdd" Text="Add" CssClass="Command" runat="server" Width="75px"
                        OnClick="btnAdd_Click" />
                    <asp:Button ID="btnDelete" Text="Delete" CssClass="Command" runat="server" Width="75px"
                        OnClick="btnDelete_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                        CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None"
                        Width="100%" AutoGenerateColumns="False" DataKeyNames="CategoryID" OnRowCommand="GridView1_RowCommand"
                        PageSize="15">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input type='checkbox' value='<%# Eval("CategoryID")%>' name='chkChecked' />
                                </ItemTemplate>
                                <HeaderStyle Width="15px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actions">
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                        AlternateText="Edit" CommandArgument='<%# Eval("CategoryID") %>' />
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="view_pictures" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                                        AlternateText="Edit" CommandArgument='<%# Eval("CategoryID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Title" HeaderText="Album Name" SortExpression="Title"
                                HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ImageURL" HeaderText="Image" SortExpression="ImageURL"
                                HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="GalleryCategory_Get" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="viewDetails" runat="server">
        <table width="100%" border="0">
            <tr>
                <td style="width: 150px">
                    ID:
                </td>
                <td>
                    <asp:Literal ID="litID" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    Album Title:<asp:RequiredFieldValidator ID="rfvAlbum" runat="server" ControlToValidate="txtTitle"
                        ErrorMessage="Album Title" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" Width="450px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Folder Name:<asp:RequiredFieldValidator ID="rfvFolderName" runat="server" ControlToValidate="txtFolderName"
                        ErrorMessage="Folder Name" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtFolderName" runat="server" Width="350px"></asp:TextBox>&nbsp;
                    <em>(a suggestion will be provided based on the Title)</em>
                </td>
            </tr>
            <tr>
                <td>
                    Album Image:<asp:RequiredFieldValidator ID="rfvImageURL" runat="server" ControlToValidate="txtImageURL"
                        ErrorMessage="Album Image" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtImageURL" runat="server" Width="350px"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnImageURL" Text="Upload..." runat="server" Width="75px" CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td>
                    Album Image Width:
                </td>
                <td>
                    <asp:TextBox ID="txtWidth" runat="server" Columns="10" Text="250"></asp:TextBox>&nbsp;
                    <em>in pixels (250px default, -1 = use original)</em>
                </td>
            </tr>
            <tr>
                <td>
                    Photo Thumbnail Height:
                </td>
                <td>
                    <asp:TextBox ID="txtPhotoHeight" runat="server" Columns="10" Text="75"></asp:TextBox>&nbsp;
                    <em>in pixels (75px default)</em>
                </td>
            </tr>
            <tr>
                <td class="ControlBox" colspan="2">
                    <%--<asp:Button ID="btnSave" CssClass="Command" Text="Save" runat="server" Width="75px"
                        OnClick="btnSave_Click" />--%>
                    <asp:Button ID="btnUpdate" CssClass="Command" Text="Update" runat="server" Width="75px"
                        OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnCancel" CssClass="Command" Text="Cancel" runat="server" Width="75px"
                        OnClick="btnCancel_Click" CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblNotify" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>