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
    public partial class WebTemplateHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new WQuery(this);
                var id = query.GetId(WebColumns.TemplateId);
                query.Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebTemplate, id));
                query.Set(ObjectKey.KeySource, query.EncodedBasePath);

                linkEdit.HRef = query.BuildQuery(CentralPages.WebTemplate);
                linkEditContent.HRef = query.BuildQuery(CentralPages.WebTemplateEditor);
                linkResources.HRef = query.BuildQuery(CentralPages.WebResources);
                linkParameters.HRef = query.BuildQuery(CentralPages.WebParameters);

                WebTemplate item = null;
                if (id > 0 && (item = WebTemplate.Get(id)) != null)
                {
                    var panel = item.PrimaryPanel;
                    var skin = item.Skin;
                    var theme = item.Theme;

                    lblName.InnerHtml = item.Name;
                    lblPath.InnerHtml = item.GetRelativePath();
                    lblTheme.InnerHtml = theme != null ? theme.Name : string.Empty;
                    lblDefaultPanel.InnerHtml = panel != null ? panel.Name : string.Empty;
                    lblDefaultSkin.InnerHtml = skin != null ? skin.Name : string.Empty;
                    lblStandalone.InnerHtml = DataHelper.ToString(item.IsStandalone, BoolStrings.YesNo);

                    WebTemplate parent = null;
                    if (item.ParentId > 0 && (parent = item.Parent) != null)
                    {
                        rowParent.Visible = true;
                        lblParent.InnerHtml = parent.Name;
                    }
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int id = query.GetId(WebColumns.TemplateId);
            if (id > 0)
            {
                WebTemplate.Delete(id);
                query.Remove(WebColumns.TemplateId);
                query.Redirect(CentralPages.WebTemplates);
            }
        }
    }
}