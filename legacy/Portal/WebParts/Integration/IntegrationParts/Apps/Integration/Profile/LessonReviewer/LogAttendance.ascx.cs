using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Utilities;

using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.AttendanceWS;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Profile.LessonReviewer
{
    public partial class LogAttendance : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;
                var makeUpHomeUrl = element.GetParameterValue("MakeUpHomeUrl");
                var minPlaybackDuration = DataUtil.GetInt32(element.GetParameterValue("MinPlaybackDuration"), 20); // Duration in Minutes
                var session = Session[MakeUpSession.SessionKey] as MakeUpSession;
                if (session == null)
                    session = MemberHelper.TryRecreateSession(context);

                var success = DataUtil.GetBool(context.Get("Success"));
                if (success)
                {
                    panelAttendanceAuth.Visible = false;
                    panelSuccess.Visible = true;
                    if (!string.IsNullOrEmpty(makeUpHomeUrl))
                    {
                        var query = new WQuery(makeUpHomeUrl);
                        var date = context.Get("Date");
                        if (!string.IsNullOrEmpty(date))
                            query.Set("Date", date);
                        linkStartOver.HRef = query.BuildQuery(true);
                    }

                    var leaveCommentUrl = element.GetParameterValue("LeaveCommentUrl");
                    if (!string.IsNullOrEmpty(leaveCommentUrl))
                    {
                        panelLeaveComment.Visible = true;
                        linkLeaveComment.HRef = leaveCommentUrl;
                    }
                }
                else if (session == null)
                {
                    DisplayError("Invalid Make-Up session, you cannot submit your attendance. Please contact the ADDCIT team.");
                    panelAttendanceInfo.Visible = false;
                    panelSubmitAttendance.Visible = false;
                    panelNote.Visible = false;
                }
                else
                {
                    var user = WSession.Current.User;
                    session.DateCompleted = DateTime.Now;
                    Session[MakeUpSession.SessionKey] = session;

                    var timeCompleted = session.DateCompleted;
                    var timeStarted = session.DateStarted;
                    var playbackDuration = timeCompleted - timeStarted;
                    var playbackDurationString = TimeUtil.TimeSpanToString(playbackDuration);
                    lblName.InnerHtml = AccountHelper.GetPrefixedName(user);
                    //lblLocaleGroup.InnerHtml = MemberHelper.GetLocaleGroup(user.Id, "- NONE -");

                    if (timeStarted.Date.Equals(timeCompleted.Date))
                        lblPlaybackDate.InnerHtml = string.Format("{0} / {1} to {2}",
                            timeStarted.ToString("dd-MMM-yyyy"),
                            timeStarted.ToString("h:mm tt"),
                            timeCompleted.ToString("h:mm tt"));
                    else
                        lblPlaybackDate.InnerHtml = string.Format("{0} to {1}",
                            timeStarted.ToString("dd-MMM-yyyy h:mm tt"),
                            timeCompleted.ToString("dd-MMM-yyyy h:mm tt"));


                    lblPlaybackDuration.InnerHtml = string.IsNullOrEmpty(playbackDurationString) ? "0 minutes" : playbackDurationString;
                    lblServiceType.InnerHtml = session.Attendance.ServiceType;
                    lblServiceDate.InnerHtml = session.Attendance.ServiceDateTime;

                    // Attendance Not Required
                    if (IsAttendanceNotRequired(context))
                    {
                        panelAttendanceAuth.Visible = false;
                        panelNotRequired.Visible = true;
                        if (!string.IsNullOrEmpty(makeUpHomeUrl))
                            linkMakeUpHome.HRef = makeUpHomeUrl;
                    }
                    else if (minPlaybackDuration > 0 && playbackDuration.TotalMinutes < minPlaybackDuration)
                    {
                        string errorMsg = string.Format("Your playback duration is too short, minimum playback time required is {0}. You cannot log your attendance.", TimeUtil.TimeSpanToString(new TimeSpan(0, minPlaybackDuration, 0)));
                        DisplayError(errorMsg);
                        panelSubmitAttendance.Visible = false;
                        panelNote.Visible = false;
                    }
                }
            }
        }

        private void DisplayError(string msg)
        {
            lblMsg.Text = msg;
            panelMsg.Visible = true;
        }

        private bool IsAttendanceNotRequired(WContext context)
        {
            var attendance = MemberHelper.GetAttendance(context);
            return (attendance != null && attendance.AttendanceID > 0);
        }

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            string absentReason = txtAbsentReason.Text.Trim();

            // Check Lockout
            if (!string.IsNullOrEmpty(absentReason))
            {
                bool success = false;
                var context = new WContext(this);
                if (!IsAttendanceNotRequired(context))
                {
                    //string additionalNotes = chkNote.Checked ? (radioNote.Checked ? cboNotes.Text : (radioCustomNote.Checked ? txtCustomNote.Text.Trim() : "")) : "";
                    try
                    {
                        var session = Session[MakeUpSession.SessionKey] as MakeUpSession;
                        if (session == null)
                        {
                            session = MemberHelper.TryRecreateSession(context);
                            if (session != null)
                                session.DateCompleted = DateTime.Now;
                        }

                        if (session != null)
                        {
                            var element = context.Element;
                            var set = context.GetParameterSet();
                            var pageId = DataUtil.GetId(ParameterizedWebObject.GetValue("PageId", context.PageId.ToString(), element, set));

                            // Request for Approval here
                            var item = session.GetItem();
                            item.AbsentReason = absentReason;
                            item.AdditionalNotes = string.Empty; //additionalNotes;
                            item.Status = LessonReviewerSessionStatus.PendingApproval;
                            item.PageId = pageId;
                            item.Update();
                            success = true;
                        }
                        else
                        {
                            DisplayError("Invalid session data. Please contact the ADDCIT team.");
                        }
                    }
                    catch (Exception ex)
                    {
                        DisplayError(string.Format("An error has occurred. Please contact the ADDCIT or the assigned worker. Error Message: {0}", ex.Message));
                        WHelper.WriteLogAndSendEmail(ex);
                    }
                }
                else
                {
                    DisplayError("There is already an attendance entry for this Make-Up session.");
                }

                if (success)
                {
                    Session[MakeUpSession.SessionKey] = null;
                    context.Set("Success", "true");
                    context.Redirect();
                }
            }
            else
            {
                DisplayError("Please enter a reason for your being absent during the Gathering / Service.");
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Session[MakeUpSession.SessionKey] = null;
            var context = new WContext(this);
            var makeUpHomeUrl = context.Element.GetParameterValue("MakeUpHomeUrl");
            var date = context.Get("Date");
            var query = new WQuery(makeUpHomeUrl);
            if (!string.IsNullOrEmpty(date))
                query.Set("Date", date);
            CancelRedirect(query.BuildQuery());
        }

        private static void CancelRedirect(string makeUpHomeUrl)
        {
            if (!string.IsNullOrEmpty(makeUpHomeUrl))
                QueryParser.StaticRedirect(makeUpHomeUrl, false);
            else
                QueryParser.StaticRedirect("/", false);
        }
    }
}