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
    public class WebPartControlProvider : IWebPartControlProvider
    {
        public WebPartControlProvider() { }

        public WebPartControl Get(int partControlId)
        {
            using (var r = DbHelper.ExecuteReader("WebPartControl_Get",
                DbHelper.CreateParameter("@PartControlId", partControlId)))
            {
                if (r.HasRows && r.Read())
                    return (WebPartControl)r;
            }

            return null;
        }

        public WebPartControl Get(int partId, string identity)
        {
            using (var r = DbHelper.ExecuteReader("WebPartControl_Get",
                DbHelper.CreateParameter("@PartId", partId),
                DbHelper.CreateParameter("@Identity", identity)))
            {
                if (r.HasRows && r.Read())
                    return (WebPartControl)r;
            }

            return null;
        }

        public IEnumerable<WebPartControl> GetList(int partId)
        {
            List<WebPartControl> items = new List<WebPartControl>();

            using (var r = DbHelper.ExecuteReader("WebPartControl_Get",
                DbHelper.CreateParameter("@PartId", partId)))
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

            using (var r = DbHelper.ExecuteReader("WebPartControl_Get",
                DbHelper.CreateParameter("@ParentId", parentId)))
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
            object o = DbHelper.ExecuteScalar("WebPartControl_Set",
                DbHelper.CreateParameter("@PartControlId", item.Id),
                DbHelper.CreateParameter("@PartId", item.PartId),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@Identity", item.Identity),
                DbHelper.CreateParameter("@ConfigFileName", item.ConfigFileName),
                DbHelper.CreateParameter("@PartAdminId", item.PartAdminId),
                DbHelper.CreateParameter("@EntryPoint", item.EntryPoint),
                DbHelper.CreateParameter("@ParentId", item.ParentId)
            );

            item.Id = DataUtil.GetId(o);
            return item.Id;
        }

        public bool Delete(int partControlId)
        {
            DbHelper.ExecuteNonQuery("WebPartControl_Del",
                DbHelper.CreateParameter("@PartControlId", partControlId));

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
            using (DbDataReader r = DbHelper.ExecuteReader("WebPartControl_Get"))
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
