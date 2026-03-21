using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Forum
{
    public class ForumCategory : WebObjectBase
    {
        public string Title { get; set; }

        public override int OBJECT_ID
        {
            get { throw new NotImplementedException(); }
        }
    }
}
