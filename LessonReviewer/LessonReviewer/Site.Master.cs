using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

namespace WCMS.LessonReviewer
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool intranet = DataUtil.GetBool(Request, "Intranet", false);
                if (intranet)
                    NavigationMenu.Items[0].NavigateUrl = "~/Default.aspx?Intranet=true";
            }
        }
    }
}
