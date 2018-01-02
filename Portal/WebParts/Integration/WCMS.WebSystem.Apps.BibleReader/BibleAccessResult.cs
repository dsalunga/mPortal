using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.WebParts.BibleReader
{
    public class BibleAccessResult
    {
        public BibleAccessResult()
        {
            Status = BibleAccessStatuses.UNINITIALIZED;
        }

        public BibleAccessResult(BibleAccess accessInfo, int status = 0)
        {
            Status = status; // Success

            this.AppAccessInfo = accessInfo;
        }

        public BibleAccess AppAccessInfo { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }
}
