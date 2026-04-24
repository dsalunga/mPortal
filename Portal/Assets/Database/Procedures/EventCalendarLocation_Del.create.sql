
-- Procedure EventCalendarLocation_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.EventCalendarLocation_Del
	(
		@LocationId int
	)
AS
	SET NOCOUNT ON

	IF(@LocationId > 0)
		DELETE FROM EventCalendarLocations
		WHERE LocationId = @LocationId;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

