using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.WebParts.BibleReader
{
    public class BibleVersionAccessResult
    {
        public BibleVersionAccessResult()
        {
            Status = BibleAccessStatuses.UNINITIALIZED;
        }

        public BibleVersionAccessResult(BibleAccess appAccessInfo, BibleVersionAccess accessInfo, int status)
        {
            Status = status; // Success
            VersionAccessInfo = accessInfo;
            this.AppAccessInfo = appAccessInfo;
        }

        //public BibleReaderVersionAccessResult(BibleAccess appAccessInfo, List<BibleReaderVersionAccess> accessList, int status)
        //{
        //    Status = status; // Success
        //    AccessList = accessList;
        //    this.AppAccessInfo = appAccessInfo;
        //}

        public BibleAccess AppAccessInfo { get; set; }
        public BibleVersionAccess VersionAccessInfo { get; set; }
        //public List<BibleReaderVersionAccess> AccessList { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }
}
