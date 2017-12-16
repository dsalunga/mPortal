using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Diagnostics;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public partial class EventViewBasic : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int eventId = DataHelper.GetId(Request, "EventId");
            if (eventId > 0)
            {
                var sw = PerformanceLog.StartLog();

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

                    lblEventDate.InnerHtml = startDate.ToString("dd MMMM yyyy (dddd)");
                    lblEventTime.InnerHtml = CalendarHelper.BuildEventTimeString(item, startDate);

                    //lblRecurrence.InnerHtml = item.Recurrence;
                    var template = item.Template;
                    if (template != null)
                        lblEventSubject.Attributes["style"] += string.Format("color: {0}; background-color: {1};", template.WebForeColor, template.WebBackColor);

                    QueryParser query = new QueryParser(this);
                    query.Remove("Date");
                    query.BasePath = WebHelper.CombineAddress(WConfig.BaseAddress, query.BasePath);
                    //query.Remove(WebColumns.PageIdInternal);

                    var permalink = query.BuildQuery();
                    linkPermalink.HRef = permalink;
                    linkPermalink.InnerHtml = permalink;
                }


                context.Remove("EventId");
                context.Remove(WConstants.Open);
                linkCalendarView.HRef = context.BuildQuery();

                PerformanceLog.EndLog(string.Format("EventCalendar-EventView: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
            }
        }
    }
}