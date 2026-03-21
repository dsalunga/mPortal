using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebPartAdminHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser qs = new QueryParser(this);
                
                int id = qs.GetId(WebColumns.PartAdminId);
                WebPartAdmin item = null;
                if (id > 0 && (item = WebPartAdmin.Get(id)) != null)
                    lblTitle.InnerHtml = item.Name;

                // ObjectKey
                qs.Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebPartAdmin, id));
                qs.Set(ObjectKey.KeySource, qs.EncodedBasePath);

                linkConfigPage.HRef = qs.BuildQuery(CentralPages.WebPartAdminEntry);
                linkSecurity.HRef = qs.BuildQuery(CentralPages.WebSecurity);
                linkResources.HRef = qs.BuildQuery(CentralPages.WebResources);
                linkParameters.HRef = qs.BuildQuery(CentralPages.WebParameters);
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int id = query.GetId(WebColumns.PartAdminId);
            if (id > 0)
            {
                WebPartAdmin.Delete(id);
                query.Remove(WebColumns.PartAdminId);
                query.Redirect(CentralPages.WebPartAdmin);
            }
        }
    }
}