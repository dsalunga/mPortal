CREATE PROCEDURE dbo.WebComment_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM WebComment
		WHERE Id=@Id;

	RETURN