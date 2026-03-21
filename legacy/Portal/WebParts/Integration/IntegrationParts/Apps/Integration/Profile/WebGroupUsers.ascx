<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebGroupUsers.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.WebGroupUsers" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<asp:HiddenField ID="hidBaseGroupId" runat="server" Value="-1" />
<div class="control-box no-bottom-margin">
    <div>
        <asp:TextBox ID="txtId" runat="server" CssClass="input" Columns="50" ClientIDMode="Static"></asp:TextBox>
        <asp:Button ID="cmdAdd" runat="server" Text="Add..." OnClientClick="return Add_Click();"
            ClientIDMode="Static" OnClick="cmdAdd_Click" Width="60px" CssClass="btn btn-default btn-sm" />
        <div class="pull-right">
            <span style="font-size: larger"><span style="font-weight: normal">Active</span>&nbsp;<span
                runat="server" class="badge" id="lblCount"></span></span>&nbsp;
                <asp:Button ID="cmdDownload" runat="server" Text="Download" OnClick="cmdDownload_Click" CssClass="btn btn-default btn-sm" />
            &nbsp;&nbsp;
                <asp:DropDownList ID="cboCelebrants" CssClass="input" Visible="false" AppendDataBoundItems="True"
                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboCelebrants_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Text="* Celebrants *" Value="-1"></asp:ListItem>
                </asp:DropDownList>
            &nbsp;
                <asp:TextBox ID="txtSearch" CssClass="input" Columns="25" runat="server" ClientIDMode="Static"></asp:TextBox>
            <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click"
                ClientIDMode="Static" CssClass="btn btn-default btn-sm" /><asp:Button ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click"
                    CssClass="btn btn-default btn-sm" />
        </div>
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-borderless"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" PageSize="15"
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
                <HeaderTemplate>
                    <img src="/Content/Assets/Images/Common/ico_x.gif" alt="" title="Remove" />
                </HeaderTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="User Name" SortExpression="UserName">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <a href='<%# Eval("UserProfileUrl") %>' title="View details" target="_blank">
                        <%# Eval("UserName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/Central/Security/WebUserHome.aspx?UserId={0}"
                DataTextField="UserName" HeaderText="User Name" SortExpression="UserName" Target="_blank"
                HeaderStyle-HorizontalAlign="Left" Visible="false" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Email" HeaderStyle-HorizontalAlign="Left" HeaderText="Email"
                SortExpression="Email" />
            <asp:BoundField DataField="MobileNumber" HeaderStyle-HorizontalAlign="Left" HeaderText="Mobile"
                SortExpression="MobileNumber" />
            <asp:BoundField DataField="ExternalIDNo" HeaderStyle-HorizontalAlign="Left" HeaderText="Group ID"
                SortExpression="ExternalIDNo" />
            <asp:BoundField DataField="DateJoined" HeaderStyle-HorizontalAlign="Left" HeaderText="Date Joined"
                SortExpression="DateJoined" DataFormatString="{0:dd-MMM-yy h:mm tt}" HtmlEncode="false" />
            <asp:TemplateField HeaderText="Appr" SortExpression="Active">
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
        <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Profile.WebGroupUsers">
        <SelectParameters>
            <asp:ControlParameter ControlID="hidBaseGroupId" DefaultValue="-1" Name="groupId"
                PropertyName="Value" Type="Int32" />
            <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
            <asp:ControlParameter ControlID="cboCelebrants" DefaultValue="-1" Name="celebrantsFilter" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<script type="text/javascript">
    function Add_Click() {
        var addValue = $("#txtId").val().Trim();
        if (addValue == "") {
            ShowAccountBrowser("txtId", <%= WCMS.Framework.WebObjects.WebUser %>, 1, 1, 1, "cmdAdd");
            return false;
        }
        return true;
    }
</script>
