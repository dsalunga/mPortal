using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.WebSystem.Apps.Integration.CommonWS;

namespace WCMS.WebSystem.Apps.Integration
{
    public partial class ServiceScheduleHelper
    {
        public static IQueryable<ServiceSchedule> GetList(DateTime month, int localeId)
        {
            var client = new CommonWSSoapClient();
            var schedules = client.GetServiceSchedulesByMonth(localeId, 1, month.Month, month.Year);
            return schedules.AsQueryable();
        }

        public static IQueryable<ServiceSchedule> GetListMonthExtra(DateTime month, int localeId)
        {
            var client = new CommonWSSoapClient();
            var startDate = new DateTime(month.Year, month.Month, 1).AddDays(-10);
            var endDate = new DateTime(month.Year, month.Month, 1).AddMonths(1).AddDays(10);
            return client.GetServiceScheduleByDate(localeId, startDate, endDate).AsQueryable();
        }

        public static IQueryable<ServiceSchedule> GetListAllMonthExtra(DateTime month, int localeId)
        {
            var client = new CommonWSSoapClient();
            var startDate = new DateTime(month.Year, month.Month, 1).AddDays(-10);
            var endDate = new DateTime(month.Year, month.Month, 1).AddMonths(1).AddDays(10);
            return client.GetAllServiceScheduleByDate(localeId, startDate, endDate).AsQueryable();
        }

        public static ServiceSchedule Get(int serviceScheduleId)
        {
            var client = new CommonWSSoapClient();
            return client.GetServiceSchedule(serviceScheduleId);
            //var db = new ExternalDBEntities();
            //return db.ServiceSchedules.SingleOrDefault(i => i.ServiceScheduleID == id);
        }

        private static bool IsSameMonth(DateTime month, DateTime date)
        {
            var monthDate = month.Date;
            var dateCompare = date.Date;
            return monthDate.Year == dateCompare.Year && monthDate.Month == dateCompare.Month;
        }
    }
}
