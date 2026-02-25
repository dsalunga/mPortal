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
    public class WebTemplatePanelProvider : IWebTemplatePanelProvider
    {
        public WebTemplatePanelProvider() { }

        public IEnumerable<WebTemplatePanel> GetList()
        {
            List<WebTemplatePanel> items = new List<WebTemplatePanel>();

            var sql = "SELECT * FROM WebTemplatePanel";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebTemplatePanel> GetList(int objectId=-2, int recordId=-2)
        {
            List<WebTemplatePanel> items = new List<WebTemplatePanel>();

            var sql = "SELECT * FROM WebTemplatePanel WHERE " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public WebTemplatePanel Get(int templatePanelId)
        {
            var sql = "SELECT * FROM WebTemplatePanel WHERE " + DbSyntax.QuoteIdentifier("TemplatePanelId") + " = @TemplatePanelId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@TemplatePanelId", templatePanelId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public int Update(WebTemplatePanel item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebTemplatePanel SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId, " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId, " +
                    DbSyntax.QuoteIdentifier("PanelName") + " = @PanelName, " +
                    DbSyntax.QuoteIdentifier("Rank") + " = @Rank" +
                    " WHERE " + DbSyntax.QuoteIdentifier("TemplatePanelId") + " = @TemplatePanelId";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@PanelName", item.PanelName),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@TemplatePanelId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebTemplatePanel (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + ", " +
                    DbSyntax.QuoteIdentifier("PanelName") + ", " +
                    DbSyntax.QuoteIdentifier("Rank") +
                    ") VALUES (@Name, @ObjectId, @RecordId, @PanelName, @Rank)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("TemplatePanelId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@PanelName", item.PanelName),
                    DbHelper.CreateParameter("@Rank", item.Rank)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj.ToString());
            }

            return item.Id;
        }

        public bool Delete(int templatePanelId)
        {
            var sql = "DELETE FROM WebTemplatePanel WHERE " + DbSyntax.QuoteIdentifier("TemplatePanelId") + " = @TemplatePanelId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@TemplatePanelId", templatePanelId));

            return true;
        }


        public WebTemplatePanel From(DbDataReader r)
        {
            WebTemplatePanel item = new WebTemplatePanel();
            item.Id = DataUtil.GetId(r, "TemplatePanelId");
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.PanelName = r["PanelName"].ToString();
            item.Rank = DataUtil.GetInt32(r, WebColumns.Rank);

            return item;
        }

        #region IDataProvider<WebTemplatePanel> Members


        public WebTemplatePanel Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebTemplatePanel> GetList(params QueryFilterElement[] filters)
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


        public WebTemplatePanel Refresh(WebTemplatePanel item)
        {
            throw new NotImplementedException();
        }
    }
}
