using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Incident.Providers;

namespace WCMS.WebSystem.WebParts.Incident.Managers
{
    public class IncidentCategoryManager : StandardDataManager<IncidentCategory>, IIncidentCategoryProvider
    {
        protected IIncidentCategoryProvider _provider;

        public IncidentCategoryManager(IIncidentCategoryProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public override IEnumerable<IncidentCategory> GetList()
        {
            return from i in base.GetList()
                   orderby i.Rank, i.Name
                   select i;
        }
    }
}
