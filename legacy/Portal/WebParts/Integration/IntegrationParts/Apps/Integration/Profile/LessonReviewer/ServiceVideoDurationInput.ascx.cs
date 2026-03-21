using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.CommonWS;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile.LessonReviewer
{
    public partial class ServiceVideoDurationInput : System.Web.UI.UserControl
    {
        private IEnumerable<ServiceSchedule> monthSchedule = null;
        private IEnumerable<LessonReviewerVideo> monthVideos = null;

        private string attendanceItemFormat = null;
        //private string _makeUpUrl;
        private WContext context;

        private const string ABSENT_COLOR = "#DA4D4D";
        private const string LATE_COLOR = "#ffff00";
        private const string ONTIME_COLOR = "#25EF2B";
        private const string MAKEUP_COLOR = "#B4E370";

        private const int HOURS_SECS = 3600;

        protected void Page_Load(object sender, EventArgs e)
        {
            var profileNode = WebRegistry.SelectNode(IntegrationConstants.REG_ProfileNode);
            context = new WContext(this);
            attendanceItemFormat = profileNode.SelectSingleNode("AttendanceItemFormat").Value;

            if (!IsPostBack)
            {
                var view = context.Get("View");
                if (!string.IsNullOrEmpty(view) && view.Equals("Input", StringComparison.InvariantCultureIgnoreCase))
                {
                    MultiView1.SetActiveView(viewDurationInput);

                    bool forUpdate = false;
                    int duration = 0;
                    var id = context.GetId("ServiceScheduleId");
                    if (id > 0)
                    {
                        // Got the ServiceSchedule from External
                        var item = LessonReviewerVideo.Provider.Get(id);
                        if (item != null && item.Duration > 0)
                        {
                            duration = item.Duration;
                            forUpdate = true;
                        }

                        var itemExternal = ServiceScheduleHelper.Get(id);
                        if (itemExternal != null)
                        {
                            var service = MemberHelper.GetService(itemExternal.ServiceID);
                            lblServiceName.InnerHtml = string.Format("{0} - {1}", service.ServiceCode, service.Description);
                            lblServiceDate.InnerHtml = itemExternal.StartServiceDateTime.ToString("dd-MMM-yyyy h:mm tt (dddd)");

                            // Setup Fetcher
                            string serviceDate = itemExternal.StartServiceDateTime.ToString("yyyy-MM-dd");
                            string serviceType = MemberHelper.GetShortService(itemExternal.ServiceID);
                            var element = context.Element;

                            var localeMakeUpWebHandlerUrl = element.GetParameterValue("LocaleMakeUpWebHandlerUrl");
                            if (!string.IsNullOrEmpty(localeMakeUpWebHandlerUrl))
                            {
                                var query = new WQuery(localeMakeUpWebHandlerUrl);
                                query.Set("Method", "Status");
                                query.Set("VarName", "makeUpTest");
                                query.Set("Ticks", DateTime.Now.Ticks);
                                scriptMakeUpTest.Text = WebUtil.BuildScriptTagWithSource(query.BuildQuery(true));
                            }

                            var language = "TL";
                            var playbackHandler = element.GetParameterValue("PlaybackHandler");
                            if (!string.IsNullOrEmpty(playbackHandler))
                            {
                                var query = new WQuery(playbackHandler);
                                query.Set("ServiceType", serviceType);
                                query.Set("Date", serviceDate);
                                query.Set("Language", language);
                                query.Set("Ticks", DateTime.Now.Ticks);
                                query.Set("FetchMode", "true");

                                var browser = Request.Browser;
                                if (browser.Browser.Equals("IE", StringComparison.InvariantCultureIgnoreCase) || browser.Browser.Equals("InternetExplorer"))
                                {
                                    mediaPlayer.MovieURL = query.BuildQuery(true);
                                }
                                else
                                {
                                    panelOtherPlayer.Visible = true;
                                    panelWMPlayer.Visible = false;
                                    paramUrl.Attributes["value"] = query.BuildQuery(true);
                                }
                            }
                        }
                    }
                    else
                    {
                        cmdUpdate.Enabled = false;
                    }

                    InitializeInput(forUpdate);
                    if (duration > 0)
                    {
                        int remMins = duration % HOURS_SECS;
                        cboHour.SelectedValue = ((duration - remMins) / HOURS_SECS).ToString();
                        int remSecs = remMins % 60;
                        cboMinute.SelectedValue = ((remMins - remSecs) / 60).ToString();
                    }

                    linkCancel.HRef = RedirectBack(true);
                }
                else
                {
                    InitMonthPicker();

                    int userId = context.GetId(WebColumns.UserId);
                    var user = (userId > 0 ? WebUser.Get(userId) : WSession.Current.User);
                    if (user != null)
                    {
                        var link = MemberLink.Provider.GetByUserId(user.Id);
                        if (link != null)
                            hMemberId.Value = link.MemberId.ToString();

                        if (link == null || link.MemberId == -1)
                        {
                            //lblStatus.Text = "You do not have an External account linked to your Portal account.";
                            DisableControls();
                        }
                        else
                        {
                            var element = context.Element;
                            hLocaleId.Value = element.GetParameterValue("LocaleId", "-1");
                        }
                    }

                    SetHiddenDateValues();
                }
            }
        }

        private void InitializeInput(bool update = false)
        {
            // Setup time
            // Hours
            if (!update)
            {
                cboHour.Items.Add(new ListItem("", "0"));
                cboMinute.Items.Add(new ListItem("", "0"));
                cboMinute.Items.Add(new ListItem("00", "0"));
            }
            else
            {
                cboHour.Items.Add(new ListItem("00", "0"));
                cboMinute.Items.Add(new ListItem("00", "0"));
            }

            for (int i = 1; i <= 99; i++)
                cboHour.Items.Add(new ListItem(i.ToString("00"), i.ToString()));

            // Minutes and seconds
            for (int i = 1; i < 60; i++)
                cboMinute.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
        }

        #region Month-Year Picker Methods

        private void InitMonthPicker()
        {
            string dateString = Request["Date"];
            DateTime date = DataUtil.GetDateTime(dateString, DateTime.Now.Date);

            // Setup months
            string[] monthNames = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames;
            for (int i = 1; i < monthNames.Length; i++)
            {
                cboMonth.Items.Add(new ListItem(monthNames[i - 1], i.ToString()));
            }
            cboMonth.SelectedValue = date.Month.ToString();

            // Setup years
            for (int i = date.Year + 10; i > date.Year - 25; i--)
            {
                cboYear.Items.Add(i.ToString());
            }
            cboYear.SelectedValue = date.Year.ToString();
        }

        protected void cmdPrevious_Click(object sender, EventArgs e)
        {
            DateTime date = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), 1).AddMonths(-1);
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

        private void ViewAttendance()
        {
            SetHiddenDateValues();
        }

        private void SetHiddenDateValues()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;

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
            DateTime startDate = DataUtil.GetDateTime(hStartDate.Value);
            var localeId = DataUtil.GetId(hLocaleId.Value);
            return ServiceScheduleHelper.GetListMonthExtra(startDate, localeId);
        }

        protected void monthCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            DateTime date = new DateTime(Convert.ToInt32(e.NewDate.Year), Convert.ToInt32(e.NewDate.Month), 1);
            this.UpdateMonthYearCombo(date);
            SetHiddenDateValues();
        }

        protected void monthCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (monthSchedule == null)
                monthSchedule = SelectMonth();

            if (monthVideos == null)
                monthVideos = LessonReviewerVideo.Provider.GetList(monthCalendar.VisibleDate);

            if (monthSchedule != null && monthSchedule.Count() > 0)
            {
                var date = e.Day.Date;
                var items = monthSchedule.Where(i => i.StartServiceDateTime.Date == date);
                if (items.Count() > 0)
                {
                    var todayText = new StringBuilder();
                    var query = new WQuery(this);

                    query.Set("View", "Input");
                    todayText.AppendFormat(@"<div>{0}</div>", e.Day.DayNumberText);

                    foreach (var item in items)
                    {
                        var duration = monthVideos.FirstOrDefault(i => i.ServiceScheduleId == item.ServiceScheduleID);
                        bool hasDuration = duration != null;
                        string bgColor = hasDuration ? ONTIME_COLOR : ABSENT_COLOR;
                        string time = TimeUtil.ToCompactTime(item.StartServiceDateTime);

                        //query.Set("ServiceType", ProfileHelper.GetShortService(item.ServiceID));
                        query.Set("Date", DataUtil.GetDateTime(date).ToString("yyyy-MM-dd"));
                        query.Set("ServiceScheduleId", item.ServiceScheduleID);
                        todayText.AppendFormat(attendanceItemFormat,
                            time,
                            MemberHelper.GetShortService(item.ServiceID),
                            "#000",
                            bgColor,
                            hasDuration ? string.Format("Duration: {0}", new TimeSpan(0,0, duration.Duration).ToString("hh\\:mm")) : "No duration yet",
                            query.BuildQuery()
                        );
                    }

                    e.Cell.Text = todayText.ToString();
                }
            }
        }

        protected void cmdToday_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now.Date;
            this.UpdateMonthYearCombo(date);
            ViewAttendance();
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            RedirectBack();
        }

        private string RedirectBack(bool returnLinkOnly = false)
        {
            var query = new WQuery(this);
            query.Remove("View");
            query.Remove("ServiceScheduleId");

            if (returnLinkOnly)
                return query.BuildQuery();
            else
                query.Redirect();

            return "#";
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var id = context.GetId("ServiceScheduleId");
            if (id > 0)
            {
                // Got the ServiceSchedule from External
                var item = LessonReviewerVideo.Provider.Get(id);
                if (item == null)
                {
                    item = new LessonReviewerVideo();
                    item.ServiceScheduleId = id;

                    var itemExternal = ServiceScheduleHelper.Get(id);
                    if (itemExternal != null)
                        item.ServiceStartDate = itemExternal.StartServiceDateTime;
                    else
                        lblMsg.Text = "Unable to get Service Schedule from External.";
                }

                int hours = DataUtil.GetInt32(cboHour.SelectedValue);
                int mins = DataUtil.GetInt32(cboMinute.SelectedValue);
                item.Duration = (hours * HOURS_SECS) + (mins * 60);
                item.Update();

                RedirectBack();
            }
            else
            {
                lblMsg.Text = "Invalid Service Schedule ID.";
            }
        }
    }
}