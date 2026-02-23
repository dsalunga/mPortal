using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Shared;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Replaces the legacy WCF Member.svc and ASMX MemberService.asmx in the Integration module.
    /// </summary>
    [ApiController]
    [Route("api/member")]
    public class MemberApiController : ControllerBase
    {
        // --- From Member.svc (WCF) ---

        [HttpPost("check")]
        public IActionResult Check([FromBody] MemberCheckRequest request)
        {
            var user = AccountHelper.ValidateLogin(request.UserName, request.Password);
            if (user != null && user.IsActive)
            {
                if (request.IncludeAllAccounts)
                    return Ok(true);

                MemberLink link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null && link.MemberId > 0)
                    return Ok(true);
            }

            return Ok(false);
        }

        [HttpPost("info")]
        public IActionResult GetInfo([FromBody] MemberCheckRequest request)
        {
            var user = AccountHelper.ValidateLogin(request.UserName, request.Password);
            if (user != null && user.IsActive)
            {
                MemberLink link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null && link.MemberId > 0)
                    return Ok(new WSMemberInfo(user, link));

                if (request.IncludeAllAccounts)
                    return Ok(new WSMemberInfo(user));
            }

            return Ok((WSMemberInfo)null);
        }

        // --- From MemberService.asmx (Profile) ---

        [HttpPut("{memberId:int}/address")]
        public IActionResult UpdatePrimaryAddress(int memberId, [FromBody] WebAddress address)
        {
            if (address != null)
            {
                var link = MemberLink.Provider.GetByMemberId(memberId);
                if (link != null && address.LastUpdated > link.LastUpdate)
                {
                    var user = link.User;
                    if (user != null)
                    {
                        var homeAddress = user.GetAddress(AddressTags.Home);
                        if (homeAddress == null)
                        {
                            homeAddress = new WebAddress();
                            homeAddress.Tag = AddressTags.Home;
                            homeAddress.ObjectId = WebObjects.WebUser;
                            homeAddress.RecordId = user.Id;
                        }

                        homeAddress.AddressLine1 = address.AddressLine1;
                        homeAddress.AddressLine2 = address.AddressLine2;
                        homeAddress.CityTown = address.CityTown;
                        homeAddress.ZipCode = address.ZipCode;
                        homeAddress.PhoneNumber = address.PhoneNumber;

                        var states = CountryState.GetList();
                        var state = states.FirstOrDefault(i => i.StateName.Equals(address.StateProvince, StringComparison.InvariantCultureIgnoreCase));
                        if (state != null)
                            homeAddress.StateProvinceCode = state.StateCode;
                        else
                            homeAddress.StateProvince = address.StateProvince;

                        var countries = Country.GetList();
                        var country = countries.FirstOrDefault(i => i.CountryName.Equals(address.CountryName, StringComparison.InvariantCultureIgnoreCase));
                        if (country != null)
                            homeAddress.CountryCode = country.CountryCode;
                        else
                            homeAddress.CountryName = address.CountryName;

                        homeAddress.Update();
                        return Ok(true);
                    }
                }
            }

            return Ok(false);
        }

        [HttpGet("{memberId:int}/address")]
        public IActionResult GetPrimaryAddress(int memberId)
        {
            var link = MemberLink.Provider.GetByMemberId(memberId);
            if (link != null)
            {
                var user = link.User;
                if (user != null)
                    return Ok(user.GetAddress(AddressTags.Home));
            }

            return Ok((WebAddress)null);
        }

        [HttpGet("{memberId:int}/profile")]
        public IActionResult GetProfile(int memberId)
        {
            return Ok(MemberLink.Provider.GetByMemberId(memberId));
        }

        [HttpGet("profiles")]
        public IActionResult GetProfiles([FromQuery] DateTime refDateTime)
        {
            return Ok(MemberLink.Provider.GetList(refDateTime, WConstants.Active).ToList());
        }

        [HttpPut("{memberId:int}/profile")]
        public IActionResult UpdateProfile(int memberId, [FromBody] MemberProfileModel newProfile)
        {
            if (newProfile != null)
            {
                var link = MemberLink.Provider.GetByMemberId(memberId);
                if (link != null)
                {
                    link.Nickname = newProfile.Nickname;
                    link.Update(true);

                    var user = link.User;
                    if (user != null)
                    {
                        user.MobileNumber = newProfile.MobileNumber;
                        user.TelephoneNumber = newProfile.HomePhone;
                        user.FirstName = newProfile.FirstName;
                        user.LastName = newProfile.LastName;
                        user.MiddleName = newProfile.MiddleName;
                        user.Update();
                    }

                    return Ok(true);
                }
            }

            return Ok(false);
        }

        [HttpGet("session/{authKey}")]
        public IActionResult GetSession(string authKey)
        {
            MemberSession session = null;
            if (!string.IsNullOrEmpty(authKey))
            {
                var guid = new Guid(authKey);
                var userSession = WSession.UserSessions.Sessions.FirstOrDefault(i => i.AuthKey.Equals(guid));
                if (userSession != null)
                {
                    var link = MemberLink.Provider.GetByUserId(userSession.UserId);
                    if (link != null)
                    {
                        userSession.AuthKey = Guid.NewGuid();
                        session = new MemberSession(userSession.AuthKey, link.MemberId, userSession.UserId, userSession.LastBrowserSession.ActivityStartDate);
                    }
                }
            }

            return Ok(session);
        }

        // --- DataSync from Registration/DataSync.svc ---

        [HttpGet("links")]
        public IActionResult GetMemberLinkList()
        {
            var links = MemberLink.Provider.GetList();
            WebUser user = null;
            var items = new List<MemberLinkContainer>();

            items.AddRange(
                from l in links
                select new MemberLinkContainer
                {
                    ItemType = RemoteItemTypes.REMOTE,
                    UserName = (user = l.User) != null ? user.UserName : string.Empty,
                    Link = l
                }
            );

            return Ok(items);
        }

        [HttpGet("link/{userName}")]
        public IActionResult GetMemberLinkComplete(string userName)
        {
            var user = WebUser.Get(userName);
            if (user != null)
            {
                var link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null)
                    return Ok(new MemberLinkContainer(link, RemoteItemTypes.REMOTE));
            }

            return Ok((MemberLinkContainer)null);
        }

        [HttpPost("link")]
        public IActionResult SetMemberLinkComplete([FromBody] MemberLinkContainer container)
        {
            if (container != null && !string.IsNullOrEmpty(container.UserName) && container.Link != null
                && (container.ItemType == RemoteItemTypes.REMOTE || container.ItemType == RemoteItemTypes.IDENTICAL))
            {
                var user = WebUser.Get(container.UserName);
                if (user != null)
                {
                    var item = MemberLink.Provider.GetByUserId(user.Id);
                    if (item == null)
                    {
                        item = container.Link;
                        item.Id = -1;
                        item.UserId = user.Id;
                        item.Update();
                    }
                }
            }

            return Ok();
        }
    }

    public class MemberCheckRequest
    {
        public string AppKey { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IncludeAllAccounts { get; set; }
    }
}
