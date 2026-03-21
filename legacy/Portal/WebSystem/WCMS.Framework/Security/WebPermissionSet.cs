using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    /// <summary>
    /// Sets of available permissions per WebObject
    /// Example, for WebPage the following permissions are applicable:
    ///     1. Full Control (Administrator) 
    ///     2. Full Instance Administration (Manager) 
    ///     3. Full Content Contributor (Content Manager) 
    ///     4. Public Content Contributor (Public User/Content Contributor) 
    ///     5. Public Read (Public User / View) 
    /// Permissions 4 & 5 may not be available to WebRegistry or other private objects
    /// </summary>
    [WebObject(Id=50)]
    public class WebPermissionSet : IWebObject
    {
        private static IWebPermissionSetProvider _provider = DataAccess.CreateProvider<IWebPermissionSetProvider>();
        //private IDataManager<WebPermissionSet> _manager;

        public WebPermissionSet()
        {
            //_manager = new StandardDataManager<WebPermissionSet>(new SqlDataProvider<WebPermissionSet>());
            Id = -1;
            ObjectId = -1;
            PermissionId = -1;
            RecordId = -1;
            Public = -1;
        }

        [ObjectColumn(IsPrimaryKey = true, NullValue = -1)]
        public int Id { get; set; }

        [ObjectColumn(NullValue = -1)]
        public int ObjectId { get; set; }

        public int RecordId { get; set; }

        [ObjectColumn(NullValue = -1)]
        public int PermissionId { get; set; }

        [ObjectColumn]
        public int Public { get; set; }

        public WebPermission Permission
        {
            get { return WebPermission.Get(PermissionId); }
        }

        public static IWebPermissionSetProvider Provider
        {
            get { return _provider; }
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public static WebPermissionSet Get(int id)
        {
            return _provider.Get(id);
        }

        public static IEnumerable<WebPermissionSet> GetList()
        {
            return _provider.GetList();
        }

        //public static List<WebPermissionSet> GetList(int objectId)
        //{
        //    return _provider.GetList(objectId);
        //}

        public static IEnumerable<WebPermission> GetPermissions(int objectId, int recordId, int public2)
        {
            var items = from item in _provider.GetList(objectId, recordId, public2)
                        select item.Permission;

            return items;
        }

        public static int Update(WebPermissionSet item)
        {
            return _provider.Update(item);
        }

        public static bool Delete(int id)
        {
            return _provider.Delete(id);
        }

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return WebObjects.WebPermissionSet; }
        }

        #endregion
    }
}
