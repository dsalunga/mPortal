<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataStoreManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.DataStoreEditor" %>
<style type="text/css">
    .HeaderStyleLeft th {
        text-align: left;
    }

    input, textarea {
        font-family: Verdana, Arial;
        font-size: 12px; /* color: #333333; */ /* filter: alpha(opacity=50); */
    }

    th.HeaderStyleCenter {
        text-align: center;
    }
</style>
<h3>Web Object Manager
</h3>
<div class="control-box">
    <div>
        <asp:Button ID="cmdSynch" CssClass="btn btn-default btn-sm" runat="server" Text="Register"
            OnClick="cmdSynch_Click" ToolTip="Registers all unregistered Objects" />&nbsp;<asp:Button
                ID="cmdUpdateLastRecords" CssClass="btn btn-default btn-sm" runat="server" OnClick="cmdUpdateLastRecords_Click"
                Text="Update Last Records" ToolTip="Synchronize table containing IDs of all tables for insert operation" />
        <div class="pull-right">
            <div class="btn-group" role="group" aria-label="Backup and Restore">
                <asp:Button ID="cmdLoadCache" CssClass="btn btn-default btn-sm" runat="server" OnClick="cmdLoadCache_Click" Text="Load Cache" />
                <asp:Button ID="cmdUnloadCache" CssClass="btn btn-default btn-sm" runat="server" Text="Unload Cache" OnClick="cmdUnloadCache_Click" />
            </div>
            &nbsp;
        <asp:Button ID="cmdCleanUp" OnClientClick="return confirm('Are you sure you want to perform a clean-up to all objects?');"
            CssClass="btn btn-default btn-sm" runat="server" Text="Clean-Up" ToolTip="Deletes unnessary or orphan items"
            OnClick="cmdCleanUp_Click" />
            &nbsp;<asp:Button ID="cmdReset" runat="server" Text="System Reset" CssClass="btn btn-default btn-sm" OnClick="cmdReset_Click"
                ToolTip="Resets all sessions and reloads the application domain" />
        </div>
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" CssClass="table table-borderless table-condensed"
    CellPadding="4" DataSourceID="sourceHeaders" ForeColor="#333333" GridLines="None"
    Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
    PageSize="24">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#507CD1" CssClass="HeaderStyleLeft" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField>
            <HeaderStyle CssClass="HeaderStyleCenter" Width="20px" />
            <HeaderTemplate>
                <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
            </HeaderTemplate>
            <ItemTemplate>
                <input type="checkbox" value='<%# Eval("Id") %>' name="chkChecked" />
            </ItemTemplate>
            <HeaderStyle Width="15px" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="">
            <HeaderStyle CssClass="HeaderStyleCenter" Width="50px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:ImageButton ID="ImageButtonEdit" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' /><asp:ImageButton ID="ImageButtonDelete" runat="server" CommandName="Custom_Delete"
                        ImageUrl="~/Content/Assets/Images/Common/ico_x.gif" AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                        CommandArgument='<%# Eval("Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:HyperLinkField DataNavigateUrlFields="NameUrl" DataTextField="Name" HeaderText="Name"
            SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="IdentityColumn" HeaderText="Identity" SortExpression="IdentityColumn" />
        <asp:BoundField DataField="Count" HeaderText="Size" SortExpression="Count" />
        <asp:BoundField DataField="LastRecordId" HeaderText="Last Record" SortExpression="LastRecordId" />
        <asp:BoundField DataField="CacheType" HeaderText="Cache" SortExpression="CacheType" />
        <asp:BoundField DataField="ManagerName" HeaderText="Manager" SortExpression="ManagerName" Visible="false" />
        <asp:BoundField DataField="DateModified" HeaderText="Modified" SortExpression="DateModified" />
        <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
        <asp:BoundField DataField="CacheStatus" HeaderText="Cache Status" SortExpression="CacheStatus" />
        <asp:BoundField DataField="CachedCount" HeaderText="Cached" SortExpression="CachedCount" />
    </Columns>
</asp:GridView>
<asp:ObjectDataSource ID="sourceHeaders" runat="server" SelectMethod="Select" TypeName="WCMS.WebSystem.WebParts.Central.DataStoreEditor"></asp:ObjectDataSource>
<br />
<br />
<div id="divMsgs" runat="server" style="padding: 5px;">
</div>

