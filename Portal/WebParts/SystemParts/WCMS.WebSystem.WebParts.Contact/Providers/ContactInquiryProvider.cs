using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Contact
{
    public class ContactInquiryProvider
    {
        private static IDataProvider _provider;

        static ContactInquiryProvider()
        {
            _provider = WebObject.ResolveProvider(typeof(ContactInquiry));
        }

        public ContactInquiry Get(int id)
        {
            return _provider.Get<ContactInquiry>(id);
        }

        public IEnumerable<ContactInquiry> GetList()
        {
            return _provider.GetList<ContactInquiry>()
                .OrderByDescending(i => i.InqDateTime)
                .ToList();
        }

        public IEnumerable<ContactInquiry> GetList(int objectId, int recordId)
        {
            return _provider.GetList<ContactInquiry>(
                new QueryFilterElement { Name = WebColumns.ObjectId, Value = objectId, NullValue = -1 },
                new QueryFilterElement { Name = WebColumns.RecordId, Value = recordId, NullValue = -1 })
                .OrderByDescending(i => i.InqDateTime);
        }

        public int Update(ContactInquiry item)
        {
            return _provider.Update<ContactInquiry>(item);
        }

        public bool Delete(int id)
        {
            return _provider.Delete<ContactInquiry>(id);
        }
    }
}
