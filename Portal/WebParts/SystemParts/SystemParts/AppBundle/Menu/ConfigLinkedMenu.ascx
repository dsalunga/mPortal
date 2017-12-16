<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigLinkedMenu.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Menu.ConfigLinkedMenu" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<div class="Header" runat="server" id="lblHeader">
    Menu Items for this page
</div>
<br />
<div class="control-box">
    <div>
        <asp:Button ID="cmdAdd" CssClass="btn btn-default" runat="server" Text="Add"
            OnClick="cmdAdd_Click" />
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" AutoGenerateColumns="False"
        Width="100%" PageSize="15" GridLines="None" OnRowCommand="GridView1_RowCommand">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Actions">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" CommandName="Action_Edit" ToolTip="Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton1" AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' /><asp:ImageButton
                            runat="server" CommandName="Action_Delete" OnClientClick="return confirm('Delete this item?');"
                            ImageUrl="~/Content/Assets/Images/Common/ico_x2.gif" ID="Imagebutton2" AlternateText="Delete"
                            ToolTip="Delete" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Text" HeaderText="Item" SortExpression="Text" HeaderStyle-HorizontalAlign="Left"
                HtmlEncode="false" />
            <asp:BoundField DataField="MenuName" HeaderText="Menu" SortExpression="MenuName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Active">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("IsActive")) %>'
                        ID="Image2" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Menu.ConfigLinkedMenu">
        <SelectParameters>
            <asp:QueryStringParameter Name="pageId" QueryStringField="PageId" Type="Int32" DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>