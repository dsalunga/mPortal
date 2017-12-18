CREATE PROCEDURE [dbo].[WebSite_Get]
	(
		@SiteId int = -1,
		@ParentId int = -2
	)
AS
	SET NOCOUNT ON
	
	SELECT     SiteId, Name, Rank, Active, [Identity], Title, ParentId, HomePageId, DefaultMasterPageId, 
				HostName, PublicAccess, LoginPage, AccessDeniedPage, PageTitleFormat, ManagementAccess,
				BaseAddress, ThemeId, SkinId
	FROM         WebSite
	WHERE     (@SiteId = - 1 OR
	                      SiteId = @SiteId) AND (@ParentId = - 2 OR
	                      ParentId = @ParentId)
	ORDER BY Rank
	
	RETURN