using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Content.Providers;

namespace WCMS.WebSystem.WebParts.Content
{
    public class WebObjectContent : IWebObject, ISelfManager
    {
        private static IWebObjectContentProvider _manager;

        static WebObjectContent()
        {
            _manager = WebObject.ResolveManager<WebObjectContent, IWebObjectContentProvider>(WebObject.ResolveProvider<WebObjectContent, IWebObjectContentProvider>());
        }

        public WebObjectContent()
        {
            ObjectId = -1;
            ContentId = -1;
            RecordId = -1;
            Id = -1;
        }

        [ObjectColumn(true)]
        public int Id { get; set; }

        [ObjectColumn]
        public int RecordId { get; set; }

        [ObjectColumn]
        public int ContentId { get; set; }

        [ObjectColumn]
        public int ObjectId { get; set; }
      

        public WebObject Object
        {
            get { return WebObject.Get(ObjectId); }
        }


        public WebContent Content
        {
            get { return WebContent.Get(ContentId); }
        }


        public int Update()
        {
            return _manager.Update(this);
        }


        public static WebObjectContent Get(int objectContentId)
        {
            return _manager.Get(objectContentId);
        }

        public static WebObjectContent GetByObjectId(int objectId, int recordId)
        {
            return _manager.GetByObjectId(objectId, recordId);
        }

        public static WebObjectContent Get(IWebObject item)
        {
            return _manager.GetByObjectId(item.OBJECT_ID, item.Id);
        }

        public static IEnumerable<WebObjectContent> GetList(int objectId)
        {
            return _manager.GetList(objectId);
        }

        public static bool Delete(int objectContentId)
        {
            return _manager.Delete(objectContentId);
        }

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return WebObjects.WebObjectContent; }
        }

        #endregion

        public bool Delete()
        {
            return Delete(this.Id);
        }
    }
}
