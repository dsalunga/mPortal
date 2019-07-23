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
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;

namespace WCMS.WebSystem.WebParts.Profile.LessonReviewer
{
    public partial class MakeUpStart : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;
                var date = context.Get("Date");
                var serviceType = context.Get("ServiceType");

                if (string.IsNullOrEmpty(date) || string.IsNullOrEmpty(serviceType))
                {
                    //var makeUpHomeUrl = element.GetParameterValue("MakeUpHomeUrl");
                    //CancelRedirect(makeUpHomeUrl);
                }
                else
                {
                    var user = WSession.Current.User;
                    var link = MemberLink.Provider.GetByUserId(user.Id);
                    if (link != null)
                    {
                        var localeMakeUpWebHandlerUrl = element.GetParameterValue("LocaleMakeUpWebHandlerUrl");
                        if (!string.IsNullOrEmpty(localeMakeUpWebHandlerUrl))
                        {
                            var query = new WQuery(localeMakeUpWebHandlerUrl);
                            query.Set("Method", "Status");
                            query.Set("VarName", "makeUpTest");
                            query.Set("Ticks", DateTime.Now.Ticks);
                            scriptMakeUpTest.Text = WebUtil.BuildScriptTagWithSource(query.BuildQuery(true));
                        }

                        lblName.InnerHtml = AccountHelper.GetPrefixedName(user);
                        //lblLocaleGroup.InnerHtml = MemberHelper.GetLocaleGroup(user.Id, "- NONE -");

                        var attendance = MemberHelper.GetAttendance(context);
                        if (attendance != null)
                        {
                            var items = LessonReviewerSession.Provider.GetList(link.MemberId, LessonReviewerSessionStatus.PendingApproval);
                            if (items.Count > 0 && items.Find(i => i.ServiceScheduleID == attendance.ServiceScheduleID) != null)
                                hAllowed.Value = "0";

                            lblServiceType.InnerHtml = attendance.ServiceType;
                            lblServiceDate.InnerHtml = attendance.ServiceDateTime;

                            var playbackHandler = element.GetParameterValue("PlaybackHandler");
                            if (!string.IsNullOrEmpty(playbackHandler))
                            {
                                var query = new WQuery(playbackHandler);
                                query.Set("ServiceType", serviceType);
                                query.Set("Date", date);
                                query.Set("Method", "Verify");
                                query.Set("VarName", "playbackTest");
                                query.Set("Ticks", DateTime.Now.Ticks);
                                scriptPlaybackTest.Text = WebUtil.BuildScriptTagWithSource(query.BuildQuery(true));
                            }
                        }
                    }
                }
            }
        }

        protected void cmdContinue_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var nextPageUrl = context.Element.GetParameterValue("NextPageUrl");
            if (!string.IsNullOrEmpty(nextPageUrl))
            {
                var attendance = MemberHelper.GetAttendance(context);
                if (attendance != null)
                {
                    // Configure Session
                    var session = Session[MakeUpSession.SessionKey] as MakeUpSession;
                    if (session != null && session.Attendance.AttendanceID != attendance.AttendanceID)
                        Session[MakeUpSession.SessionKey] = null;

                    if (session == null)
                    {
                        session = new MakeUpSession(attendance, DateTime.Now);
                        Session[MakeUpSession.SessionKey] = session;
                    }

                    if (session != null)
                    {
                        // Redirect to News Update
                        var query = new WQuery(nextPageUrl);
                        query.Add(context.Query);
                        query.Set("SessionId", session.DateStarted.Ticks);
                        query.Redirect();
                    }
                }
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

            CancelRedirect(query.BuildQuery(true));
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