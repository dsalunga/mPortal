<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestManager.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.RequestManager" %>
<div class="integration-request-manager">
    <asp:GridView ID="GridView1" CssClass="table table-borderless image-noscale" runat="server" AllowSorting="True"
        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1"
        ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand"
        AllowPaging="True" PageSize="25" EmptyDataText="There are no pending requests.">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="">
                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                <ItemStyle HorizontalAlign="Center" Width="70px" />
                <ItemTemplate>
                    <a href='<%# Eval("UserProfileUrl") %>' title="View details">
                        <img src='<%# Eval("PhotoPath") %>' width="64" /></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserName" ItemStyle-Font-Bold="true" HeaderText="User Name"
                SortExpression="UserName" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Font-Bold="True" />
            </asp:BoundField>
            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName"
                HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="GroupName" HeaderText="Requested Group" SortExpression="GroupName"
                HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Actions">
                <HeaderStyle HorizontalAlign="center" Width="50px" />
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <asp:Button runat="server" Text="Approve" CssClass="btn btn-success btn-sm" CommandName="Approve"
                        CommandArgument='<%# Eval("Id") %>' />
                    <br />
                    <asp:Button runat="server" CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Are you sure you want to REJECT this request?');"
                        Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#F5F5E6" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#5C5247" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Profile.RequestManager"></asp:ObjectDataSource>
</div>
