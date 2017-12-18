<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebParts.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.WebParts" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<h1 class="central page-header">Apps - Management
</h1>
<div runat="server" id="lblStatus" class="alert"></div>
<div id="rowControlBox" runat="server" class="control-box">
    <div>
        <asp:Button ID="cmdAdd" runat="server" CssClass="btn btn-default" Text="New Part"
            OnClick="cmdAdd_Click" />
        <div class="pull-right">
            <asp:Button ID="cmdParse" runat="server" CssClass="btn btn-default" Text="Sync Xml" OnClick="cmdParse_Click" />
        </div>
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" CssClass="table table-borderless table-condensed" AllowSorting="True" CellPadding="4" ForeColor="#333333"
    GridLines="None" Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
    PageSize="24" AllowPaging="False" DataSourceID="ObjectDataSource1">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Actions" Visible="false">
            <HeaderStyle HorizontalAlign="Center" Width="40px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:ImageButton ID="ImageButton5" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    AlternateText="Edit Web Part" CommandArgument='<%# Eval("Id") %>' />
                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                    AlternateText="Delete" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" />
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />--%>
        <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/Central/Part/Management/?PartId={0}" DataTextField="Name"
            HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Identity" HeaderText="Folder" SortExpression="Identity" HeaderStyle-HorizontalAlign="Left" />
        <asp:TemplateField HeaderText="Active" SortExpression="Active">
            <HeaderStyle HorizontalAlign="Center" Width="30px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:ImageButton runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                    ID="Image1" AlternateText="Toggle Active State" CommandName="Toggle_Active" CommandArgument='<%# Eval("Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.WebParts.Central.WebParts"></asp:ObjectDataSource>
