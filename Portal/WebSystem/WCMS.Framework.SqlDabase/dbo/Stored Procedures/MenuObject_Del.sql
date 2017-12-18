CREATE PROCEDURE dbo.MenuObject_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM MenuObject
		WHERE Id=@Id;

	RETURN