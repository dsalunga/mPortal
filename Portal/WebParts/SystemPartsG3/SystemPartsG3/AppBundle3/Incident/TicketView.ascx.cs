using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Net;
using WCMS.Framework.Core;


using WCMS.WebSystem.Controls;

using WCMS.Framework.Diagnostics;
using WCMS.Framework.Utilities;

using WCMS.WebSystem.Agent;
//using WCMS.WebSystem.WebParts.Registration;

namespace WCMS.WebSystem.WebParts.Incident
{
    public partial class TicketView : System.Web.UI.UserControl
    {
        private const string DATE_TIME_FORMAT = "dd-MMM-yyyy h:mm tt";
        private const string DATE_FORMAT = "dd-MMM-yyyy";

        private const string TAB_GENERAL = "tabGeneral";
        private const string TAB_NOTES = "tabNotes";
        private const string TAB_ATTACHMENTS = "tabAttachments";
        private const string TAB_HISTORY = "tabHistory";
        private const string TAB_NOTIFY_ALSO = "tabNotifyAlso";

        protected TabControl TabControl1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var sw = PerformanceLog.StartLog();

                WContext context = new WContext(this);
                var element = context.Element;

                InitializeView(element);

                var set = element.GetParameterSet();
                var user = WSession.Current.User;
                var isSupportUser = IncidentHelper.IsSupportUser(context, user);
                var editing = DataHelper.GetBool(context.Get("Edit"));
                var userDisplayFormat = set.GetParameterValue(IncidentConstants.UserDisplayFormatString);

                hUserDisplayFormatString.Value = userDisplayFormat;
                hBaseGroup.Value = set.GetParameterValue("BaseGroup");

