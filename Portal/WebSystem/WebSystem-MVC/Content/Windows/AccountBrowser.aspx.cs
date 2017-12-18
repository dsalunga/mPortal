using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Windows
{
    public partial class AccountBrowser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int objectId = DataHelper.GetId(Request, WebColumns.ObjectId);

                if (objectId != -1)
                    cboType.Items.Clear();

                if (objectId == -1 || objectId == WebObjects.WebUser)
                    cboType.Items.Add(new ListItem("Users", WebObjects.WebUser.ToString()));

                if (objectId == -1 || objectId == WebObjects.WebGroup)
                    cboType.Items.Add(new ListItem("Groups", WebObjects.WebGroup.ToString()));

                if (cboType.Items.Count == 1)
                {
                    panelTypeSelector.Visible = false;
                }
                else
                {
                    if (cboType.Items.FindByValue(objectId.ToString()) != null)
                        cboType.SelectedValue = objectId.ToString();
                }

                hAppend.Value = DataHelper.GetId(Request, "AppendValue").ToString();
                hBaseGroup.Value = DataHelper.Get(Request, "BaseGroup");

                int multi = DataHelper.GetInt32(Request, "Multi");
                if (multi == 0)
                {
                    grdObjects.Columns[0].Visible = false;
                    rowButtons.Visible = false;
                }

                grdObjects.DataBind();
            }
        }

        public DataSet Select(int objectId, string baseGroup, string keyword)
        {
            var userList = new List<WebUser>();
            var groupList = new List<WebGroup>();

            if (string.IsNullOrEmpty(baseGroup))
            {
                if (objectId == -1 || objectId == WebObjects.WebUser)
                    userList.AddRange(WebUser.GetList().Where(i => i.Status != AccountStatus.DISABLED));

                if (objectId == -1 || objectId == WebObjects.WebGroup)
                    groupList.AddRange(WebGroup.GetList());
            }
            else
            {
                Action<IEnumerable<WebUser>> AddToUsers = (newUsers) =>
                {
                    var uniqueNewUsers = newUsers.Except(userList);
                    userList.AddRange(uniqueNewUsers);
                };

                Action<IEnumerable<WebGroup>> AddToGroups = (newGroups) =>
                {
                    var uniqueNewGroups = newGroups.Except(groupList);
                    groupList.AddRange(uniqueNewGroups);
                };

                Action<WebGroup> RecursiveAdd = null;
                RecursiveAdd = (g) =>
                {
                    if (g != null)
                    {
                        var subGroups = g.Children;

                        if (objectId == -1 || objectId == WebObjects.WebUser)
                            AddToUsers(g.Users);

                        if (objectId == -1 || objectId == WebObjects.WebGroup)
                            AddToGroups(subGroups);

                        foreach (var subGroup in subGroups)
                            RecursiveAdd(subGroup);
                    }
                };

                var group = WebGroup.Get(baseGroup);
                RecursiveAdd(group);
            }

            string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();

            // Groups
            var items = from i in groupList
                        where
                        string.IsNullOrEmpty(kwl) ||
                            (i.Name.ToLower().Contains(kwl))
                        select new
                        {
                            i.Id,
                            Name = i.Name,
                            UserName = "",
                            Email = "",
                            MobileNumber = "",
                            ObjectType = "Group",
                            UniqueName = AccountConstants.GROUP_PREFIX + i.Name
                        };

            // Users
            if (userList.Count > 0)
            {
                items = items.Union(from i in userList
                                    where
                                    string.IsNullOrEmpty(kwl) ||
                                        (i.UserName.ToLower().Contains(kwl) ||
                                            i.FullName.ToLower().Contains(kwl) ||
                                            i.Email.ToLower().Contains(kwl))
                                    select new
                                    {
                                        i.Id,
                                        Name = i.FirstAndLastName,
                                        i.UserName,
                                        i.Email,
                                        i.MobileNumber,
                                        ObjectType = "User",
                                        UniqueName = AccountConstants.USER_PREFIX + i.UserName
                                    });
            }

            return DataHelper.ToDataSet(items);
        }

        protected void grdObjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            bool returnUnique = Request["ReturnUnique"] == "1";
            string uniqueName = e.CommandArgument.ToString();

            hAccountDelimiter.Value = AccountConstants.AccountDelimiter.ToString();
            //hAppend.Value = DataHelper.GetId(Request, "AppendValue").ToString();

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    if (returnUnique)
                    {
                        hidId.Value = uniqueName;
                    }
                    else
                    {
                        int separatorIndex = uniqueName.IndexOf(@"\");
                        hidId.Value = uniqueName.Substring(separatorIndex + 1);
                    }

                    break;
            }
        }

        protected void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdObjects.DataBind();
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            grdObjects.DataBind();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            grdObjects.DataBind();
        }

        protected void cmdOpen_Click(object sender, EventArgs e)
        {
            var selected = Request.Form["chkChecked"];
            if (!string.IsNullOrWhiteSpace(selected))
                hidId.Value = selected.Replace(',', AccountConstants.AccountDelimiter);
        }
    }
}
