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
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Central.Misc
{
    public partial class WebParameterSets : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboSites.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());

                var siteId = DataHelper.GetId(Request, WebColumns.SiteId);
                if (siteId > 0)
                {
                    WebHelper.SetCboValue(cboSites, siteId);
                    cboSites.Visible = false;
                    //ObjectDataSource1.SelectParameters["siteId"].DefaultValue = siteId.ToString();
                }

                //GridView1.DataBind();
            }
        }

        public DataSet Select()
        {
            var query = new WQuery(true);
            //query.BasePath = CentralPages.WebParameterSetHome;
            query.Set(ObjectKey.KeySource, query.EncodedBasePath);
            query.BasePath = CentralPages.WebParameters;

            return DataHelper.ToDataSet(
                from i in WebParameterSet.Provider.GetList()
                select new
                {
                    i.Id,
                    i.Name,
                    NameUrl = query.Set(WebColumns.ParameterSetId, i.Id)
                    .Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebParameterSet, i.Id))
                    .ToString()
                });
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var context = new WContext(this);
            int id = DataHelper.GetId(e.CommandArgument);

            context.Set(WebColumns.ParameterSetId, id);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    context.Redirect(CentralPages.WebParameterSetHome);
                    break;

                case "View_Parameters":
                    var q = new WQuery(context.Query);
                    q.Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebParameterSet, id));
                    q.Set(ObjectKey.KeySource, context.Query.EncodedBasePath);

                    q.Redirect(CentralPages.WebParameters);
                    break;

                case "Custom_Delete":
                    var parmSet = WebParameterSet.Provider.Get(id);
                    if (parmSet != null)
                    {
                        parmSet.Delete();
                        GridView1.DataBind();
                    }
                    break;
            }
        }

        protected void cmdNew_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            context.Redirect(CentralPages.WebParameterSet);
        }

        protected void cboSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridView1.DataBind();
        }
    }
}