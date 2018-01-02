using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.BibleReader
{
    public static class BibleAppConfig
    {
        private static int APP_DOWNLOAD_COUNT_ID = -1;
        private static int VERSION_DOWNLOAD_COUNT_ID = -1;

        static BibleAppConfig()
        {
            WebRegistry.Updated += RegistryNodeUpdated;
        }

        private static void RegistryNodeUpdated(object sender, WebRegistryUpdateEventArgs args)
        {
            var id = args.UpdatedNode.Id;

            if (id == APP_DOWNLOAD_COUNT_ID)
            {
                _appDownloadCount = null;

                var value = AppDownloadCount;
            }
            else if (id == VERSION_DOWNLOAD_COUNT_ID)
            {
                _versionDownloadCount = null;

                var value = VersionDownloadCount;
            }
        }

        private static WebRegistry _bibleReaderNode;
        private static WebRegistry BibleReaderNode
        {
            get
            {
                if (_bibleReaderNode == null)
                    _bibleReaderNode = WebRegistry.SelectNode("/Apps/Integration/BibleReader");

                return _bibleReaderNode;
            }
        }

        private static int? _appDownloadCount = null;
        public static int AppDownloadCount
        {
            get
            {
                if (_appDownloadCount == null)
                {
                    var node = BibleReaderNode.SelectSingleNode("DefaultAppDownloadCount");
                    if (node != null)
                    {
                        _appDownloadCount = DataUtil.GetInt32(node.Value, 10);

                        APP_DOWNLOAD_COUNT_ID = node.Id;
                    }
                    else
                    {
                        _appDownloadCount = 10;
                    }
                }

                return _appDownloadCount.Value;
            }
        }

        private static int? _versionDownloadCount = null;
        public static int VersionDownloadCount
        {
            get
            {
                if (_versionDownloadCount == null)
                {
                    var node = BibleReaderNode.SelectSingleNode("DefaultVersionDownloadCount");
                    if (node != null)
                    {
                        _versionDownloadCount = DataUtil.GetInt32(node.Value, 10);

                        VERSION_DOWNLOAD_COUNT_ID = node.Id;
                    }
                    else
                    {
                        _versionDownloadCount = 10;
                    }
                }

                return _versionDownloadCount.Value;
            }
        }
    }
}
