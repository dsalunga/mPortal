using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.Apps.Integration;


namespace WCMS.WebSystem.Apps.Integration
{
    public partial class ConfigMemberLinks : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var context = new WContext(this);
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

        public DataSet Select(string filterGroup, string keyword)
        {
            WebUser user = null;
            string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
            string status = null;
            var isAdmin = WSession.Current.IsAdministrator;

            var query = new WQuery(true);
            query.SetLoad("ConfigMemberLink.ascx");

            return DataHelper.ToDataSet(from i in MemberLink.Provider.GetList()
                                        where
                                            ((user = i.User) != null || user == null) &&
                                            (string.IsNullOrEmpty(filterGroup) || user != null && user.IsMemberOf(filterGroup)) &&
                                            (isAdmin || user == null || !user.IsAdministrator()) &&
                                            ((status = i.Approved == MemberAccountStatus.Approved ? MemberAccountStatus.ApprovedString : MemberAccountStatus.PendingString) != null) &&
                                            (string.IsNullOrEmpty(kwl) ||
                                                (
                                                    (user != null &&
                                                        (user.UserName.ToLower().Contains(kwl) ||
                                                            user.FullName.ToLower().Contains(kwl) ||
                                                            user.Email.ToLower().Contains(kwl))
                                                    ) ||
                                                    (!string.IsNullOrWhiteSpace(i.ExternalIdNo) && i.ExternalIdNo.ToLower().Contains(kwl)) ||
                                                    (status.ToLower().Contains(kwl))
                                                )
                                             )
                                        select new
                                        {
                                            i.Id,
                                            i.ExternalIdNo,
                                            ExternalIdUrl = query.Set(WebColumns.Id, i.Id).BuildQuery(),
                                            FirstName = user == null ? "" : user.FirstName,
                                            LastName = user == null ? "" : user.LastName,
                                            UserName = user == null ? "" : user.UserName,
                                            Email = user == null ? "" : user.Email,
                                            MobileNumber = user == null ? "" : user.MobileNumber,
                                            AccountStatus = user != null && user.IsActive ? 1 : 0,
                                            ExternalLink = i.MemberId > 0 ? 1 : 0,
                                            Status = status,
                                            i.MembershipDate,
                                            UserId = user == null ? -1 : user.Id,
                                            DateCreated = user == null ? "" : user.DateCreated.ToString("dd-MMM-yyyy")
                                        });
        }

        protected void cmdSync_Click(object sender, EventArgs e)
        {
            try
            {
                var task = new ProfileExtSyncTask();
                task.FilterGroup = hFilterGroup.Value;
                task.Execute();

                lblStatus.InnerHtml = "Update completed!";
            }
            catch (Exception ex)
            {
                lblStatus.InnerHtml = ex.ToString();
            }
        }

        protected void cmdCreateLink_Click(object sender, EventArgs e)
        {
            var username = txtCreate.Text.Trim();
            if (!string.IsNullOrEmpty(username))
            {
                var user = WebUser.GetByEmailOrUsername(username);
                if (user != null)
                {
                    var link = MemberLink.Provider.GetByUserId(user.Id);
                    if (link == null)
                    {
                        link = new MemberLink();
                        link.UserId = user.Id;
                        link.Update();

                        WQuery query = new WQuery(this);
                        query.Set(WebColumns.Id, link.Id);
                        query.LoadAndRedirect("ConfigMemberLink.ascx");

                        return;
                    }
                    else
                    {
                        lblStatus.InnerHtml = "Member Link already exists.";
                    }
                }
                else
                {
                    lblStatus.InnerHtml = "User account does not exist.";
                }
            }
        }
    }
}