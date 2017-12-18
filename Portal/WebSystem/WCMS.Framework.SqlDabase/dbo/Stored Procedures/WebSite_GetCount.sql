CREATE PROCEDURE [dbo].[WebSite_GetCount]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
	SET NOCOUNT ON
	
	SELECT COUNT(*) FROM WebSite
	
	RETURN