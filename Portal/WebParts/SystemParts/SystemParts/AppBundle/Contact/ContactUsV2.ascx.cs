using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Xml;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Diagnostics;
using WCMS.Framework.Utilities;
using WCMS.Framework.Core.Shared;

using WCMS.WebSystem.WebParts.Article;

namespace WCMS.WebSystem.WebParts.Contact
{
    /// <summary>
    ///		Summary description for Contact_Main.
    /// </summary>
    public partial class ContactUsViewV2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;

                string formState = context.Get("Sent");
                if (formState != null)
                {
                    var sentMessage = element.GetParameterValue("SentMessage");
                    if (string.IsNullOrEmpty(sentMessage))
                        sentMessage = WebRegistry.SelectNode(ContactConstants.ThankYouMsgPath).Value;

                    lblThanks.InnerHtml = sentMessage;
                    MultiView1.SetActiveView(viewThanks);
                }
                else
                {
                    MultiView1.SetActiveView(viewForm);

                    var theme = element.GetParameterValue("CAPTCHA-Theme");
                    if (!string.IsNullOrEmpty(theme))
                        recaptcha.Theme = theme;

                    // MENU PROPERTIES
                    var link = ContactLink.Get(context.ObjectId, context.RecordId);
                    var prefill = WSession.Current.IsLoggedIn && (link == null || link.Mode == ContactModes.AutoMode || link.Mode == ContactModes.AuthenticatedMode);
                    //cboSendTo.SelectedValue = link.ContactId.ToString();
                    if (prefill)
                    {
                        rowName.Visible = false;
                        rowEmail.Visible = false;
                        rowCode.Visible = false;
                        panelContactNo.Visible = false;

                        rowNameDisplay.Visible = true;
                        lblNameDisplay.InnerHtml = WSession.Current.User.FullName;
                    }
                    else
                    {
                        var requireCode = DataHelper.GetBool(element.GetParameterValue("RequireCode", "1"));
                        if (!requireCode)
                            rowCode.Visible = false;
                    }

                    var sendToXml = element.GetParameterValue("SendTo");
                    if (!string.IsNullOrEmpty(sendToXml))
                    {
                        var xdoc = new XmlDocument();
                        xdoc.LoadXml(sendToXml);

                        var nodes = xdoc.SelectNodes("//SendTo");
                        foreach (XmlNode node in nodes)
                        {
                            var text = XmlUtil.GetValue(node, "Text");
                            var value = XmlUtil.GetValue(node, "Value");
                            cboSendTo.Items.Add(new ListItem(text, value));
                        }

                        panelSendTo.Visible = true;
                    }

                    var subject = context.Get("Subject");
                    if (!string.IsNullOrEmpty(subject))
                        txtSubject.Text = subject;

                    var email = context.Get("Email");
                    if (!string.IsNullOrEmpty(email))
                        txtEmail.Text = email;

                    var contact = context.Get("Contact");
                    if (!string.IsNullOrEmpty(contact))
                        txtContactNo.Text = contact;

                    var name = context.Get("Name");
                    if (!string.IsNullOrEmpty(name))
                        txtName.Text = name;

                    var message = context.Get("Message");
                    if (!string.IsNullOrEmpty(message))
                        txtMessage.Text = message;
                }
            }
        }

        protected void cmdSend_Click(object sender, System.EventArgs e)
        {
            if (!Page.IsValid)
            {
                lblMessage.Text = "Incorrect Verification Code. Please enter the correct code.";
                return;
            }

            var sw = PerformanceLog.StartLog();

            var context = new WContext(this);
            var element = context.Element;
            var parmSet = element.GetParameterSet();

            string recipients = element.GetParameterValue(ContactConstants.RecipientsKey);
            if (string.IsNullOrEmpty(recipients) && parmSet != null)
                recipients = parmSet.GetParameterValue(ContactConstants.RecipientsKey);

            string sendTo = "";

            // Send To
            if (panelSendTo.Visible)
            {
                sendTo = cboSendTo.SelectedValue;
                recipients = (recipients.Trim(';') + ";" + sendTo).Trim(';');
            }

            if (string.IsNullOrWhiteSpace(recipients))
                recipients = WebRegistry.SelectNodeValue(ContactConstants.DefaultInquiryRecipientPath);

            string sendToName = string.Empty;

            if (string.IsNullOrWhiteSpace(recipients))
            {
                lblMessage.Text = "No default recipients found. Please check the configuration properly.";
                return;
            }

            string name = "";
            string emailAddress = "";
            string contactNo = string.Empty;
            int userId = -1;

            bool authMode = false;
            if (WSession.Current.UserId > 0)
            {
                var link = ContactLink.Get(context.ObjectId, context.RecordId);
                if (link != null && (link.Mode == ContactModes.AuthenticatedMode || link.Mode == ContactModes.AutoMode))
                {
                    // Authentication Mode: Use current user's info in the form, instead of asking for submitter's name info
                    var user = WSession.Current.User;
                    name = user.FirstAndLastName;
                    emailAddress = user.Email;
                    contactNo = string.IsNullOrEmpty(user.MobileNumber) ? user.TelephoneNumber : user.MobileNumber;
                    userId = user.Id;
                    authMode = true;
                }
            }

            if (!authMode)
            {
                name = txtName.Text.Trim();
                emailAddress = txtEmail.Text.Trim();
                contactNo = txtContactNo.Text.Trim();
            }

            string subject = txtSubject.Text.Trim();
            string message = txtMessage.Text.Trim();

            string addressLine1 = "";
            string addressLine2 = "";
            string city = "";
            int countryCode = -1;
            int stateCode = -1;
            string zipCode = "";
            string fax = "";
            string inquiryType = "";

            // SEND EMAIL HERE...
            var emails = AccountHelper.CollectEmails(recipients);
            if (emails.Count > 0)
            {
                try
                {
                    var baseAddress = element.GetParameterValue("BaseAddress");
                    if (string.IsNullOrEmpty(baseAddress) && parmSet != null)
                        baseAddress = parmSet.GetParameterValue("BaseAddress");
                    if (string.IsNullOrEmpty(baseAddress))
                        baseAddress = WConfig.BaseAddress;

                    var values = new NamedValueProvider();
                    values.Add("Name", name);
                    values.Add("Email", emailAddress);
                    values.Add("ContactNo", contactNo);
                    values.Add("Subject", subject);
                    values.Add("Message", message);
                    values.Add("BaseAddress", baseAddress.TrimEnd('/'));

                    // To Moderator Template
                    var emailTemplatePath = element.GetParameterValue("ToModeratorEmailPath");
                    if (string.IsNullOrEmpty(emailTemplatePath) && parmSet != null)
                        emailTemplatePath = parmSet.GetParameterValue("ToModeratorEmailPath");

                    string emailMsg = !string.IsNullOrEmpty(emailTemplatePath) ?
                        FileHelper.ReadFile(MapPath(emailTemplatePath)) : FileHelper.ReadFile(MapPath("Template/ToModerator.htm"));
                    emailMsg = Substituter.Substitute(emailMsg, values);

                    // To Moderator Subject
                    string toModeratorSubject = element.GetParameterValue(ContactConstants.ToModeratorSubjectKey);
                    if (string.IsNullOrEmpty(toModeratorSubject) && parmSet != null)
                        toModeratorSubject = parmSet.GetParameterValue(ContactConstants.ToModeratorSubjectKey);
                    if (string.IsNullOrWhiteSpace(toModeratorSubject))
                        toModeratorSubject = WebRegistry.SelectNodeValue(ContactConstants.EmailSubjectPath);

                    var m = new WebMailMessage();
                    m.Subject = Substituter.Substitute(toModeratorSubject, values); // string.Format("Subject Text: $(Name) - $(Subject)", toModeratorEmailSubject, name, subject);
                    m.Body = emailMsg;
                    m.AddTo(emails);

                    // ReplyTo
                    m.ReplyToList.Clear();
                    m.ReplyToList.Add(emailAddress);

                    string from = element.GetParameterValue(ContactConstants.ReplyEmailSenderKey);
                    if (string.IsNullOrWhiteSpace(from))
                        from = WebRegistry.SelectNodeValue(ContactConstants.FromPath);

                    if (!string.IsNullOrEmpty(from))
                        m.From = new MailAddress(from);

                    var bcc = WebRegistry.SelectNodeValue("System/SMTP/MonitorBCC", string.Empty);
                    if (!string.IsNullOrEmpty(bcc))
                        m.Bcc.Add(bcc);

                    m.Send();


                    // SEND REPLY

                    bool sendReply = DataHelper.GetBool(element.GetParameterValue(ContactConstants.SendReplyFlagKey), true);
                    if (sendReply)
                    {
                        // To Inquirer Template
                        emailTemplatePath = element.GetParameterValue("ToInquirerEmailPath");
                        if (string.IsNullOrEmpty(emailTemplatePath) && parmSet != null)
                            emailTemplatePath = parmSet.GetParameterValue("ToInquirerEmailPath");

                        emailMsg = !string.IsNullOrEmpty(emailTemplatePath) ?
                            FileHelper.ReadFile(MapPath(emailTemplatePath)) : element.GetParameterValue(ContactConstants.ReplyEmailTemplateKey);

                        if (string.IsNullOrWhiteSpace(emailMsg))
                            emailMsg = FileHelper.ReadFile(MapPath("Template/ToInquirer.htm"));

                        emailMsg = Substituter.Substitute(emailMsg, values);

                        // To Inquirer Subject
                        string toInquirerSubject = element.GetParameterValue(ContactConstants.ToInquirerSubjectKey);
                        if (string.IsNullOrEmpty(toInquirerSubject) && parmSet != null)
                            toInquirerSubject = parmSet.GetParameterValue(ContactConstants.ToInquirerSubjectKey);

                        if (string.IsNullOrWhiteSpace(toInquirerSubject))
                            toInquirerSubject = "Your enquiry was received: " + subject;
                        else
                            toInquirerSubject = Substituter.Substitute(toInquirerSubject, values);

                        SendReply(toInquirerSubject, emailAddress, from, name, emailMsg);
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.ToString();

                    LogHelper.WriteLog(ex);

                    PerformanceLog.EndLog(string.Format("Contact-Us: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);

                    return;
                }
            }
            else
            {
                // There are no valid email recipients
            }


            var item = new ContactInquiry
            {
                UserId = userId,
                SenderName = name,
                Subject = subject,
                Message = message,
                Email = emailAddress,

                Address1 = addressLine1,
                Address2 = addressLine2,
                City = city,
                CountryCode = countryCode,
                StateCode = stateCode,
                ZipCode = zipCode,

                Phone = contactNo,
                Fax = fax,
                InquiryType = inquiryType,
                SendToEmail = recipients,
                SendTo = sendTo,

                RecordId = context.RecordId,
                ObjectId = context.ObjectId,
                InqDateTime = DateTime.Now
            };
            item.Update();

            // DONE
            context.Set("Sent", "true");
            context.Redirect();

            PerformanceLog.EndLog(string.Format("Contact-Us: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
        }

        private void SendReply(string subject, string to, string from, string sFullname, string msg)
        {
            // REPLY HERE

            var m = new CmsEmail();
            m.Subject = subject;
            m.Message = msg;
            m.MailTo.Add(to);

            // Start: Additions
            if (!string.IsNullOrEmpty(from))
                m.MailFrom = from;

            m.SendMail();
        }

        public IEnumerable<Country> GetCountries()
        {
            return Country.GetList();
        }

        public IEnumerable<CountryState> GetUSStates()
        {
            return CountryState.GetList();
        }

        public IEnumerable<Contact> GetContacts()
        {
            return Contact.GetList();
        }
    }
}
