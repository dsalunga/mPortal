using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    class WebPagePanelProvider : IWebPagePanelProvider
    {
        public WebPagePanel Get(int pagePanelId)
        {
            using (DbDataReader r = DbHelper.ExecuteReader("WebPagePanel_Get",
                DbHelper.CreateParameter("@PagePanelId", pagePanelId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebPagePanel> GetList(int pageId)
        {
            List<WebPagePanel> items = new List<WebPagePanel>();

            using (DbDataReader r = DbHelper.ExecuteReader("WebPagePanel_Get",
                DbHelper.CreateParameter("@PageId", pageId)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        private WebPagePanel From(DbDataReader r)
        {
            WebPagePanel item = new WebPagePanel();
            item.Id = DataUtil.GetId(r["PagePanelId"]);
            item.TemplatePanelId = DataUtil.GetId(r["TemplatePanelId"]);
            item.PageId = DataUtil.GetId(r["PageId"]);
            item.UsageTypeId = Convert.ToInt32(r["UsageTypeId"].ToString());

            return item;
        }

        public WebPagePanel Get(int templatePanelId, int pageId)
        {
            using (DbDataReader r = DbHelper.ExecuteReader("WebPagePanel_Get",
                DbHelper.CreateParameter("@PageId", pageId),
                DbHelper.CreateParameter("@TemplatePanelId", templatePanelId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public int Update(WebPagePanel item)
        {
            object o = DbHelper.ExecuteScalar("WebPagePanel_Set",
                DbHelper.CreateParameter("@PagePanelId", item.Id),
                DbHelper.CreateParameter("@TemplatePanelId", item.TemplatePanelId),
                DbHelper.CreateParameter("@PageId", item.PageId),
                DbHelper.CreateParameter("@UsageTypeId", item.UsageTypeId)
            );

            item.Id = DataUtil.GetId(o);
            return item.Id;
        }

        public bool Delete(int pagePanelId)
        {
            DbHelper.ExecuteNonQuery("WebPagePanel_Del",
                DbHelper.CreateParameter("@PagePanelId", pagePanelId));

            return true;
        }

        #region IDataProvider<WebPagePanel> Members


        public WebPagePanel Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebPagePanel> GetList()
        {
            List<WebPagePanel> items = new List<WebPagePanel>();

            using (DbDataReader r = DbHelper.ExecuteReader("WebPagePanel_Get"))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        items.Add(this.From(r));
                    }
                }
            }

            return items;
        }

        public IEnumerable<WebPagePanel> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WebPagePanel Refresh(WebPagePanel item)
        {
            throw new NotImplementedException();
        }
    }
}
