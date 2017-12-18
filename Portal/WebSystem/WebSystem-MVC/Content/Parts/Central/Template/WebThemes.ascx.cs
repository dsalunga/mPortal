using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

using WCMS.Common.Utilities;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Template
{
    public partial class WebThemesViewController : UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        protected void cmdAdd_Click(object sender, System.EventArgs e)
        {
            WebHelper.Redirect(CentralPages.WebTheme, Context);
        }

        private string ProcessTemplateResources(string src, string sdestFolder)
        {
            string RESOURCE_DIR = "/Content/Assets/Uploads/Image/";
            string[] IMG_END_CHARS = new string[] { "\"", " ", "'", ">" };

            string dest = src;
            string usrc = src.ToUpper();

            int cPos = 0;

            // ALL PLACEHOLDERS
            while (true)
            {
                int cPos1 = -1;
                int cPos2 = -1;
                int cPos3 = -1;

                cPos1 = usrc.IndexOf(RESOURCE_DIR.ToUpper(), cPos);
                if (cPos1 == -1)
                    break;

                foreach (string s in IMG_END_CHARS)
                {
                    cPos3 = usrc.IndexOf(s, cPos1);

                    // FIND THE FIRST CLOSEST OCCURANCE
                    if ((cPos2 > cPos3 || cPos2 == -1) && (cPos3 != -1))
                        cPos2 = cPos3;
                }
                if (cPos2 == -1)
                    break;

                // GET FILE UNDER THE "/Uploads/Image/" FOLDER
                string sRes = src.Substring(cPos1, cPos2 - cPos1);

                string sResPath = Server.MapPath("../" + sRes);
                string sResFile = Path.GetFileName(sResPath);
                string sdestFile = sdestFolder + sResFile;
                string sdestPath = Server.MapPath("../" + sdestFile);

                // COPY THE RESOURCE
                if (!File.Exists(sdestPath))
                    File.Copy(sResPath, sdestPath);

                // REPLACE THE FILE ON SOURCE
                dest = dest.Replace(sRes, sdestFile);

                // UPDATE THE CURRENT POSITION
                cPos = cPos2;
            }

            return dest;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument.ToString());
            QueryParser query = new QueryParser(this);
            query.Set(WebColumns.ThemeId, id);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Redirect(CentralPages.WebThemeHome);
                    break;

                case "View-Templates":
                    query.Redirect(CentralPages.WebTemplates);
                    break;

                case "DeleteItem":
                    WebTheme.Provider.Delete(id);
                    GridView1.DataBind();
                    break;
            }
        }

        protected void cmdParseDirectory_Click(object sender, EventArgs e)
        {
            /*
            string[] themePaths = Directory.GetDirectories(MapPath("~/Content/Themes"));
            var items = WebTheme.Provider.GetList();

            foreach (string themeFolder in themePaths)
            {
                string identity = Path.GetFileName(themeFolder); // Get folder name
                WebTheme item = null;

                // Search all templates...
                string[] templateFiles = Directory.GetFiles(themeFolder);
                foreach (string templatePath in templateFiles)
                {
                    string templateFile = Path.GetFileName(templatePath);
                    string templateName = Path.GetFileNameWithoutExtension(templatePath);
                    string extension = Path.GetExtension(templatePath);
                    if (extension.StartsWith(".as")) // || extension.StartsWith(".ht"))
                    {
                        if (items == null || (item = items.FirstOrDefault(i => i.Identity.Equals(identity) && i.FileName.Equals(templateFile))) == null)
                        {
                            item = new WebTheme();
                            item.Name = templateName;
                            item.Identity = identity;
                            //item.Content = FileHelper.ReadFile(templatePath);
                            item.Update();
                        }
                    }
                }
            }

            GridView1.DataBind();
            */
        }

        public DataSet Select()
        {
            WebTemplate template = null;
            WebTheme parent = null;

            QueryParser query = new QueryParser(true);

            return DataHelper.ToDataSet(
                from i in WebTheme.Provider.GetList()
                select new
                {
                    i.Id,
                    i.Name,
                    i.Identity,
                    TemplateName = i.TemplateId > 0 && (template = WebTemplate.Get(i.TemplateId)) != null ? template.Name : string.Empty,
                    ParentName = i.ParentId > 0 && (parent = i.Parent) != null ? parent.Name : string.Empty,
                    NameUrl = query.Set(WebColumns.ThemeId, i.Id).BuildQuery(CentralPages.WebThemeHome)
                }
            );

            //var items = (from i in WebTemplate.GetList().AsQueryable<WebTemplate>()
            //             select i).Skip(startRowIndex).Take(maximumRows);

            //if (!string.IsNullOrEmpty(sortExpression))
            //{
            //    // Extract the column name
            //    string sortColumn = sortExpression.EndsWith(" DESC", StringComparison.InvariantCultureIgnoreCase) ?
            //        sortExpression.Substring(0, sortExpression.Length - " DESC".Length) : sortExpression;

            //    // Generate an expression for the column
            //    var param = Expression.Parameter(typeof(WebTemplate), "WebTemplate");
            //    var sort = Expression.Lambda<Func<WebTemplate, object>>(Expression.Property(param, sortColumn), param);

            //    // Sort the list
            //    items = (sortColumn.Length != sortExpression.Length) ?
            //        items.OrderByDescending(sort) : items = items.OrderBy(sort);
            //}

            //return items;
        }

        //public int SelectCount()
        //{
        //    return WebTemplate.GetList().Count;
        //}
    }
}