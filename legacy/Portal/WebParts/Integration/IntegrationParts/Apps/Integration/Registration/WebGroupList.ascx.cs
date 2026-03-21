using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Diagnostics;

namespace WCMS.WebSystem.Apps.Integration
{
    public partial class WebGroupList : System.Web.UI.UserControl
    {
        private const string GroupPathKey = "GroupPath";
        private const string ListColumnsKey = "ListColumns";
        private const string GroupPathDefault = "/Integration/Chapters/Singapore/Ministries";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var sw = PerformanceLog.StartLog();

                WContext context = new WContext(this);
                var element = context.Element;

                string listColumns = element.GetParameterValue(ListColumnsKey, "4");
                string groupPath = element.GetParameterValue(GroupPathKey, GroupPathDefault);
                string formatString = element.GetParameterValue("ListNameFormatString", "");
                string accessDeniedContent = element.GetParameterValue("AccessDeniedContent", "");
                bool isPermitted = false;

                var targetGroup = WebGroup.SelectNode(groupPath);
                if (targetGroup != null)
                {
                    var items = targetGroup.Children;
                    //DataList1.RepeatColumns = DataHelper.GetInt32(listColumns);
                    bool formatStringIsNull = string.IsNullOrWhiteSpace(formatString);

                    Repeater1.DataSource = from item in items
                                           where ((isPermitted = IsPermitted(item)) || true)
                                           select new
                                           {
                                               DisplayHTML = formatStringIsNull ? item.DisplayHTML : formatString.Replace("$(Name)", item.Name),
                                               PageUrl = string.IsNullOrEmpty(item.EvalPageUrl) ? "#" : item.EvalPageUrl,
                                               item.PageId,
                                               IsPermitted = isPermitted,
                                               AccessDeniedContent = accessDeniedContent
                                           };

                    Repeater1.DataBind();
                }

                PerformanceLog.EndLog(string.Format("Integration-WebGroupList-Load: {0}/{1}", context.ObjectId, context.RecordId), sw, context.PageId);
            }
        }

        private bool IsPermitted(WebGroup m)
        {
            if (m.PageId > 0 || !string.IsNullOrWhiteSpace(m.PageUrl)) //(m.RequireApproval && (m.PageId > 0 || !string.IsNullOrWhiteSpace(m.PageUrl)))
            {
                var page = m.Page;
                if (page != null)
                    return page.IsUserPermitted(Permissions.PublicRead, 1);
            }

            return true;
        }
    }
}