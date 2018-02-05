<%@ Control Language="C#" AutoEventWireup="true" ClassName="WCMS.WebSystem.Apps.Integration.EventRegister.AttendeesView" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.Framework.Core" %>
<%@ Import Namespace="WCMS.WebSystem.ViewModel" %>

<script runat="server">

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (!WebGlobalPolicy.IsUserPermitted(GlobalPolicies.Administration, Permissions.UsersManagement))
            //    WQuery.StaticRedirect(WConstants.AbsoluteAccessDeniedPage);

            var context = new WContext(this);
            var element = context.Element;

            int id = -1;
            var groupFilter = element.GetParameterValue("GroupFilter");
            if (!string.IsNullOrEmpty(groupFilter))
            {
                var group = WebGroup.SelectNode(groupFilter);
                if (group != null)
                    id = group.Id;
            }

            var dashboardUrl = element.GetParameterValue("DashboardUrl");
            if (!string.IsNullOrEmpty(dashboardUrl))
                linkDashboard.HRef = dashboardUrl;

            if (WSession.Current.IsAdministrator)
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;

            if (id > 0)
            {
                hGroupId.Value = id.ToString();
                GridView1.DataBind();
            }
        }
    }

    protected void cmdNewUser_Click(object sender, EventArgs e)
    {
        var query = new WQuery(this);
        query.Remove(WebColumns.UserId);
        query.Set(WConstants.Open, "Attendee");
        query.Redirect();
    }

    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        string checkedItems = Request.Form["chkChecked"];
        if (!string.IsNullOrEmpty(checkedItems))
        {
            var ids = DataHelper.ParseCommaSeparatedIdList(checkedItems);
            if (ids.Count > 0)
            {
                var groupId = DataHelper.GetId(hGroupId.Value.Trim());
                foreach (var userId in ids)
                {
                    var user = WebUser.Get(userId);
                    if (user.Status == AccountStatus.DRAFT)
                    {
                        var link = MemberLink.Provider.GetByUserId(user.Id);
                        if (link != null)
                            link.Delete();

                        user.Delete();
                    }
                    else
                    {
                        var ug = WebUserGroup.Get(groupId, userId);
                        if (ug != null)
                            ug.Delete();
                    }
                }

                GridView1.DataBind();
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = DataHelper.GetId(e.CommandArgument);
        var query = new WQuery(this);

        switch (e.CommandName)
        {
            case "Custom_Edit":
                query.SetReturn();
                query.Set(WConstants.Open, "Attendee");
                query.Set(WebColumns.UserId, id);
                query.Redirect();
                break;
        }
    }

    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }

    protected void cmdReset_Click(object sender, EventArgs e)
    {
        txtSearch.Text = string.Empty;
        GridView1.DataBind();
    }

    protected void cmdDownload_Click(object sender, EventArgs e)
    {
        var keyword = txtSearch.Text.Trim();
        var groupId = DataHelper.GetId(hGroupId.Value.Trim());
        var ds = EventRegisterUtil.SelectDownload(groupId, keyword);
        WebHelper.DownloadAsCsv(ds, "Attendees");
    }

    protected void cmdDownloadPrint_Click(object sender, EventArgs e)
    {
        //var keyword = txtSearch.Text.Trim();
        //var groupId = DataHelper.GetId(hGroupId.Value.Trim());
        //var data = EventRegisterUtil.SelectDownloadRaw(groupId, keyword);
        //EventRegisterUtil.GenerateCard(data.First());

        string checkedItems = Request.Form["chkChecked"];
        if (!string.IsNullOrEmpty(checkedItems))
        {
            var ids = DataHelper.ParseCommaSeparatedIdList(checkedItems);
            if (ids.Count > 0)
            {
                if (ids.Count == 1)
                {
                    EventRegisterUtil.DownloadCard(WebUser.Get(ids.First()));
                }
                else
                {
                    // Create temporary folder
                    var tmpPath = WebHelper.MapPath(WebHelper.CombineAddress(WConfig.TempFolder, "EventRegister", "Event2018" + Guid.NewGuid().ToString()));
                    System.IO.Directory.CreateDirectory(tmpPath);

                    foreach (var userId in ids)
                    {
                        EventRegisterUtil.GenerateCard(WebUser.Get(userId), tmpPath);
                    }

                    // Zip them and trigger download
                    WebHelper.DownloadFolder(tmpPath, "Event2018");
                }
            }
        }
    }
</script>

