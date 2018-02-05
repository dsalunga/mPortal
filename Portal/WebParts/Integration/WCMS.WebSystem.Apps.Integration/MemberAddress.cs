using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Registration
{
    public class MemberAddress
    {
        private long memberAddressIDField;
        private long memberIDField;
        private string address1Field;
        private string address2Field;
        private int cityIDField;
        private int stateIDField;
        private int countryIDField;
        private string postalCodeField;
        private string phoneField;
        private int isDefaultField;

        public long MemberAddressID
        {
            get
            {
                return this.memberAddressIDField;
            }
            set
            {
                this.memberAddressIDField = value;
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

        public string Address1
        {
            get
            {
                return this.address1Field;
            }
            set
            {
                this.address1Field = value;
            }
        }

        public string Address2
        {
            get
            {
                return this.address2Field;
            }
            set
            {
                this.address2Field = value;
            }
        }

        public int CityID
        {
            get
            {
                return this.cityIDField;
            }
            set
            {
                this.cityIDField = value;
            }
        }

        public int StateID
        {
            get
            {
                return this.stateIDField;
            }
            set
            {
                this.stateIDField = value;
            }
        }

        public int CountryID
        {
            get
            {
                return this.countryIDField;
            }
            set
            {
                this.countryIDField = value;
            }
        }

        public string PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }

        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }

        public int IsDefault
        {
            get
            {
                return this.isDefaultField;
            }
            set
            {
                this.isDefaultField = value;
            }
        }
    }
}
