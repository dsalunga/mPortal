using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using System.Reflection;

using WCMS.Framework.Core;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Core.XmlProvider
{
    public class WebObjectProvider : IWebObjectProvider
    {
        private string path;

        public WebObjectProvider()
        {
            var xmlPath = ConfigHelper.Get("DbProvider.Path");

            string providerAbsolutePath = WebHelper.IsWeb ? WebHelper.MapPath(xmlPath, true) : xmlPath;
            path = Path.Combine(providerAbsolutePath, "WebObject.xml");
        }

        #region IWebObjectProvider Members

        public WebObject Get(int id)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);

            XmlNode node = xdoc.SelectSingleNode(string.Format("//WebObject[Id={0}]", id));
            if (node != null)
                return From(node);

            return null;
        }

        private static WebObject From(XmlNode node)
        {
            WebObject item = new WebObject();
            item.Id = DataHelper.GetId(XmlUtil.GetNodeText(node, "Id"));
            item.Name = node.SelectSingleNode("Name").InnerText;
            item.IdentityColumn = node.SelectSingleNode("IdentityColumn").InnerText;
            item.ObjectType = node.SelectSingleNode("ObjectType").InnerText;
            item.LastRecordId = DataHelper.GetId(node.SelectSingleNode("LastRecordId").InnerText);
            item.MaxCacheCount = DataHelper.GetInt32(node.SelectSingleNode("MaxCacheCount").InnerText);
            item.AccessTypeId = DataHelper.GetId(node.SelectSingleNode("AccessTypeId").InnerText);
            item.CacheTypeId = DataHelper.GetId(node.SelectSingleNode("CacheTypeId").InnerText);
            item.MaxHistoryCount = DataHelper.GetInt32(node.SelectSingleNode("MaxHistoryCount").InnerText);

            return item;
        }

        public IEnumerable<WebObject> GetList()
        {
            List<WebObject> items = new List<WebObject>();

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);

            XmlNodeList nodes = xdoc.SelectNodes("//WebObject");
            foreach(XmlNode node in nodes)
                items.Add(From(node));

            return items;
        }

        public int Update(WebObject item)
        {
            throw new NotImplementedException();
        }

        public bool Update(List<WebObject> items)
        {
            DataSet ds = new DataSet();
            ds.ReadXmlSchema(path);

            Type type = typeof(WebObject);
            // Get all ObjectColumns
            var props = from prop in type.GetProperties()
                        where prop.GetCustomAttributes(typeof(ObjectColumnAttribute), false).Count() > 0
                        select prop;

            foreach (var item in items)
            {
                DataRow row = ds.Tables[0].NewRow();
                foreach (var prop in props)
                    row[prop.Name] = prop.GetValue(item, null);

                ds.Tables[0].Rows.Add(row);
            }

            ds.WriteXml(path, XmlWriteMode.WriteSchema);
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDataProvider<WebObject> Members


        public WebObject Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebObject> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            //SELECT COUNT(1) FROM WebSite
            return GetList().Count();
        }

        #endregion


        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public WebObject Refresh(WebObject item)
        {
            throw new NotImplementedException();
        }
    }
}
