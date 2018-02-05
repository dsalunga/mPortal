using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework.Core;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.Providers;

namespace WCMS.WebSystem.Apps.Integration
{
    public class MCCandidate : NamedWebObject, ISelfManager
    {
        private static IMCCandidateProvider _provider = new MCCandidateSqlProvider();

        public MCCandidate()
        {
            Entry = string.Empty;
            Lyrics = string.Empty;
            SourceUrl = string.Empty;
            SourceUrl2 = string.Empty;
            Lyricist = string.Empty;
            Interpreter = string.Empty;
            PhotoFile = string.Empty;

            CompetitionId = -1;
            Rank = 0;
            WinnerRank = 0;
        }

        public string Entry { get; set; }
        public string Lyrics { get; set; }
        public string SourceUrl { get; set; }
        public string SourceUrl2 { get; set; }
        public string Lyricist { get; set; }
        public string Interpreter { get; set; }
        public string PhotoFile { get; set; }
        public int CompetitionId { get; set; }
        public int Rank { get; set; }
        public int WinnerRank { get; set; }

        public string GetPhotoFile()
        {
            if (string.IsNullOrEmpty(PhotoFile))
                return IntegrationConstants.MCDefaultFinalistPhoto;

            return PhotoFile;
        }

        /// <summary>
        /// Creates a shallow copy of the object instance
        /// </summary>
        /// <returns></returns>
        public MCCandidate Copy()
        {
            return (MCCandidate)this.MemberwiseClone();
        }

        public override int OBJECT_ID { get { return IntegrationConstants.MCCandidate; } }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public static IMCCandidateProvider Provider { get { return _provider; } }
    }
}
