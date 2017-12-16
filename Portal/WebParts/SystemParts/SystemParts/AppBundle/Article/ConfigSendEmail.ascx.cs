using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;


namespace WCMS.WebSystem.WebParts.Article
{
    public partial class ConfigSendEmail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var query = new WQuery(this);

                int id = query.GetId("ArticleId");
                int pageId = query.GetId(WebColumns.PageId);

                var item = Article.Get(id);
                if (item != null && pageId > 0)
                {
                    var page = WPage.Get(pageId);
                    var site = page.Site;
                    var shortName = site.ShortName;

                    linkPreview.HRef += string.Format("{0}&PageId={1}", id, pageId);
                    txtSubject.Text = (!string.IsNullOrEmpty(shortName) ? string.Format("[{0}] ", shortName) : WConfig.SubjectPrefix) + item.Title;
                }

                CheckEnableSend();

                SetCurrentView("");
            }
        }

        private void CheckEnableSend()
        {
            cmdSend.Enabled = GridView1.Rows.Count > 0;
        }

        private void SetCurrentView(string viewRecipients)
        {
            // View Type
            hView.Value = viewRecipients;
            if (viewRecipients == "Users")
            {
                lblCurrentView.InnerHtml = "Users";

                // Users View
                cmdChangeView.Text = "Groups";
            }
            else
            {
                lblCurrentView.InnerHtml = "Groups";

                // Groups View
                cmdChangeView.Text = "Users";
            }
        }

        public DataSet GetGroups(int pageId, string view, string customRecipients, string exclude)
        {
            var subs = WebSubscription.Provider.GetList(WebObjects.WebGroup, -1, WPart.Get("Article").Id, pageId, 1);
            var includeList = DataHelper.ParseDelimitedStringToList(customRecipients, AccountConstants.AccountDelimiter);
            var excludeList = DataHelper.ParseDelimitedStringToList(exclude, AccountConstants.AccountDelimiter);

            var includeUserList = new List<WebUser>();
            var includeGroupList = new List<WebGroup>();

            var excludeUserList = new List<WebUser>();
            var excludeGroupList = new List<WebGroup>();

            if (includeList.Count > 0)
            {
                // Convert includeList to lists for Users and Groups
                foreach (string include in includeList)
                {
                    var parts = include.Split(AccountConstants.AccountSplitter);
                    int objectId = DataHelper.GetId(parts.First());
                    int recordId = DataHelper.GetId(parts[1]);

                    if (objectId > 0 && recordId > 0)
                    {
                        if (objectId == WebObjects.WebUser)
                        {
                            var user = WebUser.Get(recordId);
                            if (user != null)
                                includeUserList.Add(user);
                        }
                        else if (objectId == WebObjects.WebGroup)
                        {
                            var group = WebGroup.Get(recordId);
                            if (group != null)
                                includeGroupList.Add(group);
                        }
                    }
                }
            }

            if (excludeList.Count > 0)
            {
                // Convert exclude to lists for Users and Groups
                foreach (string ex in excludeList)
                {
                    var parts = ex.Split('\\');
                    int objectId = DataHelper.GetId(parts.First());
                    int recordId = DataHelper.GetId(parts[1]);

                    if (objectId > 0 && recordId > 0)
                    {
                        if (objectId == WebObjects.WebUser)
                        {
                            var user = WebUser.Get(recordId);
                            if (user != null)
                                excludeUserList.Add(user);
                        }
                        else if (objectId == WebObjects.WebGroup)
                        {
                            var group = WebGroup.Get(recordId);
                            if (group != null)
                                excludeGroupList.Add(group);
                        }
                    }
                }
            }


            // Check view type
            if (view == "Users")
            {
                // Get all users in default groups, excluding exclude groups
                var users = new List<WebUser>();
                foreach (var sub in subs)
                {
                    if (sub.ObjectId == WebObjects.WebGroup)
                    {
                        if (excludeGroupList.Find(eg => eg.Id == sub.RecordId) == null)
                        {
                            var group = WebGroup.Get(sub.RecordId);
                            users.AddRange(group.Users.Where(i => i.IsActive));
                        }
                    }
                }

                // Get all users in custom group list
                if (includeGroupList.Count > 0)
                {
                    foreach (var group in includeGroupList)
                        users.AddRange(group.Users.Where(i => i.IsActive));
                }

                // Include custom user list
                if (includeUserList.Count > 0)
                    users.AddRange(includeUserList);

                if (excludeUserList.Count > 0)
                    users = users.Except(excludeUserList).ToList();

                return DataHelper.ToDataSet(
                       (from user in users
                        select new
                        {
                            Id = user.ToUniqueShortString(),
                            Name = user.FullName,
                            user.Email,
                            user.UserName
                        }
                    ).Distinct());
            }
            else
            {
                WebGroup group = null;

                foreach (var sub in subs)
                {
                    // Filter out groups in exclude list
                    if (sub.ObjectId == WebObjects.WebGroup && excludeGroupList.Find(g => g.Id == sub.RecordId) == null)
                    {
                        group = WebGroup.Get(sub.RecordId);
                        if (group != null)
                            includeGroupList.Add(group);
                    }
                }

                var result = from g in includeGroupList
                             select new
                             {
                                 Id = g.ToUniqueShortString(),
                                 g.Name,
                                 Email = "",
                                 UserName = ""
                             };

                if (includeUserList.Count > 0)
                {
                    result = result.Union(from user in includeUserList
                                          select new
                                          {
                                              Id = user.ToUniqueShortString(),
                                              Name = user.FullName,
                                              user.Email,
                                              user.UserName
                                          });
                }

                return DataHelper.ToDataSet(result);
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            ReturnPage();
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.UnloadAndRedirect();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Custom_Delete":
                    var query = new WQuery(this);
                    string shortString = e.CommandArgument.ToString();

                    bool isIncluded = false;

                    // Check if present in custom recipients list
                    var includedList = DataHelper.ParseDelimitedStringToList(hRecipients.Value.Trim(), AccountConstants.AccountDelimiter);
                    if (includedList.Count > 0)
                    {
                        foreach (var included in includedList)
                        {
                            if (included == shortString)
                            {
                                isIncluded = true;
                                includedList.Remove(included);
                                hRecipients.Value = DataHelper.ToDelimitedString(includedList, AccountConstants.AccountDelimiter);
                                break;
                            }
                        }
                    }

                    // If not included in custom recipients, put it in exclude list
                    if (!isIncluded)
                    {
                        var excludedList = DataHelper.ParseDelimitedStringToList(hExcluded.Value.Trim(), AccountConstants.AccountDelimiter);
                        if (excludedList.Find(i => i == shortString) == null)
                        {
                            excludedList.Add(shortString);
                            hExcluded.Value = DataHelper.ToDelimitedString(excludedList, AccountConstants.AccountDelimiter);
                        }
                    }

                    GridView1.DataBind();
                    CheckEnableSend();
                    break;
            }
        }

        protected void cmdSend_Click(object sender, EventArgs e)
        {
            int id = DataHelper.GetId(Request, "ArticleId");
            int pageId = DataHelper.GetId(Request, WebColumns.PageId);
            var item = Article.Get(id);
            var page = WPage.Get(pageId);
            if (item != null && page != null)
            {
                var subs = WebSubscription.Provider.GetList(WebObjects.WebGroup, -1, WPart.Get("Article").Id, pageId, 1);
                var includeList = DataHelper.ParseDelimitedStringToList(hRecipients.Value, AccountConstants.AccountDelimiter);
                var excludeList = DataHelper.ParseDelimitedStringToList(hExcluded.Value, AccountConstants.AccountDelimiter);

                var includeUserList = new List<WebUser>();
                var includeGroupList = new List<WebGroup>();

                var excludeUserList = new List<WebUser>();
                var excludeGroupList = new List<WebGroup>();

                if (includeList.Count > 0)
                {
                    // Convert includeList to lists for Users and Groups
                    foreach (string include in includeList)
                    {
                        var parts = include.Split(AccountConstants.AccountSplitter);
                        int objectId = DataHelper.GetId(parts.First());
                        int recordId = DataHelper.GetId(parts[1]);

                        if (objectId > 0 && recordId > 0)
                        {
                            if (objectId == WebObjects.WebUser)
                            {
                                var user = WebUser.Get(recordId);
                                if (user != null)
                                    includeUserList.Add(user);
                            }
                            else if (objectId == WebObjects.WebGroup)
                            {
                                var group = WebGroup.Get(recordId);
                                if (group != null)
                                    includeGroupList.Add(group);
                            }
                        }
                    }
                }

                if (excludeList.Count > 0)
                {
                    // Convert exclude to lists for Users and Groups
                    foreach (string ex in excludeList)
                    {
                        string[] parts = ex.Split(AccountConstants.AccountSplitter);

                        int objectId = DataHelper.GetId(parts.First());
                        int recordId = DataHelper.GetId(parts[1]);

                        if (objectId > 0 && recordId > 0)
                        {
                            if (objectId == WebObjects.WebUser)
                            {
                                var user = WebUser.Get(recordId);
                                if (user != null)
                                    excludeUserList.Add(user);
                            }
                            else if (objectId == WebObjects.WebGroup)
                            {
                                var group = WebGroup.Get(recordId);
                                if (group != null)
                                    excludeGroupList.Add(group);
                            }
                        }
                    }
                }

                // Get all users in default groups, excluding exclude groups
                List<WebUser> users = new List<WebUser>();
                foreach (var sub in subs)
                {
                    if (sub.ObjectId == WebObjects.WebGroup)
                    {
                        if (excludeGroupList.Find(eg => eg.Id == sub.RecordId) == null)
                        {
                            var group = WebGroup.Get(sub.RecordId);
                            users.AddRange(group.Users.Where(i => i.IsActive));
                        }
                    }
                }

                // Get all users in custom group list
                if (includeGroupList.Count > 0)
                    foreach (var group in includeGroupList)
                        users.AddRange(group.Users.Where(i => i.IsActive));

                // Include custom user list
                if (includeUserList.Count > 0)
                    users.AddRange(includeUserList);

                if (excludeUserList.Count > 0)
                    users = users.Except(excludeUserList).ToList();

                var userEmails = (from user in users
                                  select user.Email).Distinct();

                if (userEmails.Count() > 0)
                {
                    var site = page.Site;

                    // Prepare Email
                    NamedValueProvider values = new NamedValueProvider();
                    values.Add("Title", item.Title);
                    values.Add("PublishedDate", string.Format("{0:MMMM d, yyyy}", item.Date));
                    values.Add("Content", item.Content);
                    values.Add("BaseAddress", site.BuildAbsoluteUrl());
                    values.Add("Permalink", string.Format("{0}?ArticleId={1}", page.BuildAbsoluteUrl(), item.Id));

                    string emailPath = page.GetParameterValue(ArticleConstants.EmailTemplateFileKey, "");
                    if (string.IsNullOrWhiteSpace(emailPath))
                        emailPath = site.GetPartConfig(Article.ArticleIdentity, ArticleConstants.EmailTemplateFileKey);

                    if(string.IsNullOrEmpty(emailPath))
                        emailPath = "~/Content/Parts/Article/Templates/EmailAlert.htm";

                    string emailContent = FileHelper.ReadFile(MapPath(emailPath));
                    emailContent = Substituter.Substitute(emailContent, values);

                    // Format relative paths to absolute
                    emailContent = CmsEmail.FixPaths(emailContent);

                    CmsEmail smtp = new CmsEmail();
                    smtp.Subject = txtSubject.Text.Trim();

                    switch (radioSendAs.SelectedIndex)
                    {
                        case 0: // To
                            smtp.MailTo.AddRange(userEmails);
                            break;

                        case 1: // CC
                            smtp.MailCC.AddRange(userEmails);
                            break;

                        case 2: // BCC
                            smtp.MailBCC.AddRange(userEmails);
                            break;
                    }

                    smtp.Message = emailContent;
                    smtp.ReplyTo.Clear();
                    smtp.ReplyTo.Add(WSession.Current.User.Email);
                    smtp.SetAlwaysBCC();
                    smtp.SendMail();

                    ReturnPage();
                }
                else
                {
                    lblStatus.InnerHtml = "There are no recipients in the list.";
                }
            }
            else
            {
                lblStatus.InnerHtml = "Invalid parameters";
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            int pageId = DataHelper.GetId(Request, WebColumns.PageId);
            var subs = WebSubscription.Provider.GetList(WebObjects.WebGroup, -1, WPart.Get("Article").Id, pageId, 1);
            var includeList = DataHelper.ParseDelimitedStringToList(hRecipients.Value, AccountConstants.AccountDelimiter);
            var excludeList = DataHelper.ParseDelimitedStringToList(hExcluded.Value, AccountConstants.AccountDelimiter);


            bool accountAdded = false;

            string newAccounts = txtAdd.Text.Trim();
            if (!string.IsNullOrEmpty(newAccounts))
            {
                var accountList = DataHelper.ParseDelimitedStringToList(newAccounts, AccountConstants.AccountDelimiter);
                if (accountList.Count > 0)
                {
                    List<WebGroup> groups = new List<WebGroup>();
                    foreach (var sub in subs)
                    {
                        var group = WebGroup.Get(sub.RecordId);
                        if (group != null)
                            groups.Add(group);
                    }

                    foreach (string accountString in accountList)
                    {
                        string shortString = string.Empty;
                        var account = AccountHelper.FromAccountString(accountString);

                        if (account == null)
                            continue;

                        if (account.OBJECT_ID == WebObjects.WebUser)
                            shortString = (account as WebUser).ToUniqueShortString();
                        else if (account.OBJECT_ID == WebObjects.WebGroup)
                            shortString = (account as WebGroup).ToUniqueShortString();
                        else
                            continue;

                        if (!string.IsNullOrWhiteSpace(shortString))
                        {
                            if (excludeList.Find(i => i == shortString) != null)
                            {
                                if (!accountAdded)
                                    accountAdded = true;

                                excludeList.Remove(shortString);
                                continue;
                            }

                            if (groups.Count > 0 && AccountHelper.GetObjectId(shortString) == WebObjects.WebGroup)
                            {
                                if (groups.Find(i => i.ToUniqueShortString() == shortString) != null)
                                    continue;
                            }

                            // Check if existing in custom included list, if not then add
                            if (includeList.Find(i => i == shortString) == null)
                            {
                                if (!accountAdded)
                                    accountAdded = true;
                                includeList.Add(shortString);
                            }
                        }

                        // Check if existing in

                        //if (existing.Find(ex =>
                        //    ex.ObjectId == key.ObjectId
                        //    && ex.RecordId == key.RecordId
                        //    && ex.SecurityObjectId == objectId
                        //    && ex.SecurityRecordId == securityRecordId) == null)
                        //{
                        //    WebObjectSecurity item = new WebObjectSecurity();
                        //    item.ObjectId = key.ObjectId;
                        //    item.RecordId = key.RecordId;
                        //    item.SecurityObjectId = objectId;
                        //    item.SecurityRecordId = securityRecordId;
                        //    item.Update();
                        //}

                        //if (!accountAdded)
                        //    accountAdded = true;
                    }
                }

            }

            if (accountAdded)
            {
                hExcluded.Value = DataHelper.ToDelimitedString(excludeList, AccountConstants.AccountDelimiter);
                hRecipients.Value = DataHelper.ToDelimitedString(includeList, AccountConstants.AccountDelimiter);

                txtAdd.Text = string.Empty;
                GridView1.DataBind();

                CheckEnableSend();
            }
            else
            {
                lblStatus.InnerHtml = "Account does not exist.";
            }
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            hRecipients.Value = string.Empty;
            hExcluded.Value = string.Empty;
            GridView1.DataBind();

            CheckEnableSend();
        }

        protected void cmdChangeView_Click(object sender, EventArgs e)
        {
            SetCurrentView(hView.Value == "Users" ? "" : "Users");

            GridView1.DataBind();
        }
    }
}