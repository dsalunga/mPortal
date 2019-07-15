<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigExternalMembers.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.ConfigExternalMembers" %>
<div class="control-box">
    <div>
        <asp:Button ID="cmdSync" runat="server" Text="Sync" CssClass="btn btn-default btn-sm" OnClick="cmdSync_Click" />
        <div class="pull-right">
            <asp:TextBox ID="txtSearch" Columns="25" CssClass="input" runat="server" ClientIDMode="Static"></asp:TextBox>
            <asp:Button ID="cmdSearch" runat="server" CssClass="btn btn-default btn-sm" Text="Search"
                OnClick="cmdSearch_Click" ClientIDMode="Static" />&nbsp;<asp:Button
                    ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" CssClass="btn btn-default btn-sm" />
        </div>
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-borderless"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
        PageSize="50">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Edit">
                <HeaderStyle HorizontalAlign="center" Width="20px" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton4" AlternateText="Edit Item" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton Visible="false" ID="ImageButton1" CommandName="Custom_Delete" runat="server"
                        ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ExternalIDNo" HeaderText="Group ID" SortExpression="ExternalIDNo"
                HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="TemporaryIDNo" HeaderText="Temporary ID" SortExpression="TemporaryIDNo"
                HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName"
                HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.Apps.Integration.ConfigExternalMembers" EnablePaging="True"
        SelectCountMethod="SelectCount" SortParameterName="sortBy">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtSearch" Name="keyword" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        WCMS.Form.SetDefaultSubmit($("#txtSearch"), $("#cmdSearch"));
    });
</script>
