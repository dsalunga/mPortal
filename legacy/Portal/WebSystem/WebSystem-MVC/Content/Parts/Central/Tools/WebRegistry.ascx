<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebRegistry.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.WebRegistryController" %>
<h1 class="central page-header">
    Registry
</h1>
<div style="padding: 5px 0 5px 0">
    <span runat="server" id="lblBreadcrumb"></span>
</div>
<div class="control-box">
    <div>
        <asp:Button ID="cmdAddFull" CssClass="btn btn-default" runat="server" Text="New Entry"
            OnClick="cmdAddFull_Click" />&nbsp;
            <asp:Button ID="cmdUp" runat="server" CssClass="btn btn-default" Text="Up" OnClick="cmdUp_Click" />
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
    CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
    Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
    PageSize="35">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="">
            <HeaderStyle HorizontalAlign="Center" Width="55px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <%--<asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                ID="Imagebutton1" AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />--%><asp:ImageButton
                                    ID="ImageButton3" runat="server" CommandName="View_ChildNodes" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                                    AlternateText="Children" ToolTip="Children" CommandArgument='<%# Eval("Id") %>' /><asp:ImageButton
                                        ID="ImageButton2" runat="server" CommandName="Custom_Delete" CommandArgument='<%# Eval("Id") %>'
                                        ImageUrl="~/Content/Assets/Images/Common/ico_x.gif" AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:HyperLinkField DataNavigateUrlFields="NameUrl" DataTextField="Key" HeaderText="Key"
            SortExpression="Key" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:HyperLinkField>
        <asp:BoundField DataField="Key" HeaderText="Key" SortExpression="Key" HeaderStyle-HorizontalAlign="Left" Visible="false">
            <ItemStyle VerticalAlign="Middle" />
        </asp:BoundField>
        <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" HeaderStyle-HorizontalAlign="Left" />
    </Columns>
    <PagerSettings PageButtonCount="25" />
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.WebParts.Central.Tools.WebRegistryController">
    <SelectParameters>
        <asp:QueryStringParameter Name="parentId" QueryStringField="ParentId" Type="Int32"
            DefaultValue="-1" ConvertEmptyStringToNull="true" />
    </SelectParameters>
</asp:ObjectDataSource>
