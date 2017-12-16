using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Content
{
    public partial class ConfigOpen : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdOpenContent_Click(object sender, EventArgs e)
        {
            int contentId = DataHelper.GetId(txtContentId.Text.Trim());

            // Replace or create new Content-Object link
            IPageElement item = WHelper.GetCurrentWebElement();
            if (item != null)
            {
                var objectContent = WebObjectContent.Get(item);
                if (objectContent != null && objectContent.Content != null)
                {
                    //WebObjectContent objectContent = item.ObjectContent;
                    objectContent.ContentId = contentId;
                    objectContent.Update();
                }
                else
                {
                    objectContent = new WebObjectContent();
                    objectContent.ContentId = contentId;
                    objectContent.ObjectId = item.OBJECT_ID;
                    objectContent.RecordId = item.Id;
                    objectContent.Update();
                }

                this.Return();
            }

            //if (contentId > 0)
            //{

            //}
        }

        private void Return()
        {
            var query = new WQuery(this);
            query.LoadAndRedirect("ConfigContent.ascx");
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Return();
        }
    }
}