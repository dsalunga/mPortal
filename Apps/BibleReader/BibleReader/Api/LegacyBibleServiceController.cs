using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCMS.BibleReader.Core;

namespace BibleReader.WebApp.Api
{
    /// <summary>
    /// Legacy-compatible replacement for BibleService.asmx.
    /// Returns ASP.NET AJAX style payloads: { d: ... }.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyBibleServiceController : ControllerBase
    {
        [HttpGet("/BibleService.asmx/HelloWorld")]
        [HttpPost("/BibleService.asmx/HelloWorld")]
        public IActionResult HelloWorld()
        {
            return new JsonResult(new { d = "Hello World" });
        }

        [HttpGet("/BibleService.asmx/GetVerseContent")]
        [HttpPost("/BibleService.asmx/GetVerseContent")]
        public IActionResult GetVerseContent(
            [FromQuery] string versionShortName,
            [FromQuery] int bookCode,
            [FromQuery] int chapterCode,
            [FromQuery] int verseCode,
            [FromBody] VerseRequest request)
        {
            var version = !string.IsNullOrEmpty(versionShortName) ? versionShortName : request?.VersionShortName;
            var book = bookCode > 0 ? bookCode : request?.BookCode ?? 0;
            var chapter = chapterCode > 0 ? chapterCode : request?.ChapterCode ?? 0;
            var verse = verseCode > 0 ? verseCode : request?.VerseCode ?? 0;

            return new JsonResult(new { d = BibleManager.GetVerseContent(version, book, chapter, verse) });
        }

        [HttpGet("/BibleService.asmx/GetVerse")]
        [HttpPost("/BibleService.asmx/GetVerse")]
        public IActionResult GetVerse(
            [FromQuery] string versionShortName,
            [FromQuery] int bookCode,
            [FromQuery] int chapterCode,
            [FromQuery] int verseCode,
            [FromBody] VerseRequest request)
        {
            var version = !string.IsNullOrEmpty(versionShortName) ? versionShortName : request?.VersionShortName;
            var book = bookCode > 0 ? bookCode : request?.BookCode ?? 0;
            var chapter = chapterCode > 0 ? chapterCode : request?.ChapterCode ?? 0;
            var verse = verseCode > 0 ? verseCode : request?.VerseCode ?? 0;

            return new JsonResult(new { d = BibleManager.GetVerse(version, book, chapter, verse) });
        }
    }

    public class VerseRequest
    {
        public string VersionShortName { get; set; }
        public int BookCode { get; set; }
        public int ChapterCode { get; set; }
        public int VerseCode { get; set; }
    }
}
