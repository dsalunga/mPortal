using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WCMS.Framework;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration.Controllers
{
    public class AttendanceController : ApiController
    {
        // GET: api/Attendance
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Attendance/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Attendance
        public JObject Post([FromBody]JToken value)
        {
            return (new JObject(
                new JProperty("member",
                    new JObject(
                        new JProperty("firstName", "Daniel"),
                        new JProperty("lastName", "Salunga"),
                        new JProperty("nickname", "Den"),
                        new JProperty("photoUrl", "")
                        )
                    )));
        }

        [Route("api/v1/Attendance/LiveStream")]
        [HttpPost]
        public HttpResponseMessage LiveStreamPost([FromBody]JToken value)
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
                item.ServiceName = shortServiceName; // Fetch value from External via serviceScheduleId
                item.AdditionalNotes = remarks;
                item.Status = attendanceStatus; //LessonReviewerSessionStatus.PendingApproval;
                item.Extra = new JObject(new JProperty("coAttendees", coAttendees)).ToString();
                item.Update();
                return Request.CreateResponse(HttpStatusCode.OK, new JObject());
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject());
        }

        // PUT: api/Attendance/5
        [Route("api/v1/Attendance/{externalId}")]
        [HttpPost]
        public HttpResponseMessage Post(string externalId, [FromBody]JToken value)
        {
            MemberLink link = null;
            bool isDummy = false;
            if (!string.IsNullOrEmpty(externalId) && ((isDummy = externalId.Equals("MEROCKS123", StringComparison.InvariantCultureIgnoreCase)) || (link = MemberLink.Provider.Get(externalId)) != null))
            {
                if (isDummy)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new JObject(
                        new JProperty("attendance",
                            new JObject(
                                new JProperty("firstName", "firstName"),
                                new JProperty("lastName", "lastName"),
                                new JProperty("nickname", "nickname"),
                                //new JProperty("email", "email@someorg.org.sg"),
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
                    return Request.CreateResponse(HttpStatusCode.OK, new JObject(
                        new JProperty("attendance",
                            new JObject(
                                new JProperty("firstName", user.FirstName),
                                new JProperty("lastName", user.LastName),
                                new JProperty("nickname", link.Nickname),
                        //new JProperty("email", user.Email),
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
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // GET: api/Attendance/GetAttendees/?id=Brazil2015
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Group Name</param>
        /// <returns></returns>
        [Route("api/v1/Attendance/GetAttendees/")]
        [HttpGet]
        public HttpResponseMessage GetAttendees(string id, int page = -1)
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
                    var link = MemberLink.Provider.GetByUserId(user.Id);
                    if (link != null)
                    {
                        var country = link.LocaleCountry;
                        items.Add(new JObject(
                                new JProperty("firstName", user.FirstName),
                                new JProperty("lastName", user.LastName),
                                new JProperty("nickname", link.Nickname),
                                new JProperty("externalId", link.ExternalIdNo),
                            //new JProperty("email", user.Email),
                                new JProperty("photoUrl", user.GetPhotoPath("200x200", true)),
                                new JProperty("country", country != null ? country.CountryName : ""),
                                new JProperty("chapter", link.Locale),
                                new JProperty("lastAttendance", DateTime.Now.AddDays(-2)),
                                new JProperty("status", 0)
                            ));
                    }
                }

                var json = new JObject(new JProperty("attendees", items));
                return Request.CreateResponse(HttpStatusCode.OK, json);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // DELETE: api/Attendance/5
        public void Delete(int id)
        {
        }
    }
}
