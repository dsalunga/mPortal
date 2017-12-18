using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebPartController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                QueryParser query = new QueryParser(this);
                int partId = query.GetId(WebColumns.PartId);
                WPart part = null;
                if (partId > 0 && (part = WPart.Get(partId)) != null)
                {
                    txtName.Text = part.Name;
                    txtIdentity.Text = part.Identity;
                    //txtdesc.Text = row.Description;
                    chkActive.Checked = part.Active == 1;
                }
                else
                {
                    // LOAD DEFAULT VALUES FOR NEW SITE
                    // GENERATE NEW RANK
                    // IS THERE A NEED?
                }
            }
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.Return();
        }

        private void Return()
        {
            var query = new QueryParser(this);
            if (!string.IsNullOrEmpty(query[WebColumns.PartId]))
            {
                query.Redirect(CentralPages.WebPartHome);
            }
            else
            {
                query.Redirect(CentralPages.WebParts);
            }
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            QueryParser query = new QueryParser(this);

            int partId = query.GetId(WebColumns.PartId);
            WPart part = null;
            if (partId > 0)
            {
                part = WPart.Get(partId);
            }

            if (part == null)
            {
                part = new WPart();
            }

            part.Name = txtName.Text.Trim();
            part.Identity = txtIdentity.Text.Trim();
            part.Active = chkActive.Checked ? 1 : 0;
            part.Update();
            // Update / Save here

            query.Set(WebColumns.PartId, part.Id);
            query.Redirect(CentralPages.WebPartHome);
        }
    }
}