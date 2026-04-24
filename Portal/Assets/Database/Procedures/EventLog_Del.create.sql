
-- Procedure EventLog_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.EventLog_Del
	(
		@Id int = -1,
		@EventDate datetime = NULL
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0 OR @EventDate IS NOT NULL)
		DELETE FROM EventLog
		WHERE (@Id <=-1 OR Id=@Id)
			AND (@EventDate IS NULL OR EventDate<@EventDate);

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

