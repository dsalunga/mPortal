using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;
using WCMS.Framework.Core.Manager;

namespace WCMS.Framework
{
    public class WebPermission : NamedWebObject
    {
        private static IWebPermissionProvider _provider;

        static WebPermission()
        {
            _provider = WebObject.ResolveManager<WebPermission, IWebPermissionProvider>(WebObject.ResolveProvider<WebPermission, IWebPermissionProvider>());
        }

        public WebPermission()
        {
            Id = -1;
        }

        [ObjectColumn]
        public int IsSystem { get; set; }

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public static WebPermission Get(int id)
        {
            return _provider.Get(id);
        }

        public static IEnumerable<WebPermission> GetList()
        {
            return _provider.GetList();
        }

        public static bool Delete(int id)
        {
            return _provider.Delete(id);
        }

        #region IWebObject Members

        public override int OBJECT_ID
        {
            get { return WebObjects.WebPermission; }
        }

        #endregion
    }
}
