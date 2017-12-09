using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading;

using WCMS.Common.Net;
using WCMS.Common.Utilities;

using WCMS.Framework.Utilities;

namespace WCMS.Framework.Net
{
    public class MessageProcessorTask : AgentTaskBase
    {
        public const string TASK_NAME = "MessageQueueProcessor";

        public override void Execute()
        {
            IEnumerable<WebMessageQueue> items;
            do
            {
                items = WebMessageQueue.Provider.GetList(MessageQueueStatus.Pending);
                foreach (var item in items)
                {
                    item.Refresh();
                    if (item != null && item.Status == MessageQueueStatus.Pending)
                    {
                        item.Status = MessageQueueStatus.InProgress;
                        item.Update();

                        // For email
                        if (item.SendVia == MessageSendVia.Email || item.SendVia == MessageSendVia.EmailAndSms)
                        {
                            var sender = item.Sender;
                            var includeEmails = AccountHelper.CollectTaggedEmails(item.To);
                            var excludeEmails = AccountHelper.CollectTaggedEmails(item.ToExcluded);
                            var toEmails = includeEmails.Except(excludeEmails, new UserTagEqualityComparer());
                            if (toEmails.Count() > 0)
                            {
                                var senderMailAddress = sender != null ? new MailAddress(sender.Email, sender.FirstAndLastName) : null;
                                if (item.ToOrBcc == MessageToOrBccStatus.ToGroup || item.ToOrBcc == MessageToOrBccStatus.Bcc)
                                {
                                    // Group Send
                                    var email = new WebMailMessage();
                                    email.Body = item.EmailMessage;
                                    email.Subject = item.EmailSubject;
                                    if (item.ToOrBcc == MessageToOrBccStatus.ToGroup)
                                    {
                                        foreach (var toEmail in toEmails)
                                            email.To.Add(toEmail.User == null ? new MailAddress(toEmail.Tag) : new MailAddress(toEmail.Tag, toEmail.User.FirstAndLastName));
                                    }
                                    else
                                    {
                                        foreach (var toEmail in toEmails)
                                            email.Bcc.Add(toEmail.User == null ? new MailAddress(toEmail.Tag) : new MailAddress(toEmail.Tag, toEmail.User.FirstAndLastName));
                                    }

                                    if (sender != null)
                                    {
                                        email.CC.Add(senderMailAddress);
                                        email.ReplyToList.Clear();
                                        email.ReplyToList.Add(senderMailAddress);
                                    }
                                    email.AlwaysBcc = item.EnableMonitor;
                                    email.Send();
                                    Logger.WriteLine("E-mail sent to multiple recipients.");
                                }
                                else
                                {
                                    // Email Individual Send
                                    foreach (var toEmail in toEmails)
                                    {
                                        var toMailAddress = toEmail.User == null ? new MailAddress(toEmail.Tag) : new MailAddress(toEmail.Tag, toEmail.User.FirstAndLastName);
                                        var email = new WebMailMessage();
                                        email.To.Add(toMailAddress);
                                        email.Body = !string.IsNullOrEmpty(item.EmailMessage) && item.EmailMessage.Contains(Substituter.DefaultLeftEnclose) ? Substituter.Substitute(item.EmailMessage, toEmail) : item.EmailMessage;
                                        email.Subject = !string.IsNullOrEmpty(item.EmailSubject) && item.EmailSubject.Contains(Substituter.DefaultLeftEnclose) ? Substituter.Substitute(item.EmailSubject, toEmail) : item.EmailSubject;

                                        if (sender != null)
                                        {
                                            email.CC.Add(senderMailAddress);
                                            email.ReplyToList.Clear();
                                            email.ReplyToList.Add(senderMailAddress);
                                        }
                                        email.AlwaysBcc = item.EnableMonitor;
                                        email.Send();
                                        Logger.WriteLine("E-mail sent to {0}.", toMailAddress);
                                        Thread.Sleep(50);
                                    }
                                }
                            }
                        }

                        // SMS
                        if (item.SendVia == MessageSendVia.Sms || item.SendVia == MessageSendVia.EmailAndSms)
                        {
                            var includeMobileNumbers = AccountHelper.CollectTaggedMobileNumbers(item.To);
                            var excludeMobileNumbers = AccountHelper.CollectTaggedMobileNumbers(item.ToExcluded);
                            var toMobileNumbers = includeMobileNumbers.Except(excludeMobileNumbers, new UserTagEqualityComparer());
                            if (toMobileNumbers.Count() > 0 && !string.IsNullOrEmpty(item.SmsMessage))
                            {
                                // Always Individual Send for SMS
                                foreach (var toMobileNumber in toMobileNumbers)
                                {
                                    var smsMessage = !string.IsNullOrEmpty(item.SmsMessage) && item.SmsMessage.Contains(Substituter.DefaultLeftEnclose) ? Substituter.Substitute(item.SmsMessage, toMobileNumber) : item.SmsMessage;
                                    SmsHelper.SendMessage(WConfig.HttpSmsUrl, toMobileNumber.Tag, smsMessage);
                                    Logger.WriteLine("SMS sent to {0}.", toMobileNumber.Tag);
                                    Thread.Sleep(50);
                                }
                            }
                        }

                        item.DateSent = DateTime.Now;
                        item.Status = MessageQueueStatus.Sent;
                        item.Update();
                    }
                }
            }
            while (items.Count() > 0);
        }
    }
}
