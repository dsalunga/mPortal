using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WebCategory : NamedWebObject
    {
        public WebCategory()
        {
            ObjectId = -1;
            ParentId = -1;
            Rank = 0;
        }

        public int ObjectId { get; set; }
        public int Rank { get; set; }
        public int ParentId { get; set; }

        public override int OBJECT_ID
        {
            get { return -1; }
        }
    }
}
