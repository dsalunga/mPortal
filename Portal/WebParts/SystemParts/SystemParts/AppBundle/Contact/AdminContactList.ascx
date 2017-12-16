<%@ Control Language="c#" Inherits="WCMS.WebSystem.WebParts.Contact.AdminContactList"
    CodeBehind="AdminContactList.ascx.cs" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<div class="control-box">
    <div>
        <asp:Button ID="cmdAdd" CssClass="btn btn-default" Text="Add" runat="server"
            OnClick="cmdAdd_Click" />
        <asp:Button ID="cmdDelete" CssClass="btn btn-default" OnClientClick="return confirm('Are you sure you want to delete the selected items?');"
            Width="80px" Text="Delete" runat="server" OnClick="cmdDelete_Click" />
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
        CellPadding="4" DataSourceID="ObjectDataSourceContacts" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="ContactId" OnRowCommand="GridView1_RowCommand">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle HorizontalAlign="Left" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
                </HeaderTemplate>
                <ItemTemplate>
                    <input type='checkbox' value='<%# Eval("ContactId")%>' name='chkChecked' />
                </ItemTemplate>
                <HeaderStyle Width="15px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        AlternateText="Edit" CommandArgument='<%# Eval("ContactId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Active">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image runat="server" ImageUrl='<%# WebHelper.SetStateImageInt((int)Eval("IsActive")) %>'
                        ID="Image1" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Name"
                SortExpression="Name" />
            <asp:BoundField DataField="Email" HeaderStyle-HorizontalAlign="Left" HeaderText="E-mail Address"
                SortExpression="Email" />
            <asp:BoundField DataField="Subject" HeaderStyle-HorizontalAlign="Left" HeaderText="Subject"
                SortExpression="Subject" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSourceContacts" runat="server" SelectMethod="GetContacts"
        TypeName="WCMS.WebSystem.WebParts.Contact.AdminContactList"></asp:ObjectDataSource>
</div>