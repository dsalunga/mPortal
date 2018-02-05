using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Net;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Agent;
using Newtonsoft.Json.Linq;

namespace WCMS.WebSystem.Apps.Integration.Streaming
{
    public partial class UserManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Visible = false;

            if (!IsPostBack)
            {
                var isSecretary = false;
                var context = new WContext(this);
                var element = context.Element;
                var set = context.GetParameterSet();

                var adminGroupName = ParameterizedWebObject.GetValue("AdminGroup", element, set);
                WebGroup adminGroup = string.IsNullOrEmpty(adminGroupName) ? null : WebGroup.Get(adminGroupName);
                if (adminGroup != null)
                    hAdminGroupId.Value = adminGroup.Id.ToString();

                var groupName = ParameterizedWebObject.GetValue("StreamGroup", element, set);
                WebGroup group = string.IsNullOrEmpty(groupName) ? null : WebGroup.Get(groupName);
                if (group != null)
                {
                    var streamUrl = ParameterizedWebObject.GetValue("StreamUrl", element, set);
                    hGroupId.Value = group.Id.ToString();
                    hUserFormatString.Value = ParameterizedWebObject.GetValue("UserDataFormatString", element, set);
                    linkStreaming.HRef = streamUrl;

                    var tab = context.Get("View");
                    if (string.IsNullOrEmpty(tab))
                        tab = "Session";

                    hActiveTab.Value = tab;

                    linkSetup.HRef = context.Query.Set("View", "Setup").BuildQuery();
                    linkSessions.HRef = context.Query.Set("View", "Session").BuildQuery();
                    linkAccess.HRef = context.Query.Set("View", "Access").BuildQuery();
                    linkAdmins.HRef = context.Query.Set("View", "Admins").BuildQuery();
                    linkAttendance.HRef = context.Query.Set("View", "Attendance").BuildQuery();

                    var requestsUrl = context.Query.Set("View", "Requests").BuildQuery();
                    linkRequests.HRef = requestsUrl;
                    linkRequestCancel.HRef = requestsUrl;
                    linkRequestContinue.HRef = requestsUrl;

                    switch (tab.ToUpper())
                    {
                        case "SETUP":
                            var localeId = DataUtil.GetId(ParameterizedWebObject.GetValue("LocaleId", element, set));
                            MultiView1.SetActiveView(viewSetup);
                            Setup1.Initialize(localeId);
                            break;

                        case "ACCESS":
                            MultiView1.SetActiveView(viewAccess);
                            GridViewUsers.DataBind();
                            break;

                        case "REQUESTS":
                            MultiView1.SetActiveView(viewRequests);
                            GridViewRequests.DataBind();
                            break;

                        case "ADMINS":
                            MultiView1.SetActiveView(viewAdmins);
                            GridViewAdmins.DataBind();
                            break;

                        case "ATTENDANCE":
                            MultiView1.SetActiveView(viewAttendance);
                            break;

                        default:
                            GridViewSessions.DataBind();
                            break;
                    }

                    var page = WPage.SelectNode(context.Site.Id, streamUrl);
                    if (page != null)
                    {
                        var q = context.Query.Clone();
                        q.Set(ObjectKey.KeyString, new ObjectKey(page.OBJECT_ID, page.Id));
                        q.Set(ObjectKey.KeySource, CentralPages.WebPageHome);
                        q.Set(WebColumns.PageId, page.Id);
                        q.Set(WebColumns.SiteId, page.SiteId);
                        q.Set("SelectedTab", "tabIP");
                        q.Remove("View");
                        linkManageIPs.HRef = q.BuildQuery(CentralPages.WebSecurity);
                    }
                    else
                    {
                        linkManageIPs.Visible = false;
                    }
                }
                else
                {
                    MultiView1.Visible = false;
                }
            }
        }


        public DataSet SelectSessions(int groupId, string userFormatString)
        {
            WPage page = null;
            var now = DateTime.Now;

            var sessions = WSession.UserSessions;
            var users = WebUser.GetList(groupId, 1);
            UserSession i = null;
            UserSessionBrowser bs = null;

            return DataHelper.ToDataSet(
                from user in users
                where (i = sessions.Contains(user.Id) ? sessions.SessionCache[user.Id] : null) != null
                    && (bs = i.LastBrowserSession) != null
                    && ((page = bs.LastPageId > 0 ? WPage.Get(bs.LastPageId) : null) == null || true)
                select new
                {
                    i.UserId,
                    bs.ActivityStartDate,
                    bs.LastActivityDate,
                    bs.LastPageId,
                    IPAddress = bs.IPAddress + (string.IsNullOrEmpty(bs.IPLocation) ? "" : string.Format(" ({0})", bs.IPLocation)),
                    IdleTime = now - bs.LastActivityDate,
                    SessionTime = now - bs.ActivityStartDate,
                    PageUrl = page == null ? "#" : page.BuildRelativeUrl(),
                    PageName = page == null ? "" : page.Name,
                    Name = FormatUserData(user, userFormatString)
                }
            );
        }

        private static string FormatUserData(WebUser user, string formatString)
        {
            if (user != null)
            {
                var provider = new NamedValueProvider();
                provider.Add("UserId", user.Id);
                provider.Add("Name", user.FirstAndLastName);
                return Substituter.Substitute(string.IsNullOrEmpty(formatString) ? "$(Name)" : formatString, provider);
            }

            return null;
        }

        private void AddClick(int groupId, TextBox textBox, GridView grid)
        {
            var query = new WQuery(this);
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

        protected void cmdAdminAdd_Click(object sender, EventArgs e)
        {
            AddClick(DataUtil.GetId(hAdminGroupId.Value), txtAdminId, GridViewAdmins);
        }

        private void DisplayMessage(string message)
        {
            lblStatus.Visible = true;
            lblStatus.InnerHtml = message;
        }

        public DataSet SelectUsers(int groupId, int status)
        {
            var items = from u in WebUserGroup.GetByGroupId(groupId, status)
                        let remarksJson = DataHelper.ToDynamic(u.Remarks.StartsWith("{") && u.Remarks.EndsWith("}") ? JObject.Parse(u.Remarks) : null)
                        let isJson = remarksJson != null
                        let location = isJson && remarksJson.livestream.ip != null && !string.IsNullOrEmpty((string)remarksJson.livestream.ip) ?
                            string.Format("{0}{1}", remarksJson.livestream.ip, 
                                remarksJson.livestream.location != null && !string.IsNullOrEmpty((string)remarksJson.livestream.location) ? 
                                    string.Format(" ({0})", remarksJson.livestream.location) : "") : ""
                        let reason = isJson ? remarksJson.livestream.reason : u.Remarks
                        let user = u.User
                        where user != null
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
                            Remarks = reason,
                            Location = location
                        };

            return DataHelper.ToDataSet(items);
        }

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            int groupId = DataUtil.GetId(Request, WebColumns.GroupId);
            var users = SelectUsers(groupId, 1);
            WebHelper.DownloadAsCsv(users, "Users");
        }

        protected void cmdRevoke_Click(object sender, EventArgs e)
        {
            RevokeClick(DataUtil.GetId(hGroupId.Value), GridViewUsers);
        }

        protected void cmdAdminRevoke_Click(object sender, EventArgs e)
        {
            RevokeClick(DataUtil.GetId(hAdminGroupId.Value), GridViewAdmins);
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

        protected void cmdEndSession_Click(object sender, EventArgs e)
        {
            string checkedIds = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(checkedIds))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(checkedIds);
                if (ids.Count > 0)
                {
                    var manager = WSession.UserSessions;
                    foreach (int id in ids)
                        WSession.LogOff(id);
                    GridViewSessions.DataBind();
                }
            }
        }

        protected void cmdApprove_Click(object sender, EventArgs e)
        {
            string checkedIds = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(checkedIds))
            {
                SetApprovalForm(checkedIds);

                lblReasonOrNotes.InnerHtml = "Additional Notes (optional):";
                cmdSubmitApproval.Visible = true;
                RequiredFieldValidator1.Enabled = false;
            }
        }

        protected void cmdReject_Click(object sender, EventArgs e)
        {
            string checkedIds = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(checkedIds))
            {
                SetApprovalForm(checkedIds);

                lblReasonOrNotes.InnerHtml = "Reason for Rejection:";
                cmdSubmitRejection.Visible = true;
            }
        }

        private void SetApprovalForm(string ids)
        {
            hSelectedIds.Value = ids;
            MultiViewRequests.SetActiveView(viewRequestForm);

            var sb = new StringBuilder();

            var idList = DataHelper.ParseCommaSeparatedIdList(ids);
            foreach (var id in idList)
            {
                var user = WebUser.Get(id);
                sb.Append(AccountHelper.GetPrefixedName(user));
                sb.Append(", ");

                lblRequestor.InnerHtml = sb.ToString().TrimEnd(new char[] { ',', ' ' });
            }
        }

        protected void cmdSubmitApproval_Click(object sender, EventArgs e)
        {
            var checkedIds = hSelectedIds.Value;
            var groupId = DataUtil.GetId(hGroupId.Value);
            var ids = DataHelper.ParseCommaSeparatedIdList(checkedIds);
            if (groupId > 0 && ids.Count > 0)
            {
                var context = new WContext(this);
                var element = context.Element;
                var set = context.GetParameterSet();
                var group = WebGroup.Get(groupId);
                if (group != null)
                {
                    // Send Notification Email
                    var contentTemplate = FileHelper.ReadFile(MapPath(ParameterizedWebObject.GetValue("ApprovedEmailToUser", element, set)));
                    var subjectTemplate = ParameterizedWebObject.GetValue("ApprovedEmailToUserSubject", "Integration Streaming Service - Congratulations! Your request has been approved", element, set);
                    var streamingUrl = WebHelper.CombineAddress(context.Site.BuildAbsoluteUrl(), ParameterizedWebObject.GetValue("StreamUrl", element, set));

                    foreach (int id in ids)
                    {
                        var ug = WebUserGroup.Get(groupId, id);
                        if (ug != null)
                        {
                            ug.IsActive = true;
                            ug.Update();
                        }

                        var user = WebUser.Get(id);
                        var values = new NamedValueProvider();
                        values.Add("REQUESTOR_NAME", AccountHelper.GetPrefixedName(user, true));
                        values.Add("STREAMING_URL", streamingUrl);

                        var reason = txtReason.Text.Trim();
                        if (!string.IsNullOrEmpty(reason))
                        {
                            string reasonContent = string.Format("Additional notes from the Administrator:<br/><strong>{0}</strong><br/><br/>", reason);
                            values.Add("MORE_NOTES", reasonContent);
                        }
                        else
                        {
                            values.Add("MORE_NOTES", string.Empty);
                        }

                        var content = Substituter.Substitute(contentTemplate, values);
                        var subject = Substituter.Substitute(subjectTemplate, values);

                        var email = WebMessageQueue.CreateEmail(content, subject, user.Email, WSession.Current.User);
                        email.EnableMonitor = false;
                        email.Update();
                    }

                    AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                    MultiViewRequests.SetActiveView(viewRequestDone);
                }

                GridViewRequests.DataBind();
            }
        }

        protected void cmdSubmitRejection_Click(object sender, EventArgs e)
        {
            var checkedIds = hSelectedIds.Value;
            var groupId = DataUtil.GetId(hGroupId.Value);
            var ids = DataHelper.ParseCommaSeparatedIdList(checkedIds);
            if (groupId > 0 && ids.Count > 0)
            {
                var reason = txtReason.Text.Trim();
                if (!string.IsNullOrEmpty(reason))
                {
                    var context = new WContext(this);
                    var element = context.Element;
                    var set = context.GetParameterSet();
                    var group = WebGroup.Get(groupId);
                    if (group != null)
                    {
                        // Send Notification Email
                        var contentTemplate = FileHelper.ReadFile(MapPath(ParameterizedWebObject.GetValue("RejectedEmailToUser", element, set)));
                        var subjectTemplate = ParameterizedWebObject.GetValue("RejectedEmailToUserSubject", "Integration Live Streaming Service - Sorry, your request was rejected", element, set);

                        foreach (int id in ids)
                        {
                            WebUserGroup.Delete(id, groupId);

                            var user = WebUser.Get(id);
                            var values = new NamedValueProvider();
                            values.Add("REQUESTOR_NAME", AccountHelper.GetPrefixedName(user, true));
                            values.Add("REASON", reason);
                            var content = Substituter.Substitute(contentTemplate, values);
                            var subject = Substituter.Substitute(subjectTemplate, values);

                            WebMessageQueue.CreateEmail(content, subject, user.Email, WSession.Current.User).Update();
                        }

                        AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                        MultiViewRequests.SetActiveView(viewRequestDone);
                    }
                }

                GridViewRequests.DataBind();
            }
        }
    }
}