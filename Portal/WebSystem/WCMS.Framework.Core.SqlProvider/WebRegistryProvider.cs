using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Xml;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebRegistryProvider : IWebRegistryProvider
    {

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebRegistry> GetList()
        {
            List<WebRegistry> items = new List<WebRegistry>();
            var sql = "SELECT * FROM WebRegistry";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                {
                    items.Add(this.From(r));
                }
            }

            return items;
        }

        public WebRegistry Get(string key)
        {
            var sql = "SELECT * FROM WebRegistry WHERE " + DbSyntax.QuoteIdentifier("Key") + " = @Key";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Key", key)))
            {
                if (r.HasRows && r.Read())
                {
                    return this.From(r);
                }
            }

            return null;
        }

        public WebRegistry Get(string key, int parentId)
        {
            var sql = "SELECT * FROM WebRegistry WHERE " + DbSyntax.QuoteIdentifier("Key") + " = @Key AND " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Key", key),
                DbHelper.CreateParameter("@ParentId", parentId)
                ))
            {
                if (r.HasRows && r.Read())
                {
                    return this.From(r);
                }
            }

            return null;
        }

        public WebRegistry Get(int registryId)
        {
            var sql = "SELECT * FROM WebRegistry WHERE " + DbSyntax.QuoteIdentifier("RegistryId") + " = @RegistryId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@RegistryId", registryId)))
            {
                if (r.HasRows && r.Read())
                {
                    return this.From(r);
                }
            }

            return null;
        }

        public IEnumerable<WebRegistry> GetList(int parentId)
        {
            List<WebRegistry> items = new List<WebRegistry>();

            var sql = "SELECT * FROM WebRegistry WHERE " + DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ParentId", parentId)))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        items.Add(this.From(r));
                    }
                }
            }

            return items;
        }

        private WebRegistry From(DbDataReader r)
        {
            WebRegistry item = new WebRegistry();
            item.Id = DataUtil.GetId(r["RegistryId"]);
            item.Key = r["Key"].ToString();
            item.Value = r["Value"].ToString();
            item.ParentId = DataUtil.GetId(r["ParentId"]);
            item.StageId = DataUtil.GetId(r, "StageId");

            return item;
        }

        public int Update(string key, string value)
        {
            WebRegistry item = WebRegistry.Get(key);
            if (item == null)
            {
                item = new WebRegistry();
            }

            item.Key = key;
            item.Value = value;

            return this.Update(item);
        }

        public int Update(WebRegistry item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebRegistry SET " +
                    DbSyntax.QuoteIdentifier("Key") + " = @Key, " +
                    DbSyntax.QuoteIdentifier("Value") + " = @Value, " +
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId, " +
                    DbSyntax.QuoteIdentifier("StageId") + " = @StageId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("RegistryId") + " = @RegistryId";
                parms = new[] {
                    DbHelper.CreateParameter("@Key", item.Key),
                    DbHelper.CreateParameter("@Value", item.Value),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@StageId", item.StageId),
                    DbHelper.CreateParameter("@RegistryId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebRegistry (" +
                    DbSyntax.QuoteIdentifier("Key") + ", " +
                    DbSyntax.QuoteIdentifier("Value") + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") + ", " +
                    DbSyntax.QuoteIdentifier("StageId") +
                    ") VALUES (@Key, @Value, @ParentId, @StageId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("RegistryId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Key", item.Key),
                    DbHelper.CreateParameter("@Value", item.Value),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@StageId", item.StageId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public bool Delete(string key)
        {
            var sql = "DELETE FROM WebRegistry WHERE " + DbSyntax.QuoteIdentifier("Key") + " = @Key";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Key", key));

            return true;
        }

        public bool Delete(int registryId)
        {
            var sql = "DELETE FROM WebRegistry WHERE " + DbSyntax.QuoteIdentifier("RegistryId") + " = @RegistryId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@RegistryId", registryId));

            return true;
        }

        #region IDataProvider<WebRegistry> Members

        public WebRegistry Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebRegistry> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion


        public WebRegistry Refresh(WebRegistry item)
        {
            throw new NotImplementedException();
        }
    }
}
