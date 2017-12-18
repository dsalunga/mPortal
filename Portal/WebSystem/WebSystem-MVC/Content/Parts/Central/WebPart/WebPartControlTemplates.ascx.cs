using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebPartControlTemplatesController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack) { }
        }

        protected void cmdAdd_Click(object sender, System.EventArgs e)
        {
            var query = new QueryParser(this);
            query.Remove(WebColumns.PartControlTemplateId);
            query.Redirect(CentralPages.WebPartControlTemplate);
        }

        //protected void cmdDone_Click(object sender, System.EventArgs e)
        //{
        //    QueryParser qs = new QueryParser(this);
        //    qs.Redirect("~/Central/WebPartControlHome.aspx");
        //}

        private void DuplicateTemplate(int iCSItemTemplateID)
        {
            //int iCommonSectionID = Convert.ToInt32(Request.QueryString["CommonSectionID"]);
            //CMSDALTableAdapters.CommonSectionsAdapter adapterCS = new CMSDALTableAdapters.CommonSectionsAdapter();
            //string sIdentity = adapterCS.GetIdentity(iCommonSectionID).ToString();

            //CMSDALTableAdapters.CSItemTemplatesAdapter adapter = new CMSDALTableAdapters.CSItemTemplatesAdapter();
            //CMSDAL.CSItemTemplatesDataTable table = adapter.GetData(null, iCSItemTemplateID);

            //if (table.Rows.Count > 0)
            //{
            //    string sControlURL = Path.GetFileNameWithoutExtension(table[0].ControlURL) + "_Copy" + Path.GetExtension(table[0].ControlURL);
            //    string sName = table[0].CommonSectionTemplateName + " (COPY)";
            //    string sdescription = table[0].Description;
            //    string sTemplateIdentity = table[0].TemplateIdentity;
            //    bool isHidden = table[0].IsHidden;
            //    int iCSItemID = Convert.ToInt32(Request.QueryString["CSIID"]);

            //    // INSERT NEW TEMPLATE

            //    adapter.Update(null, sName, sControlURL, sTemplateIdentity, sdescription, isHidden, iCommonSectionID, iCSItemID, new Guid(Membership.GetUser().ProviderUserKey.ToString()));
            //}
        }

        public DataSet Select(int partControlId)
        {
            var debugUrl = WebRegistry.SelectNodeValue("/System/Debugging/DebugPageUrl");
            var query = new QueryParser(string.IsNullOrEmpty(debugUrl) ? "/" : debugUrl);

            return DataHelper.ToDataSet(
                from i in WebPartControlTemplate.GetList(partControlId)
                select new
                {
                    i.Id,
                    i.Name,
                    i.FileName,
                    i.Identity,
                    QuickPreviewUrl = query.Set(WConstants.DebugTemplateId, i.Id).BuildQuery(),
                    i.Standalone,
                    TemplateEngine = i.TemplateEngineId == TemplateEngineTypes.ASPX ? "ASPX" : "Razor"
                });
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var query = new QueryParser(this);
            int id = DataHelper.GetId(e.CommandArgument);
            query.Set(WebColumns.PartControlTemplateId, id);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Redirect(CentralPages.WebPartControlTemplateHome);
                    break;

                case "Edit_File":
                    query.Set(WebColumns.TemplateId, id);
                    query.Redirect(CentralPages.TextEditor);
                    break;

                case "Custom_Duplicate":
                    this.DuplicateTemplate(id);
                    GridView1.DataBind();
                    break;

                case "Custom_Delete":
                    WebPartControlTemplate.Delete(id);
                    GridView1.DataBind();
                    break;
            }
        }
    }
}