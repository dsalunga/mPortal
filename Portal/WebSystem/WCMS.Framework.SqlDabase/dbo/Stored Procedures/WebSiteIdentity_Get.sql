CREATE PROCEDURE dbo.WebSiteIdentity_Get
	(
		@Id int = -1,
		@SiteId int = -1
	)
AS
	SET NOCOUNT ON

	SELECT     Id, SiteId, HostName, UrlPath, Port, IPAddress, RedirectUrl
	FROM         WebSiteIdentity
	WHERE     (@Id = -1 OR Id = @Id)
			AND (@SiteId = -1 OR SiteId=@SiteId)

	RETURN