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
    public partial class WebResources : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cboContentTypes.DataSource = WebTextResource.GetContentTypes();
                cboContentTypes.DataBind();

                var query = new QueryParser(this);
                var contentType = query.GetInt32("ContentType");

                ListItem item = null;
                if (contentType > 0 && (item = cboContentTypes.Items.FindByValue(contentType.ToString())) != null)
                    cboContentTypes.SelectedValue = contentType.ToString();

                GridView1.DataBind();
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            WContext.StaticRedirect(CentralPages.WebResource, true);
        }

        public DataSet Select(int contentTypeId = -2)
        {
            var query = new WQuery(true);
            string keyString = query.Get(ObjectKey.KeyString);

            IEnumerable<WebTextResource> resources = null;
            if (!string.IsNullOrEmpty(keyString))
            {
                var key = new ObjectKey(keyString);
                resources = from h in WebObjectHeader.GetList(key.ObjectId, key.RecordId)
                            select h.Header;
            }
            else
            {
                throw new Exception("KeyString not found!");
            }

            resources = resources.OrderBy(r => r.Rank);

            var items = from item in resources
                        where contentTypeId == -2 || item.ContentTypeId == contentTypeId
                        select new
                        {
                            item.Id,
                            item.Title,
                            item.Rank,
                            item.DateModified,
                            TitleUrl = query.Set(WebColumns.TextResourceId, item.Id).BuildQuery(CentralPages.WebResource),
                            Content = DataHelper.GetStringPreview(item.Content, WConstants.PreviewChars)
                        };

            return DataHelper.ToDataSet(items);
        }

        protected void cmdOpen_Click(object sender, EventArgs e)
        {
            var query = new QueryParser(this);
            query.Set(WebColumns.ObjectId, WebObjects.WebTextResource);
            query.Redirect(CentralPages.WebOpen);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var query = new WContext(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.TextResourceId, e.CommandArgument as string);
                    query.Redirect(CentralPages.WebResource);

                    break;

                case "Custom_Delete":
                    string keyString = query.Get(ObjectKey.KeyString);
                    if (!string.IsNullOrEmpty(keyString))
                    {
                        int resourceId = DataHelper.GetId(e.CommandArgument);
                        var key = new ObjectKey(keyString);

                        var objectHeader = WebObjectHeader.Get(key.ObjectId, key.RecordId, resourceId);
                        if (objectHeader != null)
                        {
                            objectHeader.Delete();
                            GridView1.DataBind();
                        }
                    }
                    else
                    {
                        throw new Exception("No given key");
                    }

                    break;
            }
        }

        protected void cboContentTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var contentType = DataHelper.GetInt32(cboContentTypes.SelectedValue);
            var query = new QueryParser(this);

            if (contentType > 0)
                query.Set("ContentType", cboContentTypes.SelectedValue);
            else
                query.Remove("ContentType");

            query.Redirect();
        }
    }
}