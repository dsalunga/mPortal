CREATE PROCEDURE dbo.BibleReaderVersionAccess_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM BibleReaderVersionAccess
		WHERE Id=@Id;

	RETURN