CREATE PROCEDURE [dbo].[WebSite_Set]
	(
		@SiteId int = -1,
		@Name nvarchar(256),
		@Rank int,
		@Active int,
		@Identity nvarchar(64),
		@Title nvarchar(256),
		@ParentId int = -1,
		@HomePageId int = -1,
		@DefaultMasterPageId int = -1,
		@HostName nvarchar(256) = null,
		@PublicAccess int,
		@LoginPage nvarchar(250) = '',
		@AccessDeniedPage nvarchar(250) = '',
		@PageTitleFormat nvarchar(250) = '',
		@ManagementAccess int,
		@BaseAddress nvarchar(500),
		@SkinId int,
		@ThemeId int
	)
AS
	SET NOCOUNT ON
	
	if(@SiteId = -1)
		BEGIN
			-- Insert
			EXEC @SiteId = WebObjects_NextId 'WebSite'
			
			INSERT INTO WebSite
						(Name, Rank, Active, [Identity], Title, ParentId, HomePageId, DefaultMasterPageId, HostName, 
							SiteId,PublicAccess, LoginPage, AccessDeniedPage, PageTitleFormat, ManagementAccess,
							BaseAddress, SkinId, ThemeId)
			VALUES      (@Name,@Rank,@Active,@Identity,@Title,@ParentId,@HomePageId,@DefaultMasterPageId,@HostName,
							@SiteId,@PublicAccess, @LoginPage, @AccessDeniedPage, @PageTitleFormat, @ManagementAccess,
							@BaseAddress, @SkinId, @ThemeId)
			
			--SELECT @@IDENTITY
		END
	ELSE
		BEGIN
			-- Update
			UPDATE    WebSite
			SET              Name = @Name, Rank = @Rank, Active = @Active, [Identity] = @Identity, Title = @Title, 
								ParentId = @ParentId, HomePageId = @HomePageId, DefaultMasterPageId = @DefaultMasterPageId, 
								HostName = @HostName, PublicAccess=@PublicAccess, LoginPage=@LoginPage, AccessDeniedPage=@AccessDeniedPage,
								PageTitleFormat=@PageTitleFormat, ManagementAccess=@ManagementAccess, BaseAddress=@BaseAddress,
								SkinId=@SkinId, ThemeId=@ThemeId
			WHERE     (SiteId = @SiteId)
		END
	
	SELECT @SiteId
	
	RETURN