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
    public partial class WebTemplateEditorController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int templateId = DataHelper.GetId(Request, WebColumns.TemplateId);

                WebTemplate item = null;
                if (templateId > 0 && (item = WebTemplate.Get(templateId)) != null)
                {
                    txtName.Text = item.Name;
                    txtControlURL.Text = item.FileName;
                    txtIdentity.Text = item.Identity;

                    // Check if to load file from disk

                    string templatePath = this.GetAbsoluteTemplatePath(item);
                    bool fileLoaded = false;

                    if (File.Exists(templatePath))
                    {
                        var fileWriteUtc = File.GetLastWriteTimeUtc(templatePath);
                        var dataWriteUtc = item.DateModified;

                        fileWriteUtc = new DateTime(fileWriteUtc.Year, fileWriteUtc.Month, fileWriteUtc.Day, fileWriteUtc.Hour, fileWriteUtc.Minute, fileWriteUtc.Second);
                        dataWriteUtc = new DateTime(dataWriteUtc.Year, dataWriteUtc.Month, dataWriteUtc.Day, dataWriteUtc.Hour, dataWriteUtc.Minute, dataWriteUtc.Second);

                        if (fileWriteUtc >= dataWriteUtc || string.IsNullOrEmpty(item.Content))
                        {
                            // Load the file
                            txtContent.Text = FileHelper.ReadFile(templatePath);
                            fileLoaded = true;
                        }
                    }

                    if (!fileLoaded)
                        txtContent.Text = item.Content;
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
                var content = txtContent.Text;
                var templatePath = this.GetAbsoluteTemplatePath(item);

                if (File.Exists(templatePath) && string.IsNullOrEmpty(content))
                {
                    item.Content = FileHelper.ReadFile(templatePath);
                }
                else
                {
                    var folder = FileHelper.GetFolder(templatePath, '\\');
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    FileHelper.WriteFile(content, templatePath);

                    item.Content = content;
                }

                item.DateModified = File.GetLastWriteTimeUtc(templatePath);
                item.Update();

                Return(item.Id);
            }
        }
    }
}