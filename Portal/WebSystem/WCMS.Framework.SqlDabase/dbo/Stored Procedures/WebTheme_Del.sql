CREATE PROCEDURE dbo.WebTheme_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM WebTheme
		WHERE Id=@Id;

	RETURN