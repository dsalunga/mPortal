using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.BranchLocator;

namespace WCMS.WebSystem.BranchLocator.Controllers
{
    public class ChapterController : ApiController
    {
        // GET: api/Member
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //[Route("api/v1/Member/GetUserId2/")]
        //[HttpGet]
        //public HttpResponseMessage GetUserId2(string id)
        //{
        //    var userId = getUserId(id);

        //    if (userId == -1)
        //        return Request.CreateResponse(HttpStatusCode.NotFound);

        //    return Request.CreateResponse(HttpStatusCode.OK, new JObject(new JProperty("userId", userId)));
        //}

        [Route("api/v1/Chapter/HasAnnouncements/{LocaleId}")]
        [HttpGet]
        public HttpResponseMessage HasAnnouncements(int localeId)
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
                return Request.CreateResponse(HttpStatusCode.OK, new JObject(
                    new JProperty("lastUpdate", TimeUtil.ToISOString(lastUpdate)),
                    new JProperty("global", global),
                    new JProperty("locale", locale)
                ));
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        /*
        [Route("api/v1/Member/FindInfo/")]
        [HttpGet]
        public HttpResponseMessage FindInfo(string id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        // GET: api/Member/5
        [Route("api/v1/Member/")]
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            return null;
        }
        */

        // POST: api/Member
        public void Post([FromBody]JToken value)
        {
        }

        // PUT: api/Member/5
        public void Put(int id, [FromBody]JToken value)
        {
        }

        // DELETE: api/Member/5
        public void Delete(int id)
        {
        }
    }
}