                int id = context.GetId("TicketId");
                var item = id > 0 ? IncidentTicket.Provider.Get(id) : null;
                if (item != null)
                {
                    var isSubmitterOrRequestor = user.Id == item.UserId || user.Id == item.SubmitterId;
                    var isClosedAndSupport = item.Status == TicketStatus.Closed && isSupportUser;
                    var isClosedAndNotSupport = item.Status == TicketStatus.Closed && !isSupportUser;
                    var isCompletedAndToVerify = user.Id == item.UserId && item.Status == TicketStatus.Completed;


                    // View or Update ticket

                    // Check if user has permission
                    if (!WSession.Current.IsAdministrator && !isSupportUser && user.Id != item.UserId && user.Id != item.SubmitterId
                        && (string.IsNullOrEmpty(item.NotifyAlso) || !AccountHelper.IsPresentOrMember(item.NotifyAlso)))
                        Return(true);

                    TabControl1.AddTab("tabHistory", "History");

                    hRecipients.Value = item.NotifyAlso;


                    // Display Info

                    lblDateEntered.InnerHtml = item.DateCreated.ToString(DATE_TIME_FORMAT);
                    lblTicketGuid.InnerHtml = item.TicketGuid;
                    lblAssignedToGroup.InnerHtml = item.AssignedGroup.Name;
                    txtDescription.Text = item.Description;
                    cboUrgency.SelectedValue = item.Urgency.ToString();

                    var sla = IncidentHelper.GetSlaInfo(set);

                    lblRunningTime.InnerHtml = IncidentHelper.DisplayRunningTime(item, sla);



                    if (!editing)
                    {
                        if (item.Status == TicketStatus.Closed)
                            cmdUpdate.Text = "Reopen";
                        else
                            cmdUpdate.Text = "Edit";

                        cmdCancel.Value = "Close";

                        panelNewNote.Visible = false;
                        panelAddAttachments.Visible = false;
                    }
                    else
                    {
                        // Editing...

                        if (item.Status != TicketStatus.Completed && item.Status != TicketStatus.Closed)
                            panelChangeRequestor.Visible = true;

                        var tabIndex = DataUtil.GetInt32(context.Get("Tab"));
                        if (tabIndex > 0)
                            TabControl1.SelectedIndex = tabIndex;
                    }

                    //if (isClosedAndNotSupport)
                    //    cmdUpdate.Visible = false;

                    // ETA
                    if (item.ETA > WConstants.DateTimeMinValue)
                    {
                        var eta = item.ETA.ToString(item.ETA.TimeOfDay.Ticks > 0 ? DATE_TIME_FORMAT : DATE_FORMAT);
                        lblETA.InnerHtml = eta;
                        txtETA.Text = eta;

                        if (!editing || !isSupportUser)
                            datePickerETA.Visible = false;
                        else
                            lblETA.Visible = false;
                    }
                    else if (!editing || !isSupportUser)
                    {
                        // Null and not editing
                        panelETA.Visible = false;
                    }

                    if (item.CategoryId > -1)
                    {
                        WebHelper.RemoveDropDownListItemByValue(cboCategory, -1);
                        cboCategory.SelectedValue = item.CategoryId.ToString();
                        lblCategory.InnerHtml = cboCategory.SelectedItem.Text;
                    }

                    if (item.TypeId > -1)
                    {
                        WebHelper.RemoveDropDownListItemByValue(cboType, -1);
                        cboType.SelectedValue = item.TypeId.ToString();
                        lblType.InnerHtml = cboType.SelectedItem.Text;
                    }

                    if (item.Status > -1)
                    {
                        WebHelper.RemoveDropDownListItemByValue(cboStatus, -1);
                        cboStatus.SelectedValue = item.Status.ToString();
                        lblStatus.InnerHtml = TicketStatus.GetText(item.Status);
                    }

                    if (item.AssignedUserId > 0)
                    {
                        WebHelper.RemoveDropDownListItemByValue(cboAssignedUser, -1);
                        cboAssignedUser.SelectedValue = item.AssignedUserId.ToString();
                        lblAssignedUser.InnerHtml = AccountHelper.FormatUserDisplay(item.AssignedUserId, cboAssignedUser.SelectedItem.Text, userDisplayFormat);
                    }

                    // Requestor
                    var requestor = item.Requestor;
                    if (requestor != null)
                    {
                        lblEnteredBy.InnerHtml = AccountHelper.FormatUserDisplay(requestor, userDisplayFormat, false);
                        hRequestor.Value = requestor.Name;
                    }

                    // Submitter
                    var submitter = item.Submitter;
                    if (submitter != null)
                    {
                        lblSubmitter.InnerHtml = AccountHelper.FormatUserDisplay(submitter, userDisplayFormat, false);
                        hSubmitter.Value = submitter.Name;

                        if (item.UserId != item.SubmitterId)
                            panelSubmitter.Visible = true;
                    }

                    if (!editing || !isSubmitterOrRequestor || isCompletedAndToVerify)
                    {
                        txtDescription.Visible = false;
                        lblDescription.Visible = true;
                        lblDescription.InnerHtml = item.Description;
                    }

                    if (!editing || !isSupportUser)
                    {
                        cboCategory.Visible = false;
                        lblCategory.Visible = true;

                        cboType.Visible = false;
                        lblType.Visible = true;

                        cboUrgency.Visible = false;
                        lblUrgency.Visible = true;
                        lblUrgency.InnerHtml = TicketUrgency.GetText(item.Urgency);

                        if (!(editing && !isSupportUser && (user.Id == item.UserId && (item.Status == TicketStatus.Completed || item.Status == TicketStatus.Closed))))
                        {
                            // Can edit when Completed
                            cboStatus.Visible = false;
                            lblStatus.Visible = true;
                        }

                        cboAssignedUser.Visible = false;
                        lblAssignedUser.Visible = true;
                    }

                    if (!editing)
                        GridViewAttachments.Columns[5].Visible = false;

                    if (user.Id == item.UserId && (item.Status == TicketStatus.Completed || item.Status == TicketStatus.Closed))
                    {
                        if (isCompletedAndToVerify)
                            SetErrorMessage("Your request has been COMPLETED. Kindly verify and confirm if the solution has resolved your request by updating the ticket status to CLOSED or set to ASSIGNED if the your request was not resolved.", AlertMessageTypes.Notification);

                        if (editing && !isSupportUser)
                        {
                            int statusCount = cboStatus.Items.Count - 1;
                            for (int i = statusCount; i >= 0; i--)
                            {
                                ListItem listItem = cboStatus.Items[i];
                                var status = DataUtil.GetInt32(listItem.Value);
                                if (status != TicketStatus.Completed && status != TicketStatus.Assigned && status != TicketStatus.Closed)
                                    cboStatus.Items.Remove(listItem);
                            }
                        }
                    }

                    GridViewAttachments.DataBind();
                    GridViewNotes.DataBind();
                    GridViewHistory.DataBind();

                    if (!editing)
                    {
                        panelNotifyAlsoControls.Visible = false;
                        gridRecipients.Columns[3].Visible = false;
                    }
                }
                else
                {
                    // New Ticket

                    if (!isSupportUser)
                    {
                        panelAssignedTo.Visible = false;
                        panelSupportGroup.Visible = false;
                        panelStatus.Visible = false;
                        panelETA.Visible = false;
                    }
                    else
                    {
                        cboStatus.Items[0].Text = string.Empty;
                        cboAssignedUser.Items[0].Text = string.Empty;
                    }

                    panelChangeRequestor.Visible = true;
                    panelRunningTime.InnerHtml = "&nbsp;";

                    chkSendSMSNew.Visible = true;

                    cboUrgency.SelectedValue = TicketUrgency.Normal.ToString();

                    //if (cboCategory.Items.Count > 0)
                    //    cboCategory.SelectedIndex = 0;

                    string userDisplayName = AccountHelper.FormatUserDisplay(user, userDisplayFormat, false);

                    lblDateEntered.InnerHtml = DateTime.Now.ToString("yyyy-MMM-dd");
                    lblEnteredBy.InnerHtml = userDisplayName;
                    lblSubmitter.InnerHtml = userDisplayName;

                    hRequestor.Value = user.Name;
                    hSubmitter.Value = user.Name;
                }

                // Cancel Link
                context.Remove("Edit");
                if (!editing)
                {
                    context.Remove("TicketId");
                    context.Remove("Tab");
                    context.RemoveOpen();
                }
                WebHelper.CreateButtonLink(cmdCancel, context.BuildQuery());

                PerformanceLog.EndLog(string.Format("Incident-TicketView-Load: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
            }
        }

        protected void TabControl1_SelectedTabChanged(object sender, TabEventArgs e)
        {
            //SelectTab(e.TabName);

            var hasId = DataUtil.GetId(Request, "TicketId") == -1;

            if (hasId && chkSendSMS.Checked && chkSendSMSNew.Visible)
                chkSendSMSNew.Visible = false;

            switch (e.TabName)
            {
                case TAB_GENERAL:
                    MultiView1.SetActiveView(viewGeneral);
                    break;

                case TAB_NOTES:
                    MultiView1.SetActiveView(viewNotes);
                    GridViewNotes.DataBind();
                    break;

                case TAB_ATTACHMENTS:
                    MultiView1.SetActiveView(viewAttachments);
                    GridViewAttachments.DataBind();
                    break;

                case TAB_HISTORY:
                    MultiView1.SetActiveView(viewHistory);
                    GridViewHistory.DataBind();
                    break;

                case TAB_NOTIFY_ALSO:
                    MultiView1.SetActiveView(viewNotifyAlso);
                    gridRecipients.DataBind();
                    break;
            }
        }

