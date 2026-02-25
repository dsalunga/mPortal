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
    public class WebConstantProvider : GenericSqlDataProviderBase<WebConstant>, IWebConstantProvider
    {
        protected override string TableName { get { return "WebConstant"; } }
        protected override string IdColumn { get { return "ConstantId"; } }
        protected override string SelectProcedure { get { return "WebConstant_Get"; } }
        protected override string DeleteProcedure { get { return "WebConstant_Del"; } }

        public WebConstantProvider() { }

        protected override string IdParameter { get { return "ConstantId"; } }

        public IEnumerable<WebConstant> GetList(string category)
        {
            List<WebConstant> items = new List<WebConstant>();

            var sql = "SELECT * FROM WebConstant WHERE " + DbSyntax.QuoteIdentifier("Category") + " = @Category";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Category", category)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        //public IEnumerable<WebConstant> GetList()
        //{
        //    List<WebConstant> items = new List<WebConstant>();

        //    using (DbDataReader r = DbHelper.ExecuteReader("WebConstant_Get"))
        //    {
        //        if (r.HasRows)
        //            while (r.Read())
        //                items.Add(this.From(r));
        //    }

        //    return items;
        //}


        //public WebConstant Get(int constantId)
        //{
        //    // Check and return the cached object
        //    if (WebConstant.ObjectCache.ContainsKey(constantId))
        //        return WebConstant.ObjectCache[constantId];

        //    // Not in cache, get from DB and return
        //    using (DbDataReader r = DbHelper.ExecuteReader("WebConstant_Get",
        //        DbHelper.CreateParameter("@ConstantId", constantId)))
        //    {
        //        if (r.HasRows && r.Read())
        //        {
        //            WebConstant item = this.From(r);
        //            WebConstant.ObjectCache.Add(item.Id, item);

        //            return item;
        //        }
        //    }

        //    return null;
        //}

        //public Dictionary<int, WebConstant> GetCacheList()
        //{
        //    // Get all objects from DB
        //    using (DbDataReader r = DbHelper.ExecuteReader("WebConstant_Get"))
        //    {
        //        if (r.HasRows)
        //        {
        //            Dictionary<int, WebConstant> items = new Dictionary<int, WebConstant>();
        //            //_objectCache = new Dictionary<int, WebConstant>();
        //            while (r.Read())
        //            {
        //                WebConstant item = this.From(r);
        //                items.Add(item.Id, item);
        //            }

        //            return items;
        //        }
        //    }

        //    return null;
        //}

        public override int Update(WebConstant item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebConstant SET " +
                    DbSyntax.QuoteIdentifier("Value") + " = @Value, " +
                    DbSyntax.QuoteIdentifier("Rank") + " = @Rank, " +
                    DbSyntax.QuoteIdentifier("Category") + " = @Category, " +
                    DbSyntax.QuoteIdentifier("Text") + " = @Text" +
                    " WHERE " + DbSyntax.QuoteIdentifier("ConstantId") + " = @ConstantId";
                parms = new[] {
                    DbHelper.CreateParameter("@Value", item.Value),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@Category", item.Category),
                    DbHelper.CreateParameter("@Text", item.Text),
                    DbHelper.CreateParameter("@ConstantId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebConstant (" +
                    DbSyntax.QuoteIdentifier("Value") + ", " +
                    DbSyntax.QuoteIdentifier("Rank") + ", " +
                    DbSyntax.QuoteIdentifier("Category") + ", " +
                    DbSyntax.QuoteIdentifier("Text") +
                    ") VALUES (@Value, @Rank, @Category, @Text)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("ConstantId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Value", item.Value),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@Category", item.Category),
                    DbHelper.CreateParameter("@Text", item.Text)
                };
                var o = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(o);
            }

            //if (WebConstant.ObjectCache.ContainsKey(item.Id))
            //{
            //    WebConstant.ObjectCache[item.Id] = item;
            //}
            //else
            //{
            //    WebConstant.ObjectCache.Add(item.Id, item);
            //}

            return item.Id;
        }

        //public bool Delete(int constantId)
        //{
        //    DbHelper.ExecuteNonQuery("WebConstant_Del",
        //        DbHelper.CreateParameter("@ConstantId", constantId));

        //    if (WebConstant.ObjectCache.ContainsKey(constantId))
        //    {
        //        WebConstant.ObjectCache.Remove(constantId);
        //    }

        //    return true;
        //}

        protected override WebConstant From(IDataReader r, WebConstant source)
        {
            WebConstant item = source ?? new WebConstant();
            item.Id = DataUtil.GetId(r["ConstantId"]);
            item.Value = r["Value"] as string;
            item.Rank = Convert.ToInt32(r["Rank"] as string);
            item.Category = r["Category"] as string;
            item.Text = r["Text"] as string;

            return item;
        }

        //private WebConstant From(DataRow r)
        //{
        //    WebConstant item = new WebConstant();
        //    item.Id = DataHelper.GetId(r["ConstantId"]);
        //    item.Value = r["Value"] as string;
        //    item.Rank = Convert.ToInt32(r["Rank"] as string);
        //    item.Category = r["Category"] as string;
        //    item.Text = r["Text"] as string;

        //    return item;
        //}

        //#region IDataProvider<WebConstant> Members


        //public WebConstant Get(params QueryFilterElement[] filters)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<WebConstant> GetList(params QueryFilterElement[] filters)
        //{
        //    throw new NotImplementedException();
        //}

        //public int GetCount()
        //{
        //    return GetList().Count();
        //}

        //#endregion


        public IEnumerable<WebConstant> GetList(int objectId)
        {
            List<WebConstant> items = new List<WebConstant>();

            var sql = "SELECT * FROM WebConstant WHERE " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
