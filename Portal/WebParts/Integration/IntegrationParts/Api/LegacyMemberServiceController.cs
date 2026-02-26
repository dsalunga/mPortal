using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Shared;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for Apps/Integration/Profile/MemberService.asmx.
    /// Returns ASP.NET AJAX style payloads: { d: ... }.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyMemberServiceController : ControllerBase
    {
        [HttpPost("/Apps/Integration/Profile/MemberService.asmx/UpdatePrimaryAddress")]
        [HttpPost("/Content/Parts/Integration/Profile/MemberService.asmx/UpdatePrimaryAddress")]
        public IActionResult UpdatePrimaryAddress([FromQuery] int memberId, [FromBody] UpdatePrimaryAddressRequest request)
        {
            var id = memberId > 0 ? memberId : request?.MemberId ?? 0;
            var address = request?.Address;
            var result = UpdatePrimaryAddressCore(id, address);
            return new JsonResult(new { d = result });
        }

        [HttpGet("/Apps/Integration/Profile/MemberService.asmx/GetPrimaryAddress")]
        [HttpPost("/Apps/Integration/Profile/MemberService.asmx/GetPrimaryAddress")]
        [HttpGet("/Content/Parts/Integration/Profile/MemberService.asmx/GetPrimaryAddress")]
        [HttpPost("/Content/Parts/Integration/Profile/MemberService.asmx/GetPrimaryAddress")]
        public IActionResult GetPrimaryAddress([FromQuery] int memberId, [FromBody] MemberIdRequest request)
        {
            var id = memberId > 0 ? memberId : request?.MemberId ?? 0;
            var result = GetPrimaryAddressCore(id);
            return new JsonResult(new { d = result });
        }

        [HttpGet("/Apps/Integration/Profile/MemberService.asmx/GetProfile")]
        [HttpPost("/Apps/Integration/Profile/MemberService.asmx/GetProfile")]
        [HttpGet("/Content/Parts/Integration/Profile/MemberService.asmx/GetProfile")]
        [HttpPost("/Content/Parts/Integration/Profile/MemberService.asmx/GetProfile")]
        public IActionResult GetProfile([FromQuery] int memberId, [FromBody] MemberIdRequest request)
        {
            var id = memberId > 0 ? memberId : request?.MemberId ?? 0;
            return new JsonResult(new { d = MemberLink.Provider.GetByMemberId(id) });
        }

        [HttpGet("/Apps/Integration/Profile/MemberService.asmx/GetProfiles")]
        [HttpPost("/Apps/Integration/Profile/MemberService.asmx/GetProfiles")]
        [HttpGet("/Content/Parts/Integration/Profile/MemberService.asmx/GetProfiles")]
        [HttpPost("/Content/Parts/Integration/Profile/MemberService.asmx/GetProfiles")]
        public IActionResult GetProfiles([FromQuery] DateTime refDateTime, [FromBody] RefDateTimeRequest request)
        {
            var effectiveRefDateTime = refDateTime != default ? refDateTime : request?.RefDateTime ?? DateTime.MinValue;
            var result = MemberLink.Provider.GetList(effectiveRefDateTime, WConstants.Active).ToList();
            return new JsonResult(new { d = result });
        }

        [HttpPost("/Apps/Integration/Profile/MemberService.asmx/UpdateProfile")]
        [HttpPost("/Content/Parts/Integration/Profile/MemberService.asmx/UpdateProfile")]
        public IActionResult UpdateProfile([FromQuery] int memberId, [FromBody] UpdateProfileRequest request)
        {
            var id = memberId > 0 ? memberId : request?.MemberId ?? 0;
            var profile = request?.NewProfile;
            var result = UpdateProfileCore(id, profile);
            return new JsonResult(new { d = result });
        }

        [HttpGet("/Apps/Integration/Profile/MemberService.asmx/GetSession")]
        [HttpPost("/Apps/Integration/Profile/MemberService.asmx/GetSession")]
        [HttpGet("/Content/Parts/Integration/Profile/MemberService.asmx/GetSession")]
        [HttpPost("/Content/Parts/Integration/Profile/MemberService.asmx/GetSession")]
        public IActionResult GetSession([FromQuery] string authKey, [FromBody] SessionAuthRequest request)
        {
            var key = !string.IsNullOrEmpty(authKey) ? authKey : request?.AuthKey;
            MemberSession session = null;

            if (Guid.TryParse(key, out var guid))
            {
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

            return new JsonResult(new { d = session });
        }

        private static bool UpdatePrimaryAddressCore(int memberId, WebAddress address)
        {
            if (address == null)
                return false;

            var link = MemberLink.Provider.GetByMemberId(memberId);
            if (link == null || address.LastUpdated <= link.LastUpdate)
                return false;

            var user = link.User;
            if (user == null)
                return false;

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

            var state = CountryState.GetList().FirstOrDefault(i =>
                i.StateName.Equals(address.StateProvince, StringComparison.InvariantCultureIgnoreCase));
            if (state != null)
                homeAddress.StateProvinceCode = state.StateCode;
            else
                homeAddress.StateProvince = address.StateProvince;

            var country = Country.GetList().FirstOrDefault(i =>
                i.CountryName.Equals(address.CountryName, StringComparison.InvariantCultureIgnoreCase));
            if (country != null)
                homeAddress.CountryCode = country.CountryCode;
            else
                homeAddress.CountryName = address.CountryName;

            homeAddress.Update();
            return true;
        }

        private static WebAddress GetPrimaryAddressCore(int memberId)
        {
            var link = MemberLink.Provider.GetByMemberId(memberId);
            var user = link?.User;
            return user?.GetAddress(AddressTags.Home);
        }

        private static bool UpdateProfileCore(int memberId, MemberProfileModel newProfile)
        {
            if (newProfile == null)
                return false;

            var link = MemberLink.Provider.GetByMemberId(memberId);
            if (link == null)
                return false;

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

            return true;
        }
    }

    public class MemberIdRequest
    {
        public int MemberId { get; set; }
    }

    public class UpdatePrimaryAddressRequest
    {
        public int MemberId { get; set; }
        public WebAddress Address { get; set; }
    }

    public class RefDateTimeRequest
    {
        public DateTime RefDateTime { get; set; }
    }

    public class UpdateProfileRequest
    {
        public int MemberId { get; set; }
        public MemberProfileModel NewProfile { get; set; }
    }

    public class SessionAuthRequest
    {
        public string AuthKey { get; set; }
    }
}
