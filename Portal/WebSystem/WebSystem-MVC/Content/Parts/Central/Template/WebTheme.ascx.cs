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
    public partial class WebThemeViewController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int id = DataHelper.GetId(Request, WebColumns.ThemeId);

                cboSkins.DataSource = WebSkin.Provider.GetList(WebObjects.WebTheme, id);
                cboSkins.DataBind();

                cboTemplates.DataSource = WebTemplate.Provider.GetList(id);
                cboTemplates.DataBind();

                WebTheme item = id > 0 ? WebTheme.Provider.Get(id) : null;

                var items = WebTheme.Provider.GetList()
                    .Where(i => (item == null || i.Id != item.Id));

                cboParent.DataSource = items;
                cboParent.DataBind();


                if (item != null)
                {
                    txtName.Text = item.Name;
                    txtIdentity.Text = item.Identity;

                    if (item.ParentId > 0)
                        WebHelper.SetCboValue(cboParent, item.ParentId);

                    if (cboTemplates.Items.Count > 0)
                    {
                        if (item.TemplateId > 0)
                            WebHelper.SetCboValue(cboTemplates, item.TemplateId);
                    }
                    else
                    {
                        panelDefaultTemplate.Visible = false;
                    }

                    if (cboSkins.Items.Count > 0)
                    {
                        if (item.SkinId > 0)
                            WebHelper.SetCboValue(cboSkins, item.SkinId);
                    }
                    else
                    {
                        panelDefaultSkin.Visible = false;
                    }

                    //txtIdentity.ReadOnly = true;
                }
                else
                {
                    panelDefaultTemplate.Visible = false;
                    panelDefaultSkin.Visible = false;
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

        private string GetAbsoluteTemplatePath(WebTheme item)
        {
            return Path.Combine(MapPath(WConfig.RelativeTemplatePath), item.Identity);
        }

        private void Return(int id = -1)
        {
            QueryParser query = new QueryParser(this);

            int themeId = id > 0 ? id : query.GetId(WebColumns.ThemeId);
            if (themeId > 0)
            {
                query.Set(WebColumns.ThemeId, themeId);
                query.Redirect(CentralPages.WebThemeHome);
            }
            else
            {
                query.Redirect(CentralPages.WebThemes);
            }
        }

        private void Update()
        {
            WebTheme item = null;

            int id = DataHelper.GetId(Request, WebColumns.ThemeId);
            if (id > 0 && (item = WebTheme.Provider.Get(id)) != null)
            {
                // Update
                item.TemplateId = DataHelper.GetId(cboTemplates.SelectedValue);
                item.SkinId = DataHelper.GetId(cboSkins.SelectedValue);
            }
            else
            {
                // Insert
                item = new WebTheme();
            }

            item.ParentId = DataHelper.GetId(cboParent.SelectedValue);
            item.Identity = txtIdentity.Text.Trim();
            item.Name = txtName.Text.Trim();
            item.Update();

            Return(item.Id);
        }
    }
}