<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminRemoteLibraryManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.FileManager.AdminRemoteLibraryManager" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<div class="control-box">
    <div>
        <asp:Button ID="cmdAdd" CssClass="btn btn-default" runat="server" Text="New Library"
            OnClick="cmdAdd_Click" />
        <div class="pull-right">
            <asp:Button ID="cmdForceExecute" CssClass="btn btn-default" runat="server" Text="Execute Indexer"
                OnClick="cmdForceExecute_Click" />
        </div>
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Id" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
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
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                HtmlEncode="false" />
            <asp:BoundField DataField="SourceType" HeaderText="Source" SortExpression="SourceType"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="BaseAddress" HeaderText="Address" SortExpression="BaseAddress"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password"
                HeaderStyle-HorizontalAlign="Left" Visible="false" />
            <asp:BoundField DataField="DisplayBaseAddress" HeaderText="Display Address" SortExpression="DisplayBaseAddress"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="LastIndexDate" HeaderText="Last Index" SortExpression="LastIndexDate"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Size" HeaderText="Size" SortExpression="Size"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Active">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                        ID="Image2" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.FileManager.AdminRemoteLibraryManager"></asp:ObjectDataSource>
</div>
<br />
<span id="lblStatus" runat="server" style="color: Red;" enableviewstate="false"></span>