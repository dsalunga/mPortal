using System;
using System.Collections.Generic;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Core
{
    public class WebRegistryUpdateEventArgs : EventArgs
    {
        public WebRegistry UpdatedNode { get; set; }

        public WebRegistryUpdateEventArgs(WebRegistry updatedNode)
        {
            this.UpdatedNode = updatedNode;
        }
    }

    public class WebRegistry : IWebObject
    {
        public static EventHandler<WebRegistryUpdateEventArgs> Updated;

        public const string WebNamePath = "/System/Name";
        public const string TreeServerModePath = "/System/TreeViewServerMode";
        public const string ShowOrphanPath = "/System/ShowOrphan";

        private static IWebRegistryProvider _manager;

        #region Constructors

        static WebRegistry()
        {
            try
            {
                _manager = WebObject.ResolveManager<WebRegistry, IWebRegistryProvider>(WebObject.ResolveProvider<WebRegistry, IWebRegistryProvider>());
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }

        public WebRegistry()
        {
            ParentId = -1;
            Id = -1;
            StageId = -1;
        }

        public WebRegistry(string regPath)
        {
            int parentId = -1;

            string[] nodeNames = regPath.Split('/');
            for (int i = 0; i < nodeNames.Length - 1; i++)
            {
                string nodeName = nodeNames[i].Trim();
                if (string.IsNullOrEmpty(nodeName)) continue;

                parentId = WebRegistry.Get(nodeName, parentId).Id;
            }

            string lastNode = nodeNames[nodeNames.Length - 1];

            this.ParentId = parentId;
            this.Key = lastNode;
        }

        public WebRegistry(string regPath, string value)
            : this(regPath)
        {
            this.Value = value;
        }

        #endregion

        [ObjectColumn(true)]
        public int Id { get; set; }

        [ObjectColumn]
        public string Key { get; set; }

        [ObjectColumn]
        public string Value { get; set; }

        [ObjectColumn]
        public int ParentId { get; set; }

        [ObjectColumn]
        public int StageId { get; set; }

        public int ValueInt32
        {
            get
            {
                int returnValue = -1;

                int.TryParse(Value, out returnValue);

                return returnValue;
            }
        }

        public DateTime ValueDateTime
        {
            get
            {
                DateTime dateReturn = new DateTime();

                DateTime.TryParse(Value, out dateReturn);
                return dateReturn;
            }
        }

        public bool ValueBool
        {
            get
            {
                if (Value.Equals(bool.TrueString) || Value.Equals(bool.FalseString))
                    return Convert.ToBoolean(Value);

                return Value.Equals("1") || Value.Equals("T") || Value.Equals("Yes") || Value.Equals("true");
            }
        }

        public WebRegistry Parent
        {
            get
            {
                if (ParentId > 0)
                    return WebRegistry.Get(this.ParentId);

                return null;
            }
        }

        public int Update()
        {
            return Update(this);
        }

        public static int Update(WebRegistry item)
        {
            var returnCode = _manager.Update(item);

            // Copy to a temporary variable to be thread-safe.
            EventHandler<WebRegistryUpdateEventArgs> temp = Updated;
            if (temp != null)
                temp(item, new WebRegistryUpdateEventArgs(item));

            return returnCode;
        }

        public WebRegistry SelectSingleNode(string regPath)
        {
            int parentId = this.Id;

            string[] nodeNames = regPath.Split('/');
            for (int i = 0; i < nodeNames.Length - 1; i++)
                parentId = WebRegistry.Get(nodeNames[i], parentId).Id;

            string lastNode = nodeNames[nodeNames.Length - 1];
            return WebRegistry.Get(lastNode, parentId);
        }

        public string SelectSingleNodeValue(string regPath, string defaultValue = null)
        {
            var node = SelectSingleNode(regPath);

            return SelectNodeValue(node);
        }

        public IEnumerable<WebRegistry> ChildNodes()
        {
            return _manager.GetList(this.Id);
        }

        // Static Methods

        public static IEnumerable<WebRegistry> GetList()
        {
            return _manager.GetList();
        }

        public static WebRegistry Get(string key)
        {
            return _manager.Get(key);
        }

        public static WebRegistry Get(int registryId)
        {
            return _manager.Get(registryId);
        }

        public static WebRegistry Get(string key, int parentId)
        {
            return _manager.Get(key, parentId);
        }

        public static IEnumerable<WebRegistry> GetByParentId(int parentId)
        {
            return _manager.GetList(parentId);
        }

        public static WebRegistry SelectNode(string regPath, params object[] args)
        {
            return SelectNode(string.Format(regPath, args));
        }

        public static WebRegistry SelectNode(string regPath)
        {
            int parentId = -1;

            string[] nodeNames = regPath.Split('/');
            for (int i = 0; i < nodeNames.Length - 1; i++)
            {
                string nodeName = nodeNames[i].Trim();
                if (string.IsNullOrEmpty(nodeName)) continue;

                var item = WebRegistry.Get(nodeName, parentId);
                if (item != null)
                    parentId = item.Id;
                else
                    return null;
            }

            string lastNode = nodeNames[nodeNames.Length - 1];
            return WebRegistry.Get(lastNode, parentId);
        }

        public static string SelectNodeValue(string regPath)
        {
            var node = SelectNode(regPath);
            return SelectNodeValue(node);
        }

        public static string SelectNodeValue(WebRegistry node)
        {
            string value = null;

            if (node != null)
                value = node.Value;

            return value;
        }

        public static string SelectNodeValue(string regPath, string defaultValue, params object[] args)
        {
            return SelectNodeValue(string.Format(regPath, args), defaultValue);
        }

        public static string SelectNodeValue(string regPath, string defaultValue)
        {
            var value = SelectNodeValue(regPath);
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            else
                return value;
        }

        public static IEnumerable<WebRegistry> SelectNodes(string regPath)
        {
            int parentId = -1;

            string[] nodeNames = regPath.Split('/');
            for (int i = 0; i < nodeNames.Length; i++)
            {
                string nodeName = nodeNames[i].Trim();
                if (string.IsNullOrEmpty(nodeName)) continue;

                parentId = WebRegistry.Get(nodeName, parentId).Id;
            }

            return WebRegistry.GetByParentId(parentId);
        }

        public static bool Delete(string key)
        {
            return _manager.Delete(key);
        }

        public static bool Delete(int registryId)
        {
            return _manager.Delete(registryId);
        }

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return WebObjects.WebRegistry; }
        }

        #endregion
    }
}
