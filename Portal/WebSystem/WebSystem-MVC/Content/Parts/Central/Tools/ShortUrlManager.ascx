<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShortUrlManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.ShortUrlManager" %>
<h1 class="central page-header">
    Short URL Manager
</h1>
<div class="min-bottom-margin">
    <asp:Button ID="cmdCreate" CssClass="btn btn-default" runat="server" Text="New URL"
        OnClick="cmdCreate_Click" />
</div>
<div class="table-responsive">
<asp:GridView ID="GridView1" runat="server" CssClass="table table-borderless" AllowPaging="True" AllowSorting="True"
    CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
    Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
    PageSize="50" EmptyDataText="No data available.">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Actions">
            <HeaderStyle HorizontalAlign="Center" Width="38px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    ID="Imagebutton1" AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" CommandArgument='<%# Eval("Id") %>'
                    ImageUrl="~/Content/Assets/Images/Common/ico_x.gif" AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:HyperLinkField DataNavigateUrlFields="Name" DataNavigateUrlFormatString="/{0}/"
            DataTextField="Name" DataTextFormatString="/{0}/" HeaderText="Short URL"
            SortExpression="Name" Target="_blank" HeaderStyle-HorizontalAlign="Left" />
        <asp:HyperLinkField DataNavigateUrlFields="PageUrl" DataTextField="PageName" HeaderText="Page URL"
            SortExpression="PageUrl" Target="_blank" HeaderStyle-HorizontalAlign="Left" />
    </Columns>
    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
        PageButtonCount="25" />
</asp:GridView>
    </div>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.WebParts.Central.Tools.ShortUrlManager"></asp:ObjectDataSource>

