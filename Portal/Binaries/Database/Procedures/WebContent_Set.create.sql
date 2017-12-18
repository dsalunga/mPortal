
-- Procedure WebContent_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebContent_Set]
	(
		@ContentId int = -1,
		@Title nvarchar(256),
		@Content ntext,
		@VersionOf int,
		@VersionNo int,
		@DirectoryId int,
		@Active int,
		@SiteId int,
		@EditorSensitive int,
		@ActiveContent int
	)
AS
	SET NOCOUNT ON
	
	if(@ContentId > 0)
		BEGIN
			-- Update
			UPDATE    WebContent
			SET              Title = @Title, Content = @Content, VersionOf = @VersionOf, VersionNo = @VersionNo,
							DirectoryId = @DirectoryId, Active=@Active, DateModified=GETDATE(), SiteId=@SiteId,
							EditorSensitive=@EditorSensitive, ActiveContent=@ActiveContent
			WHERE     (ContentId = @ContentId)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @ContentId = WebObject_NextId 'WebContent';
			
			INSERT INTO WebContent
			                      (Title, Content, VersionOf, VersionNo, ContentId, DirectoryId, Active, DateModified,
								SiteId, EditorSensitive, ActiveContent)
			VALUES     (@Title,@Content,@VersionOf,@VersionNo,@ContentId,@DirectoryId, @Active, GETDATE(), @SiteId,
						@EditorSensitive, @ActiveContent)
		END
	
	SELECT @ContentId;
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

