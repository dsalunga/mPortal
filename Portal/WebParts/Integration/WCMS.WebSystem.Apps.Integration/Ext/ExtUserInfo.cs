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
