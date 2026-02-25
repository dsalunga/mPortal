using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    class WebPagePanelProvider : IWebPagePanelProvider
    {
        public WebPagePanel Get(int pagePanelId)
        {
            var sql = "SELECT * FROM WebPagePanel WHERE " + DbSyntax.QuoteIdentifier("PagePanelId") + " = @PagePanelId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PagePanelId", pagePanelId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebPagePanel> GetList(int pageId)
        {
            List<WebPagePanel> items = new List<WebPagePanel>();

            var sql = "SELECT * FROM WebPagePanel WHERE " + DbSyntax.QuoteIdentifier("PageId") + " = @PageId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PageId", pageId)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        private WebPagePanel From(DbDataReader r)
        {
            WebPagePanel item = new WebPagePanel();
            item.Id = DataUtil.GetId(r["PagePanelId"]);
            item.TemplatePanelId = DataUtil.GetId(r["TemplatePanelId"]);
            item.PageId = DataUtil.GetId(r["PageId"]);
            item.UsageTypeId = Convert.ToInt32(r["UsageTypeId"].ToString());

            return item;
        }

        public WebPagePanel Get(int templatePanelId, int pageId)
        {
            var sql = "SELECT * FROM WebPagePanel WHERE " + DbSyntax.QuoteIdentifier("PageId") + " = @PageId AND " + DbSyntax.QuoteIdentifier("TemplatePanelId") + " = @TemplatePanelId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PageId", pageId),
                DbHelper.CreateParameter("@TemplatePanelId", templatePanelId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public int Update(WebPagePanel item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebPagePanel SET " +
                    DbSyntax.QuoteIdentifier("TemplatePanelId") + " = @TemplatePanelId, " +
                    DbSyntax.QuoteIdentifier("PageId") + " = @PageId, " +
                    DbSyntax.QuoteIdentifier("UsageTypeId") + " = @UsageTypeId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("PagePanelId") + " = @PagePanelId";
                parms = new[] {
                    DbHelper.CreateParameter("@TemplatePanelId", item.TemplatePanelId),
                    DbHelper.CreateParameter("@PageId", item.PageId),
                    DbHelper.CreateParameter("@UsageTypeId", item.UsageTypeId),
                    DbHelper.CreateParameter("@PagePanelId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebPagePanel (" +
                    DbSyntax.QuoteIdentifier("TemplatePanelId") + ", " +
                    DbSyntax.QuoteIdentifier("PageId") + ", " +
                    DbSyntax.QuoteIdentifier("UsageTypeId") +
                    ") VALUES (@TemplatePanelId, @PageId, @UsageTypeId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("PagePanelId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@TemplatePanelId", item.TemplatePanelId),
                    DbHelper.CreateParameter("@PageId", item.PageId),
                    DbHelper.CreateParameter("@UsageTypeId", item.UsageTypeId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public bool Delete(int pagePanelId)
        {
            var sql = "DELETE FROM WebPagePanel WHERE " + DbSyntax.QuoteIdentifier("PagePanelId") + " = @PagePanelId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@PagePanelId", pagePanelId));

            return true;
        }

        #region IDataProvider<WebPagePanel> Members


        public WebPagePanel Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebPagePanel> GetList()
        {
            List<WebPagePanel> items = new List<WebPagePanel>();

            var sql = "SELECT * FROM WebPagePanel";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
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

        public IEnumerable<WebPagePanel> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WebPagePanel Refresh(WebPagePanel item)
        {
            throw new NotImplementedException();
        }
    }
}
