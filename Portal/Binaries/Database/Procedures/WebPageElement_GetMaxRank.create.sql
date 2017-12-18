
-- Procedure WebPageElement_GetMaxRank
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPageElement_GetMaxRank]
	(
		@RecordId int,
		@ObjectId int,
		@TemplatePanelId int	
	)
AS
	SET NOCOUNT ON
	
	SELECT MAX(Rank) 
	FROM WebPageElement
	WHERE	
			RecordId = @RecordId
		AND TemplatePanelId = @TemplatePanelId
		AND ObjectId = @ObjectId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

