using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public class MCComposerProvider : GenericSqlDataProviderBase<MCComposer>, IMCComposerProvider
    {
        protected override string DeleteProcedure
        {
            get { return "MCComposer_Del"; }
        }

        protected override MCComposer From(IDataReader r, MCComposer source)
        {
            var item = source ?? new MCComposer();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.Entry = DataHelper.Get(r, "Entry");
            item.Locale = DataHelper.Get(r, WebColumns.Locale);
            item.Work = DataHelper.Get(r, WebColumns.Work);
            item.Description = DataHelper.Get(r, WebColumns.Description);
            item.PhotoFile = DataHelper.Get(r, "PhotoFile");
            item.NickName = DataHelper.Get(r, "NickName");
            item.Active = DataUtil.GetInt32(r, WebColumns.Active);
            item.CompetitionId = DataUtil.GetId(r, "CompetitionId");

            return item;
        }

        protected override string SelectProcedure
        {
            get { return "MCComposer_Get"; }
        }

        public override int Update(MCComposer item)
        {
            var obj = SqlHelper.ExecuteScalar("MCComposer_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Entry", item.Entry),
                new SqlParameter("@Locale", item.Locale),
                new SqlParameter("@Work", item.Work),
                new SqlParameter("@Description", item.Description),
                new SqlParameter("@PhotoFile", item.PhotoFile),
                new SqlParameter("@NickName", item.NickName),
                new SqlParameter("@Active", item.Active),
                new SqlParameter("@CompetitionId", item.CompetitionId)
            );

            return UpdatePostProcess(item, obj);
        }

        public IEnumerable<MCComposer> GetList(int competitionId)
        {
            List<MCComposer> items = new List<MCComposer>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@CompetitionId", competitionId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
