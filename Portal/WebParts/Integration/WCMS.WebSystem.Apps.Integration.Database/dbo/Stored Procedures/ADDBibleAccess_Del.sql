CREATE PROCEDURE dbo.BibleReaderAccess_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM BibleReaderAccess
		WHERE Id=@Id

	RETURN