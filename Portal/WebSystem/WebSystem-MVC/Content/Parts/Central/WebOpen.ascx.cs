using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Framework;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebOpen : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdOpen_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);

            int objectId = query.GetId(WebColumns.ObjectId);
            int id = DataHelper.GetId(txtId.Text.Trim());
            if (id > 0)
            {
                switch (objectId)
                {
                    case WebObjects.WebTextResource:
                        {
                            string keyString = query[ObjectKey.KeyString];

                            int siteId = query.GetId(WebColumns.SiteId);
                            int pageId = query.GetId(WebColumns.PageId);
                            int masterPageId = query.GetId(WebColumns.MasterPageId);

                            int templateId = query.GetId(WebColumns.TemplateId);
                            int partId = query.GetId(WebColumns.PartId);
                            int partAdminId = query.GetId(WebColumns.PartAdminId);
                            int partControldId = query.GetId(WebColumns.PartControlId);

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
                                    target = WPage.Get(pageId);
                                }
                                else if (masterPageId > 0)
                                {
                                    target = WebMasterPage.Get(masterPageId);
                                }
                                else
                                {
                                    target = WSite.Get(siteId);
                                }

                                if (pageId > 0)
                                {
                                    // Web Page
                                    target = WPage.Get(pageId);
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
                                    target = WPart.Get(partId);
                                }
                                else if (siteId > 0)
                                {
                                    // Web Site
                                    target = WSite.Get(siteId);
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
            QueryParser query = new QueryParser(this);
            query.Remove("ObjectId");
            query.Redirect(CentralPages.WebResources);
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Return();
        }
    }
}