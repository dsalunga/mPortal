CREATE PROCEDURE [dbo].[WebPage_Set]
	(
		@PageId int = -1,
		@Name nvarchar(256),
		@SiteId int = -1,
		@Rank int,
		@Active int,
		@Identity nvarchar(256),
		@ParentId int = -1,
		@Title nvarchar(250),
		@MasterPageId int = -1,
		@PartControlTemplateId int = -1,
		@PublicAccess int,
		@PageType int = 0,
		@UsePartTemplatePath int = 1,
		@ManagementAccess int,
		@ThemeId int,
		@SkinId int
	)
AS
	SET NOCOUNT ON
	
	if(@PageId = -1)
		BEGIN
			-- Insert
			EXEC @PageId = WebObject_NextId 'WebPage';
			
			INSERT INTO WebPage
			                         (Name, SiteId, Rank, Active, [Identity], ParentId, Title, MasterPageId, PartControlTemplateId, PageId, 
										PublicAccess, PageType, UsePartTemplatePath, ManagementAccess, ThemeId, SkinId)
			VALUES        (@Name,@SiteId,@Rank,@Active,@Identity,@ParentId,@Title,@MasterPageId,@PartControlTemplateId,@PageId, @PublicAccess, 
							@PageType, @UsePartTemplatePath,@ManagementAccess, @ThemeId, @SkinId)
			
		END
	ELSE
		BEGIN
			-- Update
			
			UPDATE    WebPage
			SET              Name = @Name, SiteId = @SiteId, Rank = @Rank, Active = @Active, [Identity] = @Identity, ParentId = @ParentId, 
							Title = @Title, PublicAccess=@PublicAccess, MasterPageId = @MasterPageId, PartControlTemplateId = @PartControlTemplateId, 
								PageType = @PageType, UsePartTemplatePath = @UsePartTemplatePath,ManagementAccess=@ManagementAccess,
								ThemeId=@ThemeId, SkinId=@SkinId
			WHERE     (PageId = @PageId)
		END
		
	SELECT @PageId;
	
	
	RETURN