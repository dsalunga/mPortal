<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Menu.AdminMenu08"
    CodeBehind="CCMS_Menu_08.ascx.cs" %>
<%@ Register Src="Controls/AdminTabControl.ascx" TagName="AdminTabControl" TagPrefix="uc2" %>
<uc2:AdminTabControl ID="AdminTabControl1" runat="server" />
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="ControlBox">
            <asp:Button ID="cmdNew" Width="75px" runat="server" Text="New" OnClick="cmdNew_Click" />
            <asp:Button ID="cmdDelete" OnClientClick="return confirm('Are you sure you want to delete the selected items?');"
                Width="75px" runat="server" Text="Delete" OnClick="cmdDelete_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
                Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
                PageSize="15">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type='checkbox' value='<%# Eval("Id")%>' name='chkChecked' />
                        </ItemTemplate>
                        <HeaderStyle Width="15px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <HeaderStyle HorizontalAlign="Center" Width="36px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                            <asp:ImageButton ID="ImageButton2" runat="server" CommandName="menu_items" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                                AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SiteName" HeaderText="Site" SortExpression="SiteName" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DateCreated" HeaderText="Date Created" SortExpression="DateCreated"
                        HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Active">
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%# WCMS.WebHelper.SetStateImageInt(Eval("IsActive")) %>'
                                ID="Image1" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetList"
                TypeName="WCMS.WebSystem.WebParts.Menu.AdminMenu08"></asp:ObjectDataSource>
        </td>
    </tr>
</table>
