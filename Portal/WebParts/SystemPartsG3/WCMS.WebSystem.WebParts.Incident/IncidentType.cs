using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Incident.Providers;

namespace WCMS.WebSystem.WebParts.Incident
{
    public class IncidentType : NamedWebObject, ISelfManager
    {
        private static IIncidentTypeProvider _manager;

        static IncidentType()
        {
            _manager = WebObject.ResolveManager<IncidentType, IIncidentTypeProvider>(WebObject.ResolveProvider<IncidentType, IIncidentTypeProvider>());
        }

        public IncidentType()
        {
            Rank = 1;
            FollowStdSLA = 1;
            InstanceId = -1;
        }

        public int FollowStdSLA { get; set; }
        public int Rank { get; set; }
        public int InstanceId { get; set; }

        public override int OBJECT_ID { get { return IncidentConstants.INCIDENT_TYPE_ID; } }

        public bool UseSLA { get { return FollowStdSLA == 1; } }

        public static IIncidentTypeProvider Provider { get { return _manager; } }

        #region ISelfManager Members

        public bool Delete()
        {
            return _manager.Delete(this.Id);
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        #endregion
    }
}
