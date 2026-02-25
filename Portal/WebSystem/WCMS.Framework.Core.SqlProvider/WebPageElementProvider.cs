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
            using (DbDataReader r = DbHelper.ExecuteReader("WebPageElement_Get",
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ObjectId", objectId)
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
            using (DbDataReader r = DbHelper.ExecuteReader("WebPageElement_Get",
                DbHelper.CreateParameter("@TemplatePanelId", templatePanelId),
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ObjectId", objectId)
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
            using (DbDataReader r = DbHelper.ExecuteReader("WebPageElement_Get",
                DbHelper.CreateParameter("@PageElementId", id)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public int GetCount(int recordId, int objectId, int templatePanelId)
        {
            object o = DbHelper.ExecuteScalar("WebPageElement_GetCount",
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@TemplatePanelId", templatePanelId));

            return DataUtil.GetId(o);
        }

        public int GetMaxRank(int recordId, int objectId, int templatePanelId)
        {
            object o = DbHelper.ExecuteScalar("WebPageElement_GetMaxRank",
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@TemplatePanelId", templatePanelId));

            return DataUtil.GetId(o);
        }

        public int Update(WebPageElement item)
        {
            // Validation goes here

            object o = DbHelper.ExecuteScalar("WebPageElement_Set",
                DbHelper.CreateParameter("@PageElementId", item.Id),
                DbHelper.CreateParameter("@RecordId", item.RecordId),
                DbHelper.CreateParameter("@Name", item.Name),
                DbHelper.CreateParameter("@TemplatePanelId", item.TemplatePanelId),
                DbHelper.CreateParameter("@Rank", item.Rank),
                DbHelper.CreateParameter("@PartControlTemplateId", item.PartControlTemplateId),
                DbHelper.CreateParameter("@Active", item.Active),
                DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                DbHelper.CreateParameter("@UsePartTemplatePath", item.UsePartTemplatePath),
                DbHelper.CreateParameter("@PublicAccess", item.PublicAccess),
                DbHelper.CreateParameter("@ManagementAccess", item.ManagementAccess)
            );

            if (o != null)
                item.Id = DataUtil.GetId(o.ToString());

            return item.Id;
        }

        public bool Delete(int PageElementId)
        {
            DbHelper.ExecuteNonQuery("WebPageElement_Del",
                DbHelper.CreateParameter("@PageElementId", PageElementId));

            return true;
        }

        public WebPageElement From(DbDataReader r)
        {
            WebPageElement item = new WebPageElement();
            item.Id = DataUtil.GetId(r["PageElementId"].ToString());
            item.RecordId = DataUtil.GetId(r["RecordId"].ToString());
            item.Name = r["Name"].ToString();
            item.TemplatePanelId = DataUtil.GetId(r["TemplatePanelId"].ToString());
            item.Rank = Convert.ToInt32(r["Rank"].ToString());
            item.Active = Convert.ToInt32(r["Active"].ToString());
            item.PartControlTemplateId = DataUtil.GetId(r["PartControlTemplateId"].ToString());
            item.ObjectId = DataUtil.GetId(r["ObjectId"]);
            item.UsePartTemplatePath = DataUtil.GetInt32(r["UsePartTemplatePath"]);
            item.PublicAccess = DataUtil.GetId(r["PublicAccess"]);
            item.ManagementAccess = DataUtil.GetInt32(r, "ManagementAccess");

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

            using (DbDataReader r = DbHelper.ExecuteReader("WebPageElement_Get"))
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
            object o = DbHelper.ExecuteScalar("WebPageElement_GetCount");
            return DataUtil.GetId(o);
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
