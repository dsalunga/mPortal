using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebGlobalPolicyProvider : IWebGlobalPolicyProvider
    {
        #region IDataProvider<WebGlobalPolicy> Members

        public bool Delete(int id)
        {
            var sql = "DELETE FROM WebGlobalPolicy WHERE " + DbSyntax.QuoteIdentifier("GlobalPolicyId") + " = @GlobalPolicyId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@GlobalPolicyId", id));

            return true;
        }

        public WebGlobalPolicy Get(int id)
        {
            var sql = "SELECT * FROM WebGlobalPolicy WHERE " + DbSyntax.QuoteIdentifier("GlobalPolicyId") + " = @GlobalPolicyId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@GlobalPolicyId", id)))
            {
                if (r.Read())
                {
                    return From(r);
                }
            }

            return null;
        }

        private WebGlobalPolicy From(DbDataReader r)
        {
            WebGlobalPolicy item = new WebGlobalPolicy();
            item.Id = DataUtil.GetId(r["GlobalPolicyId"]);
            item.Name = r["Name"].ToString();
            return item;
        }

        public WebGlobalPolicy Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebGlobalPolicy> GetList()
        {
            List<WebGlobalPolicy> items = new List<WebGlobalPolicy>();

            var sql = "SELECT * FROM WebGlobalPolicy";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                {
                    items.Add(From(r));
                }
            }

            return items;
        }

        public IEnumerable<WebGlobalPolicy> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(WebGlobalPolicy item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebGlobalPolicy SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name" +
                    " WHERE " + DbSyntax.QuoteIdentifier("GlobalPolicyId") + " = @GlobalPolicyId";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@GlobalPolicyId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebGlobalPolicy (" +
                    DbSyntax.QuoteIdentifier("Name") +
                    ") VALUES (@Name)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("GlobalPolicyId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WebGlobalPolicy Refresh(WebGlobalPolicy item)
        {
            throw new NotImplementedException();
        }
    }
}
