using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Menu.Providers
{
    public class MenuProvider : GenericSqlDataProviderBase<MenuEntity>, IMenuProvider
    {
        protected override string SelectProcedure { get { return "Menu_Get"; } }
        protected override string DeleteProcedure { get { return "Menu_Del"; } }

        protected override MenuEntity From(IDataReader r, MenuEntity source)
        {
            MenuEntity item = source ?? new MenuEntity();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.IsActive = DataHelper.GetInt32(r, "IsActive");
            item.DateCreated = DataHelper.GetDateTime(r, "DateCreated");
            item.SiteId = DataHelper.GetId(r, WebColumns.SiteId);
            item.UserId = DataHelper.GetId(r, WebColumns.UserId);
            item.PageId = DataHelper.GetId(r, WebColumns.PageId);
            item.IncludeChildren = DataHelper.GetInt32(r, "IncludeChildren");

            return item;
        }

        public override int Update(MenuEntity item)
        {
            var obj = SqlHelper.ExecuteScalar("Menu_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@IsActive", item.IsActive),
                new SqlParameter("@SiteId", item.SiteId),
                new SqlParameter("@UserId", item.UserId),
                new SqlParameter("@PageId", item.PageId),
                new SqlParameter("@IncludeChildren", item.IncludeChildren)
            );

            item.Id = DataHelper.GetId(obj);

            return item.Id;
        }
    }
}
