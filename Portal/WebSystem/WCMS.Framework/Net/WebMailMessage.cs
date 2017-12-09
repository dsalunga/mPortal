using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.Framework.Utilities
{
    public class WebMailMessage : MailMessage
    {
        public bool AlwaysBcc { get; set; }

        private SmtpClient client = null;
        private WebRegistry smtpNode = null;
        private bool enabled = true;

        #region Constructors

        public WebMailMessage()
            : base()
        {
            AlwaysBcc = true;
            Initialize("Default");
        }

        public WebMailMessage(string provider)
            : base()
        {
            AlwaysBcc = true;
            Initialize(provider);
        }

        /// <summary>
        /// Subject will be prefixed automatically with the defined system prefix.
        /// </summary>
        public string SubjectAutoPrefix
        {
            set { Subject = WConfig.SubjectPrefix + value; }
        }

        #endregion

        private void Initialize(string provider)
        {
            smtpNode = WConfig.SystemNode.SelectSingleNode("SMTP");

            enabled = DataHelper.GetBool(smtpNode.SelectSingleNodeValue("Enabled"), true);
            if (enabled)
            {
                var providerNode = smtpNode.SelectSingleNode(provider);

                // To disable certificate validation of the Smtp Server
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Host = providerNode.SelectSingleNodeValue("Host");

                var port = providerNode.SelectSingleNode("Port");
                if (port != null)
                    client.Port = port.ValueInt32;

                var userName = providerNode.SelectSingleNodeValue("UserName");
                if (!string.IsNullOrEmpty(userName))
                {
                    var password = providerNode.SelectSingleNodeValue("Password", string.Empty);
                    client.Credentials = new NetworkCredential(userName, password);
                }

                var enableSsl = providerNode.SelectSingleNode("SecurityEnabled");
                if (enableSsl != null)
                    client.EnableSsl = enableSsl.ValueBool;

                var displayName = providerNode.SelectSingleNodeValue("FromDisplayName");
                var mailFrom = providerNode.SelectSingleNodeValue("From");

                this.IsBodyHtml = true;

                if (!string.IsNullOrWhiteSpace(mailFrom) && !string.IsNullOrWhiteSpace(displayName))
                    this.From = new MailAddress(mailFrom.Trim(), displayName.Trim(), Encoding.UTF8);
                else if (!string.IsNullOrWhiteSpace(mailFrom) && !string.IsNullOrWhiteSpace(displayName))
                    this.From = new MailAddress(mailFrom.Trim());
                else
                    throw new Exception("Sender is required. Please provide a value of a MailFrom Property.");

                // Set max recipients per email

                var replyTo = smtpNode.SelectSingleNodeValue("ReplyTo");
                if (!string.IsNullOrWhiteSpace(replyTo))
                    this.ReplyToList.Add(replyTo);

                AlwaysBcc = true;
            }
        }

        private void SetAlwaysBcc()
        {
            // AlwaysBCC
            var alwaysBCC = smtpNode.SelectSingleNodeValue("MonitorBCC", string.Empty);
            if (!string.IsNullOrEmpty(alwaysBCC))
                this.Bcc.Add(alwaysBCC);
        }

        public void AddTo(IEnumerable<MailAddress> toEmails)
        {
            foreach (var toEmail in toEmails)
                To.Add(toEmail);
        }

        public void AddBcc(IEnumerable<MailAddress> toEmails)
        {
            foreach (var toEmail in toEmails)
                Bcc.Add(toEmail);
        }

        public void Send()
        {
            if (enabled)
            {
                if (AlwaysBcc)
                {
                    SetAlwaysBcc();
                }
                client.Send(this);
            }
        }

        #region Static Methods

        public static string PrefixSubject(string subject)
        {
            return WConfig.SubjectPrefix + subject;
        }

        public static string FixPaths(string messageContent)
        {
            string message = messageContent;
            string tmpBaseAddress = WConfig.BaseAddress;

            string[] srcFragments = new string[] { @" src=""/", "url(/" };
            string[] srcFragmentNew = new string[] { @" src=""{0}/", "url({0}/" };

            for (int i = 0; i < srcFragments.Length; i++)
                if (message.Contains(srcFragments[i]))
                    message = message.Replace(srcFragments[i], string.Format(srcFragmentNew[i], tmpBaseAddress));

            return message;
        }

        #endregion
    }
}
