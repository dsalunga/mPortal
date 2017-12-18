
-- Procedure WebSiteIdentity_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebSiteIdentity_Set
	(
		@Id int = -1,
		@SiteId int,
		@HostName nvarchar(256),
		@UrlPath nvarchar(256),
		@Port int,
		@IPAddress nvarchar(50),
		@RedirectUrl nvarchar(500),
		@ProtocolId int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    WebSiteIdentity
			SET              SiteId = @SiteId, HostName = @HostName, UrlPath = @UrlPath, Port = @Port, IPAddress = @IPAddress,
				RedirectUrl=@RedirectUrl, ProtocolId=@ProtocolId
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id=WebObject_NextId 'WebSiteIdentity';

			INSERT INTO WebSiteIdentity
								  (Id, SiteId, HostName, UrlPath, Port, IPAddress, RedirectUrl, ProtocolId)
			VALUES     (@Id,@SiteId,@HostName,@UrlPath,@Port,@IPAddress, @RedirectUrl, @ProtocolId)
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