        private void SelectTab(string tabName)
        {
            TabControl1.SelectedTab = tabName;
        }

        private void InitializeView(PageElementBase element)
        {
            // Add Tabs
            TabControl1.AddTab("tabGeneral", "General");
            TabControl1.AddTab("tabAttachments", "Attachments");
            TabControl1.AddTab("tabNotes", "Notes");
            TabControl1.AddTab("tabNotifyAlso", "Also Notify");

            hBatchGuid.Value = Guid.NewGuid().ToString("N").ToUpperInvariant();

            // Set Membership information

            var supportGroup = IncidentHelper.GetBaseSupportGroup(element.GetParameterSet());
            if (supportGroup != null)
            {
                cboAssignedUser.DataSource = from i in supportGroup.Users //.OrderBy(i => i.LastName)
                                             orderby i.LastName
                                             select new
                                             {
                                                 i.Id,
                                                 Name = AccountHelper.GetPrefixedName(i)
                                             };
                cboAssignedUser.DataBind();
            }

            // Urgency
            cboUrgency.DataSource = from i in TicketUrgency.Values
                                    select new
                                    {
                                        Id = i.Key,
                                        Name = i.Value
                                    };
            cboUrgency.DataBind();

            // Status
            cboStatus.DataSource = from i in TicketStatus.Values
                                   select new
                                   {
                                       Id = i.Key,
                                       Name = i.Value
                                   };
            cboStatus.DataBind();

            // Category
            cboCategory.DataSource = IncidentCategory.Provider.GetList();
            cboCategory.DataBind();

            // Type
            cboType.DataSource = IncidentType.Provider.GetList();
            cboType.DataBind();
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Return();
        }

