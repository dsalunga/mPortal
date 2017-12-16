using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.WeeklyScheduler
{
    public partial class WeeklySchedulerTask
    {
        public WeeklySchedulerTask()
        {
            WeeklySchedulerEntities e = new WeeklySchedulerEntities();

            WeeklySchedulerItem item = new WeeklySchedulerItem();
        }
    }
}
