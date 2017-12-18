CREATE PROCEDURE dbo.WebFile_Get
	(
		@FileId int = -1,
		@FolderId int = -1,
		@ObjectId int = -1,
		@RecordId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT        FileId, FolderId, ObjectId, RecordId, Name
	FROM            WebFile
	WHERE	(@FileId = -1 OR FileId=@FileId) AND
			(@FolderId = -1 OR FolderId=@FolderId) AND
			(@ObjectId=-1 OR ObjectId=@ObjectId) AND
			(@RecordId=-1 OR RecordId=@RecordId)
	
	RETURN