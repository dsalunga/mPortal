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
using WCMS.Framework.Diagnostics;

using WCMS.Framework.Utilities;

using WCMS.WebSystem.WebParts.Incident;
//using WCMS.WebSystem.WebParts.Integration;

namespace WCMS.WebSystem.WebParts.Incident
{
    public partial class TicketManagerView : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var sw = PerformanceLog.StartLog();

                WContext context = new WContext(this);
                var element = context.Element;
                var status = context.GetInt32("Status", -2);
                var slaStatus = context.GetInt32("SLA", -1);
                var filterBy = context.GetInt32("FilterBy", -1);

                if (status != -2 && cboStatus.Items.FindByValue(status.ToString()) != null)
                    cboStatus.SelectedValue = status.ToString();

                if (slaStatus != -1 && cboSLA.Items.FindByValue(slaStatus.ToString()) != null)
                    cboSLA.SelectedValue = slaStatus.ToString();

                context.Remove("TicketId");
                context.SetOpen("Incident/Ticket");

                var parameterSet = element.GetParameterValue("ParameterSet");
                var currentUser = WSession.Current.User;
                var set = !string.IsNullOrEmpty(parameterSet) ? WebParameterSet.Get(parameterSet) : null;
                var isSupportUser = set != null ? IncidentHelper.IsSupportUser(set, currentUser) : false;
                if (!isSupportUser)
                {
                    int itemCount = cboStatus.Items.Count;
                    for (int i = itemCount - 1; i > 2; i--)
                        cboStatus.Items.RemoveAt(i);

                    //GridView1.Columns[4].Visible = false;
                    cboSLA.Visible = false;
                    cboFilterBy.Visible = false;
                }
                else
                {
                    var group = IncidentHelper.GetSupportGroup(context, currentUser);
                    if (group != null)
                        cboFilterBy.Items[1].Value = group.Id.ToString();
                    else
                        cboFilterBy.Items.RemoveAt(1);
                }

                if (filterBy != -1 && cboFilterBy.Items.FindByValue(filterBy.ToString()) != null)
                    cboFilterBy.SelectedValue = filterBy.ToString();

                WebHelper.CreateButtonLink(cmdNewTicket, context.BuildQuery());
                hParameterSet.Value = parameterSet;

                GridView1.DataBind();

                PerformanceLog.EndLog(string.Format("Incident-TicketList-Load: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataUtil.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "ViewItem":
                    WContext context = new WContext(this);
                    context.Set("TicketId", id);
                    context.SetOpen("Incident/Ticket");
                    context.Redirect();
                    break;
            }
        }

