using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

using WCMS.Common.Media;
using WCMS.Common.Utilities;

using WCMS.WebSystem.Apps.Integration.Registration.Net;

namespace WCMS.WebSystem.WebParts.Profile.LessonReviewer
{
    /// <summary>
    /// Summary description for Playback1
    /// </summary>
    public class PlaybackHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            QueryParser query = new QueryParser(context);

            var action = query.Get("Action");
            var serviceType = query.Get("ServiceType");
            var serviceDate = query.Get("Date");
            var language = query.Get("Language");

            var baseFolder = ConfigHelper.Get("Integration.LessonReviewer.BaseFolder");
            var baseHttp = ConfigHelper.Get("Integration.LessonReviewer.BaseHttp");

            bool hasResponse = false;

            if (string.IsNullOrEmpty(language))
                language = PlaybackLanguages.Tagalog;

            if (!string.IsNullOrEmpty(baseFolder) && !string.IsNullOrEmpty(serviceType) && !string.IsNullOrEmpty(serviceDate))
            {
                var serviceFolder = FileHelper.Combine(baseFolder, string.Format("{0}\\{1}", serviceType, serviceDate), '\\');
                var videoExists = Directory.Exists(serviceFolder);

                if (!string.IsNullOrEmpty(action) && action.Equals("Verify", StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(videoExists ? "OK" : "FAILED");

                    hasResponse = true;
                }
                else
                {
                    context.Response.ContentType = "video/x-ms-asf";

                    if (videoExists)
                    {
                        var files = Directory.GetFiles(serviceFolder);
                        if (files.Length > 0)
                        {
                            PlaybackMasterList playbackList = new PlaybackMasterList(serviceType, serviceDate, baseHttp);

                            foreach (var file in files)
                            {
                                playbackList.Add(Path.GetFileName(file));
                            }

                            var pbFiles = playbackList.GetFiles(language);
                            if (pbFiles.Count > 0)
                            {
                                AsxMedia asx = new AsxMedia();
                                foreach (var pbFile in pbFiles)
                                {
                                    asx.Entries.Add(
                                        new AsxEntry(pbFile.BuildHttpUrl())
                                        {
                                            Title = pbFile.EvalFilename,
                                            Copyright = "(c)Group of God International"
                                        });
                                }

                                hasResponse = true;

                                context.Response.Write(asx.ToXmlString());
                            }
                        }
                    }
                }
            }

            if (!hasResponse)
            {
                if (string.IsNullOrEmpty(action))
                {
                    AsxMedia asx = new AsxMedia();
                    context.Response.Write(asx.ToXmlString());
                }
                else
                {
                    context.Response.Write("FAILED");
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}