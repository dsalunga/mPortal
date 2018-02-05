using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.Apps.Integration.Providers;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.Providers;

namespace WCMS.WebSystem.Apps.Integration.ExternalMemberWS
{
    public partial class Member : IWebObject, ISelfManager
    {
        private static IMemberProvider _provider;
        private static ExternalMemberSqlProvider _remoteProvider;

        static Member()
        {
            _provider = new MemberSqlProvider();
            _remoteProvider = new ExternalMemberSqlProvider();
        }

        public Member()
        {
        }

        #region Properties

        public string FullName
        {
            get
            {
                string fullName = "";
                if (LastName.Length > 0) fullName = LastName + ",";
                if (FirstName.Length > 0) fullName += " " + FirstName;
                if (MiddleName.Length > 0) fullName += " " + MiddleName.Substring(0, 1) + ".";

                return fullName.Trim();
            }
        }

        public int Age
        {
            get
            {
                return DateTime.Now.Year - BirthDate.Year - (BirthDate.Month > DateTime.Now.Month ? 1 : (BirthDate.Month != DateTime.Now.Month) ? 0 : (BirthDate.Day > DateTime.Now.Day) ? 1 : 0);
            }
        }

        public int MaterialAge
        {
            get
            {
                return DateTime.Now.Year - MembershipDate.Year - (MembershipDate.Month > DateTime.Now.Month ? 1 : (MembershipDate.Month != DateTime.Now.Month) ? 0 : (MembershipDate.Day > DateTime.Now.Day) ? 1 : 0);
            }
        }

        [XmlIgnore]
        public string EvalExternalId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ExternalIDNo))
                    return TemporaryIDNo;
                else
                    return ExternalIDNo;
            }
        }

        private string _photoPath;

        [XmlIgnore]
        public string PhotoPath
        {
            get
            {
                if (string.IsNullOrEmpty(_photoPath))
                {
                    var client = new MemberSoapClient(false);

                    var photos = client.GetPhoto(Id);
                    if (photos != null && photos.Length > 0)
                    {
                        var photo = photos.First();
                        if (!string.IsNullOrEmpty(photo.PhotoFileName))
                            _photoPath = photo.PhotoPath;
                    }
                }

                return _photoPath;
            }

            set
            {
                _photoPath = value;
            }
        }

        public string GetPhotoPath(string size = "")
        {
            var photoPath = PhotoPath;

            if (!string.IsNullOrEmpty(size) && !string.IsNullOrEmpty(photoPath) && photoPath.Contains("/brethren/photos/"))
                return photoPath.Replace("/brethren/photos/", string.Format("/brethren/photos/{0}/", size));
            else
                return photoPath;
        }

        public static Providers.IMemberProvider Provider
        {
            get { return _provider; }
        }

        public static ExternalMemberSqlProvider RemoteProvider
        {
            get { return _remoteProvider; }
        }

        [XmlIgnore]
        public DateTime MembershipDate { get; set; }

        #endregion

        #region IWebObject Members

        [XmlIgnore]
        public int Id
        {
            get { return (int)MemberID; }
            set { MemberID = value; }
        }

        public int OBJECT_ID
        {
            get { return -1; }
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

        public string GetSuggestedUserName()
        {
            string firstName = FirstName.Trim();
            if (firstName.Contains(" "))
            {
                if (firstName.Contains("."))
                {
                    firstName = firstName.Substring(firstName.IndexOf(" ") + 1);
                }
                else
                {
                    firstName = firstName.Substring(0, firstName.IndexOf(" "));
                }
            }

            var userName = string.Format("{0}.{1}", firstName, LastName).Replace(" ", "");

            return userName;
        }
    }
}
