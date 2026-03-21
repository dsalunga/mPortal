using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace WCMS.WebSystem.Apps.Integration.AttendanceWS
{
    public partial class AttendanceSoapClient
    {
        public AttendanceSoapClient(bool expect100Continue)
        {
            ServicePointManager.Expect100Continue = expect100Continue;
        }

        public static AttendanceSoapClient GetNewClientInstance()
        {
            AttendanceSoapClient client = new AttendanceSoapClient(false);
            return client;
        }
    }
}
