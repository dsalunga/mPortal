using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public partial class AdminCategoryEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cboTemplates.DataSource = CalendarTemplate.Provider.GetList();
                cboTemplates.DataBind();

                var id = DataHelper.GetId(Request, "Id");
                if (id > 0)
                {
                    var item = CalendarCategory.Provider.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;

                        if (item.TemplateId > 0)
                        {
                            if (cboTemplates.Items.FindByValue(item.TemplateId.ToString()) != null)
                                cboTemplates.SelectedValue = item.TemplateId.ToString();
                        }
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
            var item = id > 0 ? CalendarCategory.Provider.Get(id) : new CalendarCategory();

            item.Name = txtName.Text.Trim();
            item.TemplateId = DataHelper.GetId(cboTemplates.SelectedValue);
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