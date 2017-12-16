using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Photo.Providers
{
    public class AlbumLinkProvider : GenericSqlDataProviderBase<AlbumLink>, IAlbumLinkProvider
    {
        protected override string DeleteProcedure
        {
            get { throw new NotImplementedException(); }
        }

        protected override AlbumLink From(IDataReader r, AlbumLink source)
        {
            var item = source ?? new AlbumLink();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.SiteId = DataHelper.GetId(r, WebColumns.SiteId);
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.AlbumId = DataHelper.GetId(r, "CategoryId");

            return item;
        }

        protected override string SelectProcedure { get { return "GalleryCategoryLink_Get"; } }

        public IEnumerable<AlbumLink> GetList(int objectId, int recordId)
        {
            List<AlbumLink> items = new List<AlbumLink>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
