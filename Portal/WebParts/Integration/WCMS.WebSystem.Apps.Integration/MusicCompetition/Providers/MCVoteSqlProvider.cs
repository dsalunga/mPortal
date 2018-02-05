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
    public class MCVoteSqlProvider : GenericSqlDataProviderBase<MCVote>, IMCVoteProvider
    {
        protected override string DeleteProcedure
        {
            get { return "MCVote_Del"; }
        }

        protected override MCVote From(IDataReader r, MCVote source)
        {
            MCVote item = source ?? new MCVote();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Code = DataHelper.Get(r, "Code");
            item.FirstName = DataHelper.Get(r, "FirstName");
            item.LastName = DataHelper.Get(r, "LastName");
            item.MobileNumber = DataHelper.Get(r, "MobileNumber");
            item.Email = DataHelper.Get(r, "Email");
            item.CandidateId = DataUtil.GetId(r, "CandidateId");
            item.DateVoted = DataUtil.GetDateTime(r, "DateVoted");
            item.UserName = DataHelper.Get(r, WebColumns.UserName);
            item.Status = DataUtil.GetInt32(r, "Status");
            item.CompetitionId = DataUtil.GetId(r, "CompetitionId");
            item.IPAddress = DataHelper.Get(r, "IPAddress");
            item.Spam = DataUtil.GetInt32(r, "Spam");

            return item;
        }

        protected override string SelectProcedure
        {
            get { return "MCVote_Get"; }
        }

        public override int Update(MCVote item)
        {
            var obj = SqlHelper.ExecuteScalar("MCVote_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Code", item.Code),
                new SqlParameter("@FirstName", item.FirstName),
                new SqlParameter("@LastName", item.LastName),
                new SqlParameter("@MobileNumber", item.MobileNumber),
                new SqlParameter("@Email", item.Email),
                new SqlParameter("@CandidateId", item.CandidateId),
                new SqlParameter("@DateVoted", item.DateVoted),
                new SqlParameter("@UserName", item.UserName),
                new SqlParameter("@Status", item.Status),
                new SqlParameter("@CompetitionId", item.CompetitionId),
                new SqlParameter("@IPAddress", item.IPAddress),
                new SqlParameter("@Spam", item.Spam)
            );

            return UpdatePostProcess(item, obj);
        }

        public MCVote Get(string code)
        {
            MCVote item = null;

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@Code", code)))
            {
                if (r.Read())
                    return From(r);
            }

            return item;
        }

        public IEnumerable<MCVote> GetList(int competitionId = -2, int candidateId = -2)
        {
            List<MCVote> items = new List<MCVote>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@CandidateId", candidateId),
                new SqlParameter("@CompetitionId", competitionId)
                ))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public MCVote GetByUserName(int competitionId, string userName)
        {
            MCVote item = null;

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@UserName", userName),
                new SqlParameter("@CompetitionId", competitionId)))
            {
                if (r.Read())
                    return From(r);
            }

            return item;
        }

        public MCVote GetByEmail(int competitionId, string email, int status = -1)
        {
            MCVote item = null;

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@Email", email),
                new SqlParameter("@Status", status),
                new SqlParameter("@CompetitionId", competitionId)))
            {
                if (r.Read())
                    return From(r);
            }

            return item;
        }
    }
}
