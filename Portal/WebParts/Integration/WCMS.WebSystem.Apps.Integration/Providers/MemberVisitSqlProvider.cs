using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;



namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public class MemberVisitSqlProvider : IMemberVisitProvider
    {
        #region IODKVisitProvider Members

        public IEnumerable<MemberVisit> GetList(int groupId)
        {
            List<MemberVisit> items = new List<MemberVisit>();

            using (var r = SqlHelper.ExecuteReader("ODKVisit_Get",
                new SqlParameter("@GroupId", groupId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<MemberVisit> GetList(int groupId = -1, int userId = -2, string tag = null)
        {
            if (groupId <= 0 && groupId != -1)
                groupId = -1;

            if (userId <= 0 && userId != -2)
                userId = -2;

            if (tag == string.Empty)
                tag = null;

            List<MemberVisit> items = new List<MemberVisit>();

            using (var r = SqlHelper.ExecuteReader("ODKVisit_Get",
                new SqlParameter("@GroupId", groupId),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@Tag", tag)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<MemberVisit> GetListByUserId(int userId = -2)
        {
            List<MemberVisit> items = new List<MemberVisit>();

            using (var r = SqlHelper.ExecuteReader("ODKVisit_Get",
                new SqlParameter("@UserId", userId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        private MemberVisit From(SqlDataReader r)
        {
            MemberVisit item = new MemberVisit();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.CreatedUserId = DataUtil.GetId(r, "CreatedUserId");
            item.DateCreated = DataUtil.GetDateTime(r, "DateCreated");
            item.ActualReport = DataHelper.Get(r, "ActualReport");
            item.Status = DataHelper.Get(r, "Status");
            item.GroupId = DataUtil.GetId(r, WebColumns.GroupId);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.VisitedUserId = DataUtil.GetId(r, "VisitedUserId");
            item.DateVisited = DataUtil.GetDateTime(r, "DateVisited");
            item.ActionTaken = DataHelper.Get(r, "ActionTaken");
            item.ContactNo = DataHelper.Get(r, "ContactNo");
            item.TimesVisited = DataUtil.GetInt32(r, "TimesVisited");
            item.Address = DataHelper.Get(r, "Address");
            item.MembershipDate = DataUtil.GetDateTime(r, "MembershipDate");
            item.Tags = DataHelper.Get(r, "Tags");

            return item;
        }

        #endregion

        #region IDataProvider<ODKVisit> Members

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("ODKVisit_Del",
                new SqlParameter("@Id", id));

            return true;
        }

        public MemberVisit Get(int id)
        {
            using (var r = SqlHelper.ExecuteReader("ODKVisit_Get",
                new SqlParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public MemberVisit Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MemberVisit> GetList()
        {
            List<MemberVisit> items = new List<MemberVisit>();

            using (var r = SqlHelper.ExecuteReader("ODKVisit_Get"))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<MemberVisit> GetListByTag(string tag)
        {
            List<MemberVisit> items = new List<MemberVisit>();

            using (var r = SqlHelper.ExecuteReader("ODKVisit_Get",
                new SqlParameter("@Tag", tag)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<MemberVisit> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(MemberVisit item)
        {
            var obj = SqlHelper.ExecuteScalar("ODKVisit_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@CreatedUserId", item.CreatedUserId),
                new SqlParameter("@DateCreated", item.DateCreated),
                new SqlParameter("@ActualReport", item.ActualReport),
                new SqlParameter("@Status", item.Status),
                new SqlParameter("@GroupId", item.GroupId),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@VisitedUserId", item.VisitedUserId),
                new SqlParameter("@DateVisited", item.DateVisited),
                new SqlParameter("@ActionTaken", item.ActionTaken),
                new SqlParameter("@ContactNo", item.ContactNo),
                new SqlParameter("@TimesVisited", item.TimesVisited),
                new SqlParameter("@Address", item.Address),
                new SqlParameter("@MembershipDate", item.MembershipDate),
                new SqlParameter("@Tags", item.Tags)
            );

            item.Id = DataUtil.GetId(obj);

            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public MemberVisit Refresh(MemberVisit item)
        {
            throw new NotImplementedException();
        }
    }
}
