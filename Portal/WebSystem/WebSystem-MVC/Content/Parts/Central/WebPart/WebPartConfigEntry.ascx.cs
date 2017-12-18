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
    public partial class WebPartConfigEntry : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LoadData();
            }
        }

        private void LoadData()
        {
            int partConfigId = DataHelper.GetId(Request[WebColumns.PartConfigId]);
            WebPartConfig partConfig = null;
            if (partConfigId > 0 && (partConfig = WebPartConfig.Get(partConfigId)) != null)
            {
                txtName.Text = partConfig.Name;
                txtFileName.Text = partConfig.FileName;
            }

            //else
            //{
            //    // CREATE NEW COMMON SECTION PAGE

            //}
        }

        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            Return();
        }

        protected void cmdUpdate_Click(object sender, System.EventArgs e)
        {
            QueryParser qs = new QueryParser(this);

            int partConfigId = DataHelper.GetId(qs[WebColumns.PartConfigId]);
            int partId = qs.GetId(WebColumns.PartId);
            WebPartConfig partConfig = null;

            if (partConfigId > 0 && (partConfig = WebPartConfig.Get(partConfigId)) != null)
            { }
            else
            {
                partConfig = new WebPartConfig();
                partConfig.PartId = partId;
            }

            partConfig.Name = txtName.Text.Trim();
            partConfig.FileName = txtFileName.Text.Trim();
            partConfig.Update();

            Return();
        }

        private void Return()
        {
            QueryParser query = new QueryParser(this);
            query.Remove(WebColumns.PartConfigId);
            query.Redirect(CentralPages.WebPartConfig);
        }
    }
}