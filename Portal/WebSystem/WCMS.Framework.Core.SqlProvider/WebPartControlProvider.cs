using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebPartControlProvider : IWebPartControlProvider
    {
        public WebPartControlProvider() { }

        public WebPartControl Get(int partControlId)
        {
            using (var r = SqlHelper.ExecuteReader("WebPartControl_Get",
                new SqlParameter("@PartControlId", partControlId)))
            {
                if (r.HasRows && r.Read())
                    return (WebPartControl)r;
            }

            return null;
        }

        public WebPartControl Get(int partId, string identity)
        {
            using (var r = SqlHelper.ExecuteReader("WebPartControl_Get",
                new SqlParameter("@PartId", partId),
                new SqlParameter("@Identity", identity)))
            {
                if (r.HasRows && r.Read())
                    return (WebPartControl)r;
            }

            return null;
        }

        public IEnumerable<WebPartControl> GetList(int partId)
        {
            List<WebPartControl> items = new List<WebPartControl>();

            using (var r = SqlHelper.ExecuteReader("WebPartControl_Get",
                new SqlParameter("@PartId", partId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add((WebPartControl)r);
            }

            return items;
        }

        public IEnumerable<WebPartControl> GetListByParentId(int parentId)
        {
            List<WebPartControl> items = new List<WebPartControl>();

            using (var r = SqlHelper.ExecuteReader("WebPartControl_Get",
                new SqlParameter("@ParentId", parentId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add((WebPartControl)r);
            }

            return items;
        }

        #region IWebPartControlDAL Members

        public int Update(WebPartControl item)
        {
            object o = SqlHelper.ExecuteScalar("WebPartControl_Set",
                new SqlParameter("@PartControlId", item.Id),
                new SqlParameter("@PartId", item.PartId),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Identity", item.Identity),
                new SqlParameter("@ConfigFileName", item.ConfigFileName),
                new SqlParameter("@PartAdminId", item.PartAdminId),
                new SqlParameter("@EntryPoint", item.EntryPoint),
                new SqlParameter("@ParentId", item.ParentId)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        public bool Delete(int partControlId)
        {
            SqlHelper.ExecuteNonQuery("WebPartControl_Del",
                new SqlParameter("@PartControlId", partControlId));

            return true;
        }

        #endregion

        #region IDataProvider<WebPartControl> Members

        public WebPartControl Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebPartControl> GetList()
        {
            List<WebPartControl> items = new List<WebPartControl>();
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPartControl_Get"))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add((WebPartControl)r);
            }

            return items;
        }

        public IEnumerable<WebPartControl> GetList(params QueryFilterElement[] filters)
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


        public WebPartControl Refresh(WebPartControl item)
        {
            throw new NotImplementedException();
        }
    }
}
