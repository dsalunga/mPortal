CREATE PROCEDURE dbo.WebShortUrl_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM WebShortUrl
		WHERE Id=@Id;

	RETURN