using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Forum
{
    class ForumPost : WebObjectBase
    {
        public int ThreadId { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime PostDate { get; set; }

        public override int OBJECT_ID
        {
            get { throw new NotImplementedException(); }
        }
    }
}
