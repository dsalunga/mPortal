using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebSiteIdentityProvider : IWebSiteIdentityProvider
    {
        #region IDataProvider<WebSiteIdentity> Members

        public bool Delete(int id)
        {
            DbHelper.ExecuteNonQuery("WebSiteIdentity_Del",
                DbHelper.CreateParameter("@Id", id));

            return true;
        }

        public WebSiteIdentity Get(int id)
        {
            using (var r = DbHelper.ExecuteReader("WebSiteIdentity_Get",
                DbHelper.CreateParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private WebSiteIdentity From(DbDataReader r)
        {
            var item = new WebSiteIdentity();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.SiteId = DataUtil.GetId(r, WebColumns.SiteId);
            item.HostName = DataUtil.Get(r, "HostName");
            item.UrlPath = DataUtil.Get(r, "UrlPath");
            item.Port = DataUtil.GetInt32(r, "Port");
            item.IPAddress = DataUtil.Get(r, "IPAddress");
            item.RedirectUrl = DataUtil.Get(r, "RedirectUrl");
            item.ProtocolId = DataUtil.GetInt32(r, "ProtocolId");

            return item;
        }

        public WebSiteIdentity Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebSiteIdentity> GetList()
        {
            var items = new List<WebSiteIdentity>();
            using (var r = DbHelper.ExecuteReader("WebSiteIdentity_Get"))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebSiteIdentity> GetList(int siteId)
        {
            var items = new List<WebSiteIdentity>();
            using (var r = DbHelper.ExecuteReader("WebSiteIdentity_Get",
                DbHelper.CreateParameter("@SiteId", siteId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebSiteIdentity> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(WebSiteIdentity item)
        {
            var obj = DbHelper.ExecuteScalar("WebSiteIdentity_Set",
                DbHelper.CreateParameter("@Id", item.Id),
                DbHelper.CreateParameter("@SiteId", item.SiteId),
                DbHelper.CreateParameter("@HostName", item.HostName),
                DbHelper.CreateParameter("@UrlPath", item.UrlPath),
                DbHelper.CreateParameter("@Port", item.Port),
                DbHelper.CreateParameter("@IPAddress", item.IPAddress),
                DbHelper.CreateParameter("@RedirectUrl", item.RedirectUrl),
                DbHelper.CreateParameter("@ProtocolId", item.ProtocolId)
            );

            item.Id = DataUtil.GetId(obj);

            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WebSiteIdentity Refresh(WebSiteIdentity item)
        {
            throw new NotImplementedException();
        }
    }
}
