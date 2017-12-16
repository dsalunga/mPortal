using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.FileManager.Controls
{
    public partial class Breadcrumb : FileManagerBase
    {
        private WContext query;

        private string rootPath;
        private string pathSeparator;
        private string rootText;

        protected string currentPath;

        protected void Page_Load(object sender, EventArgs e)
        {
            query = new WContext(this.Parent);
            var element = query.Element;

            pathSeparator = element.GetParameterValue("PathSeparator", WConstants.Arrow);
            rootPath = element
                .GetParameterValue(FileManagerConstants.RootPathKey, FileManagerConstants.DefaultRoot)
                .TrimEnd(new char[] { '/', '\\' });

            rootText = element.GetParameterValue(FileManagerConstants.RootTextKey, "&lt;root&gt;");

            currentPath = FromVirtualPath(query.Query.GetValue(FileManagerConstants.PathKey, rootPath), rootPath);
            if (!currentPath.ToLower().StartsWith(rootPath.ToLower()))
                currentPath = rootPath;

            this.BuildPath();
        }

        private void BuildPath()
        {
            var parentPath = rootPath;
            var openParam = query.Get(WConstants.Open);

            query.Remove(WConstants.Open);
            query.Set(FileManagerConstants.PathKey, ToVirtualPath(rootPath, rootPath));

            string rootNav = string.Format("<a href='{0}' title='{1}'>{2}</a>", query.BuildQuery(), rootPath, rootText);

            if (currentPath.StartsWith(rootPath))
            {
                parentPath = parentPath.TrimEnd('/');

                string[] folders = currentPath.Substring(rootPath.Length).Trim('/').Split('/');
                int len = folders.Length;
                for (int i = 0; i < len; i++)
                {
                    var folder = folders[i];
                    if (!string.IsNullOrEmpty(folder.Trim()))
                    {
                        parentPath += "/" + folder;

                        if (i + 1 == len && (!FileHelper.IsFolder(WebHelper.MapPath(currentPath)))) // || (!string.IsNullOrEmpty(openParam) && openParam.Equals(FileManagerConstants.ViewFile, StringComparison.InvariantCultureIgnoreCase)))
                            query.SetOpen(FileManagerConstants.ViewFile);

                        query.Set(FileManagerConstants.PathKey, ToVirtualPath(parentPath, rootPath));
                        rootNav += string.Format(@"&nbsp;<span class=""separator"">{3}</span> <a href='{0}' title='{1}'>{2}</a>",
                            query.BuildQuery(), parentPath, folder, pathSeparator);
                    }
                }
            }

            lRoot.Text = rootNav;
        }
    }
}