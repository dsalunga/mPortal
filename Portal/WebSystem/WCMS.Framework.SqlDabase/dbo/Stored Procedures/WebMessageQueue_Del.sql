CREATE PROCEDURE dbo.WebMessageQueue_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM WebMessageQueue
		WHERE Id=@Id;

	RETURN