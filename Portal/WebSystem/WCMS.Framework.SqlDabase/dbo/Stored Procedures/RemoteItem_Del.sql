CREATE PROCEDURE dbo.RemoteItem_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM RemoteItem
		WHERE Id=@Id

	RETURN