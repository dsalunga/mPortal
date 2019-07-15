<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="_GroupUserManager.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.GroupUserManager" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Register Src="Controls/WebGroupTab.ascx" TagName="WebGroupTab" TagPrefix="uc1" %>

<uc1:WebGroupTab ID="WebGroupTab1" runat="server" />
<div class="ControlBox no-bottom-margin">
    <asp:Button ID="cmdAdd" OnClientClick="return Add_Click();" runat="server" ClientIDMode="Static"
        Text="Add..." OnClick="cmdAdd_Click" CssClass="btn" />
    <asp:TextBox ID="txtId" runat="server" Columns="60" ClientIDMode="Static" placeholder="Enter username separated by semicolon"></asp:TextBox>
    <div style="float: right">
        <asp:Button ID="cmdDownload" runat="server" Text="Download" CssClass="btn" OnClick="cmdDownload_Click" />
    </div>
</div>
<asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
    GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" PageSize="15"
    AllowPaging="True">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Actions">
            <HeaderStyle HorizontalAlign="Center" Width="75px" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                    CommandArgument='<%# Eval("Id") %>' ToolTip="Remove" OnClientClick="return confirm('Are you you want to delete this item?');" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/Central/Security/WebUserHome.aspx?UserId={0}"
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
                    ID="Image1" CommandName="toggle_active" CommandArgument='<%# Eval("Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
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
    TypeName="WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList.GroupUserManager">
    <SelectParameters>
        <asp:QueryStringParameter Name="groupId" QueryStringField="GroupId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<br />
<br />
<asp:Label ID="lblStatus" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
<script type="text/javascript">
    function Add_Click() {
        var addValue = $("#txtId").val().Trim();

        if (addValue == "") {
            ShowAccountBrowser("txtId", <%: WCMS.Framework.WebObjects.WebUser %>, 0, 0, 1, "cmdAdd");
            return false;
        }

        return true;
    }
</script>