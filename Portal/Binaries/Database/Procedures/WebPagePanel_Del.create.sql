
-- Procedure WebPagePanel_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPagePanel_Del]
	(
		@PagePanelId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PagePanelId > 0)
		DELETE FROM WebPagePanel
		WHERE PagePanelId=@PagePanelId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

