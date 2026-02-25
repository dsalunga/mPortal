using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
            using (DbDataReader r = DbHelper.ExecuteReader("WebObjectHeader_Get",
                DbHelper.CreateParameter("@ObjectHeaderId", objectHeaderId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        // This can contain multiple items, so this implementation should be changed
        public WebObjectHeader Get(int objectId, int recordId, int textResourceId)
        {
            using (DbDataReader r = DbHelper.ExecuteReader("WebObjectHeader_Get",
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@TextResourceId", textResourceId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public int Update(WebObjectHeader item)
        {
            object o = DbHelper.ExecuteScalar("WebObjectHeader_Set",
                DbHelper.CreateParameter("@ObjectHeaderId", item.Id),
                DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                DbHelper.CreateParameter("@RecordId", item.RecordId),
                DbHelper.CreateParameter("@TextResourceId", item.TextResourceId));

            item.Id = DataUtil.GetId(o);
            return item.Id;
        }

        public bool Delete(int objectHeaderId)
        {
            DbHelper.ExecuteNonQuery("WebObjectHeader_Del",
                DbHelper.CreateParameter("@ObjectHeaderId", objectHeaderId));

            return true;
        }

        public IEnumerable<WebObjectHeader> GetList(int objectId, int recordId)
        {
            List<WebObjectHeader> items = new List<WebObjectHeader>();

            using (DbDataReader r = DbHelper.ExecuteReader("WebObjectHeader_Get",
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)
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

            using (DbDataReader r = DbHelper.ExecuteReader("WebObjectHeader_Get",
                DbHelper.CreateParameter("@TextResourceId", textResourceId)))
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

            using (DbDataReader r = DbHelper.ExecuteReader("WebObjectHeader_Get"))
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
            item.Id = DataUtil.GetId(r, "ObjectHeaderId");
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.TextResourceId = DataUtil.GetId(r, WebColumns.TextResourceId);

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
