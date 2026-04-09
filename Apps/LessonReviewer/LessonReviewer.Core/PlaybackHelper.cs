using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using WCMS.Common.Utilities;
using WCMS.WebSystem.Apps.Integration.Net;

namespace WCMS.LessonReviewer.Core
{
    public abstract class PlaybackHelper
    {
        public static ServiceDefinition GetService(string value)
        {
            var xmlPath = PathMapper.MapPath("~/App_Data/Services.xml");
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);

            XmlNode serviceNode = xdoc.SelectSingleNode(string.Format("//Service[@Value='{0}']", value));

            var service = new ServiceDefinition();
            service.Value = XmlUtil.GetValue(serviceNode, "Value");
            service.Text = XmlUtil.GetValue(serviceNode, "Text");
            service.AllowSeek = DataUtil.GetBool(XmlUtil.GetValue(serviceNode, "AllowSeek"), true);
            service.AllowSegmented = DataUtil.GetBool(XmlUtil.GetValue(serviceNode, "AllowSegmented"), false);
            service.InstancesByDate = DataUtil.GetBool(XmlUtil.GetValue(serviceNode, "InstancesByDate"), true);

            return service;
        }

        public static ServiceDefinition GetService(XmlNode serviceNode)
        {
            var service = new ServiceDefinition();
            service.Value = XmlUtil.GetValue(serviceNode, "Value");
            service.Text = XmlUtil.GetValue(serviceNode, "Text");
            service.AllowSeek = DataUtil.GetBool(XmlUtil.GetValue(serviceNode, "AllowSeek"), true);
            service.AllowSegmented = DataUtil.GetBool(XmlUtil.GetValue(serviceNode, "AllowSegmented"), false);
            service.InstancesByDate = DataUtil.GetBool(XmlUtil.GetValue(serviceNode, "InstancesByDate"), true);

            return service;
        }

        public static bool HasFiles(QueryParser query)
        {
            var serviceType = query.Get("ServiceType");
            var serviceDate = query.Get("Date");
            var language = query.Get("Language");

            var baseFolder = ConfigManager.Get("MCGI.MakeUp.BaseFolder");
            var baseHttp = ConfigManager.Get("MCGI.MakeUp.BaseHttp");

            bool hasResponse = false;

            if (string.IsNullOrEmpty(language))
                language = PlaybackLanguages.Neutral;

            if (!string.IsNullOrEmpty(baseFolder) && !string.IsNullOrEmpty(serviceType) && !string.IsNullOrEmpty(serviceDate))
            {
                baseFolder = WebHelper.MapPath(baseFolder);

                var serviceFolder = FileHelper.Combine(baseFolder, string.Format("{0}\\{1}", serviceType, serviceDate), '\\');
                var videoExists = Directory.Exists(serviceFolder);

                if (videoExists)
                {
                    var files = Directory.GetFiles(serviceFolder);
                    if (files.Length > 0)
                    {
                        PlaybackMasterList playbackList = new PlaybackMasterList(serviceType, serviceDate, baseHttp);

                        foreach (var file in files)
                            playbackList.Add(Path.GetFileName(file));

                        var pbFiles = playbackList.GetFiles(language);
                        if (pbFiles.Count() > 0)
                            hasResponse = true;
                    }
                }
            }

            return hasResponse;
        }
    }
}
