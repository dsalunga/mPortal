using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.Apps.Integration.Providers;

namespace WCMS.WebSystem.Apps.Integration
{
    public class LessonReviewerVideo : IWebObject, ISelfManager
    {
        private static LessonReviewerVideoProvider _provider = new LessonReviewerVideoProvider();

        public LessonReviewerVideo()
        {
            ServiceScheduleId = -1;
            ServiceStartDate = WConstants.DateTimeMinValue;
            Duration = 0;
        }

        public int ServiceScheduleId { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public int Duration { get; set; }

        public static LessonReviewerVideoProvider Provider
        {
            get { return _provider; }
        }

        #region IWebObject Members

        public int Id
        {
            get { return ServiceScheduleId; }
            set { ServiceScheduleId = value; }
        }

        public int OBJECT_ID
        {
            get { return -1; }
        }

        #endregion

        #region ISelfManager Members

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        #endregion
    }
}
