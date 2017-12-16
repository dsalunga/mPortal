using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Text;
using System.Globalization;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public partial class AdminCalendar : UserControl
    {
        IEnumerable<CalendarEvent> events = null;
        string dayFormat = string.Empty;
        string dayFormatSelected = string.Empty;
        string eventListItemFormat = string.Empty;

        protected TabControl tabView;

        protected void Page_Load(object sender, EventArgs e)
        {
            var eventId = DataHelper.GetId(Request, "EventId");
            if (eventId > 0)
            {
                WContext context = new WContext(this);
                context.SetLoadAndRedirect("AdminEvent.ascx");
            }

            this.InitTabControl();

            dayFormat = WebRegistry.SelectNode("/Apps/EventCalendar/DayFormat").Value;
            dayFormatSelected = WebRegistry.SelectNode("/Apps/EventCalendar/DayFormatSelected").Value;
            eventListItemFormat = WebRegistry.SelectNode("/Apps/EventCalendar/EventListItemFormat").Value;

            if (!Page.IsPostBack)
            {
                string dateString = Request["Date"];
                int calendarId = DataHelper.GetId(Request, "CalendarId");
                DateTime date = DateTimeHelper.ParseTicks(dateString);

                tabView.SelectedIndex = 0;

                //if (Calendar1.VisibleDate.Ticks == 0) Calendar1.VisibleDate = DateTime.Today;
                if (monthCalendar.VisibleDate.Ticks == 0)
                    monthCalendar.VisibleDate = date.Date;


                // Setup months
                string[] monthNames = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames;
                for (int i = 1; i < monthNames.Length; i++)
                    cboMonth.Items.Add(new ListItem(monthNames[i - 1], i.ToString()));

                cboMonth.SelectedValue = date.Month.ToString();


                // Setup years
                for (int i = date.Year + 10; i > date.Year - 25; i--)
                    cboYear.Items.Add(i.ToString());

                cboYear.SelectedValue = date.Year.ToString();


                // Setup Calendar
                cboCalendar.DataSource = CalendarItem.Provider.GetList();
                cboCalendar.DataBind();

                // Check if in EditMode
                WContext context = new WContext(this);
                if (context.ContextType == WContextTypes.EditMode)
                {
                    var tmpCalendarId = DataHelper.GetId(context.Element.GetParameterValue("CalendarId", "-1"));
                    if (tmpCalendarId > 0)
                        calendarId = tmpCalendarId;

                    cboCalendar.Enabled = false;
                }

                ListItem listItem = null;
                if (cboCalendar.Items.Count > 0 && (listItem = cboCalendar.Items.FindByValue(calendarId.ToString())) != null)
                    cboCalendar.SelectedValue = calendarId.ToString();

                //this.BindEventList();
            }
        }

        private void InitTabControl()
        {
            // Setup tab navigation

            if (!Page.IsPostBack)
            {
                tabView.AddTab("tabCalendar", "Calendar");
                tabView.AddTab("tabAllEvents", "Events List");
            }

            tabView.SelectedTabChanged += (object oSender, TabEventArgs args) =>
            {
                switch (args.TabName)
                {
                    case "tabCalendar":
                        tabView.SelectedIndex = 0;
                        mvViews.SetActiveView(viewCalendar);
                        break;

                    case "tabAllEvents":
                        tabView.SelectedIndex = 1;
                        mvViews.SetActiveView(viewAllEvents);
                        this.BindEventList();

                        break;
                }
            };
        }

        private void BindEventList()
        {
            DateTime startDateFrom = DateTime.Now;
            DateTime endDateFrom = DateTime.Now;
            IEnumerable<CalendarEvent> events = null;
            int calendarId = cboCalendar.Items.Count > 0 ? DataHelper.GetId(cboCalendar.SelectedValue) : -1;

            if (chkMonth.Checked)
            {
                startDateFrom = new DateTime(
                    Convert.ToInt32(cboYear.SelectedValue),
                    Convert.ToInt32(cboMonth.SelectedValue), 1
                );
                endDateFrom = startDateFrom.AddMonths(1).AddMilliseconds(-1);

                events = CalendarEvent.GetList(startDateFrom, endDateFrom, calendarId);
            }
            else if (chkYear.Checked)
            {
                startDateFrom = new DateTime(Convert.ToInt32(cboYear.SelectedValue), 1, 1);
                endDateFrom = startDateFrom.AddYears(1).AddMilliseconds(-1);

                events = CalendarEvent.GetList(startDateFrom, endDateFrom, calendarId);
            }
            else
            {
                events = CalendarEvent.GetList(calendarId);
            }

            GridView1.DataSource = DataHelper.ToDataSet(events);
            GridView1.DataBind();
        }

        protected void cmdAddEvent_Click(object sender, EventArgs e)
        {
            int calendarId = cboCalendar.Items.Count > 0 ? DataHelper.GetId(cboCalendar.SelectedValue) : -1;

            var query = new WQuery(this);
            query.Set("Date", monthCalendar.VisibleDate.AddHours(DateTime.Now.Hour).Ticks);
            query.Set("CalendarId", calendarId);
            query.LoadAndRedirect("AdminEvent.ascx");
        }

        private void ReloadData()
        {
            int calendarId = cboCalendar.Items.Count > 0 ? DataHelper.GetId(cboCalendar.SelectedValue) : -1;

            var query = new WQuery(this);
            query.Set("Date", monthCalendar.VisibleDate.Ticks);

            if (calendarId > 0)
                query.Set("CalendarId", calendarId);

            query.Redirect();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Custom_Edit":
                    var query = new WQuery(this);
                    query.Set("EventId", e.CommandArgument);
                    query.LoadAndRedirect("AdminEventView.ascx");

                    break;
            }
        }

        protected void monthCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime date = e.Day.Date;
            int calendarId = cboCalendar.Items.Count > 0 ? DataHelper.GetId(cboCalendar.SelectedValue) : -1;

            if (events == null)
                events = CalendarEvent.GetCalendarEvents(monthCalendar.VisibleDate, calendarId);

            // Filter per date
            var dayEvents = CalendarEvent.GetDayEventsFromSets(events, date);

            StringBuilder todayText = new StringBuilder();
            var query = new WQuery(this);

            if (e.Day.IsSelected)
            {
                query.Set("Load", "AdminEvent.ascx");
                query.Set("Date", date.AddHours(DateTime.Now.Hour).Ticks);

                // Text for selected day
                todayText.AppendFormat(dayFormatSelected,
                    e.SelectUrl,
                    date.ToString("dd MMMM"),
                    e.Day.DayNumberText,
                    query.BuildQuery());
            }
            else
            {
                todayText.AppendFormat(dayFormat,
                    e.SelectUrl,
                    date.ToString("dd MMMM"),
                    e.Day.DayNumberText);
            }

            query.Set("Load", "AdminEventView.ascx");

            foreach (CalendarEvent ev in dayEvents)
            {
                query.Set("Date", date.AddTicks((ev.StartDate - ev.StartDate.Date).Ticks).Ticks);
                query.Set("EventId", ev.Id);

                todayText.AppendFormat(eventListItemFormat,
                    DateTimeHelper.ToCompactTime(ev.StartDate),
                    ev.Subject,
                    query.BuildQuery(),
                    ev.Template.WebForeColor,
                    ev.Template.WebBackColor
                );
            }

            e.Cell.Text = todayText.ToString();
        }

        protected void monthCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            int calendarId = cboCalendar.Items.Count > 0 ? DataHelper.GetId(cboCalendar.SelectedValue) : -1;

            events = CalendarEvent.GetCalendarEvents(monthCalendar.VisibleDate, calendarId);

            this.UpdateMonthYearCombo(monthCalendar.VisibleDate);
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime newVisibleDate = new DateTime(
                monthCalendar.VisibleDate.Year,
                Convert.ToInt32(cboMonth.SelectedValue), 1
            );

            this.SetCurrentDate(newVisibleDate);
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime newVisibleDate = new DateTime(
                Convert.ToInt32(cboYear.SelectedValue),
                monthCalendar.VisibleDate.Month, 1
            );

            this.SetCurrentDate(newVisibleDate);
        }

        private void SetCurrentDate(DateTime date)
        {
            switch (tabView.SelectedIndex)
            {
                case 0:
                    monthCalendar.VisibleDate = date;
                    break;

                case 1:
                    this.BindEventList();
                    break;
            }
        }

        protected void cmdToday_Click(object sender, EventArgs e)
        {
            monthCalendar.VisibleDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            this.UpdateMonthYearCombo(monthCalendar.VisibleDate);

            if (tabView.SelectedIndex == 1)
                this.BindEventList();
        }

        private void UpdateMonthYearCombo(DateTime date)
        {
            string month = date.Month.ToString();
            string year = date.Year.ToString();

            if (cboMonth.SelectedValue != month)
                cboMonth.SelectedValue = month;

            if (cboYear.SelectedValue != year)
                cboYear.SelectedValue = year;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

        }

        protected void chkMonth_CheckedChanged(object sender, EventArgs e)
        {
            this.BindEventList();
        }

        protected void chkYear_CheckedChanged(object sender, EventArgs e)
        {
            this.BindEventList();
        }

        protected void cmdPrevious_Click(object sender, EventArgs e)
        {
            DateTime date = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), 1).AddMonths(-1);
            this.UpdateMonthYearCombo(date);
            this.SetCurrentDate(date);
        }

        protected void cmdNext_Click(object sender, EventArgs e)
        {
            DateTime date = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), 1).AddMonths(1);
            this.UpdateMonthYearCombo(date);
            this.SetCurrentDate(date);
        }

        protected void cmdPreviousYear_Click(object sender, EventArgs e)
        {
            DateTime date = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), 1).AddYears(-1);
            this.UpdateMonthYearCombo(date);
            this.SetCurrentDate(date);
        }

        protected void cmdNextYear_Click(object sender, EventArgs e)
        {
            DateTime date = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), 1).AddYears(1);
            this.UpdateMonthYearCombo(date);
            this.SetCurrentDate(date);
        }

        protected void cboCalendar_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ReloadData();
            SetCurrentDate(monthCalendar.VisibleDate);
        }
    }
}