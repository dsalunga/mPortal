using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMember" in both code and config file together.
    [ServiceContract]
    public interface IMemberService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        bool Check(string appKey, string userName, string password, bool includeAllAccounts);

        [OperationContract]
        WSMemberInfo GetInfo(string appKey, string userName, string password, bool includeAllAccounts);

        [OperationContract]
        string[] GetInfo2(string appKey, string userName, string password, bool includeAllAccounts);
    }

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Member" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Member.svc or Member.svc.cs at the Solution Explorer and start debugging.
    public class MemberService : IMemberService
    {
        public void DoWork()
        {
        }

        public bool Check(string appKey, string userName, string password, bool includeAllAccounts)
        {
            var user = AccountHelper.ValidateLogin(userName, password);
            if (user != null && user.IsActive)
            {
                if (includeAllAccounts)
                    return true;

                MemberLink link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null && link.MemberId > 0)
                    return true;
            }

            return false;
        }

        public WSMemberInfo GetInfo(string appKey, string userName, string password, bool includeAllAccounts)
        {
            var user = AccountHelper.ValidateLogin(userName, password);
            if (user != null && user.IsActive)
            {
                MemberLink link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null && link.MemberId > 0)
                    return new WSMemberInfo(user, link);

                if (includeAllAccounts)
                    return new WSMemberInfo(user);
            }

            return null;
        }

        public string[] GetInfo2(string appKey, string userName, string password, bool includeAllAccounts)
        {
            var user = AccountHelper.ValidateLogin(userName, password);
            if (user != null && user.IsActive)
            {
                MemberLink link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null && link.MemberId > 0)
                    return new string[] { user.Id.ToString(), user.FirstAndLastName, link.MemberId.ToString() };

                if (includeAllAccounts)
                    return new string[] { user.Id.ToString(), user.FirstAndLastName };
            }

            return new string[] { };
        }
    }
}
