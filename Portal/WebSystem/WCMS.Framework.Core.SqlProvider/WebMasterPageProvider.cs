using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    class WebMasterPageProvider : IWebMasterPageProvider
    {
        public WebMasterPageProvider() { }

        public IEnumerable<WebMasterPage> GetList(int siteId)
        {
            var items = new List<WebMasterPage>();

            using (var r = DbHelper.ExecuteReader("WebMasterPage_Get",
                DbHelper.CreateParameter("@SiteId", siteId)))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public WebMasterPage Get(int masterPageId)
        {
            using (var r = DbHelper.ExecuteReader("WebMasterPage_Get",
                DbHelper.CreateParameter("@MasterPageId", masterPageId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public bool Delete(int masterPageId)
        {
            DbHelper.ExecuteNonQuery("WebMasterPage_Del",
                DbHelper.CreateParameter("@MasterPageId", masterPageId));

            return true;
        }

        public int Update(WebMasterPage item)
        {
            object o = DbHelper.ExecuteScalar("WebMasterPage_Set",
                DbHelper.CreateParameter("@MasterPageId", item.Id),
                DbHelper.CreateParameter("@SiteId", item.SiteId),
                DbHelper.CreateParameter("@TemplateId", item.TemplateId),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@PublicAccess", item.PublicAccess),
                DbHelper.CreateParameter("@OwnerPageId", item.OwnerPageId),
                DbHelper.CreateParameter("@ManagementAccess", item.ManagementAccess),
                DbHelper.CreateParameter("@SkinId", item.SkinId),
                DbHelper.CreateParameter("@ThemeId", item.ThemeId),
                DbHelper.CreateParameter("@ParentId", item.ParentId)
            );

            if (o != null)
            {
                item.Id = DataUtil.GetId(o.ToString());
                return item.Id;
            }

            return -1;
        }

        public WebMasterPage From(DbDataReader r)
        {
            var item = new WebMasterPage();
            item.Id = DataUtil.GetId(r["MasterPageId"].ToString());
            item.SiteId = DataUtil.GetId(r[WebColumns.SiteId].ToString());
            item.TemplateId = DataUtil.GetId(r["TemplateId"].ToString());
            item.Name = r["Name"].ToString();
            item.PublicAccess = DataUtil.GetInt32(r["PublicAccess"]);
            item.OwnerPageId = DataUtil.GetInt32(r["OwnerPageId"]);
            item.ManagementAccess = DataUtil.GetInt32(r, "ManagementAccess");
            item.SkinId = DataUtil.GetId(r, WebColumns.SkinId);
            item.ThemeId = DataUtil.GetId(r, WebColumns.ThemeId);
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);

            return item;
        }

        #region IDataProvider<WebMasterPage> Members


        public WebMasterPage Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebMasterPage> GetList()
        {
            List<WebMasterPage> items = new List<WebMasterPage>();

            using (var r = DbHelper.ExecuteReader("WebMasterPage_Get"))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebMasterPage> GetList(params QueryFilterElement[] filters)
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


        public WebMasterPage Refresh(WebMasterPage item)
        {
            throw new NotImplementedException();
        }
    }
}
