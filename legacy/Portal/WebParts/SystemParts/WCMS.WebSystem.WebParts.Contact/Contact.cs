using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Contact
{
    public class Contact : INameWebObject
    {
        private static ContactProvider _provider;

        static Contact()
        {
            _provider = new ContactProvider();
        }

        public Contact()
        {
            ContactId = -1;
        }

        [ObjectColumn(IsPrimaryKey=true)]
        public int ContactId { get; set; }

        [ObjectColumn]
        public string Name { get; set; }

        [ObjectColumn]
        public string Email { get; set; }

        [ObjectColumn]
        public string Details { get; set; }

        [ObjectColumn]
        public int Rank { get; set; }

        [ObjectColumn]
        public int IsActive { get; set; }

        [ObjectColumn]
        public string Subject { get; set; }

        public int Id
        {
            get
            {
                return ContactId;
            }

            set
            {
                ContactId = value;
            }
        }

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
            return Delete(this.ContactId);
        }


        public static Contact Get(int id)
        {
            return _provider.Get(id);
        }

        public static IEnumerable<Contact> GetList()
        {
            return _provider.GetList();
        }

        public static bool Delete(int id)
        {
            return _provider.Delete(id);
        }
    }
}
