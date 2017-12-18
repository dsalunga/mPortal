using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class WebGroupController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser query = new QueryParser(this);
                int id = query.GetId(WebColumns.GroupId);

                if (id > 0)
                {
                    WebGroup item = WebGroup.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        chkJoinApproval.Checked = item.JoinApproval == 1;

                        var owner = item.Owner;
                        if (owner != null)
                            txtOwner.Text = owner.UserName;

                        var parent = item.Parent;
                        if (parent != null)
                            txtParent.Text = parent.Name;

                        txtManagers.Text = item.Managers;
                        txtPageUrl.Text = item.EvalPageUrl;
                        txtDescription.Text = item.Description;
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.Redirect();
        }

        private void Redirect()
        {
            QueryParser query = new QueryParser(this);
            int id = query.GetId(WebColumns.GroupId);

            if (id > 0)
                query.Redirect(CentralPages.WebGroupHome);
            else
                query.Redirect(CentralPages.WebGroups);
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            int id = DataHelper.GetId(Request, WebColumns.GroupId);
            string pageUrl = txtPageUrl.Text.Trim();

            var owner = WebUser.Get(txtOwner.Text.Trim());
            var parent = WebGroup.Get(txtParent.Text.Trim());
            WPage page = string.IsNullOrEmpty(pageUrl) ? null : WebRewriter.ResolvePage(pageUrl);

            var item = (id > 0) ? WebGroup.Get(id) : new WebGroup();
            item.Name = txtName.Text.Trim();
            item.Description = txtDescription.Text.Trim();
            item.Managers = txtManagers.Text.Trim();
            item.OwnerId = owner == null ? -1 : owner.Id;
            item.ParentId = parent == null ? -1 : parent.Id;

            if (page != null)
            {
                item.PageId = page.Id;
                item.PageUrl = string.Empty;
            }
            else
            {
                item.PageId = -1;
                item.PageUrl = pageUrl;
            }

            item.JoinApproval = chkJoinApproval.Checked ? 1 : 0;
            item.Update();

            this.Redirect();
        }
    }
}