using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Text;
using System.IO;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Controls.Controller
{
    public partial class BreadcrumbController : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser qs = new QueryParser(this);
                StringBuilder sb = new StringBuilder();
                string basePath = qs.BasePath.ToLower();
                string path;

                sb.AppendFormat("<a href=\"{0}\">{1}</a>&nbsp;>&nbsp;", "/", "Home");
                sb.AppendFormat("<a href=\"{0}\">{1}</a>", "/Central/WebSystemHome.aspx", "Administration");

                if (basePath.StartsWith("/admin/"))
                {
                    path = Path.GetFileName(basePath);
                    switch (path)
                    {
                        case "webregistry.aspx":
                            {
                                sb.AppendFormat("&nbsp;>&nbsp;{0}&nbsp;>&nbsp;{1}", "Tools", "Web Registry");
                                break;
                            }

                        case "queryanalyzer.aspx":
                            {
                                sb.AppendFormat("&nbsp;>&nbsp;{0}&nbsp;>&nbsp;{1}", "Tools", "Query Analyzer");
                                break;
                            }

                        case "filemanager.aspx":
                            {
                                sb.AppendFormat("&nbsp;>&nbsp;{0}&nbsp;>&nbsp;{1}", "Tools", "File Manager");
                                break;
                            }

                        case "webroles.aspx":
                            {
                                sb.AppendFormat("&nbsp;>&nbsp;{0}&nbsp;>&nbsp;{1}", "Security", "User Roles");
                                break;
                            }

                        case "websites.aspx":
                            {
                                sb.AppendFormat("&nbsp;>&nbsp;<a href=\"{0}\">{1}</a>", qs.BuildQuery("WebSites.aspx"), "Web Sites");
                                break;
                            }

                        case "webtemplates.aspx":
                            {
                                sb.Append(GetWebTemplates(qs));
                                break;
                            }

                        case "webtemplate.aspx":
                            sb.Append(GetWebTemplates(qs));
                            sb.Append(GetWebTemplate(qs));
                            break;

                        case "webtemplatehome.aspx":
                            sb.Append(GetWebTemplates(qs));
                            sb.Append(GetWebTemplateHome(qs));
                            break;

                        case "webtemplatepanels.aspx":
                            sb.Append(GetWebTemplates(qs));
                            sb.Append(GetWebTemplateHome(qs));
                            sb.Append(GetWebTemplatePanels(qs));
                            break;

                        case "webtemplatepanel.aspx":
                            sb.Append(GetWebTemplates(qs));
                            sb.Append(GetWebTemplateHome(qs));
                            sb.Append(GetWebTemplatePanels(qs));
                            sb.Append(GetWebTemplatePanel(qs));
                            break;

                        case "websitehome.aspx":
                            {
                                sb.Append(GetWebSites(qs));
                                break;
                            }
                    }
                }
                else if (basePath.Equals("/setup.aspx"))
                {
                    sb.AppendFormat("&nbsp;>&nbsp;{0}&nbsp;>&nbsp;{1}", "Tools", "Online Setup");
                }
               
                breadcrumbContainer.InnerHtml = sb.ToString();
            }
        }

        private string GetWebTemplates(QueryParser qs)
        {
            QueryParser q = qs.Clone();
            q.Remove("TemplateId");

            return string.Format("&nbsp;>&nbsp;<a href=\"{0}\">{1}</a>", q.BuildQuery("WebTemplates.aspx"), "Web Templates");
        }

        private string GetWebSites(QueryParser qs)
        {
            QueryParser q = qs.Clone();
            q.Remove("SiteId");

            return string.Format("&nbsp;>&nbsp;<a href=\"{0}\">{1}</a>", q.BuildQuery("WebSites.aspx"), "Web Sites");
        }

        private string GetWebTemplatePanels(QueryParser qs)
        {
            return string.Format("&nbsp;>&nbsp;<a href=\"{0}\">{1}</a>", qs.BuildQuery("WebTemplatePanels.aspx"), "Panels");
        }

        private string GetWebTemplate(QueryParser qs)
        {
            int id = DataHelper.GetDbId(qs[WebColumns.TemplateId]);
            if (id > 0)
            {
                WebTemplate item = WebTemplate.Get(id);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("&nbsp;>&nbsp;<a href=\"{0}\">{1}</a>", qs.BuildQuery("WebTemplateHome.aspx"), item.Name);
                sb.AppendFormat("&nbsp;>&nbsp;<a href=\"{0}\">{1}</a>", qs.BuildQuery("WebTemplate.aspx"), "Edit");

                return sb.ToString();
            }
            else
            {
                return string.Format("&nbsp;>&nbsp;<a href=\"{0}\">{1}</a>", qs.BuildQuery("WebTemplate.aspx"), "New");
            }
        }

        private string GetWebTemplatePanel(QueryParser qs)
        {
            int id = DataHelper.GetDbId(qs[WebColumns.TemplatePanelId]);
            if (id > 0)
            {
                WebTemplatePanel item = WebTemplatePanel.Get(id);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("&nbsp;>&nbsp;<a href=\"{0}\">{1}</a>", qs.BuildQuery("WebTemplatePanel.aspx"), item.Name);

                return sb.ToString();
            }
            else
            {
                return string.Format("&nbsp;>&nbsp;<a href=\"{0}\">{1}</a>", qs.BuildQuery("WebTemplatePanel.aspx"), "New");
            }
        }

        private string GetWebTemplateHome(QueryParser qs)
        {
            int id = DataHelper.GetDbId(qs[WebColumns.TemplateId]);
            if (id > 0)
            {
                WebTemplate item = WebTemplate.Get(id);
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("&nbsp;>&nbsp;<a href=\"{0}\">{1}</a>", qs.BuildQuery("WebTemplateHome.aspx"), item.Name);

                return sb.ToString();
            }

            return string.Empty;
        }
    }

}