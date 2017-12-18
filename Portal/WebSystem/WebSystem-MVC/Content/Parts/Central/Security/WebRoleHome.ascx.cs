using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class WebRoleHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser qs = new QueryParser(this);
                int id = DataHelper.GetId(qs[WebColumns.RoleId]);
                if (id > 0)
                {
                    linkProperties.HRef = qs.BuildQuery(CentralPages.WebRole);
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int id = query.GetId(WebColumns.RoleId);
            if (id > 0)
            {
                WebRole.Delete(id);
                query.Remove(WebColumns.RoleId);
                query.Redirect(CentralPages.WebRoles);
            }
        }
    }
}