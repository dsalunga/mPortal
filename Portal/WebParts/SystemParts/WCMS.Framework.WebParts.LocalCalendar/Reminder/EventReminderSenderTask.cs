using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.EventCalendar;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public class EventReminderSender : AgentTaskBase
    {
        public override void Execute()
        {
            int seekIntervalSeconds = WebRegistry.SelectNode("/Apps/EventCalendar/Reminder.SeekIntervalSeconds").ValueInt32;
            int seekIntervalMinutes = seekIntervalSeconds / 60;
            int sleepTime = seekIntervalSeconds * 1000;

            Logger.WriteLine("[{0}] {1} SeekIntervalSeconds: {2}, SleepTime: {3}", TaskName, DateTime.Now, seekIntervalSeconds, sleepTime);

            while (true)
            {
                if (!WebRegistry.SelectNode("/Apps/EventCalendar/Reminder.Active").ValueBool)
                    break;

                this.Logger.WriteLine("[{0}] {1} Seeking...", TaskName, DateTime.Now);

                DateTime date = DateTime.Now;
                var events = CalendarEvent.GetReminders(date, seekIntervalMinutes);
                foreach (var calendarEvent in events)
                {
                    IReminder reminder = new EmailReminder(calendarEvent);
                    if (reminder.Send())
                        Logger.WriteLine("[{0}] {1} REMINDER SENT: {2} - {3}", TaskName, DateTime.Now, calendarEvent.Id, calendarEvent.Subject);
                    // Send reminder
                }

                //Logger.WriteLine("[{0}] {1} Sleeping...", TaskName, DateTime.Now);
                Thread.Sleep(sleepTime);
            }

            Logger.WriteLine("[{0}] {1} Execution stopped.", TaskName, DateTime.Now);
        }
    }
}
