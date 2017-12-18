CREATE PROCEDURE [dbo].[WebMasterPage_Get]
	(
		@MasterPageId int = -1,
		@SiteId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     MasterPageId, SiteId, TemplateId, Name, PublicAccess, OwnerPageId, ManagementAccess,
				ThemeId, SkinId
	FROM         WebMasterPage
	WHERE     (@MasterPageId = - 1 OR
	                      MasterPageId = @MasterPageId)
	       AND (@SiteId = -1 OR SiteId = @SiteId)
	
	RETURN