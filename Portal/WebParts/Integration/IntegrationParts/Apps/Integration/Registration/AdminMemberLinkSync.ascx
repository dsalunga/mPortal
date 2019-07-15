<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminMemberLinkSync.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.AdminMemberLinkSync" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<asp:HiddenField ID="hFilterGroup" runat="server" Value="" />
<div class="control-box">
    <div>
        <asp:DropDownList ID="cboItemTypes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboItemTypes_SelectedIndexChanged">
            <asp:ListItem Value="0">ALL</asp:ListItem>
            <asp:ListItem Value="1">LOCAL ONLY</asp:ListItem>
            <asp:ListItem Value="2">REMOTE ONLY</asp:ListItem>
            <asp:ListItem Value="3">IDENTICAL</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="cmdRefresh" runat="server" Text="Refresh" />
        <asp:Button ID="cmdSync" runat="server" Text="Sync" Width="85px"
            OnClick="cmdSync_Click" />
        <div class="pull-right">
            <asp:TextBox ID="txtSearch" Columns="25" runat="server" ClientIDMode="Static"></asp:TextBox>
            <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click"
                ClientIDMode="Static" />&nbsp;<asp:Button ID="cmdReset" runat="server" Text="Reset"
                    OnClick="cmdReset_Click" Width="55px" />
        </div>
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-borderless"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
        PageSize="20">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="checkbox" value='<%# Eval("UserName")%>' name='chkChecked' />
                </ItemTemplate>
                <HeaderStyle Width="10px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <HeaderStyle HorizontalAlign="center" Width="40px" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton4" AlternateText="Edit Item" CommandArgument='<%# Eval("Id") %>' />
                    <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                        CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="MobileNumber" HeaderText="Mobile" SortExpression="MobileNumber"
                HeaderStyle-HorizontalAlign="Left" Visible="false" />
            <asp:HyperLinkField DataNavigateUrlFields="UserId" DataNavigateUrlFormatString="/Central/Security/WebUserHome.aspx?UserId={0}"
                DataTextField="UserName" HeaderText="User Name" SortExpression="UserName" Target="_blank"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ExternalIdNo" HeaderText="Group ID" SortExpression="ExternalIdNo"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="MembershipDate" HeaderText="Membership Date" SortExpression="MembershipDate"
                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="true" Visible="false" />
            <asp:BoundField DataField="DateCreated" Visible="false" HeaderText="Registered" SortExpression="DateCreated"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="External" SortExpression="ExternalLink">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ToolTip="" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("ExternalLink")) %>'
                        OnClientClick="return false;" CommandName="Toggle_External" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Status" HeaderText="Integration" SortExpression="Status" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="CMS" SortExpression="AccountStatus">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ToolTip="Toggle Active Status" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("AccountStatus")) %>'
                        CommandName="Toggle_CMS" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ItemType" HeaderText="SyncType" SortExpression="ItemType"
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
        TypeName="WCMS.WebSystem.Apps.Integration.AdminMemberLinkSync">
        <SelectParameters>
            <asp:ControlParameter ControlID="hFilterGroup" DefaultValue="" Name="filterGroup" PropertyName="Value" />
            <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
            <asp:ControlParameter ControlID="cboItemTypes" DefaultValue="0" Name="itemType" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<br />
<br />
<span id="lblStatus" runat="server" style="color: Red"></span>
<script type="text/javascript">
    $(document).ready(function () {
        WCMS.Form.SetDefaultSubmit($("#txtSearch"), $("#cmdSearch"));
    });
</script>
