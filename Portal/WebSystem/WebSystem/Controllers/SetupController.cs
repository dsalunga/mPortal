using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.Utilities;

namespace WCMS.WebSystem.Controllers
{
    /// <summary>
    /// Modern replacement for legacy Setup.aspx recovery page.
    /// This endpoint is intentionally reachable independent of CMS page resolution.
    /// </summary>
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("Central/Setup")]
    public class SetupController : Controller
    {
        private const string FlashMessagesKey = "__setup_flash_messages";
        private const string WebObjectXmlFallbackNote = "WebObject list source: XML provider fallback (runtime provider not initialized).";
        private const string WebObjectFileFallbackNote = "WebObject list source: WebObject.xml file fallback.";
        private const string DefaultAdminUserName = "admin";
        private const string DefaultAdminPassword = "dev123";
        private const string DefaultAdminEmail = "admin@localhost";
        private static readonly DateTime DefaultAdminPasswordExpiry = new DateTime(2099, 12, 31, 0, 0, 0, DateTimeKind.Local);

        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _lifetime;

        public SetupController(IConfiguration configuration, IHostApplicationLifetime lifetime)
        {
            _configuration = configuration;
            _lifetime = lifetime;
        }

        [HttpGet("")]
        public IActionResult Index([FromQuery] string setupKey = null)
        {
            var access = AuthorizeSetupRequest(setupKey);
            if (!access.IsAuthorized)
                return StatusCode(access.StatusCode, access.Message);

            var model = BuildModel(setupKey ?? string.Empty);
            return View("~/Views/Setup/Index.cshtml", model);
        }

        [HttpPost("")]
        [ValidateAntiForgeryToken]
        public IActionResult Execute([FromForm] SetupCommandInput input)
        {
            var access = AuthorizeSetupRequest(input.SetupKey);
            if (!access.IsAuthorized)
                return StatusCode(access.StatusCode, access.Message);

            var log = new List<string>();
            ExecuteCommand(input, log);
            SaveFlashMessages(log);

            return RedirectToAction(nameof(Index), new { setupKey = input.SetupKey });
        }

        private SetupPageViewModel BuildModel(string setupKey)
        {
            var model = new SetupPageViewModel
            {
                SetupKey = setupKey
            };

            model.Messages.AddRange(ConsumeFlashMessages());

            var db = new DbManager();
            model.Report.AddRange(BuildInspectionReport(db, out var dbConnected));
            model.IsDatabaseReachable = dbConnected;

            if (dbConnected)
            {
                model.Objects.AddRange(BuildObjectRows(model.Report, db.XML_PATH));
            }

            return model;
        }

        private static List<string> BuildInspectionReport(DbManager db, out bool dbConnected)
        {
            var report = new List<string>
            {
                $"DatabaseProvider: {SafeValue(() => DbHelper.Provider.ToString())}",
                $"XmlPath: {db.XML_PATH}",
                $"BackupPath: {db.BackupPath}"
            };

            dbConnected = false;

            try
            {
                using var reader = DbHelper.ExecuteReader(CommandType.Text, "SELECT 1");
                report.Add("Database connection: OK");
                dbConnected = true;
            }
            catch (Exception ex)
            {
                report.Add($"Database connection FAILED: {ex.Message}");
                return report;
            }

            var xmlFile = Path.Combine(db.XML_PATH, DbConstants.XML_FILE);
            report.Add(System.IO.File.Exists(xmlFile)
                ? $"XML definition ({DbConstants.XML_FILE}): OK"
                : $"XML definition ({DbConstants.XML_FILE}): MISSING");

            var items = LoadWebObjectsForSetup(report, "WebObject list error", db.XML_PATH);
            if (items.Count == 0)
            {
                report.Add("WebObject list: empty");
            }
            else
            {
                foreach (var item in items)
                {
                    try
                    {
                        var quotedName = DbSyntax.QuoteIdentifier(item.Name);
                        using var r = DbHelper.ExecuteReader(CommandType.Text, $"SELECT * FROM {quotedName} LIMIT 1");
                        report.Add($"Table {item.Name}: OK");
                    }
                    catch
                    {
                        try
                        {
                            using var r2 = DbHelper.ExecuteReader(CommandType.Text, $"SELECT TOP 1 * FROM {item.Name}");
                            report.Add($"Table {item.Name}: OK");
                        }
                        catch (Exception ex2)
                        {
                            report.Add($"Table {item.Name}: MISSING or ERROR — {ex2.Message}");
                        }
                    }
                }
            }

            return report;
        }

        private static List<SetupObjectRow> BuildObjectRows(List<string> report, string xmlPath)
        {
            var rows = new List<SetupObjectRow>();
            var items = LoadWebObjectsForSetup(report, "Object listing error", xmlPath);
            foreach (var item in items.OrderBy(i => i.Name))
            {
                var count = -1;
                try { count = WebObject.GetCount(item); } catch { }

                rows.Add(new SetupObjectRow
                {
                    Name = item.Name,
                    IdentityColumn = item.IdentityColumn,
                    LastRecordId = item.LastRecordId,
                    DateModified = item.DateModified,
                    Count = count
                });
            }

            return rows;
        }

        private void ExecuteCommand(SetupCommandInput input, List<string> log)
        {
            var command = (input.Command ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(command))
            {
                log.Add("No command provided.");
                return;
            }

            var db = new DbManager();

            switch (command.ToLowerInvariant())
            {
                case "backup":
                    db.Backup(log.Add);
                    break;

                case "restoreall":
                    db.Restore(log.Add);
                    EnsureDefaultAdmin(log);
                    if (input.ResetOnRestore)
                    {
                        log.Add("System reset requested after full restore.");
                        _lifetime.StopApplication();
                    }
                    break;

                case "restoreselected":
                    RestoreSelectedObjects(db, input, log);
                    EnsureDefaultAdmin(log);
                    if (input.ResetOnRestore)
                    {
                        log.Add("System reset requested after selected restore.");
                        _lifetime.StopApplication();
                    }
                    break;

                case "dropall":
                    db.DropAllObjects(log.Add);
                    break;

                case "createdatabase":
                    if (db.CheckCreateDatabase())
                        log.Add("Database created successfully.");
                    else
                        log.Add("Database already exists (or provider does not support create operation).");
                    break;

                case "reset":
                    log.Add("System reset requested.");
                    _lifetime.StopApplication();
                    break;

                default:
                    log.Add($"Unsupported command '{input.Command}'.");
                    break;
            }
        }

        private static void RestoreSelectedObjects(DbManager db, SetupCommandInput input, List<string> log)
        {
            var selected = input.SelectedObjects?
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Select(name => name.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList() ?? new List<string>();

            if (selected.Count == 0)
            {
                log.Add("No objects selected.");
                return;
            }

            var objects = LoadWebObjectsForSetup(log, "Unable to load WebObject list", db.XML_PATH);
            if (objects.Count == 0)
                return;

            foreach (var objectName in selected)
            {
                var item = objects.FirstOrDefault(o => string.Equals(o.Name, objectName, StringComparison.OrdinalIgnoreCase));
                if (item == null)
                {
                    log.Add($"WebObject '{objectName}' not found, skipping.");
                    continue;
                }

                try
                {
                    if (input.RestoreSchema)
                    {
                        db.DropObjectSchema(item, log.Add);
                        var schemaErrors = db.RestoreObjectSchema(item);
                        if (!string.IsNullOrWhiteSpace(schemaErrors))
                            log.Add(schemaErrors);
                    }

                    db.RestoreObjectData(item, log.Add);
                }
                catch (Exception ex)
                {
                    log.Add($"Error restoring {item.Name}: {ex.Message}");
                }
            }
        }

        private static void EnsureDefaultAdmin(List<string> log)
        {
            try
            {
                var user = WebUser.Get(DefaultAdminUserName);
                if (user == null)
                {
                    user = WebUserGroup.GetByGroupId(SystemGroups.ADMINS_GROUP_ID, RecordStatus.Active)
                        .Select(link => WebUser.Get(link.RecordId))
                        .FirstOrDefault(candidate => candidate != null);
                }

                if (user == null)
                {
                    user = new WebUser
                    {
                        DateCreated = DateTime.Now
                    };
                }

                user.UserName = DefaultAdminUserName;
                user.Password = DefaultAdminPassword;
                user.FirstName = string.IsNullOrWhiteSpace(user.FirstName) ? "System" : user.FirstName;
                user.LastName = string.IsNullOrWhiteSpace(user.LastName) ? "Administrator" : user.LastName;
                user.Email = string.IsNullOrWhiteSpace(user.Email) ? DefaultAdminEmail : user.Email;
                user.Status = AccountStatus.ACTIVE;
                user.LastUpdate = DateTime.Now;
                user.LastLogin = DateTime.Now;
                user.PasswordExpiryDate = DefaultAdminPasswordExpiry;
                user.LastLoginFailureDate = WConstants.DateTimeMinValue;
                user.LoginFailureCount = 0;
                user.Update();

                if (!user.IsMemberOf(SystemGroups.ADMINS_GROUP_ID, -1))
                    user.AddToGroup(SystemGroups.ADMINS_GROUP_ID, RecordStatus.Active, user.Id);

                log.Add(string.Format(
                    "Default admin account ensured: {0}/{1} (UserId={2}).",
                    DefaultAdminUserName,
                    DefaultAdminPassword,
                    user.Id));
            }
            catch (Exception ex)
            {
                log.Add("Default admin bootstrap error: " + ex.Message);
            }
        }

        private AccessDecision AuthorizeSetupRequest(string setupKeyCandidate)
        {
            if (!IsLoopbackRequest())
            {
                return AccessDecision.Forbidden("Setup endpoint is only available from localhost.");
            }

            var expectedKey = (_configuration["WCMS:SetupApiKey"] ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(expectedKey))
            {
                return AccessDecision.Allow();
            }

            var candidates = new List<string>();
            if (!string.IsNullOrWhiteSpace(setupKeyCandidate))
                candidates.Add(setupKeyCandidate);

            if (Request.Headers.TryGetValue("X-WCMS-Setup-Key", out var headerValue))
            {
                var headerKey = headerValue.ToString();
                if (!string.IsNullOrWhiteSpace(headerKey))
                    candidates.Add(headerKey);
            }

            if (Request.Query.TryGetValue("setupKey", out var queryValue))
            {
                var queryKey = queryValue.ToString();
                if (!string.IsNullOrWhiteSpace(queryKey))
                    candidates.Add(queryKey);
            }

            foreach (var candidate in candidates)
            {
                if (IsFixedTimeEqual(expectedKey, candidate))
                    return AccessDecision.Allow();
            }

            return AccessDecision.Unauthorized("Missing or invalid setup key.");
        }

        private bool IsLoopbackRequest()
        {
            var remoteIp = HttpContext.Connection.RemoteIpAddress;
            if (remoteIp == null)
                return false;

            return IPAddress.IsLoopback(remoteIp)
                   || (remoteIp.IsIPv4MappedToIPv6 && IPAddress.IsLoopback(remoteIp.MapToIPv4()));
        }

        private static bool IsFixedTimeEqual(string expected, string actual)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(expected);
            var actualBytes = Encoding.UTF8.GetBytes(actual);
            return expectedBytes.Length == actualBytes.Length
                   && CryptographicOperations.FixedTimeEquals(expectedBytes, actualBytes);
        }

        private void SaveFlashMessages(List<string> messages)
        {
            var nonEmpty = messages.Where(m => !string.IsNullOrWhiteSpace(m)).ToArray();
            TempData[FlashMessagesKey] = string.Join("\n", nonEmpty);
        }

        private List<string> ConsumeFlashMessages()
        {
            if (!TempData.TryGetValue(FlashMessagesKey, out var value) || value == null)
                return new List<string>();

            var raw = value.ToString() ?? string.Empty;
            return raw.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private static string SafeValue(Func<string> getter)
        {
            try { return getter(); }
            catch (Exception ex) { return $"{ex.GetType().Name}: {ex.Message}"; }
        }

        private static List<WebObject> LoadWebObjectsForSetup(List<string> messages, string failurePrefix, string xmlPath)
        {
            Exception runtimeEx = null;
            Exception xmlProviderEx = null;
            Exception fileEx = null;

            try
            {
                return WebObject.GetList()?.ToList() ?? new List<WebObject>();
            }
            catch (Exception ex)
            {
                runtimeEx = ex;
            }

            try
            {
                var xmlItems = WebObject.XmlProvider?.GetList()?.ToList();
                if (xmlItems != null)
                {
                    AddMessageOnce(messages, WebObjectXmlFallbackNote);
                    return xmlItems;
                }
            }
            catch (Exception ex)
            {
                xmlProviderEx = ex;
            }

            try
            {
                var fileItems = LoadWebObjectsFromXmlFile(xmlPath);
                if (fileItems.Count > 0)
                {
                    AddMessageOnce(messages, WebObjectFileFallbackNote);
                    return fileItems;
                }
            }
            catch (Exception ex)
            {
                fileEx = ex;
            }

            var message = fileEx?.Message
                          ?? xmlProviderEx?.Message
                          ?? runtimeEx?.Message
                          ?? "Unknown setup WebObject loading failure.";
            messages?.Add($"{failurePrefix}: {message}");
            return new List<WebObject>();
        }

        private static List<WebObject> LoadWebObjectsFromXmlFile(string xmlPath)
        {
            var items = new List<WebObject>();
            if (string.IsNullOrWhiteSpace(xmlPath))
                return items;

            var xmlFile = Path.Combine(xmlPath, DbConstants.XML_FILE);
            if (!System.IO.File.Exists(xmlFile))
                return items;

            var ds = new DataSet();
            ds.ReadXml(xmlFile, XmlReadMode.ReadSchema);
            if (!ds.Tables.Contains(WebObject.OBJECT_NAME))
                return items;

            foreach (DataRow row in ds.Tables[WebObject.OBJECT_NAME].Rows)
            {
                var name = Convert.ToString(row["Name"]) ?? string.Empty;
                if (string.IsNullOrWhiteSpace(name))
                    continue;

                items.Add(new WebObject
                {
                    Name = name,
                    IdentityColumn = Convert.ToString(row["IdentityColumn"]) ?? string.Empty,
                    LastRecordId = SafeToInt(row, "LastRecordId"),
                    DateModified = SafeToDateTime(row, "DateModified")
                });
            }

            return items;
        }

        private static int SafeToInt(DataRow row, string columnName)
        {
            if (!row.Table.Columns.Contains(columnName))
                return 0;

            var raw = row[columnName];
            if (raw == null || raw == DBNull.Value)
                return 0;

            return int.TryParse(raw.ToString(), out var value) ? value : 0;
        }

        private static DateTime SafeToDateTime(DataRow row, string columnName)
        {
            if (!row.Table.Columns.Contains(columnName))
                return DateTime.MinValue;

            var raw = row[columnName];
            if (raw == null || raw == DBNull.Value)
                return DateTime.MinValue;

            return DateTime.TryParse(raw.ToString(), out var value) ? value : DateTime.MinValue;
        }

        private static void AddMessageOnce(List<string> messages, string message)
        {
            if (messages == null || string.IsNullOrWhiteSpace(message))
                return;

            if (!messages.Any(m => string.Equals(m, message, StringComparison.Ordinal)))
                messages.Add(message);
        }

        private readonly record struct AccessDecision(bool IsAuthorized, int StatusCode, string Message)
        {
            public static AccessDecision Allow() => new(true, 200, string.Empty);
            public static AccessDecision Forbidden(string message) => new(false, 403, message);
            public static AccessDecision Unauthorized(string message) => new(false, 401, message);
        }
    }

    public sealed class SetupCommandInput
    {
        public string Command { get; set; } = string.Empty;
        public bool ResetOnRestore { get; set; } = true;
        public bool RestoreSchema { get; set; }
        public string[] SelectedObjects { get; set; } = Array.Empty<string>();
        public string SetupKey { get; set; } = string.Empty;
    }

    public sealed class SetupPageViewModel
    {
        public string SetupKey { get; set; } = string.Empty;
        public bool IsDatabaseReachable { get; set; }
        public List<string> Messages { get; set; } = new();
        public List<string> Report { get; set; } = new();
        public List<SetupObjectRow> Objects { get; set; } = new();
    }

    public sealed class SetupObjectRow
    {
        public string Name { get; set; } = string.Empty;
        public string IdentityColumn { get; set; } = string.Empty;
        public long LastRecordId { get; set; }
        public DateTime DateModified { get; set; }
        public int Count { get; set; }
    }
}
