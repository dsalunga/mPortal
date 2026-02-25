using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebParameterProvider : GenericSqlDataProviderBase<WebParameter>, IWebParameterProvider
    {
        protected override string TableName { get { return "WebParameter"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "WebParameter_Get"; } }
        protected override string DeleteProcedure { get { return "WebParameter_Del"; } }

        protected override WebParameter From(IDataReader r, WebParameter source)
        {
            WebParameter item = source ?? new WebParameter();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.Value = DataUtil.Get(r, WebColumns.Value);
            item.IsRequired = DataUtil.GetInt32(r, "IsRequired");

            return item;
        }

        public override int Update(WebParameter item)
        {
            var obj = DbHelper.ExecuteScalar("WebParameter_Set",
                DbHelper.CreateParameter("@Id", item.Id),
                DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                DbHelper.CreateParameter("@RecordId", item.RecordId),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@Value", item.Value),
                DbHelper.CreateParameter("@IsRequired", item.IsRequired));

            return UpdatePostProcess(item, obj);
        }

        #region IWebParameterProvider Members

        public IEnumerable<WebParameter> GetList(int objectId, int recordId)
        {
            List<WebParameter> items = new List<WebParameter>();

            using (var r = DbHelper.ExecuteReader(SelectProcedure,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public WebParameter Get(int objectId, int recordId, string name)
        {
            using (var r = DbHelper.ExecuteReader(SelectProcedure,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        #endregion
    }
}
