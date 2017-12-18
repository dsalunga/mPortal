using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class TreeControls : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser qs = new QueryParser(WConfig.DefaultLoginPage);
                qs.Set("Mode", "LogOff");
                linkLogOff.HRef = qs.BuildQuery();

                linkSync.HRef = QueryParser.BuildQuery(this);
            }
        }
    }
}