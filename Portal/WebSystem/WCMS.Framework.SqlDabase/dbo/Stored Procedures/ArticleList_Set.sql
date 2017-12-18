CREATE PROCEDURE [dbo].[ArticleList_Set]
	(
		@ListId int = -1,
		@PageSize int,
		@ObjectId int,
		@RecordId int,
		@TemplateId int,
		@SiteId int,
		@FolderId int,
		@CommentOn int
	)
AS
	SET NOCOUNT ON
	
	IF(@ListId > 0)
		BEGIN
			-- Update
			
			UPDATE    ArticleList
			SET              PageSize = @PageSize, ObjectId = @ObjectId, RecordId = @RecordId, TemplateId = @TemplateId, 
			                      SiteId = @SiteId, FolderId = @FolderId, CommentOn=@CommentOn
			WHERE     (ListId = @ListId)
		END
	ELSE
		BEGIN
			-- Insert
			
			EXEC @ListId = WebObject_NextId 'ArticleList';
			
			INSERT INTO ArticleList
			                      (PageSize, ObjectId, RecordId, TemplateId, SiteId, ListId, FolderId, CommentOn)
			VALUES     (@PageSize, @ObjectId, @RecordId, @TemplateId, @SiteId, @ListId, @FolderId, @CommentOn)
		END
		
	SELECT @ListId
	
	RETURN