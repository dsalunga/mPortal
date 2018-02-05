using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;
using WCMS.WebSystem.Apps.Integration.Providers;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public class ExternalMemberSqlProvider : IMemberProvider
    {
        #region IDataProvider<Member> Members

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("ExternalMember_Del",
                new SqlParameter("@MemberId", id));

            return true;
        }

        public Member Get(int id)
        {
            using (var r = SqlHelper.ExecuteReader("ExternalMember_Get",
                new SqlParameter("@MemberId", id)))
            {
                if (r.Read())
                {
                    return this.From(r);
                }
            }

            return null;
        }

        private Member From(SqlDataReader r)
        {
            Member item = new Member();
            item.MemberID = DataUtil.GetId(r, "MemberID");
            item.ExternalIDNo = r["ExternalIDNo"].ToString();
            item.TemporaryIDNo = r["TemporaryIDNo"].ToString();
            item.FirstName = r["FirstName"].ToString();
            item.MiddleName = r["MiddleName"].ToString();
            item.LastName = r["LastName"].ToString();
            item.BirthDate = DataUtil.GetDateTime(r, "BirthDate");
            item.BirthPlace = r["BirthPlace"].ToString();
            item.Gender = r["Gender"].ToString();
            item.BloodType = r["BloodType"].ToString();
            item.CivilStatusID = DataUtil.GetInt32(r, "CivilStatusID");
            item.CitizenshipID = DataUtil.GetInt32(r, "CitizenshipID");
            item.RaceID = DataUtil.GetInt32(r, "RaceID");
            item.Phone = r["Phone"].ToString();
            item.Mobile = r["Mobile"].ToString();
            item.Email = r["Email"].ToString();
            item.IsActive = DataUtil.GetInt32(r, "IsActive");
            item.Flag = r["Flag"].ToString();
            item.NickName = r["NickName"].ToString();
            item.DateCreated = DataUtil.GetDateTime(r, "DateCreated");
            item.DateUpdated = DataUtil.GetDateTime(r, "DateUpdated");
            //item.MembershipDate = DataHelper.GetDateTime(r, "MembershipDate");

            return item;
        }

        public Member Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> GetList()
        {
            var items = new List<Member>();

            using (var r = SqlHelper.ExecuteReader("ExternalMember_Get"))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<Member> GetList(string keyword)
        {
            var items = new List<Member>();

            using (var r = SqlHelper.ExecuteReader("ExternalMember_Get",
                new SqlParameter("@Keyword", keyword)))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<Member> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public int Update(Member item)
        {
            /*
            if (item.Id > 0)
            {
                // Negate Id for Insert
                var old = Get(item.Id);
                if (old == null)
                    item.Id = item.Id * -1;
            }
            */

            if (0 >= item.MemberID)
                throw new Exception("AMS Member is invalid.");

            var o = SqlHelper.ExecuteReader("ExternalMember_Set",
                new SqlParameter("@MemberId", item.MemberID),
                new SqlParameter("@ExternalIDNo", item.ExternalIDNo),
                new SqlParameter("@TemporaryIDNo", item.TemporaryIDNo),
                new SqlParameter("@FirstName", item.FirstName),
                new SqlParameter("@MiddleName", item.MiddleName),
                new SqlParameter("@LastName", item.LastName),
                new SqlParameter("@Email", item.Email)

                /*
                new SqlParameter("@NickName", item.NickName),
                new SqlParameter("@BirthDate", item.BirthDate),
                new SqlParameter("@BirthPlace", item.BirthPlace),
                new SqlParameter("@Gender", item.Gender),
                new SqlParameter("@BloodType", item.BloodType),
                new SqlParameter("@CivilStatusId", item.CivilStatusID),
                new SqlParameter("@CitizenshipId", item.CitizenshipID),
                new SqlParameter("@RaceId", item.RaceID),
                new SqlParameter("@Phone", item.Phone),
                new SqlParameter("@Mobile", item.Mobile),
                new SqlParameter("@IsActive", item.IsActive),
                new SqlParameter("@Flag", item.Flag),
                new SqlParameter("@NickName", item.NickName),
                new SqlParameter("@DateCreated", item.DateCreated),
                new SqlParameter("@DateUpdated", item.DateUpdated),
                new SqlParameter("@MembershipDate", item.MembershipDate) */
            );

            item.MemberID = DataUtil.GetId(o);
            return (int)item.MemberID;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public Member Refresh(Member item)
        {
            throw new NotImplementedException();
        }
    }
}

