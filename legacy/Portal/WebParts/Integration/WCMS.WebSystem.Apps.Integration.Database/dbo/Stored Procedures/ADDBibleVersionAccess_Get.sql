CREATE PROCEDURE dbo.BibleReaderVersionAccess_Get
	(
		@Id int = -1,
		@BibleAccessId int = -1
	)
AS
	SET NOCOUNT ON

	SELECT        Id, BibleAccessId, BibleVersionId, BibleVersionName, LastAccessed, VersionAccessCount
	FROM            BibleReaderVersionAccess
	WHERE        (@Id = -1 OR Id = @Id) AND (@BibleAccessId = -1 OR BibleAccessId = @BibleAccessId)

	RETURN