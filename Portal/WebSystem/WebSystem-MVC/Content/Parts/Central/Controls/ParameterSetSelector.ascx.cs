using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class ParameterSetSelector : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        public int ParameterSetId
        {
            get { return DataHelper.GetId(cboParameterSet.SelectedValue); }
            set
            {
                if (cboParameterSet.Items.Count == 1)
                    cboParameterSet.DataBind();

                WebHelper.SetCboValue(cboParameterSet, value);
            }
        }

        public WebParameterSet GetParameterSet()
        {
            var parameterSetId = ParameterSetId;
            if (parameterSetId > 0)
                return WebParameterSet.Provider.Get(parameterSetId);

            return null;
        }

        public IEnumerable<WebParameterSet> Select()
        {
            return WebParameterSet.Provider.GetList();
        }
    }
}