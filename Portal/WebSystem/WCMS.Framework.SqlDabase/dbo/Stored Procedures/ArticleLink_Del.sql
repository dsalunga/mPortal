CREATE PROCEDURE [dbo].[ArticleLink_Del]
	(
		@Id int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		DELETE FROM ArticleLink
		WHERE Id=@Id
	
	RETURN