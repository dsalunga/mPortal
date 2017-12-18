using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Handlers;
using WCMS.WebSystem.ViewModel;
using WCMS.WebSystem.WebServices;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class DataSyncManager : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WContext context = new WContext(this);
                var element = context.ParameterizedObject;

                int id = -1;
                var groupFilter = element.GetParameterValue("GroupFilter");
                if (!string.IsNullOrEmpty(groupFilter))
                {
                    var group = WebGroup.SelectNode(groupFilter);
                    if (group != null)
                        id = group.Id;
                }

                if (id > 0)
                {
                    panelGroupFilter.Visible = false;

                    hGroupId.Value = id.ToString();

                    GridView1.DataBind();
                }
                else
                {
                    cboGroups.Items.AddRange(WebGroupViewModel.GenerateListItem(-1).ToArray());

                    id = DataHelper.GetId(Request, WebColumns.GroupId);
                    if (id > 0)
                    {
                        cboGroups.SelectedValue = id.ToString();
                        hGroupId.Value = id.ToString();

                        GridView1.DataBind();
                    }
                }

                hUserEditUrl.Value = element.GetParameterValue("UserEditUrl");
                hUserHomeUrl.Value = element.GetParameterValue("UserHomeUrl");

                var dataEntryMode = DataHelper.GetBool(element.GetParameterValue("Data-Entry"), false);
                if (dataEntryMode)
                {
                    GridView1.Columns[2].Visible = false;
                    GridView1.Columns[9].Visible = false;
                    GridView1.Columns[10].Visible = false;

                    hDataEntry.Value = "1";
                }

                // New User
                //WebHelper.CreateButtonLink(
            }
        }

        protected void cmdNewUser_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);

            query.SetReturn();

            var userEditUrl = hUserEditUrl.Value.Trim();
            if (string.IsNullOrEmpty(userEditUrl))
                query.Set(WConstants.Open, "UserEdit");
            else
                query.BasePath = userEditUrl;

            // Old Url: CentralPages.UserProfile
            query.Redirect();
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            string checkedItems = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(checkedItems))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(checkedItems);
                if (ids.Count > 0)
                {
                    foreach (var userId in ids)
                        WebUser.Delete(userId);

                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);
            QueryParser query = new QueryParser(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.SetReturn();

                    var userHomeUrl = hUserHomeUrl.Value.Trim();
                    if (string.IsNullOrEmpty(userHomeUrl))
                        query.Set(WConstants.Open, "UserEdit");
                    else
                        query.BasePath = userHomeUrl;

                    query.Set(WebColumns.UserId, id);
                    query.Redirect();
                    break;

                case "Custom_Delete":
                    WebUser.Delete(id);
                    GridView1.DataBind();
                    break;

                case "ToggleActive":
                    var user = WebUser.Get(id);
                    if (user != null)
                    {
                        user.IsActive = !user.IsActive;
                        user.Update();

                        GridView1.DataBind();
                    }
                    break;
            }
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GridView1.DataBind();
        }

        private List<WebUserContainer> GetSyncData(int itemType)
        {
            DataSyncClient client = new DataSyncClient("WCMS.BasicHttpBinding_IDataSync", ConfigHelper.Get("WCMS.RemoteInstance"));

            var remoteUsers = client.GetObjectList(WebObjects.WebUser);
            var users = WebUser.GetList();

            List<WebUserContainer> items = new List<WebUserContainer>();

            if (itemType == RemoteItemTypes.ALL || itemType == RemoteItemTypes.LOCAL)
            {
                // Unique Locale Only
                var localeOnly = users.Except(remoteUsers, new UserNameEqualityComparer());
                items.AddRange(from i in localeOnly select new WebUserContainer { User = i, ItemType = RemoteItemTypes.LOCAL });
            }

            if (itemType == RemoteItemTypes.ALL || itemType == RemoteItemTypes.REMOTE)
            {
                var remoteOnly = remoteUsers.Except(users, new UserNameEqualityComparer());
                items.AddRange(from i in remoteOnly select new WebUserContainer { User = i, ItemType = RemoteItemTypes.REMOTE });
            }

            if (itemType == RemoteItemTypes.ALL || itemType == RemoteItemTypes.IDENTICAL)
            {
                var matchedUsers = users.Intersect(remoteUsers, new UserNameEqualityComparer());
                items.AddRange(from i in matchedUsers select new WebUserContainer { User = i, ItemType = RemoteItemTypes.IDENTICAL });
            }

            return items;
        }

        public DataSet Select(int groupId, string keyword, int itemType = 0)
        {
            var items = GetSyncData(itemType);

            var isAdmin = WSession.Current.IsAdministrator;
            var keywordL = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();

            return DataHelper.ToDataSet(from i in items
                                        where (groupId == -1 || i.User.IsMemberOf(groupId))
                                            && (isAdmin || !i.User.IsMemberOf(SystemGroups.ADMINS_GROUP_ID))
                                            && (string.IsNullOrEmpty(keywordL) ||
                                                (i.User.UserName.ToLower().Contains(keywordL) ||
                                                    i.User.FullName.ToLower().Contains(keywordL) ||
                                                    i.User.Email.ToLower().Contains(keywordL)))
                                        select new
                                        {
                                            i.User.Id,
                                            i.User.UserName,
                                            i.User.FirstName,
                                            i.User.LastName,
                                            i.User.Email,
                                            i.User.MobileNumber,
                                            i.User.DateCreated,
                                            i.User.LastUpdate,
                                            i.User.LastLogin,
                                            Active = i.User.Status == 1 ? 1 : 0,
                                            ItemType = SyncHelper.GetSyncTypeName(i.ItemType)
                                        });
        }

        public IEnumerable<WebGroup> SelectGroups()
        {
            return WebGroup.GetList();
        }

        public DataSet SelectDownload(int groupId, string keyword)
        {
            var isDataEntry = hDataEntry.Value == "1";
            var isAdmin = WSession.Current.IsAdministrator;
            var keywordL = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();

            var data = from i in WebUser.GetList()
                       where (groupId == -1 || i.IsMemberOf(groupId))
                           && (isAdmin || !i.IsMemberOf(SystemGroups.ADMINS_GROUP_ID))
                           && (string.IsNullOrEmpty(keywordL) ||
                               (i.UserName.ToLower().Contains(keywordL) ||
                                   i.FullName.ToLower().Contains(keywordL) ||
                                   i.Email.ToLower().Contains(keywordL)))
                       select i;

            if (isDataEntry)
            {
                return DataHelper.ToDataSet(
                    from i in data
                    select new
                    {
                        i.Id,
                        i.FirstName,
                        i.LastName,
                        i.Email,
                        i.TelephoneNumber,
                        i.MobileNumber,
                        i.Gender,
                        i.DateCreated,
                        i.LastUpdate
                    });
            }
            else
            {
                return DataHelper.ToDataSet(
                    from i in data
                    select new
                    {
                        i.Id,
                        i.UserName,
                        i.FirstName,
                        i.LastName,
                        i.Email,
                        i.TelephoneNumber,
                        i.MobileNumber,
                        i.Gender,
                        i.DateCreated,
                        i.LastUpdate,
                        i.LastLogin,
                        Active = i.Status == 1 ? 1 : 0,
                        i.StatusText
                    });
            }
        }

        protected void cboGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            hGroupId.Value = cboGroups.SelectedValue.ToString();
            GridView1.DataBind();
        }

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            var keyword = txtSearch.Text.Trim();
            var groupId = DataHelper.GetId(hGroupId.Value.Trim());

            var ds = SelectDownload(groupId, keyword);

            WebHelper.DownloadAsCsv(ds, "Users");
        }

        protected void cboItemTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void cmdSync_Click(object sender, EventArgs e)
        {
            string checkedItems = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(checkedItems))
            {
                var userNames = DataHelper.ParseDelimitedStringToList(checkedItems);
                int itemType = DataHelper.GetInt32(cboItemTypes.SelectedValue);

                var items = GetSyncData(itemType);
                var client = new DataSyncClient("WCMS.BasicHttpBinding_IDataSync", ConfigHelper.Get("WCMS.RemoteInstance"));

                var selectedUsers = items.Where(i => userNames.Find(a => a.Equals(i.User.UserName, StringComparison.InvariantCultureIgnoreCase)) != null);

                foreach (var container in selectedUsers)
                {
                    if (container.ItemType == RemoteItemTypes.LOCAL || container.ItemType == RemoteItemTypes.REMOTE)
                    {
                        if (container.ItemType == RemoteItemTypes.LOCAL)
                        {
                            // Locale User, sync to remote
                            var c = new WebUserContainer(container.User, RemoteItemTypes.REMOTE);
                            
                            client.SetUserComplete(c);
                        }

                        if (container.ItemType == RemoteItemTypes.REMOTE)
                        {
                            // Remote User, sync to local
                            var c = client.GetUserComplete(container.User.Id);

                            DataSync sync = new DataSync();
                            sync.SetUserComplete(c);
                        }
                    }
                }

                GridView1.DataBind();
            }
        }
    }
}