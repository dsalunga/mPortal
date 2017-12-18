CREATE PROCEDURE dbo.IncidentType_Get
	(
		@Id int = -1
	)
AS
	SET NOCOUNT ON

	SELECT     Id, Name, FollowStdSLA, Rank
	FROM         IncidentType
	WHERE     (@Id = -1 OR Id = @Id)
	ORDER BY Rank, [Name]

	RETURN