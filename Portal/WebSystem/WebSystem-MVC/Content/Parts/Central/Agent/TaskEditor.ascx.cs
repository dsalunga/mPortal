using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Agent
{
    public partial class TaskEditor : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Weekdays
                foreach (var kv in WeekdaysEnum.KeyPairs)
                {
                    if (kv.Key > 0)
                        cblWeekdays.Items.Add(new ListItem(kv.Value, kv.Key.ToString()));
                }

                // Recurrence Type
                cboRecurrence.DataSource = RecurrenceType.KeyValues;
                cboRecurrence.DataBind();

                cboExecutionStatus.DataSource = ExecutionStatus.KeyValues;
                cboExecutionStatus.DataBind();

                var id = DataHelper.GetId(Request, "Id");
                if (id > 0)
                {
                    WebJob job = WebJob.Provider.Get(id);
                    if (job != null)
                    {
                        txtName.Text = job.Name;
                        txtDesription.Text = job.Description;
                        txtStartDate.Text = job.StartDate.ToString();
                        cboRecurrence.SelectedValue = job.RecurrenceId.ToString();
                        chkEnabled.Checked = job.IsEnabled;
                        txtTypeName.Text = job.TypeName;
                        txtOccursEvery.Text = job.OccursEvery.ToString();

                        foreach (ListItem item in cblWeekdays.Items)
                        {
                            var value = DataHelper.GetInt32(item.Value);
                            if(value > 0)
                                item.Selected = (job.Weekdays & value) > 0;
                        }

                        txtExecutionStartDate.Text = job.ExecutionStartDate.ToString();
                        txtExecutionEndDate.Text = job.ExecutionEndDate.ToString();
                        cboExecutionStatus.SelectedValue = job.ExecutionStatus.ToString();
                        txtExecutionMessage.Text = job.ExecutionMessage;
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ReturnPage();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var id = DataHelper.GetId(Request, "Id");
            var item = id > 0 ? WebJob.Provider.Get(id) : new WebJob();

            item.Name = txtName.Text.Trim();
            item.RecurrenceId = DataHelper.GetId(cboRecurrence.SelectedValue);

            int weekdays = 0;
            foreach (ListItem i in cblWeekdays.Items)
            {
                if (i.Selected)
                    weekdays += DataHelper.GetInt32(i.Value);
            }

            item.Weekdays = weekdays;
            item.OccursEvery = DataHelper.GetInt32(txtOccursEvery.Text.Trim());
            item.Enabled = chkEnabled.Checked ? 1 : 0;
            item.TypeName = txtTypeName.Text.Trim();
            item.StartDate = DataHelper.GetDateTime(txtStartDate.Text.Trim());
            item.Description = txtDesription.Text.Trim();
            item.ExecutionStatus = DataHelper.GetInt32(cboExecutionStatus.SelectedValue);
            item.Update();

            this.ReturnPage(item.Id);
        }

        private void ReturnPage(int id = -1)
        {
            var query = new WQuery(this);
            if (id == -1)
                id = query.GetId(WebColumns.Id);

            if (id > 0)
            {
                query.Set(WebColumns.Id, id);
                query.LoadAndRedirect("Agent/TaskView");
            }
            else
            {
                query.Remove("Id");
                query.Remove(WConstants.Load);
                query.Redirect();
            }
            //qs.LoadAndRedirect("TaskManager.ascx");
        }
    }
}