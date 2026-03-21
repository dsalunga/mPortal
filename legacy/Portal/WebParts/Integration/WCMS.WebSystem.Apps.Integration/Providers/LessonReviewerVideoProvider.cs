using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public class LessonReviewerVideoProvider : GenericSqlDataProviderBase<LessonReviewerVideo>
    {
        protected override string IdParameter { get { return "ServiceScheduleId"; } }

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
            var obj = SqlHelper.ExecuteNonQuery("LessonReviewerVideo_Set",
                new SqlParameter("@ServiceScheduleId", item.ServiceScheduleId),
                new SqlParameter("@ServiceStartDate", item.ServiceStartDate),
                new SqlParameter("@Duration", item.Duration));

            return item.ServiceScheduleId;
        }

        public List<LessonReviewerVideo> GetList(DateTime month)
        {
            List<LessonReviewerVideo> items = new List<LessonReviewerVideo>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@Month", month)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
