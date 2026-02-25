using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebSiteIdentityProvider : IWebSiteIdentityProvider
    {
        #region IDataProvider<WebSiteIdentity> Members

        public bool Delete(int id)
        {
            var sql = "DELETE FROM WebSiteIdentity WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id));

            return true;
        }

        public WebSiteIdentity Get(int id)
        {
            var sql = "SELECT * FROM WebSiteIdentity WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private WebSiteIdentity From(DbDataReader r)
        {
            var item = new WebSiteIdentity();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.SiteId = DataUtil.GetId(r, WebColumns.SiteId);
            item.HostName = DataUtil.Get(r, "HostName");
            item.UrlPath = DataUtil.Get(r, "UrlPath");
            item.Port = DataUtil.GetInt32(r, "Port");
            item.IPAddress = DataUtil.Get(r, "IPAddress");
            item.RedirectUrl = DataUtil.Get(r, "RedirectUrl");
            item.ProtocolId = DataUtil.GetInt32(r, "ProtocolId");

            return item;
        }

        public WebSiteIdentity Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebSiteIdentity> GetList()
        {
            var items = new List<WebSiteIdentity>();
            var sql = "SELECT * FROM WebSiteIdentity";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebSiteIdentity> GetList(int siteId)
        {
            var items = new List<WebSiteIdentity>();
            var sql = "SELECT * FROM WebSiteIdentity WHERE " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@SiteId", siteId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebSiteIdentity> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(WebSiteIdentity item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebSiteIdentity SET " +
                    DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId, " +
                    DbSyntax.QuoteIdentifier("HostName") + " = @HostName, " +
                    DbSyntax.QuoteIdentifier("UrlPath") + " = @UrlPath, " +
                    DbSyntax.QuoteIdentifier("Port") + " = @Port, " +
                    DbSyntax.QuoteIdentifier("IPAddress") + " = @IPAddress, " +
                    DbSyntax.QuoteIdentifier("RedirectUrl") + " = @RedirectUrl, " +
                    DbSyntax.QuoteIdentifier("ProtocolId") + " = @ProtocolId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@HostName", item.HostName),
                    DbHelper.CreateParameter("@UrlPath", item.UrlPath),
                    DbHelper.CreateParameter("@Port", item.Port),
                    DbHelper.CreateParameter("@IPAddress", item.IPAddress),
                    DbHelper.CreateParameter("@RedirectUrl", item.RedirectUrl),
                    DbHelper.CreateParameter("@ProtocolId", item.ProtocolId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebSiteIdentity (" +
                    DbSyntax.QuoteIdentifier("SiteId") + ", " +
                    DbSyntax.QuoteIdentifier("HostName") + ", " +
                    DbSyntax.QuoteIdentifier("UrlPath") + ", " +
                    DbSyntax.QuoteIdentifier("Port") + ", " +
                    DbSyntax.QuoteIdentifier("IPAddress") + ", " +
                    DbSyntax.QuoteIdentifier("RedirectUrl") + ", " +
                    DbSyntax.QuoteIdentifier("ProtocolId") +
                    ") VALUES (@SiteId, @HostName, @UrlPath, @Port, @IPAddress, @RedirectUrl, @ProtocolId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@HostName", item.HostName),
                    DbHelper.CreateParameter("@UrlPath", item.UrlPath),
                    DbHelper.CreateParameter("@Port", item.Port),
                    DbHelper.CreateParameter("@IPAddress", item.IPAddress),
                    DbHelper.CreateParameter("@RedirectUrl", item.RedirectUrl),
                    DbHelper.CreateParameter("@ProtocolId", item.ProtocolId)
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


        public WebSiteIdentity Refresh(WebSiteIdentity item)
        {
            throw new NotImplementedException();
        }
    }
}
