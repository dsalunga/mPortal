CREATE PROCEDURE [dbo].[WebPage_GetMaxRank]
	(
		@SiteId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT MAX(Rank) FROM WebPage
		WHERE @SiteId=-1 OR SiteId = @SiteId
	
	RETURN