using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WCMS.WebSystem.Controls
{
    public partial class FullNamePicker : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string FirstName
        {
            get
            {
                return txtFirstName.Text.Trim();
            }

            set
            {
                txtFirstName.Text = value;
            }
        }

        public string MiddleName
        {
            get
            {
                return txtMiddleName.Text.Trim();
            }

            set
            {
                txtMiddleName.Text = value;
            }
        }

        public string LastName
        {
            get
            {
                return txtLastName.Text.Trim();
            }

            set
            {
                txtLastName.Text = value;
            }
        }
    }
}