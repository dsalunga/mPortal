CREATE PROCEDURE dbo.WallPlugin_Get
	(
		@Id int = -1,
		@EventTypeId int = -1
	)
AS
	SET NOCOUNT ON

	SELECT        Id, Name, EventTypeId, FileName, TypeName
	FROM            WallPlugin
	WHERE        (@Id=-1 OR Id = @Id)
		AND (@EventTypeId=-1 OR EventTypeId=@EventTypeId)

	RETURN