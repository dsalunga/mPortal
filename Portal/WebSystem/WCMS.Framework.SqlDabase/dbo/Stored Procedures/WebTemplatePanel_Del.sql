CREATE PROCEDURE [dbo].[WebTemplatePanel_Del]
	(
		@TemplatePanelId int
	)
AS
	SET NOCOUNT ON
	
	if(@TemplatePanelId > 0)
		BEGIN
			DELETE FROM WebTemplatePanel
			WHERE TemplatePanelId = @TemplatePanelId
		END
	
	RETURN