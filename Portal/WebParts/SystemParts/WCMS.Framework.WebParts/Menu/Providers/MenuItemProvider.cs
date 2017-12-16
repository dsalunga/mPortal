using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;


namespace WCMS.WebSystem.WebParts.Menu.Providers
{
    public class MenuItemProvider : GenericSqlDataProviderBase<MenuItem>, IMenuItemProvider
    {
        #region IDataProvider<MenuItem> Members

        protected override MenuItem From(IDataReader r, MenuItem source)
        {
            MenuItem item = source ?? new MenuItem();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.NavigateUrl = r["NavigateUrl"].ToString();
            item.Text = r["Text"].ToString();
            item.Target = DataHelper.Get(r, "Target");
            item.ParentId = DataHelper.GetId(r, WebColumns.ParentId);
            item.MenuId = DataHelper.GetId(r, "MenuId");
            item.Active = DataHelper.GetInt32(r, "IsActive");
            item.Rank = DataHelper.GetInt32(r, WebColumns.Rank);
            item.PageId = DataHelper.GetId(r, WebColumns.PageId);
            item.Type = DataHelper.GetInt32(r, "Type");
            item.CheckPermission = DataHelper.GetInt32(r, "CheckPermission");

            return item;
        }

        public IEnumerable<MenuItem> GetList(int menuId = -2, int parentId = -2, int pageId = -2)
        {
            List<MenuItem> items = new List<MenuItem>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@MenuId", menuId),
                new SqlParameter("@ParentId", parentId),
                new SqlParameter("@PageId", pageId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public override int Update(MenuItem item)
        {
            var o = SqlHelper.ExecuteScalar("MenuItem_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@MenuId", item.MenuId),
                new SqlParameter("@ParentId", item.ParentId),
                new SqlParameter("@Text", item.Text),
                new SqlParameter("@NavigateURL", item.NavigateUrl),
                new SqlParameter("@Target", item.Target),
                new SqlParameter("@IsActive", item.Active),
                new SqlParameter("@Rank", item.Rank),
                new SqlParameter("@PageId", item.PageId),
                new SqlParameter("@Type", item.Type),
                new SqlParameter("@CheckPermission", item.CheckPermission)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        #endregion

        protected override string SelectProcedure { get { return "MenuItem_Get"; } }
        protected override string DeleteProcedure { get { return "MenuItem_Del"; } }
    }
}
