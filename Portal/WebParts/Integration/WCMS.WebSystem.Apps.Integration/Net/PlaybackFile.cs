using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.Apps.Integration.Net
{
    public class PlaybackFile
    {
        private const int NULL_SEQ = 9999;
        private const string META_NFWD = "-NFWD";
        private const string META_FWD = "-FWD";

        private const string CAPTION_START = "-[";
        private const string CAPTION_END = "]";

        public static readonly string DefaultLanguage = PlaybackLanguages.Tagalog;

        public PlaybackFile()
        {
            Language = PlaybackLanguages.Neutral;
            Caption = string.Empty;
            SegmentCode = "A"; //string.Empty;
        }

        public PlaybackFile(PlaybackMasterList master, string fileName)
            : this()
        {
            this.Master = master;

            var name = Path.GetFileNameWithoutExtension(fileName);

            var dashIndex = name.LastIndexOf('-');
            var undscrIndex = name.LastIndexOf('_');

            Action<int> ExtraMetaInfo = (metaIndex) =>
            {
                var meta = name.Substring(metaIndex + 1);

                if (meta.Contains('-'))
                {
                    // Has multiple meta-codes
                    if (meta.IndexOf(META_FWD, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        this.FWD = true;
                        meta = meta.Replace(META_FWD, string.Empty);
                    }

                    if (meta.IndexOf(META_NFWD, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        this.NFWD = true;
                        meta = meta.Replace(META_NFWD, string.Empty);
                    }

                    var captionStartIndex = meta.IndexOf(CAPTION_START);
                    var captionEndIndex = meta.IndexOf(CAPTION_END);
                    if (captionStartIndex >= 0 && captionStartIndex < captionEndIndex)
                    {
                        var captionTemp = meta.Substring(captionStartIndex, captionEndIndex - captionStartIndex + 1);

                        this.Caption = captionTemp.Substring(CAPTION_START.Length, captionTemp.Length - CAPTION_START.Length - CAPTION_END.Length);
                        meta = meta.Replace(captionTemp, string.Empty);
                    }

                    dashIndex = meta.IndexOf('-');
                    undscrIndex = meta.IndexOf('_');

                    string[] metaArray;
                    if (dashIndex > undscrIndex)
                        metaArray = meta.Split('-');
                    else
                        metaArray = meta.Split('_');

                    var lang = metaArray.First().Trim().ToUpper();
                    if (lang.Length == 2)
                        this.Language = lang;

                    var seqCode = metaArray[1].Trim().ToUpper();
                    if (seqCode.Length > 1 && seqCode.Length < 4)
                    {
                        if (char.IsLetter(seqCode[seqCode.Length - 1]))
                        {
                            this.SegmentCode = seqCode.Substring(seqCode.Length - 1);
                            seqCode = seqCode.Substring(0, seqCode.Length - 1);
                        }

                        this.SequenceNo = DataUtil.GetInt32(seqCode);
                    }
                    else
                    {
                        this.SequenceNo = NULL_SEQ;
                    }
                }
                else
                {
                    // One meta-code only (probably a seq or language)

                    if (meta.Length > 1 && meta.Length < 4)
                    {
                        var sequenceNo = DataUtil.GetInt32(meta);
                        if (sequenceNo <= 0)
                        {
                            this.SequenceNo = NULL_SEQ;

                            if (PlaybackLanguages.Values.ContainsKey(meta))
                                this.Language = meta;
                        }
                        else
                        {
                            this.SequenceNo = sequenceNo;
                        }
                    }
                    else
                    {
                        this.SequenceNo = NULL_SEQ;
                    }
                }
            };

            if (undscrIndex >= 0)
            {
                // Has underscore
                ExtraMetaInfo(undscrIndex);
            }
            //else if (dashIndex >= 0)
            //{
            //    // No underscore but has dash
            //    ExtraMetaInfo(dashIndex);
            //}
            else
            {
                this.SequenceNo = NULL_SEQ;
            }

            this.Filename = fileName;
            this.EvalFilename = name + Path.GetExtension(fileName);
        }

        public string BuildHttpUrl()
        {
            return FileHelper.Combine(Master.BaseHttp, '/', string.Format("{0}/{1}/{2}", Master.ServiceType, Master.ServiceDateString, this.Filename));
        }

        public string Filename { get; set; }
        public string EvalFilename { get; set; }
        public string Language { get; set; }

        private int _sequenceNo;
        public int SequenceNo
        {
            get { return _sequenceNo; }

            set
            {
                if (value >= NULL_SEQ)
                {
                    if (Master.LastNullSequence >= 0)
                        Master.LastNullSequence += 1;
                    else
                        Master.LastNullSequence = value;

                    _sequenceNo = Master.LastNullSequence;
                }
                else
                {
                    _sequenceNo = value;
                }
            }
        }

        public int GetSegmentNumber()
        {
            if (!string.IsNullOrEmpty(SegmentCode))
                return char.ConvertToUtf32(SegmentCode, 0) - char.ConvertToUtf32("A", 0) + 1;

            return -1;
        }

        public PlaybackMasterList Master { get; set; }

        //public bool CanSeek { get; set; }
        //public bool CanCanForward { get; set; }
        //public bool CanSkipBack { get; set; }
        public bool NFWD { get; set; }
        public bool FWD { get; set; }
        public string SegmentCode { get; set; }

        public string Caption { get; set; }
    }
}
