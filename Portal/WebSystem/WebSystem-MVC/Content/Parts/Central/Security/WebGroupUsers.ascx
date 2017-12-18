<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebGroupUsers.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Security.WebGroupUsers" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="../Controls/WebGroupTab.ascx" TagName="WebGroupTab" TagPrefix="uc1" %>

<uc1:WebGroupTab ID="WebGroupTab1" runat="server" />
<div class="control-box">
    <div>
        <asp:Button ID="cmdAdd" OnClientClick="return Add_Click();" runat="server" ClientIDMode="Static"
            Text="Add..." OnClick="cmdAdd_Click" CssClass="btn btn-default btn-sm" />
        <asp:TextBox ID="txtId" runat="server" Columns="60" CssClass="input" ClientIDMode="Static" placeholder="Enter username separated by semicolon"></asp:TextBox>
        <div class="pull-right">
            <asp:Button ID="cmdDownload" runat="server" Text="Download" CssClass="btn btn-default btn-sm" OnClick="cmdDownload_Click" />
        </div>
    </div>
</div>
<div id="lblStatus" runat="server" class="alert"></div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" CssClass="table table-borderless table-condensed" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" PageSize="25"
        AllowPaging="True">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="">
                <HeaderStyle HorizontalAlign="Center" Width="18px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                        CommandArgument='<%# Eval("Id") %>' ToolTip="Remove" OnClientClick="return confirm('Are you you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/Central/Security/WebUserHome/?UserId={0}"
                DataTextField="UserName" HeaderText="User Name" SortExpression="UserName" Target="_blank"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Email" HeaderStyle-HorizontalAlign="Left" HeaderText="Email"
                SortExpression="Email" />
            <asp:BoundField DataField="FirstName" HeaderStyle-HorizontalAlign="Left" HeaderText="First Name"
                SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderStyle-HorizontalAlign="Left" HeaderText="Last Name"
                SortExpression="LastName" />
            <asp:BoundField DataField="DateJoined" HeaderStyle-HorizontalAlign="Left" HeaderText="Date Joined"
                SortExpression="DateJoined" />
            <asp:TemplateField HeaderText="Active" SortExpression="Active">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ToolTip="Toggle Active Status" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                        ID="Image1" CommandName="ToggleActive" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle HorizontalAlign="Left" CssClass="table-pager" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="20" />
    </asp:GridView>
</div>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.WebParts.Central.Security.WebGroupUsers">
    <SelectParameters>
        <asp:QueryStringParameter Name="groupId" QueryStringField="GroupId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<br />
<br />
<script type="text/javascript">
    function Add_Click() {
        var addValue = $("#txtId").val().Trim();

        if (addValue == "") {
            ShowAccountBrowser("txtId", <% =WCMS.Framework.WebObjects.WebUser %>, 0, 0, 1, "cmdAdd");
            return false;
        }

        return true;
    }
</script>
