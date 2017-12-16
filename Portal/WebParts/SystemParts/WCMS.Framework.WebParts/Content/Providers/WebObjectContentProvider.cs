using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Content.Providers
{
    public class WebObjectContentProvider : IWebObjectContentProvider
    {
        public WebObjectContentProvider() { }

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public WebObjectContent Get(int objectContentId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebObjectContent_Get",
                new SqlParameter("@ObjectContentId", objectContentId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebObjectContent GetByObjectId(int objectId, int recordId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebObjectContent_Get",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebObjectContent> GetList(int objectId)
        {
            List<WebObjectContent> items = new List<WebObjectContent>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebObjectContent_Get",
                new SqlParameter("@ObjectId", objectId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebObjectContent> GetList()
        {
            List<WebObjectContent> items = new List<WebObjectContent>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebObjectContent_Get"))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(WebObjectContent item)
        {
            object o = SqlHelper.ExecuteScalar("WebObjectContent_Set",
                new SqlParameter("@ObjectContentId", item.Id),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@ContentId", item.ContentId),
                new SqlParameter("@RecordId", item.RecordId)
            );

            item.Id = DataHelper.GetId(o.ToString());
            return item.Id;
        }

        public bool Delete(int objectContentId)
        {
            SqlHelper.ExecuteNonQuery("WebObjectContent_Del",
                new SqlParameter("@ObjectContentId", objectContentId));

            return true;
        }

        public WebObjectContent From(DbDataReader r)
        {
            WebObjectContent item = new WebObjectContent();
            item.Id = DataHelper.GetId(r["ObjectContentId"].ToString());
            item.ContentId = DataHelper.GetId(r["ContentId"].ToString());
            item.ObjectId = DataHelper.GetId(r["ObjectId"].ToString());
            item.RecordId = DataHelper.GetId(r["RecordId"].ToString());

            return item;
        }

        public WebObjectContent From(DataRow r)
        {
            WebObjectContent item = new WebObjectContent();
            item.Id = DataHelper.GetId(r["ObjectContentId"].ToString());
            item.ContentId = DataHelper.GetId(r["ContentId"].ToString());
            item.ObjectId = DataHelper.GetId(r["ObjectId"].ToString());
            item.RecordId = DataHelper.GetId(r["RecordId"].ToString());

            return item;
        }

        #region IDataProvider<WebObjectContent> Members

        public WebObjectContent Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebObjectContent> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion


        public WebObjectContent Refresh(WebObjectContent item)
        {
            throw new NotImplementedException();
        }
    }
}
