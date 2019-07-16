using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem;
using WCMS.WebSystem.ViewModel;
using WCMS.Common.Utilities;
using System.Text;
using WCMS.Framework.Diagnostics;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class CentralLoader : WPageControl
    {
        private Control controlToLoad = null;
        private StringBuilder _resources = new StringBuilder();
        public string PageTitle { get; set; }
        public string PageUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            var sw = PerformanceLog.StartLog();
            lblStatus.Visible = false;

            if (!WHelper.CheckCentralLoaderAccess(this)) return;

            var query = new WQuery(this);
            if (query.Count == 0)
            {
                WebHelper.Redirect(CentralPages.CentrlHome, Context);
                return;
            }

            var title = string.Empty;
            var pageId = query.GetId(WebColumns.PageId);
            var pageElementId = query.GetId(WebColumns.PageElementId);
            var partAdminId = query.GetId(WebColumns.PartAdminId);

            if (pageId > 0 || pageElementId > 0)
            {
                if (pageElementId > 0)
                {
                    // MasterPageItem, Page
                    var item = WebPageElement.Get(pageElementId);
                    if (item != null)
                    {
                        title = "App Settings: " + item.Name;

                        try
                        {
                            // WebPartConfig
                            var load = query.Get(WConstants.Load);
                            var partControl = item.PartControlTemplate.PartControl;
                            var admin = partControl.PartAdmin;
                            string fileName = string.IsNullOrEmpty(load) ? partControl.GetAdminFile(admin) : FormatLoadParam(load);

                            //WebGenericTab1.Title = string.Format("{0} {2} {1}", item.Name, partControl.Name, "/");
                            PageTitle = admin != null && !admin.IsAutoTitle ? "" : item.Name; // string.Format("{0} {2} {1}", item.Name, partControl.Name, "/");

                            if (!string.IsNullOrEmpty(fileName))
                            {
                                var part = partControl.Part;
                                var controlPath = string.Format("~/Content/Parts/{0}/{1}", part.Identity, fileName);
                                controlToLoad = LoadControl(controlPath);
                                controlToLoad.ID = WContext.GenerateControlId(partControl);

                                // LOAD THE CONTROL
                                phCMSControl.Controls.Add(controlToLoad);

                                // Load Resources
                                LoadResources(part);
                            }
                            else
                            {
                                DisplayMessage(WSConstants.NO_LOAD);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog(ex);
                            DisplayMessage(WSConstants.ERROR_LOAD + ex.Message);
                        }
                    }
                }
                else if (pageId > 0)
                {
                    // Load page's contextual settings module.
                    var page = WPage.Get(pageId);
                    if (page != null)
                    {
                        title = "App Settings: " + page.Name;

                        try
                        {
                            // WebPartConfig
                            var load = query.Get(WConstants.Load);
                            var partConfigId = query.GetId(WebColumns.PartConfigId);
                            if (partConfigId > 0)
                            {
                                var config = WebPartConfig.Get(partConfigId);
                                string fileName = string.IsNullOrEmpty(load) ? config.FileName : FormatLoadParam(load);

                                //WebGenericTab1.Title = string.Format("{0} {2} {1}", page.Name, config.Name, "/");
                                //WebGenericTab1.URL = string.Format("<a href=\"{0}\">{0}</a>", page.BuildRelativeUrl());
                                PageTitle = string.Format("{0} {2} {1}", page.Name, config.Name, "/");
                                PageUrl = page.BuildRelativeUrl();

                                if (!string.IsNullOrEmpty(fileName))
                                {
                                    var part = config.Part;
                                    controlToLoad = LoadControl(string.Format("~/Content/Parts/{0}/{1}", part.Identity, fileName));
                                    controlToLoad.ID = WContext.GenerateControlId(config);

                                    // LOAD THE CONTROL
                                    phCMSControl.Controls.Add(controlToLoad);
                                    LoadResources(part);
                                }
                                else
                                {
                                    DisplayMessage(WSConstants.NO_LOAD);
                                }
                            }
                            else
                            {
                                var partControl = page.PartControlTemplate.PartControl;
                                var admin = partControl.PartAdmin;
                                string fileName = string.IsNullOrEmpty(load) ? partControl.GetAdminFile(admin) : FormatLoadParam(load);

                                //WebGenericTab1.Title = string.Format("{0} {2} {1}", page.Name, partControl.Name, "/");
                                //WebGenericTab1.URL = string.Format("<a href=\"{0}\">{0}</a>", page.BuildRelativeUrl());
                                if (admin == null || admin.IsAutoTitle)
                                {
                                    PageTitle = page.Name; //string.Format("{0} {2} {1}", page.Name, admin == null ? partControl.Name : admin.Name, "/");
                                    PageUrl = page.BuildRelativeUrl();
                                }

                                if (!string.IsNullOrEmpty(fileName))
                                {
                                    var part = partControl.Part;
                                    controlToLoad = LoadControl(string.Format("~/Content/Parts/{0}/{1}", part.Identity, fileName));
                                    controlToLoad.ID = WContext.GenerateControlId(partControl);

                                    // LOAD THE CONTROL
                                    phCMSControl.Controls.Add(controlToLoad);
                                    LoadResources(part);
                                }
                                else
                                {
                                    DisplayMessage(WSConstants.NO_LOAD);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog(ex);
                            DisplayMessage(WSConstants.ERROR_LOAD + ex.Message);
                        }
                    }
                }
                else
                {
                    trFinish.Visible = false;
                    DisplayMessage(WSConstants.NO_LOAD);
                }
            }
            else if (partAdminId > 0)
            {
                // Web Module
                trFinish.Visible = false;

                var partAdmin = WebPartAdmin.Get(partAdminId);
                if (partAdmin != null)
                {
                    title = "App Administration: " + partAdmin.Name;

                    try
                    {
                        var part = partAdmin.Part;
                        var load = query.Get(WConstants.Load);
                        string controlFilename = null;

                        if (!string.IsNullOrEmpty(load))
                            controlFilename = string.Format("~/Content/Parts/{0}/{1}", part.Identity, FormatLoadParam(load));
                        else
                            controlFilename = partAdmin.FileName.StartsWith("~") || partAdmin.FileName.StartsWith("/") ?
                                partAdmin.FileName : string.Format("~/Content/Parts/{0}/{1}", part.Identity, partAdmin.FileName);

                        // WebPartAdmin
                        controlToLoad = LoadControl(controlFilename);
                        controlToLoad.ID = WContext.GenerateControlId(partAdmin);

                        // LOAD THE CONTROL
                        //WebGenericTab1.Title = string.Format("{0} {2} {1}", part.Name, partAdmin.Name, "/");
                        if (partAdmin.IsAutoTitle)
                            PageTitle = string.Format("{0} {2} {1}", part.Name, partAdmin.Name, "/");
                        phCMSControl.Controls.Add(controlToLoad);

                        // Load Resources
                        LoadResources(part);
                        LoadResources(partAdmin);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(ex);
                        DisplayMessage(WSConstants.ERROR_LOAD + ex.Message);
                    }
                }
            }
            else
            {
                trFinish.Visible = false;
                DisplayMessage(WSConstants.NO_LOAD);
            }

            if (controlToLoad != null)
            {
                // Configure Update button
                var updatableControl = controlToLoad as IUpdatable;
                if (updatableControl != null)
                {
                    // Show Update button
                    cmdUpdate.Visible = true;
                    if (!string.IsNullOrEmpty(updatableControl.UpdateText))
                        cmdUpdate.Text = updatableControl.UpdateText;

                    cmdFinish.Text = string.IsNullOrEmpty(updatableControl.CancelText) ? "Cancel" : updatableControl.CancelText;
                    trFinish.Visible = true;
                }
            }

            Values.Add("Resources", _resources.ToString());
            Values.Add("Title", title);

            this.Header.DataBind();

            WSession.UserSessions.Update(WSession.Current.UserId, -1, Request.RawUrl);
            PerformanceLog.EndLog(string.Format("Central-Loader-Aspx: elementid-{0}, partadminid-{1}", pageElementId, partAdminId), sw, pageId);
        }

        protected void cmdFinish_Click(object sender, EventArgs e)
        {
            // This is Cancel
            Return();
        }

        private void Return()
        {
            var context = new WContext(this);
            var elementId = context.GetId(WebColumns.PageElementId);
            var pageId = context.GetId(WebColumns.PageId);
            var siteId = context.GetId(WebColumns.SiteId);
            var masterId = context.GetId(WebColumns.MasterPageId);

            var basePath = elementId > 0 ? CentralPages.WebPageElementHome : CentralPages.WebPageHome;
            var query = new QueryParser(basePath);

            if (siteId > 0)
                query.Set(WebColumns.SiteId, siteId);

            if (pageId > 0)
                query.Set(WebColumns.PageId, pageId);
            else if (masterId > 0)
                query.Set(WebColumns.MasterPageId, masterId);

            if (elementId > 0)
                query.Set(WebColumns.PageElementId, elementId);

            query.Redirect();
        }

        private string FormatLoadParam(string load)
        {
            if (string.IsNullOrEmpty(load) || load.EndsWith(".ascx", StringComparison.InvariantCultureIgnoreCase))
                return load;
            else
                return load + ".ascx";
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            bool updateSuccess = true;
            if (controlToLoad != null)
            {
                // Configure Update button
                var updatableControl = controlToLoad as IUpdatable;
                if (updatableControl != null)
                    updateSuccess = updatableControl.Update();
            }

            if (updateSuccess)
                Return();
        }

        private void LoadResources(IWebObject part)
        {
            // Part Resources
            var pageHeader = WHelper.LoadResources(part.OBJECT_ID, part.Id);
            if (pageHeader != null)
                _resources.AppendLine(pageHeader.ToString());
            //Header.Controls.Add(pageHeader);
        }

        private void DisplayMessage(string message)
        {
            lblStatus.InnerHtml = message;
            lblStatus.Visible = true;
        }
    }
}
