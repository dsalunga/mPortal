using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Reflection;

using WCMS.Common.Utilities;

namespace WCMS.Common.Data
{
    public class GenericSqlDataProvider
    {
        private GenericDataTable _table;

        private GenericSqlDataProvider() { }

        public GenericSqlDataProvider(GenericDataTable table)
        {
            _table = table;
        }

        public virtual GenericDataRow Get(int id)
        {
            Type itemType = typeof(object);
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
            query.Append(DbSyntax.QuoteIdentifier(propLast == pk ? mappingName : propLast.Name) + " FROM " + itemType.Name + " WHERE " + DbSyntax.QuoteIdentifier(mappingName) + "=@" + mappingName);

            // Fetch the record
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, query.ToString(),
                DbHelper.CreateParameter("@" + mappingName, id)
            ))
            {
                if (r.Read())
                {
                    GenericDataRow item = new GenericDataRow(_table); // Activator.CreateInstance<T>();

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

            return null;
        }

        public virtual GenericDataRow Get(params QueryFilterElement[] filters)
        {
            Type type = typeof(object);
            StringBuilder query = new StringBuilder();

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

            query.Append(DbSyntax.QuoteIdentifier(pk == propLast ? mappingName : propLast.Name));
            query.AppendFormat(" FROM {0} WHERE ", type.Name); //{1}=@{1}", t.Name, pk.Name, id);

            List<DbParameter> sqlParams = new List<DbParameter>();
            for (int i = 0; i < filters.Length; i++)
            {
                QueryFilterElement filter = filters[i];

                sqlParams.Add(DbHelper.CreateParameter("@" + filter.Name, filter.Value));
                query.Append(DbSyntax.QuoteIdentifier(filter.Name) + "=@" + filter.Name);

                if (i < filters.Length - 1)
                    query.Append(" AND ");
            }

            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, query.ToString(),
                sqlParams.ToArray()
            ))
            {
                if (r.Read())
                {
                    GenericDataRow item = new GenericDataRow(_table); // Activator.CreateInstance<T>();

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

            return null;
        }

        public virtual List<GenericDataRow> GetList(params QueryFilterElement[] filters)
        {
            Type itemType = typeof(object);
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

            query.Append(DbSyntax.QuoteIdentifier(propLast == pk ? mappingName : propLast.Name) + " FROM " + itemType.Name + " WHERE ");

            List<DbParameter> sqlParams = new List<DbParameter>();
            for (int i = 0; i < filters.Length; i++)
            {
                QueryFilterElement filter = filters[i];

                if (!filter.IsValueNull())
                {
                    sqlParams.Add(DbHelper.CreateParameter("@" + filter.Name, filter.Value));
                    query.Append(DbSyntax.QuoteIdentifier(filter.Name) + "=@" + filter.Name + " AND ");
                }
            }

            query.Remove(query.Length - 5, 5);

            List<GenericDataRow> items = new List<GenericDataRow>();
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, query.ToString(),
                sqlParams.ToArray()
            ))
            {
                while (r.Read())
                {
                    GenericDataRow item = new GenericDataRow(_table); //Activator.CreateInstance<>();

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

        public virtual bool Delete(int id)
        {
            Type itemType = typeof(object);
            StringBuilder query = new StringBuilder();

            // Get PrimaryKey
            var pk = (from prop in itemType.GetProperties()
                      where prop.GetCustomAttributes(typeof(ObjectColumnAttribute), false).Count() > 0
                      select prop)
                        .Single(prop => ((ObjectColumnAttribute)prop.GetCustomAttributes(false).First()).IsPrimaryKey);
            var pkAttr = pk.GetCustomAttributes(false).First() as ObjectColumnAttribute;
            string mappingName = string.IsNullOrEmpty(pkAttr.MappingName) ? pk.Name : pkAttr.MappingName;

            // Generate delete SQL
            query.Append("DELETE FROM " + itemType.Name + " WHERE " + DbSyntax.QuoteIdentifier(mappingName) + "=@" + mappingName);

            // Execute delete SQL
            DbHelper.ExecuteNonQuery(CommandType.Text, query.ToString(),
                DbHelper.CreateParameter("@" + mappingName, id));

            return true;
        }

        public virtual int Update(GenericDataRow item)
        {
            Type itemType = typeof(object);
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
                        query.Append(DbSyntax.QuoteIdentifier(prop.Name) + "=@" + prop.Name + ",");
                }

                query.Remove(query.Length - 1, 1); // Remove last comma
                query.Append(" WHERE " + DbSyntax.QuoteIdentifier(mappingName) + "=@" + mappingName);
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
                    query.Append(DbSyntax.QuoteIdentifier(propName) + ",");
                    subQuery.AppendFormat("@{0},", propName);
                }

                // Remove last comma and finalize sub query
                subQuery.Remove(subQuery.Length - 1, 1);
                subQuery.Append(")");

                // Remove last comma and combine sub query into the main
                query.Remove(query.Length - 1, 1);
                query.Append(") " + subQuery);

                // Retrieve the new ID and update the item's value
                // @@@ pkValue = WebObject.GetNextRecordId(itemType.Name);
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

        public virtual List<GenericDataRow> GetList()
        {
            Type itemType = typeof(object);
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
            query.Append(DbSyntax.QuoteIdentifier(propLast == pk ? mappingName : propLast.Name) + " FROM " + itemType.Name + " ");

            List<GenericDataRow> items = new List<GenericDataRow>();
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, query.ToString()))
            {
                while (r.Read())
                {
                    GenericDataRow item = new GenericDataRow(_table); // Activator.CreateInstance<T>();

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

        public virtual int GetCount()
        {
            return GetList().Count;
        }

        #region IDataManager Members


        public virtual int UpdateAllFromCache()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
