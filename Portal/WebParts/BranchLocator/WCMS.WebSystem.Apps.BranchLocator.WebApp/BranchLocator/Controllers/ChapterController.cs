using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.BranchLocator;

namespace WCMS.WebSystem.BranchLocator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ChapterController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        [Route("HasAnnouncements/{localeId}")]
        [HttpGet]
        public IActionResult HasAnnouncements(int localeId)
        {
            var lastUpdate = WConstants.DateTimeMinValue;
            var global = false;
            var locale = false;
            var enabled = false;

            var central = MChapter.GetCentral();
            if (central != null && central.HasExtra)
            {
                var announcement = central.GetExtra().Announcement;
                if (announcement.Enabled && !string.IsNullOrEmpty(announcement.Slides))
                {
                    global = true;
                    lastUpdate = announcement.LastUpdate;
                    enabled = true;
                }
            }

            var chapter = MChapter.Provider.GetByLocaleId(localeId);
            if (chapter != null && chapter.HasExtra)
            {
                var announcement = chapter.GetExtra().Announcement;
                if (announcement.Enabled && !string.IsNullOrEmpty(announcement.Slides))
                {
                    enabled = true;
                    locale = true;
                    if (lastUpdate < announcement.LastUpdate)
                        lastUpdate = announcement.LastUpdate;
                }
            }

            if (enabled)
                return Ok(new JObject(
                    new JProperty("lastUpdate", TimeUtil.ToISOString(lastUpdate)),
                    new JProperty("global", global),
                    new JProperty("locale", locale)
                ));
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] JToken value)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] JToken value)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
