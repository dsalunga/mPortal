using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCMS.WebSystem.WebParts.BibleReader;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for Apps/Integration/BibleReader/BibleService.asmx.
    /// Returns ASP.NET AJAX style payloads: { d: ... }.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyIntegrationBibleServiceController : ControllerBase
    {
        [HttpGet("/Apps/Integration/BibleReader/BibleService.asmx/GetUserSession")]
        [HttpPost("/Apps/Integration/BibleReader/BibleService.asmx/GetUserSession")]
        [HttpGet("/Content/Parts/Integration/BibleReader/BibleService.asmx/GetUserSession")]
        [HttpPost("/Content/Parts/Integration/BibleReader/BibleService.asmx/GetUserSession")]
        public IActionResult GetUserSession([FromQuery] string guid, [FromBody] GuidRequest request)
        {
            var effectiveGuid = !string.IsNullOrEmpty(guid) ? guid : request?.Guid;
            return new JsonResult(new { d = BibleUserSession.Get(effectiveGuid) });
        }

        [HttpGet("/Apps/Integration/BibleReader/BibleService.asmx/GetBibleAppAccessInfo")]
        [HttpPost("/Apps/Integration/BibleReader/BibleService.asmx/GetBibleAppAccessInfo")]
        [HttpGet("/Content/Parts/Integration/BibleReader/BibleService.asmx/GetBibleAppAccessInfo")]
        [HttpPost("/Content/Parts/Integration/BibleReader/BibleService.asmx/GetBibleAppAccessInfo")]
        public IActionResult GetBibleAppAccessInfo([FromQuery] int userId, [FromBody] UserIdRequest request)
        {
            var effectiveUserId = userId > 0 ? userId : request?.UserId ?? 0;
            var item = BibleAccess.Provider.Get(effectiveUserId);

            if (item != null && item.AppAccessCount == -1)
                item.AppAccessCount = BibleAppConfig.AppDownloadCount;

            return new JsonResult(new { d = new BibleAccessResult(item) });
        }

        [HttpGet("/Apps/Integration/BibleReader/BibleService.asmx/PerformBibleAppDownload")]
        [HttpPost("/Apps/Integration/BibleReader/BibleService.asmx/PerformBibleAppDownload")]
        [HttpGet("/Content/Parts/Integration/BibleReader/BibleService.asmx/PerformBibleAppDownload")]
        [HttpPost("/Content/Parts/Integration/BibleReader/BibleService.asmx/PerformBibleAppDownload")]
        public IActionResult PerformBibleAppDownload([FromQuery] int userId, [FromBody] UserIdRequest request)
        {
            var effectiveUserId = userId > 0 ? userId : request?.UserId ?? 0;
            var result = PerformBibleAppDownloadCore(effectiveUserId);
            return new JsonResult(new { d = result });
        }

        [HttpGet("/Apps/Integration/BibleReader/BibleService.asmx/GetBibleVersionAccessInfo")]
        [HttpPost("/Apps/Integration/BibleReader/BibleService.asmx/GetBibleVersionAccessInfo")]
        [HttpGet("/Content/Parts/Integration/BibleReader/BibleService.asmx/GetBibleVersionAccessInfo")]
        [HttpPost("/Content/Parts/Integration/BibleReader/BibleService.asmx/GetBibleVersionAccessInfo")]
        public IActionResult GetBibleVersionAccessInfo(
            [FromQuery] int userId,
            [FromQuery] int bibleVersionId,
            [FromBody] UserVersionRequest request)
        {
            var effectiveUserId = userId > 0 ? userId : request?.UserId ?? 0;
            var effectiveVersionId = bibleVersionId > 0 ? bibleVersionId : request?.BibleVersionId ?? 0;

            var item = BibleAccess.Provider.Get(effectiveUserId);
            if (item != null)
            {
                var items = BibleVersionAccess.Provider.GetList(item.Id);
                if (items.Any())
                {
                    var accessInfo = items.FirstOrDefault(i => i.BibleVersionId == effectiveVersionId);
                    if (accessInfo != null)
                    {
                        if (accessInfo.VersionAccessCount == -1)
                            accessInfo.VersionAccessCount = BibleAppConfig.VersionDownloadCount;

                        return new JsonResult(new { d = new BibleVersionAccessResult(item, accessInfo, BibleAccessStatuses.SUCCESS) });
                    }
                }
            }

            return new JsonResult(new { d = new BibleVersionAccessResult(item, default(BibleVersionAccess), BibleAccessStatuses.ERROR) });
        }

        [HttpGet("/Apps/Integration/BibleReader/BibleService.asmx/GetBibleVersionAccessList")]
        [HttpPost("/Apps/Integration/BibleReader/BibleService.asmx/GetBibleVersionAccessList")]
        [HttpGet("/Content/Parts/Integration/BibleReader/BibleService.asmx/GetBibleVersionAccessList")]
        [HttpPost("/Content/Parts/Integration/BibleReader/BibleService.asmx/GetBibleVersionAccessList")]
        public IActionResult GetBibleVersionAccessList([FromQuery] int userId, [FromBody] UserIdRequest request)
        {
            var effectiveUserId = userId > 0 ? userId : request?.UserId ?? 0;
            var item = BibleAccess.Provider.Get(effectiveUserId);
            if (item != null)
            {
                item.VersionAccesses = null;
                item.PopulateVersionAccesses();

                foreach (var accessInfo in item.VersionAccesses)
                {
                    if (accessInfo.VersionAccessCount == -1)
                        accessInfo.VersionAccessCount = BibleAppConfig.VersionDownloadCount;
                }

                return new JsonResult(new { d = new BibleVersionAccessResult(item, null, BibleAccessStatuses.SUCCESS) });
            }

            return new JsonResult(new { d = new BibleVersionAccessResult(item, null, BibleAccessStatuses.ERROR) });
        }

        [HttpGet("/Apps/Integration/BibleReader/BibleService.asmx/PerformBibleVersionDownload")]
        [HttpPost("/Apps/Integration/BibleReader/BibleService.asmx/PerformBibleVersionDownload")]
        [HttpGet("/Content/Parts/Integration/BibleReader/BibleService.asmx/PerformBibleVersionDownload")]
        [HttpPost("/Content/Parts/Integration/BibleReader/BibleService.asmx/PerformBibleVersionDownload")]
        public IActionResult PerformBibleVersionDownload(
            [FromQuery] int userId,
            [FromQuery] int bibleVersionId,
            [FromBody] UserVersionRequest request)
        {
            var effectiveUserId = userId > 0 ? userId : request?.UserId ?? 0;
            var effectiveVersionId = bibleVersionId > 0 ? bibleVersionId : request?.BibleVersionId ?? 0;
            var result = PerformBibleVersionDownloadCore(effectiveUserId, effectiveVersionId);
            return new JsonResult(new { d = result });
        }

        private static BibleAccessResult PerformBibleAppDownloadCore(int userId)
        {
            var item = BibleAccess.Provider.Get(userId);
            if (item != null)
            {
                if (item.AppAccessCount != 0)
                {
                    if (item.AppAccessCount == -1)
                        item.AppAccessCount = BibleAppConfig.AppDownloadCount - 1;
                    else
                        item.AppAccessCount--;

                    item.Update();
                    return new BibleAccessResult(item);
                }

                return new BibleAccessResult(item, BibleAccessStatuses.DENIED);
            }

            item = new BibleAccess
            {
                UserId = userId,
                AppAccessCount = BibleAppConfig.AppDownloadCount
            };
            item.Update();

            return new BibleAccessResult(item, BibleAccessStatuses.ERROR);
        }

        private static BibleVersionAccessResult PerformBibleVersionDownloadCore(int userId, int bibleVersionId)
        {
            BibleVersionAccess accessInfo = null;
            var item = BibleAccess.Provider.Get(userId);

            void CreateVersionAccessEntry()
            {
                if (bibleVersionId > 0)
                {
                    accessInfo = new BibleVersionAccess
                    {
                        BibleAccessId = item.Id,
                        BibleVersionId = bibleVersionId,
                        VersionAccessCount = BibleAppConfig.VersionDownloadCount - 1
                    };
                    accessInfo.Update();
                }
            }

            if (item != null)
            {
                var items = BibleVersionAccess.Provider.GetList(item.Id);
                accessInfo = items.FirstOrDefault(i => i.BibleVersionId == bibleVersionId);
                if (items.Any() && accessInfo != null)
                {
                    if (accessInfo.VersionAccessCount != 0)
                    {
                        if (accessInfo.VersionAccessCount == -1)
                            accessInfo.VersionAccessCount = BibleAppConfig.VersionDownloadCount - 1;
                        else
                            accessInfo.VersionAccessCount--;

                        accessInfo.Update();
                        return new BibleVersionAccessResult(item, accessInfo, BibleAccessStatuses.SUCCESS);
                    }

                    return new BibleVersionAccessResult(item, accessInfo, BibleAccessStatuses.DENIED);
                }

                CreateVersionAccessEntry();
                return new BibleVersionAccessResult(item, accessInfo, BibleAccessStatuses.SUCCESS);
            }

            if (userId > 0)
            {
                item = new BibleAccess
                {
                    UserId = userId,
                    AppAccessCount = BibleAppConfig.AppDownloadCount
                };
                item.Update();

                CreateVersionAccessEntry();
                return new BibleVersionAccessResult(item, accessInfo, BibleAccessStatuses.SUCCESS);
            }

            return new BibleVersionAccessResult(null, null, BibleAccessStatuses.ERROR);
        }
    }

    public class GuidRequest
    {
        public string Guid { get; set; }
    }

    public class UserIdRequest
    {
        public int UserId { get; set; }
    }

    public class UserVersionRequest
    {
        public int UserId { get; set; }
        public int BibleVersionId { get; set; }
    }
}
