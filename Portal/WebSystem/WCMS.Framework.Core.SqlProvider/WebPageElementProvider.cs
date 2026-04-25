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
    public class WebPageElementProvider : IWebPageElementProvider
    {
        private static string TableName => DbSyntax.QuoteIdentifier("WebPageElement");

        public WebPageElementProvider() { }

        /// <summary>
        /// GetByParentObjectId
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public IEnumerable<WebPageElement> GetList(int recordId, int objectId)
        {
            List<WebPageElement> items = new List<WebPageElement>();
            var sql = "SELECT * FROM " + TableName + " WHERE " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId AND " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ObjectId", objectId)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        /// <summary>
        /// GetByPanelId
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="objectId"></param>
        /// <param name="templatePanelId"></param>
        /// <returns></returns>
        public IEnumerable<WebPageElement> GetList(int recordId, int objectId, int templatePanelId)
        {
            List<WebPageElement> items = new List<WebPageElement>();
            var sql = "SELECT * FROM " + TableName + " WHERE " + DbSyntax.QuoteIdentifier("TemplatePanelId") + " = @TemplatePanelId AND " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId AND " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@TemplatePanelId", templatePanelId),
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ObjectId", objectId)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public WebPageElement Get(int id)
        {
            var sql = "SELECT * FROM " + TableName + " WHERE " + DbSyntax.QuoteIdentifier("PageElementId") + " = @PageElementId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PageElementId", id)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public int GetCount(int recordId, int objectId, int templatePanelId)
        {
            var sql = "SELECT COUNT(1) FROM " + TableName + " WHERE " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId AND " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("TemplatePanelId") + " = @TemplatePanelId";
            object o = DbHelper.ExecuteScalar(CommandType.Text, sql,
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@TemplatePanelId", templatePanelId));

            return DataUtil.GetId(o);
        }

        public int GetMaxRank(int recordId, int objectId, int templatePanelId)
        {
            var sql = "SELECT MAX(" + DbSyntax.QuoteIdentifier("Rank") + ") FROM " + TableName + " WHERE " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId AND " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " + DbSyntax.QuoteIdentifier("TemplatePanelId") + " = @TemplatePanelId";
            object o = DbHelper.ExecuteScalar(CommandType.Text, sql,
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@TemplatePanelId", templatePanelId));

            return DataUtil.GetId(o);
        }

        public int Update(WebPageElement item)
        {
            // Validation goes here

            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + TableName + " SET " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId, " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("TemplatePanelId") + " = @TemplatePanelId, " +
                    DbSyntax.QuoteIdentifier("Rank") + " = @Rank, " +
                    DbSyntax.QuoteIdentifier("PartControlTemplateId") + " = @PartControlTemplateId, " +
                    DbSyntax.QuoteIdentifier("Active") + " = @Active, " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId, " +
                    DbSyntax.QuoteIdentifier("UsePartTemplatePath") + " = @UsePartTemplatePath, " +
                    DbSyntax.QuoteIdentifier("PublicAccess") + " = @PublicAccess, " +
                    DbSyntax.QuoteIdentifier("ManagementAccess") + " = @ManagementAccess" +
                    " WHERE " + DbSyntax.QuoteIdentifier("PageElementId") + " = @PageElementId";
                parms = new[] {
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@TemplatePanelId", item.TemplatePanelId),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@PartControlTemplateId", item.PartControlTemplateId),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@UsePartTemplatePath", item.UsePartTemplatePath),
                    DbHelper.CreateParameter("@PublicAccess", item.PublicAccess),
                    DbHelper.CreateParameter("@ManagementAccess", item.ManagementAccess),
                    DbHelper.CreateParameter("@PageElementId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + TableName + " (" +
                    DbSyntax.QuoteIdentifier("RecordId") + ", " +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("TemplatePanelId") + ", " +
                    DbSyntax.QuoteIdentifier("Rank") + ", " +
                    DbSyntax.QuoteIdentifier("PartControlTemplateId") + ", " +
                    DbSyntax.QuoteIdentifier("Active") + ", " +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("UsePartTemplatePath") + ", " +
                    DbSyntax.QuoteIdentifier("PublicAccess") + ", " +
                    DbSyntax.QuoteIdentifier("ManagementAccess") +
                    ") VALUES (@RecordId, @Name, @TemplatePanelId, @Rank, @PartControlTemplateId, @Active, @ObjectId, @UsePartTemplatePath, @PublicAccess, @ManagementAccess)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("PageElementId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@TemplatePanelId", item.TemplatePanelId),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@PartControlTemplateId", item.PartControlTemplateId),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@UsePartTemplatePath", item.UsePartTemplatePath),
                    DbHelper.CreateParameter("@PublicAccess", item.PublicAccess),
                    DbHelper.CreateParameter("@ManagementAccess", item.ManagementAccess)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj.ToString());
            }

            return item.Id;
        }

        public bool Delete(int PageElementId)
        {
            var sql = "DELETE FROM " + TableName + " WHERE " + DbSyntax.QuoteIdentifier("PageElementId") + " = @PageElementId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@PageElementId", PageElementId));

            return true;
        }

        public WebPageElement From(DbDataReader r)
        {
            WebPageElement item = new WebPageElement();
            item.Id = DataUtil.GetId(r["PageElementId"].ToString());
            item.RecordId = DataUtil.GetId(r["RecordId"].ToString());
            item.Name = r["Name"].ToString();
            item.TemplatePanelId = DataUtil.GetId(r["TemplatePanelId"].ToString());
            item.Rank = Convert.ToInt32(r["Rank"].ToString());
            item.Active = Convert.ToInt32(r["Active"].ToString());
            item.PartControlTemplateId = DataUtil.GetId(r["PartControlTemplateId"].ToString());
            item.ObjectId = DataUtil.GetId(r["ObjectId"]);
            item.UsePartTemplatePath = DataUtil.GetInt32(r["UsePartTemplatePath"]);
            item.PublicAccess = DataUtil.GetId(r["PublicAccess"]);
            item.ManagementAccess = DataUtil.GetInt32(r, "ManagementAccess");

            return item;
        }

        #region IDataProvider<WebPageElement> Members

        public WebPageElement Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebPageElement> GetList()
        {
            List<WebPageElement> items = new List<WebPageElement>();

            var sql = "SELECT * FROM " + TableName;
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

        public IEnumerable<WebPageElement> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            var sql = "SELECT COUNT(1) FROM " + TableName;
            object o = DbHelper.ExecuteScalar(CommandType.Text, sql);
            return DataUtil.GetId(o);
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WebPageElement Refresh(WebPageElement item)
        {
            throw new NotImplementedException();
        }
    }
}
