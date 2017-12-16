using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public partial class CalendarEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cboSites.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());

                var id = DataHelper.GetId(Request, "Id");
                if (id > 0)
                {
                    var item = CalendarItem.Provider.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;

                        cboSites.DataBind();
                        WebHelper.SetCboValue(cboSites, item.SiteId);
                    }
                }

                var siteId = DataHelper.GetId(Request, WebColumns.SiteId);
                if (siteId > 0)
                {
                    if (cboSites.SelectedIndex == 0)
                        WebHelper.SetCboValue(cboSites, siteId);

                    cboSites.Enabled = false;
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            var id = DataHelper.GetId(Request, "Id");

            this.ReturnPage(id);
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var id = DataHelper.GetId(Request, "Id");
            int siteId = DataHelper.GetId(cboSites.SelectedValue);

            var item = id > 0 ? CalendarItem.Provider.Get(id) : new CalendarItem();

            item.Name = txtName.Text.Trim();
            item.SiteId = siteId;
            item.Update();


            this.ReturnPage(item.Id);
        }

        private void ReturnPage(int id = -1)
        {
            var query = new WQuery(this);
            if (id > 0)
            {
                query.SetCmd("AdminCalendarHome.ascx");
                query.Set(WebColumns.Id, id);
            }
            else
            {
                query.Remove("Id");
                query.Remove(WConstants.Load);
            }
            query.Redirect();
        }
    }
}