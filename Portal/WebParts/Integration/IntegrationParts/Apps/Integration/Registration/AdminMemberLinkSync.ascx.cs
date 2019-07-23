using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;
//using WCMS.WebSystem.Apps.Integration.WebServices;

namespace WCMS.WebSystem.Apps.Integration
{
    public partial class AdminMemberLinkSync : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WContext context = new WContext(this);

                var admin = context.PartAdmin;
                if (admin != null)
                {
                    var filterGroup = admin.GetParameterValue("FilterGroup", string.Empty);
                    if (!string.IsNullOrEmpty(filterGroup))
                    {
                        hFilterGroup.Value = filterGroup;
                        GridView1.DataBind();
                    }
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataUtil.GetId(e.CommandArgument);
            var query = new WQuery(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.Id, id);
                    query.LoadAndRedirect("ConfigMemberLink.ascx");
                    break;

                case "Custom_Delete":
                    var link = MemberLink.Provider.Get(id);
                    WebUser.Delete(link.UserId);
                    link.Delete();

                    GridView1.DataBind();
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

        private List<MemberLinkContainer> GetSyncData(int itemType)
        {
            var items = new List<MemberLinkContainer>();
            /*DataSyncClient client = new DataSyncClient("Integration.BasicHttpBinding_IDataSync", ConfigHelper.Get(MemberConstants.REMOTE_SYNC_CONFIG));

            var remoteUsers = client.GetMemberLinkList();
            var links = MemberLink.Provider.GetList();
            var linkContainers = new List<MemberLinkContainer>();

            linkContainers.AddRange(
                from l in links
                select new MemberLinkContainer(l, RemoteItemTypes.ALL)
            );

            if (itemType == RemoteItemTypes.ALL || itemType == RemoteItemTypes.LOCAL)
            {
                // Unique Locale Only
                var localeOnly = linkContainers.Except(remoteUsers, new MemberLinkEqualityComparer());
                items.AddRange(from i in localeOnly select new MemberLinkContainer { Link = i.Link, UserName = i.UserName, ItemType = RemoteItemTypes.LOCAL });
            }

            if (itemType == RemoteItemTypes.ALL || itemType == RemoteItemTypes.REMOTE)
            {
                var remoteOnly = remoteUsers.Except(linkContainers, new MemberLinkEqualityComparer());
                items.AddRange(from i in remoteOnly select new MemberLinkContainer { Link = i.Link, UserName = i.UserName, ItemType = RemoteItemTypes.REMOTE });
            }

            if (itemType == RemoteItemTypes.ALL || itemType == RemoteItemTypes.IDENTICAL)
            {
                var matchedUsers = linkContainers.Intersect(remoteUsers, new MemberLinkEqualityComparer());
                items.AddRange(from i in matchedUsers select new MemberLinkContainer { Link = i.Link, UserName = i.UserName, ItemType = RemoteItemTypes.IDENTICAL });
            }*/

            return items;
        }

        public DataSet Select(string filterGroup, string keyword, int itemType = 0)
        {
            var items = GetSyncData(itemType);

            WebUser user = null;
            string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
            string status = null;
            var isAdmin = WSession.Current.IsAdministrator;

            return DataUtil.ToDataSet(from i in items
                                        where
                                            ((user = i.Link.User) != null || user == null) &&
                                            (string.IsNullOrEmpty(filterGroup) || user != null && user.IsMemberOf(filterGroup)) &&
                                            (isAdmin || user == null || !user.IsAdministrator()) &&
                                            ((status = i.Link.Approved == MemberAccountStatus.Approved ? MemberAccountStatus.ApprovedString : MemberAccountStatus.PendingString) != null) &&
                                            (string.IsNullOrEmpty(kwl) ||
                                                (
                                                    (user != null &&
                                                        (user.UserName.ToLower().Contains(kwl) ||
                                                            user.FullName.ToLower().Contains(kwl) ||
                                                            user.Email.ToLower().Contains(kwl))
                                                    ) ||
                                                    (!string.IsNullOrWhiteSpace(i.Link.ExternalIdNo) && i.Link.ExternalIdNo.ToLower().Contains(kwl)) ||
                                                    (status.ToLower().Contains(kwl))
                                                )
                                             )
                                        select new
                                        {
                                            i.Link.Id,
                                            i.Link.ExternalIdNo,
                                            FirstName = user == null ? "" : user.FirstName,
                                            LastName = user == null ? "" : user.LastName,
                                            UserName = user == null ? "" : user.UserName,
                                            Email = user == null ? "" : user.Email,
                                            MobileNumber = user == null ? "" : user.MobileNumber,
                                            AccountStatus = user != null && user.IsActive ? 1 : 0,
                                            ExternalLink = i.Link.MemberId > 0 ? 1 : 0,
                                            Status = status,
                                            i.Link.MembershipDate,
                                            UserId = user == null ? -1 : user.Id,
                                            DateCreated = user == null ? "" : user.DateCreated.ToString("dd-MMM-yyyy"),
                                            ItemType = SyncHelper.GetSyncTypeName(i.ItemType),
                                        });
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
                /*
                var userNames = DataHelper.ParseDelimitedStringToList(checkedItems);
                int itemType = DataUtil.GetInt32(cboItemTypes.SelectedValue);

                var items = GetSyncData(itemType);
                var client = new DataSyncClient("Integration.BasicHttpBinding_IDataSync", ConfigHelper.Get(MemberConstants.REMOTE_SYNC_CONFIG));

                var selectedUsers = items.Where(i => userNames.Find(a => a.Equals(i.UserName, StringComparison.InvariantCultureIgnoreCase)) != null);

                foreach (var container in selectedUsers)
                {
                    if (container.ItemType == RemoteItemTypes.LOCAL || container.ItemType == RemoteItemTypes.REMOTE)
                    {
                        if (container.ItemType == RemoteItemTypes.LOCAL)
                        {
                            // Locale User, sync to remote
                            var c = new MemberLinkContainer(container.Link, RemoteItemTypes.REMOTE);

                            client.SetMemberLinkComplete(c);
                        }

                        if (container.ItemType == RemoteItemTypes.REMOTE)
                        {
                            // Remote User, sync to local
                            var c = client.GetMemberLinkComplete(container.UserName);

                            DataSync sync = new DataSync();
                            sync.SetMemberLinkComplete(c);
                        }
                    }
                }

                GridView1.DataBind();*/
            }
        }
    }
}