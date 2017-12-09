using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Utilities
{
    public class CmsEmail : SmtpHelper
    {
        #region Constructors

        public CmsEmail()
            : base()
        {
            Initialize("Default");
        }

        public CmsEmail(string provider)
            : base()
        {
            Initialize(provider);
        }

        public string SubjectAutoPrefix
        {
            set
            {
                Subject = WConfig.SubjectPrefix + value;
            }
        }

        #endregion


        private void Initialize(string provider)
        {
            this.SmtpServer = WebRegistry.SelectNode("System/SMTP/{0}/Host", provider).Value;

            var port = WebRegistry.SelectNode("System/SMTP/{0}/Port", provider);
            if (port != null)
                this.SmtpPort = port.ValueInt32;

            this.UserName = WebRegistry.SelectNodeValue("System/SMTP/{0}/UserName", string.Empty, provider);
            this.Password = WebRegistry.SelectNodeValue("System/SMTP/{0}/Password", string.Empty, provider);

            var enableSsl = WebRegistry.SelectNode("System/SMTP/{0}/SecurityEnabled", provider);
            if (enableSsl != null)
                this.EnableSsl = enableSsl.ValueBool;

            this.DisplayName = WebRegistry.SelectNode("System/SMTP/{0}/FromDisplayName", provider).Value;
            this.MailFrom = WebRegistry.SelectNode("System/SMTP/{0}/From", provider).Value;

            // Set max recipients per email
            var maxRecipients = WebRegistry.SelectNodeValue("System/SMTP/MaxRecipients");
            if (!string.IsNullOrWhiteSpace(maxRecipients))
                this.MaxRecipients = DataHelper.GetInt32(maxRecipients);

            var replyTo = WebRegistry.SelectNodeValue("System/SMTP/ReplyTo");
            if (!string.IsNullOrWhiteSpace(replyTo))
                ReplyTo.Add(replyTo);
        }

        public void SetAlwaysBCC()
        {
            AlwaysBCC = WebRegistry.SelectNodeValue("System/SMTP/MonitorBCC", string.Empty);
        }

        #region Static Methods

        public static string FixPaths(string messageContent)
        {
            string message = messageContent;
            string tmpBaseAddress = WConfig.BaseAddress;

            string[] srcFragments = new string[] { @" src=""/", "url(/" };
            string[] srcFragmentNew = new string[] { @" src=""{0}/", "url({0}/" };

            for (int i = 0; i < srcFragments.Length; i++)
            {
                if (message.Contains(srcFragments[i]))
                    message = message.Replace(srcFragments[i], string.Format(srcFragmentNew[i], tmpBaseAddress));
            }

            return message;
        }

        #endregion
    }
}
