using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Menu.Providers
{
    public class MenuObjectProvider : GenericSqlDataProviderBase<MenuObject>, IMenuObjectProvider
    {
        protected override string SelectProcedure { get { return "MenuObject_Get"; } }
        protected override string DeleteProcedure { get { return "MenuObject_Del"; } }

        public MenuObject Get(int objectId, int recordId)
        {
            using (var r = SqlHelper.ExecuteReader("MenuObject_Get",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        protected override MenuObject From(IDataReader r, MenuObject source)
        {
            MenuObject item = source ?? new MenuObject();

            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.Width = DataHelper.GetInt32(r, "Width");
            item.Height = DataHelper.GetInt32(r, "Height");
            item.Horizontal = DataHelper.GetInt32(r, "Horizontal");
            item.MenuId = DataHelper.GetId(r, "MenuId");
            item.ParameterSetId = DataHelper.GetId(r, "ParameterSetId");
            item.RenderMode = DataHelper.GetInt32(r, "RenderMode");

            return item;
        }

        public override int Update(MenuObject item)
        {
            object obj = SqlHelper.ExecuteScalar("MenuObject_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@Width", item.Width),
                new SqlParameter("@Height", item.Height),
                new SqlParameter("@Horizontal", item.Horizontal),
                new SqlParameter("@MenuId", item.MenuId),
                new SqlParameter("@ParameterSetId", item.ParameterSetId),
                new SqlParameter("@RenderMode", item.RenderMode)
            );

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }
    }
}
