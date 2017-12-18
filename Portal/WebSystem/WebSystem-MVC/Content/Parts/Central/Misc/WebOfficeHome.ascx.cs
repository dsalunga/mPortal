using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Misc
{
    public partial class WebOfficeHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser qs = new QueryParser(this);
                int id = qs.GetId(WebColumns.OfficeId);
                if (id > 0)
                {
                    linkProperties.HRef = qs.BuildQuery(CentralPages.WebOffice);

                    QueryParser q = new QueryParser(qs);
                    q.Set(ObjectKey.KeyString, (new ObjectKey(WebObjects.WebOffice, id)).ToString());
                    q.Set(ObjectKey.KeySource, qs.EncodedBasePath);

                    linkParameters.HRef = q.BuildQuery(CentralPages.WebParameters);
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int id = query.GetId(WebColumns.OfficeId);
            if (id > 0)
            {
                WebOffice.Delete(id);
                query.Remove(WebColumns.OfficeId);
                query.Redirect(CentralPages.WebOffices);
            }
        }
    }
}