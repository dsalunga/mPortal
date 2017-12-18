using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebPartPreview : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int id = DataHelper.GetId(Request, WebColumns.PartControlTemplateId);
                if (id > 0)
                {
                    WebPartControlTemplate item = WebPartControlTemplate.Get(id);
                    if (item != null)
                    {
                        lTemplateName.Text = item.Name;
                        imgPreview.ImageUrl = "~/Content/Assets/Images/PartThumb.jpg";
                    }
                }
            }
        }
    }
}