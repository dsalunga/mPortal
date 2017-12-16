using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework.Utilities;
using WCMS.Framework;
using WCMS.Framework.Diagnostics;
using WCMS.Framework.Net;
using WCMS.Framework.Core;


using WCMS.WebSystem.Agent;

namespace WCMS.WebSystem.WebParts.Messaging
{
    public partial class SendEmail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;
                var canSendMulti = false;

                var useNamePrefix = DataHelper.GetBool(element.GetParameterValue("UseNamePrefix"), false);
                var smsTemplate = element.GetParameterValue("SMS-Template");
                var emailTemplate = element.GetParameterValue("Email-Template");
                var emailSubjectTemplate = element.GetParameterValue("EmailSubject-Template");

                var canSendMultiList = element.GetParameterValue("FullControlList");
                if (!string.IsNullOrWhiteSpace(canSendMultiList))
                    canSendMulti = AccountHelper.IsPresentOrMember(canSendMultiList, true);

                var maxSMS = DataHelper.GetInt32(element.GetParameterValue("MaxSMS", "-1"));
                if (maxSMS > 0)
                    hMaxSMS.Value = maxSMS.ToString();

                hFullControl.Value = canSendMulti ? "1" : "0";
                hBaseGroup.Value = element.GetParameterValue("BaseGroup");

                #region PrepareRecipientAndSubject

                //PrepareRecipientAndSubject(canSendMulti, useNamePrefix);

                int userId = DataHelper.GetId(Request, WebColumns.UserId);
                WebUser toUser = userId > 0 ? WebUser.Get(userId) : null;
                var currentUser = WSession.Current.User;

                if (toUser == null && canSendMulti) { }
                else if (toUser != null)
                {
                    // Recipient
                    panelRecipientMultiple.Visible = false;
                    panelRecipientSingle.Visible = true;

                    if (toUser != null)
                    {
                        lblToSingle.InnerHtml = string.Format("<strong>{0}</strong> ({1}{2})",
                            useNamePrefix ? AccountHelper.GetPrefixedName(toUser) : toUser.FirstAndLastName, toUser.Email, !string.IsNullOrEmpty(toUser.MobileNumber) ? ", " + toUser.MobileNumber : "");

                        hSingleRecipient.Value = toUser.ToUniqueShortString();
                    }


                    // Subject
                    string messageSubject = string.Format("{0} sent you a message",
                        useNamePrefix ? AccountHelper.GetPrefixedName(currentUser) : currentUser.FirstAndLastName);

                    hEmailSubject.Value = messageSubject;

                    // Send As
                    panelSendAs.Visible = false;
                }
                else
                {
                    cmdSend.Enabled = false;

                    // Recipient
                    panelRecipientMultiple.Visible = false;
                    panelRecipientSingle.Visible = true;
                    lblToSingle.InnerHtml = "Invalid Recipient";

                    txtSubject.Enabled = false;
                    panelSendAs.Visible = false;
                }

                #endregion

                CheckEnableSend();

                SetCurrentView("Groups");

                EvaluateMessageOptions();

                // Prepare SMS & E-mail

                var smsFrom = (useNamePrefix ? AccountHelper.GetPrefixedName(currentUser) : currentUser.FirstAndLastName) +
                    (!string.IsNullOrEmpty(currentUser.MobileNumber) ? string.Format(" ({0})", currentUser.MobileNumber) : string.Empty);

                var emailFrom = string.Format("<strong>{0}</strong> (<a href=\"mailto:{1}\">{1}</a>)",
                    useNamePrefix ? AccountHelper.GetPrefixedName(currentUser) : currentUser.FirstAndLastName, currentUser.Email);

                txtSMSMessage.Text = Substituter.Substitute(string.IsNullOrEmpty(smsTemplate) || toUser != null ? "-From: $(FROM)" : smsTemplate, "FROM", smsFrom);
                txtMessage.Value = Substituter.Substitute(string.IsNullOrEmpty(emailTemplate) || toUser != null ? "<br/><br/>-From: $(FROM)" : emailTemplate, "FROM", emailFrom);

