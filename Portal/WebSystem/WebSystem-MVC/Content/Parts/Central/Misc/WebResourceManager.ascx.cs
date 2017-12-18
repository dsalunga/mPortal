using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebResourceManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboContentTypes.DataSource = WebTextResource.GetContentTypes();
                cboContentTypes.DataBind();

                var query = new QueryParser(this);
                var contentType = query.GetInt32("ContentType");

                ListItem item = null;
                if (contentType > 0 && (item = cboContentTypes.Items.FindByValue(contentType.ToString())) != null)
                    cboContentTypes.SelectedValue = contentType.ToString();

                GridView1.DataBind();



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

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            context.SetSourceAndRedirect(CentralPages.WebResource);
        }

        public DataSet Select(int contentTypeId = -2)
        {
            var query = new QueryParser(true);
            var resources = WebTextResource.Provider.GetList(contentTypeId).OrderBy(r => r.Rank);

            query.SetReturn();
            
            var items = from item in resources
                        select new
                        {
                            item.Id,
                            item.Title,
                            item.Rank,
                            item.DateModified,
                            TitleUrl = query.Set(WebColumns.TextResourceId, item.Id).BuildQuery(CentralPages.WebResource),
                            Content = DataHelper.GetStringPreview(item.Content, 30),
                            ContentType = item.ContentType.Value
                        };

            return DataHelper.ToDataSet(items);
        }

        //protected void cmdOpen_Click(object sender, EventArgs e)
        //{
        //    QueryParser qs = new QueryParser(this);
        //    qs[WebColumns.ObjectId] = WebObjects.WebTextResource.ToString();
        //    qs.Redirect("~/Central/WebOpen.aspx");
        //}

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var query = new WContext(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.TextResourceId, e.CommandArgument);
                    query.SetSourceAndRedirect(CentralPages.WebResource);
                    break;

                case "Custom_Delete":
                    {
                        int resourceId = DataHelper.GetId(e.CommandArgument);
                        WebTextResource.Delete(resourceId);

                        GridView1.DataBind();
                        break;
                    }
            }
        }

        protected void cmdSynch_Click(object sender, EventArgs e)
        {
            var resources = WebTextResource.GetList();
            int persistedCount = 0;
            int synched = 0;

            for (int i = 0; i < resources.Count(); i++)
            {
                var resource = resources.ElementAt(i);
                var filePath = resource.BuildAbsolutePhysicalPath();
                var lastWriteUtc = File.GetLastWriteTimeUtc(filePath);
                var resLastWrite = resource.DatePersisted;

                lastWriteUtc = new DateTime(lastWriteUtc.Year, lastWriteUtc.Month, lastWriteUtc.Day, lastWriteUtc.Hour, lastWriteUtc.Minute, lastWriteUtc.Second);
                resLastWrite = new DateTime(resLastWrite.Year, resLastWrite.Month, resLastWrite.Day, resLastWrite.Hour, resLastWrite.Minute, resLastWrite.Second);

                if (lastWriteUtc < resLastWrite)
                {
                    // DB is newer, so persist the resource
                    using (var writer = new StreamWriter(filePath))
                        writer.Write(resource.Content);

                    resource.DatePersisted = File.GetLastWriteTimeUtc(filePath);
                    resource.Update();

                    persistedCount++;
                }
                else if (lastWriteUtc > resLastWrite)
                {
                    // File is newer, so update the DB

                    string fullContent = null;

                    using (var reader = new StreamReader(filePath))
                    {
                        fullContent = reader.ReadToEnd();
                    }

                    resource.DatePersisted = lastWriteUtc;
                    resource.DateModified = DateTime.Now;
                    resource.Content = fullContent;
                    resource.Update();

                    synched++;
                }
            }

            lblStatus.InnerHtml += string.Format("<br />Persisted {0} files.", persistedCount);
            lblStatus.InnerHtml += string.Format("<br />Synched {0} files.", synched);
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

        protected void cboSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridView1.DataBind();
        }
    }
}