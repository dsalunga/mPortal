using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Providers
{
    public interface IMCVoteProvider : IDataProvider<MCVote>
    {
        MCVote Get(string code);
        MCVote GetByUserName(int competitionId, string userName);
        MCVote GetByEmail(int competitionId, string email, int status =-1);
        IEnumerable<MCVote> GetList(int competitionId = -2, int candidateId = -2);
    }
}
