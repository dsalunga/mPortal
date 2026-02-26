using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public class LessonReviewerVideoProvider : GenericSqlDataProviderBase<LessonReviewerVideo>
    {
        protected override string IdParameter { get { return "ServiceScheduleId"; } }

        protected override string TableName { get { return "LessonReviewerVideo"; } }


        protected override string IdColumn { get { return "ServiceScheduleId"; } }



        protected override string SelectProcedure { get { return "LessonReviewerVideo_Get"; } }
        protected override string DeleteProcedure { get { return "LessonReviewerVideo_Del"; } }

        protected override LessonReviewerVideo From(IDataReader r, LessonReviewerVideo source)
        {
            LessonReviewerVideo item = source ?? new LessonReviewerVideo();
            item.ServiceScheduleId = DataUtil.GetId(r, "ServiceScheduleId");
            item.ServiceStartDate = DataUtil.GetDateTime(r, "ServiceStartDate");
            item.Duration = DataUtil.GetInt32(r, "Duration");

            return item;
        }

        public override int Update(LessonReviewerVideo item)
        {
            var parms = new[] {
                DbHelper.CreateParameter("@ServiceScheduleId", item.ServiceScheduleId),
                DbHelper.CreateParameter("@ServiceStartDate", item.ServiceStartDate),
                DbHelper.CreateParameter("@Duration", item.Duration)
            };

            var sql = "UPDATE LessonReviewerVideo SET " +
                DbSyntax.QuoteIdentifier("ServiceStartDate") + " = @ServiceStartDate, " +
                DbSyntax.QuoteIdentifier("Duration") + " = @Duration" +
                " WHERE " + DbSyntax.QuoteIdentifier("ServiceScheduleId") + " = @ServiceScheduleId";
            var rows = DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);

            if (rows == 0)
            {
                sql = "INSERT INTO LessonReviewerVideo (" +
                    DbSyntax.QuoteIdentifier("ServiceScheduleId") + ", " +
                    DbSyntax.QuoteIdentifier("ServiceStartDate") + ", " +
                    DbSyntax.QuoteIdentifier("Duration") +
                    ") VALUES (@ServiceScheduleId, @ServiceStartDate, @Duration)";
                parms = new[] {
                    DbHelper.CreateParameter("@ServiceScheduleId", item.ServiceScheduleId),
                    DbHelper.CreateParameter("@ServiceStartDate", item.ServiceStartDate),
                    DbHelper.CreateParameter("@Duration", item.Duration)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }

            return item.ServiceScheduleId;
        }

        public List<LessonReviewerVideo> GetList(DateTime month)
        {
            List<LessonReviewerVideo> items = new List<LessonReviewerVideo>();

            var startDate = new DateTime(month.Year, month.Month, 1);
            var endDate = startDate.AddMonths(1);
            var sql = "SELECT * FROM LessonReviewerVideo WHERE " +
                DbSyntax.QuoteIdentifier("ServiceStartDate") + " >= @StartDate AND " +
                DbSyntax.QuoteIdentifier("ServiceStartDate") + " < @EndDate";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@StartDate", startDate),
                DbHelper.CreateParameter("@EndDate", endDate)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
