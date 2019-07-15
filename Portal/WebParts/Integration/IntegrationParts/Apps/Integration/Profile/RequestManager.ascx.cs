using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class RequestManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataUtil.GetInt32(e.CommandArgument);
            if (id > 0)
            {
                WebUserGroup item = WebUserGroup.Get(id);
                if (item != null && !item.IsActive)
                {
                    var user = item.User;
                    var group = item.Group;

                    switch (e.CommandName)
                    {
                        case "Approve":

                            item.Active = 1;
                            item.Update();

                            if (user != null && group != null)
                            {
                                // Send Notification Email
                                string content = FileHelper.ReadFile(MapPath("~/Content/Parts/Profile/Template/GroupJoinRequestApproved.htm"));

                                string groupUrl = group.PageUrl;
                                if (groupUrl.StartsWith("/"))
                                    groupUrl = WebHelper.CombineAddress(WConfig.BaseAddress, groupUrl);

                                NamedValueProvider values = new NamedValueProvider();
                                values.Add("RequesterName", string.IsNullOrWhiteSpace(user.FirstName) ? user.FullName : user.FirstName);
                                values.Add("GroupName", group.Name);
                                values.Add("GroupLink", groupUrl);

                                content = Substituter.Substitute(content, values);

                                CmsEmail smtp = new CmsEmail();
                                smtp.MailTo.Add(user.Email);
                                smtp.SubjectAutoPrefix = "You are now a member of " + group.Name;
                                smtp.Message = content;
                                smtp.SendMail();
                            }

                            break;

                        case "Reject":
                            WebUserGroup.Delete(item.Id);

                            if (user != null && group != null)
                            {
                                // Send Notification Email
                                string content = FileHelper.ReadFile(MapPath("~/Content/Parts/Profile/Template/GroupJoinRequestRejected.htm"));

                                NamedValueProvider values = new NamedValueProvider();
                                values.Add("RequesterName", string.IsNullOrWhiteSpace(user.FirstName) ? user.FullName : user.FirstName);
                                values.Add("GroupName", group.Name);

                                content = Substituter.Substitute(content, values);

                                CmsEmail smtp = new CmsEmail();
                                smtp.MailTo.Add(user.Email);
                                smtp.SubjectAutoPrefix = "Your request to join " + group.Name + " has been REJECTED";
                                smtp.Message = content;
                                smtp.SendMail();
                            }
                            break;
                    }

                    GridView1.DataBind();
                }
            }
        }

        public DataSet Select()
        {
            Dictionary<int, bool> approverGroups = new Dictionary<int, bool>();

            var userDetailsFormat = WebRegistry.SelectNodeValue(IntegrationConstants.REG_ProfileNode + "/UserInfoPageFormat");
            var members = MemberLink.Provider.GetList(MemberAccountStatus.Approved);

            MemberLink memberLink = null;

            var userGroups = WebUserGroup.Provider.GetList(0);
            WebUser user = null;
            WebGroup g = null;

            Func<WebGroup, bool> IsApprover = (group) =>
            {
                if (WSession.Current.IsAdministrator)
                    return true;

                var approvers = group.GetParameterValue("Approvers");
                if (!string.IsNullOrWhiteSpace(approvers))
                {
                    if (!approverGroups.ContainsKey(group.Id))
                    {
                        bool isApprover = AccountHelper.IsPresentOrMember(approvers);
                        approverGroups.Add(group.Id, isApprover);

                        return isApprover;
                    }
                    else
                    {
                        return approverGroups[group.Id];
                    }
                }

                return false;
            };

            return DataHelper.ToDataSet(from ug in userGroups
                                        where (user = ug.User) != null && (g = ug.Group) != null &&
                                            (memberLink = MemberLink.Provider.GetByUserId(user.Id)) != null &&
                                            IsApprover(g)
                                        select new
                                        {
                                            ug.Id,
                                            user.Email,
                                            user.FullName,
                                            user.UserName,
                                            GroupName = g.Name,
                                            PhotoPath = memberLink.GetPhotoPathIfNull("200x200"),
                                            UserProfileUrl = string.Format(userDetailsFormat, user.Id)
                                        }
                                    );
        }
    }
}