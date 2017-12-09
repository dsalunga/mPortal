using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.Framework.Net
{
    public class WebMessageQueue : WebObjectBase, ISelfManager
    {
        private static IWebMessageQueueProvider _provider;

        public int FromObjectId { get; set; }
        public int FromRecordId { get; set; }
        public bool EnableMonitor { get; set; }
        public string EmailMessage { get; set; }

        private string _emailSubject;
        public string EmailSubject
        {
            get { return _emailSubject; }
            set { _emailSubject = !string.IsNullOrEmpty(value) ? value.Replace("\n", " ").Replace("\r", " ") : value; }
        }

        public string SmsMessage { get; set; }
        public string To { get; set; }
        public string ToExcluded { get; set; }
        public string ToFailed { get; set; }

        /// <summary>
        /// MessageToOrBccStatus
        /// </summary>
        public int ToOrBcc { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateSent { get; set; }

        /// <summary>
        /// MessageQueueStatus
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// MessageSendOptions
        /// </summary>
        public int SendVia { get; set; }

        static WebMessageQueue()
        {
            _provider = WebObject.ResolveProvider<WebMessageQueue, IWebMessageQueueProvider>();
        }

        public WebMessageQueue()
        {
            FromObjectId = -1;
            FromRecordId = -1;
            EnableMonitor = true;
            ToOrBcc = MessageToOrBccStatus.ToIndividual;

            EmailMessage = string.Empty;
            EmailSubject = string.Empty;
            SmsMessage = string.Empty;

            To = string.Empty;
            ToExcluded = string.Empty;
            ToFailed = string.Empty;

            DateCreated = DateTime.Now;
            DateSent = WConstants.DateTimeMinValue;

            Status = MessageQueueStatus.Pending;
            SendVia = MessageSendVia.EmailAndSms;
        }

        public WebMessageQueue(IWebObject item)
            : this()
        {
            if (item != null)
            {
                FromRecordId = item.Id;
                FromObjectId = item.OBJECT_ID;
            }
        }

        public static IWebMessageQueueProvider Provider
        {
            get { return _provider; }
        }

        public static WebMessageQueue CreateEmail(string content, string subject, string to, WebUser from = null)
        {
            return Create(content, string.Empty, MessageSendVia.Email, to, subject, from);
        }

        public static WebMessageQueue Create(string emailContent, string smsMessage, int sendVia, string to, string emailSubject, WebUser user)
        {
            var msg = new WebMessageQueue(user);
            msg.To = to;
            msg.SendVia = sendVia;

            if (!string.IsNullOrEmpty(emailContent) && !string.IsNullOrEmpty(emailSubject)
                && (msg.SendVia == MessageSendVia.Email || msg.SendVia == MessageSendVia.EmailAndSms))
            {
                msg.EmailMessage = emailContent;
                msg.EmailSubject = emailSubject;
            }
            msg.ToOrBcc = MessageToOrBccStatus.ToGroup;

            if (!string.IsNullOrEmpty(smsMessage) && (msg.SendVia == MessageSendVia.Sms || msg.SendVia == MessageSendVia.EmailAndSms))
                msg.SmsMessage = smsMessage;
            return msg;
        }

        public override int OBJECT_ID
        {
            get { return WebObjects.WebMessageQueue; }
        }

        public WebUser Sender
        {
            get { return FromObjectId == WebObjects.WebUser ? WebUser.Get(FromRecordId) : null; }
        }

        public void AddTo(string accountOrEmail, string mobile = "")
        {
            if (!string.IsNullOrEmpty(accountOrEmail))
                AddTo(accountOrEmail);

            if (!string.IsNullOrEmpty(mobile))
                AddTo(mobile);

            //To = string.IsNullOrEmpty(mobile) ? string.Format("{0};{1}", To, accountOrEmail) : string.Format("{0};{1};{2}", To, accountOrEmail, mobile);
        }

        public void AddTo(string accountOrEmailOrMobile)
        {
            if (!string.IsNullOrEmpty(accountOrEmailOrMobile))
                To = string.Format("{0};{1}", To, accountOrEmailOrMobile);
        }

        public void AddTo(WebUser user)
        {
            AddTo(user.Email, user.MobileNumber);
        }

        public void AddTo(IEnumerable<WebUser> users)
        {
            if (users.Count() > 0)
                foreach (var user in users)
                    AddTo(user);
        }

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public bool Refresh()
        {
            _provider.Refresh(this);

            return true;
        }

        #endregion
    }

    public struct MessageQueueStatus
    {
        public const int Draft = -1;
        public const int Pending = 0;
        public const int Sent = 1;
        public const int Error = 2;
        public const int InProgress = 3;
    }

    public struct MessageToOrBccStatus
    {
        public const int ToIndividual = 0; // Individual To
        public const int ToGroup = 1; // One-time To
        public const int Bcc = 2;
    }

    public struct MessageSendVia
    {
        public const int None = -1;
        public const int Email = 0;
        public const int Sms = 1;
        public const int EmailAndSms = 2;
    }
}
