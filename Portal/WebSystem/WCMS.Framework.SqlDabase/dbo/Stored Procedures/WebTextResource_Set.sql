CREATE PROCEDURE [dbo].[WebTextResource_Set]
	(
		@TextResourceId int = -1,
		@ContentTypeId int,
		@Title nvarchar(250),
		@Content ntext,
		@DirectoryId int,
		@Rank int,
		@DatePersisted datetime = NULL,
		@PhysicalPath nvarchar(500)
	)
AS
	SET NOCOUNT ON
	
	IF(@DatePersisted IS NULL)
		SET @DatePersisted = GETDATE()
	
	IF(@TextResourceId < 1)
		BEGIN
			-- Insert
			EXEC @TextResourceId = WebObjects_NextId 'WebTextResource'
			
			INSERT INTO WebTextResource
			                         (ContentTypeId, [Content], Title, TextResourceId, DirectoryId, Rank, DatePersisted,
									 PhysicalPath)
			VALUES        (@ContentTypeId,@Content,@Title,@TextResourceId,@DirectoryId, @Rank, @DatePersisted,
							@PhysicalPath)
			
			--SELECT IDENT_CURRENT('WebTextResources')
		END
	ELSE
		BEGIN
			-- Update
			
			UPDATE       WebTextResource
			SET                ContentTypeId = @ContentTypeId, [Content] = @Content, Title = @Title, 
								DirectoryId = @DirectoryId, Rank=@Rank, DateModified=GETDATE(), 
								DatePersisted = @DatePersisted, PhysicalPath=@PhysicalPath
			WHERE        (TextResourceId = @TextResourceId)
		END
	
	SELECT @TextResourceId
	
	RETURN