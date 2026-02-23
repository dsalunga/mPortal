using Microsoft.AspNetCore.Mvc;
using WCMS.BibleReader.Core;

namespace BibleReader.WebApp.Api
{
    /// <summary>
    /// Replaces the legacy ASMX BibleService (BibleService.asmx).
    /// </summary>
    [ApiController]
    [Route("api/bible")]
    public class BibleApiController : ControllerBase
    {
        [HttpGet("verse/content")]
        public IActionResult GetVerseContent(
            [FromQuery] string versionShortName,
            [FromQuery] int bookCode,
            [FromQuery] int chapterCode,
            [FromQuery] int verseCode)
        {
            return Ok(BibleManager.GetVerseContent(versionShortName, bookCode, chapterCode, verseCode));
        }

        [HttpGet("verse")]
        public IActionResult GetVerse(
            [FromQuery] string versionShortName,
            [FromQuery] int bookCode,
            [FromQuery] int chapterCode,
            [FromQuery] int verseCode)
        {
            return Ok(BibleManager.GetVerse(versionShortName, bookCode, chapterCode, verseCode));
        }
    }
}
