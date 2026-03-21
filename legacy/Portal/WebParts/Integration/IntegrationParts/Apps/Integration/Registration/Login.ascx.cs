using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;

namespace WCMS.WebSystem.Apps.Integration.Registration
{
    public partial class Login : System.Web.UI.UserControl
    {
        private const string SignUpUrl = "SignUpUrl";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkForgotPass_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("");
        }

        protected void lnkSignUp_Click(object sender, EventArgs e)
        {
            WebPartContext context = new WebPartContext(this);
            string signUpRedirect = context.PageElement.GetParameterValue(SignUpUrl);
            if (!string.IsNullOrEmpty(signUpRedirect))
            {
                context.Redirect(signUpRedirect);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

        }
    }
}