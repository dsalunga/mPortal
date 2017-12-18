<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PerformanceLogManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.PerformanceLogManagerView" %>
<div class="control-box">
    <div>
        <asp:Button ID="cmdDownload" CssClass="btn btn-default" runat="server" Text="Download" Width="85px"
            OnClick="cmdDownload_Click" />
        <asp:Button ID="cmdAddSummary" runat="server" CssClass="btn btn-default" Text="Add Summary"
            OnClick="cmdAddSummary_Click" />
        <asp:Button ID="cmdClear" runat="server" Width="75px" CssClass="btn btn-default" Text="Clear"
            OnClick="cmdClear_Click" />
        <div class="pull-right">
            <asp:Button ID="cmdToggle" runat="server" CssClass="btn btn-default" Text="Debug On" OnClick="cmdToggle_Click" />
        </div>
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-borderless" AllowPaging="True" AllowSorting="True"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
        PageSize="30">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle CssClass="table-pager" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Content" HtmlEncode="false" SortExpression="Content" HeaderText="Content"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="LogDateTime" SortExpression="LogDateTime" HeaderText="Log Time"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Duration" SortExpression="Duration" HeaderText="Duration"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:HyperLinkField DataNavigateUrlFields="PageUrl" DataTextField="PageName" HeaderText="Page Url"
                SortExpression="PageUrl" Target="_blank" HeaderStyle-HorizontalAlign="Left" />
        </Columns>
        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
            PageButtonCount="25" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Central.Tools.PerformanceLogManagerView"></asp:ObjectDataSource>
</div>
