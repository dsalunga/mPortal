using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Contact
{
    public class ContactLinkProvider
    {
        private static IDataProvider _provider;

        static ContactLinkProvider()
        {
            _provider = WebObject.ResolveProvider(typeof(ContactLink));
        }

        public ContactLink Get(int id)
        {
            return _provider.Get<ContactLink>(id);
        }

        public ContactLink Get(int objectId, int recordId)
        {
            return _provider.Get<ContactLink>(
                new QueryFilterElement { Name = "ObjectId", NullValue = -1, Value = objectId },
                new QueryFilterElement { Name = "RecordId", NullValue = -1, Value = recordId });
        }

        public IEnumerable<ContactLink> GetList()
        {
            return _provider.GetList<ContactLink>();
        }

        public int Update(ContactLink item)
        {
            return _provider.Update<ContactLink>(item);
        }

        public bool Delete(int id)
        {
            return _provider.Delete<ContactLink>(id);
        }
    }
}
