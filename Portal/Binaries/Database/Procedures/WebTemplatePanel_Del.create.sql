
-- Procedure WebTemplatePanel_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

