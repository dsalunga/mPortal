using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Apps.Integration.ExtApp
{
    public class ExtAppUserInfo : MemberProfileModel
    {
        public static ExtAppUserInfo From(DataRow userInfo)
        {
            var item = new ExtAppUserInfo();
            item.UserName = DataUtil.Get(userInfo, "username");
            item.ExternalId = DataUtil.Get(userInfo, "idno");
            item.LastName = DataUtil.Get(userInfo, "lname");
            item.FirstName = DataUtil.Get(userInfo, "fname");
            item.MiddleName = DataUtil.Get(userInfo, "mname");
            item.Email = DataUtil.Get(userInfo, "emailprimary");

            return item;
        }
    }
}
