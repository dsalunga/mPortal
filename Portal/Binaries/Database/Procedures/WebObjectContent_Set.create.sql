
-- Procedure WebObjectContent_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebObjectContent_Set]
	(
		@ObjectContentId int = -1,
		@ObjectId int = -1,
		@ContentId int = -1,
		@RecordId int = -1
	)
AS
	SET NOCOUNT ON
	
	if(@ObjectContentId > 0)
		BEGIN
			-- Update
			
			UPDATE    WebObjectContent
			SET              ObjectId = @ObjectId, ContentId = @ContentId, RecordId = @RecordId
			WHERE     (ObjectContentId = @ObjectContentId)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @ObjectContentId = WebObjects_NextId 'WebObjectContent'
			
			INSERT INTO WebObjectContent
			                         (ObjectId, ContentId, RecordId, ObjectContentId)
			VALUES        (@ObjectId,@ContentId,@RecordId,@ObjectContentId)
			
			--SELECT IDENT_CURRENT('WebObjectContents')
		END
	
	SELECT @ObjectContentId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

