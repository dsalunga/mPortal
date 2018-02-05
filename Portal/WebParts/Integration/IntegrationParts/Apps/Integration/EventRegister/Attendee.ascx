<%@ Control Language="C#" AutoEventWireup="true" ClassName="WCMS.WebSystem.Apps.Integration.EventRegister.AttendeeView" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.Framework.Core" %>
<%@ Import Namespace="WCMS.WebSystem.ViewModel" %>
<%@ Import Namespace="WCMS.WebSystem.WebParts.Central" %>
<%@ Register Src="AttendeeForm.ascx" TagName="AttendeeForm" TagPrefix="uc1" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var context = new WContext(this);
            var element = context.Element;
            var groupFilter = element.GetParameterValue("GroupFilter");
            var managerGroupFilter = element.GetParameterValue("CoordinatorGroupFilter");

            var dashboardUrl = element.GetParameterValue("DashboardUrl");
            if (!string.IsNullOrEmpty(dashboardUrl))
                linkDashboard.HRef = dashboardUrl;

            hGroupFilter.Value = groupFilter;
            hManagerGroupFilter.Value = managerGroupFilter;
            FormProfile.Initialize(managerGroupFilter);

            var userId = DataHelper.GetId(Request, WebColumns.UserId);
            var memberId = DataHelper.GetId(Request, IntegrationColumns.MemberId);
            var externalId = DataHelper.Get(Request, IntegrationColumns.ExternalId);
            FormProfile.LoadData(groupFilter, userId, memberId, externalId);

            //if (!WSession.Current.IsAdministrator && user.IsAdministrator())
            //    WQuery.StaticRedirect(WConstants.AbsoluteAccessDeniedPage);
        }
    }

    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        Update();
    }

    private void Update(bool addNew = false)
    {
        var context = new WContext(this);
        var element = context.Element;
        var param = element.GetParameterValue("RegisterSet");
        var paramSet = WebParameterSet.Get(param);

        FormProfile.UpdateData(hGroupFilter.Value, hManagerGroupFilter.Value, paramSet);
        if (addNew)
            RedirectBack(false);
        else
            RedirectBack();
    }

    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        RedirectBack();
    }

    private void RedirectBack(bool removeOpen = true)
    {
        var query = new WQuery(this);
        query.Remove(WebColumns.UserId);
        query.Remove(IntegrationColumns.MemberId);
        query.Remove(IntegrationColumns.ExternalId);
        if (removeOpen)
            query.RemoveOpen();
        query.Redirect();
    }

    protected void cmdUpdateAddNew_Click(object sender, EventArgs e)
    {
        Update(true);
    }
</script>

<asp:HiddenField ID="hGroupFilter" runat="server" Value="" />
<asp:HiddenField ID="hManagerGroupFilter" runat="server" Value="" />
<div id="divGeneral" runat="server">
    <a runat="server" id="linkDashboard">
        <img src="/Content/Parts/Integration/EventRegister/assets/banner.jpg" class="img-responsive" alt="Brasil 2015" /></a>
    <br />
    <uc1:AttendeeForm ID="FormProfile" runat="server" />
</div>
<br />
<div>
    <div class="btn-group">
        <button type="submit" id="cmdUpdate" class="btn btn-primary" runat="server"
            onserverclick="cmdUpdate_Click">
            Update</button>
        <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">
            <span class="caret"></span>
            <span class="sr-only">Toggle Dropdown</span>
        </button>
        <ul class="dropdown-menu" role="menu">
            <li>
                <asp:LinkButton ID="cmdUpdateAddNew"
                    runat="server" Text="Update & Add New" OnClick="cmdUpdateAddNew_Click" ToolTip="Update and Add new user"></asp:LinkButton></li>
        </ul>
    </div>
    <asp:Button ID="cmdCancel" runat="server" Text="Close" OnClick="cmdCancel_Click"
        CausesValidation="False" CssClass="btn btn-default" />
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
<br />
<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
