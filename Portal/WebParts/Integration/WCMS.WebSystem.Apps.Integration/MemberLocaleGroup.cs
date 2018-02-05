using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Registration
{
    public class MemberLocaleGroup
    {
        private long memberLocaleGroupIDField;
        private long memberIDField;
        private int localeGroupIDField;
        private System.DateTime dateJoinedField;
        private int isActiveField;

        public long MemberLocaleGroupID
        {
            get
            {
                return this.memberLocaleGroupIDField;
            }
            set
            {
                this.memberLocaleGroupIDField = value;
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

        public int LocaleGroupID
        {
            get
            {
                return this.localeGroupIDField;
            }
            set
            {
                this.localeGroupIDField = value;
            }
        }

        public System.DateTime DateJoined
        {
            get
            {
                return this.dateJoinedField;
            }
            set
            {
                this.dateJoinedField = value;
            }
        }

        public int IsActive
        {
            get
            {
                return this.isActiveField;
            }
            set
            {
                this.isActiveField = value;
            }
        }
    }
}
