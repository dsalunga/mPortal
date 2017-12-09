using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WebShare : WebObjectBase, ISelfManager
    {
        private static IWebShareProvider _provider;

        static WebShare()
        {
            _provider = WebObject.ResolveManager<WebShare, IWebShareProvider>(WebObject.ResolveProvider<WebShare, IWebShareProvider>());
        }

        public override int OBJECT_ID
        {
            get { return WebObjects.WebShare; }
        }

        public WebShare()
        {
            ObjectId = -1;
            RecordId = -1;
            ShareObjectId = -1;
            ShareRecordId = -1;
            Allow = 1;
        }

        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int ShareObjectId { get; set; }
        public int ShareRecordId { get; set; }
        public int Allow { get; set; }

        public static IWebShareProvider Provider { get { return _provider; } }

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(Id);
        }
    }
}
