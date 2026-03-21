using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Shared;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    /// <summary>
    /// Summary description for Member
    /// </summary>
    [WebService(Namespace = "http://someorg.org/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MemberService : System.Web.Services.WebService
    {
        [WebMethod]
        public bool UpdatePrimaryAddress(int memberId, WebAddress address)
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

                        // StateCode
                        //link.HomeAddressStateCode = address.StateCode;
                        var states = CountryState.GetList();
                        var state = states.FirstOrDefault(i => i.StateName.Equals(address.StateProvince, StringComparison.InvariantCultureIgnoreCase));
                        if (state != null)
                            homeAddress.StateProvinceCode = state.StateCode;
                        else
                            homeAddress.StateProvince = address.StateProvince;

                        // CountryCode
                        //link.HomeAddressCountryCode = address.CountryCode;
                        var countries = Country.GetList();
                        var country = countries.FirstOrDefault(i => i.CountryName.Equals(address.CountryName, StringComparison.InvariantCultureIgnoreCase));
                        if (country != null)
                            homeAddress.CountryCode = country.CountryCode;
                        else
                            homeAddress.CountryName = address.CountryName;

                        homeAddress.Update();

                        return true;
                    }
                }
            }

            return false;
        }

        [WebMethod]
        public WebAddress GetPrimaryAddress(int memberId)
        {
            var link = MemberLink.Provider.GetByMemberId(memberId);
            if (link != null)
            {
                var user = link.User;
                if (user != null)
                    return user.GetAddress(AddressTags.Home);
            }

            return null;
        }

        [WebMethod]
        public MemberLink GetProfile(int memberId)
        {
            return MemberLink.Provider.GetByMemberId(memberId);
        }

        [WebMethod]
        public List<MemberLink> GetProfiles(DateTime refDateTime)
        {
            return MemberLink.Provider.GetList(refDateTime, WConstants.Active).ToList();
        }

        [WebMethod]
        public bool UpdateProfile(int memberId, MemberProfileModel newProfile)
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

                    return true;
                }
            }

            return false;
        }

        [WebMethod]
        public MemberSession GetSession(string authKey)
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

            return session;
        }
    }
}
