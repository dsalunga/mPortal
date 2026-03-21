<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServicePicker.ascx.cs" Inherits="WCMS.WebSystem.Apps.Integration.ServicePicker" %>
<asp:HiddenField ID="hStartDate" runat="server" Value="" ClientIDMode="Static" />
<asp:HiddenField ID="hEndDate" runat="server" Value="" ClientIDMode="Static" />
<asp:HiddenField ID="hMemberId" runat="server" Value="-1" ClientIDMode="Static" />
<asp:HiddenField ID="hLocaleId" runat="server" Value="-1" ClientIDMode="Static" />
<style type="text/css">
    .service-picker-item-selected {
        background-color: #5cb85c !important;
        color: #fff !important;
    }

    .service-picker-item {
        background-color: #337ab7;
        cursor: pointer;
        color: #fff;
    }
</style>
<div style="margin-bottom: 5px" class="no-bottom-margin form-inline row">
    <div class="col-md-12">
        <asp:DropDownList ID="cboYear" runat="server" CssClass="form-control input-sm control-auto" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
            AutoPostBack="True">
        </asp:DropDownList>
        <span class="hidden-xs">&nbsp;</span>
    <div class="input-group">
        <span class="input-group-btn">
            <button id="cmdPrevious" class="btn btn-default btn-sm" runat="server" onserverclick="cmdPrevious_Click">
                <span class="glyphicon glyphicon-chevron-left"></span>
            </button>
        </span>
        <asp:DropDownList ID="cboMonth" CssClass="form-control control-auto input-sm" runat="server" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
            AutoPostBack="True">
        </asp:DropDownList>
        <span class="input-group-btn">
            <button id="cmdNext" runat="server" class="btn btn-default btn-sm" onserverclick="cmdNext_Click">
                <span class="glyphicon glyphicon-chevron-right"></span>
            </button>
        </span>
    </div>
        <span class="hidden-xs">&nbsp;</span>
            <asp:Button ID="cmdToday" runat="server" Text="Today" CssClass="btn btn-default btn-sm" OnClick="cmdToday_Click" Visible="True" />
    </div>
</div>
<asp:Calendar CssClass="Calendar-Table" ID="monthCalendar" runat="server" BackColor="White"
    BorderColor="#cccccc" BorderWidth="1px" EnableTheming="False" FirstDayOfWeek="Sunday"
    ForeColor="Black" NextPrevFormat="FullMonth"
    OnDayRender="monthCalendar_DayRender" OnVisibleMonthChanged="monthCalendar_VisibleMonthChanged"
    ShowGridLines="True" Width="100%">
    <SelectedDayStyle BackColor="#3366cc" CssClass="Calendar-Day Selected-Calendar-Day" />
    <TodayDayStyle CssClass="Calendar-Day Today-Day" />
    <OtherMonthDayStyle CssClass="Calendar-Day Other-Month-Day" />
    <DayStyle CssClass="Calendar-Day" />
    <NextPrevStyle Font-Bold="True" ForeColor="White" VerticalAlign="Bottom" />
    <DayHeaderStyle CssClass="Day-Header" Font-Bold="True" />
    <TitleStyle BorderWidth="1px" BorderColor="#337ab7" BackColor="#337ab7" CssClass="EventCalendar-Calendar-Title"
        Font-Bold="True" ForeColor="White" Font-Size="Larger" />
</asp:Calendar>