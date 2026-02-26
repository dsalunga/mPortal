using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration
{
    public class SportsfestSqlProvider : ISportsfestProvider
    {
        #region IDataProvider<Sportsfest> Members

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier("Sportsfest") + " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id));

            return true;
        }

        public Sportsfest Get(int id)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("Sportsfest") + " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public Sportsfest Get(string name)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("Sportsfest") + " WHERE " + DbSyntax.QuoteIdentifier("Name") + " = @Name";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private Sportsfest From(IDataReader r)
        {
            Sportsfest item = new Sportsfest();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.MemberId = DataUtil.GetId(r, "MemberId");
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.GroupColor = DataUtil.Get(r, "GroupColor");
            item.Age = DataUtil.GetInt32(r, "Age");
            item.ShirtSize = DataUtil.Get(r, "ShirtSize");
            item.Mobile = DataUtil.Get(r, "Mobile");
            item.EntryDate = DataUtil.GetDateTime(r, "EntryDate");
            item.Sports = DataUtil.Get(r, "Sports");
            item.CountryCode = DataUtil.GetInt32(r, "CountryCode");
            item.Locale = DataUtil.Get(r, "Locale");
            item.Suggestion = DataUtil.Get(r, "Suggestion");

            return item;
        }

        public Sportsfest Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sportsfest> GetList()
        {
            List<Sportsfest> items = new List<Sportsfest>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("Sportsfest");
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<Sportsfest> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(Sportsfest item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("Sportsfest") + " SET " +
                    DbSyntax.QuoteIdentifier("MemberId") + " = @MemberId, " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("GroupColor") + " = @GroupColor, " +
                    DbSyntax.QuoteIdentifier("Age") + " = @Age, " +
                    DbSyntax.QuoteIdentifier("ShirtSize") + " = @ShirtSize, " +
                    DbSyntax.QuoteIdentifier("Mobile") + " = @Mobile, " +
                    DbSyntax.QuoteIdentifier("EntryDate") + " = @EntryDate, " +
                    DbSyntax.QuoteIdentifier("Sports") + " = @Sports, " +
                    DbSyntax.QuoteIdentifier("Locale") + " = @Locale, " +
                    DbSyntax.QuoteIdentifier("CountryCode") + " = @CountryCode, " +
                    DbSyntax.QuoteIdentifier("Suggestion") + " = @Suggestion" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@MemberId", item.MemberId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@GroupColor", item.GroupColor),
                    DbHelper.CreateParameter("@Age", item.Age),
                    DbHelper.CreateParameter("@ShirtSize", item.ShirtSize),
                    DbHelper.CreateParameter("@Mobile", item.Mobile),
                    DbHelper.CreateParameter("@EntryDate", item.EntryDate),
                    DbHelper.CreateParameter("@Sports", item.Sports),
                    DbHelper.CreateParameter("@Locale", item.Locale),
                    DbHelper.CreateParameter("@CountryCode", item.CountryCode),
                    DbHelper.CreateParameter("@Suggestion", item.Suggestion),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("Sportsfest") + " (" +
                    DbSyntax.QuoteIdentifier("MemberId") + ", " +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("GroupColor") + ", " +
                    DbSyntax.QuoteIdentifier("Age") + ", " +
                    DbSyntax.QuoteIdentifier("ShirtSize") + ", " +
                    DbSyntax.QuoteIdentifier("Mobile") + ", " +
                    DbSyntax.QuoteIdentifier("EntryDate") + ", " +
                    DbSyntax.QuoteIdentifier("Sports") + ", " +
                    DbSyntax.QuoteIdentifier("Locale") + ", " +
                    DbSyntax.QuoteIdentifier("CountryCode") + ", " +
                    DbSyntax.QuoteIdentifier("Suggestion") +
                    ") VALUES (@MemberId, @Name, @GroupColor, @Age, @ShirtSize, @Mobile, @EntryDate, @Sports, @Locale, @CountryCode, @Suggestion)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@MemberId", item.MemberId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@GroupColor", item.GroupColor),
                    DbHelper.CreateParameter("@Age", item.Age),
                    DbHelper.CreateParameter("@ShirtSize", item.ShirtSize),
                    DbHelper.CreateParameter("@Mobile", item.Mobile),
                    DbHelper.CreateParameter("@EntryDate", item.EntryDate),
                    DbHelper.CreateParameter("@Sports", item.Sports),
                    DbHelper.CreateParameter("@Locale", item.Locale),
                    DbHelper.CreateParameter("@CountryCode", item.CountryCode),
                    DbHelper.CreateParameter("@Suggestion", item.Suggestion)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #endregion


        public Sportsfest Refresh(Sportsfest item)
        {
            throw new NotImplementedException();
        }
    }
}
