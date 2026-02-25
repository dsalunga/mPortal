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
    public class WebPartAdminProvider : IWebPartAdminProvider
    {
        public WebPartAdminProvider() { }

        public IEnumerable<WebPartAdmin> GetList()
        {
            var items = new List<WebPartAdmin>();
            using (var r = DbHelper.ExecuteReader("WebPartAdmin_Get"))
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
            using (var r = DbHelper.ExecuteReader("WebPartAdmin_Get",
                DbHelper.CreateParameter("@PartId", partId)))
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
            using (var r = DbHelper.ExecuteReader("WebPartAdmin_Get",
                DbHelper.CreateParameter("@PartId", partId),
                DbHelper.CreateParameter("@ParentId", parentId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }
            return items;
        }

        public WebPartAdmin Get(int partAdminId)
        {
            using (var r = DbHelper.ExecuteReader("WebPartAdmin_Get",
                DbHelper.CreateParameter("@PartAdminId", partAdminId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }
            return null;
        }

        public WebPartAdmin Get(int partId, string name)
        {
            using (DbDataReader r = DbHelper.ExecuteReader("WebPartAdmin_Get",
                DbHelper.CreateParameter("@PartId", partId),
                DbHelper.CreateParameter("@Name", name)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }
            return null;
        }

        private WebPartAdmin From(DbDataReader r)
        {
            var item = new WebPartAdmin();
            item.Id = DataUtil.GetId(r["PartAdminId"]);
            item.PartId = DataUtil.GetId(r["PartId"]);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.FileName = r["FileName"].ToString();
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);
            item.Active = DataUtil.GetInt32(r, WebColumns.Active);
            item.Visible = DataUtil.GetInt32(r, WebColumns.Visible);
            item.InSiteContext = DataUtil.GetInt32(r, "InSiteContext");
            item.TemplateEngineId = DataUtil.GetInt32(r, "TemplateEngineId");
            item.AutoTitle = DataUtil.GetInt32(r, "AutoTitle");

            return item;
        }

        #region IWebPartAdminDAL Members

        public bool Delete(int partAdminId)
        {
            DbHelper.ExecuteNonQuery("WebPartAdmin_Del",
                DbHelper.CreateParameter("@PartAdminId", partAdminId)
            );

            return true;
        }

        public int Update(WebPartAdmin item)
        {
            object o = DbHelper.ExecuteScalar("WebPartAdmin_Set",
                DbHelper.CreateParameter("@PartAdminId", item.Id),
                DbHelper.CreateParameter("@PartId", item.PartId),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@FileName", item.FileName),
                DbHelper.CreateParameter("@ParentId", item.ParentId),
                DbHelper.CreateParameter("@Active", item.Active),
                DbHelper.CreateParameter("@Visible", item.Visible),
                DbHelper.CreateParameter("@InSiteContext", item.InSiteContext),
                DbHelper.CreateParameter("@TemplateEngineId", item.TemplateEngineId),
                DbHelper.CreateParameter("@AutoTitle", item.AutoTitle)
            );

            item.Id = DataUtil.GetId(o);
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
