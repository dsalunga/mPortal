using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebPartControlHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new WQuery(this);
                int id = query.GetId(WebColumns.PartControlId);
                linkConfigure.HRef = query.BuildQuery(CentralPages.WebPartControl);

                // ObjectKey
                query[ObjectKey.KeyString] = (new ObjectKey(WebObjects.WebPartControl, id)).ToString();
                query[ObjectKey.KeySource] = query.EncodedBasePath;

                linkSecurity.HRef = query.BuildQuery(CentralPages.WebSecurity);
                linkResources.HRef = query.BuildQuery(CentralPages.WebResources);
                linkParameters.HRef = query.BuildQuery(CentralPages.WebParameters);
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            int id = query.GetId(WebColumns.PartControlId);
            if (id > 0)
            {
                WebPartControl.Delete(id);
                query.Remove(WebColumns.PartControlId);
                query.Redirect(CentralPages.WebPartControls);
            }
        }
    }
}