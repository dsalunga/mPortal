using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebParametersController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Action accessDenied = () =>
                {
                    panelMain.Visible = false;
                    panelControlBox.Visible = false;
                    lblStatus.Text = "Access denied.";
                };

                var key = WHelper.GetObjectKey();
                var record = key.Record as PublicSecurableObject;

                if (record != null)
                    record.ExecuteUserMgmtActions(accessDenied, accessDenied);

                var query = new QueryParser(this);
                var source = query.Get(ObjectKey.KeySource);
                cmdDone.Visible = !string.IsNullOrEmpty(source);
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            var query = new QueryParser(this);
            query.Redirect(CentralPages.WebParameter);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var query = new QueryParser(this);
            int id = DataHelper.GetId(e.CommandArgument);
            query.Set(WebColumns.ParameterId, id);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Redirect(CentralPages.WebParameter);
                    break;

                case "Custom_Delete":
                    WebParameter.Delete(id);
                    GridView1.DataBind();
                    break;
            }
        }

        public DataSet Select(int objectId, int recordId, string key)
        {
            var query = new QueryParser(HttpContext.Current);
            IEnumerable<WebParameter> items = null;
            if (!string.IsNullOrEmpty(key))
            {
                var objectKey = new ObjectKey(key);
                items = WebParameter.GetList(objectKey.ObjectId, objectKey.RecordId);
            }
            else
            {
                items = WebParameter.GetList(objectId, recordId);
            }

            return DataHelper.ToDataSet(from item in items
                                        orderby item.Name
                                        select new
                                        {
                                            item.Id,
                                            item.Name,
                                            Value = DataHelper.GetStringPreview(item.Value, WConstants.PreviewChars),
                                            NameUrl = query.Set(WebColumns.ParameterId, item.Id).BuildQuery(CentralPages.WebParameter)
                                        }
                        );
        }

        protected void cmdDone_Click(object sender, EventArgs e)
        {
            var query = new QueryParser(this);
            var source = query.GetDecode(ObjectKey.KeySource);

            query.Remove(ObjectKey.KeyString);
            query.Remove(ObjectKey.KeySource);

            query.Redirect(source);
            //lblStatus.Text = source;
        }
    }
}