using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Shared
{
    public class CountryStateProvider
    {
        private static IDataManager<CountryState> _manager;

        static CountryStateProvider()
        {
            _manager = new StandardDataManager<CountryState>(new GenericSqlDataProvider<CountryState>());
        }

        public CountryState Get(int id)
        {
            return _manager.Get(id);
        }

        public IEnumerable<CountryState> GetList()
        {
            return _manager.GetList();
        }

        public IEnumerable<CountryState> GetList(int countryCode)
        {
            return _manager.GetList(
                new QueryFilterElement { Name = "CountryCode", Value = countryCode, NullValue = -1 }
            );
        }
    }
}
