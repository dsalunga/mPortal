using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Agent
{
    public partial class TaskManager : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);
            var query = new WQuery(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.Id, id);
                    query.LoadAndRedirect("Agent/TaskView");
                    break;

                case "Custom_Delete":
                    var item = WebJob.Provider.Get(id);
                    if (item != null)
                        item.Delete();

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

        public DataSet Select(string keyword)
        {
            string kwl = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();

            return DataHelper.ToDataSet(from i in WebJob.Provider.GetList()
                                        /*where
                                            ((user = i.User) != null || user == null) &&
                                            ((status = i.Approved == MemberStatus.Approved ? MemberStatus.ApprovedString : MemberStatus.PendingString) != null) &&
                                            (string.IsNullOrEmpty(kwl) ||
                                                (
                                                    (user != null &&
                                                        (user.UserName.ToLower().Contains(kwl) ||
                                                            user.FullName.ToLower().Contains(kwl) ||
                                                            user.Email.ToLower().Contains(kwl))
                                                    ) ||
                                                    (!string.IsNullOrWhiteSpace(i.MemberId) && i.MemberId.ToLower().Contains(kwl)) ||
                                                    (status.ToLower().Contains(kwl))
                                                )
                                             )*/
                                        select new
                                        {
                                            i.Id,
                                            i.Name,
                                            i.ExecutionStartDate,
                                            i.ExecutionEndDate,
                                            i.ExecutionStatus,
                                            StatusString = ExecutionStatus.ToString(i.ExecutionStatus),
                                            i.ExecutionMessage,
                                            i.Enabled,
                                            i.TypeName,
                                            i.StartDate,
                                            i.RecurrenceId,
                                            RecurrenceString = RecurrenceType.GetName(i.RecurrenceId),
                                            i.Weekdays,
                                            WeekdaysString = WeekdaysEnum.ToShortString(i.Weekdays),
                                            NextExecution = GetNextExecutionText(i)
                                        });
        }

        private string GetNextExecutionText(WebJob i)
        {
            var now = DateTime.Now;

            var next = i.GetNextOccurence(now);
            if (next == now)
                return "Immediate";
            else if (next == WConstants.DateTimeMinValue)
                return "Never";

            return next.ToString();
        }

        protected void cmdSync_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void cmdNew_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.LoadAndRedirect("Agent/TaskEditor");
        }
    }
}