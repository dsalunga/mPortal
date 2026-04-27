using System;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.EventCalendar;
using WCMS.WebSystem.WebParts.Menu;
using WCMS.WebSystem.WebParts.Menu.Managers;

namespace WCMS.WebSystem.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("central/parts")]
    public class CentralPartActionsController : Controller
    {
        private const string AdminCalendarStatusKey = "Admincalendarhome.StatusMessage";
        private const string AdminCalendarErrorKey = "Admincalendarhome.ErrorMessage";
        private const string AdminEventStatusKey = "Admineventview.StatusMessage";
        private const string AdminEventErrorKey = "Admineventview.ErrorMessage";
        private const string ImportExportStatusKey = "Importexport.StatusMessage";
        private const string ImportExportErrorKey = "Importexport.ErrorMessage";
        private const string ConfigDynamicMenuStatusKey = "Configdynamicmenuv2.StatusMessage";
        private const string ConfigDynamicMenuErrorKey = "Configdynamicmenuv2.ErrorMessage";
        private const string SurveyWelcomeStatusKey = "Surveywelcome.StatusMessage";
        private const string SurveyWelcomeErrorKey = "Surveywelcome.ErrorMessage";
        private const string OfficeDetailsStatusKey = "Officedetails.StatusMessage";
        private const string OfficeDetailsErrorKey = "Officedetails.ErrorMessage";

        [HttpPost("admincalendarhome")]
        [ValidateAntiForgeryToken]
        public IActionResult Admincalendarhome(
            [FromForm] int objectId,
            [FromForm] int recordId,
            [FromForm] string action,
            [FromForm] int calendarId = 0)
        {
            if (string.Equals(action, "cmdDelete", StringComparison.OrdinalIgnoreCase))
            {
                var id = calendarId > 0 ? calendarId : DataUtil.GetId(Request.Query[WebColumns.Id]);
                if (id > 0 && CalendarItem.Provider.Delete(id))
                    TempData[AdminCalendarStatusKey] = "Calendar deleted.";
                else
                    TempData[AdminCalendarErrorKey] = "Unable to delete calendar.";
            }

            return LocalRedirect(BuildRedirectUrl(query =>
            {
                query.Remove(WebColumns.Id);
                query.Remove(WConstants.Load);
            }));
        }

        [HttpPost("admineventview")]
        [ValidateAntiForgeryToken]
        public IActionResult Admineventview(
            [FromForm] int objectId,
            [FromForm] int recordId,
            [FromForm] string action,
            [FromForm] int eventId = 0)
        {
            var id = eventId > 0 ? eventId : DataUtil.GetId(Request.Query["EventId"]);

            if (string.Equals(action, "cmdDelete", StringComparison.OrdinalIgnoreCase))
            {
                if (id > 0 && CalendarEvent.Delete(id))
                    TempData[AdminEventStatusKey] = "Event deleted.";
                else
                    TempData[AdminEventErrorKey] = "Unable to delete event.";

                return LocalRedirect(BuildRedirectUrl(query =>
                {
                    query.Remove(WConstants.Load);
                    query.Remove("EventId");
                }));
            }

            if (string.Equals(action, "cmdCancel", StringComparison.OrdinalIgnoreCase))
            {
                return LocalRedirect(BuildRedirectUrl(query =>
                {
                    query.Remove(WConstants.Load);
                    query.Remove("EventId");
                }));
            }

            if (string.Equals(action, "cmdEdit", StringComparison.OrdinalIgnoreCase))
            {
                return LocalRedirect(BuildRedirectUrl(query =>
                {
                    query.Set(WConstants.Load, "AdminEvent.ascx");
                    if (id > 0)
                        query.Set("EventId", id.ToString());
                }));
            }

            if (string.Equals(action, "cmdSendReminder", StringComparison.OrdinalIgnoreCase))
            {
                CalendarEvent evnt = null;
                if (id > 0 && (evnt = CalendarEvent.Get(id)) != null)
                {
                    var reminder = new EmailReminder(evnt);
                    if (reminder.Send())
                        TempData[AdminEventStatusKey] = $"Reminder sent at {DateTime.Now:g}";
                    else
                        TempData[AdminEventErrorKey] = "Could not send the reminder. Check occurrence settings.";
                }
                else
                {
                    TempData[AdminEventErrorKey] = "This event is invalid.";
                }
            }

            return LocalRedirect(BuildRedirectUrl());
        }

        [HttpPost("importexport")]
        [ValidateAntiForgeryToken]
        public IActionResult Importexport(
            [FromForm] int objectId,
            [FromForm] int recordId,
            [FromForm] string action,
            [FromForm] int cboMenus = -1,
            [FromForm(Name = "FileUpload1")] IFormFile fileUpload = null)
        {
            if (string.Equals(action, "cmdExport", StringComparison.OrdinalIgnoreCase))
            {
                if (cboMenus > 0)
                {
                    var menu = MenuEntity.Provider.Get(cboMenus);
                    if (menu != null)
                    {
                        var manager = new MenuPartDataManager();
                        manager.GetMenus().Add(menu.Id, menu);
                        var output = manager.ExportData();
                        var bytes = Encoding.UTF8.GetBytes(output ?? string.Empty);
                        var fileName = $"Menu-XML-{DateTime.Now:yyyyMMdd}.xml";
                        return File(bytes, "application/xml", fileName);
                    }
                }

                TempData[ImportExportErrorKey] = "Please select a valid menu to export.";
                return LocalRedirect(BuildRedirectUrl());
            }

            if (string.Equals(action, "cmdImport", StringComparison.OrdinalIgnoreCase))
            {
                if (fileUpload != null && fileUpload.Length > 0)
                {
                    var doc = new XmlDocument();
                    using (var stream = fileUpload.OpenReadStream())
                    {
                        doc.Load(stream);
                    }

                    var manager = new MenuPartDataManager();
                    manager.InitImport(doc);
                    manager.ImportData(null);
                    TempData[ImportExportStatusKey] = "Import completed successfully!";
                }
                else
                {
                    TempData[ImportExportErrorKey] = "Please choose an XML file to import.";
                }
            }

            return LocalRedirect(BuildRedirectUrl());
        }

        [HttpPost("configdynamicmenuv2")]
        [ValidateAntiForgeryToken]
        public IActionResult Configdynamicmenuv2(
            [FromForm] int objectId,
            [FromForm] int recordId,
            [FromForm] string action,
            [FromForm] int cboMenu = -1,
            [FromForm] string parameterSetName = null)
        {
            if (string.Equals(action, "Save", StringComparison.OrdinalIgnoreCase))
            {
                var element = ResolveParameterizedObject(objectId, recordId);
                if (element != null)
                {
                    var menuParam = element.GetOrCreateParameter(ParameterKeys.MenuId);
                    menuParam.Value = cboMenu.ToString();
                    menuParam.Update();

                    var parameterSetParam = element.GetOrCreateParameter(WConstants.ParameterSetKey);
                    parameterSetParam.Value = parameterSetName ?? string.Empty;
                    parameterSetParam.Update();

                    TempData[ConfigDynamicMenuStatusKey] = "Update successful.";
                }
                else
                {
                    TempData[ConfigDynamicMenuErrorKey] = "Unable to resolve the target element for update.";
                }
            }

            return LocalRedirect(BuildRedirectUrl());
        }

        [HttpPost("surveywelcome")]
        [ValidateAntiForgeryToken]
        public IActionResult Surveywelcome(
            [FromForm] int objectId,
            [FromForm] int recordId,
            [FromForm] string action,
            [FromForm] int listId = -1)
        {
            if (string.Equals(action, "cmdStart", StringComparison.OrdinalIgnoreCase))
            {
                if (listId > 0)
                {
                    return LocalRedirect(BuildRedirectUrl(query =>
                    {
                        query.SetOpen("ListItem");
                        query.Set("ListId", listId.ToString());
                    }));
                }

                TempData[SurveyWelcomeErrorKey] = "Survey list is not available.";
            }

            return LocalRedirect(BuildRedirectUrl());
        }

        [HttpPost("officedetails")]
        [ValidateAntiForgeryToken]
        public IActionResult Officedetails(
            [FromForm] int objectId,
            [FromForm] int recordId,
            [FromForm] string action)
        {
            if (string.Equals(action, "cmdCancel", StringComparison.OrdinalIgnoreCase))
            {
                var element = ResolveParameterizedObject(objectId, recordId);
                var cancelRedirect = element?.GetParameterValue("CancelRedirect");
                if (!string.IsNullOrWhiteSpace(cancelRedirect))
                {
                    if (Url.IsLocalUrl(cancelRedirect))
                        return LocalRedirect(cancelRedirect);

                    if (Uri.TryCreate(cancelRedirect, UriKind.Absolute, out _))
                        return Redirect(cancelRedirect);
                }

                TempData[OfficeDetailsStatusKey] = "Done.";
            }

            return LocalRedirect(BuildRedirectUrl());
        }

        private string BuildRedirectUrl(Action<WQuery> mutate = null, string fallback = "/")
        {
            var referer = Request.Headers.Referer.ToString();
            var localReferer = "/";
            if (!string.IsNullOrWhiteSpace(referer))
            {
                if (Uri.TryCreate(referer, UriKind.Absolute, out var absolute))
                {
                    localReferer = absolute.PathAndQuery;
                }
                else if (Url.IsLocalUrl(referer))
                {
                    localReferer = referer;
                }
            }

            var query = new WQuery(localReferer);
            mutate?.Invoke(query);
            var path = query.BuildQuery();

            if (!Url.IsLocalUrl(path))
                return fallback;

            return path;
        }

        private static ParameterizedWebObject ResolveParameterizedObject(int objectId, int recordId)
        {
            if (recordId < 1)
                return null;

            if (objectId == WebObjects.WebPageElement)
                return WebPageElement.Get(recordId);

            if (objectId == WebObjects.WebPage)
                return WPage.Get(recordId);

            if (objectId == WebObjects.WebPartAdmin)
                return WebPartAdmin.Get(recordId);

            return null;
        }
    }
}
