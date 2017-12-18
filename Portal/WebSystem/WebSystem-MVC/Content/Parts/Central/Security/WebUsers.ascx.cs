using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.ViewModel;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebUsersController : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!WebGlobalPolicy.IsUserPermitted(GlobalPolicies.Administration, Permissions.UsersManagement))
                    WQuery.StaticRedirect(WConstants.AbsoluteAccessDeniedPage);

                var context = new WContext(this);
                var element = context.Element;

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
                //cboSites.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());

                //var siteId = DataHelper.GetId(Request, WebColumns.SiteId);
                //if (siteId > 0)
                //{
                //    WebHelper.SetCboValue(cboSites, siteId);
                //    cboSites.Visible = false;
                //    //ObjectDataSource1.SelectParameters["siteId"].DefaultValue = siteId.ToString();
                //}

                //GridView1.DataBind();
            }
        }

        protected void cmdNewUser_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            //query.SetReturn();

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
            var query = new WQuery(this);

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

                //case "Custom_Delete":
                //    WebUser.Delete(id);
                //    GridView1.DataBind();
                //    break;

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

        protected void cboSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridView1.DataBind();
        }

        public DataSet Select(int groupId, string keyword)
        {
            var isAdmin = WSession.Current.IsAdministrator;
            var keywordL = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();

            return DataHelper.ToDataSet(from i in WebUser.GetList()
                                        where (groupId == -1
                                                || (groupId > 0 && i.IsMemberOf(groupId))
                                                || (groupId == -2 && i.Groups.Count() == 0)
                                                || (groupId == -3 && !i.IsActive)
                                            )
                                            && (isAdmin || !i.IsMemberOf(SystemGroups.ADMINS_GROUP_ID))
                                            && (string.IsNullOrEmpty(keywordL) ||
                                                (
                                                    i.UserName.ToLower().Contains(keywordL) ||
                                                    i.FullName.ToLower().Contains(keywordL) ||
                                                    i.Email.ToLower().Contains(keywordL)
                                                )
                                               )
                                        select new
                                        {
                                            i.Id,
                                            i.UserName,
                                            i.FirstName,
                                            i.LastName,
                                            i.Email,
                                            i.MobileNumber,
                                            i.DateCreated,
                                            i.LastUpdate,
                                            i.LastLogin,
                                            Active = i.Status == 1 ? 1 : 0
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
    }
}