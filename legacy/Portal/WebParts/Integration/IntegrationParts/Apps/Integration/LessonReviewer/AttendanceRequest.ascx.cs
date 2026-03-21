using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Common;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.Framework.Utilities;
using WCMS.Framework.Net;

using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;
using WCMS.WebSystem.Apps.Integration.AttendanceWS;

using WCMS.WebSystem.Agent;
using Newtonsoft.Json.Linq;
using WCMS.WebSystem.Apps.Integration.CommonWS;

namespace WCMS.WebSystem.Integration.LessonReviewer
{
    public partial class AttendanceRequest : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = DataUtil.GetId(Request, WebColumns.Id);
                if (id > 0)
                {
                    var item = LessonReviewerSession.Provider.Get(id);
                    if (item != null)
                        DisplayMakeUpInfo(item);
                }

                var query = new WQuery(this);
                query.Remove(WebColumns.Id);
                query.RemoveOpen();
                linkClose.HRef = query.BuildQuery();
            }
        }

        private void DisplayMakeUpInfo(LessonReviewerSession item)
        {
            if (item == null)
                return;

            var showMemberRemarksSection = false;
            var member = item.Member;
            var user = member.User;
            var completed = item.Status == LessonReviewerSessionStatus.Draft ? DateTime.Now : item.DateCompleted;
            var started = item.DateStarted;
            ServiceSchedule serviceSchedule = item.ServiceScheduleID > 0 ? ExternalHelper.GetServiceSchedule(item.ServiceScheduleID) : null;

            lblName.InnerHtml = AccountHelper.GetPrefixedName(user);
            linkPhoto.HRef = string.Format("/Account/?UserId={0}", user.Id);
            memberPhoto.Src = member.GetPhotoPathIfNull();
            //lblLocaleGroup.InnerHtml = MemberHelper.GetLocaleGroup(user.Id, "- NONE -");

            if (started.Date.Equals(completed.Date))
                lblPlaybackDate.InnerHtml = string.Format("{0} / {1} to {2}",
                    started.ToString("dd-MMM-yyyy (dddd)"),
                    started.ToString("h:mm tt"),
                    item.Status == LessonReviewerSessionStatus.Draft ? "<em>In Progress</em>" : completed.ToString("h:mm tt"));
            else
                lblPlaybackDate.InnerHtml = string.Format("{0} to {1}",
                    started.ToString("dd-MMM-yyyy h:mm tt"),
                    item.Status == LessonReviewerSessionStatus.Draft ? "<em>In Progress</em>" : completed.ToString("dd-MMM-yyyy h:mm tt"));

            var playbackDuration = TimeUtil.TimeSpanToString(completed - started);
            lblPlaybackDuration.InnerHtml = string.IsNullOrEmpty(playbackDuration) ? "0 minutes" : playbackDuration;

            // Service Duration
            var serviceVideo = LessonReviewerVideo.Provider.Get(item.ServiceScheduleID);
            if (serviceVideo != null && serviceVideo.Duration > 0)
            {
                var serviceDuration = TimeUtil.TimeSpanToString(new TimeSpan(0, 0, serviceVideo.Duration));
                lblServiceDuration.InnerHtml = string.IsNullOrEmpty(serviceDuration) ? "0 minutes" : serviceDuration;
                panelServiceDuration.Visible = true;
            }

            lblServiceType.InnerHtml = item.ServiceName;
            lblServiceSchedule.InnerHtml = (serviceSchedule != null ? serviceSchedule.StartServiceDateTime : item.ServiceStartDate).ToString("f");
            lblLiveStream.InnerHtml = item.ServiceStartDate.ToString("f");
            if(item.AttendanceType == AttendanceTypes.MakeUp)
            {
                lblAttendanceType.InnerHtml = "Make-Up Date";
                panelLiveStream.Visible = false;
            }
            if (!string.IsNullOrEmpty(item.AbsentReason))
            {
                lblReason.InnerHtml = item.AbsentReason;
                fieldComments.Visible = true;
                showMemberRemarksSection = true;
            }

            if (!string.IsNullOrEmpty(item.AdditionalNotes))
            {
                lblNotes.InnerHtml = item.AdditionalNotes;
                panelAdditionalNotes.Visible = true;
                showMemberRemarksSection = true;
            }

            imageStatus.Src = MemberHelper.SetMakeUpStatusImage(item.Status);
            imageStatus.Attributes["title"] = LessonReviewerSessionStatus.GetName(item.Status);

            if (item.Status == LessonReviewerSessionStatus.PendingApproval)
            {
                panelApproval.Visible = true;
            }
            else
            {
                lblDateApproved.InnerHtml = item.DateCompleted.ToString("f");
                cmdSubmit.Visible = false;
                fieldDateApproved.Visible = true;
                lblDateApprovedText.InnerHtml = LessonReviewerSessionStatus.GetName(item.Status);
                if (item.Status == LessonReviewerSessionStatus.Draft)
                    panelMemberRemarks.Visible = false;
                else if (item.Status == LessonReviewerSessionStatus.Rejected || item.Status == LessonReviewerSessionStatus.Approved)
                    cmdReopen.Visible = true;
            }

            if (item.CouncillorUserId > 0)
            {
                panelApprovalInfo.Visible = true;
                panelAssignedCouncillor.Visible = true;
                lblAssignedCouncillor.InnerHtml = AccountHelper.GetPrefixedName(item.Councillor);
            }

            if (!string.IsNullOrEmpty(item.CouncillorNotes))
            {
                panelApprovalInfo.Visible = true;
                panelCouncillorNotes.Visible = true;
                lblCouncillorNotes.InnerHtml = item.CouncillorNotes;
            }

            var coAttendees = string.IsNullOrEmpty(item.Extra) ? (JArray)null : JToken.Parse(item.Extra)["coAttendees"] as JArray;
            if (coAttendees != null && coAttendees.Count > 0)
            {
                foreach (JToken coAttendee in coAttendees)
                {
                    var text = coAttendee["name"].ToString();
                    var userId = DataUtil.GetId(coAttendee["userId"]);
                    if (userId > 0)
                        text += string.Format(" <a href=\"//someorg.org/Account/?UserId={0}\" target=\"_blank\"><span class=\"glyphicon glyphicon-new-window\"></span></a>", userId);

                    var listItem = new ListItem(text, coAttendee["id"].ToString());
                    var status = DataUtil.GetInt32(coAttendee["status"].ToString());
                    listItem.Selected = status == 1;
                    cblCoAttendees.Items.Add(listItem);
                }
                cblCoAttendees.Enabled = false;
                fieldCoAttendees.Visible = true;
                showMemberRemarksSection = true;
            }

            panelMemberRemarks.Visible = showMemberRemarksSection;
        }

        protected void cmdReopen_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            int id = context.GetId(WebColumns.Id);
            if (id > 0)
            {
                var item = LessonReviewerSession.Provider.Get(id);
                if (item != null)
                {
                    item.Status = LessonReviewerSessionStatus.PendingApproval;
                    item.Update();
                    context.Redirect();
                }
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            int id = context.GetId(WebColumns.Id);
            if (id > 0)
            {
                var item = LessonReviewerSession.Provider.Get(id);
                if (item != null)
                {
                    var statusText = new StringBuilder();
                    var noteToMember = txtNote.Text.Trim();
                    bool sendMessage = radioSendMessage.Checked || radioReject.Checked || chkNotes.Checked;
                    if (sendMessage && string.IsNullOrEmpty(noteToMember))
                    {
                        statusText.Append(radioReject.Checked ? "Please enter the reason for rejecting this attendance request." : "Please enter a message.");
                    }
                    else
                    {
                        var status = radioApprove.Checked ? LessonReviewerSessionStatus.Approved : (radioReject.Checked ? LessonReviewerSessionStatus.Rejected : LessonReviewerSessionStatus.PendingApproval);
                        var admin = WSession.Current.User;
                        if (sendMessage && !string.IsNullOrEmpty(noteToMember))
                        {
                            // Send email and SMS here
                            bool hasTemplate = false;
                            var parameterSetName = context.Element.GetParameterValue("MessageTemplateParameterSet");
                            if (!string.IsNullOrEmpty(parameterSetName))
                            {
                                var parameterSet = WebParameterSet.Get(parameterSetName);
                                if (parameterSet != null)
                                {
                                    hasTemplate = true;
                                    var member = item.Member;
                                    var user = member.User;
                                    var templateSuffix = status == LessonReviewerSessionStatus.Approved ? "Approved" : (status == LessonReviewerSessionStatus.Rejected ? "Rejected" : "MessageOnly");
                                    var smsTemplate = parameterSet.GetParameterValue("SMS-Template-" + templateSuffix);
                                    var emailTemplate = parameterSet.GetParameterValue("Email-Template-" + templateSuffix);
                                    var emailSubjectTemplate = parameterSet.GetParameterValue("EmailSubject-Template-" + templateSuffix);
                                    int sendVia = DataUtil.GetInt32(parameterSet.GetParameterValue("Send-Via", "2")); // Default is Both

                                    var values = new NamedValueProvider();
                                    values.Add("MemberFirstName", AccountHelper.GetPrefixedName(user, true));
                                    values.Add("ServiceDate", item.ServiceStartDate.ToString("dd-MMM-yyyy"));
                                    values.Add("MakeUpDate", item.DateStarted.ToString("dd-MMM-yyyy h:mm tt"));
                                    values.Add("ServiceType", MemberHelper.GetShortService(item.ServiceName));
                                    values.Add("Message", noteToMember);

                                    var workerName = AccountHelper.GetPrefixedName(admin);
                                    var msg = new WebMessageQueue(admin);
                                    msg.AddTo(user); // string.Format("{0};{1}", user.Email, user.MobileNumber);

                                    if (chkSendCopy.Checked)
                                        msg.AddTo(admin);
                                    msg.SendVia = sendVia;
                                    msg.ToOrBcc = MessageToOrBccStatus.ToGroup;

                                    if (!string.IsNullOrEmpty(emailTemplate) && !string.IsNullOrEmpty(emailSubjectTemplate)
                                        && (msg.SendVia == MessageSendVia.Email || msg.SendVia == MessageSendVia.EmailAndSms))
                                    {
                                        values.Add("CouncillorName", workerName);

                                        // Format email content here
                                        string emailBody = Substituter.Substitute(emailTemplate, values);
                                        string emailSubject = Substituter.Substitute(emailSubjectTemplate, values);

                                        // Prepare Email
                                        var templateValues = new NamedValueProvider();
                                        templateValues = new NamedValueProvider();
                                        templateValues.Add("Title", emailSubject);
                                        templateValues.Add("Content", emailBody);
                                        templateValues.Add("BaseAddress", WConfig.BaseAddress.TrimEnd('/'));

                                        string emailPath = "~/Content/Parts/Messaging/Templates/EmailMessage.htm";
                                        string message = FileHelper.ReadFile(MapPath(emailPath));
                                        message = Substituter.Substitute(message, templateValues);
                                        // Format relative paths to absolute
                                        msg.EmailMessage = WebMailMessage.FixPaths(message);
                                        msg.EmailSubject = WebMailMessage.PrefixSubject(emailSubject);
                                    }

                                    if (!string.IsNullOrEmpty(smsTemplate) && (msg.SendVia == MessageSendVia.Sms || msg.SendVia == MessageSendVia.EmailAndSms))
                                    {
                                        // Prepare SMS
                                        if (values.ContainsKey("CouncillorName"))
                                            values.Remove("CouncillorName");
                                        if (!string.IsNullOrEmpty(admin.MobileNumber))
                                            values.Add("CouncillorName", string.Format("{0} ({1})", workerName, admin.MobileNumber));
                                        else
                                            values.Add("CouncillorName", workerName);
                                        msg.SmsMessage = Substituter.Substitute(smsTemplate, values);
                                    }
                                    msg.Update();

                                    try
                                    {
                                        AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                                        statusText.Append("A notification has been sent to the member. ");
                                    }
                                    catch (Exception ex)
                                    {
                                        statusText.Append("There was an error sending the notification mesage to the server. However, it was placed on queue by the messaging server and will be sent on the next schedule. ");
                                        LogHelper.WriteLog(ex);
                                    }
                                }
                            }

                            if (!hasTemplate)
                                statusText.Append("There was a problem locating the notification templates. ");
                        }


                        if (status == LessonReviewerSessionStatus.Approved || status == LessonReviewerSessionStatus.Rejected)
                        {
                            if (status == LessonReviewerSessionStatus.Approved)
                            {
                                // Create attendance
                                var memberIds = new Dictionary<int, int>(); // MemberId, Index
                                memberIds.Add(item.MemberId, -1);
                                var extra = string.IsNullOrEmpty(item.Extra) ? (JToken)null : JToken.Parse(item.Extra);
                                var coAttendees = extra == null ? (JArray)null : extra["coAttendees"] as JArray;
                                if (coAttendees != null && coAttendees.Count > 0)
                                {
                                    for (int i = 0; i < coAttendees.Count; i++)
                                    {
                                        var coAttendee = coAttendees[i];
                                        var memberId = (int)coAttendee["memberId"];
                                        var status2 = (int)coAttendee["status"];
                                        if (status2 == 0 && memberId > 0 && !memberIds.ContainsKey(memberId))
                                            memberIds.Add(memberId, i);
                                    }
                                }

                                bool hasCoAttendeeUpdate = false;
                                var attendanceType = item.AttendanceType == AttendanceTypes.MakeUp ? ExternalAttendanceStatus.MakeUp : ExternalAttendanceStatus.HookUp;
                                foreach (var y in memberIds)
                                {
                                    var memberId = y.Key;
                                    var member = MemberLink.Provider.GetByMemberId(memberId);
                                    if (member != null)
                                    {
                                        var result = ExternalHelper.LogAttendance(member.MemberId, (int)item.ServiceScheduleID, item.DateStarted, attendanceType);
                                        if (result.Status == 200)
                                        {
                                            //client.Insert(item.Member.ExternalIdNo, (int)item.ServiceScheduleID, item.ServiceStartDate.ToString("yyyy-MM-dd h:mm tt"), "MakeUp", 0))
                                            statusText.AppendFormat("An attendance for MemberId {0} has been logged to Integration-External.<br/>", memberId);
                                            if (memberId != item.MemberId)
                                            {
                                                coAttendees[y.Value]["status"] = 1;
                                                hasCoAttendeeUpdate = true;
                                            }
                                        }
                                        else
                                            statusText.AppendFormat("An attendance was not logged for MemberId - {0} due to some issues. Kindly ask the ADDCIT team for assistance.</br>", memberId);
                                    }
                                }

                                if (hasCoAttendeeUpdate)
                                {
                                    item.Extra = extra.ToString();
                                    item.Update();
                                }
                            }

                            // Update all pending items
                            var items = LessonReviewerSession.Provider
                                .GetList(item.MemberId, LessonReviewerSessionStatus.PendingApproval)
                                .FindAll(i => i.ServiceScheduleID == item.ServiceScheduleID);

                            foreach (var toUpdate in items)
                            {
                                if (!string.IsNullOrEmpty(noteToMember))
                                    toUpdate.CouncillorNotes = SetupNewNote(toUpdate.CouncillorNotes, noteToMember);

                                toUpdate.Status = status;
                                toUpdate.CouncillorUserId = admin.Id;
                                toUpdate.DateApproved = DateTime.Now;
                                toUpdate.Update();
                            }

                            panelApproval.Visible = false;
                            cmdSubmit.Enabled = false;

                            item = items.Find(i => i.Id == item.Id);
                            if (item != null)
                                DisplayMakeUpInfo(item);
                        }
                        else
                        {
                            // Send Mesage Only
                            item.CouncillorUserId = admin.Id;
                            item.CouncillorNotes = SetupNewNote(item.CouncillorNotes, noteToMember);
                            item.Update();
                            DisplayMakeUpInfo(item);
                            txtNote.Text = string.Empty;
                        }
                    }

                    DisplayMessage(statusText.ToString());
                }
            }
        }

        private void DisplayMessage(string message)
        {
            lblMessage.InnerHtml = message;
            lblMessage.Visible = true;
        }

        private string SetupNewNote(string currentNote, string notes)
        {
            string newNotes = string.Format(@"<strong>""{0}""</strong> on {1}<br/>{2}", notes, DateTime.Now.ToString("dd-MMM HH:mm"), currentNote);
            if (newNotes.Length >= 4000)
                newNotes = newNotes.Substring(0, 3999);
            return newNotes;
        }
    }
}