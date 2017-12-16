<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Photo.AdminAlbum" CodeBehind="AdminAlbum.ascx.cs" %>
<div class="control-box">
    <div>
        <asp:Button ID="btnAdd" Text="Add" CssClass="btn btn-default" runat="server"
            OnClick="btnAdd_Click" />
        <asp:Button ID="btnDelete" Text="Delete" CssClass="btn btn-default" runat="server"
            OnClick="btnDelete_Click" OnClientClick="return confirm('Delete selected items?');" />
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
        PageSize="35">
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
                    <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
                </HeaderTemplate>
                <ItemTemplate>
                    <input type='checkbox' value='<%# Eval("Id")%>' name='chkChecked' />
                </ItemTemplate>
                <HeaderStyle Width="15px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="view_pictures" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                        AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Preview">
                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <img src='<%# Eval("ThumbUrl") %>' width="80" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Title" HeaderText="Name" SortExpression="Title"
                HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="PhotoWidth" HeaderText="Photo Width" SortExpression="PhotoWidth"
                HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="PhotoHeight" HeaderText="Photo Height" SortExpression="PhotoHeight"
                HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="ImageFile" HeaderText="Thumbnail File" SortExpression="ImageFile"
                HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="DateModified" HeaderText="Modified" SortExpression="DateModified" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select" TypeName="WCMS.WebSystem.WebParts.Photo.AdminAlbum"></asp:ObjectDataSource>
</div>
<div>
    <asp:Label ID="lblNotify" runat="server" ForeColor="Red"></asp:Label>
</div>