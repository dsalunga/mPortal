using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Manager;

namespace WCMS.Framework.Core
{
    public class WebObjectCacheFlagUpdateEventArgs : EventArgs
    {
        public int OldCacheFlag { get; set; }
        public WebObject Item { get; set; }

        public WebObjectCacheFlagUpdateEventArgs(int oldCacheFlag, WebObject item)
        {
            this.OldCacheFlag = oldCacheFlag;
            this.Item = item;
        }
    }

    //public class WebObjectRecordEventArgs : EventArgs
    //{
    //    public WebObject Item { get; set; }

    //    public WebObjectRecordEventArgs(WebObject item)
    //    {
    //        this.Item = item;
    //    }
    //}

    public class WebObject : NamedWebObject
    {
        public static EventHandler<WebObjectCacheFlagUpdateEventArgs> CacheFlagUpdated;
        //public static EventHandler<WebObjectRecordEventArgs> RecordCreated;

        private static object _lock = new object();
        private static Type _type;
        private static IWebObjectProvider _manager;

        public static void Initialize()
        {
            if (_manager != null)
            {
                var items = _manager.GetList();
                foreach (var item in items)
                {
                    item.ResolveManager();
                }
            }
        }

        static WebObject()
        {
            _type = typeof(WebObject);

            // Provide caching support in the future
            _xmlProvider = DataAccess.CreateXmlProvider<IWebObjectProvider>();

            lock (_lock)
            {
                try
                {
                    var m = new WebObjectManager(DataAccess.CreateWebObjectProvider());
                    m.LoadCache();
                    _manager = m;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(ex);
                }
            }
        }

        public WebObject()
        {
            Id = -1;

            MaxCacheCount = -1;
            LastRecordId = 0;
            AccessTypeId = -1;
            CacheTypeId = -1;
            MaxHistoryCount = -1;
            CacheInterval = -1;

            ObjectType = "T";
            Prefix = "";
            DataProviderName = "";
            ManagerName = "";
            TypeName = "";
            NameColumn = "";
            FriendlyName = "";
        }

        public WebObject(dynamic dataManager)
            : this()
        {
            _dataManager = dataManager;
        }

        #region Constants

        public const int NULL_ID = -2;

        #endregion

        #region Properties

        [ObjectColumn]
        public string ObjectType { get; set; }

        [ObjectColumn]
        public string IdentityColumn { get; set; }

        [ObjectColumn]
        public int MaxCacheCount { get; set; }

        [ObjectColumn]
        public int LastRecordId { get; set; }

        [ObjectColumn]
        public int AccessTypeId { get; set; }

        [ObjectColumn]
        public int CacheTypeId { get; set; }

        /// <summary>
        /// Number of record versions to retain before considering as "old" and deleting
        /// </summary>
        [ObjectColumn]
        public int MaxHistoryCount { get; set; }

        [ObjectColumn]
        public string Prefix { get; set; }

        [ObjectColumn]
        public string Owner { get; set; }

        [ObjectColumn]
        public string TypeName { get; set; }

        [ObjectColumn]
        public string DataProviderName { get; set; }

        [ObjectColumn]
        public int CacheInterval { get; set; }

        [ObjectColumn]
        public DateTime DateModified { get; set; }

        [ObjectColumn]
        public string ManagerName { get; set; }

        [ObjectColumn]
        public string NameColumn { get; set; }

        [ObjectColumn]
        public string FriendlyName { get; set; }

        public CacheStatus CacheStatus { get { return _dataManager == null ? CacheStatus.Empty : _dataManager.Cache.CacheStatus; } }

        public bool IsFullCache
        {
            get { return (Id == WebObjects.WebRegistry || Id == WebObjects.WebObject || WConfig.AllowCache) && CacheTypeId == CacheTypes.Full; }
        }

        public bool AllowCache
        {
            get { return (Id == WebObjects.WebRegistry || Id == WebObjects.WebObject || WConfig.AllowCache) && (CacheTypeId == CacheTypes.Full || CacheTypeId == CacheTypes.Partial); }
        }

        public static Type Type
        {
            get { return _type; }
        }

        private Type _typeInstance;
        public Type TypeInstance
        {
            get
            {
                if (_typeInstance == null && !string.IsNullOrEmpty(TypeName))
                    _typeInstance = Type.GetType(TypeName);

                return _typeInstance;
            }
        }

        private dynamic _dataProvider;
        private dynamic _dataManager;

        public dynamic DataProvider { get { return _dataProvider; } }
        public dynamic DataManager { get { return _dataManager; } }

        public string FriendlyNameEval
        {
            get { return string.IsNullOrEmpty(FriendlyName) ? Name : FriendlyName; }
        }

        #endregion

        #region Instance Methods

