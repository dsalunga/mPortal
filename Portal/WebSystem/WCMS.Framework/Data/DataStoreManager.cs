using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Web;

using WCMS.Common.Utilities;

namespace WCMS.Framework.Core
{
    public static class DataStoreManager
    {
        private const string OBJECTS_XPATH = "/des/WebObjects";
        private const string XML_FILE = "WebObjects.xml";
        private const string OBJECT_NAME_NODE = "ObjectName";
        private const string OBJECT_IDENTITY_NODE = "IdentityColumn";

        public static List<DataStoreEntity> GetEntities()
        {
            List<DataStoreEntity> items = new List<DataStoreEntity>();
            string dbPath = ConfigHelper.Get("BackupPath");

            // Map to exact path
            if (dbPath.StartsWith("~"))
            {
                dbPath = HttpContext.Current.Server.MapPath(dbPath);
            }

            string databasePath = string.Format(@"{0}\{1}", dbPath, XML_FILE);

            if (File.Exists(databasePath))
            {
                // Load XML document
                XmlDocument doc = new XmlDocument();
                doc.Load(databasePath);

                // Traverse the XML records of Tables
                XmlNodeList nodes = doc.SelectNodes(OBJECTS_XPATH);
                foreach (XmlNode node in nodes)
                {
                    DataStoreEntity item = new DataStoreEntity();
                    item.Name = node.SelectSingleNode(OBJECT_NAME_NODE).InnerText;
                    item.IdentityColumn = node.SelectSingleNode(OBJECT_IDENTITY_NODE).InnerText;

                    items.Add(item);
                }
            }

            return items;
        }
    }
}
