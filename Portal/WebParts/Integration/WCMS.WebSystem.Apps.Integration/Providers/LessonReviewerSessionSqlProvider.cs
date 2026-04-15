using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration
{
    public class LessonReviewerSessionSqlProvider : GenericSqlDataProviderBase<LessonReviewerSession>
    {
        protected override string TableName { get { return "LessonReviewerSession"; } }
        protected override string IdColumn { get { return "Id"; } }

        protected override LessonReviewerSession From(IDataReader r, LessonReviewerSession source)
        {
            var item = source ?? new LessonReviewerSession();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.ServiceScheduleID = DataUtil.GetInt32(r, "ServiceScheduleID");
            item.ServiceStartDate = DataUtil.GetDateTime(r, "ServiceStartDate");
            item.ServiceName = DataUtil.Get(r, "ServiceName");
            item.DateStarted = DataUtil.GetDateTime(r, "DateStarted");
            item.DateCompleted = DataUtil.GetDateTime(r, "DateCompleted");
            item.MemberId = DataUtil.GetId(r, "MemberId");
            item.AbsentReason = DataUtil.Get(r, "AbsentReason");
            item.CouncillorNotes = DataUtil.Get(r, "CouncillorNotes");
            item.CouncillorUserId = DataUtil.GetId(r, "CouncillorUserId");
            item.Status = DataUtil.GetInt32(r, "Status");
            item.DateApproved = DataUtil.GetDateTime(r, "DateApproved");
            item.AdditionalNotes = DataUtil.Get(r, "AdditionalNotes");
            item.AttendanceType = DataUtil.GetInt32(r, "AttendanceType");
            item.PageId = DataUtil.GetId(r, WebColumns.PageId);
            item.Extra = DataUtil.Get(r, "Extra");
            return item;
        }

        public List<LessonReviewerSession> GetList(int memberId = -1, int status = -1, int serviceScheduleId = -1, int pageId = WConstants.NULL_ID, int attendanceType = WConstants.NULL_ID)
        {
            var items = new List<LessonReviewerSession>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier(TableName) + " WHERE " +
                "(@MemberId = -1 OR " + DbSyntax.QuoteIdentifier("MemberId") + " = @MemberId) AND " +
                "(@Status = -1 OR " + DbSyntax.QuoteIdentifier("Status") + " = @Status) AND " +
                "(@ServiceScheduleId = -1 OR " + DbSyntax.QuoteIdentifier("ServiceScheduleID") + " = @ServiceScheduleId) AND " +
                "(@PageId = " + WConstants.NULL_ID + " OR " + DbSyntax.QuoteIdentifier("PageId") + " = @PageId) AND " +
                "(@AttendanceType = " + WConstants.NULL_ID + " OR " + DbSyntax.QuoteIdentifier("AttendanceType") + " = @AttendanceType)";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@MemberId", memberId),
                DbHelper.CreateParameter("@Status", status),
                DbHelper.CreateParameter("@ServiceScheduleId", serviceScheduleId),
                DbHelper.CreateParameter("@PageId", pageId),
                DbHelper.CreateParameter("@AttendanceType", attendanceType)))
            {
                while (r.Read())
                    items.Add(From(r));
            }
            return items;
        }

        public List<LessonReviewerSession> GetList(DateTime dateStart, DateTime dateEnd, int memberId = -1, int status = -1, int attendanceType = WConstants.NULL_ID)
        {
            var items = new List<LessonReviewerSession>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier(TableName) + " WHERE " +
                "(@MemberId = -1 OR " + DbSyntax.QuoteIdentifier("MemberId") + " = @MemberId) AND " +
                "(@Status = -1 OR " + DbSyntax.QuoteIdentifier("Status") + " = @Status) AND " +
                DbSyntax.QuoteIdentifier("DateStarted") + " >= @DateStart AND " +
                DbSyntax.QuoteIdentifier("DateStarted") + " <= @DateEnd AND " +
                "(@AttendanceType = " + WConstants.NULL_ID + " OR " + DbSyntax.QuoteIdentifier("AttendanceType") + " = @AttendanceType)";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@MemberId", memberId),
                DbHelper.CreateParameter("@Status", status),
                DbHelper.CreateParameter("@DateStart", dateStart),
                DbHelper.CreateParameter("@DateEnd", dateEnd),
                DbHelper.CreateParameter("@AttendanceType", attendanceType)))
            {
                while (r.Read())
                    items.Add(From(r));
            }
            return items;
        }

        public override int Update(LessonReviewerSession item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier(TableName) + " SET " +
                    DbSyntax.QuoteIdentifier("ServiceScheduleID") + " = @ServiceScheduleID, " +
                    DbSyntax.QuoteIdentifier("ServiceStartDate") + " = @ServiceStartDate, " +
                    DbSyntax.QuoteIdentifier("ServiceName") + " = @ServiceName, " +
                    DbSyntax.QuoteIdentifier("DateStarted") + " = @DateStarted, " +
                    DbSyntax.QuoteIdentifier("DateCompleted") + " = @DateCompleted, " +
                    DbSyntax.QuoteIdentifier("MemberId") + " = @MemberId, " +
                    DbSyntax.QuoteIdentifier("AbsentReason") + " = @AbsentReason, " +
                    DbSyntax.QuoteIdentifier("CouncillorNotes") + " = @CouncillorNotes, " +
                    DbSyntax.QuoteIdentifier("CouncillorUserId") + " = @CouncillorUserId, " +
                    DbSyntax.QuoteIdentifier("Status") + " = @Status, " +
                    DbSyntax.QuoteIdentifier("DateApproved") + " = @DateApproved, " +
                    DbSyntax.QuoteIdentifier("AdditionalNotes") + " = @AdditionalNotes, " +
                    DbSyntax.QuoteIdentifier("AttendanceType") + " = @AttendanceType, " +
                    DbSyntax.QuoteIdentifier("PageId") + " = @PageId, " +
                    DbSyntax.QuoteIdentifier("Extra") + " = @Extra" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@ServiceScheduleID", item.ServiceScheduleID),
                    DbHelper.CreateParameter("@ServiceStartDate", item.ServiceStartDate),
                    DbHelper.CreateParameter("@ServiceName", item.ServiceName),
                    DbHelper.CreateParameter("@DateStarted", item.DateStarted),
                    DbHelper.CreateParameter("@DateCompleted", item.DateCompleted),
                    DbHelper.CreateParameter("@MemberId", item.MemberId),
                    DbHelper.CreateParameter("@AbsentReason", item.AbsentReason),
                    DbHelper.CreateParameter("@CouncillorNotes", item.CouncillorNotes),
                    DbHelper.CreateParameter("@CouncillorUserId", item.CouncillorUserId),
                    DbHelper.CreateParameter("@Status", item.Status),
                    DbHelper.CreateParameter("@DateApproved", item.DateApproved),
                    DbHelper.CreateParameter("@AdditionalNotes", item.AdditionalNotes),
                    DbHelper.CreateParameter("@AttendanceType", item.AttendanceType),
                    DbHelper.CreateParameter("@PageId", item.PageId),
                    DbHelper.CreateParameter("@Extra", item.Extra),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier(TableName) + " (" +
                    DbSyntax.QuoteIdentifier("ServiceScheduleID") + ", " +
                    DbSyntax.QuoteIdentifier("ServiceStartDate") + ", " +
                    DbSyntax.QuoteIdentifier("ServiceName") + ", " +
                    DbSyntax.QuoteIdentifier("DateStarted") + ", " +
                    DbSyntax.QuoteIdentifier("DateCompleted") + ", " +
                    DbSyntax.QuoteIdentifier("MemberId") + ", " +
                    DbSyntax.QuoteIdentifier("AbsentReason") + ", " +
                    DbSyntax.QuoteIdentifier("CouncillorNotes") + ", " +
                    DbSyntax.QuoteIdentifier("CouncillorUserId") + ", " +
                    DbSyntax.QuoteIdentifier("Status") + ", " +
                    DbSyntax.QuoteIdentifier("DateApproved") + ", " +
                    DbSyntax.QuoteIdentifier("AdditionalNotes") + ", " +
                    DbSyntax.QuoteIdentifier("AttendanceType") + ", " +
                    DbSyntax.QuoteIdentifier("PageId") + ", " +
                    DbSyntax.QuoteIdentifier("Extra") +
                    ") VALUES (@ServiceScheduleID, @ServiceStartDate, @ServiceName, @DateStarted, @DateCompleted, @MemberId, @AbsentReason, @CouncillorNotes, @CouncillorUserId, @Status, @DateApproved, @AdditionalNotes, @AttendanceType, @PageId, @Extra)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@ServiceScheduleID", item.ServiceScheduleID),
                    DbHelper.CreateParameter("@ServiceStartDate", item.ServiceStartDate),
                    DbHelper.CreateParameter("@ServiceName", item.ServiceName),
                    DbHelper.CreateParameter("@DateStarted", item.DateStarted),
                    DbHelper.CreateParameter("@DateCompleted", item.DateCompleted),
                    DbHelper.CreateParameter("@MemberId", item.MemberId),
                    DbHelper.CreateParameter("@AbsentReason", item.AbsentReason),
                    DbHelper.CreateParameter("@CouncillorNotes", item.CouncillorNotes),
                    DbHelper.CreateParameter("@CouncillorUserId", item.CouncillorUserId),
                    DbHelper.CreateParameter("@Status", item.Status),
                    DbHelper.CreateParameter("@DateApproved", item.DateApproved),
                    DbHelper.CreateParameter("@AdditionalNotes", item.AdditionalNotes),
                    DbHelper.CreateParameter("@AttendanceType", item.AttendanceType),
                    DbHelper.CreateParameter("@PageId", item.PageId),
                    DbHelper.CreateParameter("@Extra", item.Extra)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }
    }
}
