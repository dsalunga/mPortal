using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.Xml.Linq;
using System.Text;
using System.Globalization;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Common.Utilities;

using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public partial class CalendarView : System.Web.UI.UserControl
    {
        IEnumerable<CalendarEvent> events = null;
        string dayFormat = string.Empty;
        string dayFormatSelected = string.Empty;
        string eventListItemFormat = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            dayFormat = WebRegistry.SelectNode("/Apps/EventCalendar/DayFormat").Value;
            dayFormatSelected = WebRegistry.SelectNode("/Apps/EventCalendar/DayFormatSelected").Value;
            eventListItemFormat = WebRegistry.SelectNode("/Apps/EventCalendar/EventListItemFormat").Value;

            if (!Page.IsPostBack)
            {
                WContext context = new WContext(this);
                var tmpCalendarId = DataHelper.GetId(context.Element.GetParameterValue("CalendarId", "-1"));
                if (tmpCalendarId > 0)
                    hCalendarId.Value = tmpCalendarId.ToString();
                else
                    monthCalendar.Enabled = false;

                string dateString = Request["Date"];
                DateTime date = DateTimeHelper.ParseTicks(dateString);

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

                //GridView1.DataBind();
            }
        }

        //public DataSet Select(int selectedMonth, int selectedYear)
        //{
        //    var startDateFrom = new DateTime(selectedYear, selectedMonth, 1);
        //    var endDateFrom = startDateFrom.AddMonths(1).AddMilliseconds(-1);

        //    var events = CalendarEvent.GetList(startDateFrom, endDateFrom)

        //    return DataHelper.ToDataSet(events);
        //}

        protected void monthCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            // Get all Events for current date
            DateTime date = e.Day.Date;
            if (events == null)
                events = CalendarEvent.GetCalendarEvents(monthCalendar.VisibleDate, DataHelper.GetId(hCalendarId.Value));

            // Filter per date
            var dayEvents = CalendarEvent.GetDayEventsFromSets(events, date);

            StringBuilder todayText = new StringBuilder();
            var query = new WQuery(this);
            query.Set(WConstants.Open, "Event");

            //if (e.Day.IsSelected)
            //{
            //    // Text for selected day
            //    todayText.AppendFormat(dayFormatSelected,
            //        e.SelectUrl,
            //        date.ToString("dd MMMM"),
            //        e.Day.DayNumberText,
            //        qs.BuildQuery());
            //}
            //else
            //{

            todayText.AppendFormat(dayFormat,
                e.SelectUrl,
                date.ToString("dd MMMM"),
                e.Day.DayNumberText);

            //}

            foreach (CalendarEvent evnt in dayEvents)
            {
                query.Set("Date", date.AddTicks((evnt.StartDate - evnt.StartDate.Date).Ticks).Ticks);
                query.Set("EventId", evnt.Id);

                todayText.AppendFormat(eventListItemFormat,
                    DateTimeHelper.ToCompactTime(evnt.StartDate),
                    evnt.Subject,
                    query.BuildQuery(),
                    evnt.Template.WebForeColor,
                    evnt.Template.WebBackColor
                );
            }

            e.Cell.Text = todayText.ToString();
        }

        protected void monthCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            events = CalendarEvent.GetCalendarEvents(monthCalendar.VisibleDate, DataHelper.GetId(hCalendarId.Value));

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
            monthCalendar.VisibleDate = date;

            //GridView1.DataBind();
        }

        protected void cmdToday_Click(object sender, EventArgs e)
        {
            monthCalendar.VisibleDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            this.UpdateMonthYearCombo(monthCalendar.VisibleDate);

            //GridView1.DataBind();
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

        protected void chkMonth_CheckedChanged(object sender, EventArgs e)
        {
            //GridView1.DataBind();
        }

        protected void chkYear_CheckedChanged(object sender, EventArgs e)
        {
            //GridView1.DataBind();
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
    }
}