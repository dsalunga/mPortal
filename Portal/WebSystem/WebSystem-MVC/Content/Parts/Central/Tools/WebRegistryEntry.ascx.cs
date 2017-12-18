using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class WebRegistryEntryController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextEditor1.EditorToolbarSet = "Basic";
                txtParent.Text = DataHelper.Get(Request, WebColumns.ParentId);
                int id = DataHelper.GetId(Request, WebColumns.RegistryId);
                if (id > 0)
                {
                    var item = WebRegistry.Get(id);
                    if (item != null)
                    {
                        txtID.Text = item.Key;
                        TextEditor1.Text = item.Value;
                        txtParent.Text = item.ParentId.ToString();
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ReturnPage();
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.Remove(WebColumns.RegistryId);
            query.Redirect(CentralPages.WebRegistry);
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            int id = DataHelper.GetId(Request, WebColumns.RegistryId);
            var parentId = DataHelper.GetId(txtParent.Text.Trim());
            var item = id > 0 ? WebRegistry.Get(id) : new WebRegistry();
            if (item == null)
                item = new WebRegistry();
            item.Key = txtID.Text.Trim();
            item.Value = TextEditor1.Text;
            item.ParentId = parentId;
            item.Update();

            this.ReturnPage();
        }
    }
}