<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebMasterPages.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebSites.WebMasterPagesController" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="../Controls/WebSiteTab.ascx" TagName="WebSiteTab" TagPrefix="uc1" %>
<uc1:WebSiteTab ID="WebSiteTab1" runat="server" />
<div class="control-box">
    <div>
        <asp:Button ID="cmdAddFull" runat="server" Text="Create" CssClass="btn btn-default" OnClick="cmdAddFull_Click" />
        <asp:Button ID="cmdDelete" runat="server" Text="Delete" OnClick="cmdDelete_Click" CssClass="btn btn-default"
            OnClientClick="return confirm('Are you sure you want to delete the selected items?');" />
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" CssClass="table table-borderless" runat="server" AllowPaging="True" AllowSorting="True"
        CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" AutoGenerateColumns="False"
        DataKeyNames="Id" OnRowCommand="GridView1_RowCommand" DataSourceID="ObjectDataSource1">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
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
            <asp:TemplateField HeaderText="Actions" Visible="false">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton1" ToolTip="Edit" AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" CommandArgument='<%# Eval("Id") %>'
                        AlternateText="Delete" ToolTip="Delete" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif" OnClientClick="return confirm('Are you sure you want to delete this item?')" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="NameUrl" DataTextField="Name" HeaderText="Name"
                SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
            <%--<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" Visible="false" />--%>
            <asp:BoundField DataField="TemplateName" HeaderText="Template" SortExpression="TemplateName" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Default" SortExpression="IsDefault">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("IsDefault")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="PageTemplateName" HeaderText="Template Name" SortExpression="PageTemplateName" />--%>
        </Columns>
    </asp:GridView>
</div>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetMasterPages"
    TypeName="WCMS.WebSystem.WebParts.Central.WebSites.WebMasterPagesController">
    <SelectParameters>
        <asp:QueryStringParameter Name="siteId" QueryStringField="SiteId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
