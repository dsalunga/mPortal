<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfigMemberLinks.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.ConfigMemberLinks" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<asp:HiddenField ID="hFilterGroup" runat="server" Value="" />
<div class="control-box no-bottom-margin">
    <div>
        <div class="pull-left">
            <asp:Button ID="cmdDelete" runat="server" Text="Delete" CssClass="btn btn-default btn-sm" OnClick="cmdDelete_Click" OnClientClick="return confirm('Delete selected item(s)?');" />
            <asp:Button ID="cmdSync" runat="server" Enabled="false" Text="Sync" CssClass="btn btn-default btn-sm"
                OnClick="cmdSync_Click" />&nbsp;
        </div>
        <div class="pull-left col-md-4 col-sm-5 col-np">
            <div class="input-group">
                <asp:TextBox ID="txtCreate" Columns="25" CssClass="col-md-3 col-sm-3 input-sm form-control" placeholder="Enter Username or Email" runat="server" ClientIDMode="Static"></asp:TextBox>
                <div class="input-group-btn">
                    <asp:Button ID="cmdCreateLink" runat="server" Text="Create Link"
                        ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClick="cmdCreateLink_Click" />
                </div>
            </div>
        </div>
        <div class="pull-right col-md-3 col-sm-4 col-np">
            <div class="input-group">
                <asp:TextBox ID="txtSearch" Columns="25" runat="server" CssClass="col-md-3 col-sm-3 input-sm form-control" ClientIDMode="Static"></asp:TextBox>
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
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-borderless"
        CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1" ForeColor="#333333"
        GridLines="None" Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
        PageSize="25">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="checkbox" value="chkMain" onclick="CheckAll(this, 'chkItems');" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input type="checkbox" value='<%# Eval("Id") %>' name="chkItems" />
                    </ItemTemplate>
                    <HeaderStyle Width="10px" />
                </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <HeaderStyle HorizontalAlign="center" Width="18px" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <%--<asp:ImageButton runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                    ID="Imagebutton4" AlternateText="Edit Item" CommandArgument='<%# Eval("Id") %>' />--%>
                    <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                        CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you you want to delete this item?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="ExternalIdUrl" DataTextField="ExternalIdNo" HeaderText="Group ID"
                SortExpression="ExternalIdNo" HeaderStyle-HorizontalAlign="Left" />
            <%--<asp:BoundField DataField="ExternalIdNo" HeaderText="Group ID" SortExpression="ExternalIdNo"
            HeaderStyle-HorizontalAlign="Left" />--%>
            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="MobileNumber" HeaderText="Mobile" SortExpression="MobileNumber"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:HyperLinkField DataNavigateUrlFields="UserId" DataNavigateUrlFormatString="/Central/Security/WebUserHome?UserId={0}"
                DataTextField="UserName" HeaderText="Username" SortExpression="UserName" Target="_blank"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="MembershipDate" HeaderText="Membership Date" SortExpression="MembershipDate"
                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="true" Visible="false" />
            <asp:BoundField DataField="DateCreated" Visible="false" HeaderText="Registered" SortExpression="DateCreated"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="External" SortExpression="ExternalLink">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton2" ToolTip="" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("ExternalLink")) %>'
                        OnClientClick="return false;" CommandName="Toggle_External" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Status" HeaderText="Integration" SortExpression="Status" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="CMS" SortExpression="AccountStatus">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton3" ToolTip="Toggle Active Status" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("AccountStatus")) %>'
                        CommandName="Toggle_CMS" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle HorizontalAlign="Left" CssClass="table-pager" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="25" />
    </asp:GridView>
</div>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="WCMS.WebSystem.Apps.Integration.ConfigMemberLinks">
    <SelectParameters>
        <asp:ControlParameter ControlID="hFilterGroup" DefaultValue="" Name="filterGroup" PropertyName="Value" />
        <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
    </SelectParameters>
</asp:ObjectDataSource>
<br />
<br />
<span id="lblStatus" runat="server" style="color: Red"></span>
<script type="text/javascript">
    $(document).ready(function () {
        WCMS.Form.SetDefaultSubmit($("#txtSearch"), $("#cmdSearch"));
    });
</script>
