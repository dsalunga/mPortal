﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public interface IMCCandidateProvider : IDataProvider<MCCandidate>
    {
        IEnumerable<MCCandidate> GetList(int competitionId);
    }
}
