using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Common.Media
{
    public class AsxMedia
    {
        public AsxMedia()
        {
            Entries = new List<AsxEntry>();
        }

        public List<AsxEntry> Entries { get; set; }

        public string ToXmlString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<asx version=\"3.0\">");

            foreach (var entry in Entries)
            {
                sb.Append("<entry>");

                if (!string.IsNullOrEmpty(entry.Title))
                    sb.AppendFormat("<title>{0}</title>", entry.Title);

                if (!string.IsNullOrEmpty(entry.Author))
                    sb.AppendFormat("<author>{0}</author>", entry.Author);

                if (!string.IsNullOrEmpty(entry.Copyright))
                    sb.AppendFormat("<copyright>{0}</copyright>", entry.Copyright);

                if (entry.RefNodes.Count > 0)
                    foreach (var refNode in entry.RefNodes)
                        sb.AppendFormat("<ref href=\"{0}\" />", refNode.Href);

                // etc
                sb.AppendFormat("<PARAM NAME=\"CanSkipBack\" VALUE=\"{0}\" />", entry.CanSkipBack ? "Yes" : "No");
                sb.AppendFormat("<PARAM NAME=\"CanSkipForward\" VALUE=\"{0}\" />", entry.CanSkipForward ? "Yes" : "No");
                sb.AppendFormat("<PARAM NAME=\"CanSeek\" VALUE=\"{0}\" />", entry.CanSeek ? "Yes" : "No");

                sb.Append("</entry>");
            }

            sb.Append("</asx>");

            return sb.ToString();
        }
    }

    public class AsxEntry
    {
        public AsxEntry()
        {
            RefNodes = new List<AsxRefNode>();

            CanSkipBack = true;
            CanSkipForward = true;
            CanSeek = true;
        }

        public AsxEntry(string href)
            : this()
        {
            RefNodes.Add(new AsxRefNode(href));
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Copyright { get; set; }
        public bool CanSkipBack { get; set; }
        public bool CanSkipForward { get; set; }
        public bool CanSeek { get; set; }

        public List<AsxRefNode> RefNodes { get; set; }
    }

    public class AsxRefNode
    {
        private AsxRefNode() { }

        public AsxRefNode(string href)
        {
            this.Href = href;
        }

        public string Href { get; set; }
    }
}