        public IDataProvider<T> GetDataProvider<T>() where T : IWebObject
        {
            if (!string.IsNullOrEmpty(DataProviderName))
                return (IDataProvider<T>)Activator.CreateInstance(Type.GetType(DataProviderName));
            else
                return new GenericSqlDataProvider<T>();
        }

        //public T ResolveDataProvider<T>()
        //{
        //    if (_dataProvider == null)
        //    {
        //        if (!string.IsNullOrEmpty(DataProviderName))
        //            _dataProvider = (IWebObjectProvider)Activator.CreateInstance(Type.GetType(DataProviderName));
        //        //else
        //        //return (T)new SqlDataProvider();
        //    }

        //    return (T)_dataProvider;
        //}

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            return Delete(this.Id);
        }

        public int GetCachedItemCount()
        {
            if (_dataManager != null)
                return _dataManager.GetCachedItemCount();

            return 0;
        }

        #endregion

        #region Static Methods

        public static void LoadCache()
        {
            if (_manager != null)
            {
                var items = _manager.GetList();
                foreach (var item in items)
                {
                    dynamic manager = item.DataManager;
                    if (manager != null)
                        manager.LoadCache();
                }
            }
        }

        public static void UnloadCache()
        {
            var items = WebObject.Provider.GetList();
            foreach (var item in items)
            {
                dynamic manager = item.DataManager;
                if (manager != null)
                    manager.UnloadCache();
            }
        }

        public static IWebObjectProvider Provider { get { return _manager; } }

        public static bool UpdateLastRecord(WebObject item)
        {
            // Get the last recordId
            int lastRecordId = DataHelper.GetId(SqlHelper.ExecuteScalar(CommandType.Text,
                string.Format(DataConstants.SELECT_MAX, item.IdentityColumn, item.Name)));

            // If no record, assign default value of 0
            if (lastRecordId < 1)
                lastRecordId = 0;

            if (lastRecordId != item.LastRecordId)
            {
                item.LastRecordId = lastRecordId;
                item.Update();

                return true;
            }

            return false;
        }

        public static IEnumerable<WebObject> UpdateLastRecords()
        {
            var items = WebObject.GetList();
            var updatedObjects = new List<WebObject>();

            for (int i = 0; i < items.Count(); i++)
            {
                var item = items.ElementAt(i);

                if (UpdateLastRecord(item))
                    updatedObjects.Add(item);

                /*
                // Get the last recordId
                int lastRecordId = DataHelper.GetId(SqlHelper.ExecuteScalar(CommandType.Text,
                    string.Format(DataConstants.SELECT_MAX, item.IdentityColumn, item.Name)));

                // If no record, assign default value of 0
                if (lastRecordId < 1)
                    lastRecordId = 0;

                if (lastRecordId != item.LastRecordId)
                {
                    item.LastRecordId = lastRecordId;
                    item.Update();

                    updatedObjects.Add(item);
                }
                */
            }

            return updatedObjects;
        }

        public static int GetCount(WebObject item)
        {
            int count = DataHelper.GetInt32(SqlHelper.ExecuteScalar(CommandType.Text,
                string.Format(DataConstants.SELECT_COUNT, item.Name)), 0);

            return count;
        }

        public static void OnCacheFlagUpdated(int oldCacheFlag, WebObject item)
        {
            var tmp = CacheFlagUpdated;
            if (tmp != null)
                tmp(item, new WebObjectCacheFlagUpdateEventArgs(oldCacheFlag, item));
        }

        public static void OnRecordCreated(IWebObject record)
        {
            var objectId = record.OBJECT_ID;

            if (objectId > 0)
            {
                var item = _manager.Get(objectId);
                if (item != null)
                    item.LastRecordId = record.Id;
            }
        }

        public static int GetNextRecordId(int id)
        {
            WebObject item = _manager.Get(id);
            if (item != null)
            {
                int nextRecordId = item.LastRecordId + 1;

                item.LastRecordId = nextRecordId == 0 ? 1 : nextRecordId;
                item.Update();

                return nextRecordId;
            }

            //return 1;
            throw new Exception("WebObject was not found using Id: " + id);
        }

        public static int GetNextRecordId(string name)
        {
            WebObject item = WebObject.Get(name);
            if (item != null)
                return GetNextRecordId(item.Id);

            //return 1;
            throw new Exception("WebObject was not found using name: " + name);
        }

        public static bool UpdateCacheToDisk()
        {
            var items = _manager.GetList();
            foreach (var item in items)
                item.Update();

            return true;
        }