        private void Return(bool force = false)
        {
            WContext context = new WContext(this);
            var editMode = DataHelper.GetBool(context.Get("Edit"));
            int id = context.GetId("TicketId");

            if (force || !editMode || id <= 0)
            {
                context.Remove("TicketId");
                context.Remove("Edit");
                context.Remove("Tab");
                context.Open();
            }
            else
            {
                context.Remove("Edit");
                context.Redirect();
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var editMode = DataHelper.GetBool(context.Get("Edit"));
            int id = context.GetId("TicketId");

            if (editMode || id <= 0)
            {
                Update(context);
            }
            else
            {
                if (TabControl1.SelectedIndex > 0)
                    context.Set("Tab", TabControl1.SelectedIndex);
                else
                    context.Remove("Tab");

                context.Set("Edit", "1");
                context.Redirect();
            }
        }

        private void Update(WContext context)
        {
            #region Validation

            var description = txtDescription.Text.Trim();
            if (string.IsNullOrEmpty(description))
            {
                SetErrorMessage("Please enter a Description");
                return;
            }

            #endregion

            string fieldLabel;
            string oldValue;
            string newValue;

            var element = context.Element;
            var user = WSession.Current.User;
            var isInsert = true;
            var isCompletedAndToVerify = false;
            var isSupportUser = IncidentHelper.IsSupportUser(context, user);
            var isSubmitterOrRequestor = true;
            var paramSet = element.GetParameterSet();

            var emailUpdateAlertTemplate = paramSet.GetParameterValue("Email-UpdateAlert");
            var emailUpdateAlertRowTemplate = paramSet.GetParameterValue("Email-UpdateAlert-UpdateRow");
            var emailUpdateAlertTableTemplate = paramSet.GetParameterValue("Email-UpdateAlert-UpdateTable");
            var emailSubjectPrefix = paramSet.GetParameterValue("Email-Subject-Prefix");
            var emailMultilineContentFormat = paramSet.GetParameterValue("Email-Multiline-Content-Format");
            var alertType = DataUtil.GetInt32(paramSet.GetParameterValue("Alert-Type"));
            var ticketPrefix = paramSet.GetParameterValue("Incident-Prefix");
            var monitoringTeam = paramSet.GetParameterValue("Monitoring-Team");

            StringBuilder alertContent = new StringBuilder();
            StringBuilder historyContent = new StringBuilder();


            var commentText = txtNewNote.Text.Trim();
            var supportUserId = DataUtil.GetInt32(cboAssignedUser.SelectedValue);
            var status = DataUtil.GetInt32(cboStatus.SelectedValue);

            var requestorName = hRequestor.Value.Trim();
            var requestor = WebUser.Get(requestorName);

            var notifyAlso = hRecipients.Value.Trim();


            int id = context.GetId("TicketId");
            var item = id > 0 ? IncidentTicket.Provider.Get(id) : null;
            if (item != null)
            {
                isCompletedAndToVerify = user.Id == item.UserId && item.Status == TicketStatus.Completed;
                isSubmitterOrRequestor = user.Id == item.UserId || user.Id == item.SubmitterId;

                // Update

                #region Validation

                if (supportUserId <= 0 && ((isSupportUser && !isSubmitterOrRequestor) || status != TicketStatus.Submitted))
                {
                    SetErrorMessage("Please assign the ticket to a support user.");
                    return;
                }

                if (string.IsNullOrEmpty(commentText))
                {
                    SetErrorMessage("Please enter the reason or description of your change in the Notes section.");
                    TabControl1.SelectTabByName("tabNotes");
                    return;
                }

                #endregion

                isInsert = false;

                if (isSupportUser || (user.Id == item.UserId && (item.Status == TicketStatus.Completed || item.Status == TicketStatus.Closed)))
                {
                    if (status != item.Status)
                    {
                        #region Alert & History Content

                        fieldLabel = "Status";
                        oldValue = TicketStatus.GetText(item.Status);
                        newValue = TicketStatus.GetText(status);

                        alertContent.Append(BuildUpdateRowContent(emailUpdateAlertRowTemplate, fieldLabel, oldValue, newValue));
                        historyContent.Append(BuildHistoryContent(fieldLabel, oldValue, newValue));

                        #endregion

                        item.Status = status;

                        if (item.Status == TicketStatus.Completed || item.Status == TicketStatus.Closed)
                            item.DateClosed = DateTime.Now;
                        else
                            item.DateClosed = WConstants.DateTimeMinValue;
                    }
                }

                var oldRequestor = item.Requestor;
                // Update requestor when (newRequestor!=null && oldRequestor==null) OR (new!=old)
                if ((oldRequestor == null && requestor != null) || (oldRequestor != null && requestor != null && oldRequestor.Id != requestor.Id))
                {
                    fieldLabel = "Requestor";
                    oldValue = AccountHelper.GetPrefixedName(oldRequestor);
                    newValue = AccountHelper.GetPrefixedName(requestor);

                    alertContent.Append(BuildUpdateRowContent(emailUpdateAlertRowTemplate, fieldLabel, oldValue, newValue));
                    historyContent.Append(BuildHistoryContent(fieldLabel, oldValue, newValue));

                    item.UserId = requestor.Id;
                }
            }
            else
            {
                // Insert

                #region Validation: Category, Request Type

                if (DataUtil.GetInt32(cboCategory.SelectedValue) <= 0)
                {
                    SetErrorMessage("Please select a Request Category");
                    return;
                }

                if (DataUtil.GetInt32(cboType.SelectedValue) <= 0)
                {
                    SetErrorMessage("Please select a Request Type");
                    return;
                }

                #endregion

                item = new IncidentTicket();
                item.UserId = requestor != null ? requestor.Id : user.Id;
                item.SubmitterId = user.Id;
                item.DateCreated = DateTime.Now;
                item.TicketGuid = IncidentHelper.GenerateTicketGuid(ticketPrefix);
                item.Status = status == -1 || !isSupportUser ? TicketStatus.Submitted : status;
            }

            if ((isInsert || isSubmitterOrRequestor) && !isCompletedAndToVerify)
            {
                #region Alert & History Content: Description

                if (!isInsert && !item.Description.Equals(description))
                {
                    string descriptionFormatted = string.IsNullOrEmpty(emailMultilineContentFormat) ? description : string.Format(emailMultilineContentFormat, description);

                    fieldLabel = "Description";
                    oldValue = string.IsNullOrEmpty(emailMultilineContentFormat) ? item.Description : string.Format(emailMultilineContentFormat, item.Description);

                    alertContent.Append(BuildUpdateRowContent(emailUpdateAlertRowTemplate, fieldLabel, oldValue, descriptionFormatted));
                    historyContent.Append(BuildHistoryContent(fieldLabel, item.Description, description));
                }

                #endregion

                item.Description = description;
            }

            if (isSupportUser)
            {
                var eta = DataUtil.GetDateTime(txtETA.Text.Trim(), WConstants.DateTimeMinValue);
                if (eta > WConstants.DateTimeMinValue && item.ETA != eta)
                {
                    #region Validation: ETA

                    if (eta <= DateTime.Now)
                    {
                        SetErrorMessage("ETA must not be earlier than today.");
                        return;
                    }

                    #endregion

                    #region Alert & History Content: ETA

                    if (!isInsert && item.ETA != eta)
                    {
                        fieldLabel = "ETA";
                        oldValue = item.ETA.Equals(WConstants.DateTimeMinValue) ? "" : item.ETA.ToString(eta.TimeOfDay.Ticks > 0 ? DATE_TIME_FORMAT : DATE_FORMAT);
                        newValue = eta.ToString(eta.TimeOfDay.Ticks > 0 ? DATE_TIME_FORMAT : DATE_FORMAT);

                        alertContent.Append(BuildUpdateRowContent(emailUpdateAlertRowTemplate, fieldLabel, oldValue, newValue));
                        historyContent.Append(BuildHistoryContent(fieldLabel, oldValue, newValue));
                    }

                    #endregion

                    item.ETA = eta;
                }
            }

            if (isInsert || isSupportUser)
            {
                var categoryId = DataUtil.GetId(cboCategory.SelectedValue);
                var typeId = DataUtil.GetId(cboType.SelectedValue);
                var urgency = DataUtil.GetInt32(cboUrgency.SelectedValue);

                #region Alert & History Content: Request Category, Request Type, Urgency

                if (!isInsert && item.CategoryId != categoryId)
                {
                    fieldLabel = "Request Category";
                    oldValue = IncidentHelper.GetCategoryName(item.CategoryId);
                    newValue = IncidentHelper.GetCategoryName(categoryId);

                    alertContent.Append(BuildUpdateRowContent(emailUpdateAlertRowTemplate, fieldLabel, oldValue, newValue));
                    historyContent.Append(BuildHistoryContent(fieldLabel, oldValue, newValue));
                }

                if (!isInsert && item.TypeId != typeId)
                {
                    fieldLabel = "Request Type";
                    oldValue = IncidentHelper.GetTypeName(item.TypeId);
                    newValue = IncidentHelper.GetTypeName(typeId);

                    alertContent.Append(BuildUpdateRowContent(emailUpdateAlertRowTemplate, fieldLabel, oldValue, newValue));
                    historyContent.Append(BuildHistoryContent(fieldLabel, oldValue, newValue));
                }

                if (!isInsert && item.Urgency != urgency)
                {
                    fieldLabel = "Urgency";
                    oldValue = TicketUrgency.GetText(item.Urgency);
                    newValue = TicketUrgency.GetText(urgency);

                    alertContent.Append(BuildUpdateRowContent(emailUpdateAlertRowTemplate, fieldLabel, oldValue, newValue));
                    historyContent.Append(BuildHistoryContent(fieldLabel, oldValue, newValue));
                }

                #endregion

                item.CategoryId = categoryId;
                item.TypeId = typeId;
                item.Urgency = urgency;

                if (isInsert)
                {
                    // Set default assigned group
                    //var defaultGroup = WebGroup.SelectNode(paramSet.GetParameterValue("DefaultAssignedGroup"));
                    //if (defaultGroup != null)
                    //    item.AssignedGroupId = defaultGroup.Id;

                    // If Support, allow to assign Support User and Group
                    if (isSupportUser && supportUserId > 0)
                    {
                        var supportUser = WebUser.Get(supportUserId);
                        if (supportUser != null)
                        {
                            item.AssignedUserId = supportUserId;

                            var supportGroup = IncidentHelper.GetSupportGroup(context, supportUser);
                            if (supportGroup != null)
                                item.AssignedGroupId = supportGroup.Id;
                        }
                    }

                    if (categoryId > 0 && item.AssignedGroupId == -1)
                    {
                        var category = IncidentCategory.Provider.Get(categoryId);
                        if (category != null)
                            item.AssignedGroupId = category.GroupId;
                    }
                }
                else
                {
                    var supportUser = WebUser.Get(supportUserId);
                    if (supportUser != null)
                    {
                        if (item.AssignedUserId != supportUserId)
                        {
                            #region Alert & History Content: Assigned To

                            fieldLabel = "Assigned To";
                            oldValue = IncidentHelper.GetUserDisplayName(item.AssignedUserId);
                            newValue = AccountHelper.GetPrefixedName(supportUser);

                            alertContent.Append(BuildUpdateRowContent(emailUpdateAlertRowTemplate, fieldLabel, oldValue, newValue));
                            historyContent.Append(BuildHistoryContent(fieldLabel, oldValue, newValue));

                            #endregion

                            item.AssignedUserId = supportUserId;
                        }

                        var supportGroup = IncidentHelper.GetSupportGroup(context, supportUser);
                        if (supportGroup != null && item.AssignedGroupId != supportGroup.Id)
                        {
                            #region Alert & History Content: Support Group

                            fieldLabel = "Support Group";
                            oldValue = IncidentHelper.GetGroupName(item.AssignedGroupId);

                            alertContent.Append(BuildUpdateRowContent(emailUpdateAlertRowTemplate, fieldLabel, oldValue, supportGroup.Name));
                            historyContent.Append(BuildHistoryContent(fieldLabel, oldValue, supportGroup.Name));

                            #endregion

                            item.AssignedGroupId = supportGroup.Id;
                        }
                    }
                }
            }

            item.NotifyAlso = notifyAlso;
            item.Update();

            #region Send Alert & History Content: Notes

            if (commentText.Length > 0)
            {
                var commentTextFormatted = string.IsNullOrEmpty(emailMultilineContentFormat) ? commentText : string.Format(emailMultilineContentFormat, commentText);

                fieldLabel = "Notes";
                alertContent.Append(BuildUpdateRowContent(emailUpdateAlertRowTemplate, fieldLabel, string.Empty, commentTextFormatted));
            }

            var batchGuid = hBatchGuid.Value;
            if (!string.IsNullOrEmpty(batchGuid))
            {
                var attachments = WebAttachment.Provider.GetList(hBatchGuid.Value);
                if (attachments.Count() > 0)
                {
                    StringBuilder sbAlertContent = new StringBuilder();
                    StringBuilder sbHistoryContent = new StringBuilder();

                    fieldLabel = "Attachments";

                    foreach (var attachment in attachments)
                    {
                        sbAlertContent.AppendFormat("{0}<br/>", attachment.Name);
                        sbHistoryContent.AppendFormat("{0}{1}", attachment.Name, Environment.NewLine);
                    }

                    alertContent.Append(BuildUpdateRowContent(emailUpdateAlertRowTemplate, fieldLabel, string.Empty, sbAlertContent.ToString()));
                    historyContent.Append(BuildHistoryContent(fieldLabel, "N/A", sbHistoryContent.ToString()));
                }
            }

            if (historyContent.Length > 0)
            {
                var history = new IncidentTicketHistory();
                history.TicketId = item.Id;
                history.UserId = user.Id;
                history.DateCreated = DateTime.Now;
                history.Content = historyContent.ToString();
                history.Update();
            }

            if (alertContent.Length > 0)
            {
                alertContent.Append(BuildUpdateRowContent(emailUpdateAlertRowTemplate, "Modified By", string.Empty, AccountHelper.GetPrefixedName(user, NamePrefixes.Brotherhood)));
                alertContent.Append(BuildUpdateRowContent(emailUpdateAlertRowTemplate, "Date Modified", string.Empty, DateTime.Now.ToString(DATE_TIME_FORMAT)));
            }

            if (isInsert || alertContent.Length > 0)
            {
                var newStatus = TicketStatus.GetText(item.Status);
                var site = context.Site;

                QueryParser query = context.Query.Clone();
                query.Remove("Tab");
                query.Remove("Edit");
                query.Remove("Show");
                query.Remove("FilterBy");
                query.Set("TicketId", item.Id);

                NamedValueProvider values = new NamedValueProvider();
                values.Add("TICKET_GUID", item.TicketGuid);
                values.Add("DESCRIPTION", item.Description);
                values.Add("SUBMITTED_BY", IncidentHelper.GetUserDisplayName(item.UserId));
                values.Add("DATE_CREATED", item.DateCreated.ToString(DATE_TIME_FORMAT));
                values.Add("CATEGORY", IncidentHelper.GetCategoryName(item.CategoryId));
                values.Add("TYPE", IncidentHelper.GetTypeName(item.TypeId));
                values.Add("URGENCY", TicketUrgency.GetText(item.Urgency));
                values.Add("STATUS", newStatus);
                values.Add("ASSIGNED_TO", IncidentHelper.GetUserDisplayName(item.AssignedUserId));
                values.Add("SUPPORT_GROUP", IncidentHelper.GetGroupName(item.AssignedGroupId));
                values.Add("URL", WebHelper.CombineAddress(site.BuildAbsoluteUrl(), query.BuildQuery()));

                values.Add("TICKET_UPDATES", alertContent.Length > 0 ? Substituter.Substitute(emailUpdateAlertTableTemplate, "UPDATE_ROWS", alertContent.ToString()) : string.Empty);

                WebMessageQueue alertMsg = new WebMessageQueue(user);
                alertMsg.SendVia = alertType;
                alertMsg.ToOrBcc = MessageToOrBccStatus.ToGroup;
                alertMsg.EmailMessage = Substituter.Substitute(emailUpdateAlertTemplate, values);
                alertMsg.EmailSubject = string.Format("{0} {1}: {2} - {3}", emailSubjectPrefix, item.TicketGuid, newStatus, DataHelper.GetStringPreview(item.Description, WConstants.PreviewChars));

                if (chkSendSMS.Checked)
                {
                    var smsNoteAlertTemplate = paramSet.GetParameterValue("SMS-NoteAlert");
                    if (string.IsNullOrEmpty(smsNoteAlertTemplate))
                        smsNoteAlertTemplate = "$(TICKET_GUID): Notes from $(NOTE_FROM) - $(NOTE)";

                    values = new NamedValueProvider();
                    values.Add("TICKET_GUID", item.TicketGuid);
                    values.Add("NOTE_FROM", AccountHelper.GetPrefixedName(user, NamePrefixes.Brotherhood, true));
                    values.Add("FROM_NUMBER", user.MobileNumber);
                    values.Add("FROM_NUMBER_FORMATTED", !string.IsNullOrEmpty(user.MobileNumber) ? string.Format(" ({0})", user.MobileNumber) : "");
                    values.Add("NOTE", commentText);

                    alertMsg.SmsMessage = Substituter.Substitute(smsNoteAlertTemplate, values);
                }

                // Add current user
                alertMsg.AddTo(user);

                // Add Requestor
                if (requestor != null && requestor.Id != user.Id)
                    alertMsg.AddTo(requestor);

                // Add Submitter
                var submitter = item.Submitter;
                if (submitter != null)
                {
                    if (requestor != null)
                    {
                        if (requestor.Id != submitter.Id)
                            alertMsg.AddTo(submitter);
                    }
                    else
                    {
                        alertMsg.AddTo(submitter);
                    }
                }

                // Add Support
                var supportUser = item.AssignedUser;
                if (supportUser != null && supportUser.Id != user.Id)
                    alertMsg.AddTo(supportUser);

                // Add Group Owner
                var supportGroup = item.AssignedGroup;
                if (supportGroup != null)
                {
                    var groupOwner = supportGroup.Owner;
                    if (groupOwner != null)
                        alertMsg.AddTo(groupOwner);
                }

                // Add Monitoring Team
                if (!string.IsNullOrEmpty(monitoringTeam))
                {
                    var monitoringGroup = WebGroup.SelectNode(monitoringTeam);
                    if (monitoringGroup != null)
                        alertMsg.AddTo(monitoringGroup.Users);
                }

                if (!string.IsNullOrEmpty(notifyAlso))
                    alertMsg.AddTo(notifyAlso);

                alertMsg.Update();

                try
                {
                    AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME, true);
                }
                catch (Exception ex)
                {
                    SetErrorMessage("There was an error sending the notification mesage to the server. However, it was placed on queue by the messaging server and will be sent on the next schedule. ");
                    LogHelper.WriteLog(ex);
                }
            }

            #endregion

            if (isInsert) // Make attachments permanent
                WebAttachment.MarkPermanent(hBatchGuid.Value, item.Id);

            if (!string.IsNullOrEmpty(commentText))
            {
                var comment = new WebComment(IncidentConstants.INCIDENT_TICKET_ID, item.Id);
                comment.Content = commentText;
                comment.Update();
            }

            if (isInsert)
            {
                context.Set("TicketId", item.Id);
                context.Redirect();
            }
            else
            {
                Return();
            }
        }

