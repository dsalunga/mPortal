using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebShareProvider : GenericSqlDataProviderBase<WebShare>, IWebShareProvider
    {
        protected override string SelectProcedure { get { return "WebShare_Get"; } }
        protected override string DeleteProcedure { get { return "WebShare_Del"; } }

        protected override WebShare From(IDataReader r, WebShare source)
        {
            WebShare item = new WebShare();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.ShareObjectId = DataHelper.GetId(r, "ShareObjectId");
            item.ShareRecordId = DataHelper.GetId(r, "ShareRecordId");
            item.Allow = DataHelper.GetInt32(r, "Allow");

            return item;
        }

        public IEnumerable<WebShare> GetList(int objectId, int recordId)
        {
            List<WebShare> items = new List<WebShare>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)
            ))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public WebShare Get(int objectId, int recordId, int shareObjectId, int shareRecordId)
        {
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@ShareObjectId", shareObjectId),
                new SqlParameter("@ShareRecordId", shareRecordId)
            ))
            {
                while (r.Read())
                    return From(r);
            }

            return null;
        }
    }
}
