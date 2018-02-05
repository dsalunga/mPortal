using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.Providers;

namespace WCMS.WebSystem.Apps.Integration
{
    public class MCVote : WebObjectBase, ISelfManager
    {
        private static IMCVoteProvider _provider = new MCVoteSqlProvider();

        public MCVote()
        {
            Code = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            MobileNumber = string.Empty;
            Email = string.Empty;
            UserName = string.Empty;
            IPAddress = string.Empty;
            Status = 0;
            Spam = 0;

            CandidateId = -1;
            CompetitionId = -1;
            DateVoted = WConstants.DateTimeMinValue;
        }

        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int CandidateId { get; set; }
        public DateTime DateVoted { get; set; }
        public int Status { get; set; }
        public int CompetitionId { get; set; }
        public string IPAddress { get; set; }
        public int Spam { get; set; }

        public bool IsSpam { get { return Spam == 1; } set { Spam = value ? 1 : 0; } }

        public override int OBJECT_ID
        {
            get { return IntegrationConstants.MCVote; }
        }

        public static IMCVoteProvider Provider { get { return _provider; } }

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
