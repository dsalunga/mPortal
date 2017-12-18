using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Template
{
    public partial class WebTemplateController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int templateId = DataHelper.GetId(Request, WebColumns.TemplateId);
                var themeId = DataHelper.GetId(Request, WebColumns.ThemeId);

                cboSkins.DataSource = WebSkin.Provider.GetList(WebObjects.WebTemplate, templateId);
                cboSkins.DataBind();

                cboTheme.DataSource = WebTheme.Provider.GetList();
                cboTheme.DataBind();

                WebTemplate item = templateId > 0 ? WebTemplate.Get(templateId) : null;

                var templates = WebTemplate.Provider.GetList(themeId)
                    .Where(i => (item == null || i.Id != item.Id));

                cboParent.DataSource = templates;
                cboParent.DataBind();


                if (item != null)
                {
                    txtName.Text = item.Name;
                    txtControlURL.Text = item.FileName;
                    txtIdentity.Text = item.Identity;
                    chkStandalone.Checked = item.IsStandalone;

                    if (item.ThemeId > 0)
                        WebHelper.SetCboValue(cboTheme, item.ThemeId);

                    if (item.ParentId > 0)
                        WebHelper.SetCboValue(cboParent, item.ParentId);

                    cboPanels.DataBind();

                    if (cboPanels.Items.Count > 0)
                        WebHelper.SetCboValue(cboPanels, item.PrimaryPanelId);
                    else
                        panelDefaultPanel.Visible = false;

                    if (item.SkinId > 0 && cboSkins.Items.Count > 1)
                        WebHelper.SetCboValue(cboSkins, item.SkinId);

                    if (cboSkins.Items.Count == 1)
                        panelDefaultSkin.Visible = false;

                    WebHelper.SetCboValue(cboTemplateEngine, item.TemplateEngineId);

                    //txtIdentity.ReadOnly = true;
                }
                else
                {
                    panelDefaultPanel.Visible = false;
                    //panelDefaultSkin.Visible = false;

                    if (themeId > 0)
                    {
                        var theme = WebTheme.Provider.Get(themeId);
                        if (theme != null)
                        {
                            txtIdentity.Text = theme.Identity;
                            WebHelper.SetCboValue(cboTheme, themeId);
                        }
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.Return();
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            this.Update();
        }

        public IEnumerable<WebTemplatePanel> GetPanels(int templateId)
        {
            if (templateId > 0)
                return WebTemplatePanel.Provider.GetList(WebObjects.WebTemplate, templateId);

            return null;
        }

        private string GetAbsoluteTemplatePath(WebTemplate item)
        {
            return Path.Combine(MapPath(WConfig.RelativeTemplatePath), item.Identity + @"\" + item.FileName);
        }

        private void Return(int id = -1)
        {
            QueryParser query = new QueryParser(this);

            int templateId = id > 0 ? id : query.GetId(WebColumns.TemplateId);
            if (templateId > 0)
            {
                query.Set(WebColumns.TemplateId, templateId);
                query.Redirect(CentralPages.WebTemplateHome);
            }
            else
            {
                query.Redirect(CentralPages.WebTemplates);
            }
        }

        private void Update()
        {
            WebTemplate item = null;

            int templateId = DataHelper.GetId(Request, WebColumns.TemplateId);
            if (templateId > 0 && (item = WebTemplate.Get(templateId)) != null)
            {
                // Update
                item.PrimaryPanelId = DataHelper.GetId(cboPanels.SelectedValue);
                item.SkinId = DataHelper.GetId(cboSkins.SelectedValue);
            }
            else
            {
                // Insert
                item = new WebTemplate();
            }

            item.ThemeId = DataHelper.GetId(cboTheme.SelectedValue);
            item.ParentId = DataHelper.GetId(cboParent.SelectedValue);
            item.IsStandalone = chkStandalone.Checked;
            item.Identity = txtIdentity.Text.Trim();
            item.Name = txtName.Text.Trim();
            item.FileName = txtControlURL.Text.Trim();
            item.TemplateEngineId = DataHelper.GetInt32(cboTemplateEngine.SelectedValue);
            item.Update();

            Return(item.Id);
        }
    }
}