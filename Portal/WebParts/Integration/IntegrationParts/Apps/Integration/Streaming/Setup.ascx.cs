using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Integration;

namespace WCMS.WebSystem.Apps.Integration.Streaming
{
    public partial class Setup : System.Web.UI.UserControl
    {
        protected ServicePicker ServicePicker1;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void Initialize(int localeId)
        {
            ServicePicker1.Initialize(localeId);

            var context = new WContext(this);
            var set = context.Element.GetParameterSet(true);

            WebHelper.SetCboValue(cboStreamType, set.GetParameterValue("StreamType", AttendanceTypes.Live.ToString()));
            hServiceScheduleId.Value = set.GetParameterValue("ServiceScheduleId", "-1");
            txtStartDate.Text = set.GetParameterValue("StartTime", "");
            cboDuration.SelectedValue = TimeSpan.ParseExact(set.GetParameterValue("Duration", "00:00"), @"hh\:mm", CultureInfo.CurrentCulture).ToString(@"hh\:mm");
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var streamType = DataUtil.GetInt32(cboStreamType.SelectedValue);
            var serviceScheduleId = chkSchedule.Checked ? DataUtil.GetId(hServiceScheduleId.Value) : -1;
            var startDateTime = !string.IsNullOrEmpty(txtStartDate.Text) ? DataUtil.GetDateTime(txtStartDate.Text.Trim()) : new DateTime();
            var duration = TimeSpan.ParseExact(cboDuration.SelectedValue, @"hh\:mm", CultureInfo.CurrentCulture);

            var context = new WContext(this);
            var set = context.Element.GetParameterSet(true);

            set.GetOrCreateParameter("StreamType", streamType.ToString()).Update();
            set.GetOrCreateParameter("ServiceScheduleId", serviceScheduleId.ToString()).Update();
            set.GetOrCreateParameter("StartTime", startDateTime.Ticks == 0 ? "" : startDateTime.ToString("dd-MMM-yyyy hh:mm tt")).Update();
            set.GetOrCreateParameter("Duration", duration.ToString(@"hh\:mm")).Update();

            hServiceScheduleId.Value = serviceScheduleId.ToString();
        }
    }
}