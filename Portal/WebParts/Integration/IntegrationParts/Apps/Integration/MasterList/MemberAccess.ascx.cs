using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.Apps.Integration.MusicMinistry.MasterList
{
    public partial class MemberAccessView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Visible = false;

            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;
                var set = context.GetParameterSet();

                var defaultParentName = ParameterizedWebObject.GetValue("ParentGroup", element, set);
                var root = WebGroup.SelectNode(defaultParentName);

                WebGroupTab1.RootGroupId = root.Id;

                var managerGroupName = ParameterizedWebObject.GetValue("ManagerGroup", element, set);
                WebGroup managerGroup = string.IsNullOrEmpty(managerGroupName) ? null : WebGroup.SelectNode(managerGroupName);
                var user = WSession.Current.User;

                if (managerGroup == null || !(root.OwnerId == user.Id || root.IsManager(user)))
                {
                    context.RemoveOpen();
                    context.Redirect();
                    return;
                }

                hGroupId.Value = managerGroup.Id.ToString();
                GridViewUsers.DataBind();
            }
        }

        protected void cmdRevoke_Click(object sender, EventArgs e)
        {
            RevokeClick(DataUtil.GetId(hGroupId.Value), GridViewUsers);
        }

        private void RevokeClick(int groupId, GridView grid)
        {
            string checkedIds = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(checkedIds))
            {
                //var groupId = DataHelper.GetId(hGroupId.Value);
                var ids = DataHelper.ParseCommaSeparatedIdList(checkedIds);
                if (groupId > 0 && ids.Count > 0)
                {
                    var manager = WSession.UserSessions;

                    foreach (int id in ids)
                    {
                        WebUserGroup.Delete(id, groupId);

                        if (manager.Contains(id))
                            manager.End(id);
                    }

                    grid.DataBind();
                }
            }
        }

        private void DisplayMessage(string message)
        {
            lblStatus.Visible = true;
            lblStatus.InnerHtml = message;
        }

        public DataSet SelectUsers(int groupId, int status)
        {
            WebUser user = null;
            var items = from u in WebUserGroup.GetByGroupId(groupId, status)
                        where (user = u.User) != null
                        select new
                        {
                            user.Id,
                            user.UserName,
                            user.Email,
                            user.FirstName,
                            user.LastName,
                            user.MobileNumber,
                            u.Active,
                            u.DateJoined,
                            u.Remarks
                        };

            return DataHelper.ToDataSet(items);
        }

        private void AddClick(int groupId, TextBox textBox, GridView grid)
        {
            var query = new QueryParser(this);

            bool success = false;
            int id = groupId; //DataHelper.GetId(groupId);
            var item = WebGroup.Get(id);

            if (item != null)
            {
                var sb = new StringBuilder();

                string name = textBox.Text.Trim();
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
                    var users = MemberHelper.CollectUsers(name);
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
                    textBox.Text = string.Empty;
                    grid.DataBind();

                    DisplayMessage(sb.ToString().TrimEnd(','));
                }
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            AddClick(DataUtil.GetId(hGroupId.Value), txtId, GridViewUsers);
        }
    }
}