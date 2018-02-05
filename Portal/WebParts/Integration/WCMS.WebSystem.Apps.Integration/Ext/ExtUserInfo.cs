using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Apps.Integration.Ext
{
    public class ExtUserInfo : MemberProfileModel
    {
        public static ExtUserInfo From(DataRow userInfo)
        {
            var item = new ExtUserInfo();
            item.UserName = DataHelper.Get(userInfo, "username");
            item.ExternalId = DataHelper.Get(userInfo, "idno");
            item.LastName = DataHelper.Get(userInfo, "lname");
            item.FirstName = DataHelper.Get(userInfo, "fname");
            item.MiddleName = DataHelper.Get(userInfo, "mname");
            item.Email = DataHelper.Get(userInfo, "emailprimary");

            return item;
        }
    }
}
