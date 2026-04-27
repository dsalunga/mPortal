using System;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using WCMS.WebSystem.WebParts.EventCalendar;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class AdmineventviewViewComponent : WViewComponent
    {
        private const string StatusKey = "Admineventview.StatusMessage";
        private const string ErrorKey = "Admineventview.ErrorMessage";

        public AdmineventviewViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var postController = WcmsContext.Element?.GetParameterValue("PostController");
            if (string.IsNullOrWhiteSpace(postController) ||
                postController.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                postController = "CentralPartActions";
            }

            var model = new AdmineventviewViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                PostController = postController,
                PostAction = WcmsContext.Element?.GetParameterValue("PostAction") ?? "Admineventview"
            };

            var eventId = WcmsContext.GetId("EventId");
            model.EventId = eventId;
            if (eventId > 0)
            {
                var item = CalendarEvent.Get(eventId);
                if (item != null)
                {
                    var currentDate = WCMS.Common.Utilities.TimeUtil.ParseTicks(WcmsContext.Get("Date"));
                    if (currentDate <= WConstants.DateTimeMinValue)
                        currentDate = DateTime.Now;

                    var startDate = item.GetNextOccurence(currentDate);
                    if (startDate == WConstants.DateTimeMinValue)
                        startDate = item.StartDate;

                    model.Subject = item.Subject;
                    model.Description = item.Message;
                    model.Location = item.LocationId > 0 && item.Location != null
                        ? item.Location.Name
                        : item.LocationString;
                    model.EventDate = startDate.ToString("dd MMMM yyyy (dddd)");
                    model.EventTime = CalendarHelper.BuildEventTimeString(item, startDate);
                    var template = item.Template;
                    if (template != null)
                    {
                        model.SubjectStyle = $"color: {template.WebForeColor}; background-color: {template.WebBackColor};";
                    }
                }
                else
                {
                    model.ErrorMessage = "Event not found.";
                }
            }
            else
            {
                model.ErrorMessage = "No event selected.";
            }

            if (TempData.TryGetValue(StatusKey, out var status) && status != null)
                model.StatusMessage = status.ToString();

            if (TempData.TryGetValue(ErrorKey, out var error) && error != null)
                model.ErrorMessage = string.IsNullOrWhiteSpace(model.ErrorMessage)
                    ? error.ToString()
                    : model.ErrorMessage;

            return View(model);
        }
    }

    public class AdmineventviewViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int EventId { get; set; }
        public string Subject { get; set; } = "Event Title";
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string EventDate { get; set; } = string.Empty;
        public string EventTime { get; set; } = string.Empty;
        public string SubjectStyle { get; set; } = string.Empty;
        public string PostController { get; set; } = "CentralPartActions";
        public string PostAction { get; set; } = "Admineventview";
        public string StatusMessage { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
