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
    public partial class CategoryEditView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var part = context.PartAdmin.Part;

                var set = part.GetParameterSet();
                var supportGroup = IncidentHelper.GetBaseSupportGroup(set);
                if (supportGroup != null)
                {
                    cboGroup.DataSource = supportGroup.Children;
                    cboGroup.DataBind();
                }

                cboInstance.DataSource = IncidentInstance.Provider.GetList();
                cboInstance.DataBind();

                var id = DataUtil.GetId(Request, "Id");
                if (id > 0)
                {
                    var item = IncidentCategory.Provider.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        txtDescription.Text = item.Description;
                        txtRank.Text = item.Rank.ToString();

                        WebUtil.SetCboValue(cboGroup, item.GroupId);
                        WebUtil.SetCboValue(cboInstance, item.InstanceId);
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            ReturnPage();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var id = DataUtil.GetId(Request, "Id");
            var item = id > 0 ? IncidentCategory.Provider.Get(id) : new IncidentCategory();

            item.Name = txtName.Text.Trim();
            item.Description = txtDescription.Text.Trim();
            item.GroupId = DataUtil.GetId(cboGroup.SelectedValue);
            item.Rank = DataUtil.GetInt32(txtRank.Text.Trim());
            item.InstanceId = DataUtil.GetId(cboInstance.SelectedValue);
            item.Update();

            ReturnPage();
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