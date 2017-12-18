using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.BranchLocator
{
    public class MChapterSqlProvider : GenericSqlDataProviderBase<MChapter>, IMChapterProvider
    {
        protected override string DeleteProcedure { get { return "MChapter_Del"; } }
        protected override string SelectProcedure { get { return "MChapter_Get"; } }

        protected override MChapter From(IDataReader r, MChapter source)
        {
            var item = source ?? new MChapter();
            item.Id = DataUtil.GetId(r, WebColumns.Id, true);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.Address = DataUtil.Get(r, "Address");
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId, true);
            item.ChapterType = DataUtil.GetInt32(r, "ChapterType", true);
            item.Latitude = DataUtil.GetDouble(r, "Latitude", true);
            item.Longitude = DataUtil.GetDouble(r, "Longitude", true);
            item.AccessType = DataUtil.GetInt32(r, "AccessType", true);
            item.CountryCode = DataUtil.GetInt32(r, "CountryCode", true);
            item.Email = DataUtil.Get(r, WebColumns.Email);
            item.Mobile = DataUtil.Get(r, "Mobile");
            item.Telephone = DataUtil.Get(r, "Telephone");
            item.ServiceSchedule = DataUtil.Get(r, "ServiceSchedule");
            item.MoreInfo = DataUtil.Get(r, "MoreInfo");
            item.Extra = DataUtil.Get(r, "Extra");
            item.DistrictNo = DataUtil.GetInt32(r, "DistrictNo");
            item.DivisionId = DataUtil.GetId(r, "DivisionId");
            item.LocaleId = DataUtil.GetId(r, "LocaleId");
            item.LastUpdate = DataUtil.GetDateTime(r, "LastUpdate");

            return item;
        }

        public override int Update(MChapter item)
        {
            var o = SqlHelper.ExecuteScalar("MChapter_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@ParentId", item.ParentId),
                new SqlParameter("@Address", item.Address),
                new SqlParameter("@ChapterType", item.ChapterType),
                new SqlParameter("@Latitude", item.Latitude),
                new SqlParameter("@Longitude", item.Longitude),
                new SqlParameter("@AccessType", item.AccessType),
                new SqlParameter("@CountryCode", item.CountryCode),
                new SqlParameter("@Email", item.Email),
                new SqlParameter("@Mobile", item.Mobile),
                new SqlParameter("@Telephone", item.Telephone),
                new SqlParameter("@ServiceSchedule", item.ServiceSchedule),
                new SqlParameter("@MoreInfo", item.MoreInfo),
                new SqlParameter("@Extra", item.Extra),
                new SqlParameter("@DistrictNo", item.DistrictNo),
                new SqlParameter("@DivisionId", item.DivisionId),
                new SqlParameter("@LocaleId", item.LocaleId),
                new SqlParameter("@LastUpdate", item.LastUpdate)
            );

            item.Id = DataUtil.GetId(o);

            return item.Id;
        }

        public IEnumerable<MChapter> GetList(int parentId)
        {
            var items = new List<MChapter>();
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@ParentId", parentId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }
            return items;
        }

        public IEnumerable<MChapter> GetListByLocaleId(int localeId)
        {
            var items = new List<MChapter>();
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@LocaleId", localeId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }
            return items;
        }

        public MChapter GetByLocaleId(int localeId)
        {
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@LocaleId", localeId)))
            {
                if (r.Read())
                    return From(r);
            }
            return null;
        }

        public MChapter Get(string name, int parentId = -2)
        {
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@ParentId", parentId),
                new SqlParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }
            return null;
        }
    }
}
