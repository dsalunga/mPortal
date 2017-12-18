
-- Procedure WebPartConfig_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPartConfig_Del]
	(
		@PartConfigId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PartConfigId > 0)
		DELETE FROM WebPartConfig
		WHERE PartConfigId=@PartConfigId
		
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