<asp:HiddenField runat="server" ID="hGroupId" Value="-1" />
<div class="row">
    <div class="col-md-12">
        <a runat="server" id="linkDashboard">
            <img src="/Content/Parts/Integration/EventRegister/assets/banner.jpg" class="img-responsive" alt="Brasil 2015" /></a>
        <br />
        <div class="min-bottom-margin">
            <div>
                <div class="btn-group">
                    <button type="submit" id="cmdNewUser" class="btn btn-default btn-sm" runat="server"
                        onserverclick="cmdNewUser_Click">
                        New Attendee</button>
                    <button type="button" class="btn btn-default btn-sm dropdown-toggle" data-toggle="dropdown">
                        <span class="caret"></span>
                        <span class="sr-only">Toggle Dropdown</span>
                    </button>
                    <ul class="dropdown-menu" role="menu">
                        <li>
                            <asp:LinkButton ID="cmdDownload" runat="server" Text="Download CSV"
                                OnClick="cmdDownload_Click"></asp:LinkButton></li>
                    </ul>
                </div>
                <asp:LinkButton ID="cmdDownloadPrint" CssClass="btn btn-primary btn-sm hidden select-actions" OnClick="cmdDownloadPrint_Click" runat="server" Text="Download ID Cards" ToolTip="Download attendees ID Cards for printing"></asp:LinkButton>
                <asp:LinkButton ID="cmdDelete" CssClass="btn btn-danger btn-sm hidden select-actions" OnClientClick="return confirm('Delete selected attendees?');"
                    runat="server" Text="Remove" OnClick="cmdDelete_Click" ToolTip="Remove selected attendees"></asp:LinkButton>
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
            <asp:GridView ID="GridView1" CssClass="table table-borderless table-condensed image-noscale" runat="server" AllowSorting="True"
                AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="ObjectDataSource1"
                ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand"
                AllowPaging="True" PageSize="50">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input type="checkbox" name="chkCheckedMain" class="chk-items-main" onclick="CheckAll(this, 'chkChecked');">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" value='<%# Eval("Id")%>' class="chk-items" name='chkChecked' />
                        </ItemTemplate>
                        <HeaderStyle Width="10px" />
                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <HeaderStyle HorizontalAlign="Center" Width="64px" />
                        <ItemStyle HorizontalAlign="Center" Width="64px" />
                        <ItemTemplate>
                            <a href='<%# Eval("MemberUrl") %>' title="View attendee info" target="_blank">
                                <img src='<%# Eval("PhotoUrl") %>' style="width: 64px" /></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <HeaderStyle HorizontalAlign="Center" Width="10px" />
                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                        <ItemTemplate>
                            <a href='<%# Eval("EditUrl") %>' title="Edit attendee info" target="_blank">
                                <span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="EditUrl"
                        DataTextField="ExternalId" HeaderText="Group ID" SortExpression="ExternalId"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Nickname" HeaderText="Nickname" SortExpression="Nickname"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="MobileNumber" HeaderText="Contact #" SortExpression="MobileNumber"
                        HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="CountryName" HeaderText="Country" SortExpression="CountryName"
                        HeaderStyle-HorizontalAlign="Left" />
                    <%--<asp:BoundField DataField="DateCreated" HeaderText="Created" SortExpression="DateCreated"
                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:yyyy-MMdd}" HtmlEncode="true" />--%>
                    <asp:BoundField DataField="LastUpdate" HeaderText="Last Updated" SortExpression="LastUpdate"
                        HeaderStyle-HorizontalAlign="Left" HtmlEncode="true" Visible="false" />
                    <%--<asp:BoundField DataField="LastLogin" HeaderText="Last Login" SortExpression="LastLogin"
                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:yyyy-MMdd HH:mm}" HtmlEncode="true" />--%>
                    <%--<asp:TemplateField HeaderText="Active" SortExpression="Active">
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ToolTip="Toggle Active Status" EnableViewState="false" runat="server" ImageUrl='<%# WebHelper.SetStateImageInt(Eval("Active")) %>'
                        ID="Image1" CommandName="ToggleActive" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>--%>
                    <asp:HyperLinkField DataNavigateUrlFields="CoordinatorUrl"
                        DataTextField="CoordinatorName" HeaderText="Coordinator" SortExpression="CoordinatorName"
                        HeaderStyle-HorizontalAlign="Left" Visible="false" />
                </Columns>
                <RowStyle BackColor="#f9f9f9" />
                <%--<EditRowStyle BackColor="#2461BF" />--%>
                <%--<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />--%>
                <PagerStyle CssClass="table-pager" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#5C5247" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <AlternatingRowStyle BackColor="White" />
                <PagerSettings PageButtonCount="25" />
            </asp:GridView>
        </div>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
            TypeName="WCMS.WebSystem.Apps.Integration.EventRegister.EventRegisterUtil">
            <SelectParameters>
                <asp:ControlParameter ControlID="hGroupId" DefaultValue="-1" Name="groupId" PropertyName="Value"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="keyword" PropertyName="Text" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        WCMS.Form.SetDefaultSubmit($("#txtSearch"), $("#cmdSearch"));
        var isBatchChange = false;

        // Show/Hide Edit/Delete buttons
        var showHideButtons = function () {
            if (!isBatchChange) { // batch check/uncheck should not be running
                var showSelectActions = false;
                var checkedItems = $('.chk-items:checked');
                var checkedCount = checkedItems.length;
                if (checkedCount == 1) {
                    showSelectActions = true;
                }
                else if (checkedCount > 1) {
                    showSelectActions = true;
                }

                if (showSelectActions)
                    $('.select-actions').removeClass('hidden');
                else
                    $('.select-actions').addClass('hidden');
            }
        }

        // Check/Uncheck all
        $('.chk-items-main').click(function () {
            isBatchChange = true;
            $('.chk-items').attr('checked', $(this).is(':checked'));
            isBatchChange = false;
            showHideButtons();
        });

        // Trigger for any checked/unchecked item
        $('.chk-items').change(function () {
            showHideButtons();
        });
    });
</script>
