<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebThemes.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Template.WebThemesViewController" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>

<h1 class="central page-header">
    Themes
</h1>
<div class="control-box">
    <div>
        <asp:Button ID="cmdAdd" runat="server" Text="Add" OnClick="cmdAdd_Click" CssClass="btn btn-default btn-sm" />
        <asp:Button ID="cmdParseDirectory" runat="server" Text="Parse Themes" CssClass="btn btn-default btn-sm"
            OnClick="cmdParseDirectory_Click" />
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
        PageSize="20" CssClass="table table-borderless">
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
                    <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
                </HeaderTemplate>
                <ItemTemplate>
                    <input type='checkbox' value='<%# Eval("Id") %>' name='chkChecked' />
                </ItemTemplate>
                <HeaderStyle Width="15px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions" Visible="false">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' /><asp:ImageButton ID="ImageButton2"
                            runat="server" CommandName="View-Templates" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                            AlternateText="View Templates" ToolTip="View Templates" CommandArgument='<%# Eval("Id") %>' Visible="false" /><asp:ImageButton ID="ImageButton4"
                                runat="server" CommandName="DeleteItem" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                                AlternateText="Delete" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="NameUrl" DataTextField="Name" HeaderText="Name"
                SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" Visible="false" />
            <asp:BoundField DataField="Identity" HeaderText="Folder Name" SortExpression="Identity"
                HeaderStyle-HorizontalAlign="Left" />
            <%--<asp:BoundField DataField="TempalateName" HeaderText="Default Template" SortExpression="TempalateName"
                        HeaderStyle-HorizontalAlign="Left" />--%>
            <%--<asp:BoundField DataField="ThemeName" HeaderText="Default Skin" SortExpression="ThemeName"
                        HeaderStyle-HorizontalAlign="Left" />--%>
            <asp:BoundField DataField="ParentName" HeaderText="Parent" SortExpression="ParentName"
                HeaderStyle-HorizontalAlign="Left" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Central.Template.WebThemesViewController"></asp:ObjectDataSource>
</div>
