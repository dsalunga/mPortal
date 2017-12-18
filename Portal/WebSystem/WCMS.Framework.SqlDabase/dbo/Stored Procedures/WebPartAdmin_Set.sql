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
		@TemplateEngineId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PartAdminId > 0)
		BEGIN
			-- Update
			
			UPDATE    WebPartAdmin
			SET         PartId = @PartId, Name = @Name, FileName = @FileName, ParentId = @ParentId,
						Active=@Active, Visible=@Visible, InSiteContext=@InSiteContext, TemplateEngineId=@TemplateEngineId
			WHERE     (PartAdminId = @PartAdminId)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @PartAdminId = WebObject_NextId 'WebPartAdmin';
			
			INSERT INTO WebPartAdmin
			            (PartId, Name, FileName, ParentId, PartAdminId, Active, Visible, InSiteContext, TemplateEngineId)
			VALUES		(@PartId,@Name,@FileName,@ParentId,@PartAdminId, @Active, @Visible, @InSiteContext, @TemplateEngineId)
		END
		
	SELECT @PartAdminId;
	
	RETURN