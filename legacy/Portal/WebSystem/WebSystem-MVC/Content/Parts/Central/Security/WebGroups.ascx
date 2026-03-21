<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebGroups.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.WebGroupsController" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="../Controls/WebGroupTab.ascx" TagName="WebGroupTab" TagPrefix="uc1" %>

<uc1:WebGroupTab ID="WebGroupTab1" runat="server" />
<div class="control-box">
    <div>
        <asp:TextBox ID="txtName" runat="server" placeholder="New group name" CssClass="input" Columns="45"></asp:TextBox>
        <asp:Button ID="cmdAdd" runat="server" CssClass="btn btn-default btn-sm" Text="Add Group" OnClick="cmdAdd_Click" />
        &nbsp;
            <asp:Button ID="cmdUp" runat="server" CssClass="btn btn-default btn-sm" Text="Up" OnClick="cmdUp_Click" />
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%" CssClass="table table-borderless" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand"
        PageSize="25">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="">
                <HeaderStyle HorizontalAlign="Center" Width="55px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <%--<asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    ID="Imagebutton4" AlternateText="Properties" CommandArgument='<%# Eval("Id") %>' />--%>
                    <%--
                            <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Manage_Permissions"
                                AlternateText="Permissions" ImageUrl="~/Content/Assets/Images/Common/lock.gif" CommandArgument='<%# Eval("Id") %>' />
                    --%>
                    <asp:ImageButton ID="ImageButton3" runat="server" CommandName="View_Users" AlternateText="View Users"
                        ImageUrl="~/Content/Assets/Images/TreeView/u.gif" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="View_ChildNodes" ImageUrl="~/Content/Assets/Images/Common/ico_pages.gif"
                        AlternateText="Children" ToolTip="Children" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />--%>
            <asp:HyperLinkField DataNavigateUrlFields="NameUrl" DataTextField="Name" HeaderText="Name"
                SortExpression="Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:HyperLinkField DataNavigateUrlFields="OwnerId" DataNavigateUrlFormatString="/Central/Security/WebUserHome/?UserId={0}"
                DataTextField="Owner" HeaderText="Owner" SortExpression="Owner" Target="_blank"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="UserCount" HeaderText="Users" SortExpression="UserCount" />
            <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
            <asp:TemplateField HeaderText="Approval" SortExpression="RequireApproval">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ToolTip="Toggle Require Approval" runat="server" ImageUrl='<%# WebHelper.SetStateImage(Eval("RequireApproval")) %>'
                        ID="Image1" CommandName="Toggle_Approval" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="IsSystem" HeaderText="Built-In" SortExpression="IsSystem"
                        HeaderStyle-HorizontalAlign="Left" />--%>
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
</div>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="WCMS.WebSystem.WebParts.Central.WebGroupsController">
    <SelectParameters>
        <asp:QueryStringParameter Name="parentId" QueryStringField="ParentId" Type="Int32"
            DefaultValue="-1" />
    </SelectParameters>
</asp:ObjectDataSource>
