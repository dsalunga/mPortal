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
    public partial class WebFolderController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int id = DataHelper.GetId(Request[WebColumns.FolderId]);
                if (id > 0)
                {
                    WebFolder reg = WebFolder.Provider.Get(id);
                    if (reg != null)
                    {
                        txtID.Text = reg.Name;
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
            QueryParser query = new QueryParser(this);
            query.Remove(WebColumns.FolderId);
            query.Redirect(CentralPages.WebDataExplorer);
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            int id = DataHelper.GetId(Request[WebColumns.FolderId]);
            WebFolder item = id > 0 ? WebFolder.Provider.Get(id) : new WebFolder();
            if (item == null)
            {
                item = new WebFolder();
            }

            if (item.Id == -1)
            {
                item.ParentId = DataHelper.GetId(Request[WebColumns.ParentId]);
            }

            item.Name = txtID.Text.Trim();
            item.Update();

            this.ReturnPage();
        }
    }
}