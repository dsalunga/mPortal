using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class WebObjectEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WContext context = new WContext(this);

                var id = DataHelper.GetId(Request, WebColumns.Id);
                if (id > 0)
                {
                    var item = WebObject.Provider.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        txtIdentityColumn.Text = item.IdentityColumn;

                        if (cboCacheType.Items.FindByValue(item.CacheTypeId.ToString()) != null)
                            cboCacheType.SelectedValue = item.CacheTypeId.ToString();

                        txtDataProviderName.Text = item.DataProviderName;
                        txtTypeName.Text = item.TypeName;
                        txtManagerName.Text = item.ManagerName;
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
            var id = DataHelper.GetId(Request, WebColumns.Id);
            var item = id > 0 ? WebObject.Provider.Get(id) : null; //new WebObject();

            if (item != null)
            {
                var oldCacheFlag = item.CacheTypeId;

                item.Name = txtName.Text.Trim();
                item.IdentityColumn = txtIdentityColumn.Text.Trim();
                item.CacheTypeId = DataHelper.GetId(cboCacheType.SelectedValue);
                item.DataProviderName = txtDataProviderName.Text.Trim();
                item.TypeName = txtTypeName.Text.Trim();
                item.ManagerName = txtManagerName.Text.Trim();
                item.Update();

                if (oldCacheFlag != item.CacheTypeId)
                    WebObject.OnCacheFlagUpdated(oldCacheFlag, item);

                this.ReturnPage();
            }
            else
            {
                // Error here
            }
        }

        private void ReturnPage()
        {
            WContext context = new WContext(this);
            context.Remove(WebColumns.Id);
            context.Open();
        }
    }
}