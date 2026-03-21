using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Contact
{
    public class ContactLink : ISelfManager, IWebObject
    {
        private static ContactLinkProvider _provider;

        static ContactLink()
        {
            _provider = new ContactLinkProvider();
        }

        public ContactLink()
        {
            Id = -1;
            ObjectId = -1;
            RecordId = -1;
            ContactId = -1;
        }

        [ObjectColumn(true)]
        public int Id { get; set; }

        [ObjectColumn]
        public int ObjectId { get; set; }

        [ObjectColumn]
        public int RecordId { get; set; }

        [ObjectColumn]
        public int ContactId { get; set; }

        [ObjectColumn]
        public int Mode { get; set; }

        public int OBJECT_ID
        {
            get
            {
                return WConstants.NULL_ID;
            }
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return Delete(this.Id);
        }

        #region Static Methods

        public static ContactLink Get(int id)
        {
            return _provider.Get(id);
        }

        public static ContactLink Get(int objectId, int recordId)
        {
            return _provider.Get(objectId, recordId);
        }

        public static IEnumerable<ContactLink> GetList()
        {
            return _provider.GetList();
        }

        public static bool Delete(int id)
        {
            return _provider.Delete(id);
        }

        #endregion
    }
}
