CREATE PROCEDURE [dbo].[ArticleLink_Set]
	(
		@Id int = -1,
		@Rank int,
		@Style nvarchar(250),
		@ObjectId int,
		@RecordId int,
		@ArticleId int,
		@SiteId int,
		@CommentOn int
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		BEGIN
			-- Update
			
			UPDATE    ArticleLink
			SET              Rank = @Rank, Style = @Style, ObjectId = @ObjectId, RecordId = @RecordId, ArticleId = @ArticleId, 
						SiteId = @SiteId, CommentOn=@CommentOn
			WHERE     (Id = @Id);
		END
	ELSE
		BEGIN
			-- Insert
			
			EXEC @Id=WebObjects_NextId 'ArticleLink';
			
			INSERT INTO ArticleLink
			                      (Rank, Style, ObjectId, RecordId, ArticleId, Id, SiteId, CommentOn)
			VALUES     (@Rank,@Style,@ObjectId,@RecordId,@ArticleId,@Id,@SiteId, @CommentOn);
		END
		
	SELECT @Id;
	
	RETURN