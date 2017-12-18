using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.BranchLocator
{
    public class MChapter : NamedWebObject, ISelfManager
    {
        protected static IMChapterProvider _provider;

        static MChapter()
        {
            _provider = new MChapterSqlProvider();
        }

        public MChapter()
        {
            ParentId = -1;
            DivisionId = -1;
            DistrictNo = -1;
            LocaleId = -1;

            ChapterType = 0;
            AccessType = 0;
            CountryCode = 0;

            Longitude = 0;
            Latitude = 0;

            Address = string.Empty;
            Mobile = string.Empty;
            Telephone = string.Empty;
            Email = string.Empty;
            ServiceSchedule = string.Empty;
            MoreInfo = string.Empty;
            Extra = string.Empty;

            LastUpdate = DateTime.Now;
        }

        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string ServiceSchedule { get; set; }
        public string MoreInfo { get; set; }
        public string Extra { get; set; }

        public int ParentId { get; set; }
        public int ChapterType { get; set; }
        public int AccessType { get; set; }
        public int CountryCode { get; set; }
        public int DistrictNo { get; set; }
        public int DivisionId { get; set; }
        public int LocaleId { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DateTime LastUpdate { get; set; }

        public IEnumerable<MChapter> Children { get { return _provider.GetList(this.Id); } }

        public MChapter Parent
        {
            get
            {
                if (ParentId > 0)
                    return _provider.Get(ParentId);

                return null;
            }
        }

        public static MChapter GetCentral()
        {
            var item = Provider.Get(MChapterConstants.CENTRAL_NAME, -1);
            return item;
        }

        public string GetLatLng()
        {
            return Latitude + "," + Longitude;
        }

        public MChapterExtra GetExtra()
        {
            return new MChapterExtra(Extra);
        }

        public bool HasExtra
        {
            get { return !string.IsNullOrEmpty(Extra); }
        }

        private int _hasChildren = -1;
        public bool HasChildren
        {
            get
            {
                if (_hasChildren == WConstants.NullData)
                    _hasChildren = Children.Count();

                return _hasChildren > 0;
            }
        }
        public static IMChapterProvider Provider { get { return _provider; } }

        #region IWebObject Members

        public override int OBJECT_ID { get { return -1; } }

        #endregion

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            /* No need as of now, since there is no caching?
            var parent = this.Parent;
            if (parent != null)
                parent._hasChildren = WConstants.NullData;
            */

            return _provider.Delete(this.Id);
        }

        #endregion
    }
}
