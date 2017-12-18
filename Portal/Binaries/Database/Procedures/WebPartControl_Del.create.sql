
-- Procedure WebPartControl_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPartControl_Del]
	(
		@PartControlId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PartControlId > 0)
		DELETE FROM WebPartControl
		WHERE PartControlId=@PartControlId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

