<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPagesControl.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Controls.WebPagesControl" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>

<div class="control-box no-bottom-margin">
    <div>
        <asp:Button ID="cmdAddFull" CssClass="btn btn-default btn-sm" runat="server" Text="Create" OnClick="cmdAddFull_Click" />
        <asp:Button ID="cmdDelete" CssClass="btn btn-default btn-sm" runat="server" Text="Delete"
            OnClick="cmdDelete_Click" OnClientClick="return confirm('Are you sure you want to delete?');" />
        <div class="pull-right">
            <asp:Button ID="cmdMove" CssClass="btn btn-default btn-sm" runat="server" Text="Move To" OnClick="cmdMove_Click" />
            <asp:DropDownList ID="cboPages" runat="server" CssClass="input">
            </asp:DropDownList>
            <asp:Button ID="cmdGO" CssClass="btn btn-default btn-sm" runat="server" Text="GO" OnClick="cmdGO_Click" />
        </div>
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-borderless" AllowPaging="False" AllowSorting="True"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" AutoGenerateColumns="False"
        Width="100%" GridLines="None" OnRowCommand="GridView1_RowCommand" PageSize="15">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="checkbox" value='<%# Eval("Id") %>' name="chkChecked">
                </ItemTemplate>
                <HeaderStyle Width="15px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions" Visible="false">
                <HeaderStyle HorizontalAlign="Center" Width="62px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        AlternateText="Edit Page" ToolTip="Edit Page" CommandArgument='<%# Eval("Id") %>' />&nbsp;<asp:ImageButton
                            ID="ImageButton2" runat="server" CommandName="cms" ImageUrl="~/Content/Assets/Images/Common/Objects.gif"
                            AlternateText="Configure Page" ToolTip="Configure Page" CommandArgument='<%# Eval("Id") %>' />&nbsp;<asp:ImageButton
                                ID="ImageButton3" runat="server" CommandName="Custom_Delete" CommandArgument='<%# Eval("Id") %>'
                                AlternateText="Delete Page" ToolTip="Delete Page" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                                OnClientClick="return confirm('Are you sure you want to delete this item?')" />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />--%>
            <asp:HyperLinkField DataNavigateUrlFields="Id,SiteId" DataTextField="Name"
                HeaderText="Name" SortExpression="Name" DataNavigateUrlFormatString="/Central/Site/WebChildPages/?SiteId={1}&PageId={0}" HeaderStyle-HorizontalAlign="Left" />
            <asp:HyperLinkField DataNavigateUrlFields="RelativeUrl" DataTextField="RelativeUrl"
                HeaderText="URL" SortExpression="RelativeUrl" Target="_blank" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="WebPartName" HeaderText="WebPart" SortExpression="WebPartName"
                HeaderStyle-HorizontalAlign="Left" HtmlEncode="false" />
            <asp:TemplateField HeaderText="Active" SortExpression="Active">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ToolTip="Toggle Status" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                        ID="Image1" CommandName="toggle_active" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
</div>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetWebPages"
    TypeName="WCMS.WebSystem.WebParts.Central.Controls.WebPagesControl">
    <SelectParameters>
        <asp:QueryStringParameter Name="siteId" QueryStringField="SiteId" Type="Int32" DefaultValue="-1" />
        <asp:QueryStringParameter Name="parentId" QueryStringField="PageId" Type="Int32"
            DefaultValue="-1" />
    </SelectParameters>
</asp:ObjectDataSource>
