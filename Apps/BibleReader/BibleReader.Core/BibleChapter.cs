using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.BibleReader.Core
{
    public class BibleChapter
    {
        public BibleChapter() { }

        public BibleChapter(BibleVersion version, BibleBookName bookName)
        {
            this.Book = new BibleBook(version, bookName);
        }

        public int ChapterCode { get; set; }

        public BibleBook Book { get; set; }
        

        #region Verse Navigation

        public BibleVerse GetFirstVerse()
        {
            /// TODO: add implementation
            
            return null;
        }

        public BibleVerse GetLastVerse()
        {
            /// TODO: add implementation
            
            return null;
        }

        public BibleVerse GetVerse(int verseCode)
        {
            /// TODO: add implementation
            
            return null;
        }

        public List<BibleVerse> GetVerseRange(int verseCodeStart, int verseCodeEnd)
        {
            /// TODO: add implementation

            return null;
        }

        #endregion

        #region Chapter Navigation

        public BibleChapter GetPreviousChapter()
        {
            /// TODO: add implementation
            
            return null;
        }

        public BibleChapter GetNextChapter()
        {
            /// TODO: add implementation
            
            return null;
        }

        #endregion
    }
}
