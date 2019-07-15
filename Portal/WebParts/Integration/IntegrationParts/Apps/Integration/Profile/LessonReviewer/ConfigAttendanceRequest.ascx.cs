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
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;
using WCMS.WebSystem.Apps.Integration.AttendanceWS;

using WCMS.WebSystem.Agent;

namespace WCMS.WebSystem.WebParts.Profile.LessonReviewer
{
    public partial class ConfigAttendanceRequest : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int serviceMakeUpId = DataUtil.GetId(Request, "LessonReviewerSessionId");
                if (serviceMakeUpId > 0)
                {
                    var item = LessonReviewerSession.Provider.Get(serviceMakeUpId);
                    if (item != null)
                        DisplayMakeUpInfo(item);
                }

                var query = new WQuery(this);
                query.Remove("LessonReviewerSessionId");
                query.RemoveOpen();
                linkClose.HRef = query.BuildQuery();
            }
        }

        private void DisplayMakeUpInfo(LessonReviewerSession item)
        {
            if (item == null)
                return;

            var member = item.Member;
            var user = member.User;

            var completed = item.Status == LessonReviewerSessionStatus.Draft ? DateTime.Now : item.DateCompleted;
            var started = item.DateStarted;

            lblName.InnerHtml = AccountHelper.GetPrefixedName(user);
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
            var serviceSchedule = LessonReviewerVideo.Provider.Get(item.ServiceScheduleID);
            if (serviceSchedule != null && serviceSchedule.Duration > 0)
            {
                var serviceDuration = TimeUtil.TimeSpanToString(new TimeSpan(0, 0, serviceSchedule.Duration));
                lblServiceDuration.InnerHtml = string.IsNullOrEmpty(serviceDuration) ? "0 minutes" : serviceDuration;

                panelServiceDuration.Visible = true;
            }

            lblServiceType.InnerHtml = item.ServiceName;
            lblServiceDate.InnerHtml = item.ServiceStartDate.ToString("dd-MMM-yyyy h:mm tt");

            lblReason.InnerHtml = item.AbsentReason;

            if (!string.IsNullOrEmpty(item.AdditionalNotes))
            {
                lblNotes.InnerHtml = item.AdditionalNotes;
                panelAdditionalNotes.Visible = true;
            }

            imageStatus.Src = MemberHelper.SetMakeUpStatusImage(item.Status);
            switch (item.Status)
            {
                case LessonReviewerSessionStatus.PendingApproval:
                    imageStatus.Attributes["title"] = "Pending Councillor Approval";
                    break;

                case LessonReviewerSessionStatus.Approved:
                    imageStatus.Attributes["title"] = "Attendance Approved";
                    break;

                case LessonReviewerSessionStatus.Rejected:
                    imageStatus.Attributes["title"] = "Attendance Rejected";
                    break;
            }

            if (item.Status == LessonReviewerSessionStatus.PendingApproval)
                panelApproval.Visible = true;
            else
            {
                cmdLogin.Visible = false;
                if (item.Status == LessonReviewerSessionStatus.Draft)
                {
                    panelMemberRemarks.Visible = false;
                }
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
        }

        //protected void cmdCancel_Click(object sender, EventArgs e)
        //{
        //    var context = new WContext(this);
        //    context.Remove("LessonReviewerSessionId");
        //    context.Open();
        //}

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            int serviceMakeUpId = context.GetId("LessonReviewerSessionId");
            if (serviceMakeUpId > 0)
            {
                var item = LessonReviewerSession.Provider.Get(serviceMakeUpId);
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
                        var workerUser = WSession.Current.User;

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
                                    var member = item.Member;
                                    var user = member.User;
                                    var templateSuffix = status == LessonReviewerSessionStatus.Approved ? "Approved" : (status == LessonReviewerSessionStatus.Rejected ? "Rejected" : "MessageOnly");

                                    hasTemplate = true;

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

                                    var workerName = AccountHelper.GetPrefixedName(workerUser);
                                    var msg = new WebMessageQueue(workerUser);
                                    msg.AddTo(user); // string.Format("{0};{1}", user.Email, user.MobileNumber);

                                    if (chkSendCopy.Checked)
                                        msg.AddTo(workerUser);
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

                                        if (!string.IsNullOrEmpty(workerUser.MobileNumber))
                                            values.Add("CouncillorName", string.Format("{0} ({1})", workerName, workerUser.MobileNumber));
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

                                Action CreateAttendance = () =>
                                {
                                    AttendanceSoapClient client = new AttendanceSoapClient(false);
                                    if (client.Insert(item.Member.ExternalIdNo, (int)item.ServiceScheduleID, item.ServiceStartDate.ToString("yyyy-MM-dd h:mm tt"), "MakeUp", 0))
                                        statusText.Append("An attendance for the member has been logged to Integration-External.");
                                    else
                                        statusText.Append("An attendance was not logged due to some issues. Kindly ask the ADDCIT team for assistance.");
                                };

                                try
                                {
                                    CreateAttendance();
                                }
                                catch (Exception)
                                {
                                    // Determine if due to existing attendance...
                                    var existing = item.GetAttendance();
                                    if (existing != null && existing.AttendanceID > 0)
                                    {
                                        MemberAttendance.DeleteAttendance((int)existing.AttendanceID);
                                        CreateAttendance();
                                    }
                                    else
                                    {
                                        statusText.Append("An attendance was not logged due to some issues. Kindly ask the ADDCIT team for assistance.");
                                    }
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
                                toUpdate.CouncillorUserId = workerUser.Id;
                                toUpdate.Update();
                            }

                            panelApproval.Visible = false;
                            cmdLogin.Enabled = false;

                            item = items.Find(i => i.Id == item.Id);
                            if (item != null)
                                DisplayMakeUpInfo(item);
                        }
                        else
                        {
                            // Send Mesage Only

                            item.CouncillorUserId = workerUser.Id;
                            item.CouncillorNotes = SetupNewNote(item.CouncillorNotes, noteToMember);
                            item.Update();
                            DisplayMakeUpInfo(item);
                            txtNote.Text = string.Empty;
                        }
                    }

                    lblMsg.Text = statusText.ToString();
                }
            }
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