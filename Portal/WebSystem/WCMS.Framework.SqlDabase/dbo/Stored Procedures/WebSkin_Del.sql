CREATE PROCEDURE dbo.WebSkin_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM WebSkin
		WHERE Id=@Id;

	RETURN