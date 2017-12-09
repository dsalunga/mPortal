using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCMS.Framework
{
    public partial class WApproval
    {
        public WebUser GetApprover()
        {
            if (ApproverUserId > 0)
                return WebUser.Get(ApproverUserId);
            return null;
        }

        public WebUser GetUser()
        {
            if (ObjectId == WebObjects.WebUser && RecordId > 0)
                return WebUser.Get(RecordId);
            return null;
        }
    }
}
