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
    public class WebPartControlTemplateProvider : IWebPartControlTemplateProvider
    {
        public WebPartControlTemplateProvider() { }

        public WebPartControlTemplate Get(int partControlTemplateId)
        {
            using (DbDataReader r = DbHelper.ExecuteReader("WebPartControlTemplate_Get",
                DbHelper.CreateParameter("@PartControlTemplateId", partControlTemplateId)))
            {
                if (r.HasRows && r.Read())
                    return From(r);
            }

            return null;
        }

        public WebPartControlTemplate Get(int partControlId, string identity)
        {
            using (DbDataReader r = DbHelper.ExecuteReader("WebPartControlTemplate_Get",
                DbHelper.CreateParameter("@PartControlId", partControlId),
                DbHelper.CreateParameter("@Identity", identity)))
            {
                if (r.HasRows && r.Read())
                    return From(r);
            }

            return null;
        }

        public IEnumerable<WebPartControlTemplate> GetList(int partControlId)
        {
            List<WebPartControlTemplate> items = new List<WebPartControlTemplate>();

            using (DbDataReader r = DbHelper.ExecuteReader("WebPartControlTemplate_Get",
                DbHelper.CreateParameter("@PartControlId", partControlId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(From(r));
            }

            return items;
        }

        public static WebPartControlTemplate From(DbDataReader r)
        {
            WebPartControlTemplate item = new WebPartControlTemplate();
            item.Id = DataUtil.GetId(r, "PartControlTemplateId");
            item.PartControlId = DataUtil.GetId(r, "PartControlId");
            item.Name = r["Name"].ToString();
            item.FileName = r["FileName"].ToString();
            item.Identity = r["Identity"].ToString();
            item.Path = r["Path"].ToString();
            item.Standalone = DataUtil.GetInt32(r, "Standalone");
            item.TemplateEngineId = DataUtil.GetInt32(r, "TemplateEngineId");

            return item;
        }

        public int Update(WebPartControlTemplate item)
        {
            object o = DbHelper.ExecuteScalar("WebPartControlTemplate_Set",
                DbHelper.CreateParameter("@PartControlTemplateId", item.Id),
                DbHelper.CreateParameter("@PartControlId", item.PartControlId),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@FileName", item.FileName),
                DbHelper.CreateParameter("@Identity", item.Identity),
                DbHelper.CreateParameter("@Path", item.Path),
                DbHelper.CreateParameter("@Standalone", item.Standalone),
                DbHelper.CreateParameter("@TemplateEngineId", item.TemplateEngineId)
            );

            item.Id = DataUtil.GetId(o);
            return item.Id;
        }

        public bool Delete(int partControlTemplateId)
        {
            DbHelper.ExecuteNonQuery("WebPartControlTemplate_Del",
                DbHelper.CreateParameter("@PartControlTemplateId", partControlTemplateId)
            );

            return true;
        }

        public WebPartControlTemplate Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebPartControlTemplate> GetList()
        {
            List<WebPartControlTemplate> items = new List<WebPartControlTemplate>();

            using (DbDataReader r = DbHelper.ExecuteReader("WebPartControlTemplate_Get"))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        items.Add(From(r));
                    }
                }
            }

            return items;
        }

        public IEnumerable<WebPartControlTemplate> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public WebPartControlTemplate Refresh(WebPartControlTemplate item)
        {
            throw new NotImplementedException();
        }
    }
}
