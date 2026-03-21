using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class ImportExportHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new WQuery(this);
                query.SetCmd("Tools/ImportExportPage");

                linkSites.HRef = query.BuildQuery();
            }
        }
    }
}
