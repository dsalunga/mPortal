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
using System.Xml.Linq;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public partial class AdminLocation : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var id = DataHelper.GetId(Request, "Id");
                if (id > 0)
                {
                    var item = CalendarLocation.Provider.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        chkBookable.Checked = item.IsBookable;
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
            var item = id > 0 ? CalendarLocation.Provider.Get(id) : new CalendarLocation();

            item.Name = txtName.Text.Trim();
            item.IsBookable = chkBookable.Checked;
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