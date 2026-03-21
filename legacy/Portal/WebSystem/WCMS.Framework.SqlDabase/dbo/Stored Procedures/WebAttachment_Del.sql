CREATE PROCEDURE dbo.WebAttachment_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM WebAttachment
		WHERE Id=@Id;

	RETURN