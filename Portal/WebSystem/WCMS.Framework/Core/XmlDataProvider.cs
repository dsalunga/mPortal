using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;

using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public class XmlDataProvider<T> : IDataProvider<T> where T: IWebObject
    {
        private string path;
        private string xmlPath;

        public XmlDataProvider()
        {
            xmlPath = WebHelper.MapPath(ConfigHelper.Get("DbProvider.Path"));
        }

        private XmlDocument LoadXmlDocument(string name)
        {
            path = Path.Combine(xmlPath, name + ".xml");

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);

            return xdoc;
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public T Refresh(T item)
        {
            return Get(item.Id, item);
        }

        public T Get(int id)
        {
            return Get(id, default(T));
        }

        public T Get(int id, T source)
        {
            Type itemType = typeof(T);
            StringBuilder query = new StringBuilder();

            // Get all ObjectColumns
            var props = from prop in itemType.GetProperties()
                        where prop.GetCustomAttributes(typeof(ObjectColumnAttribute), false).Count() > 0
                        select prop;

            // Get PrimaryKey column
            int count = props.Count();
            var pk = props.Single(prop => ((ObjectColumnAttribute)prop.GetCustomAttributes(false).First()).IsPrimaryKey);

            XmlDocument xdoc = LoadXmlDocument(itemType.Name);

            XmlNode node = xdoc.SelectSingleNode(string.Format("/des/{0}[{1}={2}]", itemType.Name, pk.Name, id));
            if (node != null)
            {
                T item = source == null ? Activator.CreateInstance<T>() : source;

                for (int i = 0; i < count; i++)
                {
                    PropertyInfo prop = props.ElementAt(i);
                    string value = XmlUtil.GetNodeText(node, prop.Name);

                    if (prop == pk)
                        prop.SetValue(item, DataHelper.GetId(value), null);
                    else if (prop.PropertyType == typeof(DateTime))
                        prop.SetValue(item, DataHelper.GetDateTime(value), null);
                    else if (prop.PropertyType == typeof(int))
                        prop.SetValue(item, DataHelper.GetInt32(value), null);
                    else
                        prop.SetValue(item, value.ToString(), null);
                }

                return item;
            }

            return default(T);
        }

        public T Get(params QueryFilterElement[] filters)
        {
            Type t = typeof(T);
            StringBuilder query = new StringBuilder();

            // Get all ObjectColumns
            var props = from prop in t.GetProperties()
                        where prop.GetCustomAttributes(typeof(ObjectColumnAttribute), false).Count() > 0
                        select prop;

            // Get PrimaryKey column
            int count = props.Count();
            var pk = props.Single(prop => ((ObjectColumnAttribute)prop.GetCustomAttributes(false)[0]).IsPrimaryKey);

            query.Append("SELECT ");

            for (int i = 0; i < count - 1; i++)
            {
                query.AppendFormat("[{0}], ", props.ElementAt(i).Name);
            }

            query.Append(props.ElementAt(count - 1).Name);
            query.AppendFormat(" FROM {0} WHERE ", t.Name); //{1}=@{1}", t.Name, pk.Name, id);

            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < filters.Length; i++)
            {
                QueryFilterElement filter = filters[i];

                sqlParams.Add(new SqlParameter("@" + filter.Name, filter.Value));
                query.AppendFormat("[{0}]=@{0}", filter.Name);

                if (i < filters.Length - 1)
                    query.Append(" AND ");
            }

            using (DbDataReader r = SqlHelper.ExecuteReader(CommandType.Text, query.ToString(),
                sqlParams.ToArray()
            ))
            {
                if (r.Read())
                {
                    T item = Activator.CreateInstance<T>();

                    for (int i = 0; i < count; i++)
                    {
                        PropertyInfo prop = props.ElementAt(i);
                        object value = r[prop.Name];

                        if (prop == pk)
                            prop.SetValue(item, DataHelper.GetId(value), null);
                        else if (prop.PropertyType == typeof(DateTime))
                            prop.SetValue(item, DataHelper.GetDateTime(value), null);
                        else if (prop.PropertyType == typeof(int))
                            prop.SetValue(item, DataHelper.GetInt32(value), null);
                        else
                            prop.SetValue(item, value.ToString(), null);
                    }

                    return item;
                }
            }

            return default(T);
        }

        public IEnumerable<T> GetList(params QueryFilterElement[] filters)
        {
            Type itemType = typeof(T);
            StringBuilder query = new StringBuilder();

            // Get all ObjectColumns
            var props = from prop in itemType.GetProperties()
                        where prop.GetCustomAttributes(typeof(ObjectColumnAttribute), false).Count() > 0
                        select prop;

            // Get PrimaryKey column
            int count = props.Count();
            var pk = props.Single(prop => ((ObjectColumnAttribute)prop.GetCustomAttributes(false)[0]).IsPrimaryKey);

            query.Append("SELECT ");

            for (int i = 0; i < count - 1; i++)
            {
                query.AppendFormat("[{0}], ", props.ElementAt(i).Name);
            }

            query.AppendFormat("[{0}] FROM {1} WHERE ", props.ElementAt(count - 1).Name, itemType.Name); //{1}=@{1}", t.Name, pk.Name, id);

            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < filters.Length; i++)
            {
                QueryFilterElement filter = filters[i];

                if (!filter.IsValueNull())
                {
                    sqlParams.Add(new SqlParameter("@" + filter.Name, filter.Value));
                    query.AppendFormat("[{0}]=@{0} AND ", filter.Name);
                }
            }

            query.Remove(query.Length - 5, 5);

            List<T> items = new List<T>();
            using (DbDataReader r = SqlHelper.ExecuteReader(CommandType.Text, query.ToString(),
                sqlParams.ToArray()
            ))
            {
                while (r.Read())
                {
                    T item = Activator.CreateInstance<T>();

                    for (int i = 0; i < count; i++)
                    {
                        PropertyInfo prop = props.ElementAt(i);
                        object value = r[prop.Name];

                        if (prop == pk)
                            prop.SetValue(item, DataHelper.GetId(value), null);
                        else if (prop.PropertyType == typeof(DateTime))
                            prop.SetValue(item, DataHelper.GetDateTime(value), null);
                        else if (prop.PropertyType == typeof(int))
                            prop.SetValue(item, DataHelper.GetInt32(value), null);
                        else
                            prop.SetValue(item, value.ToString(), null);
                    }

                    items.Add(item);
                }
            }

            return items;
        }

        public bool Delete(int id)
        {
            Type itemType = typeof(T);
            StringBuilder query = new StringBuilder();

            // Get PrimaryKey
            var pk = (from prop in itemType.GetProperties()
                      where prop.GetCustomAttributes(typeof(ObjectColumnAttribute), false).Count() > 0
                      select prop)
                        .Single(prop => ((ObjectColumnAttribute)prop.GetCustomAttributes(false)[0]).IsPrimaryKey);

            // Generate delete SQL
            query.AppendFormat("DELETE FROM {0} WHERE [{1}]=@{1}", itemType.Name, pk.Name);

            // Execute delete SQL
            SqlHelper.ExecuteNonQuery(CommandType.Text, query.ToString(),
                new SqlParameter("@" + pk.Name, id));

            return true;
        }

        public int Update(T item)
        {
            Type itemType = typeof(T);
            StringBuilder query = new StringBuilder();

            // Get all ObjectColumns
            var props = from prop in itemType.GetProperties()
                        where prop.GetCustomAttributes(typeof(ObjectColumnAttribute), false).Count() > 0
                        select prop;

            // Get PrimaryKey column
            int count = props.Count();
            var pk = props.Single(prop => ((ObjectColumnAttribute)prop.GetCustomAttributes(false)[0]).IsPrimaryKey);

            int pkValue = (int)pk.GetValue(item, null);
            if (pkValue > 0)
            {
                // Update
                query.AppendFormat("UPDATE {0} SET ", itemType.Name);

                foreach (var prop in props)
                {
                    if (prop != pk)
                        query.AppendFormat("[{0}]=@{0},", prop.Name);
                }

                query.Remove(query.Length - 1, 1); // Remove last comma
                query.AppendFormat(" WHERE [{0}]=@{0}", pk.Name);
            }
            else
            {
                // Insert

                StringBuilder subQuery = new StringBuilder();
                query.AppendFormat("INSERT INTO {0} (", itemType.Name);
                subQuery.Append("VALUES(");

                foreach (var prop in props)
                {
                    query.AppendFormat("[{0}],", prop.Name);
                    subQuery.AppendFormat("@{0},", prop.Name);
                }

                // Remove last comma and finalize sub query
                subQuery.Remove(subQuery.Length - 1, 1);
                subQuery.Append(")");

                // Remove last comma and combine sub query into the main
                query.Remove(query.Length - 1, 1);
                query.Append(") " + subQuery);

                // Retrieve the new ID and update the item's value
                pkValue = WebObject.GetNextRecordId(itemType.Name);
                pk.SetValue(item, pkValue, null);
            }

            // Prepare the select parameters
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            foreach (var prop in props)
            {
                sqlParams.Add(new SqlParameter("@" + prop.Name, prop.GetValue(item, null)));
            }

            SqlHelper.ExecuteScalar(CommandType.Text,
                query.ToString(), sqlParams.ToArray());

            return pkValue;
        }

        public IEnumerable<T> GetList()
        {
            List<T> items = new List<T>();

            Type itemType = typeof(T);
            StringBuilder query = new StringBuilder();

            // Get all ObjectColumns
            var props = from prop in itemType.GetProperties()
                        where prop.GetCustomAttributes(typeof(ObjectColumnAttribute), false).Count() > 0
                        select prop;

            // Get PrimaryKey column
            int count = props.Count();
            var pk = props.Single(prop => ((ObjectColumnAttribute)prop.GetCustomAttributes(false)[0]).IsPrimaryKey);

            XmlDocument xdoc = LoadXmlDocument(itemType.Name);

            XmlNodeList nodes = xdoc.SelectNodes(string.Format("/des/{0}", itemType.Name));
            if (nodes.Count > 0)
            {
                foreach (XmlNode node in nodes)
                {
                    T item = Activator.CreateInstance<T>();

                    // Get row values and assign to item
                    for (int i = 0; i < count; i++)
                    {
                        PropertyInfo prop = props.ElementAt(i);
                        string value = XmlUtil.GetNodeText(node, prop.Name);

                        if (prop == pk)
                            prop.SetValue(item, DataHelper.GetId(value), null);
                        else if (prop.PropertyType == typeof(DateTime))
                            prop.SetValue(item, DataHelper.GetDateTime(value), null);
                        else if (prop.PropertyType == typeof(int))
                            prop.SetValue(item, DataHelper.GetInt32(value), null);
                        else
                            prop.SetValue(item, value.ToString(), null);
                    }

                    items.Add(item);
                }
            }

            return items;
        }

        #region IDataManager Members

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public int UpdateAllFromCache()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
