<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebSitesControl.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Controls.WebSitesControl" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>

<div class="control-box no-bottom-margin">
    <div>
        <asp:Button ID="cmdAdd" CssClass="btn btn-default btn-sm" runat="server" Text="Create" OnClick="cmdAdd_Click" />
        <asp:Button ID="cmdDelete" CssClass="btn btn-default btn-sm" runat="server" Text="Delete" OnClick="cmdDelete_Click"
            OnClientClick="return confirm('Are you sure you want to delete?');" />
        <div class="pull-right">
            <asp:Button ID="cmdMove" CssClass="btn btn-default btn-sm" runat="server" Text="Move To:" OnClick="cmdMove_Click" />
            <asp:DropDownList ID="cboSites" CssClass="input" runat="server" />
            <asp:Button ID="cmdGO" runat="server" CssClass="btn btn-default btn-sm" Text="GO" OnClick="cmdGO_Click" />
        </div>
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-borderless" AllowPaging="True" AllowSorting="True"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" AutoGenerateColumns="False"
        Width="100%" PageSize="15" GridLines="None" OnRowCommand="GridView1_RowCommand">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField Visible="False" DataField="SiteId" HeaderText="ID" />
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
            <asp:TemplateField HeaderText="Actions" Visible="false">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton1" AlternateText="Edit Site" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                        AlternateText="Delete" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you sure you want to delete this site?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />--%>
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/Central/Site/WebChildSites/?SiteId={0}" DataTextField="Name"
                HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:HyperLinkField DataNavigateUrlFields="RelativeUrl" DataTextField="RelativeUrl"
                HeaderText="URL" SortExpression="RelativeUrl" Target="_blank" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Active" SortExpression="Active">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                        ID="Image1" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
</div>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetWebSites"
    TypeName="WCMS.WebSystem.WebParts.Central.Controls.WebSitesControl">
    <SelectParameters>
        <asp:QueryStringParameter Name="siteId" QueryStringField="SiteId" Type="Int32" DefaultValue="-1" />
    </SelectParameters>
</asp:ObjectDataSource>
