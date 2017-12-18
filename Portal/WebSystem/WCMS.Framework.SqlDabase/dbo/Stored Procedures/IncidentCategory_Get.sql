CREATE PROCEDURE dbo.IncidentCategory_Get
	(
		@Id int = -1
	)
AS
	SET NOCOUNT ON

	SELECT     Id, Name, GroupId, Description, Rank
	FROM         IncidentCategory
	WHERE     (@Id = -1 OR Id = @Id)
	ORDER BY Rank, [Name]

	RETURN