using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Shared
{
    public class CountryState : IWebObject
    {
        private static CountryStateProvider _provider;

        static CountryState()
        {
            _provider = new CountryStateProvider();
        }

        public CountryState()
        {
            StateCode = -1;
            StateName = string.Empty;
            ZipCode = string.Empty;
            CountryCode = -1;
        }

        public int Id
        {
            get { return StateCode; }
            set { StateCode = value; }
        }

        [ObjectColumn(true)]
        public int StateCode { get; set; }

        [ObjectColumn]
        public string StateName { get; set; }

        [ObjectColumn]
        public string ZipCode { get; set; }

        [ObjectColumn]
        public int CountryCode { get; set; }

        public static CountryState Get(int id)
        {
            return _provider.Get(id);
        }

        public static IEnumerable<CountryState> GetList()
        {
            return _provider.GetList();
        }

        public static IEnumerable<CountryState> GetList(int countryCode)
        {
            return _provider.GetList(countryCode);
        }

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return WebObjects.USState; }
        }

        #endregion
    }
}
