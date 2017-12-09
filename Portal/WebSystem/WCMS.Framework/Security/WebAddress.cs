using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;
using WCMS.Framework.Core.Shared;

namespace WCMS.Framework.Core
{
    [Serializable]
    public class WebAddress : WebObjectBase, ISelfManager
    {
        private static IWebAddressProvider _provider;

        static WebAddress()
        {
            _provider = WebObject.ResolveProvider<WebAddress, IWebAddressProvider>();
        }

        public WebAddress()
        {
            ObjectId = -1;
            RecordId = -1;

            CountryCode = -1;
            StateProvinceCode = -1;

            Tag = string.Empty;

            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
            ZipCode = string.Empty;
            CountryName = string.Empty;
            StateProvince = string.Empty;
            CityTown = string.Empty;
            PhoneNumber = string.Empty;

            LastUpdated = DateTime.Now;
        }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CityTown { get; set; }
        public int StateProvinceCode { get; set; }
        public string StateProvince { get; set; }
        public int CountryCode { get; set; }
        public string ZipCode { get; set; }
        public string CountryName { get; set; }
        public string PhoneNumber { get; set; }
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Tag { get; set; }
        public DateTime LastUpdated { get; set; }

        public string StateProvinceString
        {
            get
            {
                if (!string.IsNullOrEmpty(StateProvince))
                    return StateProvince;

                if (StateProvinceCode > 0)
                {
                    var item = CountryState.Get(StateProvinceCode);
                    if (item != null)
                        return item.StateName;
                }

                return string.Empty;
            }
        }

        public string CountryString
        {
            get
            {
                if (!string.IsNullOrEmpty(CountryName))
                    return CountryName;

                if (CountryCode > 0)
                {
                    var item = Country.Get(CountryCode);
                    if (item != null)
                        return item.CountryName;
                }

                return string.Empty;
            }
        }
        
        public override int OBJECT_ID
        {
            get { return WebObjects.WebAddress; }
        }

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public bool Refresh()
        {
            throw new NotImplementedException();
        }

        #endregion

        public static IWebAddressProvider Provider
        {
            get { return _provider; }
        }
    }
}
