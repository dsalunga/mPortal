using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WCMS.WebSystem.WebParts.WeeklyScheduler
{
    public partial class WeeklyScheduler : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WeeklySchedulerPresenter presenter = new WeeklySchedulerPresenter("~/Content/Parts/WeeklyScheduler/Templates/Default.xml");

                Literal l = new Literal();
                l.Text = presenter.Render();

                this.Controls.Add(l);
            }
        }
    }
}