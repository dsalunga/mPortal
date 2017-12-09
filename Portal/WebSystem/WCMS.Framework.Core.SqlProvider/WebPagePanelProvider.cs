using System;
using System.Data;
using System.Data.SqlClient;
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
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPagePanel_Get",
                new SqlParameter("@PagePanelId", pagePanelId)
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

            using (DbDataReader r = SqlHelper.ExecuteReader("WebPagePanel_Get",
                new SqlParameter("@PageId", pageId)
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
            item.Id = DataHelper.GetId(r["PagePanelId"]);
            item.TemplatePanelId = DataHelper.GetId(r["TemplatePanelId"]);
            item.PageId = DataHelper.GetId(r["PageId"]);
            item.UsageTypeId = Convert.ToInt32(r["UsageTypeId"].ToString());

            return item;
        }

        public WebPagePanel Get(int templatePanelId, int pageId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPagePanel_Get",
                new SqlParameter("@PageId", pageId),
                new SqlParameter("@TemplatePanelId", templatePanelId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public int Update(WebPagePanel item)
        {
            object o = SqlHelper.ExecuteScalar("WebPagePanel_Set",
                new SqlParameter("@PagePanelId", item.Id),
                new SqlParameter("@TemplatePanelId", item.TemplatePanelId),
                new SqlParameter("@PageId", item.PageId),
                new SqlParameter("@UsageTypeId", item.UsageTypeId)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        public bool Delete(int pagePanelId)
        {
            SqlHelper.ExecuteNonQuery("WebPagePanel_Del",
                new SqlParameter("@PagePanelId", pagePanelId));

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

            using (DbDataReader r = SqlHelper.ExecuteReader("WebPagePanel_Get"))
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