        public static INameWebObject GetEntity(int objectId, int recordId)
        {
            switch (objectId)
            {
                case WebObjects.WebSite:
                    return WSite.Get(recordId);

                case WebObjects.WebPage:
                    return WPage.Get(recordId);

                case WebObjects.WebPageElement:
                    return WebPageElement.Get(recordId);

                case WebObjects.WebMasterPage:
                    return WebMasterPage.Get(recordId);

                case WebObjects.WebUser:
                    return WebUser.Get(recordId);

                case WebObjects.WebRole:
                    return WebRole.Get(recordId);

                case WebObjects.WebPartAdmin:
                    return WebPartAdmin.Get(recordId);

                case WebObjects.WebPart:
                    return WPart.Get(recordId);

                case WebObjects.WebGroup:
                    return WebGroup.Get(recordId);

                case WebObjects.WebGlobalPolicy:
                    return WebGlobalPolicy.Provider.Get(recordId);
            }

            return null;
        }

        public static WebObject Get(int id)
        {
            return _manager.Get(id);
        }

        public static WebObject Get<T>()
        {
            return WebObject.Get(typeof(T));
        }

        public static WebObject Get(Type type)
        {
            var items = _manager.GetList();
            WebObject item = items.FirstOrDefault(o => o.TypeInstance != null && o.TypeInstance.FullName == type.FullName);

            if (item == null)
                item = items.FirstOrDefault(o => o.Name == type.Name);

            return item;
        }

        public static WebObject Get(string name)
        {
            var items = _manager.GetList();
            return items.SingleOrDefault(o => o.Name == name);
        }

        public static IEnumerable<WebObject> GetList()
        {
            return _manager.GetList();
        }

        public static bool Delete(int id)
        {
            return _manager.Delete(id);
        }

        public static IDataProvider<T> ResolveProvider<T>() where T : IWebObject
        {
            Type type = typeof(T);

            WebObject item = WebObject.Get(type.Name);
            if (item != null)
                return item.GetDataProvider<T>();

            return new GenericSqlDataProvider<T>();
        }

        public static TP ResolveProvider<T, TP>(int oid = -1) where T : IWebObject
        {
            WebObject item = oid > 0 ? WebObject.Get(oid) : WebObject.Get<T>();
            if (item != null)
            {
                if (item._dataProvider == null)
                {
                    item.ResolveProvider();

                    //if (!string.IsNullOrEmpty(item.DataProviderName))
                    //    item._dataProvider = Activator.CreateInstance(Type.GetType(item.DataProviderName));
                    //else

                    //if (item._dataProvider == null)
                    //    item._dataProvider = new SqlDataProvider<T>();
                }

                return (TP)item._dataProvider;
            }

            return default(TP); //(Tp)(object)(new SqlDataProvider<T>());
        }

        private dynamic ResolveProvider()
        {
            if (!string.IsNullOrEmpty(DataProviderName))
                _dataProvider = Activator.CreateInstance(Type.GetType(DataProviderName));
            //else
            //    _dataProvider = new SqlDataProvider<T>();

            return _dataProvider;
        }

        public dynamic ResolveManager()
        {
            if (!string.IsNullOrEmpty(ManagerName))
            {
                // ManagerName
                if (_dataProvider == null)
                    ResolveProvider();

                _dataManager = Activator.CreateInstance(Type.GetType(ManagerName), _dataProvider); // ManagerName
            }

            return _dataManager;
        }

        public static TM ResolveManager<T, TM>(TM provider, int oid = -1) where T : IWebObject
        {
            WebObject item = oid > 0 ? WebObject.Get(oid) : WebObject.Get<T>();
            if (item != null)
            {
                if (item._dataManager == null)
                {
                    item.ResolveManager();

                    //if (!string.IsNullOrEmpty(item.ManagerName)) // ManagerName
                    //    item._dataManager = Activator.CreateInstance(Type.GetType(item.ManagerName), provider); // ManagerName
                    //else 
                    if (item._dataManager == null && item._dataProvider != null)
                        item._dataManager = item._dataProvider;
                    //else if(!string.IsNullOrEmpty(item.DataProviderName))
                    //    item._dataManager = 
                    //else
                    //    item._dataManager = new SqlDataProvider<T>();
                }

                return (TM)item._dataManager;
            }

            return default(TM);
        }

        public static IDataProvider ResolveProvider(Type type)
        {
            WebObject item = WebObject.Get(type.Name);
            if (item != null)
            {
                if (item._dataProvider == null)
                {
                    if (!string.IsNullOrEmpty(item.DataProviderName))
                        item._dataProvider = (IDataProvider)Activator.CreateInstance(Type.GetType(item.DataProviderName));
                    else
                        item._dataProvider = new GenericSqlDataProvider();
                }

                return (IDataProvider)item._dataProvider;
            }

            return new GenericSqlDataProvider();
        }

        #endregion

        #region Static Properties

        private static IWebObjectProvider _xmlProvider;
        public static IWebObjectProvider XmlProvider
        {
            get { return _xmlProvider; }
        }

        //public static ICacheManager<WebObject> GlobalCache { get; set; }

        #endregion

        #region IWebObject Members

        public override int OBJECT_ID
        {
            get { return WebObjects.WebObject; }
        }

        #endregion
    }
}
