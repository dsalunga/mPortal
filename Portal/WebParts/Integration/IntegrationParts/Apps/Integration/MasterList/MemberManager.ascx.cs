using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList
{
    public partial class MemberManager : System.Web.UI.UserControl
    {
        public bool IsManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Visible = false;

            if (!Page.IsPostBack)
            {
                WebGroup group = null;

                var context = new WContext(this);
                var element = context.Element;
                var set = element.GetParameterSet();
                var groupId = context.GetId(WebColumns.ParentId);

                var defaultParentName = ParameterizedWebObject.GetValue("ParentGroup", element, set);
                if (!string.IsNullOrEmpty(defaultParentName))
                {
                    group = WebGroup.SelectNode(defaultParentName);
                    if (group != null)
                    {
                        ObjectDataSource1.SelectParameters["parentId"].DefaultValue = group.Id.ToString();
                        WebGroupTab1.RootGroupId = group.Id;

                        if (groupId == -1)
                            groupId = group.Id;
                    }
                }

                if (group == null && groupId > 0)
                    group = WebGroup.Get(groupId);

                if (group != null)
                {
                    IsManager = group.IsManager(WSession.Current.User, true);
                    hIsManager.Value = IsManager ? "1" : "0";

                    if (!IsManager)
                        GridView1.Columns[0].Visible = false;
                }

                hUserProfileEditUrl.Value = ParameterizedWebObject.GetValue("UserProfileEditUrl", element, set);

                var userProfileUrlFormat = element.GetParameterValue("UserProfileUrl", "");
                if (!string.IsNullOrEmpty(userProfileUrlFormat))
                    hUserProfileUrl.Value = userProfileUrlFormat;

                hGroupId.Value = groupId.ToString();

                context.SetOpen(MasterListConstants.OPEN_KEY_MEMBER_EDIT);
                linkNewMember.HRef = context.BuildQuery();

                GridView1.DataBind();
            }
            else
            {
                IsManager = hIsManager.Value == "1";
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            int id = DataUtil.GetId(hGroupId.Value);
            var item = id > 0 ? WebGroup.Get(id) : null;
            if (item != null)
            {
                string name = txtId.Text.Trim();
                if (!string.IsNullOrEmpty(name))
                {
                    StringBuilder sb = new StringBuilder();
                    bool success = false;

                    if (name.StartsWith("/") && name.Contains(':'))
                    {
                        var colonIndex = name.IndexOf(':');
                        var prefix = name.Substring(0, colonIndex + 1);
                        var key = name.Substring(colonIndex + 1);

                        var result = AccountHelper.Search<WebUser>(key);
                        if (result.Count() == 1)
                        {
                            var user = result.First();
                            if (prefix.Equals(AccountConstants.REMOVE_CMD, StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (item.IsMember(user.Id))
                                {
                                    if (item.RemoveUser(user.Id))
                                    {
                                        sb.AppendFormat("REMOVED: {0}", user.UserName);
                                        success = true;
                                    }
                                }
                            }
                            else
                            {
                                if (!item.IsMember(user.Id))
                                {
                                    item.AddUser(user.Id);
                                    sb.AppendFormat("ADDED: {0}", user.UserName);
                                    success = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        var users = MemberHelper.CollectUsers(name, true);
                        if (users.Count() > 0)
                        {
                            sb.Append("ADDED: ");

                            foreach (var user in users)
                            {
                                if (!item.IsMember(user.Id))
                                {
                                    item.AddUser(user.Id);
                                    sb.AppendFormat("{0},", user.UserName);
                                    success = true;
                                }
                            }
                        }
                    }

                    if (success)
                    {
                        txtId.Text = string.Empty;
                        GridView1.DataBind();

                        DisplayMessage(sb.ToString().TrimEnd(','));
                    }
                }
            }
        }

        private void DisplayMessage(string message)
        {
            lblStatus.Visible = true;
            lblStatus.InnerHtml = message;
        }

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            int groupId = DataUtil.GetId(Request, WebColumns.GroupId);
            var users = Select(groupId);

            WebUtil.DownloadAsXml(users, "Group", "Users");
        }

        public DataSet Select(int groupId)
        {
            WebUser user = null;
            var items = from u in WebUserGroup.GetByGroupId(groupId, -1)
                        where (user = u.User) != null
                        select new
                        {
                            user.Id,
                            user.UserName,
                            user.Email,
                            user.FirstName,
                            user.LastName,
                            u.Active,
                            u.DateJoined,
                            user.MobileNumber
                        };

            return DataUtil.ToDataSet(items);
        }

        public DataSet Get(int parentId)
        {
            MemberLink link = null;
            WebUser user = null;
            var isAdmin = WSession.Current.IsAdministrator;

            var items = from i in WebUserGroup.GetByGroupId(parentId, -1)
                        where (user = i.User) != null
                            && ((link = MemberLink.Provider.GetByUserId(user.Id)) != null || link == null)
                        select new
                        {
                            user.Id,
                            Name = user.UserName,
                            Email = user.Email,
                            user.FirstName,
                            user.LastName,
                            i.Active,
                            DateModified = i.DateJoined,
                            user.MobileNumber,
                            VoiceDesignation = user.GetParameterValue(MasterListConstants.MEMBER_VOICE_DESIGNATION_KEY),
                            Position = user.GetParameterValue(MasterListConstants.MEMBER_POSITION_KEY),
                            ExternalId = link == null ? string.Empty : link.ExternalIdNo,
                            PhotoPath = link == null ? WConstants.NoPhotoThumb : link.GetPhotoPathIfNull("200x200")
                        };

            return DataUtil.ToDataSet(items);
        }

        protected void cmdRemove_Click(object sender, EventArgs e)
        {
            var selected = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(selected))
            {
                var groupId = DataUtil.GetInt32(hGroupId.Value);

                var ids = DataUtil.ParseCommaSeparatedIdList(selected);
                foreach (var id in ids)
                {
                    var item = WebUser.Get(id);
                    if (item != null)
                    {
                        var ug = WebUserGroup.Get(groupId, item.Id);
                        if (ug != null)
                            ug.Delete();

                        if (item.Status == AccountStatus.DRAFT)
                            item.Delete();
                    }
                }

                GridView1.DataBind();
            }
        }
    }
}