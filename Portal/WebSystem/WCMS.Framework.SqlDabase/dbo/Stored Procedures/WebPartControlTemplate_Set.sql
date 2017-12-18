CREATE PROCEDURE [dbo].[WebPartControlTemplate_Set]
	(
		@PartControlTemplateId int = -1,
		@PartControlId int,
		@Name nvarchar(250),
		@FileName nvarchar(250),
		@Identity nvarchar(250),
		@Path nvarchar(250),
		@Standalone int,
		@TemplateEngineId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PartControlTemplateId > 0)
		BEGIN
			-- Update
			
			UPDATE    WebPartControlTemplate
			SET              PartControlId = @PartControlId, Name = @Name, FileName = @FileName, [Identity] = @Identity, 
				Path = @Path, Standalone=@Standalone, TemplateEngineId=@TemplateEngineId
			WHERE     (PartControlTemplateId = @PartControlTemplateId)
		END
	ELSE
		BEGIN
			-- Insert
			
			EXEC @PartControlTemplateId = WebObject_NextId 'WebPartControlTemplate';
			
			INSERT INTO WebPartControlTemplate
			        (PartControlId, Name, FileName, [Identity], PartControlTemplateId, Path, Standalone, TemplateEngineId)
			VALUES  (@PartControlId,@Name,@FileName,@Identity,@PartControlTemplateId, @Path, @Standalone, @TemplateEngineId)
		END
		
	SELECT @PartControlTemplateId;
	
	RETURN