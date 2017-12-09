using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

//using Enu = WCMS.Framework.WebObjectHeaderEnum;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebObjectHeaderProvider : IWebObjectHeaderProvider
    {
        public WebObjectHeaderProvider() { }

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public WebObjectHeader Get(int objectHeaderId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebObjectHeader_Get",
                new SqlParameter("@ObjectHeaderId", objectHeaderId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        // This can contain multiple items, so this implementation should be changed
        public WebObjectHeader Get(int objectId, int recordId, int textResourceId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebObjectHeader_Get",
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@TextResourceId", textResourceId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public int Update(WebObjectHeader item)
        {
            object o = SqlHelper.ExecuteScalar("WebObjectHeader_Set",
                new SqlParameter("@ObjectHeaderId", item.Id),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@TextResourceId", item.TextResourceId));

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        public bool Delete(int objectHeaderId)
        {
            SqlHelper.ExecuteNonQuery("WebObjectHeader_Del",
                new SqlParameter("@ObjectHeaderId", objectHeaderId));

            return true;
        }

        public IEnumerable<WebObjectHeader> GetList(int objectId, int recordId)
        {
            List<WebObjectHeader> items = new List<WebObjectHeader>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebObjectHeader_Get",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebObjectHeader> GetList(int textResourceId)
        {
            List<WebObjectHeader> items = new List<WebObjectHeader>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebObjectHeader_Get",
                new SqlParameter("@TextResourceId", textResourceId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebObjectHeader> GetList()
        {
            List<WebObjectHeader> items = new List<WebObjectHeader>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebObjectHeader_Get"))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public WebObjectHeader From(DbDataReader r)
        {
            WebObjectHeader item = new WebObjectHeader();
            item.Id = DataHelper.GetId(r, "ObjectHeaderId");
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.TextResourceId = DataHelper.GetId(r, WebColumns.TextResourceId);

            return item;
        }

        #region IDataProvider<WebObjectHeader> Members

        public WebObjectHeader Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebObjectHeader> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion


        public WebObjectHeader Refresh(WebObjectHeader item)
        {
            throw new NotImplementedException();
        }
    }
}
