CREATE PROCEDURE [dbo].[WebTemplatePanel_Get]
	(
		@TemplatePanelId int = -2,
		@RecordId int = -2,
		@ObjectId int = -2
	)
AS
	SET NOCOUNT ON
	
	SELECT     TemplatePanelId, Name, ObjectId, RecordId, PanelName, Rank
	FROM         WebTemplatePanel
	WHERE     (@TemplatePanelId = -2 OR
	                      TemplatePanelId = @TemplatePanelId)
		AND (@ObjectId=-2 OR ObjectId=@ObjectId)
		AND (@RecordId=-2 OR RecordId=@RecordId)
	
	RETURN