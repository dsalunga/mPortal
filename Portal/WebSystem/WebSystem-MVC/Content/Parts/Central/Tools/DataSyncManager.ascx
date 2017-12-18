<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataSyncManager.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.Tools.DataSyncManager" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>

<asp:HiddenField runat="server" ID="hGroupId" Value="-1" />
<asp:HiddenField runat="server" ID="hObjectId" Value="-1" />
<asp:HiddenField runat="server" ID="hUserHomeUrl" Value="" />
<asp:HiddenField runat="server" ID="hUserEditUrl" Value="" />
<asp:HiddenField runat="server" ID="hDataEntry" Value="0" />
<div class="control-box">
    <div>
        <asp:DropDownList ID="cboItemTypes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboItemTypes_SelectedIndexChanged">
            <asp:ListItem Value="0">ALL</asp:ListItem>
            <asp:ListItem Value="1">LOCAL ONLY</asp:ListItem>
            <asp:ListItem Value="2">REMOTE ONLY</asp:ListItem>
            <asp:ListItem Value="3">IDENTICAL</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="cmdRefresh" runat="server" Text="Refresh" />
        <asp:Button ID="cmdSync" runat="server" Text="Sync" OnClick="cmdSync_Click" />
        &nbsp;
            <%--<asp:Button ID="cmdNewUser" CssClass="Command" runat="server" Text="New User" Width="85px"
                OnClick="cmdNewUser_Click" />
            <asp:Button ID="cmdDelete" CssClass="Command" OnClientClick="return confirm('Delete selected items?');"
                runat="server" Text="Delete" OnClick="cmdDelete_Click" ToolTip="Delete Selected" />--%>
        <span runat="server" id="panelGroupFilter">&nbsp; Group:&nbsp;
                <asp:DropDownList ID="cboGroups" AppendDataBoundItems="True" runat="server" AutoPostBack="True"
                    DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="cboGroups_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Text="* All Groups *" Value="-1"></asp:ListItem>
                </asp:DropDownList>
            <asp:ObjectDataSource ID="dsGroups" runat="server" SelectMethod="SelectGroups" TypeName="WCMS.WebSystem.WebParts.Central.Tools.DataSyncManager"></asp:ObjectDataSource>
        </span>
        <div class="pull-right">
            <asp:TextBox ID="txtSearch" Columns="25" Visible="false" runat="server" ClientIDMode="Static"></asp:TextBox>
            <%--<asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click"
                    ClientIDMode="Static" />
                <asp:Button ID="cmdReset" runat="server" Text="Reset" OnClick="cmdReset_Click" Width="55px" />&nbsp;
                <asp:Button ID="cmdDownload" CssClass="Command" runat="server" Text="Download"
                    OnClick="cmdDownload_Click" />--%>
        </div>
    </div>
</div>
<div>
    <asp:GridView ID="GridView1" CssClass="grid-padding" runat="server" AllowSorting="True"
        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1"
        ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand"
        AllowPaging="True" PageSize="20">
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
            <asp:TemplateField HeaderText="Action" ItemStyle-Wrap="false">
                <HeaderStyle HorizontalAlign="center" Width="40px" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton4" AlternateText="Edit Profile" ToolTip="Edit" CommandArgument='<%# Eval("Id") %>' /><asp:ImageButton
                            ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                            CommandArgument='<%# Eval("Id") %>' ToolTip="Delete" OnClientClick="return confirm('Are you you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/Central/Security/WebUserHome.aspx?UserId={0}"
                DataTextField="UserName" HeaderText="User Name" SortExpression="UserName" Target="_blank"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="MobileNumber" HeaderText="Mobile" SortExpression="MobileNumber"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="DateCreated" HeaderText="Created" SortExpression="DateCreated"
                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="true"
                Visible="false" />
            <asp:BoundField DataField="LastUpdate" HeaderText="Updated" SortExpression="LastUpdate"
                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="true" Visible="false" />
            <asp:BoundField DataField="LastLogin" HeaderText="Last Login" SortExpression="LastLogin"
                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:yy-MM-dd HH:mm}" HtmlEncode="true" Visible="false" />
            <asp:BoundField DataField="ItemType" HeaderText="SyncType" SortExpression="ItemType"
                HeaderStyle-HorizontalAlign="Left" />
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
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
        TypeName="WCMS.WebSystem.WebParts.Central.Tools.DataSyncManager">
        <SelectParameters>
            <asp:ControlParameter ControlID="hGroupId" DefaultValue="-1" Name="groupId" PropertyName="Value"
                Type="Int32" />
            <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
            <asp:ControlParameter ControlID="cboItemTypes" DefaultValue="0" Name="itemType" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        WCMS.Form.SetDefaultSubmit($("#txtSearch"), $("#cmdSearch"));
    });
</script>
