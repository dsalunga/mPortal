using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public partial class CalendarMiniView : System.Web.UI.UserControl
    {
        internal class CalendarEventOccurrence
        {
            public CalendarEventOccurrence(DateTime startDate, CalendarEvent source)
            {
                StartDate = startDate;
                Source = source;
            }

            public DateTime StartDate { get; set; }
            public CalendarEvent Source { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WContext context = new WContext(this);
                var element = context.Element;

                var calendarId = DataHelper.GetId(element.GetParameterValue("CalendarId"));
                if (calendarId > 0)
                {
                    var monthsToCheck = DataHelper.GetInt32(element.GetParameterValue("MonthsToCheck"), 3);
                    var maxItemsToDisplay = DataHelper.GetInt32(element.GetParameterValue("MaxItemsToDisplay"), 10);

                    var startDateCheck = DateTime.Now;
                    var endDateCheck = DateTime.Now.AddMonths(monthsToCheck);
                    var noUpcoming = true;

                    StringBuilder output = new StringBuilder();
                    NamedValueProvider provider = null;

                    var calendarPageId = DataHelper.GetId(element.GetParameterValue("CalendarPageId"));
                    var page = calendarPageId > 0 ? WPage.Get(calendarPageId) : null;
                    if (page == null)
                        return;

                    var listTemplate = element.GetParameterValue("ListTemplate");
                    if (string.IsNullOrEmpty(listTemplate))
                        return;

                    var query = new WQuery(page.BuildRelativeUrl());
                    query.Set(WConstants.Open, "Event");

                    var events = CalendarEvent.GetList(startDateCheck, endDateCheck, calendarId).Take(maxItemsToDisplay);
                    if (events.Count() > 0)
                    {
                        var itemTemplate = element.GetParameterValue("ItemTemplate");

                        // Generate actual events
                        List<CalendarEventOccurrence> selectedOccurrences = new List<CalendarEventOccurrence>();
                        List<CalendarEventOccurrence> recurringEvents = new List<CalendarEventOccurrence>();

                        foreach (var eventItem in events)
                        {
                            if (!eventItem.IsRecurring)
                            {
                                selectedOccurrences.Add(
                                    new CalendarEventOccurrence(eventItem.StartDate, eventItem));
                            }
                            else
                            {
                                var recurrStartDate = eventItem.GetNextOccurence(startDateCheck);
                                if (recurrStartDate <= endDateCheck) // OK
                                {
                                    selectedOccurrences.Add(new CalendarEventOccurrence(recurrStartDate, eventItem));
                                    recurringEvents.Add(new CalendarEventOccurrence(recurrStartDate, eventItem));
                                }
                            }
                        }

                        do
                        {
                            for (int i = recurringEvents.Count - 1; i >= 0; i--)
                            {
                                var eventItem = recurringEvents[i];

                                var newStartDate = eventItem.Source.GetNextOccurence(eventItem.StartDate.AddSeconds(1));
                                if (DateTimeHelper.IsWithin(startDateCheck, endDateCheck, newStartDate))
                                {
                                    eventItem.StartDate = newStartDate;
                                    selectedOccurrences.Add(new CalendarEventOccurrence(newStartDate, eventItem.Source));
                                }
                                else
                                {
                                    recurringEvents.Remove(eventItem);
                                }

                                if (selectedOccurrences.Count > maxItemsToDisplay)
                                    break;
                            }

                            if (selectedOccurrences.Count > maxItemsToDisplay || recurringEvents.Count == 0)
                                break;
                        }
                        while (true);


                        var items = selectedOccurrences.OrderBy(i => i.StartDate);

                        noUpcoming = items.Count() == 0;

                        foreach (var eventItem in items)
                        {
                            var template = eventItem.Source.Template;

                            query.Set("EventId", eventItem.Source.Id);

                            provider = new NamedValueProvider();
                            provider.Add("Url", query.BuildQuery());
                            provider.Add("Subject", eventItem.Source.Subject);
                            provider.Add("DateTime", eventItem.StartDate.ToString("dd-MMM HH:mm"));
                            provider.Add("ForeColor", template.WebForeColor);
                            provider.Add("BackColor", template.WebBackColor);

                            output.Append(Substituter.Substitute(itemTemplate, provider));
                        }
                    }

                    if (noUpcoming)
                    {
                        // No upcoming items.
                        var emptyTemplate = element.GetParameterValue("EmptyListTemplate");
                        if (!string.IsNullOrEmpty(emptyTemplate))
                            output.Append(emptyTemplate);
                    }

                    query.Remove(WConstants.Open);
                    query.Remove("EventId");

                    provider = new NamedValueProvider();
                    provider.Add("Content", output);
                    provider.Add("CalendarUrl", query.BuildQuery());

                    Literal l = new Literal();
                    l.Text = Substituter.Substitute(listTemplate, provider);
                    this.Controls.Add(l);
                }
            }
        }
    }
}