using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.CommonWS;

namespace WCMS.WebSystem.Integration
{
    public partial class ServicePicker : System.Web.UI.UserControl
    {
        private IEnumerable<ServiceSchedule> monthSchedule = null;
        //private IEnumerable<ServiceVideo> monthVideos = null;

        private const string ABSENT_COLOR = "#DA4D4D";
        private const string LATE_COLOR = "#ffff00";
        private const string ONTIME_COLOR = "#25EF2B";
        private const string MAKEUP_COLOR = "#B4E370";
        private WContext context;

        protected void Page_Load(object sender, EventArgs e)
        {
            context = new WContext(this);

            /*if (!IsPostBack)
            {
                InitMonthPicker();
                var element = context.Element;
                hLocaleId.Value = element.GetParameterValue("LocaleId", ExternalConstants.SGLocale.ToString());

                SetHiddenDateValues();
            }*/
        }

        public string View { get; set; }

        public void Initialize(int localeId)
        {
            InitMonthPicker();
            hLocaleId.Value = localeId.ToString(); //element.GetParameterValue("LocaleId", ExternalConstants.SGLocale.ToString());
            SetHiddenDateValues();
        }

        private void InitMonthPicker()
        {
            var dateString = Request["Date"];
            var date = DataUtil.GetDateTime(dateString, DateTime.Now.Date);

            // Setup months
            var monthNames = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames;
            for (int i = 1; i < monthNames.Length; i++)
                cboMonth.Items.Add(new ListItem(monthNames[i - 1], i.ToString()));
            cboMonth.SelectedValue = date.Month.ToString();

            // Setup years
            for (int i = date.Year + 10; i > date.Year - 25; i--)
                cboYear.Items.Add(i.ToString());
            cboYear.SelectedValue = date.Year.ToString();
        }

        protected void cmdPrevious_Click(object sender, EventArgs e)
        {
            var date = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), 1).AddMonths(-1);
            this.UpdateMonthYearCombo(date);
            ViewAttendance();
        }

        private void DisableControls()
        {
            monthCalendar.Enabled = false;
            cboMonth.Enabled = false;
            cboYear.Enabled = false;
        }

        protected void cmdNext_Click(object sender, EventArgs e)
        {
            var date = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), 1).AddMonths(1);
            this.UpdateMonthYearCombo(date);
            ViewAttendance();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newDate = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), 1);
            monthCalendar.VisibleDate = newDate;
            ViewAttendance();
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newDate = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), 1);
            monthCalendar.VisibleDate = newDate;
            ViewAttendance();
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

        private void ViewAttendance()
        {
            SetHiddenDateValues();
        }

        private void SetHiddenDateValues()
        {
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            // Calendar View
            startDate = new DateTime(
                Convert.ToInt32(cboYear.SelectedValue),
                Convert.ToInt32(cboMonth.SelectedValue), 1
            );

            endDate = startDate.AddMonths(1).AddMinutes(-1);
            monthCalendar.VisibleDate = startDate;
            hStartDate.Value = startDate.ToString("yyyy-MM-dd");
            hEndDate.Value = endDate.ToString("yyyy-MM-dd");
        }

        public IEnumerable<ServiceSchedule> SelectMonth()
        {
            var startDate = DataUtil.GetDateTime(hStartDate.Value);
            var localeId = DataUtil.GetId(hLocaleId.Value);
            if (localeId > 0)
                return ServiceScheduleHelper.GetListAllMonthExtra(startDate, localeId);
            return null;
        }

        protected void monthCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            var date = new DateTime(Convert.ToInt32(e.NewDate.Year), Convert.ToInt32(e.NewDate.Month), 1);
            this.UpdateMonthYearCombo(date);
            SetHiddenDateValues();
        }

        protected void monthCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (monthSchedule == null)
                monthSchedule = SelectMonth();

            //if (monthVideos == null)
            //    monthVideos = ServiceVideo.Provider.GetList(monthCalendar.VisibleDate);

            if (monthSchedule != null && monthSchedule.Count() > 0)
            {
                var date = e.Day.Date;
                var items = monthSchedule.Where(i => i.StartServiceDateTime.Date == date);
                if (items.Count() > 0)
                {
                    var todayText = new StringBuilder();
                    var query = new WQuery(this);

                    if (!string.IsNullOrEmpty(View))
                        query.Set("View", View);
                    todayText.AppendFormat(@"<div>{0}</div>", e.Day.DayNumberText);

                    foreach (var item in items)
                    {
                        //var duration = monthVideos.FirstOrDefault(i => i.ServiceScheduleId == item.ServiceScheduleID);
                        //bool hasDuration = duration != null;
                        string bgColor = ONTIME_COLOR; //hasDuration ? ONTIME_COLOR : ABSENT_COLOR;
                        string time = TimeUtil.ToCompactTime(item.StartServiceDateTime);

                        //query.Set("ServiceType", ProfileHelper.GetShortService(item.ServiceID));
                        query.Set("Date", DataUtil.GetDateTime(date).ToString("yyyy-MM-dd"));
                        query.Set("ServiceScheduleId", item.ServiceScheduleID);
                        todayText.AppendFormat("<div class=\"Event-List-Item service-picker-item\" data-schedule-id='{2}' data-date='{3}' data-time='{0}' data-service='{1}'><span>{0}</span>&nbsp;<strong>{1}</strong></div>",
                            time,
                            MemberHelper.GetShortService(item.ServiceID),
                            item.ServiceScheduleID,
                            DataUtil.GetDateTime(date).ToString("yyyy-MM-dd")
                        );
                    }

                    e.Cell.Text = todayText.ToString();
                }
            }
        }

        protected void cmdToday_Click(object sender, EventArgs e)
        {
            var date = DateTime.Now.Date;
            this.UpdateMonthYearCombo(date);
            ViewAttendance();
        }
    }
}