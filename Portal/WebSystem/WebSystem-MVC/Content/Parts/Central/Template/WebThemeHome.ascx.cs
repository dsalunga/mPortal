using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Template
{
    public partial class WebThemeHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new WQuery(this);

                var id = query.GetId(WebColumns.ThemeId);
                
                linkEdit.HRef = query.BuildQuery(CentralPages.WebTheme);

                query.Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebTheme, id));
                query.Set(ObjectKey.KeySource, query.EncodedBasePath);

                linkResources.HRef = query.BuildQuery(CentralPages.WebResources);

                WebTheme item = null;
                if (id > 0 && (item = WebTheme.Provider.Get(id)) != null)
                {
                    var template = item.Template;
                    var skin = item.Skin;
                    var parent = item.Parent;

                    lblName.InnerHtml = item.Name;
                    lblIdentity.InnerHtml = item.Identity;
                    lblDefaultTemplate.InnerHtml = template != null ? template.Name : string.Empty;
                    lblDefaultSkin.InnerHtml = skin != null ? skin.Name : string.Empty;
                    lblParent.InnerHtml = parent != null ? parent.Name : string.Empty;
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            int id = query.GetId(WebColumns.ThemeId);
            if (id > 0)
            {
                WebTheme.Provider.Delete(id);
                query.Remove(WebColumns.ThemeId);
                query.Redirect(CentralPages.WebThemes);
            }
        }
    }
}