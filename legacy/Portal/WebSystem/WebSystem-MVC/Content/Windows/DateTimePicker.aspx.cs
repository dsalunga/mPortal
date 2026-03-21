using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Globalization;

namespace WCMS.WebSystem.Windows
{
    public partial class DateTimePicker : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initialize();

                var mode = Request["Mode"];
                hInline.Value = !string.IsNullOrEmpty(mode) && mode.Equals("inline", StringComparison.InvariantCultureIgnoreCase) ? "1" : "0";

                string paramValue = Request["Value"];
                if (!string.IsNullOrEmpty(paramValue))
                {
                    DateTime dateValue;
                    if (DateTime.TryParse(HttpUtility.UrlDecode(paramValue), out dateValue))
                    {
                        cboMonth.SelectedValue = dateValue.Month.ToString();
                        cboYear.SelectedValue = dateValue.Year.ToString();
                        Calendar1.VisibleDate = dateValue.Date;
                        Calendar1.SelectedDate = dateValue.Date;

                        if (chkTime.Checked = (dateValue.TimeOfDay.Ticks > 0))
                        {
                            cboHour.SelectedValue = dateValue.ToString("hh");
                            cboMinute.SelectedValue = dateValue.ToString("mm");
                            cboSecond.SelectedValue = dateValue.ToString("tt");
                        }
                    }
                    else
                    {
                        // Set default values
                        SetupDefaults();
                    }
                }
                else
                {
                    // Set default values
                    SetupDefaults();
                }
            }

            Body.Attributes["onload"] = "pageLoad();";
        }

        private void Initialize()
        {
            // Setup months
            string[] monthNames = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames;
            for (int i = 1; i < monthNames.Length; i++)
                cboMonth.Items.Add(new ListItem(monthNames[i - 1], i.ToString()));

            // Setup years
            for (int i = DateTime.Now.Year + 10; i > DateTime.Now.Year - 25; i--)
                cboYear.Items.Add(i.ToString());


            // Setup time
            // Hours
            cboHour.Items.Add(new ListItem("12", "0"));
            for (int i = 1; i < 12; i++)
                cboHour.Items.Add(i.ToString("00"));

            // Minutes and seconds
            for (int i = 0; i < 60; i++)
            {
                if (i % 5 == 0)
                {
                    string value = i.ToString("00");
                    cboMinute.Items.Add(value);
                }
            }

            chkTime.Attributes["onclick"] = "toggleTime(this);";
        }

        private void SetupDefaults()
        {
            cboMonth.SelectedValue = DateTime.Now.Month.ToString();
            cboYear.SelectedValue = DateTime.Now.Year.ToString();

            Calendar1.SelectedDate = DateTime.Now.Date;
            Calendar1.VisibleDate = DateTime.Now.Date;
            chkTime.Checked = false;
        }

        private void SetupNow()
        {
            var minute = DateTime.Now.Minute;
            cboMonth.SelectedValue = DateTime.Now.Month.ToString();
            cboYear.SelectedValue = DateTime.Now.Year.ToString();

            cboHour.SelectedValue = DateTime.Now.ToString("hh");
            cboMinute.SelectedValue = (minute - minute % 5).ToString().PadLeft(2, '0');
            cboSecond.SelectedValue = DateTime.Now.ToString("tt");

            Calendar1.SelectedDate = DateTime.Now.Date;
            Calendar1.VisibleDate = DateTime.Now.Date;
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCalendar();
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCalendar();
        }

        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            cboMonth.SelectedValue = e.NewDate.Month.ToString();
            cboYear.SelectedValue = e.NewDate.Year.ToString();
        }

        private void UpdateCalendar()
        {
            int year = Convert.ToInt32(cboYear.SelectedValue);
            int month = Convert.ToInt32(cboMonth.SelectedValue);
            Calendar1.VisibleDate = new DateTime(year, month, 1);
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            bool isPM = cboSecond.SelectedValue == "PM";
            int hours = Convert.ToInt32(cboHour.SelectedValue) + (isPM ? 12 : 0);

            TimeSpan time = new TimeSpan(hours, Convert.ToInt32(cboMinute.SelectedValue), 0);
            //string time = string.Format("{0}:{1}:{2}", cboHour.SelectedValue, cboMinute.SelectedValue, cboSecond.SelectedValue);

            DateTime dateTime = Calendar1.SelectedDate.Add(time);
            hiddenValue.Value = dateTime.ToString("dd-MMM-yyyy") + (chkTime.Checked ? " " + dateTime.ToString("hh:mm tt") : string.Empty);
            Body.Attributes["onload"] = "returnDate();"; // Override pageLoad
        }

        protected void cmdToday_Click(object sender, EventArgs e)
        {
            SetupNow();
        }
    }
}
