using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Incident.Providers;

namespace WCMS.WebSystem.WebParts.Incident
{
    public class IncidentCategory : NamedWebObject, ISelfManager
    {
        private static IIncidentCategoryProvider _manager;

        static IncidentCategory()
        {
            _manager = WebObject.ResolveManager<IncidentCategory, IIncidentCategoryProvider>(WebObject.ResolveProvider<IncidentCategory, IIncidentCategoryProvider>());
        }

        public IncidentCategory()
        {
            GroupId = -1;
            InstanceId = -1;
            Rank = 1;

            Description = string.Empty;
        }

        public int GroupId { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public int InstanceId { get; set; }

        public WebGroup Group { get { return WebGroup.Get(GroupId); } }

        public override int OBJECT_ID { get { return IncidentConstants.INCIDENT_CATEGORY_ID; } }

        public static IIncidentCategoryProvider Provider { get { return _manager; } }

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
