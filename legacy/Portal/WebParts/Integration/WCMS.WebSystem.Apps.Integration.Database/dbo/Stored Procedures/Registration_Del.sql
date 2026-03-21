CREATE PROCEDURE dbo.Registration_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM Registration
		WHERE Id=@Id;

	RETURN