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
    public partial class WebParameterSetHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new WQuery(this);
                int id = query.GetId(WebColumns.ParameterSetId);
                if (id > 0)
                {
                    linkProperties.HRef = query.BuildQuery(CentralPages.WebParameterSet);

                    var q = new WQuery(query);
                    q.Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebParameterSet, id));
                    q.Set(ObjectKey.KeySource, query.EncodedBasePath);

                    linkParameters.HRef = q.BuildQuery(CentralPages.WebParameters);

                    //var item = WebParameterSet.Provider.Get(id);
                    //if (item != null)
                    //{
                    //    tdHeader.InnerHtml = item.Name;
                    //}
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int id = query.GetId(WebColumns.ParameterSetId);
            if (id > 0)
            {
                WebParameterSet.Provider.Delete(id);
                query.Remove(WebColumns.ParameterSetId);
                query.Redirect(CentralPages.WebParameterSets);
            }
        }
    }
}