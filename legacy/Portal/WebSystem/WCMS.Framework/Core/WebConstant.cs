using System.Collections.Generic;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WebConstant : WebObjectBase, ISelfManager
    {
        private static IWebConstantProvider _provider;

        static WebConstant()
        {
            _provider = WebObject.ResolveManager<WebConstant, IWebConstantProvider>(WebObject.ResolveProvider<WebConstant, IWebConstantProvider>());
        }

        public WebConstant()
        {
            ObjectId = -1;

            Rank = 0;
        }

        public static IWebConstantProvider Provider { get { return _provider; } }

        #region Properties

        [ObjectColumn]
        public string Value { get; set; }

        [ObjectColumn]
        public int Rank { get; set; }

        [ObjectColumn]
        public string Category { get; set; }

        [ObjectColumn]
        public string Text { get; set; }

        [ObjectColumn]
        public int ObjectId { get; set; }

        #endregion

        //public static IEnumerable<WebConstant> GetList(string category)
        //{
        //    return _provider.GetList(category);
        //}

        //public static WebConstant Get(int constantId)
        //{
        //    return _provider.Get(constantId);
        //}

        //public static Dictionary<int, WebConstant> GetList()
        //{
        //    return _provider.GetCacheList();
        //}

        public int Update()
        {
            return _provider.Update(this);
        }

        //public static bool Delete(int constantId)
        //{
        //    return _provider.Delete(constantId);
        //}


        //private static Dictionary<int, WebConstant> _objectCache = new Dictionary<int, WebConstant>();
        //public static Dictionary<int, WebConstant> ObjectCache
        //{
        //    get
        //    {
        //        return _objectCache;
        //    }
        //}

        #region IWebObject Members


        public override int OBJECT_ID
        {
            get { return WebObjects.WebConstant; }
        }

        #endregion


        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }
    }
}
