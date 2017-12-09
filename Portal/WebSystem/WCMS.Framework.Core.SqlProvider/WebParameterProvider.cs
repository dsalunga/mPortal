using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebParameterProvider : GenericSqlDataProviderBase<WebParameter>, IWebParameterProvider
    {
        protected override string SelectProcedure { get { return "WebParameter_Get"; } }
        protected override string DeleteProcedure { get { return "WebParameter_Del"; } }

        protected override WebParameter From(IDataReader r, WebParameter source)
        {
            WebParameter item = source ?? new WebParameter();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.Value = DataHelper.Get(r, WebColumns.Value);
            item.IsRequired = DataHelper.GetInt32(r, "IsRequired");

            return item;
        }

        public override int Update(WebParameter item)
        {
            var obj = SqlHelper.ExecuteScalar("WebParameter_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Value", item.Value),
                new SqlParameter("@IsRequired", item.IsRequired));

            return base.UpdatePostProcess(item, obj);
        }

        #region IWebParameterProvider Members

        public IEnumerable<WebParameter> GetList(int objectId, int recordId)
        {
            List<WebParameter> items = new List<WebParameter>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public WebParameter Get(int objectId, int recordId, string name)
        {
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        #endregion
    }
}
