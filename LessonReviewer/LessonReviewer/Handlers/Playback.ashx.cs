using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Common.Media;
using WCMS.WebSystem.Apps.Integration.Net;

using WCMS.LessonReviewer.Core;

namespace WCMS.LessonReviewer.Handers
{
    /// <summary>
    /// Summary description for Playback
    /// </summary>
    public class Playback : IHttpHandler
    {
        // var mediaSegments={"SegmentCount":3,"Segments":[{"Index":0,"Caption":"Part 1"},{"Index":3,"Caption":"Part 2"},{"Index":5,"Caption":"Part 3"}]};
        private const string SEGMENT_MAIN = "var $(SEGMENT_VAR)={\"CurrentSegment\":$(CURRENT_SEGMENT),\"Segments\":[$(SEGMENT_ITEMS)]};";
        private const string SEGMENT_ITEM = "{\"Index\":$(SEGMENT_INDEX),\"Caption\":\"$(SEGMENT_CAPTION)\",\"Number\":\"-PART$(SEGMENT_NUMBER)-\"},";

        public void ProcessRequest(HttpContext context)
        {
            var query = new QueryParser(context);

            var method = query.Get("Method");
            var serviceType = query.Get("ServiceType");
            var dateOrFolder = query.Get("Date");
            var language = query.Get("Language");

            var baseFolder = ConfigManager.Get("MCGI.MakeUp.BaseFolder");
            var baseHttp = ConfigManager.Get("MCGI.MakeUp.BaseHttp");

            bool hasResponse = false;

            if (string.IsNullOrEmpty(language))
                language = PlaybackLanguages.Neutral;

            if (!string.IsNullOrEmpty(baseFolder) && !string.IsNullOrEmpty(serviceType) && !string.IsNullOrEmpty(dateOrFolder))
            {
                IOrderedEnumerable<PlaybackFile> playFiles = null;
                List<PlaybackSegment> segments = null;
                PlaybackMasterList playbackList = null;

                var serviceDef = PlaybackHelper.GetService(serviceType);
                var allowSegments = serviceDef != null ? serviceDef.AllowSegmented : false;

                baseFolder = WebHelper.MapPath(baseFolder, true);

                var serviceFolder = FileHelper.Combine(baseFolder, string.Format("{0}\\{1}", serviceType, dateOrFolder), '\\');
                var videoExists = Directory.Exists(serviceFolder);

                Func<IOrderedEnumerable<PlaybackFile>> FetchPlaybackFiles = () =>
                {
                    if (videoExists)
                    {
                        var files = Directory.GetFiles(serviceFolder);
                        if (files.Count() > 0)
                        {
                            playbackList = new PlaybackMasterList(serviceType, dateOrFolder, baseHttp);

                            foreach (var file in files)
                                if (Path.GetExtension(file).IndexOf(".db", StringComparison.InvariantCultureIgnoreCase) < 0)
                                    playbackList.Add(Path.GetFileName(file));

                            return from file in playbackList.GetFiles(language)
                                   orderby file.SegmentCode
                                   orderby file.SequenceNo
                                   select file;
                        }
                    }

                    return null;
                };

                if (!string.IsNullOrEmpty(method))
                {
                    var varName = query.Get("VarName");
                    if (!string.IsNullOrEmpty(varName))
                    {
                        context.Response.ContentType = "text/javascript";
                        context.Response.Write(string.Format("var {0} = \"{1}\";", varName, videoExists ? "OK" : "FAILED"));
                        if (allowSegments && method.Equals("InitPlayback", StringComparison.InvariantCultureIgnoreCase))
                        {
                            // Setup Segments
                            var segmentVarName = query.Get("SegmentVarName");
                            var play = query.Get("Play");

                            playFiles = FetchPlaybackFiles();
                            if (playFiles != null && playFiles.Count() > 0)
                            {
                                segments = playbackList.GetUniqueSegments(playFiles);
                                if (segments.Count > 1)
                                {
                                    var sb = new StringBuilder();
                                    NamedValueProvider provider = null;
                                    var segmentNumber = -1;
                                    if (allowSegments && !string.IsNullOrEmpty(play) && play.StartsWith("-PART") && play.EndsWith("-"))
                                        segmentNumber = DataHelper.GetInt32(play.Substring(5, play.Length - 6));

                                    // Build Segment Items first
                                    foreach (var segment in segments)
                                    {
                                        provider = new NamedValueProvider();
                                        provider.Add("SEGMENT_INDEX", segment.MediaIndex);
                                        provider.Add("SEGMENT_CAPTION", segment.GetDisplayText());
                                        provider.Add("SEGMENT_NUMBER", segment.GetSegmentNumber());

                                        sb.Append(Substituter.Substitute(SEGMENT_ITEM, provider));
                                    }

                                    sb.Remove(sb.Length - 1, 1);

                                    provider = new NamedValueProvider();
                                    provider.Add("SEGMENT_VAR", segmentVarName);
                                    provider.Add("CURRENT_SEGMENT", segmentNumber);
                                    provider.Add("SEGMENT_ITEMS", sb);

                                    context.Response.Write(Substituter.Substitute(SEGMENT_MAIN, provider));
                                }
                            }
                        }
                    }
                    else
                    {
                        context.Response.ContentType = "text/plain";
                        context.Response.Write(videoExists ? "OK" : "FAILED");
                    }

                    hasResponse = true;
                }
                else
                {
                    context.Response.ContentType = "video/x-ms-asf";

                    if (serviceDef.InstancesByDate)
                    {
                        // Instances By Date

                        if (playFiles == null)
                            playFiles = FetchPlaybackFiles();

                        if (playFiles != null && playFiles.Count() > 0)
                        {
                            var fetchMode = query.GetBool("FetchMode", false);
                            var play = query.Get("Play");
                            var segmentNumber = -1;
                            var allowSeek = serviceDef != null ? serviceDef.AllowSeek : true;

                            if (allowSegments && segments == null)
                                segments = playbackList.GetUniqueSegments(playFiles);

                            allowSegments = segments != null && segments.Count > 1;
                            if (allowSegments && !string.IsNullOrEmpty(play) && play.StartsWith("-PART") && play.EndsWith("-"))
                                segmentNumber = DataHelper.GetInt32(play.Substring(5, play.Length - 6));

                            var playlist = new AsxMedia();
                            for (var i = 0; i < playFiles.Count(); i++)
                            {
                                var playFile = playFiles.ElementAt(i);
                                if (segmentNumber <= 0 || playFile.GetSegmentNumber() == segmentNumber)
                                {
                                    var isSegmentIndex = allowSegments && segments.Find(seg => seg.MediaIndex == i) != null;
                                    playlist.Entries.Add(new AsxEntry(playFile.BuildHttpUrl())
                                        {
                                            Title = playFile.EvalFilename,
                                            Copyright = string.Format("Copyright (C) {0}, Church of God International", DateTime.Now.Year),
                                            CanSeek = true, //fetchMode || (allowSegments ? false : allowSeek),
                                            CanSkipForward = true, //fetchMode || (allowSegments ? false : allowSeek),
                                            CanSkipBack = true // fetchMode || (allowSegments && isSegmentIndex ? false : true)
                                        });
                                }
                            }

                            hasResponse = true;

                            context.Response.Write(playlist.ToXmlString());
                        }
                    }
                    else
                    {
                        // Custom folders

                        var play = query.Get("Play");

                        var folders = Directory.GetDirectories(serviceFolder);
                        if (folders.Count() > 0)
                        {
                            if (!string.IsNullOrEmpty(language))
                            {
                                foreach (var folder in folders)
                                {
                                    var folderLang = Path.GetFileName(folder);
                                    if (PlaybackLanguages.Values.ContainsKey(folderLang) && folderLang.Equals(language, StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        var files = !string.IsNullOrEmpty(play) ? Directory.GetFiles(folder, play) : Directory.GetFiles(folder);
                                        if (files.Count() > 0)
                                        {
                                            var playlist = new AsxMedia();
                                            foreach (var file in files)
                                            {
                                                var fileName = Path.GetFileName(file);
                                                var fileHttpUrl = string.Format("{0}/{1}/{2}/{3}/{4}", baseHttp, serviceDef.Value, dateOrFolder, folderLang, fileName);
                                                playlist.Entries.Add(new AsxEntry(fileHttpUrl)
                                                {
                                                    Title = fileName,
                                                    Copyright = string.Format("Copyright (C) {0}, Church of God International", DateTime.Now.Year),
                                                    CanSeek = true, //serviceDef.AllowSeek,
                                                    CanSkipForward = true, //serviceDef.AllowSeek,
                                                    CanSkipBack = true
                                                });
                                            }

                                            hasResponse = true;
                                            context.Response.Write(playlist.ToXmlString());
                                        }

                                        break;
                                    }
                                }
                            }
                        }

                        if (!hasResponse)
                        {
                            // No Language folders
                            var files = Directory.GetFiles(serviceFolder);
                            if (files.Count() > 0)
                            {
                                playbackList = new PlaybackMasterList(serviceDef.Value, dateOrFolder, baseHttp);
                                foreach (var file in files)
                                    playbackList.Add(Path.GetFileName(file));

                                var pbFiles = playbackList.GetFiles(language);
                                if (pbFiles.Count() > 0)
                                {
                                    var playlist = new AsxMedia();
                                    for (var i = 0; i < pbFiles.Count(); i++)
                                    {
                                        var playFile = pbFiles.ElementAt(i);
                                        if (string.IsNullOrEmpty(play) || play.Equals(playFile.Filename))
                                        {
                                            var fileHttpUrl = string.Format("{0}/{1}/{2}/{3}", baseHttp, serviceDef.Value, dateOrFolder, playFile.Filename);
                                            playlist.Entries.Add(new AsxEntry(fileHttpUrl)
                                            {
                                                Title = playFile.EvalFilename,
                                                Copyright = string.Format("Copyright (C) {0}, Church of God International", DateTime.Now.Year),
                                                CanSeek = true, //serviceDef.AllowSeek,
                                                CanSkipForward = true, // serviceDef.AllowSeek,
                                                CanSkipBack = true
                                            });
                                        }
                                    }

                                    hasResponse = true;

                                    context.Response.Write(playlist.ToXmlString());
                                }
                            }
                        }
                    }
                }
            }

            if (!hasResponse)
            {
                if (string.IsNullOrEmpty(method))
                {
                    var asx = new AsxMedia();
                    context.Response.Write(asx.ToXmlString());
                }
                else
                {
                    var varName = query.Get("VarName");
                    if (!string.IsNullOrEmpty(varName))
                    {
                        context.Response.ContentType = "text/javascript";
                        context.Response.Write(string.Format("var {0} = \"FAILED\";", varName));
                    }
                    else
                    {
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("FAILED");
                    }
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