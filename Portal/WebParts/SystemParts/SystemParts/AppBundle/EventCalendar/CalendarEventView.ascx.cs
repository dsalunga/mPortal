using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

using WCMS.Framework;
using WCMS.Common.Utilities;
using WCMS.Web.Controls;
using WCMS.Framework.WebParts.EventCalendar;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public partial class CalendarEventView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int eventId = DataHelper.GetId(Request, "EventId");
            if (eventId > 0)
            {
                WebPartContext ctx = new WebPartContext(this);

                CalendarEvent item = CalendarEvent.Get(eventId);
                if (item != null)
                {
                    DateTime date = DateTimeHelper.ParseTicks(ctx.Get("Date"));

                    lblEventSubject.InnerHtml = item.Subject;
                    lblEventDescription.InnerHtml = item.Message;
                    if (item.LocationId > 0)
                        lblEventLocation.InnerHtml = item.Location.Name;
                    else
                        lblEventLocation.InnerHtml = item.LocationString;

                    var eventDate = item.GetNextOccurence(date);

                    lblEventDate.InnerHtml = eventDate.ToString("dd MMMM yyyy (dddd)");
                    lblEventTime.InnerHtml = eventDate.ToShortTimeString();
                }

                
                ctx.Remove("EventId");
                ctx.Remove(WebConstants.OpenKey);
                linkCalendarView.HRef = ctx.BuildQuery();
            }
        }
    }
}