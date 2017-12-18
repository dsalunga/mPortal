using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Web;
using WCMS.Web.Controls;

namespace WCMS.Web.cmsadmin
{
    public partial class UserProfile : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TabControl1.SelectedTabChanged += (object oSender, TabEventArgs args) =>
                {
                    switch (args.TabName)
                    {
                        case "tabBasic":
                            MultiView1.SetActiveView(viewProfile);
                            break;

                        case "tabRoles":
                            MultiView1.SetActiveView(viewRoles);
                            break;

                        case "tabSecurity":
                            MultiView1.SetActiveView(viewSecurity);
                            break;
                    }
                };

            if (!Page.IsPostBack)
            {
                TabControl1.AddTab("tabBasic", "General");
                TabControl1.AddTab("tabRoles", "Groups");
                TabControl1.AddTab("tabSecurity", "Security", true);
                TabControl1.SelectedIndex = 0;

                int userId = DataHelper.GetDbId(Request[WebColumns.UserId]);
                WebUser user = null;
                if (userId > 0 && (user = WebUser.Get(userId)) != null)
                {
                    FormProfile.LoadData(user.Id);
                    FormSecurity.LoadData(user.Id);
                    FormRoles.LoadData(user.Id);
                }

                MultiView1.SetActiveView(viewProfile);
            }
        }


        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            int userId = DataHelper.GetDbId(Request[WebColumns.UserId]);
            WebUser user = null;
            if (userId > 0 && (user = WebUser.Get(userId)) != null)
            {
                switch (TabControl1.SelectedIndex)
                {
                    case 0:
                    case 1:
                        {
                            // Basic Profile
                            FormProfile.UpdateData();
                            FormRoles.UpdateData();
                            break;
                        }

                    //case 1:
                    //    {
                    //        // Roles
                    //        FormRoles.UpdateData();
                    //        break;
                    //    }

                    case 2:
                        {
                            // Security
                            FormSecurity.UpdateData();
                            break;
                        }
                }
            }
            else
            {
                // Create new user
                user = FormProfile.UpdateData();
                FormRoles.UpdateData(user);
                FormSecurity.UpdateData(user);

                QueryParser qs = new QueryParser(this);
                qs[WebColumns.UserId] = user.Id.ToString();
                qs.Redirect();
            }

            //this.ForwardPage();
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            ForwardPage();
        }

        private void ForwardPage()
        {
            QueryParser qs = new QueryParser(this);
            string sReturn = Request["Return"];

            if (!string.IsNullOrEmpty(sReturn))
            {
                sReturn = HttpUtility.UrlDecode(sReturn);

                Response.Redirect(sReturn, true);
                return;
            }

            qs.Remove(WebColumns.UserId);
            qs.Redirect("~/Central/WebUsers.aspx");
        }
    }
}