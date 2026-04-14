using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AttendanceController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok("value");
        }

        [HttpPost]
        public IActionResult Post([FromBody] JToken value)
        {
            return Ok(new JObject(
                new JProperty("member",
                    new JObject(
                        new JProperty("firstName", "FirstName"),
                        new JProperty("lastName", "LastName"),
                        new JProperty("nickname", "Nickname"),
                        new JProperty("photoUrl", "")
                        )
                    )));
        }

        [Route("LiveStream")]
        [HttpPost]
        public IActionResult LiveStreamPost([FromBody] JToken value)
        {
            if (value != null)
            {
                var memberId = value.Value<int>("memberId");
                var pageId = value.Value<int>("pageId");
                var serviceScheduleId = value.Value<int>("serviceScheduleId");
                var shortServiceName = value.Value<string>("shortServiceName");
                var remarks = value.Value<string>("remarks");
                var startTime = value.Value<DateTime>("startTime");
                var streamType = value.Value<int>("streamType");
                var attendanceStatus = value.Value<int>("attendanceStatus");
                var coAttendees = value["coAttendees"] as JArray;

                LessonReviewerSession item = null;
                var items = LessonReviewerSession.Provider.GetList(memberId, WConstants.NULL_ID, serviceScheduleId);
                if (items.Count > 0)
                {
                    item = items.First();
                }
                else
                {
                    item = new LessonReviewerSession();
                    item.ServiceScheduleID = serviceScheduleId;
                    item.ServiceStartDate = startTime;
                    item.DateStarted = DateTime.Now;
                    item.DateCompleted = DateTime.Now;
                    item.MemberId = memberId;
                    item.PageId = pageId;
                    item.AttendanceType = streamType;
                }
                item.ServiceName = shortServiceName;
                item.AdditionalNotes = remarks;
                item.Status = attendanceStatus;
                item.Extra = new JObject(new JProperty("coAttendees", coAttendees)).ToString();
                item.Update();
                return Ok(new JObject());
            }

            return BadRequest(new JObject());
        }

        [Route("{externalId}")]
        [HttpPost]
        public IActionResult Post(string externalId, [FromBody] JToken value)
        {
            MemberLink link = null;
            bool isDummy = false;
            if (!string.IsNullOrEmpty(externalId) && ((isDummy = externalId.Equals("MEROCKS123", StringComparison.InvariantCultureIgnoreCase)) || (link = MemberLink.Provider.Get(externalId)) != null))
            {
                if (isDummy)
                {
                    return Ok(new JObject(
                        new JProperty("attendance",
                            new JObject(
                                new JProperty("firstName", "firstName"),
                                new JProperty("lastName", "lastName"),
                                new JProperty("nickname", "nickname"),
                                new JProperty("photoUrl", "https://external.someorg.org/photos/200x200/photo.jpg"),
                                new JProperty("country", "country"),
                                new JProperty("chapter", "chapter"),
                                new JProperty("lastAttendance", DateTime.Now.AddDays(-2)),
                                new JProperty("status", 0)
                            ))));
                }
                else
                {
                    var user = link.User;
                    var country = link.LocaleCountry;
                    return Ok(new JObject(
                        new JProperty("attendance",
                            new JObject(
                                new JProperty("firstName", user.FirstName),
                                new JProperty("lastName", user.LastName),
                                new JProperty("nickname", link.Nickname),
                                new JProperty("photoUrl", user.GetPhotoPath("200x200", true)),
                                new JProperty("country", country != null ? country.CountryName : ""),
                                new JProperty("chapter", link.Locale),
                                new JProperty("lastAttendance", DateTime.Now.AddDays(-2)),
                                new JProperty("status", 0)
                            ))));
                }
            }
            else
            {
                return NotFound();
            }
        }

        [Route("GetAttendees")]
        [HttpGet]
        public IActionResult GetAttendees(string id, int page = -1)
        {
            int sizePerPage = 50;
            WebGroup group = WebGroup.SelectNode(id);
            if (group != null)
            {
                var attendees = WebUser.GetList(group.Id);

                if (page > 0)
                {
                    attendees = attendees.Skip(sizePerPage * page)
                                    .Take(sizePerPage);
                }

                var items = new JArray();
                foreach (var user in attendees)
                {
                    var memberLink = MemberLink.Provider.GetByUserId(user.Id);
                    if (memberLink != null)
                    {
                        var country = memberLink.LocaleCountry;
                        items.Add(new JObject(
                                new JProperty("firstName", user.FirstName),
                                new JProperty("lastName", user.LastName),
                                new JProperty("nickname", memberLink.Nickname),
                                new JProperty("externalId", memberLink.ExternalIdNo),
                                new JProperty("photoUrl", user.GetPhotoPath("200x200", true)),
                                new JProperty("country", country != null ? country.CountryName : ""),
                                new JProperty("chapter", memberLink.Locale),
                                new JProperty("lastAttendance", DateTime.Now.AddDays(-2)),
                                new JProperty("status", 0)
                            ));
                    }
                }

                var json = new JObject(new JProperty("attendees", items));
                return Ok(json);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
