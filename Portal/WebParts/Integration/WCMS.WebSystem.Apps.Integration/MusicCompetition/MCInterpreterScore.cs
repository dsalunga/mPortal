using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;
using WCMS.WebSystem.Apps.Integration.Providers;

namespace WCMS.WebSystem.Apps.Integration
{
    public class MCInterpreterScore : WebObjectBase, ISelfManager
    {
        private static IMCInterpreterScoreProvider _provider;

        static MCInterpreterScore()
        {
            _provider = new MCInterpreterScoreSqlProvider();
        }

        public int JudgeId { get; set; }
        public int VoiceQuality { get; set; }
        public int Interpretation { get; set; }
        public int StagePresence { get; set; }
        public int OverallImpact { get; set; }
        public DateTime DateModified { get; set; }
        public int CandidateId { get; set; }
        public int CompetitionId { get; set; }

        public MCInterpreterScore()
        {
            JudgeId = -1;
            CandidateId = -1;
            CompetitionId = -1;

            VoiceQuality = -1;
            Interpretation = -1;
            StagePresence = -1;
            OverallImpact = -1;

            DateModified = DateTime.Now;
        }

        public override int OBJECT_ID
        {
            get { return -1; }
        }

        public static IMCInterpreterScoreProvider Provider { get { return _provider; } }

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