        public DataSet Select(int filterBy, int status, int slaStatus, string parameterSet, string keyword)
        {
            var context = new WContext(HttpContext.Current);
            var currentUser = WSession.Current.User;

            var set = !string.IsNullOrEmpty(parameterSet) ? WebParameterSet.Get(parameterSet) : null;
            var isSupportUser = set != null ? IncidentHelper.IsSupportUser(set, currentUser) : false;
            var userDisplayFormatString = set != null ? set.GetParameterValue(IncidentConstants.UserDisplayFormatString) : null;

            var sla = IncidentHelper.GetSlaInfo(set);

            //MemberLink link = null;
            WebUser user = null;
            WebUser assignedTo = null;
            WebGroup supportGroup = null;
            var tickets = IncidentTicket.Provider.GetList();
            string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();

            context.SetOpen("Incident/Ticket");

            var orderedResult = (from item in tickets
                                 where ((user = item.Requestor) != null)
                                    && ((supportGroup = item.AssignedGroup) != null || true)
                                    && (slaStatus == -1 || IncidentHelper.GetSlaStatus(item, sla) == slaStatus)
                                    && (status == -1 ||
                                        (status == TicketStatus.AllClosed && item.Status == TicketStatus.Closed) ||
                                        (status == TicketStatus.AllOpen && item.Status != TicketStatus.Closed) ||
                                        item.Status == status)
                                    && ((assignedTo = item.AssignedUser) != null || true)
                                    && (filterBy == -2
                                        || (filterBy == -1 && (item.AssignedUserId == currentUser.Id || item.SubmitterId == currentUser.Id || item.UserId == currentUser.Id || (!string.IsNullOrEmpty(item.NotifyAlso) && AccountHelper.IsPresentOrMember(item.NotifyAlso, true, true))))
                                        || (filterBy > 0 && (item.AssignedGroupId == filterBy || (!string.IsNullOrEmpty(item.NotifyAlso) && AccountHelper.IsPresentOrMember(item.NotifyAlso, true, false)))))
                                    && (isSupportUser ? true : user.Id == currentUser.Id || currentUser.Id == item.SubmitterId || (!string.IsNullOrEmpty(item.NotifyAlso) && AccountHelper.IsPresentOrMember(item.NotifyAlso)))
                                    && (string.IsNullOrEmpty(kwl) ||
                                           (
                                               DataHelper.HasMatch(item.TicketGuid, kwl)
                                            || DataHelper.HasMatch(user.UserName, kwl)
                                            || DataHelper.HasMatch(user.FirstName, kwl)
                                            || DataHelper.HasMatch(user.LastName, kwl)
                                            || DataHelper.HasMatch(user.MiddleName, kwl)
                                            || DataHelper.HasMatch(user.Email, kwl)
                                            //|| (link != null && DataHelper.HasMatch(link.MemberId, kwl))
                                            || DataHelper.HasMatch(item.Description, kwl)
                                            || (supportGroup != null && DataHelper.HasMatch(supportGroup.Name, kwl))
                                           )
                                       )
                                 select new
                                 {
                                     TicketGuidDisplay = IncidentHelper.FormatIncidentSLA(item, context, sla),
                                     item.TicketGuid,
                                     item.Id,
                                     item.DateCreated,
                                     item.Urgency,
                                     UrgencyDisplay = TicketUrgency.GetText(item.Urgency),
                                     Description = DataHelper.GetStringPreview(item.Description, 38),
                                     Name = AccountHelper.FormatUserDisplay(user, userDisplayFormatString, true, true),
                                     AssignedUserDisplay = AccountHelper.FormatUserDisplay(assignedTo, userDisplayFormatString, true, true),
                                     AssignedUser = assignedTo != null ? assignedTo.FirstAndLastName : "",
                                     AssignedGroup = supportGroup != null ? DataHelper.GetStringPreview(supportGroup.Name, 14) : "",
                                     StatusDisplay = IncidentHelper.FormatStatus(item.Status),
                                     item.Status
                                     //PhotoPath = link.GetPhotoPathIfNull("200x200"),
                                     //UserProfileUrl = string.Format(userDetailsFormat, user.Id),
                                     //Urgency = TicketUrgency.Values[item.Urgency]
                                 }).OrderByDescending(i => i.DateCreated).AsEnumerable();

            return DataUtil.ToDataSet(orderedResult);
        }


        protected void cmdSearch_Click(object sender, EventArgs e)
        {

        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            WContext context = new WContext(this);
            context.Redirect();
        }

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            WContext context = new WContext(this);

            var show = DataUtil.GetInt32(cboStatus.SelectedValue);
            if (show == -2)
                context.Remove("Status");
            else
                context.Set("Status", cboStatus.SelectedValue);

            context.Redirect();
        }

        protected void cboSLA_SelectedIndexChanged(object sender, EventArgs e)
        {
            WContext context = new WContext(this);

            var show = DataUtil.GetInt32(cboSLA.SelectedValue);
            if (show == -1)
                context.Remove("SLA");
            else
                context.Set("SLA", cboSLA.SelectedValue);

            context.Redirect();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            WContext context = new WContext(this);

            context.Remove("SLA");
            context.Remove("Status");
            context.Remove("FilterBy");

            context.Redirect();
        }

        protected void cboFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            WContext context = new WContext(this);

            var show = DataUtil.GetInt32(cboFilterBy.SelectedValue);
            if (show == -1)
                context.Remove("FilterBy");
            else
                context.Set("FilterBy", cboFilterBy.SelectedValue);

            context.Redirect();
        }
    }
}