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
    public class WebTemplateProvider : IWebTemplateProvider
    {
        public WebTemplateProvider() { }

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public WebTemplate Get(int templateId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebTemplate_Get",
                new SqlParameter("@Id", templateId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public WebTemplate From(DbDataReader r)
        {
            WebTemplate item = new WebTemplate();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.FileName = r["FileName"].ToString();
            item.Identity = r["Identity"].ToString();
            item.PrimaryPanelId = DataHelper.GetId(r["PrimaryPanelId"].ToString());
            item.DateModified = (DateTime)r["DateModified"];
            item.SkinId = DataHelper.GetId(r, WebColumns.SkinId);
            item.Content = DataHelper.Get(r, WebColumns.Content);
            item.Standalone = DataHelper.GetInt32(r, "Standalone");
            item.ParentId = DataHelper.GetId(r, WebColumns.ParentId);
            item.ThemeId = DataHelper.GetId(r, WebColumns.ThemeId);
            item.TemplateEngineId = DataHelper.GetInt32(r, "TemplateEngineId");

            return item;
        }

        public IEnumerable<WebTemplate> GetList()
        {
            List<WebTemplate> items = new List<WebTemplate>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebTemplate_Get"))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebTemplate> GetList(int themeId = -2)
        {
            var items = new List<WebTemplate>();

            using (var r = SqlHelper.ExecuteReader("WebTemplate_Get",
                new SqlParameter("@ThemeId", themeId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public bool Delete(int templateId)
        {
            SqlHelper.ExecuteNonQuery("WebTemplate_Del",
                new SqlParameter("@Id", templateId));

            return true;
        }

        public int Update(WebTemplate item)
        {
            object o = SqlHelper.ExecuteScalar("WebTemplate_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Content", item.Content),
                new SqlParameter("@FileName", item.FileName),
                new SqlParameter("@Identity", item.Identity),
                new SqlParameter("@PrimaryPanelId", item.PrimaryPanelId),
                new SqlParameter("@DateModified", item.DateModified),
                new SqlParameter("@SkinId", item.SkinId),
                new SqlParameter("@Standalone", item.Standalone),
                new SqlParameter("@ParentId", item.ParentId),
                new SqlParameter("@ThemeId", item.ThemeId),
                new SqlParameter("@TemplateEngineId", item.TemplateEngineId)
            );

            item.Id = DataHelper.GetId(o.ToString());

            return item.Id;
        }

        #region IDataProvider<WebTemplate> Members


        public WebTemplate Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebTemplate> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int UpdateAllFromCache()
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion


        public WebTemplate Refresh(WebTemplate item)
        {
            throw new NotImplementedException();
        }
    }
}
