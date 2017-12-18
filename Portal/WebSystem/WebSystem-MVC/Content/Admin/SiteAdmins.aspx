<%@ Page Language="c#" Inherits="des.Web.cmsadmin.Users"
    MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Codebehind="SiteAdmins.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="Header">
                Site Owners</td>
        </tr>
        <tr>
            <td class="ControlBox" style="width: 910px">
                <asp:Button ID="cmdAdd" runat="server" Width="85px" Text="Add" OnClick="cmdAdd_Click"
                    Height="30px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
                    Width="100%" AutoGenerateColumns="False" DataKeyNames="UserID" OnRowCommand="GridView1_RowCommand">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField Visible="False">
                            <HeaderTemplate>
                                <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type='checkbox' value='<%# Eval("UserID")%>' name='chkChecked' />
                            </ItemTemplate>
                            <HeaderStyle Width="15px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="cmdEditItem" runat="server" CommandName="edit_item" ImageUrl="~/Admin/Images/ico_edit.gif"
                                    AlternateText="Edit" CommandArgument='<%# Eval("UserName") %>' />
                                <asp:ImageButton ID="cmdSitePermission" runat="server" CommandName="permission" ImageUrl="~/Admin/Images/tv/ws.gif"
                                    AlternateText="Sites" CommandArgument='<%# Eval("UserID") %>' />
                                <asp:ImageButton ID="cmdWebModules" runat="server" CommandName="content" ImageUrl="~/Admin/Images/tv/mo.gif"
                                    AlternateText="Modules" CommandArgument='<%# Eval("UserID") %>' />
                                <asp:ImageButton ID="cmdDelete" runat="server" CommandName="Custom_Delete" ImageUrl="~/Admin/Images/ico_x.gif"
                                    AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete?');"
                                    CommandArgument='<%# Eval("UserID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approved" SortExpression="IsApproved">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Image ID="Image2" runat="server" ImageUrl='<%# des.WebHelper.SetStateImage(Eval("IsApproved")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Locked" SortExpression="IsLockedOut">
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Image ID="Image3" runat="server" ImageUrl='<%# des.WebHelper.SetStateImage(Eval("IsLockedOut")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                        <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="LastLoginDate" HeaderText="Last Login" SortExpression="LastLoginDate" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetData" TypeName="CMSDALTableAdapters.UsersAdapter">
                    <SelectParameters>
                        <asp:Parameter Name="UserID" Type="String" ConvertEmptyStringToNull="true" />
                        <asp:Parameter Name="RoleName" Type="String" DefaultValue="Site Owners" />
                        <asp:Parameter Name="Username" Type="String" ConvertEmptyStringToNull="true" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
