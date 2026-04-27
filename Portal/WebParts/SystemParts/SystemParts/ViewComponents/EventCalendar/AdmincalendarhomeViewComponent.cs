using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.ViewComponents;
using WCMS.WebSystem.WebParts.EventCalendar;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class AdmincalendarhomeViewComponent : WViewComponent
    {
        private const string StatusKey = "Admincalendarhome.StatusMessage";
        private const string ErrorKey = "Admincalendarhome.ErrorMessage";

        public AdmincalendarhomeViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var query = WcmsContext.Query ?? new WQuery(WSession.Context);
            var calendarId = query.GetId(WebColumns.Id);
            var calendar = calendarId > 0 ? CalendarItem.Provider.Get(calendarId) : null;
            var postController = WcmsContext.Element?.GetParameterValue("PostController");
            if (string.IsNullOrWhiteSpace(postController) ||
                postController.Equals("Admin", System.StringComparison.OrdinalIgnoreCase))
            {
                postController = "CentralPartActions";
            }

            var model = new AdmincalendarhomeViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                PostController = postController,
                PostAction = WcmsContext.Element?.GetParameterValue("PostAction") ?? "Admincalendarhome"
            };

            if (calendar != null)
            {
                model.CalendarId = calendar.Id;
                model.Header = calendar.Name;

                var configQuery = query.Clone();
                configQuery.SetCmd("CalendarEdit.ascx");
                model.ConfigUrl = configQuery.BuildQuery();

                var paramQuery = query.Clone();
                paramQuery.SetEncode(ObjectKey.KeySource, query.BuildQuery());
                paramQuery.Set(ObjectKey.KeyString, new ObjectKey(calendar.OBJECT_ID, calendar.Id));
                model.ParametersUrl = paramQuery.BuildQuery(CentralPages.WebParameters);
            }
            else if (calendarId > 0)
            {
                model.ErrorMessage = "Calendar not found.";
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

    public class AdmincalendarhomeViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CalendarId { get; set; }
        public string Header { get; set; } = "Calendar";
        public string ConfigUrl { get; set; } = "#";
        public string ParametersUrl { get; set; } = "#";
        public string PostController { get; set; } = "CentralPartActions";
        public string PostAction { get; set; } = "Admincalendarhome";
        public string StatusMessage { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;

        public List<AdmincalendarhomeItem> Items { get; set; } = new();

        public class AdmincalendarhomeItem { }
    }
}
