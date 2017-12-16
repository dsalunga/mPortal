using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Contact
{
    public class ContactInquiry : IWebObject
    {
        private static ContactInquiryProvider _provider;

        static ContactInquiry()
        {
            _provider = new ContactInquiryProvider();
        }

        public ContactInquiry()
        {
            InquiryId = -1;
            IsActive = 0;
            UserId = -1;
            ObjectId = -1;
            RecordId = -1;
        }

        [ObjectColumn(true)]
        public int InquiryId { get; set; }

        [ObjectColumn]
        public string SenderName { get; set; }

        [ObjectColumn]
        public string Subject { get; set; }

        [ObjectColumn]
        public string Message { get; set; }

        [ObjectColumn]
        public string Email { get; set; }

        [ObjectColumn]
        public string Address1 { get; set; }

        [ObjectColumn]
        public string Address2 { get; set; }

        [ObjectColumn]
        public string City { get; set; }

        [ObjectColumn]
        public int CountryCode { get; set; }

        [ObjectColumn]
        public int StateCode { get; set; }

        [ObjectColumn]
        public string ZipCode { get; set; }

        [ObjectColumn]
        public string Phone { get; set; }

        [ObjectColumn]
        public string Fax { get; set; }

        [ObjectColumn]
        public string SendTo { get; set; }

        [ObjectColumn]
        public DateTime InqDateTime { get; set; }

        [ObjectColumn]
        public int IsActive { get; set; }

        [ObjectColumn]
        public string SendToEmail { get; set; }

        [ObjectColumn]
        public string InquiryType { get; set; }

        [ObjectColumn]
        public int ObjectId { get; set; }

        [ObjectColumn]
        public int RecordId { get; set; }

        [ObjectColumn]
        public int UserId { get; set; }

        public int Id
        {
            get
            {
                return InquiryId;
            }

            set
            {
                InquiryId = value;
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
            return Delete(this.InquiryId);
        }


        public static ContactInquiry Get(int id)
        {
            return _provider.Get(id);
        }

        public static IEnumerable<ContactInquiry> GetList()
        {
            return _provider.GetList();
        }

        public static IEnumerable<ContactInquiry> GetList(int objectId, int recordId)
        {
            return _provider.GetList(objectId, recordId);
        }

        public static bool Delete(int id)
        {
            return _provider.Delete(id);
        }
    }
}
