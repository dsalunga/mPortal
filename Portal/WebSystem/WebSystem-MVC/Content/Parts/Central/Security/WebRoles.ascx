<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebRoles.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.WebRolesController" %>

<h1 class="central page-header">
    Roles
</h1>
<div class="control-box">
    <div>
        New role name: &nbsp;
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <asp:Button ID="cmdAdd" CssClass="btn btn-default" runat="server" Text="Add Role" OnClick="cmdAdd_Click" />
    </div>
</div>

<asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
    GridLines="None" Width="100%" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand"
    PageSize="15">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Actions">
            <HeaderStyle HorizontalAlign="Center" Width="55px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    ID="Imagebutton4" AlternateText="Properties" CommandArgument='<%# Eval("Id") %>' />
                <!--
                            <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                                CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
                            <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Manage_Permissions"
                                AlternateText="Permissions" ImageUrl="~/Content/Assets/Images/Common/lock.gif" CommandArgument='<%# Eval("Id") %>' />
                            -->
                <asp:ImageButton ID="ImageButton3" runat="server" CommandName="View_Users" AlternateText="View Users"
                    ImageUrl="~/Content/Assets/Images/TreeView/u.gif" CommandArgument='<%# Eval("Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
    </Columns>
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <AlternatingRowStyle BackColor="White" />
    <PagerSettings PageButtonCount="25" />
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="WCMS.WebSystem.WebParts.Central.WebRolesController"></asp:ObjectDataSource>
