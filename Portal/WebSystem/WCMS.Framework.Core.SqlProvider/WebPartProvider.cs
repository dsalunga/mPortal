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
    public class WebPartProvider : IWebPartProvider
    {
        public WebPartProvider()
        {

        }

        public WPart Get(int partId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPart_Get",
                new SqlParameter("@PartId", partId)))
            {
                if (r.HasRows && r.Read())
                    return (WPart)r;
            }

            return null;
        }

        public WPart Get(string identity)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPart_Get",
                new SqlParameter("@Identity", identity)))
            {
                if (r.HasRows && r.Read())
                    return (WPart)r;
            }

            return null;
        }

        public IEnumerable<WPart> GetList()
        {
            List<WPart> items = new List<WPart>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebPart_Get"))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                        items.Add((WPart)r);
                }
            }

            return items;
        }

        public IEnumerable<WPart> GetList(int active = -1)
        {
            List<WPart> items = new List<WPart>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebPart_Get",
                new SqlParameter("@Active", active)))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                        items.Add((WPart)r);
                }
            }

            return items;
        }

        #region IWebPartDAL Members


        public int Update(WPart item)
        {
            object o = SqlHelper.ExecuteScalar("WebPart_Set",
                new SqlParameter("@PartId", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Identity", item.Identity),
                new SqlParameter("@Active", item.Active)
                );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        public bool Delete(int partId)
        {
            SqlHelper.ExecuteNonQuery("WebPart_Del",
                new SqlParameter("@PartId", partId));

            return true;
        }

        #endregion

        #region IDataProvider<WebPart> Members


        public WPart Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WPart> GetList(params QueryFilterElement[] filters)
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


        public WPart Refresh(WPart item)
        {
            throw new NotImplementedException();
        }
    }
}