                if (toUser != null)
                    txtSubject.Text = string.Format("Message from {0}", useNamePrefix ? AccountHelper.GetPrefixedName(currentUser) : currentUser.FirstAndLastName);
                else if (!string.IsNullOrEmpty(emailSubjectTemplate))
                    txtSubject.Text = emailSubjectTemplate;
            }
        }

        private void EvaluateMessageOptions()
        {
            switch (rblSendVia.SelectedIndex)
            {
                case 0:
                    panelSMSMessage.Visible = true;
                    panelEmailMessage.Visible = false;
                    break;

                case 1:
                    panelEmailMessage.Visible = true;
                    panelSMSMessage.Visible = false;
                    break;

                case 2:
                    panelSMSMessage.Visible = true;
                    panelEmailMessage.Visible = true;
                    break;
            }
        }

        //private void PrepareRecipientAndSubject(bool canSendMulti, bool useNamePrefix)
        //{
        //    int userId = DataHelper.GetId(Request, WebColumns.UserId);
        //    WebUser user = userId > 0 ? WebUser.Get(userId) : null;

        //    if (user == null && canSendMulti) { }
        //    else if (user != null)
        //    {
        //        // Recipient
        //        panelRecipientMultiple.Visible = false;
        //        panelRecipientSingle.Visible = true;

        //        if (user != null)
        //        {
        //            lblToSingle.InnerHtml = string.Format("<strong>{0}</strong> ({1}{2})",
        //                useNamePrefix ? AccountHelper.GetPrefixedName(user) : user.FirstAndLastName, user.Email, !string.IsNullOrEmpty(user.MobileNumber) ? ", " + user.MobileNumber : "");

        //            hSingleRecipient.Value = user.ToShortString();
        //        }

        //        var currentUser = WSession.Current.User;

        //        // Subject
        //        string messageSubject = string.Format("{0} sent you a message", 
        //            useNamePrefix ? AccountHelper.GetPrefixedName(currentUser) : currentUser.FirstAndLastName);

        //        hEmailSubject.Value = messageSubject;

        //        // Send As
        //        panelSendAs.Visible = false;
        //    }
        //    else
        //    {
        //        cmdSend.Enabled = false;

        //        // Recipient
        //        panelRecipientMultiple.Visible = false;
        //        panelRecipientSingle.Visible = true;
        //        lblToSingle.InnerHtml = "Invalid Recipient";

        //        txtSubject.Enabled = false;
        //        panelSendAs.Visible = false;
        //    }
        //}

        private void CheckEnableSend()
        {
            bool isSingleRecipient = !string.IsNullOrWhiteSpace(hSingleRecipient.Value);
            cmdSend.Enabled = isSingleRecipient || gridRecipients.Rows.Count > 0;

            panelRecipientView.Visible = !isSingleRecipient && gridRecipients.Rows.Count > 0;
        }

        private void SetCurrentView(string viewRecipients)
        {
            // View Type
            hView.Value = viewRecipients;
            if (viewRecipients == "Users")
            {
                lblCurrentView.InnerHtml = "Users";

                // Users View
                cmdChangeView.Text = "Groups";
            }
            else
            {
                lblCurrentView.InnerHtml = "Groups";

                // Groups View
                cmdChangeView.Text = "Users";
            }
        }

        public DataSet GetRecipients(string view, string customRecipients, string exclude)
        {
            var includeList = DataHelper.ParseDelimitedStringToList(customRecipients, AccountConstants.AccountDelimiter);
            var excludeList = DataHelper.ParseDelimitedStringToList(exclude, AccountConstants.AccountDelimiter);

            List<WebUser> includeUserList = new List<WebUser>();
            List<WebGroup> includeGroupList = new List<WebGroup>();

            List<WebUser> excludeUserList = new List<WebUser>();
            List<WebGroup> excludeGroupList = new List<WebGroup>();

            List<string> includeEmailList = new List<string>();
            List<string> includeMobileList = new List<string>();

            if (includeList.Count > 0)
            {
                // Convert includeList to lists for Users and Groups
                foreach (string include in includeList)
                {
                    if (include.Contains(AccountConstants.AccountSplitter))
                    {
                        string[] parts = include.Split(AccountConstants.AccountSplitter);

                        int objectId = DataHelper.GetId(parts.First());
                        int recordId = DataHelper.GetId(parts[1]);

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
                    else if (Validator.IsRegexMatch(include, RegexPresets.Email))
                    {
                        includeEmailList.Add(include);
                    }
                    else if (Validator.IsRegexMatch(include, RegexPresets.MobileNumber))
                    {
                        includeMobileList.Add(include);
                    }
                }
            }

            if (excludeList.Count > 0)
            {
                // Convert exclude to lists for Users and Groups
                foreach (string ex in excludeList)
                {
                    string[] parts = ex.Split(AccountConstants.AccountSplitter);

                    int objectId = DataHelper.GetId(parts.First());
                    int recordId = DataHelper.GetId(parts[1]);

                    if (objectId > 0 && recordId > 0)
                    {
                        if (objectId == WebObjects.WebUser)
                        {
                            var user = WebUser.Get(recordId);
                            if (user != null)
                                excludeUserList.Add(user);
                        }
                        else if (objectId == WebObjects.WebGroup)
                        {
                            var group = WebGroup.Get(recordId);
                            if (group != null)
                                excludeGroupList.Add(group);
                        }
                    }
                }
            }


            // Check view type
            if (view == "Users")
            {
                // Get all users in default groups, excluding exclude groups
                List<WebUser> users = new List<WebUser>();

                // Get all users in custom group list
                if (includeGroupList.Count > 0)
                {
                    foreach (var group in includeGroupList)
                        users.AddRange(group.Users);
                }

                // Include custom user list
                if (includeUserList.Count > 0)
                    users.AddRange(includeUserList);

                if (excludeUserList.Count > 0)
                    users = users.Except(excludeUserList).ToList();

                return DataHelper.ToDataSet(
                       (from user in users
                        select new
                        {
                            Id = user.ToUniqueShortString(),
                            Name = user.FirstAndLastName,
                            user.Email,
                            user.MobileNumber
                        }
                    ).Distinct());
            }
            else
            {
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

                if (includeEmailList.Count > 0)
                {
                    result = result.Union(from c in includeEmailList
                                          select new
                                          {
                                              Id = c,
                                              Name = "",
                                              Email = c,
                                              MobileNumber = ""
                                          });
                }

                if (includeMobileList.Count > 0)
                {
                    result = result.Union(from c in includeMobileList
                                          select new
                                          {
                                              Id = c,
                                              Name = "",
                                              Email = "",
                                              MobileNumber = c
                                          });
                }

                return DataHelper.ToDataSet(result);
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            ReturnPage();
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.UnloadAndRedirect();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Custom_Delete":
                    var query = new WQuery(this);
                    string shortString = e.CommandArgument.ToString();

                    bool isIncluded = false;

                    // Check if present in custom recipients list
                    var includedList = DataHelper.ParseDelimitedStringToList(hRecipients.Value.Trim(), AccountConstants.AccountDelimiter);
                    if (includedList.Count > 0)
                    {
                        foreach (var included in includedList)
                        {
                            if (included == shortString)
                            {
                                isIncluded = true;
                                includedList.Remove(included);
                                hRecipients.Value = DataHelper.ToDelimitedString(includedList, AccountConstants.AccountDelimiter);
                                break;
                            }
                        }
                    }

                    // If not included in custom recipients, put it in exclude list
                    if (!isIncluded)
                    {
                        var excludedList = DataHelper.ParseDelimitedStringToList(hExcluded.Value.Trim(), AccountConstants.AccountDelimiter);
                        if (excludedList.Find(i => i == shortString) == null)
                        {
                            excludedList.Add(shortString);
                            hExcluded.Value = DataHelper.ToDelimitedString(excludedList, AccountConstants.AccountDelimiter);
                        }
                    }

                    gridRecipients.DataBind();
                    CheckEnableSend();
                    break;
            }
        }

        protected void cmdSend_Click(object sender, EventArgs e)
        {
            string emailBody = txtMessage.Value;
            string messageSubject = txtSubject.Text.Trim();

            if (string.IsNullOrWhiteSpace(emailBody) && string.IsNullOrWhiteSpace(messageSubject))
            {
                lblStatus.InnerHtml = "Please write a message or include a subject before sending.";
                return;
            }

            string emailSubject = hEmailSubject.Value;
            bool isSingleRecipient = !string.IsNullOrWhiteSpace(hSingleRecipient.Value);

            var user = WSession.Current.User;
            WebMessageQueue msg = new WebMessageQueue(user);
            if (!isSingleRecipient)
            {
                msg.To = hRecipients.Value;
                msg.ToExcluded = hExcluded.Value;
            }
            else
            {
                msg.To = hSingleRecipient.Value;
            }

            if (chkSendSMSCopy.Checked && user != null && !string.IsNullOrEmpty(user.MobileNumber))
                msg.AddTo(user.MobileNumber);

            msg.ToOrBcc = isSingleRecipient ? MessageToOrBccStatus.ToIndividual : DataHelper.GetInt32(radioSendAs.SelectedValue);
            msg.SendVia = DataHelper.GetInt32(rblSendVia.SelectedValue);

            if (!string.IsNullOrEmpty(msg.To))
            {
                bool success = true;

                if (msg.SendVia == MessageSendVia.Email || msg.SendVia == MessageSendVia.EmailAndSms)
                {
                    // Prepare Email
                    NamedValueProvider values = new NamedValueProvider();
                    values.Add("Title", string.IsNullOrEmpty(messageSubject) ? emailSubject : messageSubject);
                    values.Add("Content", emailBody);
                    values.Add("BaseAddress", WConfig.BaseAddress);

                    string emailPath = "~/Content/Parts/Messaging/Templates/EmailMessage.htm";
                    string message = FileHelper.ReadFile(MapPath(emailPath));
                    message = Substituter.Substitute(message, values);

                    // Format relative paths to absolute
                    msg.EmailMessage = WebMailMessage.FixPaths(message);
                    msg.EmailSubject = WebMailMessage.PrefixSubject(string.IsNullOrWhiteSpace(emailSubject) ? messageSubject : emailSubject);
                }

                if (msg.SendVia == MessageSendVia.Sms || msg.SendVia == MessageSendVia.EmailAndSms)
                    msg.SmsMessage = txtSMSMessage.Text;

                msg.Update();

                try
                {
                    AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                }
                catch (Exception ex)
                {
                    lblStatus.InnerHtml = "There was an error sending your mesage. However, it was placed on queue by the messaging server and will be sent on the next schedule.";
                    success = false;

                    LogHelper.WriteLog(ex);
                }

                if (success)
                {
                    panelSend.Visible = false;
                    panelSent.Visible = true;
                }
            }
            else
            {
                lblStatus.InnerHtml = "There are no recipients in the list.";
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            if (AddRecipients(txtAdd.Text.Trim()))
                txtAdd.Text = string.Empty;
        }

        private bool AddRecipients(string newAccounts)
        {
            var includeList = DataHelper.ParseDelimitedStringToList(hRecipients.Value, AccountConstants.AccountDelimiter);
            var excludeList = DataHelper.ParseDelimitedStringToList(hExcluded.Value, AccountConstants.AccountDelimiter);

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
                                if (excludeList.Find(i => i == shortString) != null)
                                {
                                    if (!accountAdded)
                                        accountAdded = true;

                                    excludeList.Remove(shortString);

                                    continue;
                                }

                                //if (groups.Count > 0 && AccountHelper.GetObjectId(shortString) == WebObjects.WebGroup)
                                //{
                                //    if (groups.Find(i => i.ToShortString() == shortString) != null)
                                //        continue;
                                //}

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
                hExcluded.Value = DataHelper.ToDelimitedString(excludeList, AccountConstants.AccountDelimiter);
                hRecipients.Value = DataHelper.ToDelimitedString(includeList, AccountConstants.AccountDelimiter);

                gridRecipients.DataBind();

                CheckEnableSend();

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
            hExcluded.Value = string.Empty;
            gridRecipients.DataBind();

            ResetErrorMsg();

            CheckEnableSend();
        }

        protected void cmdChangeView_Click(object sender, EventArgs e)
        {
            SetCurrentView(hView.Value == "Users" ? "" : "Users");

            gridRecipients.DataBind();
        }

        protected void rblSendVia_SelectedIndexChanged(object sender, EventArgs e)
        {
            EvaluateMessageOptions();
        }
    }
}