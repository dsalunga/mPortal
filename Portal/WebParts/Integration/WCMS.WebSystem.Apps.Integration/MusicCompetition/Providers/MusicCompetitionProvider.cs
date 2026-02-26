using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public class MusicCompetitionProvider : GenericSqlDataProviderBase<MCCompetition>, IMusicCompetitionProvider
    {
        protected override string DeleteProcedure { get { return "MusicCompetition_Del"; } }
        protected override string TableName { get { return "MusicCompetition"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "MusicCompetition_Get"; } }

        protected override MCCompetition From(IDataReader r, MCCompetition source)
        {
            var item = source ?? new MCCompetition();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.Judges = DataUtil.Get(r, "Judges");
            item.ScoreLocked = DataUtil.GetInt32(r, "ScoreLocked");
            item.CompetitionDate = DataUtil.GetDateTime(r, "CompetitionDate");
            item.VoteLocked = DataUtil.GetInt32(r, "VoteLocked");
            item.VoteMasked = DataUtil.GetInt32(r, "VoteMasked");
            item.PeoplesChoiceId = DataUtil.GetId(r, "PeoplesChoiceId");
            item.BestInterpreterId = DataUtil.GetId(r, "BestInterpreterId");

            return item;
        }

        public override int Update(MCCompetition item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE MusicCompetition SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("Judges") + " = @Judges, " +
                    DbSyntax.QuoteIdentifier("ScoreLocked") + " = @ScoreLocked, " +
                    DbSyntax.QuoteIdentifier("CompetitionDate") + " = @CompetitionDate, " +
                    DbSyntax.QuoteIdentifier("VoteLocked") + " = @VoteLocked, " +
                    DbSyntax.QuoteIdentifier("VoteMasked") + " = @VoteMasked, " +
                    DbSyntax.QuoteIdentifier("BestInterpreterId") + " = @BestInterpreterId, " +
                    DbSyntax.QuoteIdentifier("PeoplesChoiceId") + " = @PeoplesChoiceId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Judges", item.Judges),
                    DbHelper.CreateParameter("@ScoreLocked", item.ScoreLocked),
                    DbHelper.CreateParameter("@CompetitionDate", item.CompetitionDate),
                    DbHelper.CreateParameter("@VoteLocked", item.VoteLocked),
                    DbHelper.CreateParameter("@VoteMasked", item.VoteMasked),
                    DbHelper.CreateParameter("@BestInterpreterId", item.BestInterpreterId),
                    DbHelper.CreateParameter("@PeoplesChoiceId", item.PeoplesChoiceId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO MusicCompetition (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("Judges") + ", " +
                    DbSyntax.QuoteIdentifier("ScoreLocked") + ", " +
                    DbSyntax.QuoteIdentifier("CompetitionDate") + ", " +
                    DbSyntax.QuoteIdentifier("VoteLocked") + ", " +
                    DbSyntax.QuoteIdentifier("VoteMasked") + ", " +
                    DbSyntax.QuoteIdentifier("BestInterpreterId") + ", " +
                    DbSyntax.QuoteIdentifier("PeoplesChoiceId") +
                    ") VALUES (@Name, @Judges, @ScoreLocked, @CompetitionDate, @VoteLocked, @VoteMasked, @BestInterpreterId, @PeoplesChoiceId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Judges", item.Judges),
                    DbHelper.CreateParameter("@ScoreLocked", item.ScoreLocked),
                    DbHelper.CreateParameter("@CompetitionDate", item.CompetitionDate),
                    DbHelper.CreateParameter("@VoteLocked", item.VoteLocked),
                    DbHelper.CreateParameter("@VoteMasked", item.VoteMasked),
                    DbHelper.CreateParameter("@BestInterpreterId", item.BestInterpreterId),
                    DbHelper.CreateParameter("@PeoplesChoiceId", item.PeoplesChoiceId)
                };
                var o = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                return UpdatePostProcess(item, o);
            }

            return UpdatePostProcess(item, item.Id);
        }
    }
}
