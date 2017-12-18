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
using WCMS.Framework.Net;
using WCMS.Framework.Diagnostics;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class MessageQueueManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime today = DateTime.Now.Date;

                // Filter period
                cboPeriod.Items.Add(new ListItem("Today", today.ToString()));
                cboPeriod.Items.Add(new ListItem("Yesterday", today.AddDays(-1).ToString()));
                cboPeriod.Items.Add(new ListItem("This Week", today.AddDays(((int)today.DayOfWeek) * -1).ToString()));
                cboPeriod.Items.Add(new ListItem("This Month", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString()));
                cboPeriod.Items.Add(new ListItem("This Year", new DateTime(DateTime.Now.Year, 1, 1).ToString()));
                cboPeriod.Items.Add(new ListItem("All Time", WConstants.DateTimeMinValue.ToString()));

                // Delete period
                if (cboDelete.Visible)
                {
                    cboDelete.Items.Add(new ListItem("Older than 1 year", today.AddYears(-1).ToString()));
                    cboDelete.Items.Add(new ListItem("Older than 6 months", today.AddMonths(-6).ToString()));
                    cboDelete.Items.Add(new ListItem("Older than 3 months", today.AddMonths(-3).ToString()));
                    cboDelete.Items.Add(new ListItem("Older than 1 month", today.AddMonths(-1).ToString()));
                }

                WContext context = new WContext(this);
                var partAdmin = context.PartAdmin;
                if (partAdmin != null)
                {
                    hUserFormatString.Value = partAdmin.GetParameterValue("UserDataFormatString");
                    GridView1.DataBind();
                }
            }
        }

        protected void cmdDownload_Click(object sender, ImageClickEventArgs e)
        {
            //int roleId = -1;
            //if (!cboDelete.Visible) roleId = UserSession.RoleID;

            //DataSet ds = Audit.GetDataSetForDownload(null, roleId, -1, -1);
            //WebHelper.DownloadDataSet(ds);
        }

        protected void cmdClearLog_Click(object sender, EventArgs e)
        {
            if (cboDelete.Visible)
            {
                //WebMessageQueue.Provider.Delete(DataHelper.GetDateTime(cboDelete.SelectedValue));
                GridView1.DataBind();
            }
        }

        public static DataSet Select(DateTime fromDate, string userFormatString)
        {
            var items = SelectInternal(fromDate);

            return DataHelper.ToDataSet(
                from l in items
                orderby l.DateCreated descending
                select new
                {
                    l.Id,
                    l.DateCreated,
                    l.DateSent,
                    l.EmailSubject,
                    l.EmailMessage,
                    l.SendVia,
                    l.SmsMessage,
                    l.Status,
                    l.To,
                    l.ToFailed,
                    User = FormatUserData(l.Sender, userFormatString)
                }
            );
        }

        public static DataSet SelectGrouped(DateTime fromDate, string groupBy)
        {
            WebUser user = null;
            switch (groupBy)
            {
                case "User":
                    return DataHelper.ToDataSet(SelectInternal(fromDate)
                        .GroupBy(i => i.Sender.Id)
                        .Select(i => new
                        {
                            ColumnValue = (user = WebUser.Get(i.Key)) == null ? string.Empty : user.FirstAndLastName,
                            Count = i.Count()
                        }
                    ));
            }

            return DataHelper.GetEmptyDataSet();
        }

        private static string FormatUserData(WebUser user, string formatString)
        {
            if (user != null && !string.IsNullOrEmpty(formatString))
            {
                var provider = new NamedValueProvider();
                provider.Add("UserId", user.Id);
                provider.Add("Name", user.FirstAndLastName);

                return Substituter.Substitute(formatString, provider);
            }

            return string.Empty;
        }

        private static IEnumerable<WebMessageQueue> SelectInternal(DateTime fromDate)
        {
            return WebMessageQueue.Provider.GetList();
        }

        protected void cboGroupBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cboGroupBy.SelectedValue;
            if (string.IsNullOrEmpty(selectedValue))
            {
                MultiView1.SetActiveView(viewDetailed);
                GridView1.DataBind();
            }
            else
            {
                MultiView1.SetActiveView(viewGrouped);
                GridViewGrouped.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Custom_Delete":
                    var id = DataHelper.GetId(e.CommandArgument);
                    if (id > 0)
                    {
                        WebMessageQueue.Provider.Delete(id);
                        GridView1.DataBind();
                    }
                    break;
            }
        }
    }
}