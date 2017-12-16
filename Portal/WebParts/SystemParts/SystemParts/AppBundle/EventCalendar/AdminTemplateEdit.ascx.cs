using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public partial class AdminTemplateEdit : System.Web.UI.UserControl
    {
        protected TextEditor txtTemplate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var id = DataHelper.GetId(Request, "Id");
                if (id > 0)
                {
                    var item = CalendarTemplate.Provider.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        txtTemplate.Text = item.ReminderHtml;
                        txtForeColor.Text = item.ForeColor;
                        txtBackColor.Text = item.BackColor;
                        txtSmsContent.Text = item.SmsContent;
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
            var item = id > 0 ? CalendarTemplate.Provider.Get(id) : new CalendarTemplate();

            item.Name = txtName.Text.Trim();
            item.ReminderHtml = txtTemplate.Text.Trim();
            item.ForeColor = txtForeColor.Text.Trim();
            item.BackColor = txtBackColor.Text.Trim();
            item.SmsContent = txtSmsContent.Text.Trim();
            item.Update();

            this.ReturnPage();
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