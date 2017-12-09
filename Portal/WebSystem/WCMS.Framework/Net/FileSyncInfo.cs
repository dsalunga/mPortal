using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Net
{
    public class FileSyncInfo
    {
        public FileSyncInfo()
        {

        }

        public string RelativePath { get; set; }
        public DateTime DateModified { get; set; }
        public long Size { get; set; }
    }
}
