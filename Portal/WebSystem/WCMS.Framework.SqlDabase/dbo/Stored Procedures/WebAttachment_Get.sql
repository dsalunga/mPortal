CREATE PROCEDURE dbo.WebAttachment_Get
	(
		@Id int = -2,
		@UserId int = -2,
		@ObjectId int = -2,
		@RecordId int = -2,
		@BatchGuid nvarchar(50) = NULL
	)
AS
	SET NOCOUNT ON

	SELECT        Id, Name, FilePath, Size, DateUploaded, UserId, ObjectId, RecordId,
				BatchGuid
	FROM            WebAttachment
	WHERE   (@Id = -2 OR Id = @Id) 
		AND (@UserId = -2 OR UserId = @UserId) 
		AND (@ObjectId = -2 OR ObjectId = @ObjectId) 
		AND (@RecordId = -2 OR RecordId = @RecordId)
		AND (@BatchGuid IS NULL OR BatchGuid=@BatchGuid)
	                         

	RETURN