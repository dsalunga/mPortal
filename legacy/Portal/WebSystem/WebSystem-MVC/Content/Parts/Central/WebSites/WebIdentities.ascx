<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebIdentities.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebIdentities" %>
<%--<h1 class="central page-header" id="tdHeader" runat="server">
    Web Site Identities
</h1>--%>
<div class="control-box no-bottom-margin">
    <div>
        <asp:Button ID="cmdAddFull" runat="server" Text="New Binding" CssClass="btn btn-default btn-sm" OnClick="cmdAddFull_Click" />
        <asp:Button ID="cmdDone" runat="server" Text="Done" CssClass="btn btn-default"
            OnClick="cmdDone_Click" />
        <div class="pull-right">
            <asp:DropDownList ID="cboSites" runat="server" CssClass="input" AutoPostBack="true" OnSelectedIndexChanged="cboSites_SelectedIndexChanged">
                <asp:ListItem Text="" Value="-1"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-borderless"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
        PageSize="40">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Actions">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton1" AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" CommandArgument='<%# Eval("Id") %>'
                        ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif" AlternateText="Delete" ToolTip="Delete this item" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="HostName" HeaderText="Host Name" SortExpression="HostName" />
            <asp:BoundField DataField="Protocol" HeaderText="Protocol" SortExpression="Protocol" />
            <asp:BoundField DataField="Port" HeaderText="Port" SortExpression="Port" />
            <asp:BoundField DataField="IPAddress" HeaderText="IP Address" SortExpression="IPAddress" />
            <asp:BoundField DataField="UrlPath" HeaderText="URL Path" SortExpression="UrlPath" />
            <asp:BoundField DataField="RedirectUrl" HeaderText="Redirect URL" SortExpression="RedirectUrl" />
            <asp:BoundField DataField="SiteName" HeaderText="Site" SortExpression="SiteName" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
        SelectMethod="Select" TypeName="WCMS.WebSystem.WebParts.Central.WebSites.WebIdentities">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboSites" DefaultValue="-1" PropertyName="SelectedValue" Name="siteId" />
            <%--<asp:QueryStringParameter Name="siteId" QueryStringField="SiteId" Type="Int32" />--%>
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
