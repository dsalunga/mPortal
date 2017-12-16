using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;

using WCMS.Common;
using WCMS.Common.Net;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Net;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public class EmailReminder : IReminder
    {
        public EmailReminder(CalendarEvent e)
        {
            Event = e;
        }

        public bool Send()
        {
            return Send(DateTime.Now);
        }

        public bool Send(DateTime refDate)
        {
            bool reminderSent = false;
            DateTime eventOccurrenceDate = Event.GetNextOccurence(refDate);

            if (eventOccurrenceDate == WConstants.DateTimeMinValue)
                return false;
            //if (eventDate < refDate)
            //    return false; // Event happened in the past.

            string from = WebRegistry.SelectNodeValue("/Apps/EventCalendar/Reminder.FromEmail");
            
            //string localPath = PathResolver.ResolvePathFromNonWebCall(
            //    "/Apps/EventCalendar/Templates/" + Event.Category.Template.ReminderHtml);

            //if (!string.IsNullOrEmpty(Event.Message)) Event.Message += "<br /><br />";
            //if (!string.IsNullOrEmpty(location)) location = " at the " + location;

            var template = Event.Template;
            var calendar = Event.Calendar;
            var site = WSite.Get(Event.Calendar.SiteId);

            if (Event.SendReminderVia == MessageSendVia.Email || Event.SendReminderVia == MessageSendVia.EmailAndSms)
            {
                string subject = string.Format("[mPortal] Reminder: {0} on {1:dd MMMM yyyy (dddd)} at {1:t}", Event.Subject, eventOccurrenceDate);
                string emailTemplate = template.ReminderHtml.Length > 255 ? template.ReminderHtml : WebRegistry.SelectNode("/Apps/EventCalendar/Templates/" + template.ReminderHtml).Value;

                string calendarName = "Event Calendar";
                string calendarUrl = WConfig.BaseAddress;
                if (calendar != null)
                {
                    calendarName = calendar.Name;

                    var pageId = DataHelper.GetId(calendar.GetParameterValue(WebColumns.PageId));
                    if (pageId > 0)
                    {
                        var page = WPage.Get(pageId);
                        if (page != null)
                            calendarUrl = page.BuildAbsoluteUrl();
                    }
                }

                var values = new NamedValueProvider();
                values.Add("Subject", Event.Subject);
                values.Add("Date", eventOccurrenceDate.ToString("dd MMMM yyyy (dddd)"));
                values.Add("Time", eventOccurrenceDate.ToShortTimeString());
                values.Add("Location", Event.FinalLocation);
                values.Add("Description", Event.Message);
                values.Add("ForeColor", template.WebForeColor);
                values.Add("BackColor", template.WebBackColor);
                values.Add("CalendarUrl", calendarUrl);
                values.Add("CalendarName", calendarName);

                var emailMessage = Substituter.Substitute(emailTemplate, values);
                emailMessage = WebMailMessage.FixPaths(emailMessage);

                var recipients = AccountHelper.CollectEmails(Event.ReminderTo);
                foreach (var toEmail in recipients)
                {
                    using (WebMailMessage mail = new WebMailMessage())
                    {
                        if (!string.IsNullOrEmpty(from))
                            mail.From = new MailAddress(from);

                        mail.To.Add(toEmail);
                        mail.Subject = subject;
                        mail.Body = emailMessage;

                        //AttachmentManager.GenerateAttachments(mail, localPath);

                        mail.Send();

                        if (!reminderSent)
                            reminderSent = true;
                    }

                    Thread.Sleep(50);
                }
            }

            if (Event.SendReminderVia == MessageSendVia.Sms || Event.SendReminderVia == MessageSendVia.EmailAndSms)
            {
                var toMobileNumbers = AccountHelper.CollectMobileNumbers(Event.ReminderTo);
                if (toMobileNumbers.Count > 0 && !string.IsNullOrEmpty(template.SmsContent))
                {
                    NamedValueProvider values = new NamedValueProvider();
                    values.Add("Subject", Event.Subject);
                    values.Add("Date", eventOccurrenceDate.ToString("dd MMMM yyyy (dddd)"));
                    values.Add("Time", eventOccurrenceDate.ToShortTimeString());
                    values.Add("Location", Event.FinalLocation);

                    var smsMessage = Substituter.Substitute(template.SmsContent, values);

                    foreach (var toMobileNumber in toMobileNumbers)
                    {
                        SmsHelper.SendMessage(WConfig.HttpSmsUrl, toMobileNumber, smsMessage);

                        //Logger.WriteLine("SMS sent to {0}.", toMobileNumber);

                        if (!reminderSent)
                            reminderSent = true;

                        Thread.Sleep(50);
                    }
                }
            }

            // Update the last sent date
            if (reminderSent)
            {
                Event.LastReminderSent = DateTime.Now;
                Event.Update();

                return true;
            }

            return false;
        }

        public CalendarEvent Event { get; set; }
    }
}
