using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;
using WCMS.WebSystem.Apps.Integration.Providers;

namespace WCMS.WebSystem.Apps.Integration
{
    public class MCSongScore : WebObjectBase, ISelfManager
    {
        private static IMCSongScoreProvider _provider;

        static MCSongScore()
        {
            _provider = new MCSongScoreSqlProvider();
        }

        public int JudgeId { get; set; }
        public int Musicality { get; set; }
        public int LyricsMessage { get; set; }
        public int OverallImpact { get; set; }
        public DateTime DateModified { get; set; }
        public int CandidateId { get; set; }
        public int CompetitionId { get; set; }

        public MCSongScore()
        {
            JudgeId = -1;
            CandidateId = -1;
            CompetitionId = -1;

            Musicality = -1;
            LyricsMessage = -1;
            OverallImpact = -1;

            DateModified = DateTime.Now;
        }

        public static IMCSongScoreProvider Provider { get { return _provider; } }

        public override int OBJECT_ID
        {
            get { return -1; }
        }

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
