using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public partial class AdminCalendarHome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new WQuery(this);

                int calendarId = query.GetId(WebColumns.Id);
                CalendarItem calendar = CalendarItem.Provider.Get(calendarId);
                if (calendar != null)
                {
                    lblHeader.InnerHtml = calendar.Name;

                    query.SetCmd("CalendarEdit.ascx");
                    linkConfigPage.HRef = query.BuildQuery();


                    var q = new WQuery(this);
                    q.SetEncode(ObjectKey.KeySource, q.BuildQuery());
                    q.Set(ObjectKey.KeyString, new ObjectKey(calendar.OBJECT_ID, calendarId));

                    linkParameters.HRef = q.BuildQuery(CentralPages.WebParameters);
                }
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            int calendarId = query.GetId(WebColumns.Id);
            if (calendarId > 0)
            {
                CalendarItem.Provider.Delete(calendarId);

                ReturnPage();
            }
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.Remove("Id");
            query.Remove(WConstants.Load);
            query.Redirect();
        }
    }
}