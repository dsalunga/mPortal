CREATE PROCEDURE dbo.FileIdentity_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM FileIdentity
		WHERE Id=@Id;

	RETURN