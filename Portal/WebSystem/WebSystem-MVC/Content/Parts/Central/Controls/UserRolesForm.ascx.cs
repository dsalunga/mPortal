using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class UserGroupsFormController : UserControl
    {
        protected void Page_Load(object sender, EventArgs e) { }

        public void LoadData(int userId)
        {
            hidUserId.Value = userId.ToString();
            CheckBoxList1.DataBind();

            WebUser user = WebUser.Get(userId);

            //List<WebRole> roles = user.Roles;
            foreach (ListItem role in CheckBoxList1.Items)
            {
                if (user.IsMemberOf(DataHelper.GetId(role.Value)))
                {
                    role.Selected = true;
                }
            }
        }

        public IEnumerable<WebGroup> Select()
        {
            return WebGroup.GetList();
        }

        public void UpdateData(WebUser user)
        {
            int userId = DataHelper.GetId(hidUserId.Value);
            if (user == null) user = WebUser.Get(userId);
            if (user != null)
            {
                foreach (ListItem role in CheckBoxList1.Items)
                {
                    int groupId = DataHelper.GetId(role.Value);

                    if (role.Selected && !user.IsMemberOf(groupId)) user.AddToGroup(groupId);
                    if (!role.Selected && user.IsMemberOf(groupId)) user.RemoveToGroup(groupId);
                }
            }
        }

        public void UpdateData()
        {
            UpdateData(null);
        }

        public int UserId
        {
            set
            {
                hidUserId.Value = value.ToString();
            }
        }

        public string RoleNames
        {
            set
            {
                CheckBoxList1.DataBind();

                foreach (string role in value.Split(','))
                {
                    ListItem item = CheckBoxList1.Items.FindByText(role);
                    if (item != null) item.Selected = true;
                }
            }

            get
            {
                string roles = string.Empty;
                foreach (ListItem item in CheckBoxList1.Items)
                {
                    if (item.Selected) roles += item.Text + ",";
                }

                return roles.Trim(',');
            }
        }
    }
}