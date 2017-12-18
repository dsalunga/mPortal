CREATE PROCEDURE dbo.WallUpdate_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM WallUpdate
		WHERE Id=@Id;

	RETURN