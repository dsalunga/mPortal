using System.Collections.Generic;
using System.Linq;

namespace WCMS.Framework.Core
{
    public class WebOffice : INameWebObject, ISelfManager
    {
        protected static IDataProvider _provider;

        static WebOffice()
        {
            _provider = WebObject.ResolveProvider(typeof(WebOffice));
        }

        public WebOffice()
        {
            Id = -1;
            ParentId = -1;
            AddressLine1 = "";
            MobileNumber = "";
            PhoneNumber = "";
            EmailAddress = "";
            ContactPerson = "";
        }

        [ObjectColumn]
        public int ParentId { get; set; }

        [ObjectColumn]
        public string AddressLine1 { get; set; }

        [ObjectColumn]
        public string MobileNumber { get; set; }

        [ObjectColumn]
        public string PhoneNumber { get; set; }

        [ObjectColumn]
        public string EmailAddress { get; set; }

        [ObjectColumn]
        public string ContactPerson { get; set; }

        public IEnumerable<WebOffice> Children
        {
            get { return GetList(this.Id); }
        }

        public WebOffice Parent
        {
            get
            {
                if (ParentId > 0)
                    return _provider.Get<WebOffice>(ParentId);

                return null;
            }
        }

        public bool HasChildren
        {
            get { return Children.Count() > 0; }
        }

        public static IDataProvider Provider
        {
            get { return _provider; }
        }

        #region Static Methods

        public static WebOffice Get(int id)
        {
            return _provider.Get<WebOffice>(id);
        }

        public static bool Delete(int id)
        {
            return _provider.Delete<WebOffice>(id);
        }

        public static IEnumerable<WebOffice> GetList()
        {
            return _provider.GetList<WebOffice>();
        }

        public static IEnumerable<WebOffice> GetList(int parentId)
        {
            return _provider.GetList<WebOffice>(
                new QueryFilterElement { Name = "ParentId", Value = parentId });
        }

        #endregion

        #region INameWebObject Members

        [ObjectColumn]
        public string Name { get; set; }

        #endregion

        #region IWebObject Members

        [ObjectColumn(true, "OfficeId")]
        public int Id { get; set; }

        public int OBJECT_ID
        {
            get { return WebObjects.WebOffice; }
        }

        #endregion

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete<WebOffice>(this.Id);
        }

        #endregion
    }
}
