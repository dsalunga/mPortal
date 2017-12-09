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
    public class WebThemeProvider : GenericSqlDataProviderBase<WebTheme>, IWebThemeProvider
    {
        protected override string SelectProcedure { get { return "WebTheme_Get"; } }
        protected override string DeleteProcedure { get { return "WebTheme_Del"; } }

        protected override WebTheme From(IDataReader r, WebTheme source)
        {
            WebTheme item = source ?? new WebTheme();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.TemplateId = DataHelper.GetId(r, WebColumns.TemplateId);
            item.ParentId = DataHelper.GetId(r, WebColumns.ParentId);
            item.Identity = DataHelper.Get(r, WebColumns.Identity);
            item.SkinId = DataHelper.GetId(r, WebColumns.SkinId);

            return item;
        }

        public override int Update(WebTheme item)
        {
            var obj = SqlHelper.ExecuteScalar("WebTheme_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@TemplateId", item.TemplateId),
                new SqlParameter("@ParentId", item.ParentId),
                new SqlParameter("@Identity", item.Identity),
                new SqlParameter("@SkinId", item.SkinId)
            );

            item.Id = DataHelper.GetId(obj);

            return item.Id;
        }
    }
}
