using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.Apps.Integration.Net
{
    public class PlaybackSegment
    {
        public PlaybackSegment(int mediaIndex, int sequenceNo, string segmentCode, string description)
        {
            this.MediaIndex = mediaIndex;
            this.SequenceNo = sequenceNo;
            this.SegmentCode = segmentCode;
            this.Description = description;
        }

        public int MediaIndex { get; set; }
        public int SequenceNo { get; set; }
        public string SegmentCode { get; set; }
        public string Description { get; set; }

        public int GetSegmentNumber()
        {
            return char.ConvertToUtf32(SegmentCode, 0) - char.ConvertToUtf32("A", 0) + 1;
        }

        public string GetDisplayText()
        {
            if (!string.IsNullOrEmpty(Description))
                return string.Format("Part {0} - {1}", GetSegmentNumber(), Description);
            else
                return string.Format("Part {0}", GetSegmentNumber());
        }
    }
}
