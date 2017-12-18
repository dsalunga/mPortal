CREATE PROCEDURE [dbo].[WebMasterPage_Set]
	(
		@MasterPageId int = -1,
		@SiteId int = -1,
		@TemplateId int = -1,
		@Name nvarchar(256),
		@PublicAccess int,
		@OwnerPageId int,
		@ManagementAccess int,
		@ThemeId int,
		@SkinId int
	)
AS
	SET NOCOUNT ON
	
	if(@MasterPageId > 0)
		BEGIN
			-- Update
			
			UPDATE    WebMasterPage
			SET              SiteId = @SiteId, TemplateId = @TemplateId, Name = @Name, PublicAccess=@PublicAccess,
							 OwnerPageId=@OwnerPageId, ManagementAccess=@ManagementAccess, ThemeId=@ThemeId,
							 SkinId=@SkinId
			WHERE     (MasterPageId = @MasterPageId)
		END
	ELSE
		BEGIN
			-- Insert
			
			EXEC @MasterPageId = WebObject_NextId 'WebMasterPage';
			
			INSERT INTO WebMasterPage
			            (SiteId, TemplateId, Name, MasterPageId,PublicAccess,OwnerPageId, ManagementAccess,
						ThemeId, SkinId)
			VALUES		(@SiteId,@TemplateId,@Name,@MasterPageId,@PublicAccess,@OwnerPageId, @ManagementAccess,
						@ThemeId, @SkinId)
		END
		
	SELECT @MasterPageId;
	
	RETURN