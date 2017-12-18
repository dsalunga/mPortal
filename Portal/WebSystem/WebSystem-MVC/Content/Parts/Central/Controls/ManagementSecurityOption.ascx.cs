using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class ManagementSecurityOption : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public int Value
        {
            get
            {
                return DataHelper.GetInt32(RadioButtonList1.SelectedValue);
            }

            set
            {
                RadioButtonList1.SelectedValue = value.ToString();
            }
        }
    }
}