CREATE PROCEDURE dbo.WebSiteIdentity_Set
	(
		@Id int = -1,
		@SiteId int,
		@HostName nvarchar(256),
		@UrlPath nvarchar(256),
		@Port int,
		@IPAddress nvarchar(50),
		@RedirectUrl nvarchar(500)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    WebSiteIdentity
			SET              SiteId = @SiteId, HostName = @HostName, UrlPath = @UrlPath, Port = @Port, IPAddress = @IPAddress,
				RedirectUrl=@RedirectUrl
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id=WebObject_NextId 'WebSiteIdentity';

			INSERT INTO WebSiteIdentity
								  (Id, SiteId, HostName, UrlPath, Port, IPAddress, RedirectUrl)
			VALUES     (@Id,@SiteId,@HostName,@UrlPath,@Port,@IPAddress, @RedirectUrl)
		END

	SELECT @Id;

	RETURN