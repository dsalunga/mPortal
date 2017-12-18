
-- Procedure EventCalendarRecurrences_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventCalendarRecurrences_Get]
	(
		@RecurrenceId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     RecurrenceId, Name, Rank
	FROM         EventCalendarRecurrences
	WHERE     (@RecurrenceId = - 1) OR
	                      (RecurrenceId = @RecurrenceId)
	ORDER BY Rank
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

