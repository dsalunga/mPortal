CREATE PROCEDURE dbo.BibleReaderAccess_Get
	(
		@Id int = -1,
		@UserId int = -1
	)
AS
	SET NOCOUNT ON

	SELECT        Id, UserId, AppAccessCount, LastAccessed
	FROM            BibleReaderAccess
	WHERE        (@Id = -1 OR Id = @Id)
		AND (@UserId = -1 OR UserId=@UserId)

	RETURN