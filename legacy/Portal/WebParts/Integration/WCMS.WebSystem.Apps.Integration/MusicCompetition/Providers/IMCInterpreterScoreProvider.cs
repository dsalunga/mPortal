using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public interface IMCInterpreterScoreProvider : IDataProvider<MCInterpreterScore>
    {
        IEnumerable<MCInterpreterScore> GetList(int competitionId = -2, int candidateId = -2, int judgeId = -2);
    }
}
