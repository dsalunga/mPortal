using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public class WebSubscription : IWebObject, ISelfManager
    {
        private static IWebSubscriptionProvider _provider;

        static WebSubscription()
        {
            _provider = WebObject.ResolveManager<WebSubscription, IWebSubscriptionProvider>(WebObject.ResolveProvider<WebSubscription, IWebSubscriptionProvider>());
        }

        public WebSubscription()
        {
            Id = -1;
            RecordId = -1;
            ObjectId = -1;
            PartId = -1;
            PageId = -1;
            Allow = 1;
        }

        #region Properties

        public static IWebSubscriptionProvider Provider
        {
            get { return _provider; }
        }

        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int PartId { get; set; }
        public int PageId { get; set; }
        public int Allow { get; set; }

        public WPart Part
        {
            get
            {
                if (PartId > 0)
                    return WPart.Get(PartId);
                return null;
            }
        }

        public WPage Page
        {
            get
            {
                if (PageId > 0)
                    return WPage.Get(PageId);

                return null;
            }
        }

        public bool IsAllowed
        {
            get { return Allow == 1; }
        }

        #endregion

        #region IWebObject Members

        public int Id { get; set; }

        public int OBJECT_ID
        {
            get { return WebObjects.WebSubscription; }
        }

        #endregion

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        #endregion

        #region Static Methods

        public static IEnumerable<WebSubscription> GetList(int objectId, int recordId, int partId)
        {
            return _provider.GetList(objectId, recordId, partId, -1, -1);
        }

        #endregion
    }
}
