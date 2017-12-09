using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebUserGroupProvider : IWebUserGroupProvider
    {
        public WebUserGroup Get(int userRoleId)
        {
            using (var r = SqlHelper.ExecuteReader("WebUserGroup_Get",
                new SqlParameter("@Id", userRoleId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebUserGroup Get(int roleId, int id, bool isGroup = false)
        {
            using (var r = SqlHelper.ExecuteReader("WebUserGroup_Get",
                new SqlParameter("@RecordId", id),
                new SqlParameter("@ObjectId", isGroup ? WebObjects.WebGroup : WebObjects.WebUser),
                new SqlParameter("@GroupId", roleId)
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
            using (var r = SqlHelper.ExecuteReader("WebUserGroup_Get"))
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
            using (var r = SqlHelper.ExecuteReader("WebUserGroup_Get",
                new SqlParameter("@Active", active)))
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
            using (var r = SqlHelper.ExecuteReader("WebUserGroup_Get",
                new SqlParameter("@RecordId", userId),
                new SqlParameter("@ObjectId", WebObjects.WebUser),
                new SqlParameter("@Active", active)
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
            using (var r = SqlHelper.ExecuteReader("WebUserGroup_Get",
                new SqlParameter("@GroupId", groupId),
                new SqlParameter("@Active", active)
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
            using (var r = SqlHelper.ExecuteReader("WebUserGroup_Get",
                new SqlParameter("@GroupId", groupId),
                new SqlParameter("@Active", active),
                new SqlParameter("@CreatedById", createdById)
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
            object o = SqlHelper.ExecuteScalar("WebUserGroup_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@UserId", -1),
                new SqlParameter("@GroupId", item.GroupId),
                new SqlParameter("@Active", item.Active),
                new SqlParameter("@DateJoined", item.DateJoined),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@Remarks", item.Remarks),
                new SqlParameter("@CreatedById", item.CreatedById)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        public bool Delete(int userRoleId)
        {
            SqlHelper.ExecuteNonQuery("WebUserGroup_Del",
                new SqlParameter("@Id", userRoleId)
            );

            return true;
        }

        public bool Delete(int userId, int groupId)
        {
            SqlHelper.ExecuteNonQuery("WebUserGroup_Del",
                new SqlParameter("@RecordId", userId),
                new SqlParameter("@ObjectId", WebObjects.WebUser),
                new SqlParameter("@GroupId", groupId)
            );

            return true;
        }

        public bool Delete(int groupId, int objectId, int recordId)
        {
            SqlHelper.ExecuteNonQuery("WebUserGroup_Del",
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@GroupId", groupId)
            );

            return true;
        }

        private WebUserGroup From(DbDataReader r)
        {
            var item = new WebUserGroup();
            item.Id = DataHelper.GetId(r["Id"]);
            item.GroupId = DataHelper.GetId(r["GroupId"]);
            //item.UserId = DataHelper.GetId(r["UserId"]);
            item.Active = DataHelper.GetInt32(r, "Active");
            item.DateJoined = DataHelper.GetDateTime(r, "DateJoined");
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.Remarks = DataHelper.Get(r, "Remarks");
            item.CreatedById = DataHelper.GetId(r, "CreatedById");

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
