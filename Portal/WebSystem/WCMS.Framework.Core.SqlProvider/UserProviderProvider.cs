using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework.Security;

namespace WCMS.Framework.Core.SqlProvider
{
    public class UserProviderProvider : GenericSqlDataProviderBase<UserProvider>, IUserProviderProvider
    {
        protected override string SelectProcedure
        {
            get { return "UserProvider_Get"; }
        }

        protected override string DeleteProcedure
        {
            get { return "UserProvider_Del"; }
        }

        protected override UserProvider From(IDataReader r, UserProvider source)
        {
            var item = new UserProvider();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.ProviderName = DataHelper.Get(r, "ProviderName");

            return item;
        }

        public UserProvider Get(string name)
        {
            UserProvider item = null;

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@Name", name)))
                if (r.Read())
                    item = From(r);

            return item;
        }
    }
}