        private static string BuildUpdateRowContent(string emailUpdateAlertRowTemplate, string fieldLabel, string oldValue, string newValue)
        {
            var values = new NamedValueProvider();
            values.Add("FIELD_LABEL", fieldLabel);
            values.Add("FIELD_VALUE", oldValue);
            values.Add("NEW_FIELD_VALUE", newValue);

            return Substituter.Substitute(emailUpdateAlertRowTemplate, values);
        }

        private static string BuildHistoryContent(string fieldLabel, string oldValue, string newValue)
        {
            return string.Format("Old {0}: {1}{3}New {0}: {2}{3}{3}", fieldLabel, oldValue, newValue, Environment.NewLine);
        }

        public DataSet SelectNotes(int ticketId, string userDisplayFormat)
        {
            WebUser user = null;

            return DataUtil.ToDataSet(
                from i in WebComment.Provider.GetList(-2, IncidentConstants.INCIDENT_TICKET_ID, ticketId, -2)
                orderby i.DateCreated descending
                select new
                {
                    i.Id,
                    i.Content,
                    i.DateCreated,
                    User = (user = i.User) != null ? AccountHelper.FormatUserDisplay(user, userDisplayFormat) : string.Empty
                });
        }

        public DataSet SelectHistory(int ticketId, string userDisplayFormat)
        {
            WebUser user = null;

            var history = from i in WebComment.Provider.GetList(-2, IncidentConstants.INCIDENT_TICKET_ID, ticketId, -2)
                          orderby i.DateCreated descending
                          select new
                          {
                              Id = "C" + i.Id,
                              Content = string.Format("NOTES: {0}<br/><br/>", i.Content),
                              i.DateCreated,
                              User = (user = i.User) != null ? AccountHelper.FormatUserDisplay(user, userDisplayFormat) : string.Empty
                          };

            history = history.Union(from i in IncidentTicketHistory.Provider.GetList(ticketId, -2, TicketHistoryType.History)
                                    orderby i.DateCreated descending
                                    select new
                                    {
                                        Id = "H" + i.Id,
                                        i.Content,
                                        i.DateCreated,
                                        User = (user = i.User) != null ? AccountHelper.FormatUserDisplay(user, userDisplayFormat) : string.Empty
                                    }
            );

            return DataUtil.ToDataSet(
                        from i in history
                        orderby i.DateCreated descending
                        select new
                        {
                            i.Id,
                            i.Content,
                            i.DateCreated,
                            i.User
                        }
            );
        }

