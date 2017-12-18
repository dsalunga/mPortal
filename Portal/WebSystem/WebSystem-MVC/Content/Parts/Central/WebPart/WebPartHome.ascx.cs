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
    public partial class WebPartHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new QueryParser(this);
                linkConfigure.HRef = query.BuildQuery(CentralPages.WebPart);

                // ObjectKey
                query.Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebPart, query.GetId(WebColumns.PartId)));
                query.Set(ObjectKey.KeySource, query.EncodedBasePath);

                linkSecurity.HRef = query.BuildQuery(CentralPages.WebSecurity);
                linkResources.HRef = query.BuildQuery(CentralPages.WebResources);
                linkParameters.HRef = query.BuildQuery(CentralPages.WebParameters);

            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            var query = new QueryParser(this);
            int id = query.GetId(WebColumns.PartId);
            if (id > 0)
            {
                WPart.Delete(id);
                query.Remove(WebColumns.PartId);
                query.Redirect(CentralPages.WebParts);
            }
        }
    }
}