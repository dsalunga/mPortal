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
    public class WebTemplateProvider : IWebTemplateProvider
    {
        public WebTemplateProvider() { }

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public WebTemplate Get(int templateId)
        {
            using (DbDataReader r = DbHelper.ExecuteReader("WebTemplate_Get",
                DbHelper.CreateParameter("@Id", templateId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public WebTemplate From(DbDataReader r)
        {
            WebTemplate item = new WebTemplate();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.FileName = r["FileName"].ToString();
            item.Identity = r["Identity"].ToString();
            item.PrimaryPanelId = DataUtil.GetId(r["PrimaryPanelId"].ToString());
            item.DateModified = (DateTime)r["DateModified"];
            item.SkinId = DataUtil.GetId(r, WebColumns.SkinId);
            item.Content = DataUtil.Get(r, WebColumns.Content);
            item.Standalone = DataUtil.GetInt32(r, "Standalone");
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);
            item.ThemeId = DataUtil.GetId(r, WebColumns.ThemeId);
            item.TemplateEngineId = DataUtil.GetInt32(r, "TemplateEngineId");

            return item;
        }

        public IEnumerable<WebTemplate> GetList()
        {
            List<WebTemplate> items = new List<WebTemplate>();

            using (DbDataReader r = DbHelper.ExecuteReader("WebTemplate_Get"))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebTemplate> GetList(int themeId = -2)
        {
            var items = new List<WebTemplate>();

            using (var r = DbHelper.ExecuteReader("WebTemplate_Get",
                DbHelper.CreateParameter("@ThemeId", themeId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public bool Delete(int templateId)
        {
            DbHelper.ExecuteNonQuery("WebTemplate_Del",
                DbHelper.CreateParameter("@Id", templateId));

            return true;
        }

        public int Update(WebTemplate item)
        {
            object o = DbHelper.ExecuteScalar("WebTemplate_Set",
                DbHelper.CreateParameter("@Id", item.Id),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@Content", item.Content),
                DbHelper.CreateParameter("@FileName", item.FileName),
                DbHelper.CreateParameter("@Identity", item.Identity),
                DbHelper.CreateParameter("@PrimaryPanelId", item.PrimaryPanelId),
                DbHelper.CreateParameter("@DateModified", item.DateModified),
                DbHelper.CreateParameter("@SkinId", item.SkinId),
                DbHelper.CreateParameter("@Standalone", item.Standalone),
                DbHelper.CreateParameter("@ParentId", item.ParentId),
                DbHelper.CreateParameter("@ThemeId", item.ThemeId),
                DbHelper.CreateParameter("@TemplateEngineId", item.TemplateEngineId)
            );

            item.Id = DataUtil.GetId(o.ToString());

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
