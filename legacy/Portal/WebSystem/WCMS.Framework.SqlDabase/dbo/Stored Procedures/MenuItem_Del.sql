CREATE PROCEDURE dbo.MenuItem_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM MenuItem
		WHERE Id=@Id;

	RETURN