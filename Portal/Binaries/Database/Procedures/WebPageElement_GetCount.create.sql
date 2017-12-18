
-- Procedure WebPageElement_GetCount
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPageElement_GetCount]
	(
		@RecordId int = -1,
		@ObjectId int = -1,
		@TemplatePanelId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT COUNT(1) FROM WebPageElement
	WHERE	
			(@RecordId = -1 OR RecordId = @RecordId)
		AND (@TemplatePanelId = -1 OR TemplatePanelId = @TemplatePanelId)
		AND (@ObjectId = -1 OR ObjectId = @ObjectId)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

