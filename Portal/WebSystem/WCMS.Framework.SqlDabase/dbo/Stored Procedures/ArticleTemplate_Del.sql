CREATE PROCEDURE dbo.ArticleTemplate_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM ArticleTemplate
		WHERE Id=@Id;

	RETURN