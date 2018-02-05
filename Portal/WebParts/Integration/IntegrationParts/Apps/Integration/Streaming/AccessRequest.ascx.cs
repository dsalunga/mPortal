using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class AccessRequest : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;
                var set = context.GetParameterSet();

                var continueUrl = ParameterizedWebObject.GetValue("ContinueUrl", element, set);
                if (string.IsNullOrEmpty(continueUrl))
                    continueUrl = context.Site.BuildRelativeUrl();

                var requestGroupName = ParameterizedWebObject.GetValue("StreamGroup", element, set);
                WebUserGroup ug = null;
                WebGroup requestGroup = string.IsNullOrEmpty(requestGroupName) ? null : WebGroup.SelectNode(requestGroupName);
                if (!string.IsNullOrEmpty(requestGroupName) && requestGroup != null && (ug = WebUserGroup.Get(requestGroup.Id, WSession.Current.UserId)) != null)
                {
                    if (ug.IsActive)
                    {
                        var streamUrl = ParameterizedWebObject.GetValue("StreamUrl", element, set);
                        (new WQuery(this)).Redirect(string.IsNullOrEmpty(streamUrl) ? continueUrl : streamUrl);
                        return;
                    }
                    else
                    {
                        var remarksJson = DataHelper.ToDynamic(ug.Remarks.StartsWith("{") && ug.Remarks.EndsWith("}") ? JObject.Parse(ug.Remarks) : null);
                        var reason = remarksJson == null ? ug.Remarks : remarksJson.livestream.reason;
                        txtReason.Text = reason;
                        lblAlert.InnerHtml = string.Format(lblAlert.InnerHtml, ug.DateJoined);
                        lblAlert.Visible = true;
                    }
                }

                linkCancel.HRef = continueUrl;
                linkContinue.HRef = continueUrl;
            }
        }

        protected void cmdRequest_Click(object sender, EventArgs e)
        {
            var reason = txtReason.Text.Trim();
            var user = WSession.Current.User;
            if (!string.IsNullOrEmpty(reason) && user != null)
            {
                var context = new WContext(this);
                var element = context.Element;
                var set = context.GetParameterSet();

                var requestGroupName = ParameterizedWebObject.GetValue("StreamGroup", element, set);
                var adminGroupName = ParameterizedWebObject.GetValue("AdminGroup", element, set);
                var approvalUrl = WebHelper.CombineAddress(context.Site.BuildAbsoluteUrl(), ParameterizedWebObject.GetValue("ApprovalUrl", element, set));
                if (!string.IsNullOrEmpty(requestGroupName) && !string.IsNullOrEmpty(adminGroupName) && !string.IsNullOrEmpty(approvalUrl))
                {
                    var requestGroup = WebGroup.SelectNode(requestGroupName);
                    var adminGroup = WebGroup.SelectNode(adminGroupName);
                    if (requestGroup != null && adminGroup != null)
                    {
                        var location = hLocation.Value.Trim();
                        var ip = Request.UserHostAddress;
                        var ug = user.AddToGroup(requestGroup.Id, 0);
                        //ug.Remarks = reason + IntegrationConstants.IP_DELIMITER + Request.UserHostAddress; // Quick workaround
                        ug.Remarks = new JObject(
                            new JProperty("livestream",
                                new JObject(
                                    new JProperty("reason", reason),
                                    new JProperty("ip", ip),
                                    new JProperty("location", location)))).ToString();
                        ug.Update();

                        // Send Notification Email
                        var requestorName = AccountHelper.GetPrefixedName(user);
                        var adminUsers = adminGroup.Users;
                        var streamOwner = ParameterizedWebObject.GetValue("StreamOwner", "Integration", element, set);
                        var contentTemplate = FileHelper.ReadFile(MapPath(ParameterizedWebObject.GetValue("RequestEmailToAdmin", element, set)));
                        var subjectTemplate = ParameterizedWebObject.GetValue("RequestEmailToAdminSubject", streamOwner + " Live Stream - Access Request", element, set);
                        foreach (var adminUser in adminUsers)
                        {
                            var values = new NamedValueProvider();
                            values.Add("REQUESTOR_NAME", requestorName);
                            values.Add("ADMIN_NAME", AccountHelper.GetPrefixedName(adminUser, true));
                            values.Add("REASON", txtReason.Text.Trim());
                            values.Add("APPROVAL_URL", approvalUrl);
                            values.Add("LOCATION", string.IsNullOrEmpty(location) ? ip : string.Format("{0} ({1})", ip, location));
                            values.Add("STREAM_OWNER", streamOwner);
                            var content = Substituter.Substitute(contentTemplate, values);
                            var subject = Substituter.Substitute(subjectTemplate, values);
                            var q = WebMessageQueue.CreateEmail(content, subject, adminUser.Email);
                            q.EnableMonitor = false;
                            q.Update();
                        }

                        AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                        lblEmail.InnerHtml = user.Email;
                        MultiView1.SetActiveView(viewDone);
                        return;
                    }
                }
            }

            lblAlert.InnerHtml = "An error has occurred.";
            lblAlert.Visible = true;
        }
    }
}