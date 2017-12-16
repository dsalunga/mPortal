<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminNewsletter.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Newsletter.AdminNewsletter" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>

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

        //query.Set("Key", string.Format("{0}-{1}", IntegrationConstants.ASOPCompetition, id));

        return query.BuildQuery();
    }
</script>

<div class="control-box">
    <div>
    <asp:Button ID="cmdDelete" runat="server" Text="Delete" CssClass="btn" />
    <div class="pull-right">
        <asp:TextBox ID="txtSearch" Columns="25" runat="server"></asp:TextBox>
        <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click" CssClass="btn" />&nbsp;<asp:Button
            ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" CssClass="btn" />
    </div>
        </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
    GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
    PageSize="15">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
        <asp:TemplateField>
            <HeaderTemplate>
                <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');" />
            </HeaderTemplate>
            <ItemTemplate>
                <input type='checkbox' value='<%# Eval("Id") %>' name='chkChecked' />
            </ItemTemplate>
            <HeaderStyle Width="15px" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"
            HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="SubscribeDate" HeaderText="Date" SortExpression="SubscribeDate"
            HeaderStyle-HorizontalAlign="Left" />
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
    TypeName="WCMS.WebSystem.WebParts.Newsletter.AdminNewsletter">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
    </SelectParameters>
</asp:ObjectDataSource>
<br />
<br />
<span id="lblStatus" runat="server" style="color: Red"></span>