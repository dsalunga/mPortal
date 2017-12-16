using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Incident.Providers;

namespace WCMS.WebSystem.WebParts.Incident
{
    public class IncidentInstance : NamedWebObject, ISelfManager
    {
        private static IncidentInstanceProvider _manager;

        static IncidentInstance()
        {
            _manager = new IncidentInstanceProvider();
        }

        public IncidentInstance()
        {
        }

        public string IncidentPrefix { get; set; }
        public string BaseGroup { get; set; }
        public string SupportGroupPath { get; set; }
        public int SLAHighDuration { get; set; }
        public int SLALowDuration { get; set; }
        public int SLANormalDuration { get; set; }
        public double SLAWarningPercentage { get; set; }

        public static IncidentInstanceProvider Provider { get { return _manager; } }

        public override int OBJECT_ID { get { return -1; } }

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
