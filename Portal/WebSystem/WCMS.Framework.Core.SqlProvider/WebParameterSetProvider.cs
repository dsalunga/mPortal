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
    public class WebParameterSetProvider : GenericSqlDataProviderBase<WebParameterSet>, IWebParameterSetProvider
    {
        protected override string SelectProcedure { get { return "WebParameterSet_Get"; } }
        protected override string DeleteProcedure { get { return "WebParameterSet_Del"; } }

        protected override WebParameterSet From(IDataReader r, WebParameterSet source)
        {
            WebParameterSet item = source ?? new WebParameterSet();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.SchemaParameterName = DataUtil.Get(r, "SchemaParameterName");

            return item;
        }

        public override int Update(WebParameterSet item)
        {
            var obj = DbHelper.ExecuteScalar("WebParameterSet_Set",
                DbHelper.CreateParameter("@Id", item.Id),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@SchemaParameterName", item.SchemaParameterName)
            );

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }
    }
}
