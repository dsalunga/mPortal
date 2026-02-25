using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebUserGroupProvider : IWebUserGroupProvider
    {
        public WebUserGroup Get(int userRoleId)
        {
            using (var r = DbHelper.ExecuteReader("WebUserGroup_Get",
                DbHelper.CreateParameter("@Id", userRoleId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebUserGroup Get(int roleId, int id, bool isGroup = false)
        {
            using (var r = DbHelper.ExecuteReader("WebUserGroup_Get",
                DbHelper.CreateParameter("@RecordId", id),
                DbHelper.CreateParameter("@ObjectId", isGroup ? WebObjects.WebGroup : WebObjects.WebUser),
                DbHelper.CreateParameter("@GroupId", roleId)
                ))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebUserGroup> GetList()
        {
            var items = new List<WebUserGroup>();
            using (var r = DbHelper.ExecuteReader("WebUserGroup_Get"))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                        items.Add(this.From(r));
                }
            }

            return items;
        }

        public IEnumerable<WebUserGroup> GetList(int active)
        {
            var items = new List<WebUserGroup>();
            using (var r = DbHelper.ExecuteReader("WebUserGroup_Get",
                DbHelper.CreateParameter("@Active", active)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebUserGroup> GetByUserId(int userId, int active)
        {
            var items = new List<WebUserGroup>();
            using (var r = DbHelper.ExecuteReader("WebUserGroup_Get",
                DbHelper.CreateParameter("@RecordId", userId),
                DbHelper.CreateParameter("@ObjectId", WebObjects.WebUser),
                DbHelper.CreateParameter("@Active", active)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebUserGroup> GetByGroupId(int groupId, int active)
        {
            var items = new List<WebUserGroup>();
            using (var r = DbHelper.ExecuteReader("WebUserGroup_Get",
                DbHelper.CreateParameter("@GroupId", groupId),
                DbHelper.CreateParameter("@Active", active)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebUserGroup> GetByCreatedById(int groupId, int createdById, int active)
        {
            var items = new List<WebUserGroup>();
            using (var r = DbHelper.ExecuteReader("WebUserGroup_Get",
                DbHelper.CreateParameter("@GroupId", groupId),
                DbHelper.CreateParameter("@Active", active),
                DbHelper.CreateParameter("@CreatedById", createdById)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(WebUserGroup item)
        {
            object o = DbHelper.ExecuteScalar("WebUserGroup_Set",
                DbHelper.CreateParameter("@Id", item.Id),
                DbHelper.CreateParameter("@UserId", -1),
                DbHelper.CreateParameter("@GroupId", item.GroupId),
                DbHelper.CreateParameter("@Active", item.Active),
                DbHelper.CreateParameter("@DateJoined", item.DateJoined),
                DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                DbHelper.CreateParameter("@RecordId", item.RecordId),
                DbHelper.CreateParameter("@Remarks", item.Remarks),
                DbHelper.CreateParameter("@CreatedById", item.CreatedById)
            );

            item.Id = DataUtil.GetId(o);
            return item.Id;
        }

        public bool Delete(int userRoleId)
        {
            DbHelper.ExecuteNonQuery("WebUserGroup_Del",
                DbHelper.CreateParameter("@Id", userRoleId)
            );

            return true;
        }

        public bool Delete(int userId, int groupId)
        {
            DbHelper.ExecuteNonQuery("WebUserGroup_Del",
                DbHelper.CreateParameter("@RecordId", userId),
                DbHelper.CreateParameter("@ObjectId", WebObjects.WebUser),
                DbHelper.CreateParameter("@GroupId", groupId)
            );

            return true;
        }

        public bool Delete(int groupId, int objectId, int recordId)
        {
            DbHelper.ExecuteNonQuery("WebUserGroup_Del",
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@GroupId", groupId)
            );

            return true;
        }

        private WebUserGroup From(DbDataReader r)
        {
            var item = new WebUserGroup();
            item.Id = DataUtil.GetId(r["Id"]);
            item.GroupId = DataUtil.GetId(r["GroupId"]);
            //item.UserId = DataHelper.GetId(r["UserId"]);
            item.Active = DataUtil.GetInt32(r, "Active");
            item.DateJoined = DataUtil.GetDateTime(r, "DateJoined");
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.Remarks = DataUtil.Get(r, "Remarks");
            item.CreatedById = DataUtil.GetId(r, "CreatedById");

            return item;
        }

        #region IDataProvider<WebUserGroup> Members


        public WebUserGroup Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebUserGroup> GetList(params QueryFilterElement[] filters)
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


        public WebUserGroup Refresh(WebUserGroup item)
        {
            throw new NotImplementedException();
        }
    }
}
