using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.Apps.BranchLocator
{
    public class MChapterAnnouncement
    {
        public JObject Content { get; set; }

        public MChapterAnnouncement()
        {
            Content = new JObject();
        }

        public MChapterAnnouncement(JObject announce)
        {
            Content = announce == null ? new JObject() : announce;
            Init();
        }
        public MChapterAnnouncement(string json)
        {
            Content = string.IsNullOrEmpty(json) ? new JObject() : JObject.Parse(json);
            Init();
        }

        private void Init()
        {
            Enabled = DataUtil.GetBool(Content["enabled"], true);

            var slides = Content.Value<string>("slides");
            Slides = string.IsNullOrEmpty(slides) ? "" : slides;

            var lastUpdateToken = Content["lastUpdate"];
            LastUpdate = lastUpdateToken == null ? WConstants.DateTimeMinValue : DataUtil.GetDateTime(lastUpdateToken.Value<string>());
        }

        private string slides = "";
        public string Slides
        {
            get
            {
                return slides;
            }

            set
            {
                Content["slides"] = value;
                slides = value;
            }
        }

        private DateTime lastUpdate = WConstants.DateTimeMinValue;
        public DateTime LastUpdate
        {
            get
            {
                return lastUpdate;
            }

            set
            {
                Content["lastUpdate"] = TimeUtil.ToISOString(value);
                lastUpdate = value;
            }
        }

        private bool enabled = true;
        public bool Enabled
        {
            get
            {
                return enabled;
            }

            set
            {
                Content["enabled"] = value;
                enabled = value;
            }
        }
    }

    public class MChapterExtra
    {
        public JObject Content { get; set; }
        public MChapterExtra(string extra)
        {
            Content = string.IsNullOrEmpty(extra) ? new JObject() : JObject.Parse(extra);
            Announcement = new MChapterAnnouncement(Content["announcements"] as JObject);
        }

        public MChapterAnnouncement Announcement { get; set; }

        public override string ToString()
        {
            Content["announcements"] = Announcement.Content;
            return Content.ToString();
        }
    }
}
