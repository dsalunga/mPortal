using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class MyAttendance : System.Web.UI.UserControl
    {
        private IEnumerable<MemberAttendance> monthAttendance = null;
        private IEnumerable<LessonReviewerSession> monthMakeUpPA = null;

        private string attendanceItemFormat = null;
        private string _makeUpUrl;
        private WContext context;

        private const string ABSENT_COLOR = "#DA4D4D";
        private const string LATE_COLOR = "#ffff00";
        private const string ONTIME_COLOR = "#25EF2B";
        private const string MAKEUP_COLOR = "#B4E370";
        private const string MAKEUP_PA_COLOR = "#0000FF";

        protected void Page_Load(object sender, EventArgs e)
        {
            var profileNode = WebRegistry.SelectNode(IntegrationConstants.REG_ProfileNode);
            context = new WContext(this);
            attendanceItemFormat = profileNode.SelectSingleNode("AttendanceItemFormat").Value;
            _makeUpUrl = context.Element.GetParameterValue("MakeUpUrl", "");

            if (!IsPostBack)
            {
                var displayDate = InitMonthPicker();
                int userId = context.GetId(WebColumns.UserId);
                bool viewerIsManager = false;

                if (userId > 0)
                {
                    var managers = context.Element.GetParameterValue("Managers");
                    viewerIsManager = WSession.Current.IsAdministrator || (!string.IsNullOrEmpty(managers) && AccountHelper.IsPresentOrMember(managers));
                }

                var user = (userId > 0 && viewerIsManager ? WebUser.Get(userId) : WSession.Current.User);
                if (user != null)
                {
                    lblName.InnerHtml = AccountHelper.GetPrefixedName(user);
                    var link = MemberLink.Provider.GetByUserId(user.Id);
                    if (link != null)
                        hMemberId.Value = link.MemberId.ToString();

                    if (link == null || link.MemberId == -1)
                    {
                        lblStatus.Text = "You do not have an External account linked to your Portal account.";
                        DisableControls();
                    }
                }

                // Set initial range value
                var startDate = new DateTime(displayDate.Year, displayDate.Month, 1);
                txtFromDate.Text = startDate.ToString("yyyy-MMM-dd");
                txtToDate.Text = startDate.AddMonths(1).AddMinutes(-1).ToString("yyyy-MMM-dd");

                SetHiddenDateValues();
                hMakeUpUrl.Value = _makeUpUrl;
                hGridItemFormat.Value = profileNode.SelectSingleNode("AttendanceGridItemFormat").Value;
            }
        }

        #region Month-Year Picker Methods

        private DateTime InitMonthPicker()
        {
            string dateString = Request["Date"];
            DateTime date = DataUtil.GetDateTime(dateString, DateTime.Now);

            // Setup months
            string[] monthNames = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames;
            for (int i = 1; i < monthNames.Length; i++)
                cboMonth.Items.Add(new ListItem(monthNames[i - 1], i.ToString()));

            cboMonth.SelectedValue = date.Month.ToString();

            // Setup years
            for (int i = date.Year + 10; i > date.Year - 25; i--)
                cboYear.Items.Add(i.ToString());

            cboYear.SelectedValue = date.Year.ToString();

            return date;
        }

        protected void cmdPrevious_Click(object sender, EventArgs e)
        {
            DateTime date = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), 1).AddMonths(-1);
            this.UpdateMonthYearCombo(date);

            ViewAttendance();
        }

        private void DisableControls()
        {
            cmdShowAttendance.Enabled = false;
            monthCalendar.Enabled = false;

            cboMonth.Enabled = false;
            cboYear.Enabled = false;

            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
        }

        protected void cmdNext_Click(object sender, EventArgs e)
        {
            DateTime date = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), 1).AddMonths(1);
            this.UpdateMonthYearCombo(date);

            ViewAttendance();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime newDate = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), 1);
            monthCalendar.VisibleDate = newDate;

            ViewAttendance();
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime newDate = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), 1);
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

        #endregion

        protected void cmdShowAttendance_Click(object sender, EventArgs e)
        {
            ViewAttendance();
        }

        private void ViewAttendance()
        {
            SetHiddenDateValues();

            if (radioRange.Checked)
                GridView1.DataBind();
        }

        private void SetHiddenDateValues()
        {
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            if (radioMonth.Checked)
            {
                // Calendar View
                startDate = new DateTime(
                    Convert.ToInt32(cboYear.SelectedValue),
                    Convert.ToInt32(cboMonth.SelectedValue), 1
                );


                endDate = startDate.AddMonths(1).AddMinutes(-1);
                monthCalendar.VisibleDate = startDate;
                MultiView1.SetActiveView(viewMonth);
            }
            else
            {
                // Grid View
                startDate = DataUtil.GetDateTime(txtFromDate.Text.Trim());
                endDate = DataUtil.GetDateTime(txtToDate.Text.Trim()).AddDays(1).AddMinutes(-1);
                MultiView1.SetActiveView(viewRange);
            }

            hStartDate.Value = startDate.ToString("yyyy-MM-dd");
            hEndDate.Value = endDate.ToString("yyyy-MM-dd");
        }

        public DataSet Select(int memberId, DateTime startDate, DateTime endDate, string makeUpUrl, string gridItemFormat)
        {
            if (memberId > 0)
            {
                LessonReviewerSession makeUpPA = null;
                var makeUpPAItems = LessonReviewerSession.Provider.GetList(startDate, endDate, memberId, LessonReviewerSessionStatus.PendingApproval);
                var context = new WQuery(HttpContext.Current);
                var query = new WQuery(makeUpUrl);
                query.Add(context);

                var client = new MemberSoapClient(false);
                var attendances = client.GetAttendances(memberId, -1, -1, startDate, endDate);

                return DataUtil.ToDataSet(
                            from a in attendances
                            where ((makeUpPA = makeUpPAItems.Count > 0 ?
                                makeUpPAItems.Find(i => a.AttendanceID <= 0 && i.ServiceScheduleID == a.ServiceScheduleID) : null) == null || true)
                            select new
                            {
                                a.AttendanceID,
                                DateTimeIn = makeUpPA != null ? DataUtil.GetDateTime(makeUpPA.DateStarted).ToString("dd-MMM-yyyy HH:mm") : string.IsNullOrEmpty(a.DateTimeIn) ? "" : DataUtil.GetDateTime(a.DateTimeIn).ToString("dd-MMM-yyyy HH:mm"),
                                Remarks = makeUpPA == null ? a.Remarks : "Make-Up: Pending Approval",
                                ServiceDateTime = DataUtil.GetDateTime(a.ServiceDateTime),
                                ServiceType = BuildServiceType(a, query, makeUpUrl, gridItemFormat, makeUpPA),
                                a.Status,
                                a.WeekNo
                            }
                        );
            }

            return DataUtil.GetEmptyDataSet();
        }

        private string BuildServiceType(MemberAttendance item, WQuery query, string makeUpUrl, string gridItemFormat, LessonReviewerSession makeUpPA)
        {
            string bgColor = "";
            string time = "";
            string toolTip = "";
            string foreColor = "#000";
            var shortServiceType = MemberHelper.GetShortService(item.ServiceType);

            if (item.Status.Equals(AttendanceConstants.ABSENT, StringComparison.InvariantCultureIgnoreCase))
            {
                // Check for MakeUp Pending Approval
                if (makeUpPA != null)
                {
                    // MU: Pending Approval
                    bgColor = MAKEUP_PA_COLOR;
                    time = TimeUtil.ToCompactTime(item.ServiceDateTime);
                    toolTip = string.Format("Make-Up: Pending Approval (You can still continue your make-up if you haven't finished this yet) - {0} #{1}", makeUpPA.DateStarted, makeUpPA.Id);
                    foreColor = "#FF7575"; //"#fff";
                }
                else
                {
                    // Absent
                    bgColor = ABSENT_COLOR;
                    time = TimeUtil.ToCompactTime(item.ServiceDateTime);
                    toolTip = string.Format("{0} @ {1} #{2}", item.Status, string.IsNullOrEmpty(item.DateTimeIn) ? item.ServiceDateTime : item.DateTimeIn, item.AttendanceID);
                }
            }
            else
            {
                time = TimeUtil.ToCompactTime(item.DateTimeIn);
                if (item.Status.Equals(AttendanceConstants.LATE, StringComparison.InvariantCultureIgnoreCase))
                    bgColor = LATE_COLOR;
                else if (item.Status.Equals(AttendanceConstants.ON_TIME, StringComparison.InvariantCultureIgnoreCase))
                    bgColor = ONTIME_COLOR;
                else if (item.Status.Equals(AttendanceConstants.MAKE_UP, StringComparison.InvariantCultureIgnoreCase))
                    bgColor = MAKEUP_COLOR;
                toolTip = string.Format("{0} @ {1} #{2}", item.Status, string.IsNullOrEmpty(item.DateTimeIn) ? item.ServiceDateTime : item.DateTimeIn, item.AttendanceID);
            }

            query.Set("ServiceType", shortServiceType);
            query.Set("Date", DataUtil.GetDateTime(item.ServiceDateTime).ToString("yyyy-MM-dd"));
            return string.Format(gridItemFormat,
                            shortServiceType,
                            foreColor,
                            bgColor,
                            toolTip,
                            item.Status.Equals(AttendanceConstants.ABSENT, StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(makeUpUrl) ? query.BuildQuery(true) : "#"
                        );
        }

        public IEnumerable<MemberAttendance> SelectMonth()
        {
            try
            {
                int memberId = DataUtil.GetId(hMemberId.Value);
                if (memberId > 0)
                {
                    var startDate = DataUtil.GetDateTime(hStartDate.Value);
                    var endDate = DataUtil.GetDateTime(hEndDate.Value);
                    return ExternalHelper.GetAttendanceList(startDate.AddDays(-6), endDate.AddDays(6), memberId, -1, -1);
                }
            }
            catch (Exception)
            {
                //LogHelper.WriteLog(ex);
            }
            return new List<MemberAttendance>();
        }

        public List<LessonReviewerSession> SelectMonthMakeUpPA()
        {
            int memberId = DataUtil.GetId(hMemberId.Value);
            if (memberId > 0)
            {
                var startDate = DataUtil.GetDateTime(hStartDate.Value);
                var endDate = DataUtil.GetDateTime(hEndDate.Value);
                return LessonReviewerSession.Provider.GetList(startDate.AddDays(-6), endDate.AddDays(6), memberId, LessonReviewerSessionStatus.PendingApproval);
            }
            return new List<LessonReviewerSession>();
        }

        protected void monthCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            var date = new DateTime(Convert.ToInt32(e.NewDate.Year), Convert.ToInt32(e.NewDate.Month), 1);
            this.UpdateMonthYearCombo(date);

            SetHiddenDateValues();
        }

        protected void monthCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (monthAttendance == null)
                monthAttendance = SelectMonth();
            if (monthMakeUpPA == null)
                monthMakeUpPA = SelectMonthMakeUpPA();

            if (monthAttendance != null && monthAttendance.Count() > 0)
            {
                var date = e.Day.Date;
                var items = monthAttendance.Where(i =>
                    DataUtil.GetDateTime(string.IsNullOrEmpty(i.DateTimeIn) || i.Status.IndexOf("MAKE", StringComparison.InvariantCultureIgnoreCase) >= 0
                    ? i.ServiceDateTime : i.DateTimeIn).ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd"));

                if (items.Count() > 0)
                {
                    var todayText = new StringBuilder();
                    var query = new WQuery(_makeUpUrl);
                    query.Add(context.Query);
                    todayText.AppendFormat(@"<div>{0}</div>", e.Day.DayNumberText);

                    var uniqueItems = new Dictionary<long, MemberAttendance>();
                    foreach (var item in items)
                    {
                        if (!uniqueItems.ContainsKey(item.AttendanceID))
                            uniqueItems.Add(item.AttendanceID, item);
                        else
                            continue;

                        string bgColor = "";
                        string time = "";
                        string toolTip = "";
                        string foreColor = "#000";
                        LessonReviewerSession makeUpPA = null;
                        if (item.Status.Equals(AttendanceConstants.ABSENT, StringComparison.InvariantCultureIgnoreCase))
                        {
                            // Check for MakeUp Pending Approval
                            if (monthMakeUpPA.Count() > 0 && (makeUpPA = monthMakeUpPA.FirstOrDefault(i => i.ServiceScheduleID == item.ServiceScheduleID)) != null)
                            {
                                // MU: Pending Approval
                                bgColor = MAKEUP_PA_COLOR;
                                time = TimeUtil.ToCompactTime(item.ServiceDateTime);

                                toolTip = string.Format("Make-Up: Pending Approval (You can still continue your make-up if you haven't finished this yet) - {0} #{1}", makeUpPA.DateStarted, makeUpPA.Id);
                                foreColor = "#FF7575"; //"#fff";
                            }
                            else
                            {
                                // Absent
                                bgColor = ABSENT_COLOR;
                                time = TimeUtil.ToCompactTime(item.ServiceDateTime);

                                toolTip = string.Format("{0} @ {1} #{2}", item.Status, string.IsNullOrEmpty(item.DateTimeIn) ? item.ServiceDateTime : item.DateTimeIn, item.AttendanceID > 0 ? item.AttendanceID.ToString() : "SvcSchedId" + item.ServiceScheduleID);
                            }
                        }
                        else
                        {
                            time = TimeUtil.ToCompactTime(item.DateTimeIn);

                            if (item.Status.Equals(AttendanceConstants.LATE, StringComparison.InvariantCultureIgnoreCase))
                                bgColor = LATE_COLOR;
                            else if (item.Status.Equals(AttendanceConstants.ON_TIME, StringComparison.InvariantCultureIgnoreCase))
                                bgColor = ONTIME_COLOR;
                            else if (item.Status.Equals(AttendanceConstants.MAKE_UP, StringComparison.InvariantCultureIgnoreCase))
                                bgColor = MAKEUP_COLOR;

                            toolTip = string.Format("{0} @ {1} #{2}", item.Status, string.IsNullOrEmpty(item.DateTimeIn) ? item.ServiceDateTime : item.DateTimeIn, item.AttendanceID);
                        }

                        query.Set("ServiceType", MemberHelper.GetShortService(item.ServiceType));
                        query.Set("Date", DataUtil.GetDateTime(item.ServiceDateTime).ToString("yyyy-MM-dd"));
                        todayText.AppendFormat(attendanceItemFormat,
                            time,
                            MemberHelper.GetShortService(item.ServiceType),
                            foreColor,
                            bgColor,
                            toolTip,
                            item.Status.Equals(AttendanceConstants.ABSENT, StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(_makeUpUrl) ? query.BuildQuery(true) : "#"
                        );
                    }

                    e.Cell.Text = todayText.ToString();
                }
                else
                {
                    e.Cell.Text = string.Format(@"<div>{0}</div>", e.Day.DayNumberText);
                }
            }
        }

        protected void cmdToday_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now.Date;
            this.UpdateMonthYearCombo(date);
            if (!radioMonth.Checked)
                radioMonth.Checked = true;

            ViewAttendance();
        }
    }
}