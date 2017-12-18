<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPermissions.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.WebPermissionsController" %>

<h1 class="central page-header">
    Web Permissions
</h1>
<div class="control-box">
    <div>
        <asp:Button ID="cmdDelete" CssClass="btn btn-default" OnClientClick="return confirm('Delete selected items?');" runat="server" Text="Delete" OnClick="cmdDelete_Click" />
        <asp:Button ID="cmdDone" runat="server" Text="Back" OnClick="cmdDone_Click" CssClass="btn btn-default" />
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
    CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
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
        <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Name" SortExpression="Name" />
        <asp:BoundField DataField="IsSystem" HeaderStyle-HorizontalAlign="Left" HeaderText="Built-in" SortExpression="IsSystem" />
    </Columns>
    <PagerSettings PageButtonCount="25" />
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
    SelectMethod="Select" TypeName="WCMS.WebSystem.WebParts.Central.WebPermissionsController"></asp:ObjectDataSource>
