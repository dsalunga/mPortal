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
using WCMS.Framework.Diagnostics;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class UserActivitiesView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var today = DateTime.Now.Date;

                // Filter period
                cboPeriod.Items.Add(new ListItem("Today", today.ToString()));
                cboPeriod.Items.Add(new ListItem("Yesterday", today.AddDays(-1).ToString()));
                cboPeriod.Items.Add(new ListItem("This Week", today.AddDays(((int)today.DayOfWeek) * -1).ToString()));

                var thisMonth = new ListItem("This Month", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString());
                thisMonth.Selected = true;
                cboPeriod.Items.Add(thisMonth);
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

                var context = new WContext(this);
                //var partAdmin = context.PartAdmin;
                //if (partAdmin != null)
                //{
                //    hUserFormatString.Value = partAdmin.GetParameterValue("UserDataFormatString");
                //    GridView1.DataBind();
                //}

                int id = context.GetId(WebColumns.UserId);
                if(id > 0)
                {
                    hUserId.Value = id.ToString();
                    GridView1.DataBind();
                }

                panelDelete.Visible = WSession.Current.IsAdministrator;
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
                EventLog.Provider.Delete(DataHelper.GetDateTime(cboDelete.SelectedValue));
                GridView1.DataBind();
            }
        }

        public static DataSet Select(int userId, DateTime fromDate, string userFormatString)
        {
            var items = SelectInternal(userId, fromDate);

            return DataHelper.ToDataSet(
                from l in items
                select new
                {
                    l.Id,
                    l.EventDate,
                    l.EventName,
                    l.IPAddress,
                    l.UserId,
                    User = FormatUserData(l.User, userFormatString)
                }
            );
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

        private static IEnumerable<EventLog> SelectInternal(int userId, DateTime fromDate)
        {
            return EventLog.Provider.GetList(fromDate, userId);
        }

        public static DataSet SelectGrouped(int userId, DateTime fromDate, string groupBy)
        {
            var items = SelectInternal(userId, fromDate);

            switch (groupBy)
            {
                //case "User":
                //    return DataHelper.ToDataSet(SelectInternal(userId, fromDate)
                //        .GroupBy(i => i.UserId)
                //        .Select(i => new
                //        {
                //            ColumnValue = (user = WebUser.Get(i.Key)) == null ? string.Empty : user.FirstAndLastName,
                //            Count = i.Count()
                //        }
                //    ));

                case "Event":
                    return DataHelper.ToDataSet(items
                        .GroupBy(i => i.EventName)
                        .Select(i => new
                        {
                            ColumnValue = i.Key,
                            Count = i.Count()
                        }
                    ));

                case "IPAddress":
                    return DataHelper.ToDataSet(items
                        .GroupBy(i => i.IPAddress)
                        .Select(i => new
                        {
                            ColumnValue = i.Key,
                            Count = i.Count()
                        }
                    ));
            }

            return DataHelper.GetEmptyDataSet();
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
    }
}