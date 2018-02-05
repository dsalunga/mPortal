using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Registration
{
    public class MemberStatus
    {
        private long memberStatusIDField;
        private long memberIDField;
        private int memberTypeIDField;
        private int membershipStatusIDField;
        private int localeStatusIDField;
        private int localeIDField;
        private int groupIDField;
        private int committeeIDField;
        private System.DateTime membershipDateField;
        private string membershipPlaceField;
        private long orientedByIDField;
        private long onboardedByIDField;
        private int previousOrganizationIDField;
        private int withIDField;

        public long MemberStatusID
        {
            get
            {
                return this.memberStatusIDField;
            }
            set
            {
                this.memberStatusIDField = value;
            }
        }

        public long MemberID
        {
            get
            {
                return this.memberIDField;
            }
            set
            {
                this.memberIDField = value;
            }
        }

        public int MemberTypeID
        {
            get
            {
                return this.memberTypeIDField;
            }
            set
            {
                this.memberTypeIDField = value;
            }
        }

        public int MembershipStatusID
        {
            get
            {
                return this.membershipStatusIDField;
            }
            set
            {
                this.membershipStatusIDField = value;
            }
        }

        public int LocaleStatusID
        {
            get
            {
                return this.localeStatusIDField;
            }
            set
            {
                this.localeStatusIDField = value;
            }
        }

        public int LocaleID
        {
            get
            {
                return this.localeIDField;
            }
            set
            {
                this.localeIDField = value;
            }
        }

        public int GroupID
        {
            get
            {
                return this.groupIDField;
            }
            set
            {
                this.groupIDField = value;
            }
        }

        public int CommitteeID
        {
            get
            {
                return this.committeeIDField;
            }
            set
            {
                this.committeeIDField = value;
            }
        }

        public System.DateTime MembershipDate
        {
            get
            {
                return this.membershipDateField;
            }
            set
            {
                this.membershipDateField = value;
            }
        }

        public string MembershipPlace
        {
            get
            {
                return this.membershipPlaceField;
            }
            set
            {
                this.membershipPlaceField = value;
            }
        }

        public long OrientedByID
        {
            get
            {
                return this.orientedByIDField;
            }
            set
            {
                this.orientedByIDField = value;
            }
        }

        public long OnboardedByID
        {
            get
            {
                return this.onboardedByIDField;
            }
            set
            {
                this.onboardedByIDField = value;
            }
        }

        public int PreviousOrganizationID
        {
            get
            {
                return this.previousOrganizationIDField;
            }
            set
            {
                this.previousOrganizationIDField = value;
            }
        }

        public int WithID
        {
            get
            {
                return this.withIDField;
            }
            set
            {
                this.withIDField = value;
            }
        }
    }
}
