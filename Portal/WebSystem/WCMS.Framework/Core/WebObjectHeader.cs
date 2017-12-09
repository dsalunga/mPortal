using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    public class WebObjectHeader : IWebObject, ISelfManager
    {
        private static IWebObjectHeaderProvider _provider;

        static WebObjectHeader()
        {
            _provider = WebObject.ResolveManager<WebObjectHeader, IWebObjectHeaderProvider>(WebObject.ResolveProvider<WebObjectHeader, IWebObjectHeaderProvider>());
        }

        public WebObjectHeader()
        {
            Id = -1;
            ObjectId = -1;
            RecordId = -1;
        }

        #region Properties

        [ObjectColumn(true)]
        public int Id { get; set; }

        [ObjectColumn]
        public int ObjectId { get; set; }

        [ObjectColumn]
        public int RecordId { get; set; }

        [ObjectColumn]
        public int TextResourceId { get; set; }

        public WebTextResource Header
        {
            get
            {
                if (TextResourceId > 0)
                {
                    return WebTextResource.Get(TextResourceId);
                }

                return null;
            }
        }

        #endregion Properties

        #region Methods

        public static WebObjectHeader Get(int objectHeaderId)
        {
            return _provider.Get(objectHeaderId);
        }

        // This can contain multiple items, so this implementation should be changed
        public static WebObjectHeader Get(int objectId, int recordId, int textResourceId)
        {
            return _provider.Get(objectId, recordId, textResourceId);
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public static bool Delete(int objectHeaderId)
        {
            return _provider.Delete(objectHeaderId);
        }

        public static bool AddHeader(int objectId, int recordId, int textResourceId)
        {
            // Check if Header is already added
            WebObjectHeader item = WebObjectHeader.Get(objectId, recordId, textResourceId);
            if (item == null)
            {
                // Create a new WebObjectHeader
                item = new WebObjectHeader();
                item.ObjectId = objectId;
                item.RecordId = recordId;
                item.TextResourceId = textResourceId;
                item.Update();
            }

            return true;
        }

        public static IEnumerable<WebObjectHeader> GetList(int objectId, int recordId)
        {
            return _provider.GetList(objectId, recordId);
        }

        public static IEnumerable<WebObjectHeader> GetList(int resourceId)
        {
            return _provider.GetList(resourceId);
        }

        #endregion

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return WebObjects.WebObjectHeader; }
        }

        #endregion
    }
}