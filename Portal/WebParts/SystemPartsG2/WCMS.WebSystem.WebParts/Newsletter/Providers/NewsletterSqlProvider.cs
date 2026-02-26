using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Newsletter.Providers
{
    public class NewsletterSqlProvider : GenericSqlDataProviderBase<NewsletterEntry>, INewsletterProvider
    {
        protected override string DeleteProcedure { get { return string.Empty; } }
        protected override string TableName { get { return "Newsletter"; } }

        protected override string IdColumn { get { return "Id"; } }


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
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("Newsletter") + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("Email") + " = @Email, " +
                    DbSyntax.QuoteIdentifier("IPAddress") + " = @IPAddress, " +
                    DbSyntax.QuoteIdentifier("SubscribeDate") + " = @SubscribeDate, " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId, " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId, " +
                    DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId, " +
                    DbSyntax.QuoteIdentifier("Gender") + " = @Gender" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Email", item.Email),
                    DbHelper.CreateParameter("@IPAddress", item.IPAddress),
                    DbHelper.CreateParameter("@SubscribeDate", item.SubscribeDate),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@Gender", item.Gender),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("Newsletter") + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("Email") + ", " +
                    DbSyntax.QuoteIdentifier("IPAddress") + ", " +
                    DbSyntax.QuoteIdentifier("SubscribeDate") + ", " +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + ", " +
                    DbSyntax.QuoteIdentifier("SiteId") + ", " +
                    DbSyntax.QuoteIdentifier("Gender") +
                    ") VALUES (@Name, @Email, @IPAddress, @SubscribeDate, @ObjectId, @RecordId, @SiteId, @Gender)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Email", item.Email),
                    DbHelper.CreateParameter("@IPAddress", item.IPAddress),
                    DbHelper.CreateParameter("@SubscribeDate", item.SubscribeDate),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@Gender", item.Gender)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public NewsletterEntry Get(int objectId, int recordId, string email)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("Newsletter") + " WHERE " +
                DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId AND " +
                DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId AND " +
                DbSyntax.QuoteIdentifier("Email") + " = @Email";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@Email", email)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }
    }
}
