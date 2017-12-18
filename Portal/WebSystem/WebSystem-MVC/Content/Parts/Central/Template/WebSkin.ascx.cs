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
    public partial class WebSkinController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WContext context = new WContext(this);

                var themeId = context.GetId(WebColumns.ThemeId);

                cboTemplates.DataSource = WebTemplate.Provider.GetList(themeId);
                cboTemplates.DataBind();

                WebSkin item = null;
                var id = context.GetId(WebColumns.SkinId);
                if (id > 0 && (item = WebSkin.Provider.Get(id)) != null)
                {
                    txtName.Text = item.Name;
                    txtRank.Text = item.Rank.ToString();

                    if (item.RecordId > 0 && item.ObjectId == WebObjects.WebTemplate)
                    {
                        if (cboTemplates.Items.FindByValue(item.RecordId.ToString()) != null)
                            cboTemplates.SelectedValue = item.RecordId.ToString();

                        var template = WebTemplate.Get(item.RecordId);
                        if (template != null && template.SkinId == item.Id)
                            chkSetDefault.Checked = true;
                    }
                }
                else
                {
                    var templateId = context.GetId(WebColumns.TemplateId);
                    if (templateId > 0)
                        if (cboTemplates.Items.FindByValue(templateId.ToString()) != null)
                            cboTemplates.SelectedValue = templateId.ToString();
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ReturnPage();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var id = DataHelper.GetId(Request, WebColumns.SkinId);
            var item = id > 0 ? WebSkin.Provider.Get(id) : new WebSkin();

            item.Name = txtName.Text.Trim();
            item.ObjectId = WebObjects.WebTemplate;
            item.RecordId = DataHelper.GetId(cboTemplates.SelectedValue);
            item.Rank = DataHelper.GetInt32(txtRank.Text.Trim());
            item.Update();

            if (chkSetDefault.Checked)
            {
                var template = WebTemplate.Get(item.RecordId);
                if (template != null && template.SkinId != item.Id)
                {
                    template.SkinId = item.Id;
                    template.Update();
                }
            }

            this.ReturnPage(item.Id);
        }

        private void ReturnPage(int id = -1)
        {
            QueryParser query = new QueryParser(this);

            if (id <= 0)
                id = query.GetId(WebColumns.SkinId);

            if (id > 0)
            {
                query.Set(WebColumns.SkinId, id);
                query.Redirect(CentralPages.WebSkinHome);
            }
            else
            {
                query.Redirect(CentralPages.WebSkins);
            }
        }
    }
}