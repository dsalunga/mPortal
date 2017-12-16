using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.WebSystem.WebParts.RemoteIndexer;

namespace WCMS.WebSystem.Content.Parts.FileManager
{
    public partial class IndexerDownload : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = DataHelper.GetId(Request, "Id");
                bool force = DataHelper.GetBool(Request, "Force", false);
                RemoteLibraryHelper.InvokeDownload(id, force, 0);
            }
        }
    }
}