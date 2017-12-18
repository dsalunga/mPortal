using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WCMS.WebSystem.Controls
{
    public partial class ComboDatePicker : System.Web.UI.UserControl
    {
        private DateTime selectedDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Fill Combo
                DateTimeFormatInfo fi = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat;
                for (int i = 1; i < 13; i++)
                {
                    cboMonth.Items.Add(new ListItem(fi.GetMonthName(i), i.ToString()));
                }

                for (int i = 1; i < 32; i++)
                {
                    cboDay.Items.Add(i.ToString());
                }

                for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 60; i--)
                {
                    cboYear.Items.Add(i.ToString());
                }

                cboDay.SelectedValue = selectedDate.Day.ToString();
                cboMonth.SelectedValue = selectedDate.Month.ToString();
                cboYear.SelectedValue = selectedDate.Year.ToString();
            }
        }

        public DateTime SelectedDate
        {
            get
            {
                try
                {
                    selectedDate = new DateTime(Convert.ToInt32(cboYear.SelectedValue), Convert.ToInt32(cboMonth.SelectedValue), Convert.ToInt32(cboDay.SelectedValue));
                }
                catch
                {
                    selectedDate = DateTime.Now;
                }

                return selectedDate;
            }

            set
            {
                selectedDate = value;
            }
        }
    }
}