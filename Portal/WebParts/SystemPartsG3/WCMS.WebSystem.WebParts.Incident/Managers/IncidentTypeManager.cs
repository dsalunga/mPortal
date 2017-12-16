using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Incident.Providers;

namespace WCMS.WebSystem.WebParts.Incident.Managers
{
    public class IncidentTypeManager : StandardDataManager<IncidentType>, IIncidentTypeProvider
    {
        protected IIncidentTypeProvider _provider;

        public IncidentTypeManager(IIncidentTypeProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public override IEnumerable<IncidentType> GetList()
        {
            return from i in base.GetList()
                   orderby i.Rank, i.Name
                   select i;
        }
    }
}
