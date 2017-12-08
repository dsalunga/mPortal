using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.BibleReader.Core
{
    /// <summary>
    /// Does not represent a table entity. Please do not confuse with BibleBook table,
    /// which is represented by BibleBookName.
    /// </summary>
    public class BibleBook
    {
        public BibleBook(BibleVersion version, BibleBookName bookName)
        {
            this.BookName = bookName;
            this.Version = version;
        }

        public BibleBookName BookName { get; set; }
        public BibleVersion Version { get; set; }

        public BibleChapter GetFirstChapter()
        {
            /// TODO: add implementation

            return null;
        }

        public BibleChapter GetLastChapter()
        {
            /// TODO: add implementation

            return null;
        }

        public BibleBook GetNextBook()
        {
            /// TODO: add implementation

            return null;
        }

        public BibleBook GetPreviousBook()
        {
            /// TODO: add implementation

            return null;
        }
    }
}
