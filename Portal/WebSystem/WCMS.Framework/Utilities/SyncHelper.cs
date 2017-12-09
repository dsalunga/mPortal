using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Utilities
{
    public class SyncHelper
    {
        public static string GetSyncTypeName(int itemType)
        {
            switch (itemType)
            {
                case RemoteItemTypes.LOCAL:
                    return "LOCAL";

                case RemoteItemTypes.REMOTE:
                    return "REMOTE";

                case RemoteItemTypes.IDENTICAL:
                    return "IDENTICAL";

                default:
                    return "UNKNOWN";
            }
        }
    }
}
