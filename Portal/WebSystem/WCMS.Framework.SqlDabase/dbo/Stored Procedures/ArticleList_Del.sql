CREATE PROCEDURE [dbo].[ArticleList_Del]
	(
		@ListId int
	)
AS
	SET NOCOUNT ON
	
	IF(@ListId > 0)
		DELETE FROM ArticleList
		WHERE ListId=@ListId
	
	RETURN