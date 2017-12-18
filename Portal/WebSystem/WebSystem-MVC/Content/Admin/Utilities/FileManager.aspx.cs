using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.FileManager;

namespace WCMS.WebSystem.FileManager
{
    public partial class FileManagerController : Page
    {
        private QueryParser qs2;
        private string sRoot;
        protected string sPath;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            qs2 = new QueryParser(this);
            sRoot = "~/";

            string sQSPath = qs2["__path"];

            if (string.IsNullOrEmpty(sQSPath))
                sPath = sRoot;
            else
                sPath = sQSPath;

            if (!Page.IsPostBack)
            {
                this.LoadData();
            }
        }

        private void LoadData()
        {
            this.BuildPath();
        }

        private void BuildPath()
        {
            string sParent = sRoot;

            qs2["__path"] = sRoot;
            string sRootNav = "<a href='" + qs2.BuildQuery() + "' title='" + sRoot + "'>root</a>";

            if (sPath.StartsWith(sRoot))
            {
                string[] sFolders = sPath.Substring(sRoot.Length).Trim('/').Split('/');
                foreach (string sFolder in sFolders)
                {
                    if (sFolder.Trim() != string.Empty)
                    {
                        sParent += sFolder + "/";
                        qs2["__path"] = sParent;
                        sRootNav += string.Format("<strong>/</strong><a href='{0}' title='{1}'>{2}</a>", qs2.BuildQuery(), sParent.TrimEnd('/'), sFolder);
                    }
                }
            }

            lRoot.Text = sRootNav;
        }


        private void lbRoot_Click(object sender, System.EventArgs e)
        {
            qs2["__path"] = sPath;
            qs2.Redirect();
        }

        protected void cmdDeleteFile_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkFileChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                // START DELETE
                foreach (string sFile in sChecked.Split(','))
                {
                    if (!this.DeleteFile(sFile)) return;
                }

                GridViewFiles.DataBind();
            }
        }

        private bool DeleteFile(string sFile)
        {
            string sVirtualFile = sPath + sFile;

            try
            {
                FileManagerFile.Delete(sFile, sPath);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "[ " + ex.Message + " ]";
                return false;
            }

            return true;
        }

        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                foreach (string sFolder in sChecked.Split(','))
                {
                    if (!this.DeleteFolder(sFolder)) break;
                }

                qs2.Redirect();
            }
        }

        private bool DeleteFolder(string sFolder)
        {
            string sVirtualFolder = sPath + sFolder;

            // START DELETE
            try
            {
                Directory.Delete(MapPath(sVirtualFolder), true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "[ " + ex.Message + " ]";
                return false;
            }

            return true;
        }

        protected void cmdCreate_Click(object sender, System.EventArgs e)
        {
            string sNewDir = txtDir.Text.Trim();
            if (sNewDir != string.Empty)
            {
                string sVirtualFolder = sPath + sNewDir;
                if (!Directory.Exists(Server.MapPath(sVirtualFolder)))
                {
                    // CREATE FOLDER
                    try
                    {
                        Directory.CreateDirectory(Server.MapPath(sVirtualFolder));
                        GridViewFolders.DataBind();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "[ " + ex.Message + " ]";
                    }
                }
                else
                    lblMessage.Text = "[ The specified folder already exists. ]";
            }
        }

        protected void cmdRefresh_Click(object sender, System.EventArgs e)
        {
            qs2.Redirect();
        }

        protected void cmdUpload_Click(object sender, System.EventArgs e)
        {
            string sNewFile = Path.GetFileName(this.fileUploader.PostedFile.FileName);

            if (sNewFile == string.Empty)
                return;

            try
            {
                this.fileUploader.PostedFile.SaveAs(Server.MapPath(sPath + sNewFile));
                GridViewFiles.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "[ " + ex.Message + " ]";
            }
        }

        protected void cmdCreateLink_Click(object sender, System.EventArgs e)
        {
            string sLink = txtLink.Text.Trim();
            string sFilename = txtFilename.Text.Trim();

            if (sLink != string.Empty)
            {
                string sLinkFile;
                if (sFilename == string.Empty)
                    sLinkFile = "Default.aspx";
                else
                    sLinkFile = sFilename;

                if (!File.Exists(Server.MapPath(sPath + sLinkFile)))
                {
                    StreamWriter writer;
                    StreamReader reader = new StreamReader(Server.MapPath("Default.aspx_"));
                    string sLinkSrc = reader.ReadToEnd().Replace("[URL]", sLink);
                    writer = File.CreateText(Server.MapPath(sPath + sLinkFile));
                    writer.WriteLine(sLinkSrc);

                    writer.Close();

                    GridViewFiles.DataBind();
                }
                else
                {
                    lblMessage.Text = "[ The specified file already exists. ]";
                }
            }
        }

        private void DownloadFile(string sFile)
        {
            // START DOWNLOAD
            string sVirtualFile = sPath + sFile;

            try
            {
                Response.AppendHeader("content-disposition", "attachment; filename=" + sFile);
                Response.WriteFile(MapPath(sVirtualFile));
                Response.End();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "[ " + ex.Message + " ]";
            }
        }

        protected void GridViewFolders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sFolderName = e.CommandArgument.ToString();

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    break;

                case "Custom_Delete":
                    {
                        if (this.DeleteFolder(sFolderName)) GridViewFolders.DataBind();
                        break;
                    }

                case "View_SubFolders":
                    {
                        sPath += HttpUtility.UrlEncode(sFolderName) + "/";
                        qs2["__path"] = sPath;

                        qs2.Redirect();
                        break;
                    }

                case "Custom_Download":
                    {
                        Compression.Download(MapPath(sPath + sFolderName));
                        break;
                    }
            }
        }

        public DataSet GetFiles(string path)
        {
            return DataHelper.ToDataSet(FileManagerFile.GetList(path));
        }

        public DataSet GetFolders(string path)
        {
            return DataHelper.ToDataSet(FileManagerFolder.GetList(path));
        }

        protected void GridViewFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Custom_Edit":
                    {
                        string sFilename = e.CommandArgument.ToString();
                        string sVirtualFile = sPath + sFilename;
                        string sSrc = HttpUtility.UrlEncode(sFilename);
                        string sReturn = HttpUtility.UrlEncode(Page.Request.RawUrl);

                        Response.Redirect("TextEditor.aspx?Src=" + sSrc + "&Return=" + sReturn, true);

                        break;
                    }

                case "Custom_Delete":
                    {
                        string sFilename = e.CommandArgument.ToString();
                        if (this.DeleteFile(sFilename)) GridViewFiles.DataBind();
                        break;
                    }

                case "Custom_Download":
                    {
                        string sFilename = e.CommandArgument.ToString();
                        this.DownloadFile(sFilename);
                        break;
                    }
            }
        }
    }
}