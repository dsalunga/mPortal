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
    public class WebSkinProvider : GenericSqlDataProviderBase<WebSkin>, IWebSkinProvider
    {
        protected override string SelectProcedure { get { return "WebSkin_Get"; } }
        protected override string DeleteProcedure { get { return "WebSkin_Del"; } }

        protected override WebSkin From(IDataReader r, WebSkin source)
        {
            WebSkin item = source ?? new WebSkin();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.Rank = DataHelper.GetInt32(r, WebColumns.Rank);

            return item;
        }

        public override int Update(WebSkin item)
        {
            var obj = SqlHelper.ExecuteScalar("WebSkin_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@Rank", item.Rank)
            );

            item.Id = DataHelper.GetId(obj);

            return item.Id;
        }

        #region IWebThemeProvider Members

        public IEnumerable<WebSkin> GetList(int objectId, int recordId)
        {
            List<WebSkin> items = new List<WebSkin>();

            if (objectId > -2 || recordId > -2)
            {
                using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                    new SqlParameter("@ObjectId", objectId),
                    new SqlParameter("@RecordId", recordId)
                ))
                {
                    while (r.Read())
                        items.Add(From(r));
                }
            }

            return items;
        }

        #endregion
    }
}
