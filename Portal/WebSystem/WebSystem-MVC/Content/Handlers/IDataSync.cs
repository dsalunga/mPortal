using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Net;

namespace WCMS.WebSystem.Handlers
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDataSync" in both code and config file together.
    [ServiceContract]
    public interface IDataSync
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        WebUser GetObject();

        [OperationContract]
        List<WebUser> GetObjectList(int objectId);

        [OperationContract]
        WebUserContainer GetUserComplete(int userId);

        [OperationContract]
        void SetUserComplete(WebUserContainer container);

        [OperationContract]
        List<FileSyncInfo> GetFiles(string relativePath = "~", bool recursive = true);

        [OperationContract]
        List<WebSiteIdentity> GetBindings();
    }
}
