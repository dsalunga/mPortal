
-- Procedure EventCalendarLocations_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventCalendarLocations_Get]
	(
		@LocationId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     LocationId, Name, Bookable
	FROM         EventCalendarLocations
	WHERE
		(@LocationId = -1 OR
			LocationId=@LocationId)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

