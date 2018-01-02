CREATE PROCEDURE dbo.BibleBookName_Get
	(
		@Id int = -1,
		@BookNameCode int = -2,
		@BookCode INT = -2
	)
AS
	SET NOCOUNT ON

	SELECT        BookNameCode, BookCode, Name, MaxChapter, Id, ShortName
	FROM            BibleBookName
	WHERE        (@Id=-1 OR Id = @Id)
		AND (@BookNameCode =-2 OR BookNameCode=@BookNameCode)
		AND (@BookCode = -2 OR BookCode = @BookCode)
	ORDER BY BookNameCode, BookCode

	RETURN