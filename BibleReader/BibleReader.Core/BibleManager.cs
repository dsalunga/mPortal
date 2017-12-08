using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.BibleReader.Core
{
    public class BibleManager
    {
        private static List<BibleVersion> _versions;
        private static List<BibleBookName> _books;

        static BibleManager()
        {
            _versions = BibleVersion.Provider.GetList();
            _books = BibleBookName.Provider.GetList();
        }

        public BibleManager()
        {

        }

        public static List<BibleVersion> Versions { get { return _versions; } }
        public static List<BibleBookName> BookNames { get { return _books; } }

        public static List<BibleVerse> GetVerseRange()
        {
            /// TODO: add implementation

            return new List<BibleVerse>();
        }

        public static BibleVerse GetVerse(string versionShortName, int bookCode, int chapterCode, int verseCode)
        {
            var version = _versions.Find(i => i.ShortName.Equals(versionShortName, StringComparison.InvariantCultureIgnoreCase));
            if (version != null)
            {
                var book = _books.Find(i => i.BookNameCode == version.BookNameCode && i.BookCode == bookCode);
                if (book != null)
                {
                    var verse = BibleVerse.Provider.Get(version, book, chapterCode, verseCode);
                    if (verse != null)
                        return verse;
                }
            }

            return null;
        }

        public static string GetVerseContent(string versionShortName, int bookCode, int chapterCode, int verseCode)
        {
            var verse = GetVerse(versionShortName, bookCode, chapterCode, verseCode);
            if (verse != null)
                return verse.Content;

            return null;
        }
    }
}
