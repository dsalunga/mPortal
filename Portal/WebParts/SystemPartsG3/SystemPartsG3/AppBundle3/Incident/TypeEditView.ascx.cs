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
    public partial class TypeEditView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WContext context = new WContext(this);
                var part = context.PartAdmin.Part;

                var id = DataUtil.GetId(Request, "Id");
                if (id > 0)
                {
                    var item = IncidentType.Provider.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        txtRank.Text = item.Rank.ToString();
                        chkSLA.Checked = item.UseSLA;
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
            var item = id > 0 ? IncidentType.Provider.Get(id) : new IncidentType();

            item.Name = txtName.Text.Trim();
            item.Rank = DataUtil.GetInt32(txtRank.Text.Trim());
            item.FollowStdSLA = chkSLA.Checked ? 1 : 0;
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