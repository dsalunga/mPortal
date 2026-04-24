
-- Procedure WebMasterPage_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
		@SkinId int,
		@ParentId int
	)
AS
	SET NOCOUNT ON
	
	if(@MasterPageId > 0)
		BEGIN
			-- Update
			
			UPDATE    WebMasterPage
			SET              SiteId = @SiteId, TemplateId = @TemplateId, Name = @Name, PublicAccess=@PublicAccess,
							 OwnerPageId=@OwnerPageId, ManagementAccess=@ManagementAccess, ThemeId=@ThemeId,
							 SkinId=@SkinId, ParentId=@ParentId
			WHERE     (MasterPageId = @MasterPageId)
		END
	ELSE
		BEGIN
			-- Insert
			
			EXEC @MasterPageId = WebObject_NextId 'WebMasterPage';
			
			INSERT INTO WebMasterPage
			            (SiteId, TemplateId, Name, MasterPageId,PublicAccess,OwnerPageId, ManagementAccess,
						ThemeId, SkinId, ParentId)
			VALUES		(@SiteId,@TemplateId,@Name,@MasterPageId,@PublicAccess,@OwnerPageId, @ManagementAccess,
						@ThemeId, @SkinId, @ParentId)
		END
		
	SELECT @MasterPageId;
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

