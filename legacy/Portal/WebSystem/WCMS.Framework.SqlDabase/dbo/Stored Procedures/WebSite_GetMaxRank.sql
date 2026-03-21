CREATE PROCEDURE [dbo].[WebSite_GetMaxRank]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
	SET NOCOUNT ON
	
	SELECT MAX(Rank) FROM WebSite
	
	RETURN