<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Menu.AdminMenu08"
    CodeBehind="AdminMenu.ascx.cs" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="Controls/AdminTabControl.ascx" TagName="AdminTabControl" TagPrefix="uc2" %>
<uc2:AdminTabControl ID="AdminTabControl1" runat="server" />
<div class="control-box no-bottom-margin">
    <div>
        <asp:Button ID="cmdNew" CssClass="btn btn-default btn-sm" runat="server" Text="New" OnClick="cmdNew_Click" />
        <asp:Button ID="cmdDelete" OnClientClick="return confirm('Are you sure you want to delete the selected items?');"
            CssClass="btn btn-default btn-sm" runat="server" Text="Delete" OnClick="cmdDelete_Click" />
        <div class="pull-right">
            <asp:DropDownList ID="cboSites" CssClass="input" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboSites_SelectedIndexChanged">
                <asp:ListItem Text="" Value="-2"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
</div>
<div class="table-responsive" style="clear: left">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" CssClass="table table-borderless"
        CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None"
        Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="GridView1_RowCommand"
        PageSize="15">
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
                    <input type='checkbox' value='<%# Eval("Id")%>' name='chkChecked' />
                </ItemTemplate>
                <HeaderStyle Width="15px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <HeaderStyle HorizontalAlign="Center" Width="18px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit_item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                    <%--<asp:ImageButton ID="ImageButton2" runat="server" CommandName="menu_items" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                        AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' Visible="false" />--%>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>--%>
            <asp:HyperLinkField DataNavigateUrlFields="NameUrl" DataTextField="Name" HeaderText="Name"
                SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="SiteName" HeaderText="Web Site" SortExpression="SiteName" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="DateCreated" HeaderText="Date Created" SortExpression="DateCreated"
                HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Active">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("IsActive")) %>'
                        ID="Image1" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetList"
        TypeName="WCMS.WebSystem.WebParts.Menu.AdminMenu08">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboSites" DefaultValue="-2" PropertyName="SelectedValue" Name="siteId" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
