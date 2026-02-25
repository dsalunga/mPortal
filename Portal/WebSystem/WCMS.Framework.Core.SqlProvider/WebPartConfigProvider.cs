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
    public class WebPartConfigProvider : IWebPartConfigProvider
    {
        public WebPartConfigProvider() { }

        public IEnumerable<WebPartConfig> GetList()
        {
            List<WebPartConfig> items = new List<WebPartConfig>();

            using (DbDataReader r = DbHelper.ExecuteReader("WebPartConfig_Get"))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public WebPartConfig Get(int partConfigId)
        {
            using (DbDataReader r = DbHelper.ExecuteReader("WebPartConfig_Get",
                DbHelper.CreateParameter("@PartConfigId", partConfigId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebPartConfig> GetList(int partId)
        {
            List<WebPartConfig> items = new List<WebPartConfig>();

            using (DbDataReader r = DbHelper.ExecuteReader("WebPartConfig_Get",
                DbHelper.CreateParameter("@PartId", partId)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        private WebPartConfig From(DbDataReader r)
        {
            WebPartConfig item = new WebPartConfig();
            item.Id = DataUtil.GetId(r["PartConfigId"]);
            item.PartId = DataUtil.GetId(r["PartId"]);
            item.Name = r["Name"].ToString();
            item.FileName = r["FileName"].ToString();

            return item;
        }

        #region IWebPartConfigDAL Members


        public int Update(WebPartConfig item)
        {
            object o = DbHelper.ExecuteScalar("WebPartConfig_Set",
                DbHelper.CreateParameter("@PartConfigId", item.Id),
                DbHelper.CreateParameter("@PartId", item.PartId),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@FileName", item.FileName)
                );

            item.Id = DataUtil.GetId(o);
            return item.Id;
        }

        public bool Delete(int partConfigId)
        {
            DbHelper.ExecuteNonQuery("WebPartConfig_Del",
                DbHelper.CreateParameter("@PartConfigId", partConfigId));

            return true;
        }

        #endregion

        #region IDataProvider<WebPartConfig> Members


        public WebPartConfig Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebPartConfig> GetList(params QueryFilterElement[] filters)
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


        public WebPartConfig Refresh(WebPartConfig item)
        {
            throw new NotImplementedException();
        }
    }
}
