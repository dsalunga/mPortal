using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WebRole : INameWebObject
    {
        private static IWebRoleProvider _provider;

        static WebRole()
        {
            _provider = DataAccess.CreateProvider<IWebRoleProvider>();
        }

        public WebRole()
        {
            Id = -1;
        }

        [ObjectColumn(true)]
        public int Id { get; set; }

        [ObjectColumn]
        public string Name { get; set; }

        [ObjectColumn]
        public int ParentId { get; set; }


        public WebRole Parent
        {
            get { return WebRole.Get(ParentId); }
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }


        public static WebRole Get(int id)
        {
            if (id > 0)
                return _provider.Get(id);

            return null;
        }

        public static IEnumerable<WebRole> GetList()
        {
            return _provider.GetList();
        }

        public static bool Delete(int id)
        {
            return _provider.Delete(id);
        }

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return WebObjects.WebRole; }
        }

        #endregion
    }
}
