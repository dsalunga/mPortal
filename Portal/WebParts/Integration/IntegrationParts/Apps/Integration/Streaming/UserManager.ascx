<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserManager.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.Streaming.UserManager" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Register Src="~/Content/Parts/Integration/MakeUp/AttendanceRequests.ascx" TagPrefix="uc1" TagName="AttendanceRequests" %>
<%@ Register Src="Setup.ascx" TagName="Setup" TagPrefix="uc2" %>
<ul class="nav nav-tabs">
    <li class="<%=hActiveTab.Value == "Setup" ? "active" : "" %>"><a href="#" runat="server" id="linkSetup">Setup</a></li>
    <li class="<%=hActiveTab.Value == "Session" ? "active" : "" %>"><a href="#" runat="server" id="linkSessions">Sessions</a></li>
    <li class="<%=hActiveTab.Value == "Access" ? "active" : "" %>"><a href="#" runat="server" id="linkAccess">Access</a></li>
    <li class="<%=hActiveTab.Value == "Requests" ? "active" : "" %>"><a href="#" runat="server" id="linkRequests">Requests</a></li>
    <li class="<%=hActiveTab.Value == "Attendance" ? "active" : "" %>"><a href="#" runat="server" id="linkAttendance">Attendance</a></li>
    <li class="<%=hActiveTab.Value == "Admins" ? "active" : "" %>"><a href="#" runat="server" id="linkAdmins">Admins</a></li>
    <li class="pull-right"><a href="#" runat="server" id="linkStreaming">Streaming Player</a></li>
</ul>
<br />
<div id="lblStatus" runat="server" class="alert"></div>
<asp:HiddenField ID="hGroupId" runat="server" Value="-1" />
<asp:HiddenField ID="hAdminGroupId" runat="server" Value="-1" />
<asp:HiddenField ID="hActiveTab" runat="server" Value="" />

