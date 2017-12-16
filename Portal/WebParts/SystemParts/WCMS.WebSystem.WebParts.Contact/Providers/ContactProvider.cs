using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Contact
{
    public class ContactProvider
    {
        private static IDataProvider _provider;

        static ContactProvider()
        {
            _provider = WebObject.ResolveProvider(typeof(Contact));
        }

        public Contact Get(int id)
        {
            return _provider.Get<Contact>(id);
        }

        public IEnumerable<Contact> GetList()
        {
            return _provider.GetList<Contact>();
        }

        public int Update(Contact item)
        {
            return _provider.Update<Contact>(item);
        }

        public bool Delete(int id)
        {
            return _provider.Delete<Contact>(id);
        }
    }
}
