using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;


namespace WCMS.WebSystem.WebParts.Incident
{
    public partial class InstanceEditView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var context = new WContext(this);
                var part = context.PartAdmin.Part;

                var set = part.GetParameterSet();
                var id = DataUtil.GetId(Request, "Id");
                if (id > 0)
                {
                    var item = IncidentInstance.Provider.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        //txtDescription.Text = item.Description;
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
            var id = DataUtil.GetId(Request, "Id");
            var item = id > 0 ? IncidentInstance.Provider.Get(id) : new IncidentInstance();

            item.Name = txtName.Text.Trim();
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