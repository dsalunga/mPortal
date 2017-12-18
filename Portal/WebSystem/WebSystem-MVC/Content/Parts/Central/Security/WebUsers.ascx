<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUsers.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Central.WebUsersController" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>

<asp:HiddenField runat="server" ID="hGroupId" Value="-1" />
<asp:HiddenField runat="server" ID="hUserHomeUrl" Value="" />
<asp:HiddenField runat="server" ID="hUserEditUrl" Value="" />
<asp:HiddenField runat="server" ID="hDataEntry" Value="0" />
<div class="control-box no-bottom-margin">
    <div>
        <div class="btn-group">
            <button type="submit" ID="cmdNewUser" class="btn btn-default btn-sm" runat="server"
                onserverclick="cmdNewUser_Click">New User</button>
            <button type="button" class="btn btn-default btn-sm dropdown-toggle" data-toggle="dropdown">
                <span class="caret"></span>
                <span class="sr-only">Toggle Dropdown</span>
            </button>
            <ul class="dropdown-menu" role="menu">
                <li>
                    <asp:LinkButton ID="cmdDelete" OnClientClick="return confirm('Delete selected items?');"
                        runat="server" Text="Delete Selected" OnClick="cmdDelete_Click" ToolTip="Delete Selected"></asp:LinkButton></li>
                <li class="divider"></li>
                <li>
                    <asp:LinkButton ID="cmdDownload" runat="server" Text="Download"
                        OnClick="cmdDownload_Click"></asp:LinkButton></li>
            </ul>
        </div>

        <span runat="server" id="panelGroupFilter">
            <asp:DropDownList ID="cboGroups" AppendDataBoundItems="True" runat="server" AutoPostBack="True" CssClass="input"
                DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="cboGroups_SelectedIndexChanged">
                <asp:ListItem Selected="True" Text="* All Groups *" Value="-1"></asp:ListItem>
                <asp:ListItem Text="* No Membership *" Value="-2"></asp:ListItem>
                <asp:ListItem Text="* Inactive Accounts *" Value="-3"></asp:ListItem>
            </asp:DropDownList>
            <asp:ObjectDataSource ID="dsGroups" runat="server" SelectMethod="SelectGroups" TypeName="WCMS.WebSystem.WebParts.Central.WebUsersController"></asp:ObjectDataSource>
        </span>
        <%--<span runat="server" id="panelSiteFilter">
            <asp:DropDownList ID="cboSites" runat="server" AutoPostBack="true" CssClass="input" OnSelectedIndexChanged="cboSites_SelectedIndexChanged">
                <asp:ListItem Text="* All Sites *" Value="-2"></asp:ListItem>
            </asp:DropDownList>
        </span>--%>
        <div class="pull-right col-md-4 col-sm-5 col-np">
            <div class="input-group">
                <asp:TextBox ID="txtSearch" CssClass="col-md-3 col-sm-3 col-xs-3 input-sm form-control" Columns="25" runat="server" ClientIDMode="Static"></asp:TextBox>
                <div class="input-group-btn">
                    <button type="submit" onserverclick="cmdSearch_Click" id="cmdSearch" title="Search" runat="server" class="btn btn-default btn-sm"
                        clientidmode="Static">
                        <span class='glyphicon glyphicon-search'></span>
                    </button>
                    <button type="submit" id="cmdReset" runat="server" title="Reset" class="btn btn-default btn-sm" onserverclick="cmdReset_Click">
                        <span class='glyphicon glyphicon-remove'></span>
                    </button>
                </div>

            </div>
        </div>
    </div>
</div>
<div class="table-responsive">
    <asp:GridView ID="GridView1" CssClass="table table-borderless table-condensed" runat="server" AllowSorting="True"
        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1"
        ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand"
        AllowPaging="True" PageSize="50">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <input type="checkbox" name="chkCheckedMain" onclick="CheckAll(this, 'chkChecked');">
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="checkbox" value='<%# Eval("Id")%>' name='chkChecked' />
                </ItemTemplate>
                <HeaderStyle Width="10px" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Action" ItemStyle-Wrap="false" Visible="false">
                <HeaderStyle HorizontalAlign="center" Width="20px" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <asp:ImageButton runat="server" EnableViewState="false" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                        ID="Imagebutton4" AlternateText="Edit Profile" ToolTip="Edit" CommandArgument='<%# Eval("Id") %>' Visible="false" />
                    <asp:ImageButton EnableViewState="false"
                            ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                            CommandArgument='<%# Eval("Id") %>' ToolTip="Delete" OnClientClick="return confirm('Are you you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/Central/Security/WebUserHome/?UserId={0}"
                DataTextField="UserName" HeaderText="User Name" SortExpression="UserName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="MobileNumber" HeaderText="Mobile" SortExpression="MobileNumber"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="DateCreated" HeaderText="Created" SortExpression="DateCreated"
                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:yyyy-MMdd}" HtmlEncode="true" />
            <asp:BoundField DataField="LastUpdate" HeaderText="Updated" SortExpression="LastUpdate"
                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:yyyy-MMdd}" HtmlEncode="true" />
            <asp:BoundField DataField="LastLogin" HeaderText="Last Login" SortExpression="LastLogin"
                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:yyyy-MMdd HH:mm}" HtmlEncode="true" />
            <asp:TemplateField HeaderText="Active" SortExpression="Active">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ToolTip="Toggle Active Status" EnableViewState="false" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                        ID="Image1" CommandName="ToggleActive" CommandArgument='<%# Eval("Id") %>' />
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
</div>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.WebParts.Central.WebUsersController">
    <SelectParameters>
        <asp:ControlParameter ControlID="hGroupId" DefaultValue="-1" Name="groupId" PropertyName="Value"
            Type="Int32" />
        <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
    </SelectParameters>
</asp:ObjectDataSource>
<script type="text/javascript">
    $(document).ready(function () {
        WCMS.Form.SetDefaultSubmit($("#txtSearch"), $("#cmdSearch"));
    });
</script>
