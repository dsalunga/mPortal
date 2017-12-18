using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Misc
{
    public partial class WebParameterSetController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser query = new QueryParser(this);

                WebParameterSet set = WebParameterSet.GetSchemaSet();
                if (set != null)
                {
                    var parms = set.Parameters;
                    foreach (var parm in parms)
                        cboSchema.Items.Add(new ListItem(parm.Name));
                }

                WebParameterSet item = null;

                int id = query.GetId(WebColumns.ParameterSetId);
                if (id > 0 && (item = WebParameterSet.Provider.Get(id)) != null)
                {
                    txtName.Text = item.Name;

                    if (!string.IsNullOrEmpty(item.SchemaParameterName))
                    {
                        var listItem = cboSchema.Items.FindByValue(item.SchemaParameterName);
                        if (listItem != null)
                            cboSchema.SelectedValue = listItem.Value;
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.Redirect();
        }

        private void Redirect(int parameterSetId = -1)
        {
            QueryParser query = new QueryParser(this);
            int id = parameterSetId > 0 ? parameterSetId : query.GetId(WebColumns.ParameterSetId);

            if (id > 0)
            {
                query.Set(WebColumns.ParameterSetId, id);
                query.Redirect(CentralPages.WebParameterSetHome);
            }
            else
            {
                query.Redirect(CentralPages.WebParameterSets);
            }
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            int id = DataHelper.GetId(Request, WebColumns.ParameterSetId);

            WebParameterSet item = (id > 0) ? WebParameterSet.Provider.Get(id) : new WebParameterSet();
            item.Name = txtName.Text.Trim();
            item.SchemaParameterName = cboSchema.SelectedValue;
            item.Update();

            this.Redirect(item.Id);
        }
    }
}