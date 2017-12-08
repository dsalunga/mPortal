using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Data;

using WCMS.BibleReader.Core.Providers;

namespace WCMS.BibleReader.Core
{
    public class BibleVerse : DataObject
    {
        private static GenericBibleVerseProvider _provider;

        static BibleVerse()
        {
            _provider = new GenericBibleVerseProvider();
        }

        public BibleVerse() { }

        public BibleVerse(BibleChapter chapter)
        {
            Chapter = chapter;
        }

        public string Content { get; set; }
        public int VerseCode { get; set; }
        public int CompositeCode { get; set; }

        public BibleChapter Chapter { get; set; }

        public static GenericBibleVerseProvider Provider { get { return _provider; } }

        #region Verse Navigation

        public BibleVerse GetPreviousVerse()
        {
            /// TODO: add implementation

            return null;
        }

        public BibleVerse GetNextVerse()
        {
            /// TODO: add implementation
            
            return null;
        }

        #endregion
    }
}
