CREATE PROCEDURE dbo.RemoteLibrary_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM RemoteLibrary
		WHERE Id=@Id

	RETURN