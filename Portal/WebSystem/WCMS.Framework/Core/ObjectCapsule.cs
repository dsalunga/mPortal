using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core.Interfaces;

namespace WCMS.Framework.Core
{
    class ObjectCapsule
    {
        public int UniqueId { get; set; }

        public int ObjectId { get; set; }

        public int RecordId { get; set; }

        public IWebObject Object { get { return null; } }

        public IWebObject Record { get { return null; } }
    }
}
