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
    public partial class WebTemplates : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void cmdAdd_Click(object sender, System.EventArgs e)
        {
            WQuery query = new WQuery(this);
            query.Redirect(CentralPages.WebTemplate);
        }

        //protected void cmdDuplicate_Click(object sender, System.EventArgs e)
        //{
        //    string sChecked = Request.Form["chkChecked"];
        //    string sNewFile = txtDuplicate.Text.Trim();
        //    string sNewFolder = sNewFile;
        //    string sTemplateFolder = "~/Templates/"; //(sPageType == "H") ? "/_Home/" : "/_Content/";
        //    string sResourceFolder = "~/Assets/Uploads/Image/Template/"; //(sPageType == "H") ? "/Assets/Uploads/Image/HOME/" : "/Assets/Uploads/Image/CONTENT/";

        //    if (string.IsNullOrEmpty(sChecked))
        //    {
        //        // NONE SELECTED
        //        return;
        //    }

        //    if (sNewFile == string.Empty)
        //    {
        //        // NO FILENAME
        //        return;
        //    }
        //    else
        //    {
        //        // EXTENSION NAME
        //        if (!sNewFile.EndsWith(".ascx"))
        //            sNewFile += ".ascx";
        //        else
        //            sNewFolder = sNewFile.Substring(0, sNewFile.Length - 5);
        //    }

        //    foreach (string sID in sChecked.Split(','))
        //    {
        //        using (SqlDataReader r = SqlHelper.ExecuteReader("CMS.SELECT_PageTemplates",
        //                   new SqlParameter("@PageTemplateID", Convert.ToInt32(sID))))
        //        {
        //            if (r.Read())
        //            {
        //                string sTemplateFile = r["ControlURL"].ToString();
        //                string sTemplateName = r["TemplateName"].ToString() + " (COPY)";
        //                string sdescription = r["Description"].ToString();

        //                // READ FILE
        //                string sContent = FileHelper.ReadFile(Server.MapPath(sTemplateFolder + sTemplateFile));

        //                // CREATE RESOURCE FOLDER
        //                if (!Directory.Exists(Server.MapPath(sResourceFolder + sNewFolder)))
        //                {
        //                    Directory.CreateDirectory(Server.MapPath(sResourceFolder + sNewFolder));
        //                }
        //                else
        //                {
        //                    // FOLDER ALREADY EXISTS
        //                    //return;
        //                }

        //                // PROCESS CONTENT HERE
        //                sContent = this.ProcessTemplateResources(sContent, sResourceFolder + sNewFolder + "/");


        //                // WRITE FILE
        //                if (!File.Exists(Server.MapPath(sTemplateFolder + sNewFile)))
        //                {
        //                    FileHelper.WriteFile(sContent, Server.MapPath(sTemplateFolder + sNewFile));
        //                }
        //                else
        //                {
        //                    // TEMPLATE NAME ALREADY EXISTS
        //                    return;
        //                }

        //                // INSERT NEW TEMPLATE
        //                SqlHelper.ExecuteNonQuery("CMS.UPDATE_PageTemplates",
        //                    new SqlParameter("@TemplateName", sTemplateName),
        //                    new SqlParameter("@ControlURL", sNewFile),
        //                    new SqlParameter("@description", sdescription),
        //                    new SqlParameter("@UserID", Membership.GetUser().ProviderUserKey));
        //            }
        //        }

        //        break;
        //    }

        //    GridView1.DataBind();
        //}

        /*
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
        */

        //private string GetPossibleResourcePath(string src)
        //{
        //    string C_RES_DIR = "/Uploads/Image/";
        //    string[] C_IMG_END = new string[] { "\"", " ", "'", ">" };
        //    string usrc = src.ToUpper();

        //    int cPos = 0;

        //    // ALL PLACEHOLDERS
        //    int cPos1 = -1;
        //    int cPos2 = -1;
        //    int cPos3 = -1;

        //    cPos1 = usrc.IndexOf(C_RES_DIR.ToUpper(), cPos);
        //    if (cPos1 == -1)
        //        return string.Empty;

        //    foreach (string s in C_IMG_END)
        //    {
        //        cPos3 = usrc.IndexOf(s, cPos1);

        //        // FIND THE FIRST CLOSEST OCCURANCE
        //        if ((cPos2 > cPos3 || cPos2 == -1) && (cPos3 != -1))
        //            cPos2 = cPos3;
        //    }
        //    if (cPos2 == -1)
        //        return string.Empty;

        //    // GET FILE UNDER THE "/Uploads/Image/" FOLDER
        //    string sRes = src.Substring(cPos1, cPos2 - cPos1);
        //    string sResFolder = Path.GetDirectoryName(sRes);

        //    // UPDATE THE CURRENT POSITION
        //    //cPos = cPos2;


        //    return sResFolder.Replace("\\", "/") + "/";
        //}

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int templateId = DataHelper.GetId(e.CommandArgument.ToString());
            QueryParser query = new QueryParser(this);
            query.Set(WebColumns.TemplateId, templateId);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Redirect(CentralPages.WebTemplateHome);
                    break;

                case "View_Placeholders":
                    query.Redirect(CentralPages.WebTemplatePanels);
                    break;

                case "template":
                    //qs["PageTemplateID"] = templateId.ToString();
                    query.Redirect(CentralPages.WebTemplateEditor);
                    break;

                /*
                case "Edit_File":
                    qs[WebColumns.TemplateId] = sID;
                    Response.Redirect("TemplateTextEditor.aspx?" + qs, true);
                    break;
                */
                case "Toggle_Active":
                    //CMSDALTableAdapters.PageTemplatesAdapter aPT = new CMSDALTableAdapters.PageTemplatesAdapter();
                    //aPT.ToggleStatus(Convert.ToInt32(sID));

                    GridView1.DataBind();
                    break;

                case "DeleteItem":
                    WebTemplate.Delete(templateId);
                    GridView1.DataBind();
                    break;

                /*
            case "files":
                string sTemplateFolder = "/_Template/"; //(sPageType == "H") ? "/_Home/" : "/_Content/";
                string sResPath = string.Empty; // this.GetPossibleResourcePath(FileHelper.ReadFile(Server.MapPath(sTemplateFolder + e.Item.Cells[5].Text)));
                if (sResPath != string.Empty)
                {
                    object obj = SqlHelper.ExecuteScalar("CMS.SELECT_CommonSectionContentCMS_ID",
                        new SqlParameter("@ControlIdentity", "FM"),
                        new SqlParameter("@PageIdentity", (int)1));
                    qs = new QueryParser(this.Request.QueryString);
                    qs["CSCCMSID"] = obj.ToString();
                    qs["PageType"] = "L";
                    qs["__path"] = SystemSettings.GetSettings("System.WEB_ROOT").TrimEnd('/') + "/" + sResPath;
                    Response.Redirect(".?" + qs, true);
                }
                else
                {
                    // NO PATH FOUND
                }
                break;
                */
            }
        }

        protected void cmdParseDirectory_Click(object sender, EventArgs e)
        {
            var themeId = DataHelper.GetId(Request, WebColumns.ThemeId);
            if (themeId > 0)
            {
                WebTemplate item = null;

                var theme = WebTheme.Provider.Get(themeId);
                var items = WebTemplate.Provider.GetList(themeId);

                var templatePaths = Directory.EnumerateFiles(MapPath("~/Content/Themes/" + theme.Identity));

                foreach (string templatePath in templatePaths)
                {
                    string templateFile = Path.GetFileName(templatePath);
                    string templateName = Path.GetFileNameWithoutExtension(templatePath);
                    string extension = Path.GetExtension(templatePath);
                    if (extension.StartsWith(".as") || extension.StartsWith(".cs"))
                    {
                        if (items == null || (item = items.FirstOrDefault(i => i.FileName.Equals(templateFile))) == null)
                        {
                            var content = FileHelper.ReadFile(templatePath);

                            item = new WebTemplate();
                            item.TemplateEngineId = extension.StartsWith(".cs") ? TemplateEngineTypes.Razor : TemplateEngineTypes.ASPX;
                            item.Name = item.TemplateEngineId == TemplateEngineTypes.ASPX ? templateName : templateName + " (Razor)";
                            item.FileName = templateFile;
                            item.Identity = theme.Identity;
                            item.ThemeId = themeId;
                            item.Content = content;
                            item.IsStandalone = content.IndexOf("<html", StringComparison.InvariantCultureIgnoreCase) > -1;
                            item.Update();
                        }
                    }
                }
            }
            else
            {
                var items = WebTemplate.Provider.GetList();

                string[] templatePaths = Directory.GetDirectories(MapPath("~/Content/Themes"));

                foreach (string templateFolder in templatePaths)
                {
                    string identity = Path.GetFileName(templateFolder); // Get folder name
                    WebTemplate item = null;

                    // Search all templates...
                    string[] templateFiles = Directory.GetFiles(templateFolder);
                    foreach (string templatePath in templateFiles)
                    {
                        string templateFile = Path.GetFileName(templatePath);
                        string templateName = Path.GetFileNameWithoutExtension(templatePath);
                        string extension = Path.GetExtension(templatePath);
                        if (extension.StartsWith(".as")) // || extension.StartsWith(".ht"))
                        {
                            if (items == null || (item = items.FirstOrDefault(i => i.Identity.Equals(identity) && i.FileName.Equals(templateFile))) == null)
                            {
                                item = new WebTemplate();
                                item.Name = templateName;
                                item.FileName = templateFile;
                                item.Identity = identity;
                                item.Content = FileHelper.ReadFile(templatePath);
                                item.Update();
                            }
                        }
                    }
                }
            }

            GridView1.DataBind();
        }

        public DataSet Select(int themeId = -2)
        {
            QueryParser query = new QueryParser(this.Context);

            WebSkin skin = null;
            WebTheme theme = null;
            WebTemplatePanel panel = null;
            WebTemplate parent = null;

            return DataHelper.ToDataSet(
                from i in WebTemplate.Provider.GetList(themeId)
                where ((panel = i.PrimaryPanel) != null || true)
                select new
                {
                    i.Id,
                    i.Name,
                    i.FileName,
                    i.Identity,
                    ThemeName = i.ThemeId > 0 && (theme = WebTheme.Provider.Get(i.ThemeId)) != null ? theme.Name : string.Empty,
                    SkinName = i.SkinId > 0 && (skin = WebSkin.Provider.Get(i.SkinId)) != null ? skin.Name : string.Empty,
                    PanelName = panel != null ? panel.Name : string.Empty,
                    i.Standalone,
                    ParentName = i.ParentId > 0 && (parent = i.Parent) != null ? parent.Name : string.Empty,
                    NameUrl = query.Set(WebColumns.TemplateId, i.Id).BuildQuery(CentralPages.WebTemplateHome)
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