CREATE PROCEDURE dbo.FileVersion_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM FileVersion
		WHERE Id=@Id;

	RETURN