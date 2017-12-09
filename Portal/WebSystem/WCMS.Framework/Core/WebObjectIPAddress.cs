using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public class WebObjectIPAddress : IWebObject
    {
        private static IWebObjectIPAddressProvider _provider = DataAccess.CreateProvider<IWebObjectIPAddressProvider>();

        public WebObjectIPAddress()
        {
            Id = -1;
            ObjectId = -1;
            RecordId = -1;
        }

        [ObjectColumn(IsPrimaryKey=true)]
        public int Id { get; set; }

        [ObjectColumn]
        public int ObjectId { get; set; }

        [ObjectColumn]
        public int RecordId { get; set; }

        [ObjectColumn]
        public string IPAddress { get; set; }

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }


        public static WebObjectIPAddress Get(int id)
        {
            return _provider.Get(id);
        }

        public static IEnumerable<WebObjectIPAddress> GetList(int objectId, int recordId)
        {
            return _provider.GetList(objectId, recordId);
        }

        public static bool Delete(int id)
        {
            return _provider.Delete(id);
        }

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return -1; }
        }

        #endregion
    }
}
