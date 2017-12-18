
-- Procedure WebSiteIdentity_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebSiteIdentity_Get
	(
		@Id int = -1,
		@SiteId int = -1
	)
AS
	SET NOCOUNT ON

	SELECT     Id, SiteId, HostName, UrlPath, Port, IPAddress, RedirectUrl,
		ProtocolId
	FROM         WebSiteIdentity
	WHERE     (@Id = -1 OR Id = @Id)
			AND (@SiteId = -1 OR SiteId=@SiteId)

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

