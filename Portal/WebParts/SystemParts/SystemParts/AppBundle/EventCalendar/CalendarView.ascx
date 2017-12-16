<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalendarView.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.EventCalendar.CalendarView" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<div class="event-calendar">
    <asp:HiddenField ID="hCalendarId" runat="server" Value="-1" />
    <asp:Button ID="cmdPreviousYear" ToolTip="Previous Year" runat="server" Text="&lt;&lt;"
        OnClick="cmdPreviousYear_Click" Visible="false" /><asp:Button ID="cmdPrevious" runat="server"
            ToolTip="Previous Month" OnClick="cmdPrevious_Click" Text="&lt;&lt;" />
    <asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:Button ID="cmdNext" runat="server" ToolTip="Next Month" OnClick="cmdNext_Click"
        Text="&gt;&gt;" /><asp:Button ID="cmdNextYear" runat="server" Text="&gt;&gt;" OnClick="cmdNextYear_Click"
            ToolTip="Next Year" Visible="false" />
    &nbsp;
            <asp:Button ID="cmdToday" runat="server" Text="Today" OnClick="cmdToday_Click" />
    <br />
    <br />
    <asp:Calendar ID="monthCalendar" runat="server" BorderWidth="1px"
        Font-Names="Verdana" Font-Size="9pt" NextPrevFormat="FullMonth" ShowGridLines="True"
        FirstDayOfWeek="Sunday" OnDayRender="monthCalendar_DayRender" OnVisibleMonthChanged="monthCalendar_VisibleMonthChanged"
        EnableTheming="False" ClientIDMode="Static" SelectionMode="None" BorderStyle="Solid"
        CssClass="EventCalendar-Calendar">
        <SelectedDayStyle CssClass="Calendar-Day Selected-Calendar-Day" />
        <TodayDayStyle CssClass="Calendar-Day Today-Day" />
        <OtherMonthDayStyle CssClass="Calendar-Day Other-Month-Day" />
        <DayStyle CssClass="Calendar-Day" Width="120px" />
        <NextPrevStyle Font-Bold="True" Font-Size="9pt" VerticalAlign="Bottom" />
        <DayHeaderStyle Font-Bold="True" CssClass="Day-Header" />
        <TitleStyle Font-Bold="True" Font-Size="12pt" CssClass="EventCalendar-Calendar-Title" />
    </asp:Calendar>
    <br />
    <br />
</div>
