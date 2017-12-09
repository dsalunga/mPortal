using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebConstantProvider : GenericSqlDataProviderBase<WebConstant>, IWebConstantProvider
    {
        public WebConstantProvider() { }

        protected override string IdParameter { get { return "ConstantId"; } }

        public IEnumerable<WebConstant> GetList(string category)
        {
            List<WebConstant> items = new List<WebConstant>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebConstant_Get",
                new SqlParameter("@Category", category)))
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

        //    using (DbDataReader r = SqlHelper.ExecuteReader("WebConstant_Get"))
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
        //    using (DbDataReader r = SqlHelper.ExecuteReader("WebConstant_Get",
        //        new SqlParameter("@ConstantId", constantId)))
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
        //    using (DbDataReader r = SqlHelper.ExecuteReader("WebConstant_Get"))
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
            object o = SqlHelper.ExecuteScalar("WebConstant_Set",
                new SqlParameter("@ConstantId", item.Id),
                new SqlParameter("@Value", item.Value),
                new SqlParameter("@Rank", item.Rank),
                new SqlParameter("@Category", item.Category),
                new SqlParameter("@Text", item.Text)
            );

            item.Id = DataHelper.GetId(o);

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
        //    SqlHelper.ExecuteNonQuery("WebConstant_Del",
        //        new SqlParameter("@ConstantId", constantId));

        //    if (WebConstant.ObjectCache.ContainsKey(constantId))
        //    {
        //        WebConstant.ObjectCache.Remove(constantId);
        //    }

        //    return true;
        //}

        protected override WebConstant From(IDataReader r, WebConstant source)
        {
            WebConstant item = source ?? new WebConstant();
            item.Id = DataHelper.GetId(r["ConstantId"]);
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

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@ObjectId", objectId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        protected override string SelectProcedure { get { return "WebConstant_Get"; } }
        protected override string DeleteProcedure { get { return "WebConstant_Del"; } }
    }
}
