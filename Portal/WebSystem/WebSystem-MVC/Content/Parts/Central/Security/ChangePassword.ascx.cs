using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class ChangePassword : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int userId = DataHelper.GetId(Request, WebColumns.UserId);
                WebUser user = null;
                if (userId > 0 && (user = WebUser.Get(userId)) != null)
                    FormSecurity.LoadData(user);
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);

            WebUser user = null;
            int userId = query.GetId(WebColumns.UserId);

            if (userId > 0 && (user = WebUser.Get(userId)) != null)
            {
                if (string.IsNullOrEmpty(FormSecurity.UpdateData(user)))
                    ForwardPage(user);
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            ForwardPage();
        }

        private void ForwardPage(WebUser user = null)
        {
            QueryParser query = new QueryParser(this);

            string returnUrl = query.Get(QueryParser.SourceKey);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = HttpUtility.UrlDecode(returnUrl);
                query.Redirect(returnUrl);
                return;
            }

            int userId = user != null ? user.Id : query.GetId(WebColumns.UserId);
            if (userId > 0)
                query.Redirect(CentralPages.WebUserHome);
            else
                query.Redirect(CentralPages.WebUsers);
        }
    }
}