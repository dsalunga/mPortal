using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;



namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public class MemberVisitSqlProvider : IMemberVisitProvider
    {
        #region IODKVisitProvider Members

        public IEnumerable<MemberVisit> GetList(int groupId)
        {
            List<MemberVisit> items = new List<MemberVisit>();

            var sql = "SELECT * FROM ODKVisit WHERE " + DbSyntax.QuoteIdentifier("GroupId") + " = @GroupId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@GroupId", groupId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<MemberVisit> GetList(int groupId = -1, int userId = -2, string tag = null)
        {
            if (groupId <= 0 && groupId != -1)
                groupId = -1;

            if (userId <= 0 && userId != -2)
                userId = -2;

            if (tag == string.Empty)
                tag = null;

            List<MemberVisit> items = new List<MemberVisit>();

            var sql = "SELECT * FROM ODKVisit WHERE " +
                "(@GroupId = -1 OR " + DbSyntax.QuoteIdentifier("GroupId") + " = @GroupId) AND " +
                "(@UserId = -2 OR " + DbSyntax.QuoteIdentifier("CreatedUserId") + " = @UserId) AND " +
                "(@Tag IS NULL OR " + DbSyntax.QuoteIdentifier("Tags") + " = @Tag)";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@GroupId", groupId),
                DbHelper.CreateParameter("@UserId", userId),
                DbHelper.CreateParameter("@Tag", (object)tag ?? DBNull.Value)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<MemberVisit> GetListByUserId(int userId = -2)
        {
            List<MemberVisit> items = new List<MemberVisit>();

            var sql = "SELECT * FROM ODKVisit WHERE " +
                "(@UserId = -2 OR " + DbSyntax.QuoteIdentifier("CreatedUserId") + " = @UserId)";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@UserId", userId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        private MemberVisit From(IDataReader r)
        {
            MemberVisit item = new MemberVisit();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.CreatedUserId = DataUtil.GetId(r, "CreatedUserId");
            item.DateCreated = DataUtil.GetDateTime(r, "DateCreated");
            item.ActualReport = DataUtil.Get(r, "ActualReport");
            item.Status = DataUtil.Get(r, "Status");
            item.GroupId = DataUtil.GetId(r, WebColumns.GroupId);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.VisitedUserId = DataUtil.GetId(r, "VisitedUserId");
            item.DateVisited = DataUtil.GetDateTime(r, "DateVisited");
            item.ActionTaken = DataUtil.Get(r, "ActionTaken");
            item.ContactNo = DataUtil.Get(r, "ContactNo");
            item.TimesVisited = DataUtil.GetInt32(r, "TimesVisited");
            item.Address = DataUtil.Get(r, "Address");
            item.MembershipDate = DataUtil.GetDateTime(r, "MembershipDate");
            item.Tags = DataUtil.Get(r, "Tags");

            return item;
        }

        #endregion

        #region IDataProvider<ODKVisit> Members

        public bool Delete(int id)
        {
            var sql = "DELETE FROM ODKVisit WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id));

            return true;
        }

        public MemberVisit Get(int id)
        {
            var sql = "SELECT * FROM ODKVisit WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public MemberVisit Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MemberVisit> GetList()
        {
            List<MemberVisit> items = new List<MemberVisit>();

            var sql = "SELECT * FROM ODKVisit";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<MemberVisit> GetListByTag(string tag)
        {
            List<MemberVisit> items = new List<MemberVisit>();

            var sql = "SELECT * FROM ODKVisit WHERE " + DbSyntax.QuoteIdentifier("Tags") + " = @Tag";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Tag", tag)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<MemberVisit> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(MemberVisit item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE ODKVisit SET " +
                    DbSyntax.QuoteIdentifier("CreatedUserId") + " = @CreatedUserId, " +
                    DbSyntax.QuoteIdentifier("DateCreated") + " = @DateCreated, " +
                    DbSyntax.QuoteIdentifier("ActualReport") + " = @ActualReport, " +
                    DbSyntax.QuoteIdentifier("Status") + " = @Status, " +
                    DbSyntax.QuoteIdentifier("GroupId") + " = @GroupId, " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("VisitedUserId") + " = @VisitedUserId, " +
                    DbSyntax.QuoteIdentifier("DateVisited") + " = @DateVisited, " +
                    DbSyntax.QuoteIdentifier("ActionTaken") + " = @ActionTaken, " +
                    DbSyntax.QuoteIdentifier("ContactNo") + " = @ContactNo, " +
                    DbSyntax.QuoteIdentifier("TimesVisited") + " = @TimesVisited, " +
                    DbSyntax.QuoteIdentifier("Address") + " = @Address, " +
                    DbSyntax.QuoteIdentifier("MembershipDate") + " = @MembershipDate, " +
                    DbSyntax.QuoteIdentifier("Tags") + " = @Tags" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@CreatedUserId", item.CreatedUserId),
                    DbHelper.CreateParameter("@DateCreated", item.DateCreated),
                    DbHelper.CreateParameter("@ActualReport", item.ActualReport),
                    DbHelper.CreateParameter("@Status", item.Status),
                    DbHelper.CreateParameter("@GroupId", item.GroupId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@VisitedUserId", item.VisitedUserId),
                    DbHelper.CreateParameter("@DateVisited", item.DateVisited),
                    DbHelper.CreateParameter("@ActionTaken", item.ActionTaken),
                    DbHelper.CreateParameter("@ContactNo", item.ContactNo),
                    DbHelper.CreateParameter("@TimesVisited", item.TimesVisited),
                    DbHelper.CreateParameter("@Address", item.Address),
                    DbHelper.CreateParameter("@MembershipDate", item.MembershipDate),
                    DbHelper.CreateParameter("@Tags", item.Tags),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO ODKVisit (" +
                    DbSyntax.QuoteIdentifier("CreatedUserId") + ", " +
                    DbSyntax.QuoteIdentifier("DateCreated") + ", " +
                    DbSyntax.QuoteIdentifier("ActualReport") + ", " +
                    DbSyntax.QuoteIdentifier("Status") + ", " +
                    DbSyntax.QuoteIdentifier("GroupId") + ", " +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("VisitedUserId") + ", " +
                    DbSyntax.QuoteIdentifier("DateVisited") + ", " +
                    DbSyntax.QuoteIdentifier("ActionTaken") + ", " +
                    DbSyntax.QuoteIdentifier("ContactNo") + ", " +
                    DbSyntax.QuoteIdentifier("TimesVisited") + ", " +
                    DbSyntax.QuoteIdentifier("Address") + ", " +
                    DbSyntax.QuoteIdentifier("MembershipDate") + ", " +
                    DbSyntax.QuoteIdentifier("Tags") +
                    ") VALUES (@CreatedUserId, @DateCreated, @ActualReport, @Status, @GroupId, @Name, @VisitedUserId, @DateVisited, @ActionTaken, @ContactNo, @TimesVisited, @Address, @MembershipDate, @Tags)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@CreatedUserId", item.CreatedUserId),
                    DbHelper.CreateParameter("@DateCreated", item.DateCreated),
                    DbHelper.CreateParameter("@ActualReport", item.ActualReport),
                    DbHelper.CreateParameter("@Status", item.Status),
                    DbHelper.CreateParameter("@GroupId", item.GroupId),
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@VisitedUserId", item.VisitedUserId),
                    DbHelper.CreateParameter("@DateVisited", item.DateVisited),
                    DbHelper.CreateParameter("@ActionTaken", item.ActionTaken),
                    DbHelper.CreateParameter("@ContactNo", item.ContactNo),
                    DbHelper.CreateParameter("@TimesVisited", item.TimesVisited),
                    DbHelper.CreateParameter("@Address", item.Address),
                    DbHelper.CreateParameter("@MembershipDate", item.MembershipDate),
                    DbHelper.CreateParameter("@Tags", item.Tags)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public MemberVisit Refresh(MemberVisit item)
        {
            throw new NotImplementedException();
        }
    }
}
