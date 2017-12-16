using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;

using WCMS.Common.Utilities;

using WCMS.WebSystem.Controls;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.ViewModel;

using WCMS.WebSystem.WebParts.Central.Controls;

namespace WCMS.WebSystem.WebParts.Article
{
    public partial class ConfigArticleController : UserControl
    {
        protected TabControl tabArticle;
        protected WebSiteElementSelector elementSelector;
        protected SaveInFolder saveToFolder;
        protected SaveInFolder articleFolder;

        private const string PublishTab = "tabPublish";
        private const string GeneralTab = "tabGeneral";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var items = WebSiteViewModel.GenerateListItem(-1).ToArray();
                cboSiteId.Items.AddRange(items);
                cboSites.Items.AddRange(items);

                var siteId = DataHelper.GetId(Request, WebColumns.SiteId);
                if (cboSites.Items.FindByValue(siteId.ToString()) != null)
                    cboSites.SelectedValue = siteId.ToString();

                var pair = WHelper.GetObjectStruct();
                hidObjectId.Value = pair.ObjectId.ToString();
                hidRecordId.Value = pair.RecordId.ToString();

                cboTemplate.DataBind();
                LoadTemplate();

                #region Populate Date

                // Days
                for (int x = 1; x < 32; x++)
                    ddlDay.Items.Add(new ListItem(x.ToString(), x.ToString()));

                WebHelper.SetCboValue(ddlDay, DateTime.Now.Day);

                // Months
                for (int x = 1; x < 13; x++)
                    ddlMonth.Items.Add(new ListItem(DateTimeFormatInfo.CurrentInfo.GetMonthName(x), x.ToString()));

                WebHelper.SetCboValue(ddlMonth, DateTime.Now.Month);

                // Years
                for (int x = DateTime.Now.Year - 45; x < DateTime.Now.Year + 10; x++)
                    ddlYear.Items.Add(new ListItem(x.ToString(), x.ToString()));

                WebHelper.SetCboValue(ddlYear, DateTime.Now.Year);

                #endregion

                LoadDataRSS();

                if (AdminTabControl1.PermissionId != Permissions.ManageInstance)
                    panelInsertExisting.Visible = false;


