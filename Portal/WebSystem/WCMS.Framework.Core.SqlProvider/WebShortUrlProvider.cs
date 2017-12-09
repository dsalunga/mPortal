using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebShortUrlProvider : GenericSqlDataProviderBase<WebShortUrl>, IWebShortUrlProvider
    {
        protected override string SelectProcedure
        {
            get { return "WebShortUrl_Get"; }
        }

        protected override string DeleteProcedure
        {
            get { return "WebShortUrl_Del"; }
        }

        protected override WebShortUrl From(IDataReader r, WebShortUrl source)
        {
            var item = source ?? new WebShortUrl();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.PageId = DataHelper.GetId(r, WebColumns.PageId);
            item.PageUrl = DataHelper.Get(r, "PageUrl");

            return item;
        }

        public override int Update(WebShortUrl item)
        {
            var obj = SqlHelper.ExecuteScalar("WebShortUrl_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@PageId", item.PageId),
                new SqlParameter("@PageUrl", item.PageUrl)
            );

            return UpdatePostProcess(item, obj);
        }

        public WebShortUrl Get(string name)
        {
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public WebShortUrl GetByPageId(int pageId)
        {
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@PageId", pageId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }
    }
}
