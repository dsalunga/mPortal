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
    public class WebTemplatePanelProvider : IWebTemplatePanelProvider
    {
        public WebTemplatePanelProvider() { }

        public IEnumerable<WebTemplatePanel> GetList()
        {
            List<WebTemplatePanel> items = new List<WebTemplatePanel>();

            using (DbDataReader r = DbHelper.ExecuteReader("WebTemplatePanel_Get"))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebTemplatePanel> GetList(int objectId=-2, int recordId=-2)
        {
            List<WebTemplatePanel> items = new List<WebTemplatePanel>();

            using (DbDataReader r = DbHelper.ExecuteReader("WebTemplatePanel_Get",
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public WebTemplatePanel Get(int templatePanelId)
        {
            using (DbDataReader r = DbHelper.ExecuteReader("WebTemplatePanel_Get",
                DbHelper.CreateParameter("@TemplatePanelId", templatePanelId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public int Update(WebTemplatePanel item)
        {
            object o = DbHelper.ExecuteScalar("WebTemplatePanel_Set",
                DbHelper.CreateParameter("@TemplatePanelId", item.Id),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                DbHelper.CreateParameter("@RecordId", item.RecordId),
                DbHelper.CreateParameter("@PanelName", item.PanelName),
                DbHelper.CreateParameter("@Rank", item.Rank)
            );

            item.Id = DataUtil.GetId(o.ToString());

            return item.Id;
        }

        public bool Delete(int templatePanelId)
        {
            DbHelper.ExecuteNonQuery("WebTemplatePanel_Del",
                DbHelper.CreateParameter("@TemplatePanelId", templatePanelId));

            return true;
        }


        public WebTemplatePanel From(DbDataReader r)
        {
            WebTemplatePanel item = new WebTemplatePanel();
            item.Id = DataUtil.GetId(r, "TemplatePanelId");
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.PanelName = r["PanelName"].ToString();
            item.Rank = DataUtil.GetInt32(r, WebColumns.Rank);

            return item;
        }

        #region IDataProvider<WebTemplatePanel> Members


        public WebTemplatePanel Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebTemplatePanel> GetList(params QueryFilterElement[] filters)
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


        public WebTemplatePanel Refresh(WebTemplatePanel item)
        {
            throw new NotImplementedException();
        }
    }
}
