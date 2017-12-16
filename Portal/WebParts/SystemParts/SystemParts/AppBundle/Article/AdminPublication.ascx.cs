using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

using WCMS.WebSystem.ViewModel;
using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.WebParts.Article
{
    public partial class AdminPublicationController : UserControl
    {
        #region Designer Code
        
        /// <summary>
        /// elementSelector control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::WCMS.WebSystem.WebParts.Central.Controls.WebSiteElementSelector elementSelector;

        #endregion

        private const string PublishTab = "tabPublish";

        protected TabControl TabControl1;

        protected void Page_Load(object sender, EventArgs e)
        {
            TabControl1.SelectedTabChanged += (object oSender, TabEventArgs args) =>
                {
                    switch (args.TabName)
                    {
                        case "tabGeneral":
                            mtvContent.SetActiveView(viewGeneral);
                            break;

                        case "tabExtended":
                            mtvContent.SetActiveView(viewExtended);
                            break;

                        case PublishTab:
                            mtvContent.SetActiveView(viewPublish);

                            SelectPublishTab();
                            break;
                    }

                    cmdPublish.Visible = args.TabName == PublishTab;
                };

            if (TabControl1.Tabs.Count == 0)
            {
                TabControl1.AddTab("tabGeneral", "General");
                TabControl1.AddTab("tabExtended", "Extended Properties");
                TabControl1.AddTab("tabPublish", "Publishing");
                TabControl1.SelectedTab = "tabGeneral";
            }

            if (!Page.IsPostBack)
            {
                ddlSiteID.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());
                cboSites.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());

                PopulateDate();

                var siteId = DataHelper.GetId(Request, WebColumns.SiteId);
                if (siteId > 0)
                {
                    WebHelper.SetCboValue(cboSites, siteId);
                    grvContent.DataBind();
                }
            }
        }

        private void SelectPublishTab()
        {
            SetSelectedObjects();
            elementSelector.SelectionPartId = WPart.Get("Article").Id;
            elementSelector.PopulateWebSites();
            elementSelector.BuildTree();
        }

        private void PopulateDate()
        {
            int x = 0;

            for (x = 1; x < 32; x++)
                ddlDay.Items.Add(new ListItem(x.ToString(), x.ToString()));
            WebHelper.SetCboValue(ddlDay, DateTime.Now.Day);


            for (x = 1; x < 13; x++)
                ddlMonth.Items.Add(new ListItem(DateTimeFormatInfo.CurrentInfo.GetMonthName(x), x.ToString()));
            WebHelper.SetCboValue(ddlMonth, DateTime.Now.Month);
            

            for (x = DateTime.Now.Year - 50; x < DateTime.Now.Year + 5; x++)
                ddlYear.Items.Add(new ListItem(x.ToString(), x.ToString()));
            WebHelper.SetCboValue(ddlYear, DateTime.Now.Year);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            mtvContent.SetActiveView(viewGeneral);

            litID.Text = "";
            txtAuthor.Text = "";
            txtTitle.Text = "";
            //fckImage.Value = "";
            fckDescription.Value = "";
            fckContent.Value = "";
            txtTags.Text = string.Empty;

            pnlContent.Visible = false;
            pnlContentEdit.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlContent.Visible = true;
            pnlContentEdit.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdateArticle();

            pnlContent.Visible = true;
            pnlContentEdit.Visible = false;
            grvContent.DataBind();
        }

        private int UpdateArticle()
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
            catch
            {
                dtDate = DateTime.Now;
            }

            item.Title = txtTitle.Text.Trim();
            //item.Image = fckImage.Value;
            item.Description = fckDescription.Value;
            item.Date = dtDate;
            item.Content = fckContent.Value;
            item.Tags = txtTags.Text.Trim();
            item.Author = txtAuthor.Text.Trim();
            item.SiteId = DataHelper.GetId(ddlSiteID.SelectedValue);
            item.Active = chkIsActive.Checked ? 1 : 0;
            item.DateModified = DateTime.Now;
            item.ModifiedUserId = WSession.Current.UserId;
            item.Update();

            litID.Text = item.Id.ToString();
            return item.Id;
        }

        protected void grvContent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "ContentEdit":
                    LoadDataEdit(id);
                    break;

                case "Custom_Delete":
                    Article.Delete(id);
                    grvContent.DataBind();
                    break;
            }
        }

        public void LoadDataEdit(int articleId)
        {
            Article item = null;
            if (articleId > 0 && (item = Article.Get(articleId)) != null)
            {
                pnlContent.Visible = false;
                pnlContentEdit.Visible = true;
                litID.Text = item.Id.ToString();
                txtTitle.Text = item.Title;
                txtAuthor.Text = item.Author;
                try
                {
                    DateTime dtDate = item.Date;
                    ddlMonth.SelectedValue = dtDate.Month.ToString();
                    ddlDay.SelectedValue = dtDate.Day.ToString();
                    ddlYear.SelectedValue = dtDate.Year.ToString();
                }
                catch { }

                try
                {
                    ddlSiteID.SelectedValue = item.SiteId.ToString();
                }
                catch { }

                //fckImage.Value = item.Image;
                fckDescription.Value = item.Description;
                txtTags.Text = item.Tags;
                fckContent.Value = item.Content;
                chkIsActive.Checked = item.Active == 1;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strIDs = Request.Form["grvContent"];
            if (!string.IsNullOrEmpty(strIDs))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(strIDs);
                foreach (var sId in ids)
                {
                    Article.Delete(sId);
                }

                grvContent.DataBind();
            }
        }

        protected void cboSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvContent.DataBind();
        }

        protected void cmdDuplicate_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["grvContent"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                //SqlHelper.ExecuteNonQuery(CommandType.Text,
                //    "INSERT INTO P.Items " +
                //    "SELECT Title + ' - Copy' AS Title,Image,description,Date,Content,Author,SiteID,IsActive,@UserId AS UserId,GETDATE() AS DateModified,@UserId AS ModifiedUserId " +
                //    "FROM P.Items WHERE ID IN (" + sChecked + ");",
                //    new SqlParameter("@UserId", Membership.GetUser().ProviderUserKey)
                //);

                grvContent.DataBind();
            }
        }

        public DataSet Select(int siteId)
        {
            WebUser createdBy = null;

            return DataHelper.ToDataSet(
                from i in Article.GetList(siteId)
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

        private void SetSelectedObjects()
        {
            int id = DataHelper.GetId(litID.Text);
            if (id > 0)
            {
                // Remove all unselected
                var locations = ArticleLink.GetList(id);

                List<ObjectKey> selected = new List<ObjectKey>();
                foreach (var loc in locations)
                {
                    selected.Add(new ObjectKey(loc.ObjectId, loc.RecordId));
                }

                elementSelector.SelectedObjects = selected;
            }
        }

        protected void cmdPublish_Click(object sender, EventArgs e)
        {
            int id = UpdateArticle(); //DataHelper.GetDbId(litID.Text);
            if (id > 0)
            {
                // Remove all unselected
                var locations = ArticleLink.GetList(id);
                var unselectedObjects = elementSelector.GetUnselectedObjects();
                foreach (var loc in locations)
                {
                    if (unselectedObjects.Find(item => item.RecordId == loc.RecordId && item.ObjectId == loc.ObjectId) != null)
                    {
                        loc.Delete();
                    }
                }

                locations = ArticleLink.GetList(id);

                // Insert selected objects
                var selectedObjects = elementSelector.GetSelectedObjects();
                foreach (var item in selectedObjects)
                {
                    // Check first if already inserted
                    if (locations.FirstOrDefault(loc => item.RecordId == loc.RecordId && item.ObjectId == loc.ObjectId) == null)
                    {
                        ArticleLink loc = new ArticleLink();
                        loc.ObjectId = item.ObjectId;
                        loc.RecordId = item.RecordId;
                        loc.Style = "";
                        loc.Rank = 1;
                        loc.ArticleId = id;
                        loc.SiteId = elementSelector.SiteId;
                        loc.Update();
                    }
                }
            }
        }
    }
}