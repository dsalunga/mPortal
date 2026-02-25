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
    public class WebThemeProvider : GenericSqlDataProviderBase<WebTheme>, IWebThemeProvider
    {
        protected override string TableName { get { return "WebTheme"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "WebTheme_Get"; } }
        protected override string DeleteProcedure { get { return "WebTheme_Del"; } }

        protected override WebTheme From(IDataReader r, WebTheme source)
        {
            WebTheme item = source ?? new WebTheme();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.TemplateId = DataUtil.GetId(r, WebColumns.TemplateId);
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);
            item.Identity = DataUtil.Get(r, WebColumns.Identity);
            item.SkinId = DataUtil.GetId(r, WebColumns.SkinId);

            return item;
        }

        public override int Update(WebTheme item)
        {
            var obj = DbHelper.ExecuteScalar("WebTheme_Set",
                DbHelper.CreateParameter("@Id", item.Id),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@TemplateId", item.TemplateId),
                DbHelper.CreateParameter("@ParentId", item.ParentId),
                DbHelper.CreateParameter("@Identity", item.Identity),
                DbHelper.CreateParameter("@SkinId", item.SkinId)
            );

            item.Id = DataUtil.GetId(obj);

            return item.Id;
        }
    }
}
