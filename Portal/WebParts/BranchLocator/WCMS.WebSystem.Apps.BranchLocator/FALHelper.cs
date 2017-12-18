using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WCMS.Framework;
using WCMS.Framework.Core.Shared;

namespace WCMS.WebSystem.Apps.BranchLocator
{
    public class FALHelper
    {
        public static string BuildAdminBreadcrumb(WQuery query)
        {
            var sb = new StringBuilder();
            int parentId = query.GetId(WebColumns.ParentId);

            query.Remove(WebColumns.Id);
            query.Remove(WConstants.Load); //.SetLoad("Admin/Chapters");

            while (parentId > 0)
            {
                var item = MChapter.Provider.Get(parentId);
                if (item != null)
                {
                    query.Set(WebColumns.ParentId, item.Id);
                    sb.Insert(0, string.Format(@"&nbsp;<span id=""cms_path_separator"">{2}</span>&nbsp;<a href='{0}' title='{1}'>{1}</a>", query.BuildQuery(), item.Name, WConstants.Arrow));
                    parentId = item.ParentId;
                }
                else
                {
                    parentId = -1;
                }
            }

            query.Remove(WebColumns.ParentId);
            sb.Insert(0, string.Format("<a href='{0}' title='{1}'>{1}</a>", query.BuildQuery(), "<strong>Chapters</strong>"));

            return sb.ToString();
        }
        public static string BuildBreadcrumb(WQuery q, int id)
        {
            var sb = new StringBuilder();
            var query = q.Clone();
            var parentId = id;
            var open = query.Get(WConstants.Open);

            if (id > 0 || open.Equals("browse", StringComparison.InvariantCultureIgnoreCase))
            {
                var countryCode = query.GetInt32("country");
                query.Set(WConstants.Open, "browse");

                if (parentId > 0)
                {
                    query.Remove("country");
                    while (parentId > 0)
                    {
                        var item = MChapter.Provider.Get(parentId);
                        if (item != null)
                        {
                            query.Set(WebColumns.Id, item.Id);
                            sb.Insert(0, string.Format(@"&nbsp;<span>/</span>&nbsp;<a href='{0}' title='{1}'>{1}</a>", query.BuildQuery(), item.Name));
                            parentId = item.ParentId;
                        }
                        else
                        {
                            parentId = -1;
                        }
                    }
                    query.Remove(WebColumns.Id);
                }

                if (countryCode > 0)
                {
                    query.Set("country", countryCode);

                    var country = Country.Provider.Get(countryCode);
                    if (country != null)
                        sb.Insert(0, string.Format(@"&nbsp;(<a href='{0}' title='{1}'>{1}</a>)", query.BuildQuery(), country.CountryName));

                    query.Remove("country");
                }


                if (id > 0 || countryCode > 0)
                    sb.Insert(0, string.Format("<a href='{0}' title='{1}'>{1}</a>", query.BuildQuery(), "Browse"));
                else
                    sb.Insert(0, "Browse");

                query.Remove(WConstants.Open);
                sb.Insert(0, string.Format("<a href='{0}' title='{1}'><strong>{1}</strong></a>&nbsp;<span>/</span>&nbsp;", query.BuildQuery(), "Dashboard"));
            }
            else
            {
                query.Remove(WConstants.Open);
                query.Remove("Search");
                sb.Insert(0, string.Format("<a href='{0}' title='{1}'><strong>{1}</strong></a>&nbsp;<span>/ Search</span>&nbsp;", query.BuildQuery(), "Dashboard"));
            }

            return sb.ToString();
        }

        public static bool IsAllowed(MChapter c, bool loggedIn, bool isAdmin)
        {
            if (isAdmin || (loggedIn && c.AccessType != ChapterAccess.RESTRICTED) || (!loggedIn && c.AccessType == ChapterAccess.PUBLIC))
                return true;
            return false;
        }

        public static double GetCoordinatesDistance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            var firstCordinate = new GeoCoordinate(latitude1, longitude1);
            var secondCordinate = new GeoCoordinate(latitude2, longitude2);

            double distance = firstCordinate.GetDistanceTo(secondCordinate);
            return distance;
        }
    }
}
