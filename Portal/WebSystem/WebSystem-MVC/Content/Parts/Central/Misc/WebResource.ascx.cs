using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebResourceController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cboContentType.DataSource = WebTextResource.GetContentTypes();
                cboContentType.DataBind();

                int webTextResourceId = DataHelper.GetId(Request, WebColumns.TextResourceId);
                if (webTextResourceId > 0)
                {
                    WebTextResource item = WebTextResource.Get(webTextResourceId);
                    cboContentType.SelectedValue = item.ContentTypeId.ToString();
                    txtTitle.Text = item.Title;
                    txtContent.Text = item.Content;
                    txtRank.Text = item.Rank.ToString();
                    txtPermalink.Text = item.Permalink;
                    txtPhysicalPath.Text = item.BuildRelativePhysicalPath();
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Redirect();
        }

        private void Redirect()
        {
            QueryParser query = new QueryParser(this);
            query.Remove(WebColumns.TextResourceId);

            if (query.HasSourceValue)
                query.RedirectToSource();
            else
                query.Redirect(CentralPages.WebResources);
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            string keyString = context[ObjectKey.KeyString];

            int webTextResourceId = context.GetId(WebColumns.TextResourceId);

            var item = webTextResourceId < 1
                ? new WebTextResource() : WebTextResource.Get(webTextResourceId);

            item.Content = txtContent.Text.Trim();
            item.Title = txtTitle.Text.Trim();
            item.ContentTypeId = DataHelper.GetId(cboContentType.SelectedValue);
            item.Rank = DataHelper.GetInt32(txtRank.Text);
            item.DatePersisted = DateTime.Now;
            item.DateModified = DateTime.Now;
            item.PhysicalPath = txtPhysicalPath.Text.Trim();
            item.Update();

            if (!string.IsNullOrEmpty(keyString))
            {
                ObjectKey key = new ObjectKey(keyString);

                // If not yet existing then add to Site
                if (webTextResourceId < 1)
                    WebObjectHeader.AddHeader(key.ObjectId, key.RecordId, item.Id);
            }

            this.Redirect();
        }

        protected void cmdOpen_Click(object sender, EventArgs e)
        {

        }
    }
}