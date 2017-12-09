using System;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Core
{
    public class WebJob : ParameterizedWebObject, ISelfManager
    {
        private static IWebJobProvider _provider;

        static WebJob()
        {
            _provider = WebObject.ResolveProvider<WebJob, IWebJobProvider>();
        }

        public WebJob()
        {
            RecurrenceId = RecurrenceType.None;
            Weekdays = WeekdaysEnum.None;
            OccursEvery = 1;
            ExecutionStartDate = WConstants.DateTimeMinValue;
            ExecutionEndDate = WConstants.DateTimeMinValue;
            ExecutionStatus = Framework.ExecutionStatus.Pending;
            ExecutionMessage = string.Empty;
            Description = string.Empty;
        }

        [ObjectColumn]
        public int Weekdays { get; set; }

        /// <summary>
        /// Please refer to WCMS.Framework.RecurrenceType enum
        /// </summary>
        [ObjectColumn]
        public int RecurrenceId { get; set; }

        [ObjectColumn]
        public int OccursEvery { get; set; }

        [ObjectColumn]
        public DateTime ExecutionStartDate { get; set; }

        [ObjectColumn]
        public DateTime ExecutionEndDate { get; set; }

        /// <summary>
        /// Refer to WCMS.Framework.ExecutionStatus
        /// </summary>
        [ObjectColumn]
        public int ExecutionStatus { get; set; }

        /// <summary>
        /// A Warning or an Error Message
        /// </summary>
        [ObjectColumn]
        public string ExecutionMessage { get; set; }

        [ObjectColumn]
        public int Enabled { get; set; }

        [ObjectColumn]
        public string TypeName { get; set; }

        [ObjectColumn]
        public DateTime StartDate { get; set; }

        [ObjectColumn]
        public string Description { get; set; }

        public override int OBJECT_ID
        {
            get { return WebObjects.WebJob; }
        }

        public bool IsEnabled
        {
            get { return Enabled == 1; }
            set { Enabled = value ? 1 : 0; }
        }

        public static IWebJobProvider Provider
        {
            get { return _provider; }
        }

        public DateTime GetNextOccurence(DateTime refDate)
        {
            if (this.StartDate >= refDate)
                return this.StartDate;

            switch (RecurrenceId)
            {
                case RecurrenceType.None:
                    return WConstants.DateTimeMinValue;

                case RecurrenceType.Always:
                    return refDate;

                case RecurrenceType.Daily:
                    return new DateTime(refDate.Year, refDate.Month, refDate.Day).AddDays(1).Add(StartDate.TimeOfDay);

                case RecurrenceType.Weekly:
                    return DateTimeHelper.GetNextWeekdayDate(refDate, StartDate, Weekdays);

                case RecurrenceType.Monthly:
                    return new DateTime(refDate.Year, refDate.Month, 1).AddMonths(1).AddDays(StartDate.Day - 1).Add(StartDate.TimeOfDay);
            }

            return WConstants.DateTimeMinValue;
        }

        public bool IsForExecution(DateTime refDate, int jobTimerIntervalSeconds)
        {
            return IsForExecution(refDate, new TimeSpan(0, 0, jobTimerIntervalSeconds));
        }

        public bool IsForExecution(DateTime refDate, TimeSpan jobTimerInterval)
        {
            if (this.ExecutionStatus == Framework.ExecutionStatus.Running)
                return false;

            if (this.RecurrenceId == RecurrenceType.Always && this.StartDate <= refDate)
                return true;

            switch (RecurrenceId)
            {
                case RecurrenceType.None:
                    if (DateTimeHelper.IsOccurring(refDate, StartDate, jobTimerInterval))
                        return true;
                    break;

                case RecurrenceType.Daily:
                    if (DateTimeHelper.IsOccurring(refDate, refDate.Date.Add(StartDate.TimeOfDay), jobTimerInterval))
                        return true;
                    break;

                case RecurrenceType.Weekly:
                    if (DateTimeHelper.IsOccurring(refDate, DateTimeHelper.GetNextWeekdayDate(refDate, StartDate, Weekdays), jobTimerInterval))
                        return true;
                    break;

                case RecurrenceType.Monthly:
                    if (DateTimeHelper.IsOccurring(refDate, new DateTime(refDate.Year, refDate.Month, 1).AddDays(StartDate.Day - 1).Add(StartDate.TimeOfDay), jobTimerInterval))
                        return true;
                    break;
            }

            return false;
        }

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        #endregion
    }
}
