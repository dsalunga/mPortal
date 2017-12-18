<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebGlobalPolicy.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Security.WebGlobalPolicyController" %>
<h1 class="central page-header">
    Global Policy
</h1>
<div class="control-box">
    <div>
        <asp:Button ID="cmdNew" runat="server" Text="New Policy" CssClass="btn btn-default" />
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    CellPadding="4" DataKeyNames="Id" ForeColor="#333333" GridLines="None"
    Width="100%" OnRowCommand="GridView1_RowCommand" DataSourceID="ObjectDataSource1">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Actions">
            <HeaderStyle HorizontalAlign="center" Width="40px" />
            <ItemStyle HorizontalAlign="center" />
            <ItemTemplate>
                <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    ID="Imagebutton4" AlternateText="Edit Profile" CommandArgument='<%# Eval("Id") %>' />
                <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                    CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
    </Columns>
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <AlternatingRowStyle BackColor="White" />
    <PagerSettings PageButtonCount="25" />
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.WebParts.Central.Security.WebGlobalPolicyController"></asp:ObjectDataSource>
