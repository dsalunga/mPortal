using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public partial class AdminEventView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int eventId = DataHelper.GetId(Request, "EventId");
                if (eventId > 0)
                {
                    WContext context = new WContext(this);

                    CalendarEvent item = CalendarEvent.Get(eventId);
                    if (item != null)
                    {
                        DateTime date = DateTimeHelper.ParseTicks(context.Get("Date"));

                        lblEventSubject.InnerHtml = item.Subject;
                        lblEventDescription.InnerHtml = item.Message;
                        if (item.LocationId > 0)
                            lblEventLocation.InnerHtml = item.Location.Name;
                        else
                            lblEventLocation.InnerHtml = item.LocationString;

                        var startDate = item.GetNextOccurence(date);
                        if (startDate == WConstants.DateTimeMinValue)
                            startDate = item.StartDate;
                        
                        lblEventDate.InnerHtml = startDate.ToString("dd MMMM yyyy (dddd)");
                        lblEventTime.InnerHtml = CalendarHelper.BuildEventTimeString(item, startDate);

                        //lblRecurrence.InnerHtml = item.Recurrence;

                        var template = item.Template;
                        if (template != null)
                            lblEventSubject.Attributes["style"] += string.Format("color: {0}; background-color: {1};", template.WebForeColor, template.WebBackColor);
                    }
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            int eventId = DataHelper.GetId(Request, "EventId");
            CalendarEvent.Delete(eventId);

            this.Return();
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Return();
        }

        private void Return()
        {
            var query = new WQuery(this);
            query.Remove("Load");
            query.Remove("EventId");
            query.Redirect();
        }

        protected void cmdEdit_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.LoadAndRedirect("AdminEvent.ascx");
        }

        protected void cmdSendReminder_Click(object sender, EventArgs e)
        {
            int eventId = DataHelper.GetId(Request, "EventId");
            CalendarEvent evnt = null;

            if (eventId > 0 && (evnt = CalendarEvent.Get(eventId)) != null)
            {
                EmailReminder reminder = new EmailReminder(evnt);
                if (reminder.Send())
                {
                    lblStatus.InnerHtml = "Reminder sent at " + DateTime.Now;
                    return;
                }
                else
                {
                    lblStatus.InnerHtml = "Could not send the reminder. Probably the event has occurred in the past. Check the occurrence settings properly.";
                }
            }
            else
            {
                lblStatus.InnerHtml = "This event is invalid.";
            }
        }
    }
}