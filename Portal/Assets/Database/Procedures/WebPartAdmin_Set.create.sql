
-- Procedure WebPartAdmin_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPartAdmin_Set]
	(
		@PartAdminId int = -1,
		@PartId int,
		@Name nvarchar(250),
		@FileName nvarchar(250),
		@ParentId int,
		@Active int,
		@Visible int,
		@InSiteContext int,
		@TemplateEngineId int,
		@AutoTitle int
	)
AS
	SET NOCOUNT ON
	
	IF(@PartAdminId > 0)
		BEGIN
			-- Update
			
			UPDATE    WebPartAdmin
			SET         PartId = @PartId, Name = @Name, FileName = @FileName, ParentId = @ParentId,
						Active=@Active, Visible=@Visible, InSiteContext=@InSiteContext, TemplateEngineId=@TemplateEngineId,
						AutoTitle=@AutoTitle
			WHERE     (PartAdminId = @PartAdminId)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @PartAdminId = WebObject_NextId 'WebPartAdmin';
			
			INSERT INTO WebPartAdmin
			            (PartId, Name, FileName, ParentId, PartAdminId, Active, Visible, InSiteContext, TemplateEngineId,
						AutoTitle)
			VALUES		(@PartId,@Name,@FileName,@ParentId,@PartAdminId, @Active, @Visible, @InSiteContext, @TemplateEngineId,
						@AutoTitle)
		END
		
	SELECT @PartAdminId;
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

