using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.Providers;

namespace WCMS.WebSystem.Apps.Integration
{
    public class MemberVisit : IWebObject, ISelfManager
    {
        private static IMemberVisitProvider _provider;

        static MemberVisit()
        {
            _provider = new MemberVisitSqlProvider();
        }

        public MemberVisit()
        {
            Name = string.Empty;
            Tags = string.Empty;

            CreatedUserId = -1;
            GroupId = -1;
            TimesVisited = 1;

            DateCreated = DateTime.Now;
            DateVisited = DateTime.Now;
            MembershipDate = WConstants.DateTimeMinValue;
        }

        #region Properties

        private string _name;
        public string Name
        {
            get
            {
                var user = VisitedUser;

                return user == null ? _name : AccountHelper.GetPrefixedName(user, NamePrefixes.Brotherhood);
            }

            set { _name = value; }
        }

        private int _visitedUserId = -1;
        public int VisitedUserId
        {
            get { return _visitedUserId; }

            set
            {
                if (value != _visitedUserId)
                {
                    _linkFetched = false;
                    _link = null;
                    _visitedUser = null;
                }

                _visitedUserId = value;
            }
        }


        public DateTime DateCreated { get; set; }
        public string ActualReport { get; set; }
        public string Status { get; set; }
        public int GroupId { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime DateVisited { get; set; }
        public string ActionTaken { get; set; }
        public int TimesVisited { get; set; }
        public string Tags { get; set; }

        private string _contactNo = string.Empty;
        public string ContactNo
        {
            get
            {
                var link = Link;

                if (link != null && string.IsNullOrEmpty(_contactNo))
                    return link.ContactNoEval;

                return _contactNo;
            }

            set
            {
                _contactNo = value;
            }
        }

        private string _address = string.Empty;
        public string Address
        {
            get
            {
                var link = Link;

                if (link != null && string.IsNullOrEmpty(_address))
                    return link.SingleLineHomeAddress;

                return _address;
            }

            set { _address = value; }
        }

        public DateTime MembershipDate { get; set; }

        private bool _linkFetched = false;
        private MemberLink _link = null;
        public MemberLink Link
        {
            get
            {
                if (VisitedUser != null)
                {
                    if (_link == null && !_linkFetched)
                    {
                        _linkFetched = true;
                        _link = MemberLink.Provider.GetByUserId(VisitedUser.Id);
                    }

                    return _link;
                }

                return null;
            }
        }


        public WebUser CreatedUser
        {
            get
            {
                if (CreatedUserId > 0)
                    return WebUser.Get(CreatedUserId);

                return null;
            }
        }

        private WebUser _visitedUser = null;
        public WebUser VisitedUser
        {
            get
            {
                if (_visitedUser == null && _visitedUserId > 0)
                    _visitedUser = WebUser.Get(_visitedUserId);

                return _visitedUser;
            }
        }

        public WebGroup Group
        {
            get { return GroupId > 0 ? WebGroup.Get(GroupId) : null; }
        }

        public static IMemberVisitProvider Provider
        {
            get { return _provider; }
        }

        #endregion

        #region IWebObject Members

        public int Id { get; set; }

        public int OBJECT_ID
        {
            get { return 97; }
        }

        #endregion

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        #endregion
    }
}
