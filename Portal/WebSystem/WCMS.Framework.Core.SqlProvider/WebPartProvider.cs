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
    public class WebPartProvider : IWebPartProvider
    {
        public WebPartProvider()
        {

        }

        public WPart Get(int partId)
        {
            using (DbDataReader r = DbHelper.ExecuteReader("WebPart_Get",
                DbHelper.CreateParameter("@PartId", partId)))
            {
                if (r.HasRows && r.Read())
                    return (WPart)r;
            }

            return null;
        }

        public WPart Get(string identity)
        {
            using (DbDataReader r = DbHelper.ExecuteReader("WebPart_Get",
                DbHelper.CreateParameter("@Identity", identity)))
            {
                if (r.HasRows && r.Read())
                    return (WPart)r;
            }

            return null;
        }

        public IEnumerable<WPart> GetList()
        {
            List<WPart> items = new List<WPart>();

            using (DbDataReader r = DbHelper.ExecuteReader("WebPart_Get"))
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

            using (DbDataReader r = DbHelper.ExecuteReader("WebPart_Get",
                DbHelper.CreateParameter("@Active", active)))
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
            object o = DbHelper.ExecuteScalar("WebPart_Set",
                DbHelper.CreateParameter("@PartId", item.Id),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@Identity", item.Identity),
                DbHelper.CreateParameter("@Active", item.Active)
                );

            item.Id = DataUtil.GetId(o);
            return item.Id;
        }

        public bool Delete(int partId)
        {
            DbHelper.ExecuteNonQuery("WebPart_Del",
                DbHelper.CreateParameter("@PartId", partId));

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
