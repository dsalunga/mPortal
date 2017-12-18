using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class ShortUrlEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var id = DataHelper.GetId(Request, WebColumns.Id);
                if (id > 0)
                {
                    var item = WebShortUrl.Provider.Get(id);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        if (!string.IsNullOrEmpty(item.PageUrl))
                        {
                            txtNavigateURL.Text = item.PageUrl;
                        }
                        else if (item.PageId > 0)
                        {
                            var page = item.Page;
                            if (page != null)
                                txtNavigateURL.Text = page.BuildRelativeUrl();
                        }
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
            var item = id > 0 ? WebShortUrl.Provider.Get(id) : new WebShortUrl();
            if (item != null)
            {
                item.Name = txtName.Text.Trim();
                
                var pageUrl = txtNavigateURL.Text.Trim();
                var page = WPage.Resolve(pageUrl);
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
                item.Update();

                this.ReturnPage();
            }
        }

        private void ReturnPage()
        {
            var context = new WContext(this);
            context.Remove(WebColumns.Id);
            context.RemoveOpen();
            context.Redirect(CentralPages.ShortUrlManager);
        }
    }
}