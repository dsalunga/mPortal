using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Admin
{
    public partial class WebOpen : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdOpen_Click(object sender, EventArgs e)
        {
            QueryParser qs = new QueryParser(this);

            int objectId = qs.GetDbId(WebColumns.ObjectId);
            int id = DataHelper.GetDbId(txtId.Text.Trim());
            if (id > 0)
            {
                switch (objectId)
                {
                    case WebObjects.WebTextResource:
                        {
                            string keyString = qs[ObjectKey.KeyString];

                            int siteId = qs.GetDbId(WebColumns.SiteId);
                            int pageId = qs.GetDbId(WebColumns.PageId);
                            int masterPageId = qs.GetDbId(WebColumns.MasterPageId);

                            int templateId = qs.GetDbId(WebColumns.TemplateId);
                            int partId = qs.GetDbId(WebColumns.PartId);
                            int partAdminId = qs.GetDbId(WebColumns.PartAdminId);
                            int partControldId = qs.GetDbId(WebColumns.PartControlId);

                            if (!string.IsNullOrEmpty(keyString))
                            {
                                ObjectKey key = new ObjectKey(keyString);

                                // If not yet existing then add to Site
                                WebObjectHeader.AddHeader(key.ObjectId, key.RecordId, id);
                            }
                            else
                            {
                                IWebHeaderTarget target = null; //WebSite.Get(siteId);
                                if (pageId > 0)
                                {
                                    target = WebPage.Get(pageId);
                                }
                                else if (masterPageId > 0)
                                {
                                    target = WebMasterPage.Get(masterPageId);
                                }
                                else
                                {
                                    target = WebSite.Get(siteId);
                                }

                                if (pageId > 0)
                                {
                                    // Web Page
                                    target = WebPage.Get(pageId);
                                }
                                else if (masterPageId > 0)
                                {
                                    // Master Page
                                    target = WebMasterPage.Get(masterPageId);
                                }
                                else if (templateId > 0)
                                {
                                    target = WebTemplate.Get(templateId);
                                }
                                else if (partControldId > 0)
                                {
                                    target = WebPartControl.Get(partControldId);
                                }
                                else if (partAdminId > 0)
                                {
                                    target = WebPartAdmin.Get(partAdminId);
                                }
                                else if (partId > 0)
                                {
                                    target = WebPart.Get(partId);
                                }
                                else if (siteId > 0)
                                {
                                    // Web Site
                                    target = WebSite.Get(siteId);
                                }
                                else
                                {
                                    throw new Exception("No given key");
                                }

                                WebTextResource resource = WebTextResource.Get(id);
                                target.AddHeader(resource);
                            }



                            this.Return();
                            break;
                        }
                }
            }
        }

        private void Return()
        {
            QueryParser qs = new QueryParser(this);
            qs.Remove("ObjectId");
            qs.Redirect("~/Central/WebResources.aspx");
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Return();
        }
    }
}
