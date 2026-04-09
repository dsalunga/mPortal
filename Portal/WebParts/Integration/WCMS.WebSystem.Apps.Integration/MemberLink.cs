using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Shared;

using WCMS.WebSystem.Apps.Integration.Providers;
using WCMS.Common.Utilities;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration
{
    [Serializable]
    public class MemberLink : IWebObject, ISelfManager
    {
        private static IMemberLinkProvider _provider;

        static MemberLink()
        {
            //_provider = new MemberLinkSqlProvider();
            _provider = WebObject.ResolveManager<MemberLink, IMemberLinkProvider>(WebObject.ResolveProvider<MemberLink, IMemberLinkProvider>());
        }

        public MemberLink()
        {
            MemberLinkId = -1;
            UserId = -1;
            MemberId = -1;
            LocaleId = -1;
            Private = 0;

            MembershipDate = WConstants.DateTimeMinValue;
            LastUpdate = DateTime.Now;

            #region Obsolete

            HomeAddressLine1 = "";
            HomeAddressLine2 = "";
            HomeAddressZipCode = "";
            //HomeAddressCountryCode = -1;
            HomeAddressStateCode = -1;
            HomePhone = "";
            MobileNumber = "";

            WorkAddressLine1 = "";
            WorkAddressLine2 = "";
            WorkAddressZipCode = "";
            WorkAddressCountryCode = -1;
            WorkAddressStateCode = -1;
            WorkPhone = "";

            #endregion

            Nickname = string.Empty;
            PhotoPath = string.Empty;
            ExternalIdNo = string.Empty;
            WorkDesignation = string.Empty;
            Locale = string.Empty;
            AdditionalInfo = string.Empty;
        }

        #region IWebObject Members

        public int Id
        {
            get { return MemberLinkId; }
            set { MemberLinkId = value; }
        }

        public int OBJECT_ID
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Properties

        public int MemberLinkId { get; set; }

        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
            }
        }

        public int MemberId { get; set; }
        public int LocaleId { get; set; }
        public string ExternalIdNo { get; set; }

        #region Obsolete

        public string HomeAddressLine1 { get; set; }
        public string HomeAddressLine2 { get; set; }
        public int HomeAddressStateCode { get; set; }

        private int? _homeCountryCode = null;
        public int LocaleCountryCode
        {
            get
            {
                if (_homeCountryCode == null)
                {
                    var user = User;
                    if (user != null)
                    {
                        var address = user.GetAddress(AddressTags.Home);
                        if (address != null)
                            _homeCountryCode = address.CountryCode;
                    }

                    if (_homeCountryCode == null)
                        _homeCountryCode = -1;
                }

                return _homeCountryCode.Value;
            }

            set
            {
                _homeCountryCode = value;
            }
        }

        public string HomeAddressZipCode { get; set; }
        public string MobileNumber { get; set; }
        public string HomePhone { get; set; }

        public string WorkAddressLine1 { get; set; }
        public string WorkAddressLine2 { get; set; }
        public int WorkAddressStateCode { get; set; }
        public int WorkAddressCountryCode { get; set; }
        public string WorkAddressZipCode { get; set; }

        public string WorkPhone { get; set; }

        #endregion

        public string WorkDesignation { get; set; }
        public string Nickname { get; set; }

        public DateTime LastUpdate { get; set; }
        public DateTime MembershipDate { get; set; }

        public int Approved { get; set; }
        public int Private { get; set; }
        public string Locale { get; set; }

        public string AdditionalInfo { get; set; }

        public bool IsApproved
        {
            get { return Approved == 1; }
            set { Approved = value ? 1 : 0; }
        }

        public bool IsPrivate
        {
            get { return Private == 1; }
            set { Private = value ? 1 : 0; }
        }

        public static IMemberLinkProvider Provider
        {
            get { return _provider; }
        }

        public Country LocaleCountry
        {
            get
            {
                if (LocaleCountryCode > 0)
                    return Country.Get(LocaleCountryCode);

                return null;
            }
        }

        public Country WorkAddressCountry
        {
            get { return WorkAddressCountryCode > 0 ? Country.Get(WorkAddressCountryCode) : null; }
        }

        public CountryState HomeAddressState
        {
            get { return HomeAddressStateCode > 0 ? CountryState.Get(HomeAddressStateCode) : null; }
        }

        public CountryState WorkAddressState
        {
            get { return WorkAddressStateCode > 0 ? CountryState.Get(WorkAddressStateCode) : null; }
        }


        public WebUser User
        {
            get
            {
                if (UserId > 0)
                    return WebUser.Get(UserId);
                return null;
            }
        }


        public string SingleLineHomeAddress
        {
            get
            {
                var sb = new StringBuilder();

                var user = this.User;
                if (user != null)
                {
                    var address = user.GetAddress(AddressTags.Home);
                    if (address != null)
                    {
                        if (!string.IsNullOrEmpty(address.AddressLine1))
                            sb.AppendFormat(" {0}", address.AddressLine1);

                        if (!string.IsNullOrEmpty(address.AddressLine2))
                            sb.AppendFormat(" {0}", address.AddressLine2);

                        if (!string.IsNullOrEmpty(address.CityTown))
                            sb.AppendFormat(" {0}", address.CityTown);

                        var provinceOrState = address.StateProvinceString;
                        if (!string.IsNullOrEmpty(provinceOrState))
                            sb.AppendFormat(" {0}", provinceOrState);

                        var countryName = address.CountryString;
                        if (!string.IsNullOrEmpty(countryName))
                            sb.AppendFormat(" {0}", countryName);

                        if (!string.IsNullOrEmpty(address.ZipCode))
                            sb.AppendFormat(" {0}", address.ZipCode);
                    }
                }

                return sb.ToString().Trim();
            }
        }

        public string ContactNoEval
        {
            get
            {
                var user = this.User;
                if (user != null)
                    return string.IsNullOrEmpty(user.MobileNumber) ? user.TelephoneNumber : user.MobileNumber;

                return string.Empty;
            }
        }


        #endregion

        /// <summary>
        /// Updates only the fields but does not commit the update to datastore
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>

        private string _photoPath;
        /// <summary>
        /// Absolute path of Member's photo (Accessible to public)
        /// </summary>
        public string GetPhotoPathIfNull(string size = null)
        {
            var user = User;
            if (string.IsNullOrEmpty(_photoPath) || user != null)
            {
                var photoPath = user.GetPhotoPath(size); //user.PhotoPath : user.PhotoThumbPath;
                if (!string.IsNullOrEmpty(photoPath))
                {
                    _photoPath = photoPath;
                    return _photoPath;
                }
            }

            if (string.IsNullOrEmpty(_photoPath) && MemberId > 0)
            {
            }

            if (string.IsNullOrEmpty(_photoPath))
                return WConstants.NoPhotoThumb;
            else if (!string.IsNullOrEmpty(size) && _photoPath.Contains("/brethren/photos/"))
                return _photoPath.Replace("/brethren/photos/", string.Format("/brethren/photos/{0}/", size));
            else
                return _photoPath;
        }

        public string PhotoPath
        {
            get { return _photoPath; }
            set { _photoPath = value; }
        }

        /// <summary>
        /// Ex: Mr. Synthetic8ff324, Ms. Synthetic5844a1
        /// </summary>
        public string GetShortName
        {
            get { return User.FirstName; }
        }

        #region ISelfManager Members

        public int Update(bool updateLastUpdate)
        {
            if (updateLastUpdate)
                this.LastUpdate = DateTime.Now;

            return Update();
        }

        public int Update()
        {
            // Update user's photo
            //var user = this.User;
            //if (user != null && !string.IsNullOrEmpty(this.PhotoPath) && !user.PhotoPath.Equals(this.PhotoPath, StringComparison.InvariantCultureIgnoreCase))
            //{
            //    user.PhotoPath = this.PhotoPath;
            //    user.Update();
            //}

            return _provider.Update(this);
        }

        /// <summary>
        /// Deletes only this obj and does not include deleting WUser
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            return _provider.Delete(this.MemberLinkId);
        }

        #endregion


        public static WebAddress WebAddressFromMemberLink(MemberLink link, string tag = "", WebAddress eAddress = null)
        {
            var address = eAddress;
            if (eAddress == null)
            {
                address = new WebAddress();
                address.ObjectId = WebObjects.WebUser;
                address.RecordId = link.UserId;
            }

            address.LastUpdated = link.LastUpdate;

            if (string.IsNullOrEmpty(tag) || !tag.Equals(AddressTags.Work, StringComparison.InvariantCultureIgnoreCase))
            {
                // Home Address

                if (string.IsNullOrEmpty(address.AddressLine1))
                    address.AddressLine1 = link.HomeAddressLine1;

                if (string.IsNullOrEmpty(address.AddressLine2))
                    address.AddressLine2 = link.HomeAddressLine2;

                address.CityTown = string.Empty;

                if (string.IsNullOrEmpty(address.ZipCode))
                    address.ZipCode = link.HomeAddressZipCode;

                if (address.StateProvinceCode == -1)
                    address.StateProvinceCode = link.HomeAddressStateCode;

                if (address.CountryCode == -1)
                    address.CountryCode = link.LocaleCountryCode;

                var state = link.HomeAddressState;
                var country = link.LocaleCountry;

                address.StateProvince = state == null ? string.Empty : state.StateName;
                address.CountryName = country == null ? string.Empty : country.CountryName;

                if (string.IsNullOrEmpty(address.PhoneNumber))
                    address.PhoneNumber = link.HomePhone;

                address.Tag = AddressTags.Home;
            }
            else
            {
                // Work Address

                if (string.IsNullOrEmpty(address.AddressLine1))
                    address.AddressLine1 = link.WorkAddressLine1;

                if (string.IsNullOrEmpty(address.AddressLine2))
                    address.AddressLine2 = link.WorkAddressLine2;

                address.CityTown = string.Empty;

                if (string.IsNullOrEmpty(address.ZipCode))
                    address.ZipCode = link.WorkAddressZipCode;

                if (address.StateProvinceCode == -1)
                    address.StateProvinceCode = link.WorkAddressStateCode;

                if (address.CountryCode == -1)
                    address.CountryCode = link.WorkAddressCountryCode;

                var state = link.WorkAddressState;
                var country = link.WorkAddressCountry;

                address.StateProvince = state == null ? string.Empty : state.StateName;
                address.CountryName = country == null ? string.Empty : country.CountryName;

                if (string.IsNullOrEmpty(address.PhoneNumber))
                    address.PhoneNumber = link.WorkPhone;

                address.Tag = AddressTags.Work;
            }

            return address;
        }
    }
}
