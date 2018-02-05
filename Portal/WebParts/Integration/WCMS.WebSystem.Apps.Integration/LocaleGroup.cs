using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Registration
{
    public class LocaleGroup
    {
        private int localeGroupIDField;
        private int localeIDField;
        private string localeGroupNameField;
        private int isActiveField;

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

        public string LocaleGroupName
        {
            get
            {
                return this.localeGroupNameField;
            }
            set
            {
                this.localeGroupNameField = value;
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
