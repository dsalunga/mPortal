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
                int templateId = DataUtil.GetId(Request, WebColumns.TemplateId);
                var themeId = DataUtil.GetId(Request, WebColumns.ThemeId);

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
                        WebUtil.SetCboValue(cboTheme, item.ThemeId);

                    if (item.ParentId > 0)
                        WebUtil.SetCboValue(cboParent, item.ParentId);

                    cboPanels.DataBind();

                    if (cboPanels.Items.Count > 0)
                        WebUtil.SetCboValue(cboPanels, item.PrimaryPanelId);
                    else
                        panelDefaultPanel.Visible = false;

                    if (item.SkinId > 0 && cboSkins.Items.Count > 1)
                        WebUtil.SetCboValue(cboSkins, item.SkinId);

                    if (cboSkins.Items.Count == 1)
                        panelDefaultSkin.Visible = false;

                    WebUtil.SetCboValue(cboTemplateEngine, item.TemplateEngineId);

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
                            WebUtil.SetCboValue(cboTheme, themeId);
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

            int templateId = DataUtil.GetId(Request, WebColumns.TemplateId);
            if (templateId > 0 && (item = WebTemplate.Get(templateId)) != null)
            {
                // Update
                item.PrimaryPanelId = DataUtil.GetId(cboPanels.SelectedValue);
                item.SkinId = DataUtil.GetId(cboSkins.SelectedValue);
            }
            else
            {
                // Insert
                item = new WebTemplate();
            }

            item.ThemeId = DataUtil.GetId(cboTheme.SelectedValue);
            item.ParentId = DataUtil.GetId(cboParent.SelectedValue);
            item.IsStandalone = chkStandalone.Checked;
            item.Identity = txtIdentity.Text.Trim();
            item.Name = txtName.Text.Trim();
            item.FileName = txtControlURL.Text.Trim();
            item.TemplateEngineId = DataUtil.GetInt32(cboTemplateEngine.SelectedValue);
            item.Update();

            Return(item.Id);
        }
    }
}