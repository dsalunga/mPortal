using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.WebParts.Content
{
    public partial class _Sections_C_CMS_Custom : UserControl
    {
        protected TextEditor txtContent;
        protected TabControl editTab;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Default Editor
                var isPlainTextEditor = ContentHelper.IsPlainTextDefault();

                IPageElement item = WHelper.GetCurrentWebElement();
                if (item != null)
                {
                    WebContent content = null;
                    var objectContent = WebObjectContent.Get(item);

                    // If has Link and Content
                    if (objectContent != null && (content = objectContent.Content) != null)
                    {
                        LoadContentToForm(content);

                        if (content.IsEditorSensitive)
                            isPlainTextEditor = true;
                    }
                    else
                    {
                        txtTitle.Text = item.Name;
                    }
                }

                txtContent.IsPlainTextDefault = isPlainTextEditor;

                editTab.AddTab("tabEdit", "Edit");
                //editTab.AddTab("tabPublish", "Publish");
                editTab.AddTab("tabHistory", "History");
                editTab.AddTab("tabDraft", "Draft");
                editTab.AddTab("tabInfo", "Details");

                var permissionId = WHelper.GetUserMgmtPermission();

                // Setup Tabs
                if (permissionId == Permissions.ManageInstance)
                {
                    var query = new WQuery(this);
                    query.Set(WConstants.Load, "WM_Custom.ascx");
                    linkManage.HRef = query.BuildQuery();
                }
                else
                {
                    linkViewAll.Visible = false;

                    cmdBrowse.Visible = false;
                    cmdNew.Visible = false;
                }
            }
        }

        private void LoadContentToForm(WebContent item)
        {
            txtContent.Text = item.Content;
            txtTitle.Text = item.Title;
            chkActiveContent.Checked = item.IsActiveContent;
            // Store ID to hidden field
            hidId.Value = item.Id.ToString();
        }

        // Setup tab navigation
        protected void editTab_SelectedTabChanged(object oSender, TabEventArgs args)
        {
            switch (args.TabName)
            {
                case "tabEdit":
                    mtvContent.SetActiveView(viewEdit);
                    break;

                case "tabHistory":
                    mtvContent.SetActiveView(viewHistory);
                    this.BindHistory();
                    break;

                case "tabDraft":
                    mtvContent.SetActiveView(viewDraft);
                    this.BindDrafts();
                    break;

                case "tabPublish":
                    break;

                case "tabInfo":
                    mtvContent.SetActiveView(viewInfo);
                    break;
            }
        }

        private void BindDrafts()
        {
            IPageElement item = WHelper.GetCurrentWebElement();
            WebObjectContent objectContent = null;

            if (item != null && (objectContent = WebObjectContent.Get(item)) != null)
            {
                var items = from i in objectContent.Content.Drafts
                            select new
                            {
                                i.Id,
                                i.Title,
                                Content = DataHelper.GetStringPreview(i.Content, WConstants.PreviewChars),
                                i.DateModified
                            };

                grdDraft.DataSource = items;
                grdDraft.DataBind();
            }
        }

        private void BindHistory()
        {
            IPageElement item = WHelper.GetCurrentWebElement();
            WebObjectContent objectContent = null;

            if (item != null && (objectContent = WebObjectContent.Get(item)) != null)
            {
                var items = from i in objectContent.Content.History
                            select new
                            {
                                i.Id,
                                i.Title,
                                Content = DataHelper.GetStringPreview(i.Content, WConstants.PreviewChars),
                                i.VersionNo,
                                i.DateModified
                            };

                grdHistory.DataSource = items;
                grdHistory.DataBind();
            }
        }

        private void ReadContentFromForm(WebContent item, int siteId, int version)
        {
            item.Title = txtTitle.Text.Trim();
            item.Content = txtContent.Text;
            item.IsActiveContent = chkActiveContent.Checked;
            item.SiteId = siteId;
            item.VersionNo = version;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            PageElementBase element = WHelper.GetCurrentWebElement();
            if (element != null)
            {
                WebObjectContent objContent = WebObjectContent.Get(element);
                if (objContent == null)
                {
                    WebContent item = new WebContent();
                    ReadContentFromForm(item, element.Site.Id, 1);
                    item.Update();

                    item.CreateContentLink(element.OBJECT_ID, element.Id);
                }
                else
                {
                    // Archive or promote item here and create history
                    int contentId = DataHelper.GetId(hidId.Value);
                    if (contentId > 0)
                    {
                        WebContent item = WebContent.Get(contentId);
                        item.PublishNewContent(txtTitle.Text.Trim(), txtContent.Text, chkActiveContent.Checked);
                    }
                    else
                    {
                        WebContent item = new WebContent();
                        ReadContentFromForm(item, element.Site.Id, 1);
                        item.Update();

                        // Update WebObjectContent
                        objContent.ContentId = item.Id;
                        objContent.Update();
                    }
                }

                Return(element.OBJECT_ID);
            }
        }

        private void Return(int objectId)
        {
            QueryParser query = new QueryParser(this);

            switch (objectId)
            {
                case WebObjects.WebPageElement:
                    query.Redirect(CentralPages.WebPageElementHome);
                    break;

                case WebObjects.WebPage:
                    query.Redirect(CentralPages.WebPageHome);
                    break;
            }
        }

        protected void btnIDone_Click(object sender, EventArgs e)
        {

        }

        protected void cmdSaveDraft_Click(object sender, EventArgs e)
        {
            int contentId = DataHelper.GetId(hidId.Value);

            WebContent item = WebContent.Get(contentId);
            if (item != null)
            {
                //hidId.Value = content.CreateDraft(txtTitle.Text, txtContent.Text).ToString();
            }
            else
            {
                // Cases here: 1. New draft w/o existing draft and history
                PageElementBase element = WHelper.GetCurrentWebElement();
                if (element != null)
                {
                    // No related content yet
                    item = new WebContent();
                    ReadContentFromForm(item, element.Site.Id, 0);
                    item.Update();

                    // Create new WebObjectContent
                    item.CreateContentLink(element.OBJECT_ID, element.Id);

                    Return(element.OBJECT_ID);
                }
            }
        }

        protected void grdDraft_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    {
                        WebContent content = WebContent.Get(id);
                        if (content != null)
                        {
                            mtvContent.SetActiveView(viewEdit);
                            editTab.SelectedIndex = 0; // Edit

                            LoadContentToForm(content);
                        }

                        break;
                    }
                case "Custom_Delete":
                    {
                        WebContent content = WebContent.Get(id);
                        if (content.VersionOf > 0)
                            content.Delete();

                        this.BindDrafts();

                        break;
                    }
            }
        }

        protected void grdHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    {
                        WebContent content = WebContent.Get(id);
                        if (content != null)
                        {
                            mtvContent.SetActiveView(viewEdit);
                            editTab.SelectedIndex = 0; // Edit

                            LoadContentToForm(content);
                        }

                        break;
                    }
            }
        }

        protected void cmdEditCurrent_Click(object sender, EventArgs e)
        {
            IPageElement item = WHelper.GetCurrentWebElement();
            if (item != null)
            {
                WebContent content = null;
                var objectContent = WebObjectContent.Get(item);

                if (objectContent != null && (content = objectContent.Content) != null)
                {
                    LoadContentToForm(content);
                }
            }
        }

        protected void cmdBrowse_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.LoadAndRedirect("ConfigOpen.ascx");
        }

        protected void cmdPreview_Click(object sender, EventArgs e)
        {

        }

        protected void cmdNew_Click(object sender, EventArgs e)
        {
            IPageElement element = WHelper.GetCurrentWebElement();
            if (element != null)
            {
                hidId.Value = "-1";
                txtTitle.Text = "";
                txtContent.Text = "";

                WebObjectContent item = WebObjectContent.Get(element);
                if (item != null)
                {
                    item.ContentId = -1;
                    item.Update();
                }
            }
        }
    }
}