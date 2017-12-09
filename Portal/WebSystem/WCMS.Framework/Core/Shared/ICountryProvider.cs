using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Shared
{
    interface ICountryProvider : IDataProvider<Country>
    {
        Country Get(string countryName);
        Country GetByISOCode(string isoCode);
    }
}
