using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common;
using WCMS.Common.Data;
using WCMS.Common.Utilities;

namespace WCMS.BibleReader.Core.Providers
{
    public class GenericBibleVerseProvider
    {
        private string connectionString;
        public GenericBibleVerseProvider()
        {
            connectionString = SqlHelper.GetConnectionString(BibleConstants.ConnectionString);
        }

        public BibleVerse Get(BibleVersion version, BibleBookName bookName, int chapterCode, int verseCode)
        {
            NamedValueProvider provider = new NamedValueProvider();
            provider.Add("VERSION_SHORT_NAME", version.ShortName);

            string verseSql = Substituter.Substitute(BibleConstants.VERSE_GET_SQL_TEMPLATE, provider);

            using (var r = SqlHelper.ExecuteReader(connectionString, CommandType.Text, verseSql,
                new SqlParameter("@BookCode", bookName.BookCode),
                new SqlParameter("@ChapterCode", chapterCode),
                new SqlParameter("@VerseCode", verseCode)
            ))
            {
                if (r.Read())
                {
                    BibleChapter chapter = new BibleChapter(version, bookName);
                    chapter.ChapterCode = DataUtil.GetId(r, "ChapterCode");

                    BibleVerse item = new BibleVerse(chapter);

                    item.VerseCode = DataUtil.GetId(r, "VerseCode");
                    item.CompositeCode = DataUtil.GetId(r, "CompositeCode");
                    item.Content = DataHelper.Get(r, "Content");
                    item.Id = DataUtil.GetId(r, "Id");

                    return item;
                }
            }

            return null;
        }

        public List<BibleVerse> GetList(BibleVersion version, BibleBookName bookName, int chapterCode)
        {
            NamedValueProvider provider = new NamedValueProvider();
            provider.Add("VERSION_SHORT_NAME", version.ShortName);

            string verseSql = Substituter.Substitute(BibleConstants.VERSE_GET_SQL_TEMPLATE, provider);

            List<BibleVerse> items = new List<BibleVerse>();

            using (var r = SqlHelper.ExecuteReader(connectionString, CommandType.Text, verseSql,
                new SqlParameter("@BookCode", bookName.BookCode),
                new SqlParameter("@ChapterCode", chapterCode),
                new SqlParameter("@VerseCode", -2)
            ))
            {
                while (r.Read())
                {
                    BibleChapter chapter = new BibleChapter(version, bookName);
                    chapter.ChapterCode = DataUtil.GetId(r, "ChapterCode");

                    BibleVerse item = new BibleVerse(chapter);

                    item.VerseCode = DataUtil.GetId(r, "VerseCode");
                    item.CompositeCode = DataUtil.GetId(r, "CompositeCode");
                    item.Content = DataHelper.Get(r, "Content");
                    item.Id = DataUtil.GetId(r, "Id");

                    items.Add(item);
                }
            }

            return items;
        }

        public List<BibleVerse> Search(BibleVersion version, BibleBookName bookName, string search)
        {
            var provider = new NamedValueProvider();
            provider.Add("VERSION_SHORT_NAME", version.ShortName);
            //provider.Add("SEARCH", search);

            var searchParam = string.Format("%{0}%", search);
            BibleBookName bkName = bookName;

            string verseSql = Substituter.Substitute(BibleConstants.VERSE_SEARCH_SQL_TEMPLATE, provider);
            var items = new List<BibleVerse>();

            using (var r = SqlHelper.ExecuteReader(connectionString, CommandType.Text, verseSql,
                new SqlParameter("@BookCode", bkName == null ? -2 : bkName.BookCode),
                new SqlParameter("@ChapterCode", -2),
                new SqlParameter("@Search", searchParam)
            ))
            {
                while (r.Read())
                {
                    var bookCode = DataUtil.GetId(r, "BookCode");
                    if (bkName == null || bkName.BookCode != bookCode)
                        bkName = BibleBookName.Provider.Get(version.BookNameCode, bookCode);

                    var chapter = new BibleChapter(version, bkName);
                    chapter.ChapterCode = DataUtil.GetId(r, "ChapterCode");

                    var item = new BibleVerse(chapter);
                    item.VerseCode = DataUtil.GetId(r, "VerseCode");
                    item.CompositeCode = DataUtil.GetId(r, "CompositeCode");
                    item.Content = DataHelper.Get(r, "Content");
                    item.Id = DataUtil.GetId(r, "Id");

                    items.Add(item);
                }
            }

            return items;
        }
    }
}
