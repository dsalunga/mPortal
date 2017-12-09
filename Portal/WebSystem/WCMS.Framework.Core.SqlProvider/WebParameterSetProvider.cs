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
    public class WebParameterSetProvider : GenericSqlDataProviderBase<WebParameterSet>, IWebParameterSetProvider
    {
        protected override string SelectProcedure { get { return "WebParameterSet_Get"; } }
        protected override string DeleteProcedure { get { return "WebParameterSet_Del"; } }

        protected override WebParameterSet From(IDataReader r, WebParameterSet source)
        {
            WebParameterSet item = source ?? new WebParameterSet();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.SchemaParameterName = DataHelper.Get(r, "SchemaParameterName");

            return item;
        }

        public override int Update(WebParameterSet item)
        {
            var obj = SqlHelper.ExecuteScalar("WebParameterSet_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@SchemaParameterName", item.SchemaParameterName)
            );

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }
    }
}
