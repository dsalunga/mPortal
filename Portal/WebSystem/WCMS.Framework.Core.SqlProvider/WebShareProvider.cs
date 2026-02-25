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
    public class WebShareProvider : GenericSqlDataProviderBase<WebShare>, IWebShareProvider
    {
        protected override string TableName { get { return "WebShare"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "WebShare_Get"; } }
        protected override string DeleteProcedure { get { return "WebShare_Del"; } }

        protected override WebShare From(IDataReader r, WebShare source)
        {
            WebShare item = new WebShare();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.ShareObjectId = DataUtil.GetId(r, "ShareObjectId");
            item.ShareRecordId = DataUtil.GetId(r, "ShareRecordId");
            item.Allow = DataUtil.GetInt32(r, "Allow");

            return item;
        }

        public IEnumerable<WebShare> GetList(int objectId, int recordId)
        {
            List<WebShare> items = new List<WebShare>();

            var sql = "SELECT * FROM WebShare WHERE " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)
            ))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public WebShare Get(int objectId, int recordId, int shareObjectId, int shareRecordId)
        {
            var sql = "SELECT * FROM WebShare WHERE " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId AND " +
                    DbSyntax.QuoteIdentifier("ShareObjectId") + " = @ShareObjectId AND " +
                    DbSyntax.QuoteIdentifier("ShareRecordId") + " = @ShareRecordId";
                using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ShareObjectId", shareObjectId),
                DbHelper.CreateParameter("@ShareRecordId", shareRecordId)
            ))
            {
                while (r.Read())
                    return From(r);
            }

            return null;
        }
    }
}