<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewSession" runat="server">
        <div id="sessionManager">
            <asp:HiddenField ID="hUserFormatString" runat="server" Value="" />
            <div class="min-bottom-margin">
                <a class="btn btn-default" href="<%=Request.RawUrl %>">Refresh</a>
                <asp:Button ID="cmdEndSession" OnClientClick="return confirm('Are you sure you want to terminate the selected sessions?');" runat="server" ClientIDMode="Static"
                    Text="Terminate" Style="display: none" CssClass="btn btn-danger delete-sessions" OnClick="cmdEndSession_Click" />
            </div>
            <div class="table-responsive">
                <asp:GridView CssClass="table" ID="GridViewSessions" runat="server" AllowPaging="True" AllowSorting="True"
                    CellPadding="4" DataSourceID="ObjectDataSourceSessions" ForeColor="#333333" GridLines="None"
                    Width="100%" AutoGenerateColumns="False" DataKeyNames="UserId"
                    PageSize="50">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle CssClass="table-pager" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <input type="checkbox" name="chkCheckedMain" class="chk-main-sessions" onclick="CheckAll(this, 'chkChecked');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type='checkbox' value='<%# Eval("UserId") %>' class="chk-item-sessions" name='chkChecked' />
                            </ItemTemplate>
                            <HeaderStyle Width="15px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                        </asp:TemplateField>
                        <asp:HyperLinkField DataNavigateUrlFields="UserId" DataNavigateUrlFormatString="//someorg.org/Account/?UserId={0}"
                            DataTextField="Name" HeaderText="Name" SortExpression="Name" Target="_blank"
                            HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="IPAddress" ItemStyle-CssClass="prepare-geoip" SortExpression="IPAddress"
                            HeaderText="IP Location" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="ActivityStartDate" SortExpression="ActivityStartDate"
                            DataFormatString="{0:dd-MMM-yy h\:mm tt}" HeaderText="Date Started" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="LastActivityDate" SortExpression="LastActivityDate" Visible="false"
                            DataFormatString="{0:dd-MMM-yy h\:mm tt}" HeaderText="Last Activity" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="SessionTime" SortExpression="SessionTime" HeaderText="Session Time"
                            DataFormatString="{0:hh\:mm\:ss}" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="IdleTime" SortExpression="IdleTime" HeaderText="Idle Time"
                            DataFormatString="{0:hh\:mm\:ss}" HeaderStyle-HorizontalAlign="Left" />
                        <asp:HyperLinkField DataNavigateUrlFields="PageUrl" DataTextField="PageName" HeaderText="Last Url"
                            SortExpression="PageUrl" Target="_blank" HeaderStyle-HorizontalAlign="Left" />
                    </Columns>
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                        PageButtonCount="30" />
                    <EmptyDataRowStyle CssClass="table-borderless" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSourceSessions" runat="server" SelectMethod="SelectSessions"
                    TypeName="WCMS.WebSystem.Apps.Integration.Streaming.UserManager">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hGroupId" DefaultValue="-1" Name="groupId" PropertyName="Value" Type="Int32" />
                        <asp:ControlParameter ControlID="hUserFormatString" Name="userFormatString" PropertyName="Value" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <script type="text/javascript">
                $(document).ready(function () {
                    var isBatchChange = false;

                    // Show/Hide Edit/Delete buttons
                    var showHideButtons = function () {
                        if (!isBatchChange) { // batch check/uncheck should not be running
                            var showEdit = false, showDelete = false;
                            var checkedItems = $('.chk-item-sessions:checked');
                            var checkedCount = checkedItems.length;
                            if (checkedCount == 1) {
                                showEdit = true;
                                showDelete = true;
                            }
                            else if (checkedCount > 1) {
                                showEdit = false;
                                showDelete = true;
                            }

                            //$('.edit').css('display', showEdit ? 'inline-block' : 'none');
                            $('.delete-sessions').css('display', showDelete ? 'inline-block' : 'none');
                        }
                    }

                    // Check/Uncheck all
                    $('.chk-main-sessions').click(function () {
                        isBatchChange = true;
                        $('.chk-item-sessions').attr('checked', $(this).is(':checked'));
                        isBatchChange = false;
                        showHideButtons();
                    });

                    // Trigger for any checked/unchecked item
                    $('.chk-item-sessions').change(function () {
                        showHideButtons();
                    });
                });
            </script>
        </div>
    </asp:View>
    <asp:View ID="viewAttendance" runat="server">
        <uc1:AttendanceRequests runat="server" ID="AttendanceRequests" Visible="true" />
    </asp:View>
    <asp:View ID="viewSetup" runat="server">
        <uc2:Setup ID="Setup1" runat="server" />
    </asp:View>
    <asp:View ID="viewAccess" runat="server">
        <div id="accessManager">
            <div class="min-bottom-margin row">
                <div class="col-md-12 col-sm-12" style="padding-left: 0">
                    <div class="col-md-5 col-sm-6 col-xs-9" style="padding-right: 5px">
                        <div class="input-group">
                            <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ClientIDMode="Static" ToolTip="Enter Email, Username, or Group ID separated by semicolon" placeholder="Email, Username, or Group ID"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="cmdAdd" OnClientClick="return addUser('txtId','cmdAdd');" runat="server" ClientIDMode="Static"
                                    Text="Add..." OnClick="cmdAdd_Click" CssClass="btn btn-default" />
                            </span>
                        </div>
                    </div>
                    &nbsp;
                <asp:Button ID="cmdRevoke" OnClientClick="return confirm('Are you sure you want to remove access to the selected accounts?');" runat="server" ClientIDMode="Static"
                    Text="Revoke" CssClass="btn btn-danger delete-users" Style="display: none" OnClick="cmdRevoke_Click" />
                    <div class="pull-right"><a href="#" id="linkManageIPs" runat="server" target="_blank" class="btn btn-default">Allow IP Addresses</a></div>
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView CssClass="table table-borderless" ID="GridViewUsers" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="0" DataKeyNames="Id" DataSourceID="ObjectDataSourceUsers" ForeColor="#333333"
                    GridLines="None" Width="100%" PageSize="50"
                    AllowPaging="True">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <input type="checkbox" name="chkCheckedMain" class="chk-main-users" onclick="CheckAll(this, 'chkChecked');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type='checkbox' value='<%# Eval("Id") %>' class="chk-item-users" name='chkChecked' />
                            </ItemTemplate>
                            <HeaderStyle Width="15px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" Width="55px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                                    CommandArgument='<%# Eval("Id") %>' ToolTip="Remove" OnClientClick="return confirm('Are you you want to delete this item?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/Member-Profile/?UserId={0}"
                            DataTextField="UserName" HeaderText="Username" SortExpression="UserName" Target="_blank"
                            HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Email" HeaderStyle-HorizontalAlign="Left" HeaderText="Email"
                            SortExpression="Email" />
                        <asp:BoundField DataField="FirstName" HeaderStyle-HorizontalAlign="Left" HeaderText="First Name"
                            SortExpression="FirstName" />
                        <asp:BoundField DataField="LastName" HeaderStyle-HorizontalAlign="Left" HeaderText="Last Name"
                            SortExpression="LastName" />
                        <asp:BoundField DataField="MobileNumber" HeaderStyle-HorizontalAlign="Left" HeaderText="Mobile"
                            SortExpression="MobileNumber" Visible="false" />
                        <asp:BoundField DataField="DateJoined" HeaderStyle-HorizontalAlign="Left" HeaderText="Date Granted"
                            SortExpression="DateJoined" />
                        <asp:TemplateField HeaderText="Active" SortExpression="Active" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton ToolTip="Toggle Active Status" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                                    ID="Image1" CommandName="toggle_active" CommandArgument='<%# Eval("Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle CssClass="table-pager" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="White" />
                    <PagerSettings PageButtonCount="25" />
                    <EmptyDataRowStyle CssClass="table-borderless" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSourceUsers" runat="server" SelectMethod="SelectUsers"
                    TypeName="WCMS.WebSystem.Apps.Integration.Streaming.UserManager">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hGroupId" DefaultValue="-1" Name="groupId" PropertyName="Value" Type="Int32" />
                        <asp:Parameter DefaultValue="1" Name="status" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <br />
            <br />
            <script type="text/javascript">
                $(document).ready(function () {
                    var isBatchChange = false;

                    // Show/Hide Edit/Delete buttons
                    var showHideButtons = function () {
                        if (!isBatchChange) { // batch check/uncheck should not be running
                            var showEdit = false, showDelete = false;
                            var checkedItems = $('.chk-item-users:checked');
                            var checkedCount = checkedItems.length;
                            if (checkedCount == 1) {
                                showEdit = true;
                                showDelete = true;
                            }
                            else if (checkedCount > 1) {
                                showEdit = false;
                                showDelete = true;
                            }

                            //$('.edit').css('display', showEdit ? 'inline-block' : 'none');
                            $('.delete-users').css('display', showDelete ? 'inline-block' : 'none');
                        }
                    }

                    // Check/Uncheck all
                    $('.chk-main-users').click(function () {
                        isBatchChange = true;
                        $('.chk-item-users').attr('checked', $(this).is(':checked'));
                        isBatchChange = false;
                        showHideButtons();
                    });

                    // Trigger for any checked/unchecked item
                    $('.chk-item-users').change(function () {
                        showHideButtons();
                    });
                });
            </script>
        </div>
    </asp:View>
    <asp:View ID="viewAdmins" runat="server">
        <div id="panelAdmins">
            <div class="min-bottom-margin row">
                <div class="col-md-11 col-sm-11" style="padding-left: 0">
                    <div class="col-md-5 col-sm-6 col-xs-9" style="padding-right: 5px">
                        <div class="input-group">
                            <asp:TextBox ID="txtAdminId" runat="server" CssClass="form-control" ClientIDMode="Static" ToolTip="Enter Email, Username, or Group ID separated by semicolon" placeholder="Email, Username, or Group ID"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="cmdAdminAdd" OnClientClick="return addUser('txtAdminId','cmdAdminAdd');" runat="server" ClientIDMode="Static"
                                    Text="Add..." OnClick="cmdAdminAdd_Click" CssClass="btn btn-default" />
                            </span>
                        </div>
                    </div>
                    &nbsp;
                <asp:Button ID="cmdAdminRevoke" OnClientClick="return confirm('Are you sure you want to remove access to the selected accounts?');" runat="server" ClientIDMode="Static"
                    Text="Revoke" CssClass="btn btn-danger delete-users" Style="display: none" OnClick="cmdAdminRevoke_Click" />
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView CssClass="table" ID="GridViewAdmins" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSourceAdmins" ForeColor="#333333"
                    GridLines="None" Width="100%" PageSize="50"
                    AllowPaging="True">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <input type="checkbox" name="chkCheckedMain" class="chk-main-users" onclick="CheckAll(this, 'chkChecked');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type='checkbox' value='<%# Eval("Id") %>' class="chk-item-users" name='chkChecked' />
                            </ItemTemplate>
                            <HeaderStyle Width="15px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" Width="55px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                                    CommandArgument='<%# Eval("Id") %>' ToolTip="Remove" OnClientClick="return confirm('Are you you want to delete this item?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/Member-Profile/?UserId={0}"
                            DataTextField="UserName" HeaderText="Username" SortExpression="UserName" Target="_blank"
                            HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Email" HeaderStyle-HorizontalAlign="Left" HeaderText="Email"
                            SortExpression="Email" />
                        <asp:BoundField DataField="FirstName" HeaderStyle-HorizontalAlign="Left" HeaderText="First Name"
                            SortExpression="FirstName" />
                        <asp:BoundField DataField="LastName" HeaderStyle-HorizontalAlign="Left" HeaderText="Last Name"
                            SortExpression="LastName" />
                        <asp:BoundField DataField="MobileNumber" HeaderStyle-HorizontalAlign="Left" HeaderText="Mobile"
                            SortExpression="MobileNumber" Visible="false" />
                        <asp:BoundField DataField="DateJoined" HeaderStyle-HorizontalAlign="Left" HeaderText="Date Granted"
                            SortExpression="DateJoined" />
                        <asp:TemplateField HeaderText="Active" SortExpression="Active" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton ToolTip="Toggle Active Status" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                                    ID="Image1" CommandName="toggle_active" CommandArgument='<%# Eval("Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle CssClass="table-pager" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="White" />
                    <PagerSettings PageButtonCount="25" />
                    <EmptyDataRowStyle CssClass="table-borderless" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSourceAdmins" runat="server" SelectMethod="SelectUsers"
                    TypeName="WCMS.WebSystem.Apps.Integration.Streaming.UserManager">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hAdminGroupId" DefaultValue="-1" Name="groupId" PropertyName="Value" Type="Int32" />
                        <asp:Parameter DefaultValue="1" Name="status" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <br />
            <br />
            <script type="text/javascript">
                $(document).ready(function () {
                    var isBatchChange = false;

                    // Show/Hide Edit/Delete buttons
                    var showHideButtons = function () {
                        if (!isBatchChange) { // batch check/uncheck should not be running
                            var showEdit = false, showDelete = false;
                            var checkedItems = $('.chk-item-users:checked');
                            var checkedCount = checkedItems.length;
                            if (checkedCount == 1) {
                                showEdit = true;
                                showDelete = true;
                            }
                            else if (checkedCount > 1) {
                                showEdit = false;
                                showDelete = true;
                            }

                            //$('.edit').css('display', showEdit ? 'inline-block' : 'none');
                            $('.delete-users').css('display', showDelete ? 'inline-block' : 'none');
                        }
                    }

                    // Check/Uncheck all
                    $('.chk-main-users').click(function () {
                        isBatchChange = true;
                        $('.chk-item-users').attr('checked', $(this).is(':checked'));
                        isBatchChange = false;
                        showHideButtons();
                    });

                    // Trigger for any checked/unchecked item
                    $('.chk-item-users').change(function () {
                        showHideButtons();
                    });
                });
            </script>
        </div>
    </asp:View>
    <asp:View ID="viewRequests" runat="server">
        <asp:MultiView ID="MultiViewRequests" runat="server" ActiveViewIndex="0">
            <asp:View ID="viewRequestList" runat="server">
                <div id="requestManager">
                    <div class="min-bottom-margin">
                        <asp:Button ID="cmdApprove" runat="server" ClientIDMode="Static"
                            Text="Approve" CssClass="btn btn-success request-users" Style="display: none" OnClick="cmdApprove_Click" />
                        <asp:Button ID="cmdReject" runat="server" ClientIDMode="Static"
                            Text="Reject" CssClass="btn btn-danger request-users" Style="display: none" OnClick="cmdReject_Click" />
                    </div>
                    <div class="table-responsive">
                        <asp:GridView CssClass="table" ID="GridViewRequests" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSourceRequests" ForeColor="#333333"
                            GridLines="None" Width="100%" PageSize="50" EmptyDataText="There are no pending requests."
                            AllowPaging="True">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input type="checkbox" name="chkCheckedMain" class="chk-main-users" onclick="CheckAll(this, 'chkChecked');" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input type='checkbox' value='<%# Eval("Id") %>' class="chk-item-users" name='chkChecked' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="15px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                                            CommandArgument='<%# Eval("Id") %>' ToolTip="Remove" OnClientClick="return confirm('Are you you want to delete this item?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/Singapore/Member-Profile/?UserId={0}"
                                    DataTextField="UserName" HeaderText="Username" SortExpression="UserName" Target="_blank"
                                    HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Email" HeaderStyle-HorizontalAlign="Left" HeaderText="Email"
                                    SortExpression="Email" />
                                <asp:BoundField DataField="FirstName" HeaderStyle-HorizontalAlign="Left" HeaderText="First Name"
                                    SortExpression="FirstName" />
                                <asp:BoundField DataField="LastName" HeaderStyle-HorizontalAlign="Left" HeaderText="Last Name"
                                    SortExpression="LastName" />
                                <asp:BoundField DataField="MobileNumber" HeaderStyle-HorizontalAlign="Left" HeaderText="Mobile"
                                    SortExpression="MobileNumber" Visible="false" />
                                <asp:BoundField DataField="DateJoined" HeaderStyle-HorizontalAlign="Left" HeaderText="Date Requested"
                                    SortExpression="DateJoined" />
                                <asp:TemplateField HeaderText="Active" SortExpression="Active" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ToolTip="Toggle Active Status" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                                            ID="Image1" CommandName="toggle_active" CommandArgument='<%# Eval("Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Remarks" HeaderStyle-HorizontalAlign="Left" HeaderText="Reason for Request"
                                    SortExpression="Remarks" />
                                <asp:BoundField DataField="Location" ItemStyle-CssClass="prepare-geoip" SortExpression="Location"
                                    HeaderText="IP Location" HeaderStyle-HorizontalAlign="Left" />
                            </Columns>
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle CssClass="table-pager" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <AlternatingRowStyle BackColor="White" />
                            <PagerSettings PageButtonCount="25" />
                            <EmptyDataRowStyle CssClass="table-borderless" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="ObjectDataSourceRequests" runat="server" SelectMethod="SelectUsers"
                            TypeName="WCMS.WebSystem.Apps.Integration.Streaming.UserManager">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hGroupId" DefaultValue="-1" Name="groupId" PropertyName="Value" Type="Int32" />
                                <asp:Parameter DefaultValue="0" Name="status" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                    <br />
                    <br />
                    <script type="text/javascript">
                        $(document).ready(function () {
                            var isBatchChange = false;

                            // Show/Hide Edit/Delete buttons
                            var showHideButtons = function () {
                                if (!isBatchChange) { // batch check/uncheck should not be running
                                    var showEdit = false, showDelete = false;
                                    var checkedItems = $('.chk-item-users:checked');
                                    var checkedCount = checkedItems.length;
                                    if (checkedCount == 1) {
                                        showEdit = true;
                                        showDelete = true;
                                    }
                                    else if (checkedCount > 1) {
                                        showEdit = false;
                                        showDelete = true;
                                    }

                                    $('.request-users').css('display', showDelete ? 'inline-block' : 'none');
                                }
                            }

                            // Check/Uncheck all
                            $('.chk-main-users').click(function () {
                                isBatchChange = true;
                                $('.chk-item-users').attr('checked', $(this).is(':checked'));
                                isBatchChange = false;
                                showHideButtons();
                            });

                            // Trigger for any checked/unchecked item
                            $('.chk-item-users').change(function () {
                                showHideButtons();
                            });
                        });
                    </script>
                </div>
            </asp:View>
            <asp:View ID="viewRequestForm" runat="server">
                <asp:HiddenField ID="hSelectedIds" runat="server" Value="" />
                <div class="row">
                    <div class="col-md-6 col-sm-8">
                        <div>
                            <%--<a id="linkUserProfileUrl" runat="server" href='#' title="View details" target="_blank">
                                <img runat="server" id="imagePhotoPath" src='#' width="300" /></a>
                            <br />--%>
                            Requestor(s):&nbsp;<strong runat="server" id="lblRequestor"></strong>
                        </div>
                        <br />
                        <div>
                            <span runat="server" id="lblReasonOrNotes"></span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Rejection Reason" ForeColor="Red" ControlToValidate="txtReason">*</asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" Rows="4" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 5px">
                    <div class="col-md-12">
                        <asp:Button ID="cmdSubmitApproval" CssClass="btn btn-success" runat="server" Text="Approve" OnClick="cmdSubmitApproval_Click" Visible="false" /><asp:Button ID="cmdSubmitRejection" CssClass="btn btn-danger" runat="server" Text="Reject" OnClick="cmdSubmitRejection_Click" Visible="false" />&nbsp;<a href="#" runat="server" id="linkRequestCancel" class="btn btn-default">Cancel</a>
                    </div>
                </div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:" ShowMessageBox="True" ShowSummary="False" />
            </asp:View>
            <asp:View ID="viewRequestDone" runat="server">
                Thank you! A notification email has been sent to the requestor(s).
                <br />
                <br />
                <a href="#" runat="server" id="linkRequestContinue" class="btn btn-default">Done</a>
            </asp:View>
        </asp:MultiView>
    </asp:View>
</asp:MultiView>

<script type="text/javascript">
    function addUser(textBoxId, buttonAdd) {
        var addValue = $("#" + textBoxId).val().Trim();
        if (addValue == "") {
            ShowAccountBrowser(textBoxId, <% =WebObjects.WebUser %>, 0, 0, 1, buttonAdd);
            return false;
        }
        return true;
    }

    $(document).ready(function() {
        $('.prepare-geoip').each(function () {
            var text = $(this).text();
            if (text && text.length > 4 && text.indexOf('(') == -1) {
                $(this).html(text + " (<a class='to-geoip' data-ip='" + text + "' href='#'>Resolve Location</a>)");
            }
        });

        $('.to-geoip').each(function () {
            $(this).click(function (event) {
                event.preventDefault();

                var ip = $(this).data('ip');
                if (ip) {
                    var that = this;
                    $(that).removeClass('to-geoip');
                    ResolveGeoIp(function (loc) { $(that).replaceWith(loc); }, ip);
                }
            });
        });
    });
</script>
