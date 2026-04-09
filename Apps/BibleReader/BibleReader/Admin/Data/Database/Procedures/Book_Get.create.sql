
-- Procedure Book_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.Book_Get
	(
		@Id int = -1,
		@BookCode int = -1,
		@TranslationCode int = -1
	)
AS
	SET NOCOUNT ON

	SELECT        Id, BookCode, TranslationCode, Name
	FROM            Book
	WHERE        (@Id=-1 OR Id = @Id) 
			AND (@BookCode=-1 OR BookCode = @BookCode) 
			AND (@TranslationCode=-1 OR TranslationCode = @TranslationCode)

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

