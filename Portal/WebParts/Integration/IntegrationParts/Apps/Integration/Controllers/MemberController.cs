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
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;

namespace WCMS.WebSystem.Apps.Integration.Controllers
{
    public class MemberController : ApiController
    {
        private WebUser GetUser(string id)
        {
            var user = WebUser.GetByEmailOrUsername(id);
            if (user == null && MemberHelper.IsExternalId(id))
            {
                var link = MemberLink.Provider.Get(id);
                if (link != null)
                    user = link.User;
            }

            return user;
        }

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

        [Route("api/v1/Member/GetUserId/")]
        [HttpGet]
        public HttpResponseMessage GetUserId(string id, int checkExternal = 0)
        {
            int memberId = -1;
            int userId = -1;
            var user = GetUser(id);

            if (user == null)
            {
                if (checkExternal == 0)
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                userId = user.Id;
            }

            if ((checkExternal == 1 && userId == -1 || checkExternal == 2) && MemberHelper.IsExternalId(id))
            {
                var client = WCMS.WebSystem.Apps.Integration.ExternalMemberWS.MemberSoapClient.GetNewClientInstance();
                memberId = (int)client.GetMemberID(id);
            }

            return Request.CreateResponse(HttpStatusCode.OK,
                new JObject(
                    new JProperty("userId", userId),
                    new JProperty("memberId", memberId)
                ));
        }

        [Route("api/v1/Member/FindInfo/")]
        [HttpGet]
        public HttpResponseMessage FindInfo(string id)
        {
            int memberId = -1;
            int userId = -1;
            var user = GetUser(id);
            Member member = null;

            if (user == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                userId = user.Id;

            if (MemberHelper.IsExternalId(id))
            {
                var client = WCMS.WebSystem.Apps.Integration.ExternalMemberWS.MemberSoapClient.GetNewClientInstance();
                memberId = (int)client.GetMemberID(id);
                if (memberId > 0)
                    member = Member.RemoteProvider.Get(memberId);
            }

            var response = new JObject(
                    new JProperty("userId", userId),
                    new JProperty("memberId", memberId)
                );

            if (user != null)
                response.Add(new JProperty("name", AccountHelper.GetPrefixedName(user, NamePrefixes.Brotherhood)));
            else if (member != null)
                response.Add(new JProperty("name", AccountHelper.GetPrefixedName(MemberHelper.CreateDraftUser(member), NamePrefixes.Brotherhood)));

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // GET: api/Member/5
        // [Route("api/v1/Member/{id}")]
        [Route("api/v1/Member/")]
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            var user = GetUser(id);
            if (user == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var link = MemberLink.Provider.GetByUserId(user.Id);
            if (link == null)
                link = new MemberLink();

            var country = link.LocaleCountry;
            return Request.CreateResponse(HttpStatusCode.OK, new JObject(
                new JProperty("member",
                    new JObject(
                        new JProperty("externalId", link.ExternalIdNo),
                        new JProperty("fistName", user.FirstName),
                        new JProperty("lastName", user.LastName),
                        new JProperty("nickname", link.Nickname),
                        new JProperty("email", user.Email),
                        new JProperty("mobile", user.MobileNumber),
                        new JProperty("gender", user.Gender.ToString()),
                        new JProperty("photoUrl", user.GetPhotoPath("200x200", true)),
                        new JProperty("country", country != null ? country.CountryName : ""),
                        new JProperty("chapter", link.Locale),
                        new JProperty("dob", link.MembershipDate)
                    ))));
        }

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
