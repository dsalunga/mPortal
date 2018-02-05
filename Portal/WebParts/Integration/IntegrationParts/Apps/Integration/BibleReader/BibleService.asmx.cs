using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WCMS.WebSystem.WebParts.BibleReader
{
    /// <summary>
    /// Summary description for BibleService
    /// </summary>
    [WebService(Namespace = "https://someorg.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BibleService : System.Web.Services.WebService
    {
        [WebMethod] //(EnableSession=true)]
        public BibleUserSession GetUserSession(string guid)
        {
            return BibleUserSession.Get(guid);
        }

        [WebMethod]
        public BibleAccessResult GetBibleAppAccessInfo(int userId)
        {
            BibleAccess item = BibleAccess.Provider.Get(userId);

            if (item.AppAccessCount == -1)
                item.AppAccessCount = BibleAppConfig.AppDownloadCount;

            return new BibleAccessResult(item);
        }

        [WebMethod]
        public BibleAccessResult PerformBibleAppDownload(int userId)
        {
            BibleAccess item = BibleAccess.Provider.Get(userId);
            if (item != null)
            {
                if (item.AppAccessCount != 0)
                {
                    if (item.AppAccessCount == -1)
                        item.AppAccessCount = BibleAppConfig.AppDownloadCount - 1;
                    else
                        item.AppAccessCount--;

                    item.Update();

                    return new BibleAccessResult(item);
                }

                return new BibleAccessResult(item, BibleAccessStatuses.DENIED);
            }
            else
            {
                item = new BibleAccess();
                item.UserId = userId;
                item.AppAccessCount = BibleAppConfig.AppDownloadCount;
                item.Update();
            }

            return new BibleAccessResult(item, BibleAccessStatuses.ERROR);
        }

        [WebMethod]
        public BibleVersionAccessResult GetBibleVersionAccessInfo(int userId, int bibleVersionId)
        {
            BibleAccess item = BibleAccess.Provider.Get(userId);
            if (item != null)
            {
                var items = BibleVersionAccess.Provider.GetList(item.Id);
                if (items.Count() > 0)
                {
                    var accessInfo = items.FirstOrDefault(i => i.BibleVersionId == bibleVersionId);
                    if (accessInfo != null)
                    {
                        if (accessInfo.VersionAccessCount == -1)
                            accessInfo.VersionAccessCount = BibleAppConfig.VersionDownloadCount;

                        return new BibleVersionAccessResult(item, accessInfo, BibleAccessStatuses.SUCCESS);
                    }
                }
            }

            return new BibleVersionAccessResult(item, default(BibleVersionAccess), BibleAccessStatuses.ERROR);
        }

        [WebMethod]
        public BibleVersionAccessResult GetBibleVersionAccessList(int userId)
        {
            BibleAccess item = BibleAccess.Provider.Get(userId);
            if (item != null)
            {
                //var items = BibleReaderVersionAccess.Provider.GetList(item.Id).ToList();

                item.VersionAccesses = null;
                item.PopulateVersionAccesses();

                foreach (var accessInfo in item.VersionAccesses)
                {
                    if (accessInfo.VersionAccessCount == -1)
                        accessInfo.VersionAccessCount = BibleAppConfig.VersionDownloadCount;
                }

                return new BibleVersionAccessResult(item, null, BibleAccessStatuses.SUCCESS);
            }

            return new BibleVersionAccessResult(item, null, BibleAccessStatuses.ERROR);
        }

        [WebMethod]
        public BibleVersionAccessResult PerformBibleVersionDownload(int userId, int bibleVersionId)
        {
            BibleVersionAccessResult result = null;
            BibleVersionAccess accessInfo = null;

            BibleAccess item = BibleAccess.Provider.Get(userId);

            Action CreateVersionAccessEntry = () =>
            {
                if (bibleVersionId > 0)
                {
                    accessInfo = new BibleVersionAccess();
                    accessInfo.BibleAccessId = item.Id;
                    accessInfo.BibleVersionId = bibleVersionId;
                    accessInfo.VersionAccessCount = BibleAppConfig.VersionDownloadCount - 1;
                    accessInfo.Update();
                }
            };

            if (item != null)
            {
                var items = BibleVersionAccess.Provider.GetList(item.Id);
                if (items.Count() > 0 && (accessInfo = items.FirstOrDefault(i => i.BibleVersionId == bibleVersionId)) != null)
                {
                    if (accessInfo.VersionAccessCount != 0)
                    {
                        if (accessInfo.VersionAccessCount == -1)
                            accessInfo.VersionAccessCount = BibleAppConfig.VersionDownloadCount - 1;
                        else
                            accessInfo.VersionAccessCount--;

                        accessInfo.Update();

                        result = new BibleVersionAccessResult(item, accessInfo, BibleAccessStatuses.SUCCESS);
                    }
                    else
                    {
                        result = new BibleVersionAccessResult(item, accessInfo, BibleAccessStatuses.DENIED);
                    }
                }
                else
                {
                    // Create VersionAccess entry

                    CreateVersionAccessEntry();

                    result = new BibleVersionAccessResult(item, accessInfo, BibleAccessStatuses.SUCCESS);
                }
            }
            else if (userId > 0)
            {
                // Create AppAccess entry
                item = new BibleAccess();
                item.UserId = userId;
                item.AppAccessCount = BibleAppConfig.AppDownloadCount;
                item.Update();

                CreateVersionAccessEntry();

                result = new BibleVersionAccessResult(item, accessInfo, BibleAccessStatuses.SUCCESS);
            }
            else
            {
                result = new BibleVersionAccessResult(null, null, BibleAccessStatuses.ERROR);
            }

            return result;
        }
    }
}
