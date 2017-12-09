using System.Collections.Generic;

namespace WCMS.Framework.Core
{
    /// <summary>
    /// Represents the permissions applied to a User or Group (Role) in relation to an Object
    /// </summary>
    public class WebObjectSecurityPermission : IWebObject
    {
        private static IWebObjectSecurityPermissionProvider _provider;

        static WebObjectSecurityPermission()
        {
            _provider = WebObject.ResolveManager<WebObjectSecurityPermission, IWebObjectSecurityPermissionProvider>(WebObject.ResolveProvider<WebObjectSecurityPermission, IWebObjectSecurityPermissionProvider>());
        }

        public WebObjectSecurityPermission()
        {
            Id = -1;
            ObjectSecurityId = -1;
            PermissionId = -1;
            Allow = 1;
            Deny = 0;
        }

        [ObjectColumn(IsPrimaryKey=true)]
        public int Id { get; set; }

        [ObjectColumn]
        public int ObjectSecurityId { get; set; }

        [ObjectColumn]
        public int PermissionId { get; set; }

        [ObjectColumn]
        public int Allow { get; set; }

        [ObjectColumn]
        public int Deny { get; set; }

        public bool IsDenied
        {
            get { return Deny == 1; }
        }

        public bool IsAllowed
        {
            get { return Allow == 1; }
        }

        public WebObjectSecurity ObjectSecurity
        {
            get { return WebObjectSecurity.Get(ObjectSecurityId); }
        }

        public WebPermission Permission
        {
            get { return WebPermission.Get(PermissionId); }
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public static WebObjectSecurityPermission Get(int id)
        {
            return _provider.Get(id);
        }

        public static IEnumerable<WebObjectSecurityPermission> GetList(int objectSecurityId)
        {
            return _provider.GetList(objectSecurityId);
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
