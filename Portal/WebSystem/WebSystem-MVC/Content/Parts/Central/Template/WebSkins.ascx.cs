using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

using WCMS.Common.Utilities;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central.Template
{
    public partial class WebSkins : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var templateId = DataHelper.GetId(Request, WebColumns.TemplateId);
                if (templateId > 0)
                    WebTemplateHome1.Visible = true;
                else
                    WebThemeHome1.Visible = true;
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            context.Redirect(CentralPages.WebSkin);
        }

        protected void cmdDelete_Click(object sender, System.EventArgs e)
        {
            var sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(sChecked);
                foreach (var id in ids)
                    WebSkin.Provider.Delete(id);

                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var context = new WContext(this);
            var themeId = e.CommandArgument.ToString();

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    context.Set(WebColumns.SkinId, themeId);
                    context.Redirect(CentralPages.WebSkinHome);
                    break;
            }
        }

        public DataSet Select(int templateId)
        {
            return DataHelper.ToDataSet(WebSkin.Provider.GetList(WebObjects.WebTemplate, templateId));
        }
    }
}