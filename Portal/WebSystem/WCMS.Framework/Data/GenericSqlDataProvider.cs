using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Reflection;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public class GenericSqlDataProvider<T> : GenericSqlDataProvider, IDataProvider<T> where T : IWebObject
    {
        public virtual T Get(int id)
        {
            return Get<T>(id);
        }

        public virtual T Get(params QueryFilterElement[] filters)
        {
            return Get<T>(filters);
        }

        public virtual IEnumerable<T> GetList(params QueryFilterElement[] filters)
        {
            return GetList<T>(filters);
        }

        public virtual int GetCount()
        {
            return GetCount<T>();
        }

        public virtual bool Delete(int id)
        {
            return Delete<T>(id);
        }

        public virtual int Update(T item)
        {
            return Update<T>(item);
        }

        public virtual IEnumerable<T> GetList()
        {
            return GetList<T>();
        }

        public virtual IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            return GetByDirectory(directoryId, loweredKeyword);
        }

        #region IDataManager Members


        public virtual int UpdateAllFromCache()
        {
            return UpdateAllFromCache<T>();
        }

        #endregion

        public T Refresh(T item)
        {
            return Get<T>(item.Id, item);
        }
    }

    public class GenericSqlDataProvider : IDataProvider
    {
        public virtual T Get<T>(int id) where T : IWebObject
        {
            return Get<T>(id, default(T));
        }

        public T Get<T>(int id, T source) where T : IWebObject
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
            var pkAttr = pk.GetCustomAttributes(false).First() as ObjectColumnAttribute;
            string mappingName = string.IsNullOrEmpty(pkAttr.MappingName) ? pk.Name : pkAttr.MappingName;
            var propLast = props.Last();

            query.Append("SELECT ");

            // Include all columns into the SELECT except the last one
            for (int i = 0; i < count - 1; i++)
            {
                var prop = props.ElementAt(i);
                query.AppendFormat("{0}, ", DbSyntax.QuoteIdentifier(prop == pk ? mappingName : prop.Name));
            }

            // Finalize query
            query.AppendFormat("{0} FROM {1} WHERE {2}=@{3}", DbSyntax.QuoteIdentifier(propLast == pk ? mappingName : propLast.Name), itemType.Name, DbSyntax.QuoteIdentifier(mappingName), mappingName);

            // Fetch the record
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, query.ToString(),
                DbHelper.CreateParameter("@" + mappingName, id)
            ))
            {
                if (r.Read())
                {
                    T item = source == null ? Activator.CreateInstance<T>() : source;

                    for (int i = 0; i < count; i++)
                    {
                        var prop = props.ElementAt(i);
                        if (prop == pk)
                        {
                            object value = r[mappingName];
                            prop.SetValue(item, DataUtil.GetId(value), null);
                        }
                        else
                        {
                            object value = r[prop.Name];

                            if (prop.PropertyType == typeof(DateTime))
                                prop.SetValue(item, DataUtil.GetDateTime(value), null);
                            else if (prop.PropertyType == typeof(int))
                                prop.SetValue(item, DataUtil.GetInt32(value), null);
                            else
                                prop.SetValue(item, value.ToString(), null);
                        }
                    }

                    return item;
                }
            }

            return default(T);
        }

        public virtual T Get<T>(params QueryFilterElement[] filters) where T : IWebObject
        {
            Type type = typeof(T);
            var query = new StringBuilder();

            // Get all ObjectColumns
            var props = from prop in type.GetProperties()
                        where prop.GetCustomAttributes(typeof(ObjectColumnAttribute), false).Count() > 0
                        select prop;

            // Get PrimaryKey column
            int count = props.Count();
            var pk = props.Single(prop => ((ObjectColumnAttribute)prop.GetCustomAttributes(false).First()).IsPrimaryKey);
            var pkAttr = pk.GetCustomAttributes(false).First() as ObjectColumnAttribute;
            string mappingName = string.IsNullOrEmpty(pkAttr.MappingName) ? pk.Name : pkAttr.MappingName;
            var propLast = props.Last();

            query.Append("SELECT ");

            for (int i = 0; i < count - 1; i++)
            {
                var prop = props.ElementAt(i);
                query.AppendFormat("{0}, ", DbSyntax.QuoteIdentifier(prop == pk ? mappingName : prop.Name));
            }

            query.AppendFormat("{0}", DbSyntax.QuoteIdentifier(pk == propLast ? mappingName : propLast.Name));
            query.AppendFormat(" FROM {0} WHERE ", type.Name); //{1}=@{1}", t.Name, pk.Name, id);

            List<DbParameter> sqlParams = new List<DbParameter>();
            for (int i = 0; i < filters.Length; i++)
            {
                QueryFilterElement filter = filters[i];

                sqlParams.Add(DbHelper.CreateParameter("@" + filter.Name, filter.Value));
                query.AppendFormat("{0}=@{1}", DbSyntax.QuoteIdentifier(filter.Name), filter.Name);

                if (i < filters.Length - 1)
                    query.Append(" AND ");
            }

            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, query.ToString(),
                sqlParams.ToArray()
            ))
            {
                if (r.Read())
                {
                    T item = Activator.CreateInstance<T>();

                    for (int i = 0; i < count; i++)
                    {
                        PropertyInfo prop = props.ElementAt(i);
                        if (prop == pk)
                        {
                            object value = r[mappingName];
                            prop.SetValue(item, DataUtil.GetId(value), null);
                        }
                        else
                        {
                            object value = r[prop.Name];
                            if (prop.PropertyType == typeof(DateTime))
                                prop.SetValue(item, DataUtil.GetDateTime(value), null);
                            else if (prop.PropertyType == typeof(int))
                                prop.SetValue(item, DataUtil.GetInt32(value), null);
                            else
                                prop.SetValue(item, value.ToString(), null);
                        }
                    }

                    return item;
                }
            }

            return default(T);
        }

        public virtual IEnumerable<T> GetList<T>(params QueryFilterElement[] filters) where T : IWebObject
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
            var pkAttr = pk.GetCustomAttributes(false).First() as ObjectColumnAttribute;
            string mappingName = string.IsNullOrEmpty(pkAttr.MappingName) ? pk.Name : pkAttr.MappingName;
            var propLast = props.Last();

            query.Append("SELECT ");

            for (int i = 0; i < count - 1; i++)
            {
                var prop = props.ElementAt(i);
                query.AppendFormat("{0}, ", DbSyntax.QuoteIdentifier(prop == pk ? mappingName : prop.Name));
            }

            query.AppendFormat("{0} FROM {1} WHERE ", DbSyntax.QuoteIdentifier(propLast == pk ? mappingName : propLast.Name), itemType.Name); //{1}=@{1}", t.Name, pk.Name, id);

            List<DbParameter> sqlParams = new List<DbParameter>();
            for (int i = 0; i < filters.Length; i++)
            {
                QueryFilterElement filter = filters[i];

                if (!filter.IsValueNull())
                {
                    sqlParams.Add(DbHelper.CreateParameter("@" + filter.Name, filter.Value));
                    query.AppendFormat("{0}=@{1} AND ", DbSyntax.QuoteIdentifier(filter.Name), filter.Name);
                }
            }

            query.Remove(query.Length - 5, 5);

            List<T> items = new List<T>();
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, query.ToString(),
                sqlParams.ToArray()
            ))
            {
                while (r.Read())
                {
                    T item = Activator.CreateInstance<T>();

                    for (int i = 0; i < count; i++)
                    {
                        PropertyInfo prop = props.ElementAt(i);
                        if (prop == pk)
                        {
                            object value = r[mappingName];
                            prop.SetValue(item, DataUtil.GetId(value), null);
                        }
                        else
                        {
                            object value = r[prop.Name];
                            if (prop.PropertyType == typeof(DateTime))
                                prop.SetValue(item, DataUtil.GetDateTime(value), null);
                            else if (prop.PropertyType == typeof(int))
                                prop.SetValue(item, DataUtil.GetInt32(value), null);
                            else
                                prop.SetValue(item, value.ToString(), null);
                        }
                    }

                    items.Add(item);
                }
            }

            return items;
        }

        public virtual bool Delete<T>(int id) where T : IWebObject
        {
            Type itemType = typeof(T);
            StringBuilder query = new StringBuilder();

            // Get PrimaryKey
            var pk = (from prop in itemType.GetProperties()
                      where prop.GetCustomAttributes(typeof(ObjectColumnAttribute), false).Count() > 0
                      select prop)
                        .Single(prop => ((ObjectColumnAttribute)prop.GetCustomAttributes(false).First()).IsPrimaryKey);
            var pkAttr = pk.GetCustomAttributes(false).First() as ObjectColumnAttribute;
            string mappingName = string.IsNullOrEmpty(pkAttr.MappingName) ? pk.Name : pkAttr.MappingName;

            // Generate delete SQL
            query.AppendFormat("DELETE FROM {0} WHERE {1}=@{2}", itemType.Name, DbSyntax.QuoteIdentifier(mappingName), mappingName);

            // Execute delete SQL
            DbHelper.ExecuteNonQuery(CommandType.Text, query.ToString(),
                DbHelper.CreateParameter("@" + mappingName, id));

            return true;
        }

        public virtual int Update<T>(T item) where T : IWebObject
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
            var pkAttr = pk.GetCustomAttributes(false).First() as ObjectColumnAttribute;
            string mappingName = string.IsNullOrEmpty(pkAttr.MappingName) ? pk.Name : pkAttr.MappingName;
            var propLast = props.Last();

            int pkValue = (int)pk.GetValue(item, null);
            if (pkValue > 0)
            {
                // Update
                query.AppendFormat("UPDATE {0} SET ", itemType.Name);

                foreach (var prop in props)
                {
                    if (prop != pk)
                        query.AppendFormat("{0}=@{1},", DbSyntax.QuoteIdentifier(prop.Name), prop.Name);
                }

                query.Remove(query.Length - 1, 1); // Remove last comma
                query.AppendFormat(" WHERE {0}=@{1}", DbSyntax.QuoteIdentifier(mappingName), mappingName);
            }
            else
            {
                // Insert

                StringBuilder subQuery = new StringBuilder();
                query.AppendFormat("INSERT INTO {0} (", itemType.Name);
                subQuery.Append("VALUES(");

                foreach (var prop in props)
                {
                    string propName = prop == pk ? mappingName : prop.Name;
                    query.AppendFormat("{0},", DbSyntax.QuoteIdentifier(propName));
                    subQuery.AppendFormat("@{0},", propName);
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
            List<DbParameter> sqlParams = new List<DbParameter>();
            foreach (var prop in props)
            {
                string propName = prop == pk ? mappingName : prop.Name;
                sqlParams.Add(DbHelper.CreateParameter("@" + propName, prop.GetValue(item, null)));
            }

            DbHelper.ExecuteScalar(CommandType.Text,
                query.ToString(), sqlParams.ToArray());

            return pkValue;
        }

        public virtual IEnumerable<T> GetList<T>() where T : IWebObject
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
            var pkAttr = pk.GetCustomAttributes(false).First() as ObjectColumnAttribute;
            string mappingName = string.IsNullOrEmpty(pkAttr.MappingName) ? pk.Name : pkAttr.MappingName;
            var propLast = props.Last();

            query.Append("SELECT ");

            // Build SELECT for all columns except last one
            for (int i = 0; i < count - 1; i++)
            {
                var prop = props.ElementAt(i);
                query.AppendFormat("{0}, ", DbSyntax.QuoteIdentifier(prop == pk ? mappingName : prop.Name));
            }

            // Complete the SQL query
            query.AppendFormat("{0} FROM {1} ", DbSyntax.QuoteIdentifier(propLast == pk ? mappingName : propLast.Name), itemType.Name);

            List<T> items = new List<T>();
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, query.ToString()))
            {
                while (r.Read())
                {
                    T item = Activator.CreateInstance<T>();

                    // Get row values and assign to item
                    for (int i = 0; i < count; i++)
                    {
                        PropertyInfo prop = props.ElementAt(i);
                        if (prop == pk)
                        {
                            object value = r[mappingName];
                            prop.SetValue(item, DataUtil.GetId(value), null);
                        }
                        else
                        {
                            object value = r[prop.Name];
                            if (prop.PropertyType == typeof(DateTime))
                                prop.SetValue(item, DataUtil.GetDateTime(value), null);
                            else if (prop.PropertyType == typeof(int))
                                prop.SetValue(item, DataUtil.GetInt32(value), null);
                            else
                                prop.SetValue(item, value.ToString(), null);
                        }
                    }

                    items.Add(item);
                }
            }

            return items;
        }

        public virtual int GetCount<T>() where T : IWebObject
        {
            Type itemType = typeof(T);
            var o = DbHelper.ExecuteScalar(CommandType.Text, String.Format("SELECT COUNT(1) FROM {0}", itemType.Name));
            if (o != null)
                return Convert.ToInt32(o.ToString());
            return -1;
        }

        #region IDataManager Members

        public virtual IEnumerable<WebDirectoryEntry> GetByDirectory<T>(int directoryId, string loweredKeyword) where T : IWebObject
        {
            throw new NotImplementedException();
        }

        public virtual int UpdateAllFromCache<T>() where T : IWebObject
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
