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
    public partial class WebPartControlTemplateHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new QueryParser(this);
                linkConfigure.HRef = query.BuildQuery(CentralPages.WebPartControlTemplate);

                var id = query.GetId(WebColumns.PartControlTemplateId);

                // ObjectKey
                var key = new ObjectKey(WebObjects.WebPartControlTemplate, id);
                query.Set(ObjectKey.KeyString, key);
                query.Set(ObjectKey.KeySource, query.EncodedBasePath);

                linkParameters.HRef = query.BuildQuery(CentralPages.WebParameters);
                linkSecurity.HRef = query.BuildQuery(CentralPages.WebSecurity);
                linkResources.HRef = query.BuildQuery(CentralPages.WebResources);

                //var template = WebPartControlTemplate.Get(id);
                //if (template != null)
                //    lblTemplate.InnerHtml = template.Name;
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            var query = new QueryParser(this);
            int id = query.GetId(WebColumns.PartControlTemplateId);
            if (id > 0)
            {
                WebPartControlTemplate.Delete(id);
                query.Remove(WebColumns.PartControlTemplateId);
                query.Redirect(CentralPages.WebPartControlTemplates);
            }
        }
    }
}