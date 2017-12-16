using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Forum
{
    class ForumThread : WebObjectBase
    {
        public int ForumId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public DateTime DateCreated { get; set; }

        public override int OBJECT_ID
        {
            get { throw new NotImplementedException(); }
        }
    }
}
