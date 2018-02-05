using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.Apps.Integration
{
    public partial class ExternalCountry
    {
        private static IEnumerable<ExternalCountry> _cache;

        public static IEnumerable<ExternalCountry> CACHE
        {
            get
            {
                if (_cache == null)
                {
                    var db = new ExternalDBEntities();
                    _cache = db.ExtCountries;
                }

                return _cache;
            }
        }
    }
}
