<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebOffices.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Misc.WebOfficesController" %>
<div class="Header">
    Offices
            <br />
</div>
<div style="padding: 5px 0 5px 0">
    <span runat="server" id="lblBreadcrumb"></span>
</div>
<div class="control-box">
    <div>
        <asp:Button ID="cmdAddFull" CssClass="btn btn-default" runat="server" Text="New Office"
            OnClick="cmdAddFull_Click" />&nbsp;
            <asp:Button ID="cmdUp" runat="server" CssClass="btn btn-default" Text="Up" OnClick="cmdUp_Click" />
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-borderless"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" PageSize="15">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Actions">
                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton4" AlternateText="Properties" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="View_ChildNodes" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                        AlternateText="Children" ToolTip="Children" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" CommandArgument='<%# Eval("Id") %>'
                        ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif" AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="WCMS.WebSystem.WebParts.Central.Misc.WebOfficesController">
        <SelectParameters>
            <asp:QueryStringParameter Name="parentId" QueryStringField="ParentId" Type="Int32"
                DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
