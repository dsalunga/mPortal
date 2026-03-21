using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Social;

namespace WCMS.WebSystem.Apps.Integration
{
    public class ProfileUpdateEvent : WallUpdateEventBase
    {
        public ProfileUpdateEvent(int userId)
            : base()
        {
            _update.EventTypeId = WallEventTypes.StatusUpdate;

            _update.UpdateObjectId = WebObjects.WebUser;
            _update.UpdateRecordId = userId;

            _update.ByObjectId = WebObjects.WebUser;
            _update.ByRecordId = userId;
        }
    }
}
