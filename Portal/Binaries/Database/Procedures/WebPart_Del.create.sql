
-- Procedure WebPart_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPart_Del]
	(
		@PartId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PartId > 0)
		DELETE FROM WebPart
		WHERE PartId=@PartId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