                var defaultView = DataHelper.GetInt32(Request, "DefaultView");
                switch (defaultView)
                {
                    case 1:
                        mtvInstance.SetActiveView(viwSettings);
                        LoadTemplate();
                        break;

                    default:
                        mtvInstance.SetActiveView(viwContent);

                        var articleId = DataHelper.GetId(Request, "ArticleId");
                        if (articleId > 0)
                            LoadEditView(articleId);

                        break;
                }
            }
        }

        protected void tabArticle_SelectedTabChanged(object oSender, TabEventArgs args)
        {
            switch (args.TabName)
            {
                case GeneralTab:
                    mtvContent.SetActiveView(viwBasic);
                    break;

                case "tabExtended":
                    mtvContent.SetActiveView(viwAdvance);
                    break;

                case PublishTab:
                    mtvContent.SetActiveView(viewPublish);

                    SelectPublishTab();
                    break;
            }

            cmdPublish.Visible = args.TabName == PublishTab;
        }

        #region Publishing-specific Methods

        private void SelectPublishTab()
        {
            var query = new WQuery(this);

            SetSelectedObjects();
            elementSelector.SelectionPartId = WPart.Get("Article").Id;

            elementSelector.PopulateWebSites();
            elementSelector.SiteId = WPage.Get(query.GetId(WebColumns.PageId)).SiteId;

            elementSelector.BuildTree();
        }

        private void SetSelectedObjects()
        {
            int id = DataHelper.GetId(litID.Text);
            if (id > 0)
            {
                // Remove all unselected
                var links = ArticleLink.GetList(id);

                var selected = new List<ObjectKey>();
                foreach (var link in links)
                    selected.Add(new ObjectKey(link.ObjectId, link.RecordId));

                elementSelector.SelectedObjects = selected;
            }
        }

        protected void cmdPublish_Click(object sender, EventArgs e)
        {
            int id = UpdateArticle().Id;
            if (id > 0)
            {
                // Remove all unselected
                var locations = ArticleLink.GetList(id);
                var unselectedObjects = elementSelector.GetUnselectedObjects();
                foreach (var loc in locations)
                    if (unselectedObjects.Find(item => item.RecordId == loc.RecordId && item.ObjectId == loc.ObjectId) != null)
                        loc.Delete();

                locations = ArticleLink.GetList(id);

                // Insert selected objects
                var selectedObjects = elementSelector.GetSelectedObjects();
                foreach (var item in selectedObjects)
                {
                    // Check first if already inserted
                    if (locations.FirstOrDefault(loc => item.RecordId == loc.RecordId && item.ObjectId == loc.ObjectId) == null)
                    {
                        ArticleLink link = new ArticleLink();
                        link.ObjectId = item.ObjectId;
                        link.RecordId = item.RecordId;
                        link.Style = "";
                        link.Rank = 1;
                        link.ArticleId = id;
                        link.SiteId = elementSelector.SiteId;
                        link.CommentOn = DataHelper.GetId(cboCommentOn.SelectedValue);
                        link.Update();
                    }
                }

                if (chkSendEmail.Checked)
                    CheckSendEmail(id);
            }
        }

        private Article UpdateArticle()
        {
            int id = DataHelper.GetId(litID.Text);
            Article item = null;

            if (id > 0 && (item = Article.Get(id)) != null)
            { }
            else
            {
                item = new Article();
                item.UserId = WSession.Current.UserId;
            }

            DateTime dtDate;
            try
            {
                dtDate = new DateTime(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue), int.Parse(ddlDay.SelectedValue));
            }
            catch (Exception)
            {
                dtDate = DateTime.Now;
            }

            item.Title = txtTitle.Text.Trim();
            //item.Image = fckImage.Value;
            item.Description = fckDescription.Value;
            item.Date = dtDate;
            item.Content = fckContent.Value;
            item.Author = txtAuthor.Text.Trim();
            item.SiteId = DataHelper.GetId(cboSiteId.SelectedValue);
            item.Active = chkIsActive.Checked ? 1 : 0;
            item.DateModified = DateTime.Now;
            item.ModifiedUserId = WSession.Current.UserId;
            item.Tags = txtTags.Text.Trim(); 

            litID.Text = item.Update().ToString();
            return item;
        }

        #endregion

        private void LoadDataRSS()
        {
            var item = WHelper.GetCurrentWebElement();
            string path = Request.Url.IsDefaultPort ?
                "http://" + Request.Url.Host :
                string.Format("http://{0}:{1}", Request.Url.Host, Request.Url.Port);

            if (item.OBJECT_ID == WebObjects.WebPage)
            {
                linkRSS.HRef = string.Format("{0}/Content/Parts/Article/RSS.aspx?{1}={2}",
                    path, Article.ArticleRSSQueryKey, item.Id);
                linkRSS.InnerHtml = linkRSS.HRef;
                linkRSS.Title = item.Name;
            }
            else
            {
                linkRSS.InnerHtml = "N/A";
            }
        }

        protected void grvContent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "ContentEdit":
                    LoadEditView(DataHelper.GetId(e.CommandArgument));
                    break;

                case "A_SendEmail":
                    var query = new WQuery(this);
                    query.Set(ArticleConstants.ArticleId, e.CommandArgument);
                    query.LoadAndRedirect("ConfigSendEmail.ascx");
                    break;
            }
        }

        private void LoadEditView(int articleId)
        {
            LoadDataRank(false);
            LoadDataEdit(articleId);

            tabArticle.SelectedTab = GeneralTab;
        }

        public void LoadDataEdit(int id)
        {
            var item = WHelper.GetCurrentWebElement();
            ArticleLink link = null;

            if (item != null && (link = ArticleLink.Get(item.OBJECT_ID, item.Id, id)) != null)
            {
                OpenArticleView(viwBasic);

                var article = link.Article;
                litID.Text = article.Id.ToString();
                txtTitle.Text = article.Title;

                DateTime dtDate = article.Date;
                ddlMonth.SelectedValue = dtDate.Month.ToString();
                ddlDay.SelectedValue = dtDate.Day.ToString();
                ddlYear.SelectedValue = dtDate.Year.ToString();

                if (ddlRank.Items.FindByValue(link.Rank.ToString()) != null)
                    ddlRank.SelectedValue = link.Rank.ToString();

                if (cboSiteId.Items.FindByValue(article.SiteId.ToString()) != null)
                    cboSiteId.SelectedValue = article.SiteId.ToString();

                txtAuthor.Text = article.Author;
                fckDescription.Value = article.Description;
                fckContent.Value = article.Content;
                txtTags.Text = article.Tags;
                chkIsActive.Checked = article.Active == 1;

                // COMMENTS
                panelComment.Visible = true;
                WebHelper.SetCboValue(cboCommentOn, link.CommentOn);

                // Load from WebFS
                saveToFolder.FolderId = saveToFolder.GetFolder(Article.ID, article.Id);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var item = WHelper.GetCurrentWebElement();
            var article = UpdateArticle();
            // Save location to WebFS
            if (saveToFolder.FolderId > -2)
                saveToFolder.Update(article.Title, Article.ID, article.Id);

            var link = ArticleLink.Get(item.OBJECT_ID, item.Id, article.Id);
            if (link == null)
                link = new ArticleLink();

            link.ObjectId = item.OBJECT_ID;
            link.RecordId = item.Id;
            link.Rank = DataHelper.GetInt32(ddlRank.SelectedValue);
            link.ArticleId = article.Id;
            link.SiteId = DataHelper.GetId(cboSiteId.SelectedValue);
            link.CommentOn = DataHelper.GetId(cboCommentOn.SelectedValue);
            link.Update();

            panelComment.Visible = false;
            pnlContent.Visible = true;
            pnlContentEdit.Visible = false;

            grvContent.DataBind();
            grvContentAll.DataBind();
            if (chkSendEmail.Checked)
                CheckSendEmail(article.Id);
        }

        private void CheckSendEmail(int articleId)
        {
            var query = new WQuery(this);
            query.Set("ArticleId", articleId);
            query.LoadAndRedirect("ConfigSendEmail.ascx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlContent.Visible = true;
            pnlContentEdit.Visible = false;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            litID.Text = string.Empty;
            txtAuthor.Text = string.Empty;
            txtTitle.Text = string.Empty;
            fckDescription.Value = string.Empty;
            fckContent.Value = string.Empty;
            txtTags.Text = string.Empty;
            cboCommentOn.SelectedIndex = 0;

            OpenArticleView(viwBasic);

            LoadDataRank(true);

            //chkSendEmail.Checked = true;
        }

        private void LoadDataRank(bool IsAdding)
        {
            ddlRank.Items.Clear();

            IPageElement item = WHelper.GetCurrentWebElement();
            IEnumerable<ArticleLink> locations = null;

            if (item != null && (locations = ArticleLink.GetList(item.OBJECT_ID, item.Id)).Count() > 0)
            {
                int x = 0;
                foreach (ArticleLink location in locations)
                {
                    x += 1;
                    ddlRank.Items.Add(new ListItem(x.ToString(), x.ToString()));
                }

                if (IsAdding)
                {
                    x += 1;
                    ddlRank.Items.Add(new ListItem(x.ToString(), x.ToString()));
                }
                ddlRank.SelectedValue = x.ToString();
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["grvContentAll"];
            if (!string.IsNullOrEmpty(ids))
            {
                IPageElement item = WHelper.GetCurrentWebElement();
                var strIds = DataHelper.ParseCommaSeparatedIdList(ids);
                var list = ArticleList.Get(item.OBJECT_ID, item.Id);

                if (list != null && list.FolderId > 0)
                {
                    var files = from f in WebFile.Provider.GetList(list.FolderId)
                                where f.ObjectId == item.OBJECT_ID
                                select f;

                    foreach (var id in strIds)
                    {
                        if (files.FirstOrDefault(f => f.Id == id) == null)
                        {
                            Article article = Article.Get(id);

                            WebFile file = new WebFile();
                            file.Name = article.Title;
                            file.ObjectId = Article.ID;
                            file.RecordId = id;
                            file.FolderId = list.FolderId;
                            file.Update();
                        }
                    }
                }
                else
                {
                    var locations = ArticleLink.GetList(item.OBJECT_ID, item.Id);
                    int rank = locations.Count();

                    foreach (var id in strIds)
                    {
                        rank += 1;
                        var loc = new ArticleLink
                        {
                            Rank = rank,
                            ArticleId = id,
                            ObjectId = item.OBJECT_ID,
                            RecordId = item.Id,
                            SiteId = item.OBJECT_ID == WebObjects.WebSite ? item.Id : -1,
                            Style = string.Empty
                        };

                        loc.Update();
                    }
                }

                grvContent.DataBind();
                grvContentAll.DataBind();
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["grvContent"];
            if (!String.IsNullOrEmpty(ids))
            {
                var strIds = DataHelper.ParseCommaSeparatedIdList(ids);
                var item = WHelper.GetCurrentWebElement();
                var list = ArticleList.Get(item.OBJECT_ID, item.Id);

                if (list != null && list.FolderId > 0)
                {
                    var links = from f in WebFile.Provider.GetList(list.FolderId)
                                where f.ObjectId == Article.ID
                                select f;

                    foreach (var id in strIds)
                    {
                        var link = links.FirstOrDefault(f => f.RecordId == id);
                        if (link != null)
                            link.Delete();
                    }
                }
                else
                {
                    foreach (int locationId in strIds)
                        ArticleLink.Delete(locationId);
                }

                grvContent.DataBind();
                grvContentAll.DataBind();
            }
        }

        private void SaveListConfiguration()
        {
            IPageElement item = WHelper.GetCurrentWebElement();

            //string dateFormatString = txtDateFormat.Text.Trim();
            int pageSize = DataHelper.GetInt32(txtPageSize.Text.Trim(), 10);

            ArticleList list = ArticleList.Get(item.OBJECT_ID, item.Id);
            if (list == null)
            {
                list = new ArticleList();
                list.ObjectId = item.OBJECT_ID;
                list.RecordId = item.Id;
            }

            list.SiteId = item.OBJECT_ID == WebObjects.WebSite ? item.Id : -1;
            list.TemplateId = DataHelper.GetId(cboTemplate.SelectedValue);

            list.PageSize = pageSize;
            //link.DateFormatString = dateFormatString;
            list.CommentOn = chkComments.Checked ? 1 : 0;
            list.FolderId = articleFolder.FolderId;
            list.Update();
        }

        private void LoadTemplate()
        {
            if (cboTemplate.Items.Count > 0)
            {
                IPageElement item = WHelper.GetCurrentWebElement();
                ArticleList list = null;
                if (item != null && (list = ArticleList.Get(item.OBJECT_ID, item.Id)) != null)
                {
                    if (cboTemplate.Items.FindByValue(list.TemplateId.ToString()) != null)
                        cboTemplate.SelectedValue = list.TemplateId.ToString();

                    txtPageSize.Text = list.PageSize.ToString();
                    chkComments.Checked = list.CommentOn == 1;
                    //txtDateFormat.Text = link.DateFormatString;

                    if (list.FolderId > 0)
                        articleFolder.FolderId = list.FolderId;
                }
                else
                {
                    SaveListConfiguration();
                }
            }
        }

        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDataTemplateSelected(int.Parse(ddlTemplate.SelectedValue));
        }

        protected void btnExisting_Click(object sender, EventArgs e)
        {
            mtvItems.SetActiveView(viwExisting);
        }

        protected void btnIDone_Click(object sender, EventArgs e)
        {
            mtvItems.SetActiveView(viwInserted);
        }

        private void OpenArticleView(View view)
        {
            if (pnlContent.Visible)
                pnlContent.Visible = false;
            if (!pnlContentEdit.Visible)
                pnlContentEdit.Visible = true;

            if (view != null)
                mtvContent.SetActiveView(view);

            if (tabArticle.Tabs.Count == 0)
            {
                var permissionId = WHelper.GetUserMgmtPermission();

                // Setup Article Tabs
                tabArticle.AddTab("tabGeneral", "General");
                tabArticle.AddTab("tabExtended", "Extended Properties");

                if (permissionId == Permissions.ManageInstance)
                    tabArticle.AddTab("tabPublish", "Publishing");

                tabArticle.SelectedTab = "tabGeneral";
            }
        }

        protected void cmdConfigSave_Click(object sender, EventArgs e)
        {
            //EmptyHidden();

            SaveListConfiguration();
            lblStatus.Text = "Configuration updated.";

            //AdminTabControl1.TabControl.SelectedTab = "tabArticles";
        }

        protected void cboSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvContentAll.DataBind();
        }

        protected void cmdDuplicate_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["grvContentAll"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(sChecked);

                grvContentAll.DataBind();
            }
        }

        protected void grvContentAll_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int articleId = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Delete":
                    Article.Delete(articleId);
                    grvContentAll.DataBind();
                    break;
            }
        }

        public IEnumerable<ArticleTemplate> SelectTemplates()
        {
            return ArticleTemplate.GetList();
        }

        public DataSet SelectLocations(int objectId, int recordId, int siteId)
        {
            WebUser createdBy = null;
            Article article = null;

            var list = ArticleList.Get(objectId, recordId);

            if (list != null && list.FolderId > 0)
            {
                return DataHelper.ToDataSet(from a in list.Articles
                                            orderby a.Date descending
                                            select new
                                            {
                                                a.Id,
                                                ArticleId = a.Id,
                                                a.Title,
                                                a.Date,
                                                Rank = -1,
                                                a.Author,
                                                CreatedByName = (createdBy = a.CreatedBy) == null ? "" : createdBy.FirstAndLastName
                                            }
                           );
            }
            else
            {
                return DataHelper.ToDataSet(from a in ArticleLink.GetList(objectId, recordId, siteId)
                                            orderby a.Article.Date descending
                                            where (article = a.Article) != null
                                            select new
                                            {
                                                a.Id,
                                                a.ArticleId,
                                                article.Title,
                                                article.Date,
                                                a.Rank,
                                                article.Author,
                                                CreatedByName = (createdBy = article.CreatedBy) == null ? "" : createdBy.FirstAndLastName
                                            }
                           );
            }
        }

        public DataSet SelectArticles(int siteId, int pageId, int pageElementId)
        {
            WebUser createdBy = null;
            var items = Article.GetList(siteId);
            IEnumerable<Article> subItems = null;

            if (pageElementId > 0)
            {
                var inserted = Article.GetList(WebObjects.WebPageElement, pageElementId);
                subItems = from i in items
                           where inserted.FirstOrDefault(item => item != null && item.Id == i.Id) == null // where not inserted
                           select i;
            }
            else if (pageId > 0)
            {
                var inserted = Article.GetList(WebObjects.WebPage, pageId);
                subItems = from i in items
                           where inserted.FirstOrDefault(item => item.Id == i.Id) == null // where not inserted
                           select i;
            }
            else
            {
                subItems = from i in items
                           select i;
            }

            return DataHelper.ToDataSet(from i in subItems
                                        orderby i.Date descending
                                        select new
                                        {
                                            i.Id,
                                            i.Title,
                                            i.Date,
                                            i.Author,
                                            i.Active,
                                            CreatedByName = (createdBy = i.CreatedBy) == null ? "" : createdBy.FirstAndLastName
                                        });
        }
    }
}