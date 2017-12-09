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
    public class WebPartControlTemplateProvider : IWebPartControlTemplateProvider
    {
        public WebPartControlTemplateProvider() { }

        public WebPartControlTemplate Get(int partControlTemplateId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPartControlTemplate_Get",
                new SqlParameter("@PartControlTemplateId", partControlTemplateId)))
            {
                if (r.HasRows && r.Read())
                    return From(r);
            }

            return null;
        }

        public WebPartControlTemplate Get(int partControlId, string identity)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPartControlTemplate_Get",
                new SqlParameter("@PartControlId", partControlId),
                new SqlParameter("@Identity", identity)))
            {
                if (r.HasRows && r.Read())
                    return From(r);
            }

            return null;
        }

        public IEnumerable<WebPartControlTemplate> GetList(int partControlId)
        {
            List<WebPartControlTemplate> items = new List<WebPartControlTemplate>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebPartControlTemplate_Get",
                new SqlParameter("@PartControlId", partControlId)))
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
            item.Id = DataHelper.GetId(r, "PartControlTemplateId");
            item.PartControlId = DataHelper.GetId(r, "PartControlId");
            item.Name = r["Name"].ToString();
            item.FileName = r["FileName"].ToString();
            item.Identity = r["Identity"].ToString();
            item.Path = r["Path"].ToString();
            item.Standalone = DataHelper.GetInt32(r, "Standalone");
            item.TemplateEngineId = DataHelper.GetInt32(r, "TemplateEngineId");

            return item;
        }

        public int Update(WebPartControlTemplate item)
        {
            object o = SqlHelper.ExecuteScalar("WebPartControlTemplate_Set",
                new SqlParameter("@PartControlTemplateId", item.Id),
                new SqlParameter("@PartControlId", item.PartControlId),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@FileName", item.FileName),
                new SqlParameter("@Identity", item.Identity),
                new SqlParameter("@Path", item.Path),
                new SqlParameter("@Standalone", item.Standalone),
                new SqlParameter("@TemplateEngineId", item.TemplateEngineId)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        public bool Delete(int partControlTemplateId)
        {
            SqlHelper.ExecuteNonQuery("WebPartControlTemplate_Del",
                new SqlParameter("@PartControlTemplateId", partControlTemplateId)
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

            using (DbDataReader r = SqlHelper.ExecuteReader("WebPartControlTemplate_Get"))
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
