using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class WebGlobalPolicyHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser qs = new QueryParser(this);
                int id = qs.GetId(WebColumns.GlobalPolicyId);
                if (id > 0)
                {
                    linkProperties.HRef = qs.BuildQuery(CentralPages.WebGlobalPolicy);

                    QueryParser q = new QueryParser(qs);
                    q.Set(ObjectKey.KeyString, (new ObjectKey(WebObjects.WebGlobalPolicy, id)).ToString());
                    q.Set(ObjectKey.KeySource, qs.EncodedBasePath);

                    linkSecurity.HRef = q.BuildQuery(CentralPages.WebSecurity);
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            QueryParser qs = new QueryParser(this);
            int id = qs.GetId(WebColumns.GlobalPolicyId);
            if (id > 0)
            {
                WebGlobalPolicy.Provider.Delete(id);
                qs.Remove(WebColumns.GlobalPolicyId);
                qs.Redirect(CentralPages.WebGlobalPolicy);
            }
        }
    }
}