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
    public class WebPageElementProvider : IWebPageElementProvider
    {
        public WebPageElementProvider() { }

        /// <summary>
        /// GetByParentObjectId
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public IEnumerable<WebPageElement> GetList(int recordId, int objectId)
        {
            List<WebPageElement> items = new List<WebPageElement>();
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPageElement_Get",
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@ObjectId", objectId)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        /// <summary>
        /// GetByPanelId
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="objectId"></param>
        /// <param name="templatePanelId"></param>
        /// <returns></returns>
        public IEnumerable<WebPageElement> GetList(int recordId, int objectId, int templatePanelId)
        {
            List<WebPageElement> items = new List<WebPageElement>();
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPageElement_Get",
                new SqlParameter("@TemplatePanelId", templatePanelId),
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@ObjectId", objectId)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public WebPageElement Get(int id)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebPageElement_Get",
                new SqlParameter("@PageElementId", id)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public int GetCount(int recordId, int objectId, int templatePanelId)
        {
            object o = SqlHelper.ExecuteScalar("WebPageElement_GetCount",
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@TemplatePanelId", templatePanelId));

            return DataHelper.GetId(o);
        }

        public int GetMaxRank(int recordId, int objectId, int templatePanelId)
        {
            object o = SqlHelper.ExecuteScalar("WebPageElement_GetMaxRank",
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@TemplatePanelId", templatePanelId));

            return DataHelper.GetId(o);
        }

        public int Update(WebPageElement item)
        {
            // Validation goes here

            object o = SqlHelper.ExecuteScalar("WebPageElement_Set",
                new SqlParameter("@PageElementId", item.Id),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@TemplatePanelId", item.TemplatePanelId),
                new SqlParameter("@Rank", item.Rank),
                new SqlParameter("@PartControlTemplateId", item.PartControlTemplateId),
                new SqlParameter("@Active", item.Active),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@UsePartTemplatePath", item.UsePartTemplatePath),
                new SqlParameter("@PublicAccess", item.PublicAccess),
                new SqlParameter("@ManagementAccess", item.ManagementAccess)
            );

            if (o != null)
                item.Id = DataHelper.GetId(o.ToString());

            return item.Id;
        }

        public bool Delete(int PageElementId)
        {
            SqlHelper.ExecuteNonQuery("WebPageElement_Del",
                new SqlParameter("@PageElementId", PageElementId));

            return true;
        }

        public WebPageElement From(DbDataReader r)
        {
            WebPageElement item = new WebPageElement();
            item.Id = DataHelper.GetId(r["PageElementId"].ToString());
            item.RecordId = DataHelper.GetId(r["RecordId"].ToString());
            item.Name = r["Name"].ToString();
            item.TemplatePanelId = DataHelper.GetId(r["TemplatePanelId"].ToString());
            item.Rank = Convert.ToInt32(r["Rank"].ToString());
            item.Active = Convert.ToInt32(r["Active"].ToString());
            item.PartControlTemplateId = DataHelper.GetId(r["PartControlTemplateId"].ToString());
            item.ObjectId = DataHelper.GetId(r["ObjectId"]);
            item.UsePartTemplatePath = DataHelper.GetInt32(r["UsePartTemplatePath"]);
            item.PublicAccess = DataHelper.GetId(r["PublicAccess"]);
            item.ManagementAccess = DataHelper.GetInt32(r, "ManagementAccess");

            return item;
        }

        #region IDataProvider<WebPageElement> Members

        public WebPageElement Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebPageElement> GetList()
        {
            List<WebPageElement> items = new List<WebPageElement>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebPageElement_Get"))
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

        public IEnumerable<WebPageElement> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            object o = SqlHelper.ExecuteScalar("WebPageElement_GetCount");
            return DataHelper.GetId(o);
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WebPageElement Refresh(WebPageElement item)
        {
            throw new NotImplementedException();
        }
    }
}
