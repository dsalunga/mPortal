using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDataSync" in both code and config file together.
    [ServiceContract]
    public interface IDataSync
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<MemberLinkContainer> GetMemberLinkList();

        [OperationContract]
        MemberLinkContainer GetMemberLinkComplete(string userName);

        [OperationContract]
        void SetMemberLinkComplete(MemberLinkContainer container);
    }
}
