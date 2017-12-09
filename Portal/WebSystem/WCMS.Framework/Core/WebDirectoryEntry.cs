using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public class WebDirectoryEntry
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public int ObjectId { get; set; }
        public string Name { get; set; }
        public string ObjectName { get; set; }
        public DateTime DateModified { get; set; }
    }
}
