using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.Apps.Integration.Net
{
    /// <summary>
    /// => MasterList => Sequence, FileList
    /// </summary>
    public class PlaybackMasterList
    {
        public string ServiceType { get; set; }
        public string ServiceDateString { get; set; }
        public string BaseHttp { get; set; }

        public int LastNullSequence { get; set; }

        private PlaybackMasterList()
        {
            MasterList = new List<PlaybackSequenceList>();

            LastNullSequence = -1;
        }

        public PlaybackMasterList(string serviceType, string serviceDate, string baseHttp)
            : this()
        {
            this.ServiceType = serviceType;
            this.ServiceDateString = serviceDate;
            this.BaseHttp = baseHttp;
        }

        public void Add(PlaybackFile file)
        {
            var seqList = MasterList.Find(i => i.SequenceNo == file.SequenceNo);
            if (seqList == null)
            {
                seqList = new PlaybackSequenceList(file.SequenceNo);
                MasterList.Add(seqList);
            }
            else
            {
                var existingFile = seqList.Files.Find(i => i.Filename.Equals(file.Filename, StringComparison.InvariantCultureIgnoreCase));
                if (existingFile != null)
                    return;
            }

            seqList.Files.Add(file);
        }

        public void Add(string fileName)
        {
            var file = new PlaybackFile(this, fileName);
            if (file != null)
                Add(file);
        }

        public IOrderedEnumerable<PlaybackFile> GetFiles(string language)
        {
            List<PlaybackFile> items = new List<PlaybackFile>();

            foreach (var list in MasterList)
            {
                var files = list.Files;
                if (files.Count > 0)
                {
                    var file = files.Find(i => i.Language.Equals(PlaybackLanguages.Neutral) || i.Language.Equals(language, StringComparison.InvariantCultureIgnoreCase));
                    if (file != null)
                        items.Add(file);
                }
            }

            return items.OrderBy(i => i.SequenceNo);
        }

        public List<PlaybackSegment> GetUniqueSegments(string language)
        {
            var files = from file in GetFiles(language)
                        orderby file.SegmentCode
                        orderby file.SequenceNo
                        select file;

            return GetUniqueSegments(files);
        }

        public List<PlaybackSegment> GetUniqueSegments(IOrderedEnumerable<PlaybackFile> files)
        {
            List<PlaybackSegment> segments = new List<PlaybackSegment>();

            string currentSegment = null;
            for (int i = 0; i < files.Count(); i++)
            {
                var file = files.ElementAt(i);
                if (file.SegmentCode != currentSegment)
                {
                    currentSegment = file.SegmentCode;
                    if (segments.Find(segment => segment.SegmentCode == file.SegmentCode) == null)
                        segments.Add(new PlaybackSegment(i, file.SequenceNo, file.SegmentCode, file.Caption));
                }
            }

            return segments;
        }

        internal List<PlaybackSequenceList> MasterList { get; set; }

        #region Inner Class

        internal class PlaybackSequenceList
        {
            public PlaybackSequenceList()
            {
                Files = new List<PlaybackFile>();
            }

            public PlaybackSequenceList(int sequence)
                : this()
            {
                this.SequenceNo = sequence;
            }

            public int SequenceNo { get; set; }
            internal List<PlaybackFile> Files { get; set; }
        }

        #endregion
    }
}
