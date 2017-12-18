using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class WebGroupUsers : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Visible = false;
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            bool success = false;
            int id = query.GetId(WebColumns.GroupId);
            var item = WebGroup.Get(id);

            if (item != null)
            {
                var sb = new StringBuilder();
                string name = txtId.Text.Trim();
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
                                    success = true;
                                    sb.AppendFormat("REMOVED: {0}", user.UserName);
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
                    var users = AccountHelper.CollectUsers(name);
                    if (users.Count > 0)
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

        private void DisplayMessage(string message)
        {
            lblStatus.Visible = true;
            lblStatus.InnerHtml = message;
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
                            u.DateJoined
                        };

            return DataHelper.ToDataSet(items);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var query = new QueryParser(this);

            int userId = DataHelper.GetId(e.CommandArgument);
            int id = query.GetId(WebColumns.GroupId);

            switch (e.CommandName)
            {
                case "Custom_Delete":
                    WebUserGroup.Delete(userId, id);
                    GridView1.DataBind();
                    break;

                case "ToggleActive":
                    var userGroup = WebUserGroup.Get(id, userId);
                    if (userGroup != null)
                    {
                        userGroup.IsActive = !userGroup.IsActive;
                        userGroup.Update();
                        GridView1.DataBind();
                    }
                    break;
            }
        }

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            int groupId = DataHelper.GetId(Request, WebColumns.GroupId);

            var users = Select(groupId);

            WebHelper.DownloadAsXml(users, "Group", "Users");
        }
    }
}