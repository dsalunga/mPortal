using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.BibleReader.Core
{
    public class BibleConstants
    {
        public const string ConnectionString = "BibleConnection";

        public const string VERSE_GET_SQL_TEMPLATE =    @"SELECT [CompositeCode]
                                                                ,[Content]
                                                                ,[Id]
                                                                ,[BookCode]
                                                                ,[ChapterCode]
                                                                ,[VerseCode]
                                                        FROM [dbo].[BIBLE_$(VERSION_SHORT_NAME)]
                                                        WHERE
		                                                        BookCode	=@BookCode
	                                                        AND	ChapterCode	=@ChapterCode
	                                                        AND (@VerseCode=-2 OR VerseCode=@VerseCode);";

        public const string VERSE_SEARCH_SQL_TEMPLATE = @"SELECT TOP 50 [CompositeCode]
                                                                ,[Content]
                                                                ,[Id]
                                                                ,[BookCode]
                                                                ,[ChapterCode]
                                                                ,[VerseCode]
                                                        FROM [dbo].[BIBLE_$(VERSION_SHORT_NAME)]
                                                        WHERE
		                                                        (@BookCode=-2 OR BookCode=@BookCode)
	                                                        AND	(@ChapterCode=-2 OR ChapterCode=@ChapterCode)
                                                            AND (Content LIKE @Search);";
    }
}
