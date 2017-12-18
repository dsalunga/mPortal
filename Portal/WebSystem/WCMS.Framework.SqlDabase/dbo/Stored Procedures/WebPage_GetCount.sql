CREATE PROCEDURE [dbo].[WebPage_GetCount]
	(
		@SiteId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT COUNT(*) FROM WebPage
		WHERE @SiteId=-1 OR SiteId = @SiteId
	
	RETURN