        public DataSet SelectAttachments(int ticketId, string batchGuid, string userDisplayFormat)
        {
            WebUser user = null;

            var items = ticketId > 0 ?
                WebAttachment.Provider.GetList(-2, IncidentConstants.INCIDENT_TICKET_ID, ticketId) :
                WebAttachment.Provider.GetList(batchGuid);

            return DataUtil.ToDataSet(
                from i in items
                select new
                {
                    i.Id,
                    i.Name,
                    i.SizeString,
                    TypeName = i.Extension,
                    i.DateUploaded,
                    UploadedBy = (user = i.User) == null ? "" : AccountHelper.FormatUserDisplay(user, userDisplayFormat)
                });
        }

        protected void cboAssignedUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAssignedUser.Items.Count > 0)
            {
                var userId = DataUtil.GetId(cboAssignedUser.SelectedValue);
                if (userId > 0)
                {
                    var currentStatus = DataUtil.GetInt32(cboStatus.SelectedValue);
                    if (WSession.Current.UserId != userId)
                        cboStatus.SelectedValue = TicketStatus.Assigned.ToString();
                    else if (currentStatus == -1 || currentStatus == TicketStatus.Assigned)
                        cboStatus.SelectedValue = TicketStatus.InProgress.ToString();

                    var supportGroup = IncidentHelper.GetSupportGroup(new WContext(this), WebUser.Get(userId));
                    lblAssignedToGroup.InnerHtml = supportGroup != null ? supportGroup.Name : string.Empty;
                }
            }
        }

        private void SetErrorMessage(string message, AlertMessageTypes alertType = AlertMessageTypes.Error)
        {
            panelError.Visible = true;
            lblError.InnerHtml = message;
        }

        protected void cmdAttachmentUpload_Click(object sender, EventArgs e)
        {
            WContext context = new WContext(this);
            int id = context.GetId("TicketId");

            foreach (var control in fileUploads.Controls)
            {
                var fileUpload = control as FileUpload;
                WebAttachment.Upload(fileUpload, IncidentConstants.INCIDENT_TICKET_ID, id, hBatchGuid.Value);
            }

            GridViewAttachments.DataBind();
        }

        protected void GridViewAttachments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var id = DataUtil.GetId(e.CommandArgument);
            WebAttachment item = null;

            switch (e.CommandName)
            {
                case "Download-File":
                    item = WebAttachment.Provider.Get(id);
                    if (item != null)
                        item.Download();

                    break;

                case "Delete-File":
                    item = WebAttachment.Provider.Get(id);
                    if (item != null)
                    {
                        item.Delete();

                        GridViewAttachments.DataBind();
                    }

                    break;
            }
        }

        protected void cmdChangeRequestor_Click(object sender, EventArgs e)
        {
            var userDisplayFormat = hUserDisplayFormatString.Value;

            var requestorName = hRequestor.Value.Trim();
            var submitterName = hSubmitter.Value.Trim();

            var requestor = WebUser.GetByUniqueName(requestorName);

            lblEnteredBy.InnerHtml = AccountHelper.FormatUserDisplay(requestor, userDisplayFormat, false);

            panelSubmitter.Visible = !requestorName.Equals(submitterName);
        }

        protected void chkSendSMSNew_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSendSMSNew.Checked)
            {
                chkSendSMS.Checked = true;

                if (string.IsNullOrEmpty(txtNewNote.Text.Trim()))
                {
                    txtNewNote.Text = DataHelper.GetStringPreview(txtDescription.Text, WConstants.PreviewChars);

                    SelectTab(TAB_NOTES);
                }

                chkSendSMSNew.Visible = false;
            }
            else
            {
                chkSendSMS.Checked = false;
            }
        }

        protected void cmdAddNotifyAlso_Click(object sender, EventArgs e)
        {
            if (AddRecipients(txtAdd.Text.Trim()))
                txtAdd.Text = string.Empty;
        }

        private bool AddRecipients(string newAccounts)
        {
            var includeList = DataHelper.ParseDelimitedStringToList(hRecipients.Value, AccountConstants.AccountDelimiter);

            bool accountAdded = false;

            if (!string.IsNullOrEmpty(newAccounts))
            {
                var accountList = DataHelper.ParseDelimitedStringToList(newAccounts, AccountConstants.AccountDelimiter);
                if (accountList.Count > 0)
                {
                    //List<WebGroup> groups = new List<WebGroup>();
                    foreach (string account in accountList)
                    {
                        string shortString = string.Empty;

                        if (WebUser.IsValidUniqueName(account))
                        {
                            // WebUser in UniqueName
                            WebUser user = WebUser.GetByUniqueName(account);
                            if (user == null)
                                continue;

                            shortString = user.ToUniqueShortString();
                        }
                        else if (WebGroup.IsValidUniqueName(account)) // Should support GROUP\
                        {
                            // WebGroup in UniqueName
                            WebGroup group = WebGroup.GetByUniqueName(account);
                            if (group == null)
                                continue;

                            shortString = group.ToUniqueShortString();
                        }
                        else
                        {
                            //objectId = WebObjects.WebGroup;
                            WebGroup group = WebGroup.Get(account);
                            if (group != null)
                            {
                                shortString = group.ToUniqueShortString();
                            }
                            else
                            {
                                var user = WebUser.Get(account);
                                if (user != null)
                                    shortString = user.ToUniqueShortString();
                                else if (Validator.IsRegexMatch(account, RegexPresets.Email) || Validator.IsRegexMatch(account, RegexPresets.MobileNumber))
                                    shortString = account;
                                else
                                    continue;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(shortString))
                        {
                            if (shortString.Contains(AccountConstants.AccountSplitter))
                            {
                                // Check if existing in custom included list, if not then add
                                if (includeList.Find(i => i == shortString) == null)
                                {
                                    if (!accountAdded)
                                        accountAdded = true;

                                    includeList.Add(shortString);
                                }
                            }
                            else
                            {
                                // SMS and E-mail
                                includeList.Add(shortString);

                                if (!accountAdded)
                                    accountAdded = true;
                            }
                        }
                    }
                }
            }

            if (accountAdded)
            {
                hRecipients.Value = DataHelper.ToDelimitedString(includeList, AccountConstants.AccountDelimiter);

                gridRecipients.DataBind();

                ResetErrorMsg();

                return true;
            }
            else
            {
                lblStatus.InnerHtml = "Account does not exist or invalid recipient type.";
            }

            return false;
        }

        private void ResetErrorMsg()
        {
            lblStatus.InnerHtml = string.Empty;
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtAdd.Text = string.Empty;

            hRecipients.Value = string.Empty;
            gridRecipients.DataBind();

            ResetErrorMsg();
        }

        protected void GridViewAlsoNotify_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Custom_Delete":
                    QueryParser query = new QueryParser(this);
                    string shortString = e.CommandArgument.ToString();

                    // Check if present in custom recipients list
                    var includedList = DataHelper.ParseDelimitedStringToList(hRecipients.Value.Trim(), AccountConstants.AccountDelimiter);
                    if (includedList.Count > 0)
                    {
                        foreach (var included in includedList)
                        {
                            if (included == shortString)
                            {
                                includedList.Remove(included);
                                hRecipients.Value = DataHelper.ToDelimitedString(includedList, AccountConstants.AccountDelimiter);
                                break;
                            }
                        }
                    }

                    gridRecipients.DataBind();
                    break;
            }
        }

        public DataSet GetNotifyAlsoRecipients(string customRecipients)
        {
            var includeList = DataHelper.ParseDelimitedStringToList(customRecipients, AccountConstants.AccountDelimiter);

            List<WebUser> includeUserList = new List<WebUser>();
            List<WebGroup> includeGroupList = new List<WebGroup>();

            if (includeList.Count > 0)
            {
                // Convert includeList to lists for Users and Groups
                foreach (string include in includeList)
                {
                    if (include.Contains(AccountConstants.AccountSplitter))
                    {
                        string[] parts = include.Split(AccountConstants.AccountSplitter);

                        int objectId = DataUtil.GetId(parts.First());
                        int recordId = DataUtil.GetId(parts[1]);

                        if (objectId > 0 && recordId > 0)
                        {
                            if (objectId == WebObjects.WebUser)
                            {
                                var user = WebUser.Get(recordId);
                                if (user != null)
                                    includeUserList.Add(user);
                            }
                            else if (objectId == WebObjects.WebGroup)
                            {
                                var group = WebGroup.Get(recordId);
                                if (group != null)
                                    includeGroupList.Add(group);
                            }
                        }
                    }
                }
            }


            var result = from g in includeGroupList
                         select new
                         {
                             Id = g.ToUniqueShortString(),
                             g.Name,
                             Email = "",
                             MobileNumber = ""
                         };

            if (includeUserList.Count > 0)
            {
                result = result.Union(from user in includeUserList
                                      select new
                                      {
                                          Id = user.ToUniqueShortString(),
                                          Name = user.FirstAndLastName,
                                          user.Email,
                                          user.MobileNumber
                                      });
            }

            return DataUtil.ToDataSet(result);
        }
    }
}