using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

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
            item.Code = DataUtil.Get(r, "Code");
            item.FirstName = DataUtil.Get(r, "FirstName");
            item.LastName = DataUtil.Get(r, "LastName");
            item.MobileNumber = DataUtil.Get(r, "MobileNumber");
            item.Email = DataUtil.Get(r, "Email");
            item.CandidateId = DataUtil.GetId(r, "CandidateId");
            item.DateVoted = DataUtil.GetDateTime(r, "DateVoted");
            item.UserName = DataUtil.Get(r, WebColumns.UserName);
            item.Status = DataUtil.GetInt32(r, "Status");
            item.CompetitionId = DataUtil.GetId(r, "CompetitionId");
            item.IPAddress = DataUtil.Get(r, "IPAddress");
            item.Spam = DataUtil.GetInt32(r, "Spam");

            return item;
        }

        protected override string TableName { get { return "MCVote"; } }


        protected override string IdColumn { get { return "Id"; } }



        protected override string SelectProcedure
        {
            get { return "MCVote_Get"; }
        }

        public override int Update(MCVote item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE MCVote SET " +
                    DbSyntax.QuoteIdentifier("Code") + " = @Code, " +
                    DbSyntax.QuoteIdentifier("FirstName") + " = @FirstName, " +
                    DbSyntax.QuoteIdentifier("LastName") + " = @LastName, " +
                    DbSyntax.QuoteIdentifier("MobileNumber") + " = @MobileNumber, " +
                    DbSyntax.QuoteIdentifier("Email") + " = @Email, " +
                    DbSyntax.QuoteIdentifier("CandidateId") + " = @CandidateId, " +
                    DbSyntax.QuoteIdentifier("DateVoted") + " = @DateVoted, " +
                    DbSyntax.QuoteIdentifier("UserName") + " = @UserName, " +
                    DbSyntax.QuoteIdentifier("Status") + " = @Status, " +
                    DbSyntax.QuoteIdentifier("CompetitionId") + " = @CompetitionId, " +
                    DbSyntax.QuoteIdentifier("IPAddress") + " = @IPAddress, " +
                    DbSyntax.QuoteIdentifier("Spam") + " = @Spam" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Code", item.Code),
                    DbHelper.CreateParameter("@FirstName", item.FirstName),
                    DbHelper.CreateParameter("@LastName", item.LastName),
                    DbHelper.CreateParameter("@MobileNumber", item.MobileNumber),
                    DbHelper.CreateParameter("@Email", item.Email),
                    DbHelper.CreateParameter("@CandidateId", item.CandidateId),
                    DbHelper.CreateParameter("@DateVoted", item.DateVoted),
                    DbHelper.CreateParameter("@UserName", item.UserName),
                    DbHelper.CreateParameter("@Status", item.Status),
                    DbHelper.CreateParameter("@CompetitionId", item.CompetitionId),
                    DbHelper.CreateParameter("@IPAddress", item.IPAddress),
                    DbHelper.CreateParameter("@Spam", item.Spam),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO MCVote (" +
                    DbSyntax.QuoteIdentifier("Code") + ", " +
                    DbSyntax.QuoteIdentifier("FirstName") + ", " +
                    DbSyntax.QuoteIdentifier("LastName") + ", " +
                    DbSyntax.QuoteIdentifier("MobileNumber") + ", " +
                    DbSyntax.QuoteIdentifier("Email") + ", " +
                    DbSyntax.QuoteIdentifier("CandidateId") + ", " +
                    DbSyntax.QuoteIdentifier("DateVoted") + ", " +
                    DbSyntax.QuoteIdentifier("UserName") + ", " +
                    DbSyntax.QuoteIdentifier("Status") + ", " +
                    DbSyntax.QuoteIdentifier("CompetitionId") + ", " +
                    DbSyntax.QuoteIdentifier("IPAddress") + ", " +
                    DbSyntax.QuoteIdentifier("Spam") +
                    ") VALUES (@Code, @FirstName, @LastName, @MobileNumber, @Email, @CandidateId, @DateVoted, @UserName, @Status, @CompetitionId, @IPAddress, @Spam)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Code", item.Code),
                    DbHelper.CreateParameter("@FirstName", item.FirstName),
                    DbHelper.CreateParameter("@LastName", item.LastName),
                    DbHelper.CreateParameter("@MobileNumber", item.MobileNumber),
                    DbHelper.CreateParameter("@Email", item.Email),
                    DbHelper.CreateParameter("@CandidateId", item.CandidateId),
                    DbHelper.CreateParameter("@DateVoted", item.DateVoted),
                    DbHelper.CreateParameter("@UserName", item.UserName),
                    DbHelper.CreateParameter("@Status", item.Status),
                    DbHelper.CreateParameter("@CompetitionId", item.CompetitionId),
                    DbHelper.CreateParameter("@IPAddress", item.IPAddress),
                    DbHelper.CreateParameter("@Spam", item.Spam)
                };
                var o = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                return UpdatePostProcess(item, o);
            }

            return UpdatePostProcess(item, item.Id);
        }

        public MCVote Get(string code)
        {
            MCVote item = null;

            var sql = "SELECT * FROM MCVote WHERE " + DbSyntax.QuoteIdentifier("Code") + " = @Code";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Code", code)))
            {
                if (r.Read())
                    return From(r);
            }

            return item;
        }

        public IEnumerable<MCVote> GetList(int competitionId = -2, int candidateId = -2)
        {
            List<MCVote> items = new List<MCVote>();

            var sql = "SELECT * FROM MCVote WHERE " +
                "(@CandidateId = -2 OR " + DbSyntax.QuoteIdentifier("CandidateId") + " = @CandidateId) AND " +
                "(@CompetitionId = -2 OR " + DbSyntax.QuoteIdentifier("CompetitionId") + " = @CompetitionId)";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@CandidateId", candidateId),
                DbHelper.CreateParameter("@CompetitionId", competitionId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public MCVote GetByUserName(int competitionId, string userName)
        {
            MCVote item = null;

            var sql = "SELECT * FROM MCVote WHERE " +
                DbSyntax.QuoteIdentifier("UserName") + " = @UserName AND " +
                DbSyntax.QuoteIdentifier("CompetitionId") + " = @CompetitionId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@UserName", userName),
                DbHelper.CreateParameter("@CompetitionId", competitionId)))
            {
                if (r.Read())
                    return From(r);
            }

            return item;
        }

        public MCVote GetByEmail(int competitionId, string email, int status = -1)
        {
            MCVote item = null;

            var sql = "SELECT * FROM MCVote WHERE " +
                DbSyntax.QuoteIdentifier("Email") + " = @Email AND " +
                "(@Status = -1 OR " + DbSyntax.QuoteIdentifier("Status") + " = @Status) AND " +
                DbSyntax.QuoteIdentifier("CompetitionId") + " = @CompetitionId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Email", email),
                DbHelper.CreateParameter("@Status", status),
                DbHelper.CreateParameter("@CompetitionId", competitionId)))
            {
                if (r.Read())
                    return From(r);
            }

            return item;
        }
    }
}
