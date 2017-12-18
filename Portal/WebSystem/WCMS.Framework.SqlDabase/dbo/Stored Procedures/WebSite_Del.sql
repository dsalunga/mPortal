CREATE PROCEDURE [dbo].[WebSite_Del]
	(
		@SiteId int
	)
AS
	SET NOCOUNT ON
	
	if(@SiteId > 0)
		BEGIN
			DELETE FROM WebSite
			WHERE SiteId=@SiteId
		END
	
	RETURN