
-- Procedure WebPartConfig_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPartConfig_Get]
	(
		@PartConfigId int = -1,
		@PartId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT        PartConfigId, PartId, Name, FileName
	FROM            WebPartConfig
	WHERE        
		(@PartConfigId=-1 OR PartConfigId = @PartConfigId)
		AND
		(@PartId=-1 OR PartId=@PartId)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

