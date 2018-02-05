using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

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
            SqlHelper.ExecuteNonQuery("Sportsfest_Del",
                new SqlParameter("@Id", id));

            return true;
        }

        public Sportsfest Get(int id)
        {
            using (var r = SqlHelper.ExecuteReader("Sportsfest_Get",
                new SqlParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public Sportsfest Get(string name)
        {
            using (var r = SqlHelper.ExecuteReader("Sportsfest_Get",
                new SqlParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private Sportsfest From(SqlDataReader r)
        {
            Sportsfest item = new Sportsfest();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.MemberId = DataUtil.GetId(r, "MemberId");
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.GroupColor = DataHelper.Get(r, "GroupColor");
            item.Age = DataUtil.GetInt32(r, "Age");
            item.ShirtSize = DataHelper.Get(r, "ShirtSize");
            item.Mobile = DataHelper.Get(r, "Mobile");
            item.EntryDate = DataUtil.GetDateTime(r, "EntryDate");
            item.Sports = DataHelper.Get(r, "Sports");
            item.CountryCode = DataUtil.GetInt32(r, "CountryCode");
            item.Locale = DataHelper.Get(r, "Locale");
            item.Suggestion = DataHelper.Get(r, "Suggestion");

            return item;
        }

        public Sportsfest Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sportsfest> GetList()
        {
            List<Sportsfest> items = new List<Sportsfest>();

            using (var r = SqlHelper.ExecuteReader("Sportsfest_Get"))
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
            var obj = SqlHelper.ExecuteScalar("Sportsfest_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@MemberId", item.MemberId),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@GroupColor", item.GroupColor),
                new SqlParameter("@Age", item.Age),
                new SqlParameter("@ShirtSize", item.ShirtSize),
                new SqlParameter("@Mobile", item.Mobile),
                new SqlParameter("@EntryDate", item.EntryDate),
                new SqlParameter("@Sports", item.Sports),
                new SqlParameter("@Locale", item.Locale),
                new SqlParameter("@CountryCode", item.CountryCode),
                new SqlParameter("@Suggestion", item.Suggestion)
            );

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }

        #endregion


        public Sportsfest Refresh(Sportsfest item)
        {
            throw new NotImplementedException();
        }
    }
}
