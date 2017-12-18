CREATE PROCEDURE [dbo].[WebPage_Get]
	(
		@PageId int = -1,
		@SiteId int = -1,
		@ParentId int = -2,
		@Identity nvarchar(250) = NULL
	)
AS
	SET NOCOUNT ON
	
	SELECT     PageId, Name, SiteId, Rank, Active, [Identity], ParentId, Title, MasterPageId, 
				PartControlTemplateId, PublicAccess, PageType, UsePartTemplatePath, ManagementAccess,
				ThemeId, SkinId
	FROM         WebPage
	WHERE     (@PageId = -1 OR
	                      PageId = @PageId) AND 
				(@SiteId = -1 OR
	                      SiteId = @SiteId) AND 
	            (@ParentId = -2 OR
	                      ParentId = @ParentId) AND
	            (@Identity IS NULL OR [Identity]=@Identity)
	ORDER BY ParentId, Rank
	
	RETURN