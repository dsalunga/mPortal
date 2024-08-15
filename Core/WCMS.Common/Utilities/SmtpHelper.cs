/*
    Author: SVMR
    Date: 2 Jan 2010
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Linq;
using System.Configuration;

using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// WCMS.Common.Utilities Namespace - mPortal
/// Copyright 2006. 
/// All Rights Reserved.
/// </summary>
namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Represents WCMS.Misc.Messaging class
    /// </summary>
    public class SmtpHelper
    {
        protected SmtpClient smtp = null;

        /// <summary>
        /// Initializes a new instance of the WCMS.Common.Utilities.SmtpHelper class.
        /// </summary>
        public SmtpHelper()
        {
            // To disable certificate validation of the Smtp Server
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            smtp = new SmtpClient();

            MailTo = new List<string>();
            MailCC = new List<string>();
            MailBCC = new List<string>();
            ReplyTo = new List<string>();

            AlwaysBCC = "";

            SmtpPort = 0;
            SmsPort = 0;
            MaxRecipients = 50;
        }

        /// <summary>
        /// Initializes a new instance of the WCMS.Common.Utilities.SmtpHelper class.
        /// </summary>
        /// <param name="smtpServer">Name or IP Address of the SMTP Server to be used for sending e-mail.</param>
        /// <param name="smtpPort">Port to be used for sending e-mail</param>
        public SmtpHelper(string smtpServer, int smtpPort)
            : this()
        {
            this.SmtpServer = smtpServer;
            this.SmtpPort = smtpPort;
        }

        /// <summary>
        /// Initializes a new instance of the WCMS.Common.Utilities.SmtpHelper class.
        /// </summary>
        /// <param name="smtpServer">Name or IP Address of the SMTP Server to be used for sending e-mail.</param>
        /// <param name="smtpPort">Port to be used for sending e-mail</param>
        public SmtpHelper(string smtpServer, int smtpPort, string userName, string password)
            : this()
        {
            this.SmtpServer = smtpServer;
            this.SmtpPort = smtpPort;
            this.UserName = userName;
            this.Password = password;
        }

        /// <summary>
        /// Initializes a new instance of the WCMS.Common.Utilities.SmtpHelper class.
        /// </summary>
        /// <param name="smtpServer">Name or IP Address of the SMTP Server to be used for sending e-mail.</param>
        /// <param name="smtpPort">Port to be used for sending e-mail</param>
        /// <param name="smsServer">Name or IP Address of the SMS Server to be used for sending SMS.</param>
        /// <param name="smsPort">Port to be used for sending SMS.</param>
        public SmtpHelper(string smtpServer, int smtpPort, string smsServer, int smsPort)
            : this()
        {
            this.SmtpServer = smtpServer;
            this.SmtpPort = smtpPort;
            this.SmsServer = smsServer;
            this.SmsPort = smsPort;
        }

        /// <summary>
        /// Gets or sets the Name or IP Address of the SMTP Server to be used for sending e-mail.
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Gets or sets the Port to be used for sending e-mail.
        /// </summary>
        public int SmtpPort { get; set; }

        /// <summary>
        /// Gets or sets the Name or IP Address of the SMS Server to be used for sending SMS.
        /// </summary>
        public string SmsServer { get; set; }

        /// <summary>
        /// Gets or sets the Port to be used for sending SMS.
        /// </summary>
        public int SmsPort { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the sender of the Mail Message.
        /// </summary>
        public string MailFrom { get; set; }

        /// <summary>
        /// Gets or sets the sender's display name of the Mail Message.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the collection of recipients of the Mail Message.
        /// </summary>
        public List<string> MailTo { get; set; }

        /// <summary>
        /// Gets or sets the collection of Carbon Copy (CC) recipients of the Mail Message.
        /// </summary>
        public List<string> MailCC { get; set; }

        /// <summary>
        /// Gets or sets the collection of Blind Carbon Copy (BCC) recipients of the Mail Message.
        /// </summary>
        public List<string> MailBCC { get; set; }

        public List<string> ReplyTo { get; set; }

        /// <summary>
        /// Gets or sets the Header the Mail Message.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the Subject the Mail Message.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the Message Body the Mail Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the sender of the SMS Message.
        /// </summary>
        public string SmsFrom { get; set; }

        /// <summary>
        /// Gets or sets the collection of recipients of the SMS Message.
        /// </summary>
        public string[] SmsTo { get; set; }

        public bool EnableSsl
        {
            get { return smtp.EnableSsl; }
            set { smtp.EnableSsl = value; }
        }


        public int MaxRecipients { get; set; }

        public void SendMail()
        {
            SendMail(-1);
        }

        public string AlwaysBCC { get; set; }

        /// <summary>
        /// Sends the Mail Message notification.
        /// </summary>
        public void SendMail(int maxRecipients)
        {
            int maxReps = maxRecipients > 0 ? maxRecipients : MaxRecipients;

            int lastToIndex = 0;
            int lastCCIndex = 0;
            int lastBCCIndex = 0;

            do
            {
                MailMessage mail = new MailMessage();
                MailAddress from = null;

                if (!string.IsNullOrWhiteSpace(MailFrom) && !string.IsNullOrWhiteSpace(DisplayName))
                    from = new MailAddress(MailFrom.Trim(), DisplayName.Trim(), Encoding.UTF8);
                else if (!string.IsNullOrWhiteSpace(MailFrom) && !string.IsNullOrWhiteSpace(DisplayName))
                    from = new MailAddress(MailFrom.Trim());
                else
                    throw new Exception("Sender is required. Please provide a value of a MailFrom Property.");

                mail.IsBodyHtml = true;
                mail.From = from;
                mail.Sender = from;

                // Setup ReplyTo
                if (ReplyTo.Count > 0)
                {
                    foreach (var replyTo in ReplyTo)
                    {
                        mail.ReplyToList.Add(replyTo);
                    }
                }
                else
                {
                    mail.ReplyToList.Add(from);
                }

                int i;

                if (MailTo.Count > lastToIndex)
                {
                    for (i = lastToIndex; i < MailTo.Count && i < lastToIndex + maxReps; i++)
                        mail.To.Add(MailTo[i]);

                    lastToIndex = i;
                }
                else if (lastToIndex == 0 && MailCC.Count == 0 && MailBCC.Count == 0)
                {
                    throw new Exception("At least one Destination is required.");
                }

                if (MailCC.Count > lastCCIndex)
                {
                    for (i = lastCCIndex; i < MailCC.Count && i < lastCCIndex + maxReps; i++)
                        mail.CC.Add(MailCC[i]);

                    lastCCIndex = i;
                }

                if (MailBCC.Count > lastBCCIndex)
                {
                    for (i = lastBCCIndex; i < MailBCC.Count && i < lastBCCIndex + maxReps; i++)
                        mail.Bcc.Add(MailBCC[i]);

                    lastBCCIndex = i;
                }

                if (!string.IsNullOrWhiteSpace(AlwaysBCC))
                    mail.Bcc.Add(AlwaysBCC);

                if (!string.IsNullOrWhiteSpace(Header))
                {
                    System.Collections.Specialized.NameValueCollection hdrs = new System.Collections.Specialized.NameValueCollection();
                    hdrs.Add("header1", Header.Trim());
                    mail.Headers.Add(hdrs);
                }

                mail.Subject = Subject;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Host = SmtpServer;

                if (!string.IsNullOrWhiteSpace(Message))
                    mail.Body = Message;

                if (SmtpPort > 0)
                    smtp.Port = SmtpPort;

                smtp.UseDefaultCredentials = false;

                if (!string.IsNullOrWhiteSpace(UserName))
                    smtp.Credentials = new NetworkCredential(UserName.Trim(), Password);

                smtp.Send(mail);
            }
            while (MailTo.Count > lastToIndex || MailCC.Count > lastCCIndex || MailBCC.Count > lastBCCIndex);
        }

        /// <summary>
        /// Sends the SMS Message notification.
        /// </summary>
        public bool SendSMS()
        {
            bool sent = false;
            try
            {
                //string popupScript = "<script language='javascript'>var oReq = new ActiveXObject('Microsoft.XMLHTTP'); oReq.open('GET', 'http://Jupiter2:8800/?PhoneNumber=" + mobileNo + "&Text=" + msgNotification + "');oReq.send();</script>";
                //RegisterClientScriptBlock("PopupScript", popupScript);

                //string popupScript = "<script language='javascript'>" +
                //    "window.open('http://Jupiter2:8800/?PhoneNumber=" + mobileNo + "&Text=" + msgNotification + "', 'sendSMS', " +
                //    "'titlebar=no, top=0, left=0, width=50, height=50, menubar=no, resizable=no, toolbars=no')" +
                //    "</script>";

                //Page.RegisterStartupScript("PopupScript", popupScript);
                sent = true;
            }
            catch
            {
                sent = false;
            }
            return sent;
        }
    }
}