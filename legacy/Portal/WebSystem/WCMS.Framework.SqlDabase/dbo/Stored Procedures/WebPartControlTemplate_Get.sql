CREATE PROCEDURE [dbo].[WebPartControlTemplate_Get]
	(
		@PartControlTemplateId int = -1,
		@PartControlId int = -1,
		@Identity nvarchar(256) = NULL,
		@Standalone INT = -1,
		@TemplateEngineId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     PartControlTemplateId, PartControlId, Name, FileName, [Identity], Path, Standalone,
		TemplateEngineId
	FROM         WebPartControlTemplate
	WHERE	(@PartControlTemplateId = -1 OR PartControlTemplateId = @PartControlTemplateId)
		AND	(@PartControlId = -1 OR PartControlId = @PartControlId)
		AND (@Identity IS NULL OR [Identity]=@Identity)
		AND (@Standalone = -1 OR Standalone=@Standalone)
		AND (@TemplateEngineId = -1 OR TemplateEngineId=@TemplateEngineId)
	
	RETURN