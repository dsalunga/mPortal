using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;
using WCMS.Framework;
using System.Data.SqlClient;

namespace WCMS.WebSystem.WebParts.Newsletter.Providers
{
    public class NewsletterSqlProvider : GenericSqlDataProviderBase<NewsletterEntry>, INewsletterProvider
    {
        protected override string DeleteProcedure { get { return string.Empty; } }
        protected override string SelectProcedure { get { return "Newsletter_Get"; } }

        protected override NewsletterEntry From(IDataReader r, NewsletterEntry source)
        {
            var item = source ?? new NewsletterEntry();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.Email = DataHelper.Get(r, WebColumns.Email);
            item.IPAddress = DataHelper.Get(r, "IPAddress");
            item.SubscribeDate = DataHelper.GetDateTime(r, "SubscribeDate");
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.SiteId = DataHelper.GetId(r, WebColumns.SiteId);
            item.Gender = DataHelper.GetInt32(r, "Gender");

            return item;
        }

        public override int Update(NewsletterEntry item)
        {
            var obj = SqlHelper.ExecuteScalar("Newsletter_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Email", item.Email),
                new SqlParameter("@IPAddress", item.IPAddress),
                new SqlParameter("@SubscribeDate", item.SubscribeDate),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@SiteId", item.SiteId),
                new SqlParameter("@Gender", item.Gender)
            );

            return UpdatePostProcess(item, obj);
        }

        public NewsletterEntry Get(int objectId, int recordId, string email)
        {
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@Email", email)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }
    }
}
