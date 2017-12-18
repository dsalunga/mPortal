CREATE PROCEDURE [dbo].[WebObjectHeader_Set]
	(
		@ObjectHeaderId int = -1,
		@ObjectId int,
		@RecordId int,
		@TextResourceId int
	)
AS
	SET NOCOUNT ON
	
	IF(@ObjectHeaderId < 1)
		BEGIN
			-- Insert
			EXEC @ObjectHeaderId = WebObjects_NextId 'WebObjectHeader'
			
			INSERT INTO WebObjectHeader
			                         (ObjectId, RecordId, TextResourceId, ObjectHeaderId)
			VALUES        (@ObjectId,@RecordId,@TextResourceId,@ObjectHeaderId)
			
			--SELECT IDENT_CURRENT('WebObjectHeaders')
		END
	ELSE
		BEGIN
			-- Update
			
			UPDATE    WebObjectHeader
			SET              ObjectId = @ObjectId, RecordId = @RecordId, TextResourceId = @TextResourceId
			WHERE     (ObjectHeaderId = @ObjectHeaderId)
		END
	
	SELECT @ObjectHeaderId
	
	RETURN