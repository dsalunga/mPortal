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
    public class WebTemplatePanelProvider : IWebTemplatePanelProvider
    {
        public WebTemplatePanelProvider() { }

        public IEnumerable<WebTemplatePanel> GetList()
        {
            List<WebTemplatePanel> items = new List<WebTemplatePanel>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebTemplatePanel_Get"))
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

            using (DbDataReader r = SqlHelper.ExecuteReader("WebTemplatePanel_Get",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public WebTemplatePanel Get(int templatePanelId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebTemplatePanel_Get",
                new SqlParameter("@TemplatePanelId", templatePanelId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public int Update(WebTemplatePanel item)
        {
            object o = SqlHelper.ExecuteScalar("WebTemplatePanel_Set",
                new SqlParameter("@TemplatePanelId", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@PanelName", item.PanelName),
                new SqlParameter("@Rank", item.Rank)
            );

            item.Id = DataHelper.GetId(o.ToString());

            return item.Id;
        }

        public bool Delete(int templatePanelId)
        {
            SqlHelper.ExecuteNonQuery("WebTemplatePanel_Del",
                new SqlParameter("@TemplatePanelId", templatePanelId));

            return true;
        }


        public WebTemplatePanel From(DbDataReader r)
        {
            WebTemplatePanel item = new WebTemplatePanel();
            item.Id = DataHelper.GetId(r, "TemplatePanelId");
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.PanelName = r["PanelName"].ToString();
            item.Rank = DataHelper.GetInt32(r, WebColumns.Rank);

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
