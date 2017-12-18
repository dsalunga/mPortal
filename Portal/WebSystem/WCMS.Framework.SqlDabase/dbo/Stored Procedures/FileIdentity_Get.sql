CREATE PROCEDURE dbo.FileIdentity_Get
	(
		@Id int = -1,
		@LibraryId int = -1,
		@ObjectId int =-1,
		@RecordId int =-1,
		@FilePath nvarchar(4000) = NULL,
		@Name nvarchar(500) = NULL
	)
AS
	SET NOCOUNT ON

	SELECT        Id, ObjectId, RecordId, LibraryId, FilePath, Name
	FROM            FileIdentity
	WHERE        (@Id = -1 OR Id = @Id) 
		AND (@LibraryId =-1 OR LibraryId = @LibraryId) 
		AND (@FilePath IS NULL OR FilePath = @FilePath)
		AND (@Name IS NULL OR Name=@Name)
		AND (@ObjectId =-1 OR ObjectId=@ObjectId)
		AND (@RecordId = -1 OR RecordId=@RecordId)

	RETURN