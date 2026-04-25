using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebObjectProvider : IWebObjectProvider
    {
        private static string TableName => DbSyntax.QuoteIdentifier("WebObject");

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public WebObject Get(int id)
        {
            var sql = "SELECT * FROM " + TableName + " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebObject> GetList()
        {
            //return _provider.GetList<WebObject>();
            List<WebObject> items = new List<WebObject>();

            var sql = "SELECT * FROM " + TableName;
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(WebObject item)
        {
            //return _provider.Update<WebObject>(item);

            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + TableName + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name" + ", " +
                    DbSyntax.QuoteIdentifier("IdentityColumn") + " = @IdentityColumn" + ", " +
                    DbSyntax.QuoteIdentifier("ObjectType") + " = @ObjectType" + ", " +
                    DbSyntax.QuoteIdentifier("LastRecordId") + " = @LastRecordId" + ", " +
                    DbSyntax.QuoteIdentifier("MaxCacheCount") + " = @MaxCacheCount" + ", " +
                    DbSyntax.QuoteIdentifier("AccessTypeId") + " = @AccessTypeId" + ", " +
                    DbSyntax.QuoteIdentifier("CacheTypeId") + " = @CacheTypeId" + ", " +
                    DbSyntax.QuoteIdentifier("MaxHistoryCount") + " = @MaxHistoryCount" + ", " +
                    DbSyntax.QuoteIdentifier("Owner") + " = @Owner" + ", " +
                    DbSyntax.QuoteIdentifier("Prefix") + " = @Prefix" + ", " +
                    DbSyntax.QuoteIdentifier("DataProviderName") + " = @DataProviderName" + ", " +
                    DbSyntax.QuoteIdentifier("TypeName") + " = @TypeName" + ", " +
                    DbSyntax.QuoteIdentifier("CacheInterval") + " = @CacheInterval" + ", " +
                    DbSyntax.QuoteIdentifier("NameColumn") + " = @NameColumn" + ", " +
                    DbSyntax.QuoteIdentifier("FriendlyName") + " = @FriendlyName" + ", " +
                    DbSyntax.QuoteIdentifier("ManagerName") + " = @ManagerName" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@IdentityColumn", item.IdentityColumn),
                    DbHelper.CreateParameter("@ObjectType", item.ObjectType),
                    DbHelper.CreateParameter("@LastRecordId", item.LastRecordId),
                    DbHelper.CreateParameter("@MaxCacheCount", item.MaxCacheCount),
                    DbHelper.CreateParameter("@AccessTypeId", item.AccessTypeId),
                    DbHelper.CreateParameter("@CacheTypeId", item.CacheTypeId),
                    DbHelper.CreateParameter("@MaxHistoryCount", item.MaxHistoryCount),
                    DbHelper.CreateParameter("@Owner", item.Owner),
                    DbHelper.CreateParameter("@Prefix", item.Prefix),
                    DbHelper.CreateParameter("@DataProviderName", item.DataProviderName),
                    DbHelper.CreateParameter("@TypeName", item.TypeName),
                    DbHelper.CreateParameter("@CacheInterval", item.CacheInterval),
                    DbHelper.CreateParameter("@NameColumn", item.NameColumn),
                    DbHelper.CreateParameter("@FriendlyName", item.FriendlyName),
                    DbHelper.CreateParameter("@ManagerName", item.ManagerName),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + TableName + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("IdentityColumn") + ", " +
                    DbSyntax.QuoteIdentifier("ObjectType") + ", " +
                    DbSyntax.QuoteIdentifier("LastRecordId") + ", " +
                    DbSyntax.QuoteIdentifier("MaxCacheCount") + ", " +
                    DbSyntax.QuoteIdentifier("AccessTypeId") + ", " +
                    DbSyntax.QuoteIdentifier("CacheTypeId") + ", " +
                    DbSyntax.QuoteIdentifier("MaxHistoryCount") + ", " +
                    DbSyntax.QuoteIdentifier("Owner") + ", " +
                    DbSyntax.QuoteIdentifier("Prefix") + ", " +
                    DbSyntax.QuoteIdentifier("DataProviderName") + ", " +
                    DbSyntax.QuoteIdentifier("TypeName") + ", " +
                    DbSyntax.QuoteIdentifier("CacheInterval") + ", " +
                    DbSyntax.QuoteIdentifier("NameColumn") + ", " +
                    DbSyntax.QuoteIdentifier("FriendlyName") + ", " +
                    DbSyntax.QuoteIdentifier("ManagerName") +
                    ") VALUES (@Name, @IdentityColumn, @ObjectType, @LastRecordId, @MaxCacheCount, @AccessTypeId, @CacheTypeId, @MaxHistoryCount, @Owner, @Prefix, @DataProviderName, @TypeName, @CacheInterval, @NameColumn, @FriendlyName, @ManagerName)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@IdentityColumn", item.IdentityColumn),
                    DbHelper.CreateParameter("@ObjectType", item.ObjectType),
                    DbHelper.CreateParameter("@LastRecordId", item.LastRecordId),
                    DbHelper.CreateParameter("@MaxCacheCount", item.MaxCacheCount),
                    DbHelper.CreateParameter("@AccessTypeId", item.AccessTypeId),
                    DbHelper.CreateParameter("@CacheTypeId", item.CacheTypeId),
                    DbHelper.CreateParameter("@MaxHistoryCount", item.MaxHistoryCount),
                    DbHelper.CreateParameter("@Owner", item.Owner),
                    DbHelper.CreateParameter("@Prefix", item.Prefix),
                    DbHelper.CreateParameter("@DataProviderName", item.DataProviderName),
                    DbHelper.CreateParameter("@TypeName", item.TypeName),
                    DbHelper.CreateParameter("@CacheInterval", item.CacheInterval),
                    DbHelper.CreateParameter("@NameColumn", item.NameColumn),
                    DbHelper.CreateParameter("@FriendlyName", item.FriendlyName),
                    DbHelper.CreateParameter("@ManagerName", item.ManagerName)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public bool Delete(int id)
        {
            var sql = "DELETE FROM " + TableName + " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id));

            return true;
        }


        private WebObject From(DbDataReader r)
        {
            WebObject item = new WebObject();
            item.Id = DataUtil.GetId(r["Id"].ToString());
            item.Name = r["Name"].ToString();
            item.IdentityColumn = r["IdentityColumn"].ToString();
            item.ObjectType = r["ObjectType"].ToString();
            item.LastRecordId = DataUtil.GetInt32(r["LastRecordId"]);
            item.MaxCacheCount = Convert.ToInt32(r["MaxCacheCount"].ToString());
            item.AccessTypeId = DataUtil.GetId(r["AccessTypeId"]);
            item.CacheTypeId = DataUtil.GetId(r["CacheTypeId"]);
            item.MaxHistoryCount = Convert.ToInt32(r["MaxHistoryCount"].ToString());
            item.Owner = r["Owner"].ToString();
            item.Prefix = r["Prefix"].ToString();
            item.DataProviderName = r["DataProviderName"].ToString();
            item.TypeName = r["TypeName"].ToString();
            item.CacheInterval = DataUtil.GetInt32(r["CacheInterval"]);
            item.DateModified = (DateTime)r["DateModified"];
            item.ManagerName = r["ManagerName"].ToString();
            item.NameColumn = r["NameColumn"].ToString();
            item.FriendlyName = r["FriendlyName"].ToString();

            return item;
        }

        #region IWebObjectProvider Members


        public bool Update(List<WebObject> items)
        {
            foreach (var item in items)
            {
                item.Update();
            }

            return true;
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
            return GetList().Count();
        }

        #endregion


        public WebObject Refresh(WebObject item)
        {
            throw new NotImplementedException();
        }
    }
}
