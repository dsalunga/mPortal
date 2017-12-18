using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class WebGroupHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser query = new QueryParser(this);
                int id = query.GetId(WebColumns.GroupId);
                if (id > 0)
                {
                    linkProperties.HRef = query.BuildQuery(CentralPages.WebGroup);

                    QueryParser q = new QueryParser(query);
                    q.Set(ObjectKey.KeyString, (new ObjectKey(WebObjects.WebGroup, id)).ToString());
                    q.Set(ObjectKey.KeySource, query.EncodedBasePath);

                    linkParameters.HRef = q.BuildQuery(CentralPages.WebParameters);

                    var item = WebGroup.Get(id);
                    if (item != null)
                    {
                        var owner = item.Owner;
                        var parent = item.Parent;

                        lblName.InnerHtml = item.Name;
                        lblDescription.InnerHtml = item.Description;
                        lblOwner.InnerHtml = owner != null ? owner.FirstAndLastName : string.Empty;
                        lblParent.InnerHtml = parent != null ? parent.Name : string.Empty;
                        lblPageUrl.InnerHtml = item.EvalPageUrl;
                        lblRequireApproval.InnerHtml = item.RequireApproval ? "Yes" : "No";
                    }
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            QueryParser qs = new QueryParser(this);
            int id = qs.GetId(WebColumns.GroupId);
            if (id > 0)
            {
                WebGroup.Delete(id);
                qs.Remove(WebColumns.GroupId);
                qs.Redirect(CentralPages.WebGroups);
            }
        }
    }
}