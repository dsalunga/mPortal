CREATE PROCEDURE [dbo].[ArticleList_Get]
	(
		@ListId int = -1,
		@ObjectId int = -1,
		@RecordId int = -1,
		@TemplateId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     ListId, PageSize, ObjectId, RecordId, TemplateId, SiteId, FolderId,
			CommentOn
	FROM         ArticleList
	WHERE
		(@ListId=-1 OR ListId=@ListId)
		AND
		(@ObjectId =-1 OR @RecordId=-1 OR 
			(ObjectId=@ObjectId AND RecordId=@RecordId))
		AND
		(@TemplateId=-1 OR TemplateId=@TemplateId)
	
	RETURN