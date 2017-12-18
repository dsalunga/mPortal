CREATE PROCEDURE dbo.Menu_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM Menu
		WHERE Id=@Id

	RETURN