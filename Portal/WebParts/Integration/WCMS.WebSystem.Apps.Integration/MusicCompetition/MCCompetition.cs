using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WCMS.Framework.Core;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.Providers;

namespace WCMS.WebSystem.Apps.Integration
{
    public class MCCompetition : ParameterizedWebObject, ISelfManager
    {
        public MCCompetition()
        {
            Judges = string.Empty;

            ScoreLocked = 0;
            VoteLocked = 0;
            VoteMasked = 0;

            PeoplesChoiceId = -1;
            BestInterpreterId = -1;

            CompetitionDate = DateTime.Now;
        }

        private static IMusicCompetitionProvider _provider = new MusicCompetitionProvider();
        public override int OBJECT_ID { get { return IntegrationConstants.MusicCompetition; } }
        public static IMusicCompetitionProvider Provider { get { return _provider; } }

        public string Judges { get; set; }
        public int ScoreLocked { get; set; }
        public DateTime CompetitionDate { get; set; }
        public int VoteLocked { get; set; }
        public int VoteMasked { get; set; }
        public int PeoplesChoiceId { get; set; }
        public int BestInterpreterId { get; set; }

        public bool IsScoreLocked { get { return ScoreLocked == 1; } set { ScoreLocked = value ? 1 : 0; } }
        public bool IsVoteLocked { get { return VoteLocked == 1; } set { VoteLocked = value ? 1 : 0; } }
        public bool IsVoteMasked { get { return VoteMasked == 1; } set { VoteMasked = value ? 1 : 0; } }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public int Update()
        {
            return _provider.Update(this);
        }
    }
}
