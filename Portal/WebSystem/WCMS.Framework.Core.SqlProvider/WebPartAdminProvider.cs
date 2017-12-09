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
    public class WebPartAdminProvider : IWebPartAdminProvider
    {
        public WebPartAdminProvider() { }

        public IEnumerable<WebPartAdmin> GetList()
        {
            var items = new List<WebPartAdmin>();
            using (var r = SqlHelper.ExecuteReader("WebPartAdmin_Get"))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }
            return items;
        }

        public IEnumerable<WebPartAdmin> GetList(int partId)
        {
            var items = new List<WebPartAdmin>();
            using (var r = SqlHelper.ExecuteReader("WebPartAdmin_Get",
                new SqlParameter("@PartId", partId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }
            return items;
        }

        public IEnumerable<WebPartAdmin> GetList(int partId, int parentId)
        {
            var items = new List<WebPartAdmin>();
            using (var r = SqlHelper.ExecuteReader("WebPartAdmin_Get",
                new SqlParameter("@PartId", partId),
                new SqlParameter("@ParentId", parentId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }
            return items;
        }

        public WebPartAdmin Get(int partAdminId)
        {
            using (var r = SqlHelper.ExecuteReader("WebPartAdmin_Get",
                new SqlParameter("@PartAdminId", partAdminId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }
            return null;
        }

        public WebPartAdmin Get(int partId, string name)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPartAdmin_Get",
                new SqlParameter("@PartId", partId),
                new SqlParameter("@Name", name)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }
            return null;
        }

        private WebPartAdmin From(DbDataReader r)
        {
            var item = new WebPartAdmin();
            item.Id = DataHelper.GetId(r["PartAdminId"]);
            item.PartId = DataHelper.GetId(r["PartId"]);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.FileName = r["FileName"].ToString();
            item.ParentId = DataHelper.GetId(r, WebColumns.ParentId);
            item.Active = DataHelper.GetInt32(r, WebColumns.Active);
            item.Visible = DataHelper.GetInt32(r, WebColumns.Visible);
            item.InSiteContext = DataHelper.GetInt32(r, "InSiteContext");
            item.TemplateEngineId = DataHelper.GetInt32(r, "TemplateEngineId");
            item.AutoTitle = DataHelper.GetInt32(r, "AutoTitle");

            return item;
        }

        #region IWebPartAdminDAL Members

        public bool Delete(int partAdminId)
        {
            SqlHelper.ExecuteNonQuery("WebPartAdmin_Del",
                new SqlParameter("@PartAdminId", partAdminId)
            );

            return true;
        }

        public int Update(WebPartAdmin item)
        {
            object o = SqlHelper.ExecuteScalar("WebPartAdmin_Set",
                new SqlParameter("@PartAdminId", item.Id),
                new SqlParameter("@PartId", item.PartId),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@FileName", item.FileName),
                new SqlParameter("@ParentId", item.ParentId),
                new SqlParameter("@Active", item.Active),
                new SqlParameter("@Visible", item.Visible),
                new SqlParameter("@InSiteContext", item.InSiteContext),
                new SqlParameter("@TemplateEngineId", item.TemplateEngineId),
                new SqlParameter("@AutoTitle", item.AutoTitle)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        #endregion

        #region IDataProvider<WebPartAdmin> Members

        public WebPartAdmin Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebPartAdmin> GetList(params QueryFilterElement[] filters)
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


        public WebPartAdmin Refresh(WebPartAdmin item)
        {
            throw new NotImplementedException();
        }
    }
}
