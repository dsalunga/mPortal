using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.LessonReviewer.Core
{
    public class MakeUpServiceSession
    {
        public const string SessionKey = "MakeUpService";

        public MakeUpServiceSession()
        {

        }

        public bool IntranetMode { get; set; }
        public bool LoggedIn { get; set; }
        public bool OverrideSeek { get; set; }

        public bool BypassPortal
        {
            get { return IntranetMode && !LoggedIn; }
        }
    }
}
