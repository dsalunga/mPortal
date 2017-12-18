CREATE PROCEDURE dbo.WebFile_Set
	(
		@FileId int = -1,
		@FolderId int,
		@Name nvarchar(250),
		@ObjectId int,
		@RecordId int
	)
AS
	SET NOCOUNT ON
	
	IF(@FileId > 0)
		BEGIN
			-- Update
			
			UPDATE       WebFile
			SET                FolderId = @FolderId, ObjectId = @ObjectId, RecordId = @RecordId, Name = @Name
			WHERE (FileId = @FileId)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @FileId = WebObject_NextId 'WebFile'
			
			INSERT INTO WebFile
			                         (FileId, FolderId, ObjectId, RecordId, Name)
			VALUES        (@FileId,@FolderId,@ObjectId,@RecordId,@Name)
		END
		
	SELECT @FileId
	
	RETURN