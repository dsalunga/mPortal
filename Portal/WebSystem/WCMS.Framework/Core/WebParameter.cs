using System;
using System.Collections.Generic;
using System.Linq;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Core
{
    public class WebParameter : NamedWebObject
    {
        private static IWebParameterProvider _manager;

        static WebParameter()
        {
            _manager = WebObject.ResolveManager<WebParameter, IWebParameterProvider>(WebObject.ResolveProvider<WebParameter, IWebParameterProvider>());
        }

        public WebParameter()
        {
            ObjectId = -1;
            RecordId = -1;
            IsRequired = 0;

            Value = string.Empty;
        }

        [ObjectColumn]
        public int ObjectId { get; set; }

        [ObjectColumn]
        public int RecordId { get; set; }

        [ObjectColumn]
        public string Value { get; set; }

        [ObjectColumn]
        public int IsRequired { get; set; }

        public int ValueInt32
        {
            get { return DataHelper.GetInt32(Value); }
            //set { Value = value.ToString(); }
        }

        private string GetValue(string nullEmptyDefaultValue)
        {
            return string.IsNullOrEmpty(Value) ? nullEmptyDefaultValue : Value;
        }

        #region IDataProvider<WebParameter> Members

        public bool Delete()
        {
            return _manager.Delete(Id);
        }

        public int GetCount()
        {
            return _manager.GetCount();
        }

        public int Update()
        {
            return _manager.Update(this);
        }


        public static WebParameter Get(int id)
        {
            return _manager.Get(id);
        }

        //public static WebParameter Get(params QueryFilterElement[] filters)
        //{
        //    return _manager.Get(filters);
        //}

        public static IEnumerable<WebParameter> GetList(int objectId, int recordId)
        {
            return _manager.GetList(objectId, recordId);
        }

        public static WebParameter Get(int objectId, int recordId, string name)
        {
            return _manager.Get(objectId, recordId, name);
        }

        public static string GetStringValue(int objectId, int recordId, string name)
        {
            var item = Get(objectId, recordId, name);
            if (item != null)
                return item.GetValue(null);

            return null;
        }

        public static IEnumerable<WebParameter> GetList()
        {
            return _manager.GetList();
        }

        //public static List<WebParameter> GetList(params QueryFilterElement[] filters)
        //{
        //    return _manager.GetList(filters);
        //}

        public static bool Delete(int id)
        {
            return _manager.Delete(id);
        }

        #endregion

        #region IWebObject Members

        public override int OBJECT_ID
        {
            get { return WebObjects.WebParameter; }
        }

        #endregion

        public static string GetValue(IEnumerable<WebParameter> items, string key)
        {
            var parm = items.FirstOrDefault(i => i.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (parm != null)
                return parm.Value;

            return null;
        }

        public static string GetValue(IEnumerable<WebParameter> items, string key, string nullEmptyDefaultValue)
        {
            var value = GetValue(items, key);
            return string.IsNullOrEmpty(value) ? nullEmptyDefaultValue : value;
        }
    }
}
