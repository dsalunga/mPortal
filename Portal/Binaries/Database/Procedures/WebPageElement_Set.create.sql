
-- Procedure WebPageElement_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPageElement_Set]
	(
		@PageElementId int = -1,
		@RecordId int = -1,
		@ObjectId int = -1,
		@Name nvarchar(256),
		@TemplatePanelId int = -1,
		@Rank int,
		@PartControlTemplateId int = -1,
		@UsePartTemplatePath int,
		@Active int,
		@PublicAccess int,
		@ManagementAccess int
	)
AS
	SET NOCOUNT ON
	
	if(@PageElementId > 0)
		BEGIN
			-- Update
			
			UPDATE    WebPageElement
			SET              RecordId = @RecordId, Name = @Name, TemplatePanelId = @TemplatePanelId, Rank = @Rank, PartControlTemplateId = @PartControlTemplateId, 
			                      Active = @Active, ObjectId = @ObjectId, UsePartTemplatePath = @UsePartTemplatePath, PublicAccess=@PublicAccess,
								  ManagementAccess=@ManagementAccess
			WHERE     (PageElementId = @PageElementId)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @PageElementId = WebObjects_NextId 'WebPageElement'
			
			INSERT INTO WebPageElement
			                         (RecordId, Name, TemplatePanelId, Rank, PartControlTemplateId, Active, PageElementId, ObjectId, UsePartTemplatePath, PublicAccess,
										ManagementAccess)
			VALUES        (@RecordId,@Name,@TemplatePanelId,@Rank,@PartControlTemplateId,@Active,@PageElementId, @ObjectId, @UsePartTemplatePath, @PublicAccess,
							@ManagementAccess)
		END
	
	SELECT @PageElementId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

