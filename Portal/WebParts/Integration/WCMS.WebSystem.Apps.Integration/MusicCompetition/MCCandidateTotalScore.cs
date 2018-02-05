using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.Apps.Integration
{
    public class MCCandidateTotalScore
    {
        public MCCandidateTotalScore()
        {

        }

        public MCCandidateTotalScore(MCCandidate candidate, double totalScore)
        {
            this.Candidate = candidate;
            this.TotalScore = totalScore;
        }

        public MCCandidate Candidate { get; set; }
        public double TotalScore { get; set; }
    }
}
