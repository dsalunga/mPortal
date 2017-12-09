using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Shared
{
    public class Country : IWebObject
    {
        private static CountryProvider _provider;

        static Country()
        {
            _provider = new CountryProvider();

        }

        public Country()
        {
            ISOCode3 = string.Empty;
            ISONumeric = string.Empty;
            ShortName = string.Empty;
        }

        public int Id
        {
            get { return CountryCode; }
            set { CountryCode = value; }
        }

        [ObjectColumn(IsPrimaryKey=true)]
        public int CountryCode { get; set; }

        [ObjectColumn]
        public string CountryName { get; set; }

        [ObjectColumn]
        public int RegionCode { get; set; }

        [ObjectColumn]
        public string Description { get; set; }

        [ObjectColumn]
        public string ISOCode { get; set; }

        [ObjectColumn]
        public int DialingCode { get; set; }

        [ObjectColumn]
        public int MaxPhoneDigit { get; set; }

        public string ShortName { get; set; }
        public string ISOCode3 { get; set; }
        public string ISONumeric { get; set; }

        public static Country Get(int id)
        {
            return _provider.Get(id);
        }

        public static CountryProvider Provider
        {
            get { return _provider; }
        }

        public static IEnumerable<Country> GetList()
        {
            return _provider.GetList();
        }

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return WebObjects.Country; }
        }

        #endregion
    }
}
