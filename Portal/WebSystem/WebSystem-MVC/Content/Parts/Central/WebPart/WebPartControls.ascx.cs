using System;
using System.Data;
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
    public partial class WebPartControls : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdAdd_Click(object sender, System.EventArgs e)
        {
            var query = new WQuery(this);
            query.Redirect(CentralPages.WebPartControl);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int partControlId = DataHelper.GetId(e.CommandArgument);
            var query = new WQuery(this);
            query.Set(WebColumns.PartControlId, partControlId);

            switch (e.CommandName)
            {
                //case "edit_item":
                //    query.Redirect(CentralPages.WebPartControlHome);
                //    break;

                case "Templates":
                    query.Redirect(CentralPages.WebPartControlTemplates);
                    break;

                case "Custom_Delete":
                    WebPartControl.Delete(partControlId);
                    GridView1.DataBind();
                    break;
            }
        }

        public DataSet Select(int partId)
        {
            if (partId > 0)
            {
                WebPartAdmin admin = null;
                var debugUrl = WebRegistry.SelectNodeValue("/System/Debugging/DebugPageUrl");
                var query = new WQuery(string.IsNullOrEmpty(debugUrl) ? "/" : debugUrl);
                var queryName = new WQuery(true);
                queryName.BasePath = CentralPages.WebPartControlHome;

                return DataHelper.ToDataSet(
                    from i in WebPartControl.GetList(partId)
                    select new
                    {
                        i.Id,
                        i.Name,
                        i.Identity,
                        i.ConfigFileName,
                        i.EntryPoint,
                        PartAdmin = (admin = i.PartAdminId > 0 ? i.PartAdmin : null) != null ? admin.Name : string.Empty,
                        QuickPreviewUrl = BuildDebugUrl(i, query),
                        NameUrl = queryName.Set(WebColumns.PartControlId, i.Id).BuildQuery()
                    }
                );
            }

            return null;
        }

        private string BuildDebugUrl(WebPartControl control, QueryParser query)
        {
            var template = control.Templates.FirstOrDefault();
            return query.Set(WConstants.DebugTemplateId, template == null ? -1 : template.Id).BuildQuery();
        }
    }
}