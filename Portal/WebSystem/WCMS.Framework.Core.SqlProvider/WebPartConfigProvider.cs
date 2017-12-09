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
    public class WebPartConfigProvider : IWebPartConfigProvider
    {
        public WebPartConfigProvider() { }

        public IEnumerable<WebPartConfig> GetList()
        {
            List<WebPartConfig> items = new List<WebPartConfig>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebPartConfig_Get"))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public WebPartConfig Get(int partConfigId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPartConfig_Get",
                new SqlParameter("@PartConfigId", partConfigId)
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

            using (DbDataReader r = SqlHelper.ExecuteReader("WebPartConfig_Get",
                new SqlParameter("@PartId", partId)
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
            item.Id = DataHelper.GetId(r["PartConfigId"]);
            item.PartId = DataHelper.GetId(r["PartId"]);
            item.Name = r["Name"].ToString();
            item.FileName = r["FileName"].ToString();

            return item;
        }

        #region IWebPartConfigDAL Members


        public int Update(WebPartConfig item)
        {
            object o = SqlHelper.ExecuteScalar("WebPartConfig_Set",
                new SqlParameter("@PartConfigId", item.Id),
                new SqlParameter("@PartId", item.PartId),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@FileName", item.FileName)
                );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        public bool Delete(int partConfigId)
        {
            SqlHelper.ExecuteNonQuery("WebPartConfig_Del",
                new SqlParameter("@PartConfigId", partConfigId));

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
