using System;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Reflection;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.Controls;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration
{
    public partial class ActivateAccount : System.Web.UI.UserControl
    {
        private const string LoginRedirect = "LoginRedirect";
        private const string GroupsToAddKey = "GroupsToAdd";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                int id = DataUtil.GetId(Request, "id");
                string key = DataUtil.Get(Request, "key");
                if (id > 0 && !string.IsNullOrEmpty(key))
                {
                    WebUser user = WebUser.Get(id);
                    if (user != null)
                    {
                        if (user.ActivationKey == key)
                        {
                            string groups = WebRegistry.SelectNodeValue(MemberConstants.GroupsToAddPath);
                            user.AddToGroups(groups);
                            
                            var link = MemberLink.Provider.GetByUserId(user.Id);
                            if (link != null && link.MemberId <= 0 && !link.IsApproved)
                            {
                                lblMessage.InnerHtml = "Thank you! Your e-mail has been confirmed, however, your account is <strong>Pending Approval</strong> from the Portal administrator. Please wait while your information is being reviewed and you will recieve a notification e-mail once the review is done.";
                            }
                            else
                            {
                                lblMessage.InnerHtml = "Thank you! Your account has been <strong>activated</strong>. Please click the link below to login.";
                                user.IsActive = true;
                            }

                            user.ActivationKey = "";
                            user.Update();
                        }
                    }
                }

                string loginRedirect = context.Element.GetParameterValue(LoginRedirect);
                if (!string.IsNullOrEmpty(loginRedirect))
                {
                    //context.Redirect(loginRedirect);
                    linkLogin.HRef = loginRedirect;
                }
            }
        }
    }
}