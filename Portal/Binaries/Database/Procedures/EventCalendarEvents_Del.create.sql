
-- Procedure EventCalendarEvents_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventCalendarEvents_Del]
	(
		@EventId int
	)
AS
	SET NOCOUNT ON
	
	IF(@EventId > 0)
		BEGIN
			DELETE FROM EventCalendarEvents
			WHERE EventId=@EventId
		END
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

