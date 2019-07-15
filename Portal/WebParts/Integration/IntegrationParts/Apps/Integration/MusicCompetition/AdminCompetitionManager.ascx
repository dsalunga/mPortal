<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminCompetitionManager.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.MusicCompetition.AdminCompetitionManagerView" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.WebSystem.Apps.Integration" %>

<script runat="server">
    private WQuery query = null;

    public string BuildParametersUrl(int id)
    {
        if (query == null)
        {
            query = new WQuery(this);
            query.BasePath = CentralPages.WebParameters;
            query.SetSource(CentralPages.LoaderMain);
        }

        query.Set("Key", string.Format("{0}-{1}", IntegrationConstants.MusicCompetition, id));

        return query.BuildQuery();
    }
</script>

<div class="control-box no-bottom-margin">
    <div>
        <asp:Button ID="cmdNew" runat="server" Text="New Competition" CssClass="btn btn-default btn-sm" OnClick="cmdNew_Click" />
        <div class="pull-right">
            <asp:TextBox ID="txtSearch" Columns="25" runat="server" CssClass="input"></asp:TextBox>
            <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click" CssClass="btn btn-default btn-sm" />&nbsp;<asp:Button
                ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" CssClass="btn btn-default btn-sm" />
        </div>
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-borderless"
    CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
    GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
    PageSize="15">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="">
            <HeaderStyle HorizontalAlign="center" Width="18px" />
            <ItemStyle HorizontalAlign="center" />
            <ItemTemplate>
                <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    AlternateText="Edit Item" CommandArgument='<%# Eval("Id") %>' Visible="false" />
                <%--<a href="<%# BuildParametersUrl((int)Eval("Id")) %>" target="_blank">
                    <img src="/Content/Assets/Images/Common/ico_pages.gif" /></a>--%>
                <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                    CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"
            HeaderStyle-HorizontalAlign="Left" Visible="false" />
        <asp:HyperLinkField DataNavigateUrlFields="NameUrl" DataTextField="Name" HeaderText="Name"
            SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Judges" HeaderText="Judges" SortExpression="Judges"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="CompetitionDate" HeaderText="Event Date" SortExpression="CompetitionDate"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="ScoreLocked" ItemStyle-HorizontalAlign="Center" HeaderText="Judging"
            SortExpression="ScoreLocked" HeaderStyle-HorizontalAlign="Center" />
        <asp:BoundField DataField="VotingLocked" ItemStyle-HorizontalAlign="Center" HeaderText="Voting"
            SortExpression="VotingLocked" HeaderStyle-HorizontalAlign="Center" />
        <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id"
            HeaderStyle-HorizontalAlign="Left" />
    </Columns>
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <AlternatingRowStyle BackColor="White" />
    <PagerSettings PageButtonCount="25" />
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.Apps.MusicCompetition.AdminCompetitionManagerView">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
    </SelectParameters>
</asp:ObjectDataSource>
<br />
<br />
<span id="lblStatus" runat="server" style="color: Red"></span>