using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;

namespace WCMS.WebSystem.Apps.Integration.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MemberController : ControllerBase
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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        [Route("GetUserId")]
        [HttpGet]
        public IActionResult GetUserId(string id, int checkExternal = 0)
        {
            int memberId = -1;
            int userId = -1;
            var user = GetUser(id);

            if (user == null)
            {
                if (checkExternal == 0)
                    return NotFound();
            }
            else
            {
                userId = user.Id;
            }

            if ((checkExternal == 1 && userId == -1 || checkExternal == 2) && MemberHelper.IsExternalId(id))
            {
                var client = MemberSoapClient.GetNewClientInstance();
                memberId = (int)client.GetMemberID(id);
            }

            return Ok(new JObject(
                    new JProperty("userId", userId),
                    new JProperty("memberId", memberId)
                ));
        }

        [Route("FindInfo")]
        [HttpGet]
        public IActionResult FindInfo(string id)
        {
            int memberId = -1;
            int userId = -1;
            var user = GetUser(id);
            Member member = null;

            if (user == null)
                return NotFound();
            else
                userId = user.Id;

            if (MemberHelper.IsExternalId(id))
            {
                var client = MemberSoapClient.GetNewClientInstance();
                memberId = (int)client.GetMemberID(id);
                if (memberId > 0)
                    member = Member.RemoteProvider.Get(memberId);
            }

            var response = new JObject(
                    new JProperty("userId", userId),
                    new JProperty("memberId", memberId)
                );

            if (user != null)
                response.Add(new JProperty("name", AccountHelper.GetPrefixedName(user, NamePrefixes.Salutation)));
            else if (member != null)
                response.Add(new JProperty("name", AccountHelper.GetPrefixedName(MemberHelper.CreateDraftUser(member), NamePrefixes.Salutation)));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var user = GetUser(id);
            if (user == null)
                return NotFound();

            var link = MemberLink.Provider.GetByUserId(user.Id);
            if (link == null)
                link = new MemberLink();

            var country = link.LocaleCountry;
            return Ok(new JObject(
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
