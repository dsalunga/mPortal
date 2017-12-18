using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Template
{
    public partial class WebSkinHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser query = new QueryParser(this);
                linkEdit.HRef = query.BuildQuery(CentralPages.WebSkin);

                query.Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebSkin, query.GetId(WebColumns.SkinId)));
                query.Set(ObjectKey.KeySource, query.EncodedBasePath);

                linkResources.HRef = query.BuildQuery(CentralPages.WebResources);
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int id = query.GetId(WebColumns.SkinId);
            if (id > 0)
            {
                WebSkin.Provider.Delete(id);
                query.Remove(WebColumns.SkinId);
                query.Redirect(CentralPages.WebSkins);
            }
        }
    }
}