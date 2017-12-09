using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core.Interfaces;

namespace WCMS.Framework.Data.SQLServerProvider
{
    public class WebMasterPageItemProvider : IWebMasterPageItemProvider
    {
        public WebMasterPageItemProvider() { }

        public List<WebPageElement> GetByMasterPageId(int masterPageId)
        {
            List<WebPageElement> items = new List<WebPageElement>();
            using (DbDataReader r = SqlHelper.ExecuteReader("WebMasterPageItems_Get",
                new SqlParameter("@MasterPageId", masterPageId)
                ))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        items.Add(this.From(r));
                    }
                }
            }

            return items;
        }

        public List<WebPageElement> GetByPanelId(int masterPageId, int templatePanelId)
        {
            List<WebPageElement> items = new List<WebPageElement>();
            using (DbDataReader r = SqlHelper.ExecuteReader("WebMasterPageItems_Get",
                new SqlParameter("@TemplatePanelId", templatePanelId),
                new SqlParameter("@MasterPageId", masterPageId)
                ))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        items.Add(this.From(r));
                    }
                }
            }

            return items;
        }

        public WebPageElement Get(int masterPageItemId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebMasterPageItems_Get",
                new SqlParameter("@MasterPageItemId", masterPageItemId)))
            {
                if (r.HasRows && r.Read())
                {
                    return this.From(r);
                }
            }

            return null;
        }

        public List<WebPageElement> GetList(int masterPageId)
        {
            List<WebPageElement> items = new List<WebPageElement>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebMasterPageItems_Get",
                new SqlParameter("@MasterPageId", masterPageId)))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        items.Add(this.From(r));
                    }
                }
            }

            return items;
        }

        public int GetCount(int masterPageId, int templatePanelId)
        {
            object o = SqlHelper.ExecuteScalar("WebMasterPageItems_GetCount",
                new SqlParameter("@MasterPageId", masterPageId),
                new SqlParameter("@TemplatePanelId", templatePanelId));

            if (o != null)
            {
                return Convert.ToInt32(o.ToString());
            }

            return 0;
        }

        public int Update(WebPageElement item)
        {
            // Validation goes here

            object o = SqlHelper.ExecuteScalar("WebMasterPageItems_Set",
                new SqlParameter("@MasterPageItemId", item.Id),
                new SqlParameter("@MasterPageId", item.MasterPageId),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@TemplatePanelId", item.TemplatePanelId),
                new SqlParameter("@Rank", item.Rank),
                new SqlParameter("@PartControlTemplateId", item.PartControlTemplateId),
                new SqlParameter("@Active", item.Active)
                );

            if (o != null)
            {
                return DataHelper.GetDbId(o.ToString());
            }

            return -1;
        }

        public bool Delete(int masterPageItemId)
        {
            SqlHelper.ExecuteNonQuery("WebMasterPageItems_Del",
                new SqlParameter("@MasterPageItemId", masterPageItemId)
                );

            return true;
        }

        public WebPageElement From(DbDataReader r)
        {
            WebPageElement item = new WebPageElement();
            item.Id = DataHelper.GetDbId(r["MasterPageItemId"].ToString());
            item.MasterPageId = DataHelper.GetDbId(r["MasterPageId"].ToString());
            item.Name = r["Name"].ToString();
            item.TemplatePanelId = DataHelper.GetDbId(r["TemplatePanelId"].ToString());
            item.Rank = Convert.ToInt32(r["Rank"].ToString());
            item.Active = Convert.ToInt32(r["Active"].ToString());
            item.PartControlTemplateId = DataHelper.GetDbId(r["PartControlTemplateId"].ToString());

            return item;
        }

        public WebPageElement From(DataRow r)
        {
            WebPageElement item = new WebPageElement();
            item.Id = DataHelper.GetDbId(r["MasterPageItemId"].ToString());
            item.MasterPageId = DataHelper.GetDbId(r["MasterPageId"].ToString());
            item.Name = r["Name"].ToString();
            item.TemplatePanelId = DataHelper.GetDbId(r["TemplatePanelId"].ToString());
            item.Rank = Convert.ToInt32(r["Rank"].ToString());
            item.Active = Convert.ToInt32(r["Active"].ToString());
            item.PartControlTemplateId = DataHelper.GetDbId(r["PartControlTemplateId"].ToString());

            return item;
        }
    }
}
