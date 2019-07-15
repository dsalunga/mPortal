using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;

using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile.LessonReviewer
{
    public partial class Playback : System.Web.UI.UserControl
    {
        public double PrevPos { get; set; }
        public string PrevFile { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;
                var session = Session[MakeUpSession.SessionKey] as MakeUpSession;
                if (session == null)
                    session = MemberHelper.TryRecreateSession(context.Query);

                if (session == null)
                {
                    panelErrorMgs.InnerHtml = "Invalid session data. Please contact the ADDCIT team.";
                }
                else
                {
                    hEnablePlayback.Value = "1";

                    var attendance = session.Attendance; //ProfileHelper.GetAttendance(context);
                    var serviceDateTime = DataUtil.GetDateTime(attendance.ServiceDateTime);
                    var date = serviceDateTime.ToString("yyyy-MM-dd"); // context.Get("Date");
                    var serviceType = MemberHelper.GetShortService(session.Attendance.ServiceType); // context.Get("ServiceType");

                    // Display Service Title
                    
                    //if (attendance != null)
                    lblServiceTitle.InnerHtml = string.Format("{0} ({1:d MMMM yyyy, h:mm tt})", attendance.ServiceType, serviceDateTime);

                    if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(serviceType))
                    {
                        var item = session.GetItem();
                        if (item.Status == LessonReviewerSessionStatus.Draft && item.AdditionalNotes.Contains('|'))
                        {
                            var notes = item.AdditionalNotes.Split('|');
                            var pos = DataHelper.GetDouble(notes[0]);
                            var file = notes[1];
                            if (!string.IsNullOrEmpty(file) && !file.Equals("Playback"))
                            {
                                PrevFile = file;
                                PrevPos = pos;
                            }
                        }

                        var language = context.Get("Language");
                        var playbackHandler = element.GetParameterValue("PlaybackHandler");
                        var localeMakeUpWebHandlerUrl = element.GetParameterValue("LocaleMakeUpWebHandlerUrl");
                        if (!string.IsNullOrEmpty(playbackHandler) && !string.IsNullOrEmpty(localeMakeUpWebHandlerUrl))
                        {
                            if (string.IsNullOrEmpty(language))
                            {
                                var query = new WQuery(localeMakeUpWebHandlerUrl);
                                query.Set("Method", "Status");
                                query.Set("VarName", "makeUpTest");
                                query.Set("Ticks", DateTime.Now.Ticks);
                                scriptMakeUpTest.Text = WebHelper.BuildScriptTagWithSource(query.BuildQuery(true));
                            }
                            else
                            {
                                var play = context.Get("Play");

                                #region Init Script
                                var initQuery = new WQuery(playbackHandler);
                                initQuery.Set("ServiceType", serviceType); // context.Get("ServiceType"));
                                initQuery.Set("Date", date); // context.Get("Date"));
                                initQuery.Set("Language", language);
                                initQuery.Set("Method", "InitPlayback");
                                initQuery.Set("VarName", "makeUpTest");
                                initQuery.Set("SegmentVarName", "mediaSegments");
                                initQuery.Set("Ticks", DateTime.Now.Ticks);
                                if (!string.IsNullOrEmpty(play))
                                {
                                    initQuery.Set("Play", play);
                                    hPlay.Value = play;
                                }
                                scriptMakeUpTest.Text = WebHelper.BuildScriptTagWithSource(initQuery.BuildQuery(true));
                                #endregion

                                hShowExitAlert.Value = "1";

                                cboLanguage.Items.RemoveAt(0);
                                if (cboLanguage.Items.FindByValue(language) != null)
                                    cboLanguage.SelectedValue = language;

                                var query = new WQuery(playbackHandler);
                                query.Set("ServiceType", serviceType); // context.Get("ServiceType"));
                                query.Set("Date", date); // context.Get("Date"));
                                query.Set("Language", language);
                                query.Set("Ticks", DateTime.Now.Ticks);
                                if (!string.IsNullOrEmpty(play))
                                    query.Set("Play", play);
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
                                panelPlayer.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        cboLanguage.Enabled = false;
                    }
                }
            }
        }

        protected void cmdSilentPost_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var play = hPlay.Value;
            if (string.IsNullOrEmpty(play))
                context.Remove("Play");
            else
                context.Set("Play", play);
            context.Redirect();
        }

        protected void cmdSilentLanguage_Click(object sender, EventArgs e)
        {
            var language = cboLanguage.SelectedValue;
            if (!string.IsNullOrEmpty(language))
            {
                var context = new WContext(this);
                context.Set("Language", language);
                context.Redirect();
            }
        }

        //protected void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var language = cboLanguage.SelectedValue;
        //    if (!string.IsNullOrEmpty(language))
        //    {
        //        WContext context = new WContext(this);
        //        context.Set("Language", language);
        //        context.Redirect();
        //    }
        //}

        protected void cmdContinue_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var playbackUrl = context.Element.GetParameterValue("AttendanceLogUrl");
            if (!string.IsNullOrEmpty(playbackUrl))
            {
                context.Remove("Language");
                var query = new WQuery(playbackUrl);
                query.Add(context.Query);
                query.Redirect();
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
                WQuery.StaticRedirect(makeUpHomeUrl, false);
            else
                WQuery.StaticRedirect("/", false);
        }
    }
}