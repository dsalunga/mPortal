<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="WCMS.WebSystem.Admin.PermissionController" Codebehind="WebObjectPermissions.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="Header">
                Object Permissions</td>
        </tr>
        <tr>
            <td class="ControlBox">
                <asp:Button ID="cmdDelete" runat="server" Text="Remove Permission" OnClick="cmdDelete_Click" Height="30px" />
                <asp:Button ID="cmdDone" runat="server" Text="Back" OnClick="cmdDone_Click" Width="85px" Height="30px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                    CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None"
                    AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
                    Width="100%" PageSize="15">
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
                                <input type='checkbox' value='<%# Eval("Id")%>' name='chkChecked' />
                            </ItemTemplate>
                            <HeaderStyle Width="15px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    </Columns>
                    <PagerSettings PageButtonCount="25" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="CMS.SELECT_RolePermissions" SelectCommandType="StoredProcedure"
                    CancelSelectOnNullParameter="True">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hidRecordId" Name="recordId" PropertyName="Value"
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="hidRecordId" runat="server" />
                <asp:HiddenField ID="hidObjectId" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <br />
                <br />
                <div class="big_bold">
                    Available Permissions</div>
            </td>
        </tr>
        <tr>
            <td class="ControlBox">
                <asp:Button ID="cmdInsert" runat="server" Text="Grant Permission" OnClick="cmdInsert_Click" Height="30px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                    CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None"
                    AutoGenerateColumns="False" DataKeyNames="Id" Width="100%" PageSize="15">
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
                                <input type="checkbox" name="chkCheckedMain2" onclick="CheckAll(this, 'chkChecked2');">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type='checkbox' value='<%# Eval("Id")%>' name='chkChecked2' />
                            </ItemTemplate>
                            <HeaderStyle Width="15px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    </Columns>
                    <PagerSettings PageButtonCount="25" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="CMS.SELECT_Permissions" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="True">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hiddenRoleID" Name="RoleName" PropertyName="Value"
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
