CREATE PROCEDURE dbo.WebSiteIdentity_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM WebSiteIdentity
		WHERE Id=@Id;

	RETURN