using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.Apps.Integration
{
    public partial class AMSCountry
    {
        private static IEnumerable<AMSCountry> _cache;

        public static IEnumerable<AMSCountry> CACHE
        {
            get
            {
                if (_cache == null)
                {
                    var db = new ExternalDBEntities();
                    _cache = db.AMSCountries;
                }

                return _cache;
            }
        }
    }
}
