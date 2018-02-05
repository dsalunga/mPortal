using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using WCMS.Common.Utilities;

using System.Text;
using WCMS.Framework;
using WCMS.BibleReader.Core;

namespace WCMS.WebSystem.Apps.Integration.Bible
{
    public partial class BibleBrowser : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.MaintainScrollPositionOnPostBack = false;
            if (!IsPostBack)
            {
                var query = new WQuery(this);

                cboLanguages.DataSource = BibleVersionLanguage.Provider.GetList();
                cboLanguages.DataBind();

                var languageId = query.GetInt32("Language", -2);
                WebUtil.SetCboValue(cboLanguages, languageId);

                var view = query.Get("View");
                query.Set("View", "Search");

                var search = query.Get("Search");
                if (!string.IsNullOrEmpty(search))
                {
                    query.Set("Search", WebHelper.UrlEncode(search));
                }

                linkSearch.HRef = query.BuildQuery();

                query.Remove("View");
                query.Remove("Search");
                linkBrowser.HRef = query.BuildQuery();

                var isSearch = !string.IsNullOrEmpty(view) && view.Equals("Search", StringComparison.InvariantCultureIgnoreCase);

                panelSearch.Visible = isSearch;
                panelBookChapters.Visible = !isSearch;
                panelNav.Visible = !isSearch;
                panelNav2.Visible = !isSearch;

                var versionId = query.GetId("Version");
                SetVersions(isSearch, languageId, versionId);

                if (isSearch)
                {
                    tabSearch.Attributes["class"] = "active";

                    if (versionId == -1 && cboVersions.Items.Count > 0)
                        versionId = DataUtil.GetId(cboVersions.Items[0].Value);

                    DisplaySearch(search, versionId);
                }
                else
                {
                    tabBrowser.Attributes["class"] = "active";
                }
            }
        }

        protected void cboVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetBooks();
            var versionId = DataUtil.GetId(cboVersions.SelectedValue);
            var query = new WQuery(this);
            query.Set("Version", versionId);
            query.Redirect();
        }

        private void DisplaySearch(string search, int versionId)
        {
            if (!string.IsNullOrEmpty(search))
            {
                txtSearch.Text = search;
                var version = BibleVersion.Provider.Get(versionId);
                var bookNameId = DataUtil.GetId(Request, "Book"); //DataHelper.GetId(cboBooks.SelectedValue);
                //var chapter = DataHelper.GetInt32(cboChapters.SelectedValue);

                if (bookNameId > 0 && cboBooks.Items.FindByValue(bookNameId.ToString()) == null)
                    bookNameId = -1;

                BibleBookName bookName = bookNameId > 0 ? BibleBookName.Provider.Get(bookNameId) : null;
                var verses = BibleVerse.Provider.Search(version, bookName, search);

                var sb = new StringBuilder();
                //int i = 1;
                foreach (var v in verses)
                {
                    var chapter = v.Chapter;

                    var match = Regex.Match(v.Content, search, RegexOptions.IgnoreCase);
                    var content = v.Content.Replace(match.Value, "<strong class='found'>" + match.Value + "</strong>");

                    //sb.AppendFormat("<sup class='verse-id'>#{4} {0} {1}:{2}</sup>&nbsp;{3}<br/><br/>", chapter.Book.BookName.GetShortName(), chapter.ChapterCode, v.VerseCode, content, i);
                    sb.AppendFormat("<sup class='verse-id'>{0} {1}:{2}</sup>&nbsp;{3}<br/><br/>", chapter.Book.BookName.GetShortName(), chapter.ChapterCode, v.VerseCode, content);
                    //i++;
                }

                panelVerses.InnerHtml = sb.ToString();
                //lblHeader.InnerHtml = string.Format("{0} {1} <span class='lead'>{2}</span>", cboBooks.SelectedItem.Text, cboChapters.SelectedValue, cboVersions.SelectedItem.Text);
                if(verses.Count == 0)
                    lblHeader.InnerHtml = string.Format("Search Results <span class='lead'>for \"{0}\", no match found</span>", search);
                else if(verses.Count == 50)
                    lblHeader.InnerHtml = string.Format("Search Results <span class='lead'>for \"{0}\", showing first 50 results only</span>", search);
                else
                    lblHeader.InnerHtml = string.Format("Search Results <span class='lead'>for \"{0}\", showing {1} results</span>", search, verses.Count);
            }
            else
            {
                lblHeader.Visible = false;
                panelVerses.Visible = false;
            }
        }

        private void SetBooks(bool fromSearch = false)
        {
            int versionId = DataUtil.GetId(cboVersions.SelectedValue);
            if (versionId > 0)
            {
                var version = BibleVersion.Provider.Get(versionId);
                var bookNames = BibleBookName.Provider.GetList(version.BookNameCode);

                var bookNameId = DataUtil.GetId(Request, "Book");
                //var bookNameId = DataHelper.GetId(cboBooks.SelectedValue);
                BibleBookName bookName = bookNameId > 0 ? BibleBookName.Provider.Get(bookNameId) : null;
                BibleBookName newSelectedBookName = bookName != null ? bookNames.Find(i => i.BookCode == bookName.BookCode) : null;

                cboBooks.Items.Clear();
                cboBooks.DataSource = bookNames;
                cboBooks.DataBind();

                if (fromSearch)
                    cboBooks.Items.Insert(0, new ListItem("All Books", "-1"));

                Action selectChapter = () =>
                {
                    var currentChapter = DataUtil.GetInt32(Request, "Chapter", -1); // cboChapters.SelectedValue;
                    if (currentChapter > 0)
                        WebHelper.SetCboValue(cboChapters, currentChapter);
                    else if (currentChapter == 0)
                        WebHelper.SetCboValue(cboChapters, cboChapters.Items[cboChapters.Items.Count - 1].Value);
                };

                if (newSelectedBookName != null)
                {
                    WebHelper.SetCboValue(cboBooks, newSelectedBookName.Id);

                    SetChapters();
                    selectChapter();
                    ShowChapter();
                }
                else
                {
                    cboBooks.SelectedIndex = 0;

                    SetChapters();
                    selectChapter();
                    ShowChapter();
                }
            }
        }

        protected void cboBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetChapters();

            var bookId = DataUtil.GetId(cboBooks.SelectedValue);

            var query = new WQuery(this);
            if (bookId > 0)
                query.Set("Book", bookId);
            else
                query.Remove("Book");

            query.Remove("Chapter");
            query.Redirect();
        }

        private void SetChapters()
        {
            var bookNameId = DataUtil.GetId(cboBooks.SelectedValue);
            if (bookNameId > 0)
            {
                var bookName = BibleBookName.Provider.Get(bookNameId);

                cboChapters.Items.Clear();
                for (int i = 1; i <= bookName.MaxChapter; i++)
                    cboChapters.Items.Add(new ListItem("Chapter " + i, i.ToString()));

                //var chapterId = DataHelper.GetId(Request, "Chapter");
                //if (chapterId > 0)
                //    WebHelper.SetCboValue(cboChapters, chapterId);
            }
        }

        protected void cboChapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ShowChapter();
            var chapterId = DataUtil.GetId(cboChapters.SelectedValue);

            var query = new WQuery(this);
            if (chapterId >= 0)
                query.Set("Chapter", chapterId);
            else
                query.Remove("Chapter");
            query.Redirect();
        }

        private void ShowChapter()
        {
            var versionId = DataUtil.GetId(cboVersions.SelectedValue);
            var bookNameId = DataUtil.GetId(cboBooks.SelectedValue);
            var chapter = DataUtil.GetInt32(cboChapters.SelectedValue);

            var version = BibleVersion.Provider.Get(versionId);
            var bookName = BibleBookName.Provider.Get(bookNameId);

            var verses = BibleVerse.Provider.GetList(version, bookName, chapter);

            var sb = new StringBuilder();
            foreach (var v in verses)
            {
                sb.AppendFormat("<sup class='verse-id'>{0}</sup>&nbsp;{1}<br/><br/>", v.VerseCode, v.Content);
                //sb.Append(v.Content);
                //sb.Append("<br/><br/>");
            }
            panelVerses.InnerHtml = sb.ToString();

            lblHeader.InnerHtml = string.Format("{0} {1} <span class='lead'>{2}</span>", cboBooks.SelectedItem.Text, cboChapters.SelectedValue, cboVersions.SelectedItem.Text);
        }

        protected void cboLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetVersions();
            var query = new WQuery(this);
            query.Set("Language", cboLanguages.SelectedValue);
            query.Redirect();
        }

        private void SetVersions(bool showAllBooks = false, int languageId = -2, int versionId = -1)
        {
            //var langaugeId = DataHelper.GetInt32(cboLanguages.SelectedValue);

            cboVersions.Items.Clear();
            cboVersions.DataSource = BibleVersion.Provider.GetList(languageId);
            cboVersions.DataBind();

            if (versionId > 0)
                WebHelper.SetCboValue(cboVersions, versionId);

            //WebHelper.SetCboValue(cboLanguages, langaugeId);

            //if (setBooks)
            SetBooks(showAllBooks);
        }

        protected void cmdNext_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            var chapterCount = cboChapters.Items.Count;
            var bookCount = cboBooks.Items.Count;

            if (cboChapters.SelectedIndex + 1 == chapterCount)
            {
                if (cboBooks.SelectedIndex + 1 == bookCount)
                    query.Set("Book", cboBooks.Items[0].Value); //cboBooks.SelectedIndex = 0;
                else
                    query.Set("Book", cboBooks.Items[cboBooks.SelectedIndex + 1].Value); //cboBooks.SelectedIndex += 1;

                //SetChapters();
                query.Remove("Chapter");
            }
            else
            {
                query.Set("Chapter", cboChapters.Items[cboChapters.SelectedIndex + 1].Value);
                //cboChapters.SelectedIndex += 1;
                //ShowChapter();
            }

            query.Redirect();
        }

        protected void cmdPrevious_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            var chapters = cboChapters.Items.Count;
            var books = cboBooks.Items.Count;

            if (cboChapters.SelectedIndex == 0)
            {
                if (cboBooks.SelectedIndex == 0)
                {
                    //cboBooks.SelectedIndex = cboBooks.Items.Count - 1;
                    query.Set("Book", cboBooks.Items[cboBooks.Items.Count - 1].Value);
                }
                else
                {
                    //cboBooks.SelectedIndex -= 1;
                    query.Set("Book", cboBooks.Items[cboBooks.SelectedIndex - 1].Value);
                }

                //SetChapters(false);

                //cboChapters.SelectedIndex = cboChapters.Items.Count - 1;
                query.Set("Chapter", 0); //cboChapters.Items[cboChapters.Items.Count - 1].Value);
                //ShowChapter();
            }
            else
            {
                //cboChapters.SelectedIndex -= 1;
                query.Set("Chapter", cboChapters.Items[cboChapters.SelectedIndex - 1].Value);
                //ShowChapter();
            }

            query.Redirect();
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            var search = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                //int versionId = DataHelper.GetId(cboVersions.SelectedValue);

                var query = new WQuery(this);
                query.Set("Search", search);
                //query.Set("Version", versionId);
                query.Redirect();
            }
        }
    }
}